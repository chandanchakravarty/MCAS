IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_CLAIM_INFO_DESCRIPTION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_CLAIM_INFO_DESCRIPTION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                
Proc Name       : Proc_GetCLM_CLAIM_INFO_DESCRIPTION                  
Created by      : Sumit Chhabra                  
Date            : 05/06/2006                                  
Purpose         : Get Claim Description from claim notification screen for claim payment screen  
Revison History :                                  
Used In                   : Wolverine                                                                          
------------------------------------------------------------                                                                                
Date     Review By          Comments                                                                                
------   ------------       -------------------------*/                                                                                
CREATE PROC dbo.Proc_GetCLM_CLAIM_INFO_DESCRIPTION                                                                      
@CLAIM_ID int  
AS                                                                                
BEGIN                               
 SELECT   
  ISNULL(CLAIM_DESCRIPTION,'') CLAIM_DESCRIPTION   
 FROM   
  CLM_CLAIM_INFO   
 WHERE  
  CLAIM_ID=@CLAIM_ID  
END                          
      
    
  



GO

