IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDF_OtherStructures_Amount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDF_OtherStructures_Amount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetPDF_OtherStructures_Amount
Created by         : Anurag Verma
Date               : 08-Aug-2006
Purpose            : 
Revison History    :
Used In            : Wolverine  

Modified By		: Anurag Verma
Modified On		: 30/03/2007
Purpose			: changing query by changing clause from 1 to 2 for satelite equipment	
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROCEDURE Proc_GetPDF_OtherStructures_Amount
CREATE       PROCEDURE dbo.Proc_GetPDF_OtherStructures_Amount
(
	@CUSTOMERID 		int,
	@POLID 	             	int,
	@VERSIONID 		int,
	@DWELLINGID			int,
	@CALLEDFROM		VARCHAR(20)
)
AS
BEGIN
	IF (@CALLEDFROM='APPLICATION')
	BEGIN
		SELECT SUM(ISNULL(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,0)) ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED
		FROM APP_OTHER_STRUCTURE_DWELLING AOSD 		
		WHERE AOSD.CUSTOMER_ID=@CUSTOMERID AND AOSD.APP_ID=@POLID AND AOSD.APP_VERSION_ID=@VERSIONID 
		AND AOSD.DWELLING_ID=@DWELLINGID AND AOSD.SATELLITE_EQUIPMENT=2
	END
	ELSE IF (@CALLEDFROM='POLICY')
	BEGIN
		SELECT SUM(ISNULL(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,0)) ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED
		FROM POL_OTHER_STRUCTURE_DWELLING AOSD 		
		WHERE AOSD.CUSTOMER_ID=@CUSTOMERID AND AOSD.POLICY_ID=@POLID AND AOSD.POLICY_VERSION_ID=@VERSIONID 
		AND AOSD.DWELLING_ID=@DWELLINGID AND AOSD.SATELLITE_EQUIPMENT=2
	END
END


















GO

