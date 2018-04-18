IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetProcessStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetProcessStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name         	: dbo.Proc_SetProcessStatus
Created by         	: Vijay Arora    
Date                 	: 26-12-2005  
Purpose           	: Set the Process Status.
Revison History   	:            
Used In           	: Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE  PROC Dbo.Proc_SetProcessStatus            
(            
 @CUSTOMER_ID INT,  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID SMALLINT,  
 @ROW_ID INT,
 @PROCESS_STATUS NVARCHAR(10)
)            
AS            
            
BEGIN            
	UPDATE POL_POLICY_PROCESS 
	SET PROCESS_STATUS = @PROCESS_STATUS
	WHERE	CUSTOMER_ID = @CUSTOMER_ID
	AND	POLICY_ID = @POLICY_ID
	AND 	POLICY_VERSION_ID = @POLICY_VERSION_ID
	AND 	ROW_ID = @ROW_ID	
		
END            
    
  
  



GO

