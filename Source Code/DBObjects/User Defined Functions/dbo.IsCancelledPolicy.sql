IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IsCancelledPolicy]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[IsCancelledPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ravindra
-- Create date: 8-20-2008
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[IsCancelledPolicy] 
(
	-- Add the parameters for the function here
	@CUSTOMER_ID Int ,
	@POLICY_ID   Int
)
RETURNS int
AS
BEGIN

	DECLARE @Result int,
			@MAX_VERSION_ID Int

	SELECT @MAX_VERSION_ID = MAX(POLICY_VERSION_ID )FROM POL_CUSTOMER_POLICY_LIST WHERE 
	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
	
	IF EXISTS	( 
				SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID 
				AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @MAX_VERSION_ID AND 
				POLICY_STATUS IN ( 'CANCEL','RESCIND','SCANCEL' ) 
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

