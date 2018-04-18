IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_chkVoid_Activities]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_chkVoid_Activities]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
--begin tran  
--DROP PROC dbo.Proc_chkVoid_Activities  
--go  
/*----------------------------------------------------------      
Proc Name       : dbo.Proc_chkVoid_Activities      
Created by      : Sibin Thomas Philip      
Date            : 4/29/2010      
Purpose         : To check if check has been cleared or not      
Revison History :      
Used In   : Wolverine      
------------------------------------------------------------ */      
-- DROP PROC dbo.Proc_chkVoid_Activities      
CREATE PROC [dbo].[Proc_chkVoid_Activities]      
(      
 @ACTION_ON_PAYMENT INT  
)      
AS      
  
BEGIN      
 IF((SELECT IS_VOID_ACTIVITY FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @ACTION_ON_PAYMENT AND TYPE_ID=8) = 1)  
 BEGIN   
  SELECT 1 AS IS_VOID_ACTIVITY  
 END  
 ELSE  
 BEGIN  
  SELECT 0 AS IS_VOID_ACTIVITY  
 END  
END    
--  
--go  
--exec Proc_chkVoid_Activities 240  
--rollback tran   
  
  
  
  
  
  
  
  
  
  
GO

