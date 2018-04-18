IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGeneralInformationUN_Tier]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGeneralInformationUN_Tier]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author	  :	Charles Gomes
-- Create date: 23-Dec-09
-- Description:	 RETRIEVING DATA FROM APP_UNDERWRITING_TIER    
-- =============================================
--drop proc Proc_GetPolicyGeneralInformationUN_Tier
CREATE PROCEDURE [DBO].[Proc_GetPolicyGeneralInformationUN_Tier]
@CUSTOMER_ID INT,   
@POLICY_ID INT,          
@POLICY_VERSION_ID INT  
AS
BEGIN
	SELECT 
	UNDERWRITING_TIER,
	CONVERT(VARCHAR,UNTIER_ASSIGNED_DATE,101) AS UNTIER_ASSIGNED_DATE,
	ISNULL(CAP_INC,'N') AS CAP_INC,
	ISNULL(CAP_DEC,'N') AS CAP_DEC,
	CAP_RATE_CHANGE_REL,
	CAP_MIN_MAX_ADJUST,
	ACL_PREMIUM
	FROM POL_UNDERWRITING_TIER
	WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID          
   
END

GO

