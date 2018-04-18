IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNumberOfLineItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNumberOfLineItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Dbo.Proc_GetNumberOfLineItems
Created by      : Vijay Arora
Date            : 05-04-2006
Purpose     	: Return the Number of Line Items of particular Journal Entry.
Revison History :  
Used In  	: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetNumberOfLineItems  
(  
 @JOURNAL_ID      int,  
 @NUMBER_OF_ITEMS  int OUTPUT  
)  
AS  
BEGIN  

SELECT @NUMBER_OF_ITEMS = COUNT(JOURNAL_ID) FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID
print @NUMBER_OF_ITEMS

END



GO

