IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--sp_helptext Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES  
  
/*----------------------------------------------------------              
Proc Name   : dbo.Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES             
Created by  : Pradeep              
Date        : 23 May,2005            
Purpose     :               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/        
-- drop proc Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES     
CREATE     PROCEDURE dbo.Proc_DeleteHOME_OWNER_RECREATIONAL_VEHICLES      
(      
       
 @CUSTOMER_ID Int,      
 @APP_ID Int,      
 @APP_VERSION_ID SmallInt,      
 @REC_VEH_ID SmallInt      
)      
      
As      
      
     
DELETE FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES_COVERAGES      
WHERE CUSTOMER_ID = CUSTOMER_ID AND      
       APP_ID = @APP_ID AND       
       APP_VERSION_ID = @APP_VERSION_ID AND      
       REC_VEH_ID = @REC_VEH_ID      
      
DELETE FROM APP_HOMEOWNER_REC_VEH_ADD_INT      
WHERE CUSTOMER_ID = CUSTOMER_ID AND      
       APP_ID = @APP_ID AND       
       APP_VERSION_ID = @APP_VERSION_ID AND      
       REC_VEH_ID = @REC_VEH_ID  

--Done for Itrack Issue 6737 on 20 Nov 09
DELETE FROM APP_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE        
WHERE CUSTOMER_ID = CUSTOMER_ID AND      
      APP_ID = @APP_ID AND       
      APP_VERSION_ID = @APP_VERSION_ID AND        
      RECREATIONAL_VEH_ID = @REC_VEH_ID  

--Update Assign Rec Veh field at watercraft driver details table
	UPDATE APP_WATERCRAFT_DRIVER_DETAILS SET APP_REC_VEHICLE_PRIN_OCC_ID=NULL,REC_VEH_ID=NULL
		WHERE CUSTOMER_ID = CUSTOMER_ID AND  
			APP_ID = @APP_ID AND   
			APP_VERSION_ID = @APP_VERSION_ID AND  
			REC_VEH_ID = @REC_VEH_ID 
  
IF @@ERROR <> 0      
BEGIN      
 RETURN -10      
END       
      
DELETE FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES      
WHERE CUSTOMER_ID = CUSTOMER_ID AND      
      APP_ID = @APP_ID AND       
      APP_VERSION_ID = @APP_VERSION_ID AND      
      REC_VEH_ID = @REC_VEH_ID         
    
    --Update Endorsement       
 EXEC  Proc_UpdateHomeEndorsementFromRV   @CUSTOMER_ID,@APP_ID, @APP_VERSION_ID    


      
RETURN 1         
      
  
--select * from POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE WHERE CUSTOMER_ID=547 AND POLICY_ID=83 AND POLICY_VERSION_ID =2





GO

