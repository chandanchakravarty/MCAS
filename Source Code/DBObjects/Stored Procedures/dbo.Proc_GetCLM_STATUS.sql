IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_STATUS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_STATUS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       :  dbo.Proc_GetCLM_STATUS                  
Created by      :  Asfa Praveen      
Date            :  18/Oct/2007                  
Purpose         :  To fetch Calim Status  
Revison History :                  
Used In         :   Wolverine                  
-------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- DROP PROC dbo.Proc_GetCLM_STATUS  1236    
CREATE proc [dbo].[Proc_GetCLM_STATUS]          
@CLAIM_ID INT
--@LANG INT                  
AS                  
BEGIN   

--------------------------------------------------------------------
-- MODIFIED BY SANTOSH KUMAR GAUTAM ON 23 MARCH 2011 
-- TO GET PENDING AMOUNT TO RECOVER
--------------------------------------------------------------------


	   DECLARE @AMOUNT_TO_RECOVER DECIMAL(18,2)=0

       SELECT @AMOUNT_TO_RECOVER=ISNULL(SUM(PAYMENT_AMT+RECOVERY_AMT) ,0)
       FROM CLM_ACTIVITY_CO_RI_BREAKDOWN
       WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'
       
       SELECT CLAIM_STATUS,
       @AMOUNT_TO_RECOVER AS AMOUNT_TO_RECOVER      
       FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID  
       
       SELECT ISNULL(IS_VICTIM_CLAIM,0) AS IS_VICTIM_CLAIM--,ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS VICTIM_DESC
	   FROM CLM_CLAIM_INFO CCI
	   --LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON CCI.IS_VICTIM_CLAIM = MLV.LOOKUP_UNIQUE_ID 
	   --LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID = MLVM.LOOKUP_UNIQUE_ID AND MLVM.LANG_ID = @LANG
	   WHERE CCI.CLAIM_ID = @CLAIM_ID
END           
          
        
      
    
  
  
  
  
GO

