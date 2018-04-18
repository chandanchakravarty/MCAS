IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_AUTHORITY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_AUTHORITY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
                    
Proc Name       : Proc_DeleteCLM_AUTHORITY_LIMIT    
Created by      : Sumit Chhabra    
Date            : 21/04/2006                    
Purpose         : Delete Authority Limit data from CLM_AUTHORITY_LIMIT    
Revison History :                    
Used In                   : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
CREATE PROC Dbo.Proc_DeleteCLM_AUTHORITY_LIMIT                    
(                    
 @LIMIT_ID int                
)                    
AS                    
BEGIN          
 --Update CLM_ADJUSTER_AUTHORITY set limit_id to null wherever it is assigned
-- UPDATE CLM_ADJUSTER_AUTHORITY SET LIMIT_ID=null where LIMIT_ID=@LIMIT_ID
--Delete data from CLM_AUTHORITY_LIMIT  
 DELETE FROM CLM_AUTHORITY_LIMIT  WHERE LIMIT_ID=@LIMIT_ID    
     
END          
  



GO

