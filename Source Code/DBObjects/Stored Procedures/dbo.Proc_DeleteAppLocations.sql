IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAppLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAppLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeleteAppLocations         
Created by      : Swastika Gaur          
Date            : 5th Apr'06          
Purpose         : Delete application level location details.          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_DeleteAppLocations          
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
		SELECT * FROM APP_DWELLINGS_INFO
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
		 DELETE FROM APP_LOCATIONS
		 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND LOCATION_ID=@LOCATION_ID 
 
	END
END         
      


GO

