IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_RESERVE_ACCOUNTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_RESERVE_ACCOUNTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetCLM_RESERVE_ACCOUNTS                
Created by      :  Asfa Praveen    
Date            :  16/Nov/2007                
Purpose         :  To get Debit and Credit Accounts corresponding to claim_id, activity_id, transaction_id
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetCLM_RESERVE_ACCOUNTS             
CREATE proc dbo.Proc_GetCLM_RESERVE_ACCOUNTS                
@CLAIM_ID INT,                
@ACTIVITY_ID INT,
@TRANSACTION_ID INT 
AS                
BEGIN 
  IF(@ACTIVITY_ID = 1)  
    BEGIN
      SET @ACTIVITY_ID = 0
    END
 
 SELECT TOP 1 CRACCTS, DRACCTS  FROM CLM_ACTIVITY_RESERVE with (nolock)    
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  AND TRANSACTION_ID=@TRANSACTION_ID
            
END         
        
      
    
  





GO

