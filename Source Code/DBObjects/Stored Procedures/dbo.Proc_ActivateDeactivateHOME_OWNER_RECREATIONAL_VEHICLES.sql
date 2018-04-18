IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES         
Created by  : Pradeep          
Date        : 23 May,2005        
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
-- drop proc Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES  
CREATE    PROCEDURE dbo.Proc_ActivateDeactivateHOME_OWNER_RECREATIONAL_VEHICLES  
(  
   
 @CUSTOMER_ID Int,  
 @APP_ID Int,  
 @APP_VERSION_ID SmallInt,  
 @REC_VEH_ID SmallInt,  
 @ACTIVE nchar(1)  
)  
  
As  

	if(@ACTIVE='N')
	BEGIN
		UPDATE APP_WATERCRAFT_DRIVER_DETAILS SET APP_REC_VEHICLE_PRIN_OCC_ID=NULL,REC_VEH_ID=NULL
		WHERE CUSTOMER_ID = CUSTOMER_ID AND  
			APP_ID = @APP_ID AND   
			APP_VERSION_ID = @APP_VERSION_ID AND  
			REC_VEH_ID = @REC_VEH_ID 
	END

UPDATE APP_HOME_OWNER_RECREATIONAL_VEHICLES  
SET ACTIVE = @ACTIVE  
WHERE CUSTOMER_ID = CUSTOMER_ID AND  
      APP_ID = @APP_ID AND   
      APP_VERSION_ID = @APP_VERSION_ID AND  
      REC_VEH_ID = @REC_VEH_ID      
  
  --Update Endorsement     
 EXEC  Proc_UpdateHomeEndorsementFromRV   @CUSTOMER_ID,@APP_ID, @APP_VERSION_ID   
  
RETURN 1     
  
  
  
  
  
  
  
  
  



GO

