IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_LINKED_CLAIMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_LINKED_CLAIMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetCLM_LINKED_CLAIMS                                                          
Created by      : Sumit Chhabra                                                              
Date            : 11/07/2006                                                                
Purpose         : Fetch list of claims           
Created by      : Sumit Chhabra                                                               
Revison History :                                                                
Used In        : Wolverine                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------*/                                                                
--DROP PROC dbo.Proc_GetCLM_LINKED_CLAIMS                                                                                   
CREATE PROC dbo.Proc_GetCLM_LINKED_CLAIMS                                                                                   
@CLAIM_ID int                                          
AS                                                                
BEGIN                                                                
 SELECT     
    
 (CAST(ISNULL(CCI.CLAIM_NUMBER,'') AS VARCHAR) + '^' + CAST(ISNULL(CCI.CUSTOMER_ID,0) AS VARCHAR) + '^' +       
 CAST(ISNULL(CCI.POLICY_ID,0) AS VARCHAR) + '^' + CAST(ISNULL(CCI.POLICY_VERSION_ID,0) AS VARCHAR) + '^' +       
 CAST(ISNULL(CCI.CLAIM_ID,0) AS VARCHAR) + '^' + CAST(ISNULL(CCI.LOB_ID,0) AS VARCHAR) + '^' +       
 CAST(ISNULL(CCI.HOMEOWNER,0) AS VARCHAR) + '^' + CAST(ISNULL(CCI.RECR_VEH,0) AS VARCHAR) + '^' +       
 CAST(ISNULL(CCI.IN_MARINE,0) AS VARCHAR) + '^' +   
 LTRIM(RTRIM(CAST(CONVERT(CHAR,CCI.LOSS_DATE,101) AS VARCHAR)))) AS LINKED_CLAIM_LIST,      
  CCI.CLAIM_ID,CLC.LINKED_CLAIM_ID  AS LINKED_CLAIM_ID_LIST,CCI.CLAIM_NUMBER
 FROM  
 CLM_CLAIM_INFO CCI  
 JOIN   
 CLM_LINKED_CLAIMS CLC  
 ON   
 CCI.CLAIM_ID = CLC.LINKED_CLAIM_ID  
 WHERE  
 CLC.CLAIM_ID = @CLAIM_ID AND  
 CCI.IS_ACTIVE = 'Y'  
 ORDER BY CCI.CLAIM_ID                                          
END

GO

