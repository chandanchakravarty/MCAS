IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDueDatesAtCancelRollback]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDueDatesAtCancelRollback]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_UpdateDueDatesAtCancelRollback
Created by       : Ravinda Gupta 
Date             : 08-27-2008
Purpose      	 : 
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_UpdateDueDatesAtCancelRollback
CREATE PROC [dbo].[Proc_UpdateDueDatesAtCancelRollback]
(
	@CUSTOMER_ID		Int,
	@POLICY_ID			Int, 
	@POLICY_VERSION_ID  Int
)
AS
BEGIN 
	DECLARE @DUE_DATE Datetime , 
			@CURRENT_TERM Int

	SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 

	SELECT @DUE_DATE = DUE_DATE FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND PROCESS_ID = 2 AND PROCESS_STATUS = 'PENDING'


	UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = GETDATE() 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID IN ( SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL
				WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM ) 
	AND DATEDIFF(DD,DUE_DATE , @DUE_DATE) = 0 
END


GO

