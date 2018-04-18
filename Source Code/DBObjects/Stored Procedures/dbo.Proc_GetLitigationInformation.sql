IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLitigationInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLitigationInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                
Proc Name             : Dbo.Proc_GetLitigationInformation                                                
Created by            : Santosh Kumar Gautam                                               
Date                  : 22/11/2010                                               
Purpose               : To get litigation information details      
Revison History       :                                                
Used In               : claim module      
------------------------------------------------------------                                                
Date     Review By          Comments                   
          
drop Proc Proc_GetLitigationInformation                                       
------   ------------       -------------------------*/        
      
CREATE PROC [dbo].[Proc_GetLitigationInformation]        
      
@LITIGATION_ID   int        
      
AS                                                                                  
BEGIN           
        
    SELECT [CLAIM_ID]    
      ,[LITIGATION_ID]    
      ,[JUDICIAL_PROCESS_NO]    
      ,[JUDICIAL_COMPLAINT_STATE]    
      ,[PLAINTIFF_NAME]    
      ,[PLAINTIFF_CPF]    
      ,[PLAINTIFF_REQUESTED_AMOUNT]    
      ,[DEFEDANT_OFFERED_AMOUNT]    
      ,[ESTIMATE_CLASSIFICATION]    
      ,[OPERATION_REASON]    
      ,[JUDICIAL_PROCESS_DATE]
      ,[EXPERT_SERVICE_ID]
      ,[IS_ACTIVE]          
  FROM [dbo].[CLM_LITIGATION_INFORMATION]    
  WHERE ([LITIGATION_ID]=@LITIGATION_ID )      
    
        
END        
      
      
      

GO

