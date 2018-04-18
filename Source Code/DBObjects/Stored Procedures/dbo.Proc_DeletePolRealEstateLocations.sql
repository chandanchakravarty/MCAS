IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolRealEstateLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolRealEstateLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
         
Proc Name       : dbo.Proc_DeletePolRealEstateLocations         
Created by      : Swastika Gaur          
Date            : 28th Apr'06          
Purpose         : Delete policy level real estate location details.          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/ 
--drop proc dbo.Proc_DeletePolRealEstateLocations        
CREATE PROC dbo.Proc_DeletePolRealEstateLocations          
(          
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID smallint,    
 @LOCATION_ID smallint   
  
)          
AS          
BEGIN     
     
 IF EXISTS
	(
		SELECT CUSTOMER_ID FROM POL_UMBRELLA_DWELLINGS_INFO
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		POLICY_ID =  @POLICY_ID AND
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND
		LOCATION_ID =  @LOCATION_ID
	)  
	BEGIN
		RETURN -1
	END
 ELSE
	
	BEGIN
		 -- Delete Location Info
		 DELETE FROM POL_UMBRELLA_REAL_ESTATE_LOCATION
		 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID 
 
	END
END         



GO

