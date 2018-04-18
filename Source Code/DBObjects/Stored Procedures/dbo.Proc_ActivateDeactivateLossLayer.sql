IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateLossLayer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateLossLayer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivateCoverageCatagory        
CREATED BY  : Swarup   
CREATED DATE : August 14, 2007     
Purpose         : To activate/deactivate the record in MNT_REIN_LOSSLAYER table        
Revison History :        
Used In         : Wolvorine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC dbo.Proc_ActivateDeactivateLossLayer        
(        
 @LOSS_LAYER_ID int,        
 @IS_ACTIVE  Char(1)        
)        
AS        
BEGIN        
      
 UPDATE MNT_REIN_LOSSLAYER  SET   IS_ACTIVE = @IS_ACTIVE        
 WHERE  LOSS_LAYER_ID= @LOSS_LAYER_ID        
END        
        
        
      
    
  



GO

