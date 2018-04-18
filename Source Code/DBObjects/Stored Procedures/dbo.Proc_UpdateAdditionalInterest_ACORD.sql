IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAdditionalInterest_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAdditionalInterest_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name    : dbo.Proc_UpdateAdditionalInterest_ACORD  
Created by   : Shrikant Bhatt      
Date         : 28 April.,2005     
Purpose     : Insert the record into  AdditionalInterest  Table  
Created by   : Sumit Chhabra
Date         : 22 November,2005     
Purpose     : Modified the parameters' datatypes
Revison History :  
Used In  :   Wolverine         
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/     
  
CREATE    PROC dbo.Proc_UpdateAdditionalInterest_ACORD  
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
 @MODIFIED_BY     int,        
 @LAST_UPDATED_DATETIME     datetime ,  
 @ADD_INT_ID Int,  
 @HOLDER_NAME nvarchar(140),    
 @HOLDER_ADD1 nvarchar(280),    
 @HOLDER_ADD2 nvarchar(280),    
 @HOLDER_CITY nvarchar(160),    
 @HOLDER_COUNTRY nvarchar(20),    
 @HOLDER_STATE nvarchar(20),    
 @HOLDER_ZIP varchar(11)
)  
AS  
BEGIN  
 DECLARE @STATE_ID Int  
   
 EXECUTE @STATE_ID = Proc_GetSTATE_ID 1,@HOLDER_STATE  
   
 IF ( @HOLDER_ID = 0 )  
 BEGIN  
  UPDATE APP_ADD_OTHER_INT  
  SET   
   MEMO=@MEMO,  
   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,  
   RANK=@RANK,  
   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,  
   MODIFIED_BY=@MODIFIED_BY,   
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
   HOLDER_NAME = @HOLDER_NAME,  
   HOLDER_ADD1 = @HOLDER_ADD1,  
   HOLDER_ADD2 = @HOLDER_ADD2,  
   HOLDER_CITY = @HOLDER_CITY,  
   HOLDER_COUNTRY = @HOLDER_COUNTRY,  
   HOLDER_STATE = @STATE_ID,  
   HOLDER_ZIP = @HOLDER_ZIP  
  
  WHERE   
   CUSTOMER_ID  = @CUSTOMER_ID and  
   APP_ID   = @APP_ID and  
   APP_VERSION_ID = @APP_VERSION_ID and  
   ADD_INT_ID  = @ADD_INT_ID and  
   VEHICLE_ID  = @VEHICLE_ID  
 END  
 ELSE  
 BEGIN  
  UPDATE APP_ADD_OTHER_INT  
  SET   
   MEMO=@MEMO,  
   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,  
   RANK=@RANK,  
   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,  
   MODIFIED_BY=@MODIFIED_BY,   
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
  WHERE   
   CUSTOMER_ID  = @CUSTOMER_ID and  
   APP_ID   = @APP_ID and  
   APP_VERSION_ID = @APP_VERSION_ID and  
   ADD_INT_ID  = @ADD_INT_ID and  
   VEHICLE_ID  = @VEHICLE_ID  
    
  IF @@ERROR <> 0  
  BEGIN   
   RETURN -4   
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
   
END  
  
  
  
  
  
  
  



GO

