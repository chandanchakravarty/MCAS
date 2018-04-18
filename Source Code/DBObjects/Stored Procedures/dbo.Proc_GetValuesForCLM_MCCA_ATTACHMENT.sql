IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesForCLM_MCCA_ATTACHMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesForCLM_MCCA_ATTACHMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetValuesForCLM_MCCA_ATTACHMENT    
Created by      : Vijay Arora    
Date            : 8/8/2006    
Purpose     : To get the values for table named CLM_MCCA_ATTACHMENT    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_GetValuesForCLM_MCCA_ATTACHMENT    
CREATE PROC [dbo].[Proc_GetValuesForCLM_MCCA_ATTACHMENT]    
(    
 @MCCA_ATTACHMENT_ID     int    
)    
AS    
BEGIN    
SELECT MCCA_ATTACHMENT_ID,    
 POLICY_PERIOD_DATE_FROM,    
  POLICY_PERIOD_DATE_TO,    
 LOSS_PERIOD_DATE_FROM,    
 LOSS_PERIOD_DATE_TO,  
MCCA_ATTACHMENT_POINT,    
IS_ACTIVE    
FROM CLM_MCCA_ATTACHMENT    
WHERE MCCA_ATTACHMENT_ID = @MCCA_ATTACHMENT_ID    
END    
    
  
  

GO

