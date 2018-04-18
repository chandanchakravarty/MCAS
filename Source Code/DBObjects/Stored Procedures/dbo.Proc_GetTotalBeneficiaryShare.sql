IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTotalBeneficiaryShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTotalBeneficiaryShare]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------                                                        
Proc Name             : Dbo.[Proc_GetTotalBeneficiaryShare]                                                     
Created by            : Aditya Goel                                                       
Date                  : 22/12/2010                                                       
Purpose               : To get Beneficiary information details              
Revison History       :                                                        
Used In               : Policy module              
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc [Proc_GetTotalBeneficiaryShare] 28169,23,1,1,37                                            
------   ------------       -------------------------*/                
              
CREATE PROC [dbo].[Proc_GetTotalBeneficiaryShare]                
@CUSTOMER_ID  int,                                          
@POLICY_ID   int,                                          
@POLICY_VERSION_ID  int,     
@RISK_ID int,            
@BENEFICIARY_ID   int                
             
AS                                                                                          
BEGIN                   
     
  SELECT ISNULL(SUM(BENEFICIARY_SHARE),0) AS [BENEFICIARY_SHARE]  
  FROM   [POL_BENEFICIARY]  
  WHERE   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID AND BENEFICIARY_ID!=@BENEFICIARY_ID  
            
                
END                
              
GO

