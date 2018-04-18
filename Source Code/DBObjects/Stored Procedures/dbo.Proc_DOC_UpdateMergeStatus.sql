IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_UpdateMergeStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_UpdateMergeStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_UpdateMergeStatus
Created by      : Deepak Gupta         
Date            : 08-29-2006                         
Purpose         : To Update Merge Status after Merging
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_DOC_UpdateMergeStatus
(                
	@MERGEID     varchar(500)
)                
AS      
DECLARE @strSQL VARCHAR(2000)
BEGIN                
	
	SELECT @strSQL = 'UPDATE DOC_SEND_LETTER SET MERGE_STATUS = ''M'', MERGE_DATE=GETDATE() WHERE MERGE_STATUS <> ''S'' '
	if @MERGEID <> 'ALL'
		SELECT @strSQL = @strSQL + ' AND MERGE_ID IN (' + @MERGEID + ') '

	exec (@strSQL)
END        
    





GO

