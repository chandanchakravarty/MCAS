IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveLocation_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveLocation_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : Dbo.Proc_InsertLocation                    
Created by      : Pradeep                    
Date            : 8/12/05                    
Purpose       :Evaluation                    
Revison History :                    
Used In        : Wolverine                    
------------------------------------------------------------                    
Date      Review By          Comments                    
17,May 2005 Vijay     Checking the location no with respect to customer,application and veriosn only                    
------   ------------       -------------------------*/        
   
CREATE      PROC Dbo.Proc_SaveLocation_ACORD                    
(                    
 @CUSTOMER_ID int,                    
 @APP_ID      int,                    
 @APP_VERSION_ID smallint,                    
 @LOCATION_ID    smallint OUTPUT,                    
 @LOC_NUM      int,                    
 --@IS_PRIMARY     nchar(2),    
 @IS_PRIMARY     nchar(1),                    
 @LOC_ADD1      nvarchar(75),                    
 @LOC_ADD2      nvarchar(75),                    
 @LOC_CITY      nvarchar(75),                    
 @LOC_COUNTY     nvarchar(75)=null,                    
 @LOC_STATE      nvarchar(20),                    
 @LOC_ZIP      nvarchar(11),                    
-- @LOC_COUNTRY    nvarchar(10),    
 @LOC_COUNTRY    nvarchar(5),                                    
 @PHONE_NUMBER   nvarchar(20),                    
 @FAX_NUMBER     nvarchar(20),                    
 @DEDUCTIBLE     nvarchar(20),                    
 @NAMED_PERILL   int,                    
 @CREATED_BY     int,                    
 @CREATED_DATETIME datetime,                    
 @DESCRIPTION      varchar(1000)         ,  
 @LOCATION_TYPE int     
)                    
AS                    
BEGIN                    
 DECLARE @LOCATION_ID_EXISTS SmallInt                    
 DECLARE @STATE_ID Int                    
 DECLARE @CUST_ZIP nvarchar(11)     
     
                     
 EXECUTE @STATE_ID = Proc_GetSTATE_ID_FROM_NAME 1,@LOC_STATE                     

-----GET LOB ID----------
Declare @APP_LOB nvarchar(5)
Select @APP_LOB=APP_LOB from  APP_LIST 
 WHERE                     
  CUSTOMER_ID = @CUSTOMER_ID AND                    
  APP_ID = @APP_ID AND           
  APP_VERSION_ID = @APP_VERSION_ID       

/*IF @APP_LOB ='1' --home
BEGIN
 EXECUTE @LOCATION_TYPE = Proc_GetLookupID 'LOCTYP',@LOCATION_TYPE  
END

IF @APP_LOB ='6' --rental
BEGIN
 EXECUTE @LOCATION_TYPE = Proc_GetLookupID 'REN_LO',@LOCATION_TYPE  
END*/


---------------------             
          
/*          
 IF ( @LOC_COUNTY IS NULL OR @LOC_COUNTY = '')            
 BEGIN            
          
 EXEC @LOC_COUNTY = Proc_GetCOUNTY_FROM_ZIP @CUSTOMER_ID,            
        @APP_ID,            
        @APP_VERSION_ID,            
        @LOC_ZIP            
          
          
 END            
*/                   
          
            
--Get Customer address as location address---                
/*EXECUTE Proc_GetCUSTOMER_ADDRESS @CUSTOMER_ID,                
        @LOC_ADD1 OUTPUT,                
        @LOC_ADD2 OUTPUT,                
        @LOC_CITY OUTPUT,                
        @LOC_STATE OUTPUT,              
        @CUST_ZIP OUTPUT,                
        @LOC_COUNTRY OUTPUT              */  
  
  
SELECT  @LOC_ADD1 = CUSTOMER_ADDRESS1,  
        @LOC_ADD2 = CUSTOMER_ADDRESS2,  
        @LOC_CITY =  CUSTOMER_CITY,      
 @LOC_STATE =  CUSTOMER_STATE,      
 @CUST_ZIP = CUSTOMER_ZIP,  
 @LOC_COUNTRY = CUSTOMER_COUNTRY  
 FROM CLT_CUSTOMER_LIST  CLT ,  MNT_AGENCY_LIST MAL     
 where MAL.AGENCY_ID=CLT.CUSTOMER_AGENCY_ID                
 and CLT.CUSTOMER_ID  =  @CUSTOMER_ID    
  
/*EXECUTE PROC_GETCUSTOMERINFO @CUSTOMER_ID,                
        @LOC_ADD1 OUTPUT,                
        @LOC_ADD2 OUTPUT,                
        @LOC_CITY OUTPUT,                
        @LOC_STATE OUTPUT,              
        @CUST_ZIP OUTPUT,                
        @LOC_COUNTRY OUTPUT    */  
--------------------                
        
--Location Territory-------                  
IF ( @LOC_COUNTY IS NULL OR  @LOC_COUNTY = '' )                  
BEGIN                  
 EXEC Proc_GetCountyFromZip         
 @LOC_ZIP,                  
        @CUSTOMER_ID,                                      
        @APP_ID ,                                   
        @APP_VERSION_ID ,        
 @LOC_COUNTY OUTPUT                   
END                  
        ---------------                  
               
 --Check if location exists                    
 SELECT @LOCATION_ID_EXISTS = LOCATION_ID                    
 FROM APP_LOCATIONS                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
  APP_ID = @APP_ID AND                    
  APP_VERSION_ID = @APP_VERSION_ID AND                    
  LOC_ADD1 = @LOC_ADD1 AND                    
  LOC_ADD2 = @LOC_ADD2 AND                    
  LOC_CITY = @LOC_CITY AND                    
  LOC_STATE = @STATE_ID AND                    
  LOC_ZIP = @LOC_ZIP AND                
  DESCRIPTION = @DESCRIPTION                    
                    
 /*Checking the duplicay for Location number field*/                    
                    
 If ( @LOCATION_ID_EXISTS IS NULL )                    
 BEGIN                    
                    
  /*Generating the maximum Location id and setting it in output prarameter*/                    
  SELECT @LOCATION_ID = IsNull(Max(LOCATION_ID),0) + 1                     
  FROM APP_LOCATIONS                    
  WHERE                     
  CUSTOMER_ID = @CUSTOMER_ID AND                    
  APP_ID = @APP_ID AND           
  APP_VERSION_ID = @APP_VERSION_ID                    
                    
  SELECT @LOC_NUM = IsNull(Max(LOC_NUM),0) + 1                     
   FROM APP_LOCATIONS                    
  WHERE                     
  CUSTOMER_ID = @CUSTOMER_ID AND                    
  APP_ID = @APP_ID AND                    
  APP_VERSION_ID = @APP_VERSION_ID                    
                    
  INSERT INTO APP_LOCATIONS                    
  (                    
   CUSTOMER_ID,         
 APP_ID,         
 APP_VERSION_ID,         
 LOCATION_ID,                    
   LOC_NUM,        
  IS_PRIMARY,        
  LOC_ADD1,        
  LOC_ADD2,         
 LOC_CITY,                    
   LOC_COUNTY,        
  LOC_STATE,        
  LOC_ZIP,         
 LOC_COUNTRY,         
 PHONE_NUMBER,                    
   FAX_NUMBER,        
  DEDUCTIBLE,        
  NAMED_PERILL,                    
   IS_ACTIVE,         
 CREATED_BY,         
 CREATED_DATETIME,        
  [DESCRIPTION]                  ,  
LOCATION_TYPE  
  )                    
  VALUES                    
  (                    
   @CUSTOMER_ID,         
 @APP_ID,        
  @APP_VERSION_ID,        
  @LOCATION_ID,                    
   @LOC_NUM,        
  @IS_PRIMARY,        
  @LOC_ADD1,        
  @LOC_ADD2,        
  @LOC_CITY,                    
   @LOC_COUNTY,         
 @STATE_ID,         
 @LOC_ZIP,        
  @LOC_COUNTRY,        
  @PHONE_NUMBER,                    
   @FAX_NUMBER,         
 @DEDUCTIBLE,         
 @NAMED_PERILL,                    
   'Y',        
  @CREATED_BY,        
  @CREATED_DATETIME,        
 @DESCRIPTION,  
@LOCATION_TYPE                    
  )                    
                    
  RETURN @LOCATION_ID                    
 END                    
                    
 --Update                    
 UPDATE APP_LOCATIONS                    
  SET IS_PRIMARY = @IS_PRIMARY,                     
   LOC_ADD1 = @LOC_ADD1,                     
   LOC_ADD2 = @LOC_ADD2,                     
   LOC_CITY = @LOC_CITY,                    
   LOC_COUNTY = @LOC_COUNTY,                     
   LOC_STATE = @STATE_ID,                     
   LOC_ZIP = @LOC_ZIP,                     
   LOC_COUNTRY = @LOC_COUNTRY,                     
   PHONE_NUMBER = @PHONE_NUMBER,                    
   FAX_NUMBER = @FAX_NUMBER,                     
   DEDUCTIBLE = @DEDUCTIBLE,                     
   NAMED_PERILL = @NAMED_PERILL,                    
   [DESCRIPTION] = @DESCRIPTION                  ,  
LOCATION_TYPE = @LOCATION_TYPE  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
   APP_ID = @APP_ID AND         APP_VERSION_ID = @APP_VERSION_ID AND                    
   LOCATION_ID = @LOCATION_ID_EXISTS                    
                    
 SET @LOCATION_ID = @LOCATION_ID_EXISTS                    
                    
END                    
                    
                    
                    
     
                    
  
                
              
            
          
        
      
    
  
  
  





GO

