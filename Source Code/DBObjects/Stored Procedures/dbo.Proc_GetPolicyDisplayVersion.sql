IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDisplayVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDisplayVersion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /**************************************************            
CREATED BY   : Lalit Kr Chauhan         
CREATED DATETIME : April 29 2010              
PURPOSE  :  Get policy Display Version 


sp_find Proc_GetPolicyDisplayVersion,p  
drop proc Proc_GetPolicyDisplayVersion
*/
CREATE PROC [dbo].[Proc_GetPolicyDisplayVersion]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT = NULL,
@CALLED_FROM NVARCHAR(50) = NULL
)
AS
BEGIN 
DECLARE @CURRENT_TERM INT
	IF (@CALLED_FROM IS NULL AND @POLICY_VERSION_ID IS NOT NULL) 
	--get policy info ,display version if policy version supplied to get diaplsy version
	BEGIN 
		SELECT * FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID
		AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID  =@POLICY_VERSION_ID
	
	END
	ELSE IF (UPPER(@CALLED_FROM) = 'MAXVERISON') --get policy max version 
	BEGIN
		IF(@POLICY_VERSION_ID IS NOT NULL) 
		--if policy any version id supplied , get that term max version display version
		BEGIN 
		 SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID
		 
		 SELECT * FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  --AND CURRENT_TERM = @CURRENT_TERM
		 AND POLICY_VERSION_ID IN (
		 SELECT MAX(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)
		END 
		ELSE --if policy no version id supplied , get max version detail or display verion of that policy
		SELECT * FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  --AND CURRENT_TERM = @CURRENT_TERM
		 AND POLICY_VERSION_ID IN (
		 SELECT MAX(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID )
		
		
	END
 

END

GO

