IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckInstallPlanData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckInstallPlanData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sonal
-- Create date: 20 july 2010
-- Description:	to check wether partcular plan is generated installment for particular application or policy
-- =============================================
-- DROP PROCEDURE  dbo.Proc_CheckInstallPlanData 2126,53,1,24

CREATE PROCEDURE [dbo].[Proc_CheckInstallPlanData]
(
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID INT,
    @CALLED_FOR VARCHAR(20)= NULL
)	
AS
BEGIN
   IF(@CALLED_FOR = 'INSTALLMENTS')
	   BEGIN
		   SELECT INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE 
			 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
	   END
   ELSE
   BEGIN
		SELECT PLAN_ID FROM ACT_POLICY_INSTALL_PLAN_DATA WHERE 
		 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
   END
END





GO

