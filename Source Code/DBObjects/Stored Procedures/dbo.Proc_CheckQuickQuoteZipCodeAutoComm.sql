IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckQuickQuoteZipCodeAutoComm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckQuickQuoteZipCodeAutoComm]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckQuickQuoteZipCodeAutoComm
Created by         : Praveen Kasana
Date               : 12/12/2005
Purpose            : To Check Zip Code against lob and state for Auto Commercial Type (Indaian)
		     Michnagan Data no available for fething ZIp code for AUto P	
Revison History    :
Modified 	   : Praveen kasana  	
Used In            : Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_CheckQuickQuoteZipCodeAutoComm
CREATE   PROCEDURE dbo.Proc_CheckQuickQuoteZipCodeAutoComm
	@LOB 		NVARCHAR(20)='',
	@STATE 		NVARCHAR(50)='',
	@ZIP 		NVARCHAR(20)='',
	@POL_EFFECTIVE_DATE DATETIME = null
AS
BEGIN


--GrandFathered Option for AUTO/CYCL/HOME
	SELECT TERR FROM MNT_TERRITORY_CODES 
	INNER JOIN MNT_COUNTRY_STATE_LIST ON STATE_ID=STATE
	INNER JOIN MNT_LOB_MASTER ON LOB_ID=LOBID
	WHERE UPPER(STATE_NAME) = @STATE AND LOB_CODE = @LOB AND ZIP=@ZIP
	AND @POL_EFFECTIVE_DATE BETWEEN MNT_TERRITORY_CODES.EFFECTIVE_FROM_DATE 
	AND ISNULL(MNT_TERRITORY_CODES.EFFECTIVE_TO_DATE,'3000-03-16')
	AND AUTO_VEHICLE_TYPE = 'COM'



END




















GO

