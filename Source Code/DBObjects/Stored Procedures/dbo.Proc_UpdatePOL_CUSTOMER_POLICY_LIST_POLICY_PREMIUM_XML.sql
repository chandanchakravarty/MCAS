IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_CUSTOMER_POLICY_LIST_POLICY_PREMIUM_XML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_CUSTOMER_POLICY_LIST_POLICY_PREMIUM_XML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*************************************************************
Created By		:Vijay Joshi
Created Datetime: 27 Feb, 2006 2:47 pm
Purpose			:To update the POLICY_PREMIUM_XML fields

Review Date		Review By		Purpose
**************************************************************/
CREATE PROCEDURE dbo.Proc_UpdatePOL_CUSTOMER_POLICY_LIST_POLICY_PREMIUM_XML
(
	@CUSTOMER_ID		INT,
	@POLICY_ID			INT,
	@POLICY_VERSION_ID	INT,
	@POLICY_PREMIUM_XML	TEXT
)
AS
BEGIN
	UPDATE POL_CUSTOMER_POLICY_LIST
	SET POLICY_PREMIUM_XML = @POLICY_PREMIUM_XML
	WHERE CUSTOMER_ID  = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
END


GO

