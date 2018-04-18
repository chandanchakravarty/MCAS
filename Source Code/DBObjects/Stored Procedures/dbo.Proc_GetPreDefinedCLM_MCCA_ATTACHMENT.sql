IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPreDefinedCLM_MCCA_ATTACHMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPreDefinedCLM_MCCA_ATTACHMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetPreDefinedCLM_MCCA_ATTACHMENT
Created by      : Vijay Arora      
Date            : 10/2006      
Purpose     	: To get the attachment points in the specified period.
Revison History :      
Used In        	: Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC Dbo.Proc_GetPreDefinedCLM_MCCA_ATTACHMENT      
CREATE PROC dbo.Proc_GetPreDefinedCLM_MCCA_ATTACHMENT      
(      
	@CLAIM_ID  int
)      
AS      
BEGIN      
    
DECLARE @POLICY_PERIOD_DATE DATETIME
DECLARE @LOSS_PERIOD_DATE DATETIME

SELECT @POLICY_PERIOD_DATE = PCPL.APP_EFFECTIVE_DATE,@LOSS_PERIOD_DATE=CCI.LOSS_DATE
FROM CLM_CLAIM_INFO CCI 
LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND
PCPL.POLICY_ID = CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
WHERE CCI.CLAIM_ID = @CLAIM_ID


 IF EXISTS     
 (SELECT MCCA_ATTACHMENT_POINT FROM CLM_MCCA_ATTACHMENT     
  WHERE (@POLICY_PERIOD_DATE BETWEEN POLICY_PERIOD_DATE_FROM AND POLICY_PERIOD_DATE_TO)     
  AND (@LOSS_PERIOD_DATE BETWEEN LOSS_PERIOD_DATE_FROM AND LOSS_PERIOD_DATE_TO) AND ISNULL(IS_ACTIVE,'N')='Y')    

	SELECT MCCA_ATTACHMENT_POINT FROM CLM_MCCA_ATTACHMENT     
	  WHERE (@POLICY_PERIOD_DATE BETWEEN POLICY_PERIOD_DATE_FROM AND POLICY_PERIOD_DATE_TO)     
	  AND (@LOSS_PERIOD_DATE BETWEEN LOSS_PERIOD_DATE_FROM AND LOSS_PERIOD_DATE_TO)  AND ISNULL(IS_ACTIVE,'N')='Y' 
	
 ELSE
	SELECT MCCA_ATTACHMENT_POINT = 0

END      
      
      





GO

