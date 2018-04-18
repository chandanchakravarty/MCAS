IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertQuote]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertQuote]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                            
Proc Name    : dbo.Proc_InsertQuote                           
Created by   : Ashwani                   
Date         : 20 April,2006                        
Purpose      : Insert the record into QOT_CUSTOMER_QUOTE_LIST Table                          
Revison History :                        
Used In  :   Wolverine                                  
                      
 ------------------------------------------------------------                                        
Date     Review By          Comments                                      
                             
------   ------------       -------------------------*/                           
     
create PROC dbo.Proc_InsertQuote                        
(                       
 @CUSTOMER_ID int,                        
 @APP_ID int,                        
 @APP_VERSION_ID int,                        
 @QUOTE_TYPE nvarchar(12),                 
 @QUOTE_DESCRIPTION varchar(125),                        
 @IS_ACCEPTED nchar(1)='N',                        
 @IS_ACTIVE nchar(1)='Y',                        
 @CREATED_BY int,                        
 @CREATED_DATETIME datetime,                        
 @QUOTE_XML text,                     
 @QUOTE_INPUT_XML text,               
 @QUOTE_ID int,    
 @RATE_EFFECTIVE_DATE datetime = NULL,
 @BUSINESS_TYPE varchar(15)  =NULL,             
 @QID int  output                        
)                        
AS                        
BEGIN                        
                        
----------------------------------------------------------------------------------------------------------------------              
              
 declare @QUOTE_VERSION_ID smallint                 
 declare @QUOTE_NUMBER nvarchar(75)        
        
 -- get the QUOTE_TYPe as LOB_CODE here         
 declare @APP_LOB nvarchar(10)        
 declare @LOB_CODE varchar(20)        
        
 if(@QUOTE_TYPE <> 'HOME-BOAT')        
 begin         
	SELECT @APP_LOB=APP_LOB FROM APP_LIST         
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
	 
	SELECT @QUOTE_TYPE=LOB_CODE FROM MNT_LOB_MASTER WHERE LOB_ID=@APP_LOB 
	                
	DELETE FROM QOT_CUSTOMER_QUOTE_LIST             
	WHERE CUSTOMER_ID = @CUSTOMER_ID  and APP_ID =@APP_ID and APP_VERSION_ID=@APP_VERSION_ID          
 end        
    
 else if(@QUOTE_TYPE = 'HOME-BOAT')         
 begin    
	  DELETE FROM QOT_CUSTOMER_QUOTE_LIST             
	  WHERE CUSTOMER_ID = @CUSTOMER_ID  and APP_ID =@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and QUOTE_TYPE='HOME-BOAT'          
 end    
    
 ---------------------------------------------------------------------------------------------        
    
 select @QUOTE_ID = isnull(max(QUOTE_ID),0)+1 from QOT_CUSTOMER_QUOTE_LIST where CUSTOMER_ID=@CUSTOMER_ID              
 and  APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID;             
    
      
 -- declare @QUOTE_VERSION_ID smallint                        
 select @QUOTE_VERSION_ID   = isnull(max(QUOTE_VERSION_ID),0)+1               
 from  QOT_CUSTOMER_QUOTE_LIST               
 where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and QUOTE_ID=@QUOTE_ID;                        
        
  -- declare @QUOTE_NUMBER nvarchar(75)                        
 select @QUOTE_NUMBER= 'Q-' + app_number               
 from APP_LIST              
 where CUSTOMER_ID = @CUSTOMER_ID  and APP_ID =@APP_ID and APP_VERSION_ID=@APP_VERSION_ID           
        
               
  INSERT INTO QOT_CUSTOMER_QUOTE_LIST                        
   (                        
	CUSTOMER_ID,           QUOTE_ID,           QUOTE_VERSION_ID,         APP_ID,                        
	APP_VERSION_ID,        QUOTE_TYPE,         QUOTE_NUMBER,             QUOTE_DESCRIPTION,                        
	IS_ACCEPTED,           IS_ACTIVE,          CREATED_BY,               CREATED_DATETIME,                        
	QUOTE_XML,             QUOTE_INPUT_XML,    RATE_EFFECTIVE_DATE,	     BUSINESS_TYPE                                  
   )                        
   VALUES                        
   (                         
	@CUSTOMER_ID,          @QUOTE_ID,    	   @QUOTE_VERSION_ID,          @APP_ID,               
	@APP_VERSION_ID,       @QUOTE_TYPE,        @QUOTE_NUMBER,              @QUOTE_DESCRIPTION,                        
	@IS_ACCEPTED,          @IS_ACTIVE,         @CREATED_BY,                @CREATED_DATETIME,                        
	@QUOTE_XML ,           @QUOTE_INPUT_XML,   @RATE_EFFECTIVE_DATE,       @BUSINESS_TYPE	                        
   )               
                             
  set @QID= @QUOTE_ID                        
  return @QUOTE_ID;                        
              
                
end                        
                      


GO

