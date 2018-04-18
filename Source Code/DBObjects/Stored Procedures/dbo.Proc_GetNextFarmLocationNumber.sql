IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextFarmLocationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextFarmLocationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name   : dbo.Proc_GetNextFarmLocationNumber       
Created by  : Ravindra Gupta
Date        : 03-13-2006
Purpose     : Returns the next Farm Location Number        
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  

CREATE PROCEDURE Proc_GetNextFarmLocationNumber
(
	
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt
)

As

DECLARE @MAX BigInt
	
	SELECT @MAX = ISNULL(MAX(LOCATION_NUMBER),0)
	FROM APP_UMBRELLA_FARM_INFO
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND 
		APP_VERSION_ID = @APP_VERSION_ID 
	
	
	IF @MAX = 2147483647
	BEGIN
		SELECT -1
		RETURN		
	END

	SELECT @MAX + 1



GO

