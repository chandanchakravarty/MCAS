IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSubLocationsForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSubLocationsForApplication]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO







/*
----------------------------------------------------------    
Proc Name       : dbo.Proc_GetSubLocationsForApplication
Created by      : Pradeep  
Date            : 13 May,2005    
Purpose         : Selects the sub-locations for a locatoin
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------   
*/

CREATE      PROCEDURE Proc_GetSubLocationsForApplication
(
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID smallint,
	@LOCATION_ID smallint
)

As

SELECT ASL.SUB_LOC_ID,
       CONVERT(VarChar(20),ASL.SUB_LOC_NUMBER) + '-' + isnull( ASL.SUB_LOC_DESC,'') as SUB_LOC_DESC
FROM APP_SUB_LOCATIONS ASL
INNER JOIN APP_LOCATIONS AL ON
	ASL.LOCATION_ID = AL.LOCATION_ID
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND
      AL.APP_ID = @APP_ID AND
      AL.APP_VERSION_ID = @APP_VERSION_ID AND
      AL.LOCATION_ID = 	@LOCATION_ID


GO

