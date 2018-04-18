IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyLateFee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyLateFee]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name       : dbo.Proc_GetPolicyLateFee
Created by      :  Pravesh K Chandel                                                        
Date            :  2-Aug-2007                                                            
Purpose         :  To get Late Fee for Policy
Revison History :                                                            
Used In         :    Wolverine                                                            
------------------------------------------------------------                                                            
Date     Review By          Comments                                                            
------   ------------       -------------------------*/                                                            
--drop PROC Dbo.Proc_GetPolicyLateFee                     
CREATE PROC dbo.Proc_GetPolicyLateFee                                                           
(                                                                  
@CUSTOMER_ID int,                                                  
@POLICY_ID int,                                                  
@POLICY_VERSION_ID int                              
)                                                                  
AS                                                                  
BEGIN                         
                    
--DECLARE  @LATE_FEES Decimal(18,2)                      
SELECT  
isnull(cast(INS_MASTER.LATE_FEES as int),0) as LATE_FEES                      
FROM POL_CUSTOMER_POLICY_LIST CPL with(nolock)                    
INNER JOIN ACT_INSTALL_PLAN_DETAIL INS_MASTER  with(nolock)                    
ON INS_MASTER.IDEN_PLAN_ID = CPL.INSTALL_PLAN_ID                      
WHERE CPL.CUSTOMER_ID =@CUSTOMER_ID                       
AND   CPL.POLICY_ID = @POLICY_ID                  
AND   CPL.POLICY_VERSION_ID = @POLICY_VERSION_ID                       
                      
                  
--exec Proc_GetTransactionHistory @CUSTOMER_ID , @POLICY_ID , NULL                      
                    
          
END               
          
      
      
    
   







GO

