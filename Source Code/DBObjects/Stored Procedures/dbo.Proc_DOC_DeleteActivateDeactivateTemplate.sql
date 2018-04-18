IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_DeleteActivateDeactivateTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_DeleteActivateDeactivateTemplate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_DeleteActivateDeactivateTemplate
Created by      : Deepak Gupta         
Date            : 09-11-2006                         
Purpose         : To Delete Or Activate Or Deactivate Template Information
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_DOC_DeleteActivateDeactivateTemplate
(                
	@TEMPLATE_ID INT,
	@ACTIVITY NVARCHAR(20)
)                
AS
BEGIN                
	IF @ACTIVITY = 'DELETE'
	BEGIN
		DELETE FROM DOC_TEMPLATE_LIST WHERE TEMPLATE_ID = @TEMPLATE_ID;
	END
	ELSE IF @ACTIVITY = 'ACTIVATE'
	BEGIN
		UPDATE DOC_TEMPLATE_LIST SET 	IS_ACTIVE = 'Y',
						LAST_MODIFIED_DATE = GetDate()
					 WHERE TEMPLATE_ID = @TEMPLATE_ID;
	END
	ELSE IF @ACTIVITY = 'DEACTIVATE'
	BEGIN
		UPDATE DOC_TEMPLATE_LIST SET 	IS_ACTIVE = 'N',
						LAST_MODIFIED_DATE = GetDate()
					 WHERE TEMPLATE_ID = @TEMPLATE_ID;
	END
END





GO

