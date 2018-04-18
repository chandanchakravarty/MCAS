IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClaimActivityDescription]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClaimActivityDescription]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateClaimActivityDescription            
Created by      : Sumit Chhabra
Date            : 07/11/2007            
Purpose     	: To update activity description at table named CLM_ACTIVITY            
Revison History :            
Used In  : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC dbo.Proc_UpdateClaimActivityDescription            
(            
 @CLAIM_ID     int,            
 @ACTIVITY_ID  int,
 @DESCRIPTION VARCHAR(255)            
)            
AS            
BEGIN      
	UPDATE CLM_ACTIVITY SET [DESCRIPTION]= @DESCRIPTION
		WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID            
	RETURN 1
END            
          
        
      
    
  











GO

