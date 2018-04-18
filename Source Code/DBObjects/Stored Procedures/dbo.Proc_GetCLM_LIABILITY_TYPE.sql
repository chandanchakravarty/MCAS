IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_LIABILITY_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_LIABILITY_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
                            
Proc Name       : Proc_GetCLM_LIABILITY_TYPE    
Created by      : Sumit Chhabra            
Date            : 09/05/2006                            
Purpose         : Get Liability Type data from CLM_LIABILITY_TYPE            
Revison History :                            
Used In                   : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
create PROC dbo.Proc_GetCLM_LIABILITY_TYPE                   
(                            
 @LIABILITY_TYPE_ID int,  
 @CLAIM_ID int  
)                            
AS                            
BEGIN                            
 SELECT    
  LIABILITY_TYPE_ID,  
  CLAIM_ID,  
  PREMISES_INSURED,  
  OTHER_DESCRIPTION,  
  TYPE_OF_PREMISES,  
  IS_ACTIVE  
 FROM  
  CLM_LIABILITY_TYPE  
 WHERE  
  LIABILITY_TYPE_ID = @LIABILITY_TYPE_ID AND  
  CLAIM_ID = @CLAIM_ID  
   
END            



GO

