IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProcessDiaryFollowUpDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProcessDiaryFollowUpDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name        : dbo.Proc_UpdateProcessDiaryFollowUpDate    
Created by        : Pravesh k Chandel  
Date                : 21-06-2007    
Purpose          : Update the Diary Entry FolowUp Date for process related diary entries   
Revison History  :            
Used In          : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE  PROC dbo.Proc_UpdateProcessDiaryFollowUpDate            
(            
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID smallint,  
 @PROCESS_ROW_ID  int,            
 @FOLLOWUPDATE datetime   
)            
AS            
            
BEGIN            
  
 UPDATE TODOLIST   
  SET FOLLOWUPDATE = @FOLLOWUPDATE   
 WHERE   
  CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  PROCESS_ROW_ID = @PROCESS_ROW_ID AND  
  (RULES_VERIFIED IS NULL OR RULES_VERIFIED=0)  
  
    
END            
    



GO

