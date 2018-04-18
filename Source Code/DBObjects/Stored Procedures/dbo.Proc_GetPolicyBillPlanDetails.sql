IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyBillPlanDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyBillPlanDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : dbo.Proc_GetPolicyAgencyDetails
Created by           : Pravesh k Chandel
Date                    : 23 Nov 2007
Purpose               : fecth Policy 's Agency Details
Revison History :
Used In                :   Wolverine*/
--drop proc dbo.Proc_GetPolicyBillPlanDetails
CREATE PROC dbo.Proc_GetPolicyBillPlanDetails
(
@CUSTOMER_ID 	int,
@POLICY_ID	int,
@POLICY_VERSION_ID int
)
AS
BEGIN
DECLARE @INSTALL_PLAN_ID   INT

SELECT   @INSTALL_PLAN_ID=INSTALL_PLAN_ID
FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND POLICY_VERSION_ID  =@POLICY_VERSION_ID


SELECT 	IDEN_PLAN_ID,PLAN_CODE,PLAN_TYPE,PLAN_DESCRIPTION,isnull(GRACE_PERIOD,0) GRACE_PERIOD

FROM ACT_INSTALL_PLAN_DETAIL WHERE IDEN_PLAN_ID=@INSTALL_PLAN_ID


END






GO

