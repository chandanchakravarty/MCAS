IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyEndorsementNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyEndorsementNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                      
sp_find Proc_InsertPolicyPremiumItems,p                        
----------------------------------------------------------                                          
Proc Name       : dbo.Proc_GetPolicyBillingInstalmentsDetails                                
Created by      : LALIT CHAUHAN                              
Date            : 05/20/2010                                          
Purpose         : Get Policy Endorsement Details
Revison History :                                          
Used In   : Ebix Advantage Web
DROP PROC Proc_GetPolicyEndorsementNo
*/
CREATE PROC [dbo].[Proc_GetPolicyEndorsementNo]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT
)
AS 
BEGIN
IF EXISTS(SELECT * FROM POL_POLICY_ENDORSEMENTS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
			POLICY_VERSION_ID  = @POLICY_VERSION_ID AND ISNULL(ENDORSEMENT_STATUS,'') <> 'CAN')
	BEGIN
			SELECT * FROM POL_POLICY_ENDORSEMENTS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
			POLICY_VERSION_ID  = @POLICY_VERSION_ID AND ISNULL(ENDORSEMENT_STATUS,'') <> 'CAN'
	END
END
GO

