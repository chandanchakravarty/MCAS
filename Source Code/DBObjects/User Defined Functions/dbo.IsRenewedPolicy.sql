IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsRenewedPolicy]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsRenewedPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ravindra
-- Create date: 11-18-2008
-- Description:	
-- =============================================

--DROP FUNCTION [dbo].[IsRenewedPolicy]
CREATE FUNCTION [dbo].[IsRenewedPolicy] 
(
	-- Add the parameters for the function here
	@CUSTOMER_ID Int ,
	@POLICY_ID   Int , 
	@POLICY_VERSION_ID	Int
)
RETURNS int
AS
BEGIN

	DECLARE @Result int
	SELECT @Result = 0 

	
	IF EXISTS	( 
				SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
				AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID  <= @POLICY_VERSION_ID
				AND PROCESS_STATUS = 'COMPLETE' AND ISNULL(REVERT_BACK,'N') = 'N' 
				AND PROCESS_ID = 18
				)
	BEGIN 
		SELECT @Result = 1
	END
	ELSE
	BEGIN 
		SELECT @Result = 0 
	END

	-- Return the result of the function
	RETURN @Result

END




GO

