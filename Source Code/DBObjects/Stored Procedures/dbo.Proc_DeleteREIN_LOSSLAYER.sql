IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteREIN_LOSSLAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteREIN_LOSSLAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Dbo.Proc_DeleteREIN_LOSSLAYER    
CREATED BY  : Swarup   
CREATED DATE : August 14, 2007    
Purpose     : Delete records from REIN_LOSSLAYER table    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc Proc_DeleteREIN_LOSSLAYER    
CREATE PROC Dbo.Proc_DeleteREIN_LOSSLAYER    
(    
 @LOSS_LAYER_ID  int    
)    
AS    
BEGIN    
    
 DELETE FROM MNT_REIN_LOSSLAYER     
  WHERE LOSS_LAYER_ID = @LOSS_LAYER_ID   
 return 1    
     
END    
    
    
    
    
    
    
    
    
    
    
    
    
    
  



GO

