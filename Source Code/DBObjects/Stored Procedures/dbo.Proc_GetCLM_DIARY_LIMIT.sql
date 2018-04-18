IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_DIARY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_DIARY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetCLM_DIARY_LIMIT                
Created by      :  Asfa Praveen    
Date            :  15/JAN/2008                
Purpose         :  To fetch Calim Activity Reserves and Payment Limit for Diary Notification
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetCLM_DIARY_LIMIT           
CREATE proc dbo.Proc_GetCLM_DIARY_LIMIT                
@ACTIVITY_REASON INT                
AS                
BEGIN
	IF(@ACTIVITY_REASON = 11773) -- RESERVE_UPDATE
 	  BEGIN
		SELECT CLAIM_RESERVE_LIMIT FROM MNT_SYSTEM_PARAMS
	  END
	ELSE IF(@ACTIVITY_REASON = 11775 OR @ACTIVITY_REASON = 11774) -- 11775=CLAIM_PAYMENT AND 11774=EXPENSE_PAYMENT
	  BEGIN
		SELECT CLAIM_PAYMENT_LIMIT FROM MNT_SYSTEM_PARAMS
	  END
END         
        
      
    
  









GO

