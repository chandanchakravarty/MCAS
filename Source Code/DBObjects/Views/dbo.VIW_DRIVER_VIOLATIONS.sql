IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VIW_DRIVER_VIOLATIONS]'))
DROP VIEW [dbo].[VIW_DRIVER_VIOLATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
View Name          	: dbo.VIW_DRIVER_VIOLATIONS
Created by         	: Ashwani
Date               	: 13 Mar 2006
Purpose            	: 
Revison History 	:
Used In             :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------
DROP VIEW VIW_DRIVER_VIOLATIONS
*/
CREATE VIEW dbo.VIW_DRIVER_VIOLATIONS
AS
SELECT VIOLATION_ID, MVR_POINTS as WOLVERINE_VIOLATIONS,VIOLATION_CODE
FROM MNT_VIOLATIONS






GO

