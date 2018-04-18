IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyStatusforDepositItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyStatusforDepositItem]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetPolicyStatusforDepositItem                          
Created by      : Pradeep Kushwaha                     
Date            : 28/Oct/2010                                    
Purpose         : To get the Policy Status description for deposit items
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_GetPolicyStatusforDepositItem    
exec Proc_GetPolicyStatusforDepositItem     
*/                  
                       
CREATE PROC  [dbo].[Proc_GetPolicyStatusforDepositItem]  
 (                    
  @CUSTOMER_ID   int,
  @POLICY_ID   int,
  @POLICY_VERSION_ID  smallint
 )                    
AS                    
BEGIN         
SELECT STATUS_MASTER.POLICY_DESCRIPTION 
          
FROM POL_CUSTOMER_POLICY_LIST POLICY with(nolock)             
	LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER with(nolock)     
	ON STATUS_MASTER.POLICY_STATUS_CODE = ISNULL(POLICY.POLICY_STATUS,POLICY.APP_STATUS)
    where  POLICY.CUSTOMER_ID=@CUSTOMER_ID AND POLICY.POLICY_ID=@POLICY_ID AND POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID
    
END  
      
                                     
       
     
GO

