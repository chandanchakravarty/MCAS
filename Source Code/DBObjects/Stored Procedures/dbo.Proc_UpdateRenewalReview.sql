IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRenewalReview]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRenewalReview]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateRenewalReview      
Created by      : Vijay Arora          
Date            : 02-02-2006      
Purpose         : Update the status for Diary Entry Reminder for Renewal Reminder.     
Revison History :              
Used In         : Wolverine       
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/            
CREATE PROC Proc_UpdateRenewalReview          
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID SMALLINT
AS          
BEGIN          

UPDATE POL_CUSTOMER_POLICY_LIST
SET SEND_RENEWAL_DIARY_REM = 'Y'
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
POLICY_ID = @POLICY_ID AND
POLICY_VERSION_ID = @POLICY_VERSION_ID       
     
END          
        



GO

