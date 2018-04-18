IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteBeneficiaryInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteBeneficiaryInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
/*----------------------------------------------------------                              
Proc Name      : dbo.[Proc_DeleteBeneficiaryInfo]                              
Created by     : Aditya Goel                       
Date           : 09-03-2011
Modify by      : 
Date           :                    
Purpose        : Delete data from POL_BENEFICIARY                                                    
Used In        : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.[Proc_DeleteBeneficiaryInfo]  28169,49,1  
    
CREATE PROC [dbo].[Proc_DeleteBeneficiaryInfo]      
(       
@CUSTOMER_ID  int,                                          
@POLICY_ID   int,                                          
@POLICY_VERSION_ID  int,     
@RISK_ID int,            
@BENEFICIARY_ID   int 
--@PERSONAL_INFO_ID INT                      
)                  
AS                  
BEGIN                  
  DELETE FROM POL_BENEFICIARY  WHERE  CUSTOMER_ID =@CUSTOMER_ID 
  AND POLICY_ID = @POLICY_ID 
  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID 
  AND  RISK_ID = @RISK_ID AND
 BENEFICIARY_ID = @BENEFICIARY_ID    
END  
      
      
   
        
      
GO

