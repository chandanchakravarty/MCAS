IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAdditionalInterest_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAdditionalInterest_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_InsertAdditionalInterest_ACORD
/*----------------------------------------------------------        
Proc Name    : dbo.Proc_InsertAdditionalInterest_ACORD    
Created by   : Shrikant Bhatt        
Date         : 27 April.,2005       
Purpose     : Insert the record into  AdditionalInterest  Table    
Revison History :    
Created by   : Sumit Chhabra  
Date         : 22 November,2005       
Purpose     : Modified the parameters' data-types  
Used In  :   Wolverine           
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/       
    
CREATE         PROC dbo.Proc_InsertAdditionalInterest_ACORD    
(    
 @CUSTOMER_ID int,  
 @APP_ID smallint,  
 @APP_VERSION_ID smallint,  
 @HOLDER_ID   int,      
 @VEHICLE_ID int,  
 --@MEMO   NVARCHAR(500),      
 @MEMO   NVARCHAR(250),
 --@NATURE_OF_INTEREST NVARCHAR(60),
 @NATURE_OF_INTEREST NVARCHAR(30),      
 @RANK   smallint,  
 --@LOAN_REF_NUMBER NVARCHAR(150),      
 @LOAN_REF_NUMBER NVARCHAR(75),
 @CREATED_BY     int,            
 @CREATED_DATETIME     datetime,      
 --@HOLDER_NAME nvarchar(140),      
 --@HOLDER_ADD1 nvarchar(280),      
 --@HOLDER_ADD2 nvarchar(280),      
 --@HOLDER_CITY nvarchar(160),      
 --@HOLDER_COUNTRY nvarchar(20),      
 --@HOLDER_STATE nvarchar(20),      
 @HOLDER_NAME nvarchar(70),      
 @HOLDER_ADD1 nvarchar(140),      
 @HOLDER_ADD2 nvarchar(140),      
 @HOLDER_CITY nvarchar(80),      
 @HOLDER_COUNTRY nvarchar(10),      
 @HOLDER_STATE nvarchar(10), 
 @HOLDER_ZIP varchar(11)          
 
)    
AS    
    
BEGIN    
 DECLARE @MAX_ADD_INT int     
 DECLARE @STATE_ID Int    
     
 EXECUTE @STATE_ID = Proc_GetSTATE_ID 1,@HOLDER_STATE    
     
 SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1    
 FROM APP_ADD_OTHER_INT    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
       APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID --AND    
  --VEHICLE_ID = @VEHICLE_ID     
      
    
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
   ADD_INT_ID    
    
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
   @STATE_ID,    
   @HOLDER_ZIP,    
   @MAX_ADD_INT    
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
   ADD_INT_ID    
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
   @MAX_ADD_INT    
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
    
    
    
  



GO

