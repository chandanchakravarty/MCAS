IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_ACTIVITY_RESERVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_ACTIVITY_RESERVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                      
Proc Name       : dbo.Proc_DeleteCLM_ACTIVITY_RESERVE                                          
Created by      : Sumit Chhabra                                                    
Date            : 30/05/2006                                                      
Purpose         : Delete data at CLM_ACTIVITY_RESERVE table for claim reserve screen                                  
Created by      : Sumit Chhabra                                                     
Revison History :                                                      
Used In        : Wolverine                                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/                                                      
CREATE PROC dbo.Proc_DeleteCLM_ACTIVITY_RESERVE                                            
@CLAIM_ID int                                  
AS                                                      
BEGIN                                                      
 DELETE FROM CLM_ACTIVITY_RESERVE     
	WHERE CLAIM_ID=@CLAIM_ID
   
END


GO

