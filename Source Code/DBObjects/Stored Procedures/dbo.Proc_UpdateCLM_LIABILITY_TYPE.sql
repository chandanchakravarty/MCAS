IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_LIABILITY_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_LIABILITY_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
                            
Proc Name       : Proc_UpdateCLM_LIABILITY_TYPE    
Created by      : Sumit Chhabra            
Date            : 09/05/2006                            
Purpose         : Update of Liability Type data in CLM_LIABILITY_TYPE            
Revison History :                            
Used In                   : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE PROC dbo.Proc_UpdateCLM_LIABILITY_TYPE                   
(                            
 @LIABILITY_TYPE_ID int,  
 @CLAIM_ID int,  
 @PREMISES_INSURED int,  
 @OTHER_DESCRIPTION varchar(25),  
 @TYPE_OF_PREMISES varchar(256),  
 @MODIFIED_BY int,      
 @LAST_UPDATED_DATETIME datetime                      
)                            
AS                            
BEGIN                            
 UPDATE   
  CLM_LIABILITY_TYPE  
 SET  
  PREMISES_INSURED = @PREMISES_INSURED,  
  OTHER_DESCRIPTION = @OTHER_DESCRIPTION,  
  TYPE_OF_PREMISES = @TYPE_OF_PREMISES,  
  @MODIFIED_BY = @MODIFIED_BY,  
  @LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
 WHERE  
  LIABILITY_TYPE_ID = @LIABILITY_TYPE_ID AND  
  CLAIM_ID = @CLAIM_ID  
   
END                  
      
      
    


GO

