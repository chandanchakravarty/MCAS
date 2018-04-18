IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSavedRuleXml_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSavedRuleXml_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetSavedRuleXml_Pol  
Created by      : Pravesh
Date            : 13 June. 2008
Purpose         : Fetch Saved Rule XML  
Revison History :  
Used In         : 
drop proc dbo.Proc_GetSavedRuleXml_Pol    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROC [dbo].[Proc_GetSavedRuleXml_Pol]  
(  
 @CUSTOMER_ID     int,  
 @POLICY_ID     int,  
 @POLICY_VERSION_ID     int    
)  
AS  
BEGIN  
DECLARE @POLICY_STATUS NVARCHAR(15)

SELECT  @POLICY_STATUS=POLICY_STATUS FROM  POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)
	WHERE   CUSTOMER_ID		=@CUSTOMER_ID AND  
			POLICY_ID		=@POLICY_ID AND 
			POLICY_VERSION_ID=@POLICY_VERSION_ID 
IF (@POLICY_STATUS IN ('NORMAL','CANCEL','RENEWED','EXPIRED','INACTIVE','REJECT','RESCIND','NONRENEWED','SCANCEL','MREINSTATE','MENDORSE','MNONRENEWED'))
	SELECT APP_VERIFICATION_XML,RULE_INPUT_XML FROM  POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)
		WHERE   CUSTOMER_ID		=@CUSTOMER_ID AND  
				POLICY_ID		=@POLICY_ID AND 
				POLICY_VERSION_ID=@POLICY_VERSION_ID 

ELSE
SELECT '' AS APP_VERIFICATION_XML,'' AS RULE_INPUT_XML 
 
END  




GO

