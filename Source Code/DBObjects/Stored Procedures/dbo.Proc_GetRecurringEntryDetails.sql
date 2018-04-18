IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRecurringEntryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRecurringEntryDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetRecurringEntryDetails
Created by      : Vijay Arora    
Date            : 24/02/2006    
Purpose         : Get the Recurring Entry Details   
Revison History :        
Used In         :   Wolverine        
-------------------------------------------------------------    
Modified By     :     
Modified On     :     
Purpose         :     
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetRecurringEntryDetails    
(        
 @JOURNAL_ID INT
)        
AS        
BEGIN        

	SELECT * FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID
	
END    
  



GO

