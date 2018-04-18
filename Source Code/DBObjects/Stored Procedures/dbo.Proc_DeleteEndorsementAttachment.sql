IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteEndorsementAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteEndorsementAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DeleteEndorsementAttachment                
Created by      : Mohit Agarwal                
Date            : 06/23/2007                
Purpose       :Delete an endorsement attachment                
Revison History :                
Used In        : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/  
--drop PROC dbo.Proc_DeleteEndorsementAttachment                
CREATE PROC dbo.Proc_DeleteEndorsementAttachment                
(                
@ENDORSEMENT_ATTACH_ID     int                
)                
AS                
BEGIN         
 DELETE FROM MNT_ENDORSEMENT_ATTACHMENT WHERE ENDORSEMENT_ATTACH_ID = @ENDORSEMENT_ATTACH_ID
            
END          
  
      
    
  



GO

