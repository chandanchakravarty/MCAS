IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyTranType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyTranType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
CREATED BY			: Lalit Kr Chauhan     
CREATED DATETIME	: Jan 21 2011       
PURPOSE				: Get policy Transaction Type For Master Policy
Review				:     
Review By  Date		:       
Purpose				:    
DROP PROC Proc_GetPolicyTranType  2156,426,2
*/
Create PROC [dbo].[Proc_GetPolicyTranType]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT
)
AS
BEGIN
	SELECT TRANSACTION_TYPE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID

END
GO

