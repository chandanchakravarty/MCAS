IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetTemplateInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetTemplateInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_GetTemplateInformation     
Created by      : Deepak Gupta         
Date            : 08-29-2006                         
Purpose         : To Get Information Related to Template
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_DOC_GetTemplateInformation
(                
	@TEMPLATEID     INT,
	@USERID		INT=0,
	@MODE		VARCHAR(10)=''
)                
AS
DECLARE @MAXTEMPLATE INT                
BEGIN                
	IF @MODE = 'COPY'
	BEGIN
		SELECT @MAXTEMPLATE = ISNULL(MAX(TEMPLATE_ID),0)+1 FROM DOC_TEMPLATE_LIST
		
		INSERT INTO DOC_TEMPLATE_LIST (TEMPLATE_ID,FILENAME,DISPLAYNAME,DESCRIPTION,VERSION,QUERYXML,LOB,AGENCY_ID,CREATED_BY,CREATION_DATE,LAST_MODIFIED_DATE,LETTERTYPE,IS_SYSTEM_TEMPLATE,IS_ACTIVE)
		SELECT @MAXTEMPLATE,FILENAME,'Copy Of ' + DISPLAYNAME,DESCRIPTION,VERSION,QUERYXML,LOB,AGENCY_ID,@USERID,GetDate(),GetDate(),LETTERTYPE,'N',IS_ACTIVE
		FROM DOC_TEMPLATE_LIST WHERE TEMPLATE_ID=@TEMPLATEID
		
		SELECT @TEMPLATEID = @MAXTEMPLATE
	END
	
	SELECT * 
	FROM DOC_TEMPLATE_LIST WHERE TEMPLATE_ID=@TEMPLATEID
END        
    







GO

