IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesCLM_ACTIVITY_RECOVERY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesCLM_ACTIVITY_RECOVERY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 
/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetValuesCLM_ACTIVITY_RECOVERY    
Created by      : Vijay Arora    
Date            : 5/26/2006    
Purpose     : To get the values from table CLM_ACTIVITY_RECOVERY    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Proc_GetValuesCLM_ACTIVITY_RECOVERY
CREATE PROC dbo.Proc_GetValuesCLM_ACTIVITY_RECOVERY    
(    
    @CLAIM_ID  int,    
    @RECOVERY_ID int    
)    
AS    
BEGIN    
select CLAIM_ID,    
RECOVERY_ID, 
ISNULL(PAYMENT_METHOD,'') AS PAYMENT_METHOD,   
--RECOVERY_TYPE,    
--CONVERT(varchar(10),RECEIVED_DATE,101) AS RECEIVED_DATE,    
--RECEIVED_FROM,    
AMOUNT,    
--[DESCRIPTION],    
--TRANSACTION_CODE,    
--ACCOUNT_ID,    
ACTIVITY_ID    
from  CLM_ACTIVITY_RECOVERY    
where  CLAIM_ID = @CLAIM_ID AND RECOVERY_ID = @RECOVERY_ID    
END    
    
  








GO

