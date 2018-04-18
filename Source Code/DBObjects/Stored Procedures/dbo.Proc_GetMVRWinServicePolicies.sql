IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMVRWinServicePolicies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMVRWinServicePolicies]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name           : Dbo.Proc_GetMVRWinServicePolicies  
Created by            : Vijay Arora  
Date                    : 20-03-2006  
Purpose                :     
Revison History  :    
Used In                 :   Wolverine      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--- DROP PROC Proc_GetMVRWinServicePolicies
CREATE PROCEDURE Proc_GetMVRWinServicePolicies    
AS    
BEGIN    
 SELECT * FROM POL_CUSTOMER_POLICY_LIST   
 WHERE POLICY_STATUS IN ('SUSPENDED', 'UENDRS','RENEWED')  
 AND MVR_WIN_SERVICE <> 'Y'
 AND CONVERT(VARCHAR(100),CUSTOMER_ID+POLICY_ID+POLICY_VERSION_ID) NOT IN   
 (  
  SELECT CONVERT(VARCHAR(100),CUSTOMER_ID+POLICY_ID+POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST   
  WHERE (POLICY_STATUS IN ('SUSPENDED', 'UENDRS','RENEWED') AND POLICY_LOB = 2 AND POLICY_SUBLOB = 1)  
 )  
  
END  
  
  
  



GO

