IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckIncompleteActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckIncompleteActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_CheckIncompleteActivity    
Created by      : Sumit Chhabra    
Date            : 6/7/2006    
Purpose       : To check for existence of any incomplete activity for the claim    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_CheckIncompleteActivity     
(    
 @CLAIM_ID int    
)    
AS    
BEGIN    
declare @ACTIVITY_STATUS_INCOMPLETE int    
declare @ACTIVITY_ID int    
    
set @ACTIVITY_STATUS_INCOMPLETE= 11800    
    
IF EXISTS(SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE ACTIVITY_STATUS=@ACTIVITY_STATUS_INCOMPLETE AND CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y')    
 SELECT @ACTIVITY_ID=ACTIVITY_ID FROM CLM_ACTIVITY WHERE ACTIVITY_STATUS=@ACTIVITY_STATUS_INCOMPLETE AND CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'
ELSE    
 SET @ACTIVITY_ID=0        

RETURN @ACTIVITY_ID    
    
END    
  





GO

