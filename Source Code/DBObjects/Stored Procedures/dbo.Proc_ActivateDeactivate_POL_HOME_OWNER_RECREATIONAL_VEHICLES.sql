IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES               
Created by  : Pradeep                
Date        : 11/10/2005            
Purpose     : Activates/Deactivates a record in POL_HOME_OWNER_RECREATIONA_VEHICLES                
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/          
  
-- drop proc Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES        
CREATE    PROCEDURE dbo.Proc_ActivateDeactivate_POL_HOME_OWNER_RECREATIONAL_VEHICLES        
(        
         
 @CUSTOMER_ID Int,        
 @POLICY_ID Int,        
 @POLICY_VERSION_ID SmallInt,        
 @REC_VEH_ID SmallInt,        
 @ACTIVE nchar(1)        
)        
        
As    

IF(@ACTIVE='N')
BEGIN
	UPDATE POL_WATERCRAFT_DRIVER_DETAILS SET APP_REC_VEHICLE_PRIN_OCC_ID=NULL,REC_VEH_ID=NULL
	WHERE CUSTOMER_ID = CUSTOMER_ID AND  
		POLICY_ID = @POLICY_ID AND         
        POLICY_VERSION_ID = @POLICY_VERSION_ID AND 
		REC_VEH_ID = @REC_VEH_ID 
END    
        
UPDATE POL_HOME_OWNER_RECREATIONAL_VEHICLES        
SET ACTIVE = @ACTIVE        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
      POLICY_ID = @POLICY_ID AND         
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
      REC_VEH_ID = @REC_VEH_ID            
EXEC Proc_UpdatePolicyHomeEndorsementFromRV @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID         
        
RETURN 1           
        
        
        
        
        
        
        
      
    
  
  
  



GO

