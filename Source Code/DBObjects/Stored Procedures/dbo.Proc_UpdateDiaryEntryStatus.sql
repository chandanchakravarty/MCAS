IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDiaryEntryStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDiaryEntryStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name        : dbo.Proc_UpdateDiaryEntryStatus  
Created by        : Vijay Arora  
Date                : 22-12-2005  
Purpose          : Update the Diary Entry Status  
Revison History  :          
Used In          : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE  PROC dbo.Proc_UpdateDiaryEntryStatus          
(          
	@LIST_ID     int,          
	@LIST_OPEN   char(2)  
)          
AS          
          
BEGIN          

	UPDATE TODOLIST SET LISTOPEN = @LIST_OPEN WHERE LISTID = @LIST_ID  
  
END          
  





GO

