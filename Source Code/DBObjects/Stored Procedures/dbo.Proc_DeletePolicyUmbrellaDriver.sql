IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyUmbrellaDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyUmbrellaDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name        : dbo.Proc_DeletePolicyUmbrellaDriver                                    
Created by       : Sumit Chhabra  
Date             : 21/03/2006                                    
Purpose       : Delete Policy Umbrella Driver Details                                    
Revison History :                                    
Used In  : Wolverine                                     
  
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                     
CREATE PROCEDURE Proc_DeletePolicyUmbrellaDriver                                    
(                                    
                                     
 @CUSTOMER_ID int,                                    
 @POLICY_ID  int,                                    
 @POLICY_VERSION_ID smallint,                                    
 @DRIVER_ID INT                                     
  
)                                    
AS                                    
BEGIN                                    
 DELETE FROM POL_UMBRELLA_DRIVER_DETAILS  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                                     
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                    
                    
 --delete data from the app_water_mvr_information table also                          
 DELETE FROM POL_UMBRELLA_MVR_INFORMATION                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                                     
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID                                    
  
                      
END                      
                 



GO

