IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyInsuredName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyInsuredName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_GetPolicyInsuredName  
(  
	@CUSTOMER_ID INT,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID INT  
)  
AS  
BEGIN  

DECLARE @POLICY_NUMBER varchar(20)
SELECT @POLICY_NUMBER = ISNULL(POLICY_NUMBER,'') FROM POL_CUSTOMER_POLICY_LIST
WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
	POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  


	SELECT ISNULL(FIRST_NAME,'') + ' ' +  isnull(LAST_NAME,'') + '/' + @POLICY_NUMBER AS INSURED_NAME
	FROM CLT_APPLICANT_LIST CAL 
	INNER JOIN POL_APPLICANT_LIST PAL ON PAL.CUSTOMER_ID=CAL.CUSTOMER_ID 
	AND PAL.APPLICANT_ID=CAL.APPLICANT_ID
	WHERE  PAL.CUSTOMER_ID=@CUSTOMER_ID AND 
	PAL.POLICY_ID=@POLICY_ID AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND 
	PAL.IS_PRIMARY_APPLICANT = 1
END  


GO

