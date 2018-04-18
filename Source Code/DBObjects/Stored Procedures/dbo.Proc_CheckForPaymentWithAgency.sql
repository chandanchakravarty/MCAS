IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckForPaymentWithAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckForPaymentWithAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_CheckForPaymentWithAgency
Created by      :  Ravindra
Date            :  4-9-2007
Purpose         :  Will check for is there any payment with agency is there for concerned policy
			and the Grace period has not elapsed
Revison History :                
Used In         :  Wolverine                
                
exec dbo.Proc_CheckForPaymentWithAgency
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop PROC dbo.Proc_CheckForPaymentWithAgency
CREATE PROC dbo.Proc_CheckForPaymentWithAgency
(
	@CUSTOMER_ID		Int,
	@POLICY_ID		Int,
	@CURRENT_TERM		Int,
	@CALLED_FROM		Varchar(20),
	@DATE_TO_USE		DateTime
)
AS
BEGIN 

IF(@CALLED_FROM = 'CANCEL_LAUNCH')
BEGIN 
	IF EXISTS ( SELECT  IDEN_ROW_ID FROM ACT_CUSTOMER_PAYMENTS_FROM_AGENCY AGNP WITH(NOLOCK) 
			INNER JOIN POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK) 
			ON CPL.CUSTOMER_ID = AGNP.CUSTOMER_ID 
				AND CPL.POLICY_ID  = AGNP.POLICY_ID
				AND CPL.POLICY_VERSION_ID = AGNP.POLICY_VERSION_ID
			INNER JOIN ACT_INSTALL_PLAN_DETAIL INS WITH(NOLOCK)  
				ON CPL.INSTALL_PLAN_ID = INS.IDEN_PLAN_ID 
			WHERE CPL.CURRENT_TERM = @CURRENT_TERM 
				AND CPL.POLICY_ID = @POLICY_ID 
				AND CPL.CUSTOMER_ID = @CUSTOMER_ID 
				AND CAST(CONVERT(VARCHAR,DATEADD(DD,INS.POST_PHONE,AGNP.DATE_COMMITTED),101) AS DATETIME)
				 > CAST(CONVERT(VARCHAR,@DATE_TO_USE,101) AS DATETIME)
			)
	BEGIN
		RETURN  1 -- Agency Has Payment & grace period is not elapsed
	END
	ELSE 
	BEGIN 
		RETURN -2
	END
END

IF(@CALLED_FROM = 'CANCEL_COMMIT')
BEGIN 
	IF EXISTS ( SELECT  IDEN_ROW_ID FROM ACT_CUSTOMER_PAYMENTS_FROM_AGENCY AGNP WITH(NOLOCK) 
			INNER JOIN POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK) 
			ON CPL.CUSTOMER_ID = AGNP.CUSTOMER_ID 
				AND CPL.POLICY_ID  = AGNP.POLICY_ID
				AND CPL.POLICY_VERSION_ID = AGNP.POLICY_VERSION_ID
			INNER JOIN ACT_INSTALL_PLAN_DETAIL INS WITH(NOLOCK) 
				ON CPL.INSTALL_PLAN_ID = INS.IDEN_PLAN_ID 
			WHERE CPL.CURRENT_TERM = @CURRENT_TERM 
				AND CPL.POLICY_ID = @POLICY_ID 
				AND CPL.CUSTOMER_ID = @CUSTOMER_ID 
				AND CAST(CONVERT(VARCHAR,DATEADD(DD,INS.POST_CANCEL,AGNP.DATE_COMMITTED),101) AS DATETIME) 
				> CAST(CONVERT(VARCHAR,@DATE_TO_USE,101) as DATETIME)
				)
	BEGIN
		RETURN  1-- Agency Has Payment & grace period is not elapsed
	END
	ELSE 
	BEGIN 
		RETURN -2
	END
END
	
	
END



GO

