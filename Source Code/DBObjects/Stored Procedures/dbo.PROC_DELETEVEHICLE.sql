IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEVEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEVEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
-- drop proc PROC_DELETEVEHICLE
--go

CREATE PROCEDURE dbo.PROC_DELETEVEHICLE            
(            
	@CUSTOMER_ID  INT,            
	@APP_ID  INT,            
	@APP_VERSION_ID INT,            
	@VEHICLE_ID INT,            
	@CALLEDFROM varchar(10)            
)            
AS            
BEGIN            

IF UPPER(@CALLEDFROM)='UMB'             
BEGIN            
 DELETE FROM APP_UMBRELLA_VEHICLE_INFO             
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND             
  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            
        
      
--Modified by Sumit Chhabra on Nov 10,2005        
    
--Vehicled Id at drivers table be set to null to indicate that the vehicle has been de-assigned        
      
 UPDATE APP_UMBRELLA_DRIVER_DETAILS SET VEHICLE_ID=NULL        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND             
 APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            
  
END 
ELSE
BEGIN
	 DELETE FROM APP_VEHICLE_COVERAGES 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID 
	--Delete from Vehicle Endorsements          
	      
	 DELETE FROM APP_VEHICLE_ENDORSEMENTS 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND 
	 APP_ID = @APP_ID  AND          
	 APP_VERSION_ID = @APP_VERSION_ID  
	 AND VEHICLE_ID = @VEHICLE_ID       

	 DELETE FROM APP_AUTO_ID_CARD_INFO   
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID 
	      
	      
	 DELETE FROM APP_ADD_OTHER_INT    
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND  
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID 

	 DELETE FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND 
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID  
	      
	 DELETE FROM APP_VEHICLES  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND   
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            

	DELETE FROM APP_DRIVER_ASSIGNED_VEHICLE 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND    
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID   
	      
	--Modified by Sumit Chhabra on Nov 10,2005        
	      
	--Vehicled Id at drivers table be set to null to indicate that the vehicle has been de-assigned        
	     
	 UPDATE APP_DRIVER_DETAILS SET VEHICLE_ID=NULL        
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND             
	 APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID       

     /* Praveen Kasana : Itrack 5964 :If there are no other cars on the policy that are eligible for Multi Car
        Discount then change this field to Not Applicable and 
        remove/do not apply the Multi Car Discount 
		11918 - Not applicable - NOT IN
		11919 - Other car on this policy
		11920 - Other Policy with Wolverine NOT IN 

		If “Other Car on Policy”: Discount will not be applicable on single Vehicle:
		Then we have to check number of Private passenger in this policy if more than one then we will give Multi car discount,  
		(except for Camper and Travel Trailer & Utility Trailer) ,otherwise no.*/
	DECLARE @UTILITY_TRAILER INT 
	SET @UTILITY_TRAILER = 11337

	DECLARE @CAMPER_TRAVEL_TRAILER INT
	SET @CAMPER_TRAVEL_TRAILER  = 11870

	DECLARE @TRAILER INT
    SET @TRAILER = 11341

--select * from mnt_lookup_values where lookup_unique_id = 11334




    DECLARE @COUNT int
	
	SELECT @COUNT = COUNT(*) from APP_VEHICLES with(nolock)
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND 
	APP_VERSION_ID = @APP_VERSION_ID AND MULTI_CAR ='11919' 
    AND (
		ISNULL(VEHICLE_TYPE_PER,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER) 
		AND
		ISNULL(VEHICLE_TYPE_COM,0) NOT IN (@TRAILER))

    AND ISNULL(IS_ACTIVE,'') = 'Y'




      IF(@COUNT =1)

		BEGIN
			IF(SELECT ISNULL(MULTI_CAR,'') FROM APP_VEHICLES WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND 
				APP_VERSION_ID = @APP_VERSION_ID and MULTI_CAR ='11919' 
			   AND (
				ISNULL(VEHICLE_TYPE_PER,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER) 
				AND 
				ISNULL(VEHICLE_TYPE_COM,0) NOT IN (@TRAILER)
				)
				AND ISNULL(IS_ACTIVE,'') = 'Y') = '11919'
			BEGIN

				UPDATE APP_VEHICLES SET MULTI_CAR = '11918'	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND 
				APP_VERSION_ID = @APP_VERSION_ID and MULTI_CAR ='11919' 
				AND (
					ISNULL(VEHICLE_TYPE_PER,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER) 
					AND
					ISNULL(VEHICLE_TYPE_COM,0) NOT IN (@TRAILER)
					)
				AND ISNULL(IS_ACTIVE,'') = 'Y'
			END
		END
	
	

  END            
      
END     

--go
--exec PROC_DELETEVEHICLE 197,109,1,5,'APP'
--rollback
 
  
--select VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,* from APP_VEHICLES 
--where customer_id = 1351 and app_id = 35 and app_version_id =1




GO

