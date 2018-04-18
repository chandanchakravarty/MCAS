IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyDisplayVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyDisplayVersion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
PROC NAME        : DBO.Proc_UpdatePolicyDisplayVersion
CREATED BY       : VIJAY ARORA      
DATE             : 31-01-2006
PURPOSE          : Update the Display Version of the Policy.
REVISON HISTORY  :              
USED IN          : WOLVERINE              
------------------------------------------------------------              
DATE     REVIEW BY          COMMENTS              
-----   ------------       -------------------------*/              
CREATE PROC dbo.Proc_UpdatePolicyDisplayVersion
(              
	@CUSTOMER_ID      	INT,  
	@POLICY_ID       	INT,  
	@POLICY_VERSION_ID  SMALLINT,  
	@POLICY_DISP_VERSION NVARCHAR(6)
)              
AS                            
BEGIN              
	
	--Updating the policy display version
	UPDATE POL_CUSTOMER_POLICY_LIST   
	SET POLICY_DISP_VERSION = @POLICY_DISP_VERSION      
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
	
END  
  
  








GO

