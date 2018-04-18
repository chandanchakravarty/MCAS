IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name    : dbo.Proc_InsertAdditionalInterest        
Created by   : Shrikant Bhatt            
Date         : 27 April.,2005           
Purpose     : Insert the record into  AdditionalInterest  Table        
Revison History :        
Created by   : Sumit Chhabra    
Date         : 22 November,2005           
Purpose     : Modified the parameter's data-types    
Used In  :   Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
-- drop proc dbo.Proc_InsertAdditionalInterest        
CREATE PROC dbo.Proc_InsertAdditionalInterest        
(        
 @CUSTOMER_ID int,    
 @APP_ID smallint,    
 @APP_VERSION_ID smallint,    
 @HOLDER_ID   int,        
 @VEHICLE_ID int,    
 @MEMO   NVARCHAR(500),        
 @NATURE_OF_INTEREST NVARCHAR(60),        
 @RANK   smallint,    
 @LOAN_REF_NUMBER NVARCHAR(150),        
 @CREATED_BY     int,              
 @CREATED_DATETIME     datetime,        
 @HOLDER_NAME nvarchar(140),        
 @HOLDER_ADD1 nvarchar(280),        
 @HOLDER_ADD2 nvarchar(280),        
 @HOLDER_CITY nvarchar(160),        
 @HOLDER_COUNTRY nvarchar(20),        
 @HOLDER_STATE nvarchar(20),        
 @HOLDER_ZIP varchar(11),      
 @IS_ACTIVE nchar(2)      
             
)        
AS        
        
DECLARE @MAX_ADD_INT int, @COUNT NUMERIC, @LOBID INT       
 BEGIN        
SET @COUNT=0        
 SELECT @LOBID=APP_LOB  FROM APP_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID     
     AND APP_VERSION_ID = @APP_VERSION_ID   
  
--Removed by Charles, IF(@LOBID<>2) block for Itrack Issue 5634 on 27-April-2009.  
  SELECT @COUNT = COUNT(RANK) FROM APP_ADD_OTHER_INT     
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID     
   AND APP_VERSION_ID = @APP_VERSION_ID AND RANK = @RANK         
	--Added by Charles on 12-May-2009 for Itrack 5634
	AND VEHICLE_ID= @VEHICLE_ID       

    IF (@COUNT >0 )    
   BEGIN    
   RETURN -1    
   END    
    
    ELSE         
 BEGIN        
          
  SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1        
  FROM APP_ADD_OTHER_INT        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
        APP_ID = @APP_ID AND        
   APP_VERSION_ID = @APP_VERSION_ID --AND        
   --VEHICLE_ID = @VEHICLE_ID  commented by mohit as creating primary key voilation.        
           
         
  IF ( @HOLDER_ID = 0 )        
  BEGIN        
   INSERT INTO APP_ADD_OTHER_INT        
   (        
    CUSTOMER_ID,        
    APP_ID,        
    APP_VERSION_ID,        
    VEHICLE_ID,        
    MEMO,        
    NATURE_OF_INTEREST,        
    RANK,        
    LOAN_REF_NUMBER,        
    CREATED_BY,         
    CREATED_DATETIME,        
    HOLDER_NAME,        
    HOLDER_ADD1,        
    HOLDER_ADD2,        
    HOLDER_CITY,        
    HOLDER_COUNTRY,        
    HOLDER_STATE,        
    HOLDER_ZIP,        
    ADD_INT_ID,      
    IS_ACTIVE        
         
   )        
   VALUES        
   (        
    @CUSTOMER_ID,        
    @APP_ID,        
    @APP_VERSION_ID,        
    @VEHICLE_ID,        
    @MEMO,        
    @NATURE_OF_INTEREST,        
    @RANK,        
    @LOAN_REF_NUMBER,        
    @CREATED_BY,         
    @CREATED_DATETIME,        
    @HOLDER_NAME,        
    @HOLDER_ADD1,        
    @HOLDER_ADD2,        
    @HOLDER_CITY,        
    @HOLDER_COUNTRY,        
    @HOLDER_STATE,        
    @HOLDER_ZIP,        
    @MAX_ADD_INT,      
    @IS_ACTIVE        
   )         
  END        
  ELSE        
  BEGIN        
   INSERT INTO APP_ADD_OTHER_INT        
   (        
    CUSTOMER_ID,        
    APP_ID,        
    APP_VERSION_ID,        
    HOLDER_ID,        
    VEHICLE_ID,        
    MEMO,        
    NATURE_OF_INTEREST,        
    RANK,        
    LOAN_REF_NUMBER,        
    CREATED_BY,         
    CREATED_DATETIME,        
    ADD_INT_ID,      
    IS_ACTIVE        
   )        
   VALUES        
   (        
    @CUSTOMER_ID,        
    @APP_ID,        
    @APP_VERSION_ID,        
   @HOLDER_ID,        
    @VEHICLE_ID,        
    @MEMO,        
    @NATURE_OF_INTEREST,        
    @RANK,        
    @LOAN_REF_NUMBER,        
    @CREATED_BY,         
    GetDate(),        
    @MAX_ADD_INT,      
    @IS_ACTIVE        
   )        
           
   IF @@ERROR <> 0         
   BEGIN        
    RETURN -2        
   END        
           
   UPDATE MNT_HOLDER_INTEREST_LIST        
   SET HOLDER_ADD1 = @HOLDER_ADD1,        
    HOLDER_ADD2 = @HOLDER_ADD2,        
    HOLDER_CITY = @HOLDER_CITY,        
    HOLDER_COUNTRY = @HOLDER_COUNTRY,        
    HOLDER_STATE = @HOLDER_STATE,        
    HOLDER_ZIP = @HOLDER_ZIP        
   WHERE HOLDER_ID = @HOLDER_ID        
         
  END        
          
  RETURN  @MAX_ADD_INT         
 END        
      
END    
    
    
    
GO

