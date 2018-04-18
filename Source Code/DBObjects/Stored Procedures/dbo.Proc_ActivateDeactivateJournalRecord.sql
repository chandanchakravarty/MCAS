IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateJournalRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateJournalRecord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateJournalRecord    
Created by      : Ashwnai    
Date            : 10 May,2006   
Purpose         : To activate/deactivate the record in ACT_JOURNAL_MASTER table    
Revison History :    
Used In         : Wolvorine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivateJournalRecord    
(    
 @CODE numeric(9),    
 @IS_ACTIVE  Char(1)    
)    
AS    
BEGIN    
  
 UPDATE ACT_JOURNAL_MASTER  SET   IS_ACTIVE = @IS_ACTIVE    
 WHERE  JOURNAL_ID= @CODE    
END    
    
    
  



GO

