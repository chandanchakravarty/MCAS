IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDiaryReminderID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDiaryReminderID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateDiaryReminderID
Created by      : Vijay Arora        
Date            : 22-02-2006    
Purpose         : Update the diary list id in table pol_policy_process
Revison History :            
Used In         : Wolverine     
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/          
CREATE PROC Proc_UpdateDiaryReminderID
(
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID SMALLINT,
	@ROW_ID INT,
	@DIARY_LIST_ID INT
)
AS        
BEGIN        
     
	UPDATE  POL_POLICY_PROCESS  SET DIARY_LIST_ID = @DIARY_LIST_ID
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND ROW_ID = @ROW_ID
   
END        
      
    
    
    
    
      
    
    
      
    
    
    
    
  



GO

