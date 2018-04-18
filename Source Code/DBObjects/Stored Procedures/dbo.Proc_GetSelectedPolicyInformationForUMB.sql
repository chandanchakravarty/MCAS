IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSelectedPolicyInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSelectedPolicyInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                    
Proc Name           : Proc_GetSelectedPolicyInformationForUMB                                                          
Created by          : Neeraj singh                                                                   
Date                : 10-01-2006                                                                    
Purpose             : To get the information for creating the input xml                                                                     
Revison History     :                                                                    
Used In             : Wolverine                                                                    
------------------------------------------------------------                                                                    
Date     Review By          Comments                                                                    
------   ------------       -------------------------*/              
                 
--drop PROC dbo.Proc_GetSelectedPolicyInformationForUMB 
CREATE     PROC dbo.Proc_GetSelectedPolicyInformationForUMB    
 (    
  @CUSTOMER_ID       INT,                                                                    
  @ID           INT,                                                                    
  @VERSION_ID      INT    
 )    
    
AS    
BEGIN    
SET QUOTED_IDENTIFIER OFF    
select POLICY_NUMBER, POLICY_LOB,  POLICY_COMPANY, convert(varchar(20),IS_POLICY)
from APP_UMBRELLA_UNDERLYING_POLICIES     
 WHERE CUSTOMER_ID = @CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID              
order by POLICY_LOB desc  
END     
--execute dbo.Proc_GetSelectedPolicyInformationForUMB 626,170,1  
  
--select * from APP_UMBRELLA_UNDERLYING_POLICIES  



GO

