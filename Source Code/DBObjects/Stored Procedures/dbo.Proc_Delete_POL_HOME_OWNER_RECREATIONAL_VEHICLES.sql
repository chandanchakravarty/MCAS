IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name   : dbo.Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES               
Created by  : Pradeep                
Date        : 11/10/2005              
Purpose     :                 
Revison History  :                      
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/          
-- drop proc Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES        
CREATE     PROCEDURE dbo.Proc_Delete_POL_HOME_OWNER_RECREATIONAL_VEHICLES        
(        
         
 @CUSTOMER_ID Int,        
 @POLICY_ID Int,        
 @POLICY_VERSION_ID SmallInt,        
 @REC_VEH_ID SmallInt        
)        
        
As        
        
IF EXISTS        
(        
 SELECT * FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES        
 WHERE        
 CUSTOMER_ID = CUSTOMER_ID AND        
       POLICY_ID = @POLICY_ID AND         
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
       REC_VEH_ID = @REC_VEH_ID        
)        
DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES        
WHERE CUSTOMER_ID = CUSTOMER_ID AND        
       POLICY_ID = @POLICY_ID AND         
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
       REC_VEH_ID = @REC_VEH_ID       
  
DELETE FROM POL_HOMEOWNER_REC_VEH_ADD_INT        
WHERE CUSTOMER_ID = CUSTOMER_ID AND        
       POLICY_ID = @POLICY_ID AND         
       POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
       REC_VEH_ID = @REC_VEH_ID  

UPDATE pol_WATERCRAFT_DRIVER_DETAILS SET APP_REC_VEHICLE_PRIN_OCC_ID=NULL,REC_VEH_ID=NULL
	WHERE CUSTOMER_ID = CUSTOMER_ID AND  
		POLICY_ID = @POLICY_ID AND         
        POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
		REC_VEH_ID = @REC_VEH_ID    
  
  
        
IF @@ERROR <> 0        
BEGIN        
 RETURN -10        
END         
        
DELETE FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES        
WHERE CUSTOMER_ID = CUSTOMER_ID AND        
      POLICY_ID = @POLICY_ID AND         
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
      REC_VEH_ID = @REC_VEH_ID 

--Done for Itrack Issue 6737 on 20 Nov 09

DELETE FROM POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE        
WHERE CUSTOMER_ID = CUSTOMER_ID AND        
      POLICY_ID = @POLICY_ID AND         
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
      RECREATIONAL_VEH_ID = @REC_VEH_ID           
         
EXEC Proc_UpdatePolicyHomeEndorsementFromRV @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID    
        
RETURN 1           
        
        
        
        
        
        
        
        
      
    
    
    
  
  
  
  




GO

