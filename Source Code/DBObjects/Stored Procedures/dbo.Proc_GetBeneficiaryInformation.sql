IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBeneficiaryInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBeneficiaryInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[Proc_GetBeneficiaryInformation]]                                                             
Created by            : Aditya Goel                                                             
Date                  : 22/02/2011                                                             
Purpose               : To get Beneficiary information details                    
Revison History       :                                                              
Used In               : Policy module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc [Proc_GetBeneficiaryInformation] 28033,437,1,1 ,1                                               
------   ------------       -------------------------*/                      
                    
CREATE PROC [dbo].[Proc_GetBeneficiaryInformation]                      
@CUSTOMER_ID  int,                                                
@POLICY_ID   int,                                                
@POLICY_VERSION_ID  int,           
@RISK_ID int,                  
@BENEFICIARY_ID   int                      
                   
AS                                                                                                
BEGIN                         
                      
    SELECT [CUSTOMER_ID] ,          
    [POLICY_ID],          
    [POLICY_VERSION_ID],          
    [RISK_ID],          
    [BENEFICIARY_ID],          
       [BENEFICIARY_NAME]                  
      ,[BENEFICIARY_SHARE]                  
      ,[BENEFICIARY_RELATION]                  
      ,[IS_ACTIVE]                  
      ,[CREATED_BY]                  
      ,[CREATED_DATETIME]                  
      ,[MODIFIED_BY]                  
      ,[LAST_UPDATED_DATETIME]                  
  FROM [dbo].[POL_BENEFICIARY]                 
  WHERE  ( 
  [BENEFICIARY_ID] = @BENEFICIARY_ID AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
   AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID AND   IS_ACTIVE='Y'  )      -- changed by praveer for TFS # 2393      
          
      
                      
END 
GO

