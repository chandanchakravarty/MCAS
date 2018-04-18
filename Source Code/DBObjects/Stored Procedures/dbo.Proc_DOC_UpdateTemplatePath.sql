IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_UpdateTemplatePath]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_UpdateTemplatePath]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_InsertUpdateSendLetter
Created by      : Praveen kasana         
Date            : 09-15-2006                         
Purpose         : To Update Template Path
Revison History :                
Used In         : Wolverine                
               
------   ------------       -------------------------*/                
    
CREATE PROC dbo.Proc_DOC_UpdateTemplatePath
(     
	@MERGE_ID INT,           
	@TEMPLATE_PATH VARCHAR(2000)
)                
AS
BEGIN
DECLARE @TEMPLATE_ID INT

	/*SELECT @TEMPLATE_ID = DS.TEMPLATE_ID FROM DOC_SEND_LETTER DS
	INNER JOIN DOC_TEMPLATE_LIST DT
	ON DS.TEMPLATE_ID = DT.TEMPLATE_ID
	WHERE MERGE_ID=@MERGE_ID

	UPDATE DOC_TEMPLATE_LIST SET 	
	TEMPLATE_PATH	= @TEMPLATE_PATH
	WHERE TEMPLATE_ID = @TEMPLATE_ID*/

UPDATE DOC_SEND_LETTER SET TEMPLATE_PATH	= @TEMPLATE_PATH 
WHERE MERGE_ID = @MERGE_ID


	
END

GO

