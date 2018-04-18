IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSavedRuleXml_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSavedRuleXml_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetSavedRuleXml_APP  
Created by      : Pravesh
Date            : 16 June. 2008
Purpose         : Fetch Saved Rule XML  
Revison History :  
Used In         : 
drop proc dbo.Proc_GetSavedRuleXml_APP    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROC [dbo].[Proc_GetSavedRuleXml_APP]  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     int    
)  
AS  
BEGIN  
DECLARE @APP_STATUS NVARCHAR(15)

SELECT  @APP_STATUS=APP_STATUS FROM  APP_LIST  WITH(NOLOCK)
	WHERE   CUSTOMER_ID		=@CUSTOMER_ID AND  
			APP_ID			=@APP_ID AND 
			APP_VERSION_ID	=@APP_VERSION_ID 

IF (upper(@APP_STATUS) IN ('COMPLETE'))
	SELECT APP_VERIFICATION_XML,RULE_INPUT_XML,SHOW_QUOTE FROM  APP_LIST  WITH(NOLOCK)
		WHERE   CUSTOMER_ID		=@CUSTOMER_ID AND  
				APP_ID			=@APP_ID AND 
				APP_VERSION_ID	=@APP_VERSION_ID 

ELSE
SELECT '' AS APP_VERIFICATION_XML,'' AS RULE_INPUT_XML ,'0' as SHOW_QUOTE 
 
END  




GO

