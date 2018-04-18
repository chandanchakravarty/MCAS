IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_CLAIM_INFO_DESCRIPTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_CLAIM_INFO_DESCRIPTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                              
Proc Name       : Proc_UpdateCLM_CLAIM_INFO_DESCRIPTION                
Created by      : Sumit Chhabra                
Date            : 05/06/2006                                
Purpose         : Update Claim Description at claim notification screen for claim payment screen
Revison History :                                
Used In                   : Wolverine                                                                        
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                                              
------   ------------       -------------------------*/                                                                              
CREATE PROC dbo.Proc_UpdateCLM_CLAIM_INFO_DESCRIPTION                                                                    
@CLAIM_ID int,
@CLAIM_DESCRIPTION varchar(500)
AS                                                                              
BEGIN    
	UPDATE 
		CLM_CLAIM_INFO 
	SET
		CLAIM_DESCRIPTION = @CLAIM_DESCRIPTION
	WHERE
		CLAIM_ID=@CLAIM_ID
END  


GO

