IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_btnVoid_Reverse_Visible]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_btnVoid_Reverse_Visible]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
--BEGIN TRAN  
--DROP PROC Proc_btnVoid_Reverse_Visible  
--GO  
/* ----------------------------------------------------------                                                                                                                                                                                
Proc Name                : Dbo.Proc_btnVoid_Reverse_Visible                                                                                                                                                                 
Created by               : Sibin                                                                                                                                                                                
Date                     : 23 June 2010                                                                                                                                
Purpose                  : To check whether there is any incomplete activity and if so do not show void and reverse button                                                                                                                                
Used In                  : Wolverine                                                                                                                                                                                
------------------------------------------------------------                                                                                                                                                                                
Date     Review By          Comments                                                                                                                                                                                
------   ------------       -------------------------*/   
--DROP PROC Proc_btnVoid_Reverse_Visible  
CREATE PROC [dbo].[Proc_btnVoid_Reverse_Visible]  
(  
 @CLAIM_ID INT  
)  
AS  
  
BEGIN  
  
 IF EXISTS(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_STATUS = 11800  AND IS_ACTIVE = 'Y' ORDER BY ACTIVITY_ID DESC)  
 BEGIN  
  SELECT -1 AS VOID_REVERSE_BUTTON_VISIBLE  
 END  
 ELSE  
 BEGIN  
  SELECT 1 AS VOID_REVERSE_BUTTON_VISIBLE  
 END  
END  
  
--GO  
--EXEC Proc_btnVoid_Reverse_Visible 2745  
--ROLLBACK TRAN  
  
  
  
GO

