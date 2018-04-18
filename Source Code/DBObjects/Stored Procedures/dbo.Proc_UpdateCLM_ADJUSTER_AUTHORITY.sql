IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ADJUSTER_AUTHORITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ADJUSTER_AUTHORITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 
/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateCLM_ADJUSTER_AUTHORITY      
Created by      : Sumit Chhabra          
Date            : 25/04/2006            
Purpose         : Update data at CLM_ADJUSTER_AUTHORITY  for an adjuster and the chosen LOB    
Created by      : Sumit Chhabra           
Revison History :            
Used In        : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC dbo.Proc_UpdateCLM_ADJUSTER_AUTHORITY        
 @ADJUSTER_AUTHORITY_ID int output,    
-- @LOB_ID int,    
 @LIMIT_ID int,  
 @ADJUSTER_ID int,    
 @EFFECTIVE_DATE datetime,    
 @MODIFIED_BY int,    
 @LAST_UPDATED_DATETIME datetime,
 @NOTIFY_AMOUNT decimal(12,2)
AS            
BEGIN            
 UPDATE CLM_ADJUSTER_AUTHORITY SET     
 -- LOB_ID = @LOB_ID,    
  LIMIT_ID = @LIMIT_ID,    
  EFFECTIVE_DATE = @EFFECTIVE_DATE,    
  MODIFIED_BY = @MODIFIED_BY,    
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,
  NOTIFY_AMOUNT = @NOTIFY_AMOUNT    
 WHERE    
  ADJUSTER_AUTHORITY_ID = @ADJUSTER_AUTHORITY_ID AND  
 ADJUSTER_ID = @ADJUSTER_ID  
END       






GO

