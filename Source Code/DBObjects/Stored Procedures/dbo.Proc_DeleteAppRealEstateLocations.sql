IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAppRealEstateLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAppRealEstateLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
         
Proc Name       : dbo.Proc_DeleteAppRealEstateLocations         
Created by      : Swastika Gaur          
Date            : 28th Apr'06          
Purpose         : Delete application level real estate location details.          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/ 
--drop proc dbo.Proc_DeleteAppRealEstateLocations        
CREATE PROC dbo.Proc_DeleteAppRealEstateLocations          
(          
 @CUSTOMER_ID INT,    
 @APP_ID INT,    
 @APP_VERSION_ID smallint,    
 @LOCATION_ID smallint   
  
)          
AS          
BEGIN     
     
 IF EXISTS
	(
		SELECT CUSTOMER_ID FROM APP_UMBRELLA_DWELLINGS_INFO
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID =  @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID =  @LOCATION_ID
	)  
	BEGIN
		RETURN -1
	END
 ELSE
	
	BEGIN
		 -- Delete Location Info
		 DELETE FROM APP_UMBRELLA_REAL_ESTATE_LOCATION
		 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND LOCATION_ID=@LOCATION_ID 
 
	END
END    



GO

