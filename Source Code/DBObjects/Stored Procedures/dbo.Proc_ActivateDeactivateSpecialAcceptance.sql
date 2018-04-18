IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateSpecialAcceptance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateSpecialAcceptance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivateSpecialAcceptance        
CREATED BY  : Swarup   
CREATED DATE : Sep 21, 2007     
Purpose         : To activate/deactivate the record in MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT table        
Revison History :        
Used In         : Wolvorine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/ 
--drop PROC dbo.Proc_ActivateDeactivateSpecialAcceptance          
CREATE PROC dbo.Proc_ActivateDeactivateSpecialAcceptance        
(        
 @SPECIAL_ACCEPTANCE_LIMIT_ID int,        
 @IS_ACTIVE  Char(1)        
)        
AS        
BEGIN        
      
 UPDATE MNT_REIN_SPECIAL_ACCEPTANCE_AMOUNT  SET   IS_ACTIVE = @IS_ACTIVE        
 WHERE  SPECIAL_ACCEPTANCE_LIMIT_ID= @SPECIAL_ACCEPTANCE_LIMIT_ID      
END        
        
        
      
    
  




GO

