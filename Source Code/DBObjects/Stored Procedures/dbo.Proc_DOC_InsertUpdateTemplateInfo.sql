IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_InsertUpdateTemplateInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_InsertUpdateTemplateInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_InsertUpdateTemplateInfo
Created by      : Deepak Gupta         
Date            : 09-11-2006                         
Purpose         : To Insert Or Update Template Information
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC [dbo].[Proc_DOC_InsertUpdateTemplateInfo]
(                
	@TEMPLATE_ID INT,
	@DISPLAYNAME NVARCHAR(200),
	@DESCRIPTION NVARCHAR(450),
	@VERSION INT,
	@LOB INT,
	@AGENCY_ID INT,
	@CREATED_BY INT,
	@LETTERTYPE INT
)                
AS
DECLARE @NEWTEMPLATEID NUMERIC
BEGIN                
	IF @TEMPLATE_ID=-1
	BEGIN
		SELECT @NEWTEMPLATEID=ISNULL(MAX(TEMPLATE_ID),0)+1 FROM DOC_TEMPLATE_LIST
		INSERT INTO DOC_TEMPLATE_LIST (TEMPLATE_ID,
						FILENAME,
						DISPLAYNAME,
						DESCRIPTION,
						VERSION,
						LOB,
						AGENCY_ID,
						CREATED_BY,
						CREATION_DATE,
						LAST_MODIFIED_DATE,
						LETTERTYPE,
						IS_SYSTEM_TEMPLATE,
						IS_ACTIVE) 
					VALUES
						(@NEWTEMPLATEID,
						'TEMPLATE_' + CONVERT(VARCHAR(5),@NEWTEMPLATEID) + '.RTF',
						@DISPLAYNAME,
						@DESCRIPTION,
						@VERSION,
						@LOB,
						@AGENCY_ID,
						@CREATED_BY,
						GetDate(),
						GetDate(),
						@LETTERTYPE,
						'N',
						'Y'
						)
	END
	ELSE
	BEGIN
		UPDATE DOC_TEMPLATE_LIST SET 	DISPLAYNAME = @DISPLAYNAME,
						DESCRIPTION = @DESCRIPTION,
						VERSION	    = @VERSION,
						LOB         = @LOB,
						AGENCY_ID   = @AGENCY_ID,
						CREATED_BY  = @CREATED_BY,
						LAST_MODIFIED_DATE = GetDate(),
						LETTERTYPE  = @LETTERTYPE
					 WHERE TEMPLATE_ID = @TEMPLATE_ID;

		SELECT @NEWTEMPLATEID=@TEMPLATE_ID;
	END
	SELECT @NEWTEMPLATEID;
END





GO

