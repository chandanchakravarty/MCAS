IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_deletediary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_deletediary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name     : dbo.proc_deletediary      
Created by      : Nidhi      
Date                  : 17/06/2005      
Purpose         : To delete record from todolist
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/    


CREATE     PROCEDURE dbo.proc_deletediary
(
	@LISTID INT
)
as
BEGIN
		DELETE FROM TODOLIST
		WHERE listid = @LISTID	
END






GO

