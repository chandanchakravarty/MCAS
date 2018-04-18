IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_OTHER_LOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_OTHER_LOCATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetXMLAPP_OTHER_LOCATIONS
Created by      : Swastika Gaur
Date            : 13th jun'06
Purpose         : Selects the location for an application (Homeowner)
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------   
*/
-- drop proc dbo.Proc_GetXMLAPP_OTHER_LOCATIONS
CREATE PROCEDURE dbo.Proc_GetXMLAPP_OTHER_LOCATIONS
(
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID smallint,
	@LOCATION_ID	int
)
As
SELECT *
FROM APP_OTHER_LOCATIONS AL (NOLOCK)
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON
	
	AL.LOC_STATE = SL.STATE_ID
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND
      AL.APP_ID = @APP_ID AND
      AL.APP_VERSION_ID = @APP_VERSION_ID		AND
	AL.LOCATIOn_ID = @LOCATION_ID

















GO

