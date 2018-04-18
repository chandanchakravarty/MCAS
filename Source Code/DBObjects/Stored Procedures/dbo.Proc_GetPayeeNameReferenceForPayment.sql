IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPayeeNameReferenceForPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPayeeNameReferenceForPayment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetPayeeNameReferenceForPayment  
Created by      : Sumit Chhabra  
Date            : 06/22/2006    
Purpose         : To get the values of PAYEE from table named CLM_PARTIES  
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_GetPayeeNameReferenceForPayment    
(    
 @CLAIM_ID int,    
 @ACTIVITY_ID int   
)    
AS    
BEGIN    
declare @PAYEE_PARTIES_ID varchar(200)  
declare @TEMP_STR varchar(1000)  
SELECT @PAYEE_PARTIES_ID = ISNULL(PAYEE_PARTIES_ID,'') FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
  
if @PAYEE_PARTIES_ID is not null and @PAYEE_PARTIES_ID<>''  
begin   
 set @TEMP_STR = 'SELECT  [NAME],REFERENCE, ADDRESS1, ADDRESS2, CITY, COUNTRY, STATE, ZIP, PARTY_ID, ' +   
         ' BANK_NAME,ACCOUNT_NAME,ACCOUNT_NUMBER FROM  CLM_PARTIES ' +    
         ' WHERE CLAIM_ID = ' + CAST(@CLAIM_ID AS VARCHAR(10)) +   
         ' AND PARTY_ID IN (' + CAST(@PAYEE_PARTIES_ID AS VARCHAR(200)) + ') AND IS_ACTIVE=''Y'''  
 EXEC (@TEMP_STR)  
end  
END  
  
  
  
  
  



GO

