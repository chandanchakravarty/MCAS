IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPoliciesWithPendingRenewal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPoliciesWithPendingRenewal]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/******************************************************************************************************************
Proc  		: dbo.Proc_GetPoliciesWithPendingRenewal
Created By 	: Ravindra Gupta
Date 		: 
Purpose		: 
Used in 	: Wolvorine 's EOD process

*********************************************************************************************/
-- drop proc dbo.Proc_GetPoliciesWithPendingRenewal
CREATE PROC dbo.Proc_GetPoliciesWithPendingRenewal
AS
BEGIN

DECLARE @POLICY_NORMAL nvarchar(15),
	@RENEWAL_LAUNCH smallint

SET @POLICY_NORMAL='NORMAL'
SET @RENEWAL_LAUNCH = 5 

SELECT CPL.APP_EXPIRATION_DATE,CPL.APP_EFFECTIVE_DATE ,
	CPL.CUSTOMER_ID, CPL.POLICY_ID, CPL.POLICY_VERSION_ID, CPL.POLICY_STATUS, CPL.POLICY_LOB AS LOB_ID,
	DATEDIFF(DAY,GETDATE(),CPL.APP_EXPIRATION_DATE) AS DAYS_TOEXPIRE  
FROM POL_CUSTOMER_POLICY_LIST  CPL

INNER JOIN POL_POLICY_PROCESS  PPP
ON CPL.CUSTOMER_ID = PPP.CUSTOMER_ID
AND CPL.POLICY_ID = PPP.POLICY_ID 
AND CPL.POLICY_VERSION_ID = PPP.POLICY_VERSION_ID
WHERE CPL.POLICY_STATUS  = @POLICY_NORMAL 
AND DATEDIFF(DAY,GETDATE(),CPL.APP_EXPIRATION_DATE)>0
AND PPP.PROCESS_ID = @RENEWAL_LAUNCH 
AND PPP.PROCESS_STATUS = 'PENDING'

END


GO

