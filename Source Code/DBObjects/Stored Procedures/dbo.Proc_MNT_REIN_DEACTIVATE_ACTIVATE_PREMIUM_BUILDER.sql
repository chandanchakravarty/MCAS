IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_PREMIUM_BUILDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_PREMIUM_BUILDER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_MNT_REIN_DEACTIVATE_ACTIVATE_PREMIUM_BUILDER          
CREATED BY  : Swarup     
CREATED DATE : August 20, 2007       
Purpose         : To activate/deactivate the record in MNT_REIN_PREMIUM_BUILDER table          
Revison History :          
Used In         : Wolvorine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC dbo.Proc_MNT_REIN_DEACTIVATE_ACTIVATE_PREMIUM_BUILDER          
(          
 @PREMIUM_BUILDER_ID int,          
 @IS_ACTIVE  Char(1)          
)          
AS          
BEGIN          
        
 UPDATE MNT_REIN_PREMIUM_BUILDER  SET   IS_ACTIVE = @IS_ACTIVE          
 WHERE  PREMIUM_BUILDER_ID= @PREMIUM_BUILDER_ID          
END          
          
          
        
      
    
  



GO

