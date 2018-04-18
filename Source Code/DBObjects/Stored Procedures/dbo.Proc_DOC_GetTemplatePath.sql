IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetTemplatePath]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetTemplatePath]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_GetTemplatePath
Created by      : Praveen kasana         
Date            : 09-15-2006                         
Purpose         : To get Template Path
Revison History :                
Used In         : Wolverine  
Modifed on :  Oct 1 2008       
drop proc dbo.Proc_DOC_GetTemplatePath       

               
------   ------------       -------------------------*/                
    
CREATE PROC dbo.Proc_DOC_GetTemplatePath
(     
 @MERGE_ID INT           
)                
AS
BEGIN

SELECT ISNULL(TEMPLATE_PATH,'') as TEMPLATE_PATH FROM DOC_SEND_LETTER WITH(NOLOCK) 
WHERE MERGE_ID = @MERGE_ID

	
END








GO

