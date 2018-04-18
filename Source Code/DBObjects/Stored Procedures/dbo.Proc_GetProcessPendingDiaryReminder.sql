IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProcessPendingDiaryReminder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProcessPendingDiaryReminder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetProcessPendingDiaryReminder  
Created by      : Vijay Arora      
Date            : 01-02-2006  
Purpose         : Selects the records from policy process table which are pending  
    and needs to send the reminder to the underwriter of the policy.  
Revison History :          
Used In         : Wolverine   
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/        
CREATE PROC Proc_GetProcessPendingDiaryReminder      
AS      
BEGIN      
   
 DECLARE @PENDING_STATUS_DAYS SMALLINT   
 DECLARE @TEMP_DATE DATETIME  
  
 SELECT @PENDING_STATUS_DAYS = SYS_NUM_DAYS_EXPIRE FROM MNT_SYSTEM_PARAMS    
 SELECT @TEMP_DATE = DATEADD(day,-@PENDING_STATUS_DAYS,GETDATE())  
  
 SELECT * FROM POL_POLICY_PROCESS   
 WHERE CREATED_DATETIME > @TEMP_DATE AND PROCESS_STATUS = 'PENDING'  
  
END      
    
  
  
  
  
    
  
  
    
  
  
  
  



GO

