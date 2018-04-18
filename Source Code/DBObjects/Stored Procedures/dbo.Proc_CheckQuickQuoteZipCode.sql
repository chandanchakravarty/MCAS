IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckQuickQuoteZipCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckQuickQuoteZipCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckQuickQuoteZipCode
Created by         : Deepak Gupta
Date               : 11/18/2005
Purpose            : To Check Zip Code against lob and state
Revison History    :
Modified 	   : Praveen kasana  	
Used In            : Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_CheckQuickQuoteZipCode 'autop','indiana','','01/21/2008'
CREATE   PROCEDURE dbo.Proc_CheckQuickQuoteZipCode
	@LOB 		NVARCHAR(20)='',
	@STATE 		NVARCHAR(50)='',
	@ZIP 		NVARCHAR(20)='',
	@POL_EFFECTIVE_DATE DATETIME = null
AS
BEGIN



IF(@LOB= 'REDW')
BEGIN
	IF(@ZIP = '')
	BEGIN
		SELECT '-1'
	END
	ELSE
	BEGIN
	
		SELECT TERR FROM MNT_TERRITORY_CODES 
		INNER JOIN MNT_COUNTRY_STATE_LIST ON STATE_ID=STATE
		INNER JOIN MNT_LOB_MASTER ON LOB_ID=LOBID
		WHERE UPPER(STATE_NAME) = @STATE AND LOB_CODE = @LOB AND ZIP=@ZIP
	END
END
ELSE --GrandFathered Option for AUTO/CYCL/HOME
BEGIN
	IF(@LOB= 'AUTOP')--Territory only for Personal Auto
	BEGIN
		IF(@ZIP = '')
		BEGIN
			SELECT '-1'
		END
		ELSE
		BEGIN
		
			SELECT TERR FROM MNT_TERRITORY_CODES 
			INNER JOIN MNT_COUNTRY_STATE_LIST ON STATE_ID=STATE
			INNER JOIN MNT_LOB_MASTER ON LOB_ID=LOBID
			WHERE UPPER(STATE_NAME) = @STATE AND LOB_CODE = @LOB AND ZIP=@ZIP
			AND @POL_EFFECTIVE_DATE BETWEEN MNT_TERRITORY_CODES.EFFECTIVE_FROM_DATE 
			AND ISNULL(MNT_TERRITORY_CODES.EFFECTIVE_TO_DATE,'3000-03-16')
			AND ISNULL(AUTO_VEHICLE_TYPE,'') <> 'COM'
		END
	END
	ELSE
	BEGIN
		IF(@ZIP = '')
		BEGIN
			SELECT '-1'
		END
		ELSE
		BEGIN
			SELECT TERR FROM MNT_TERRITORY_CODES 
			INNER JOIN MNT_COUNTRY_STATE_LIST ON STATE_ID=STATE
			INNER JOIN MNT_LOB_MASTER ON LOB_ID=LOBID
			WHERE UPPER(STATE_NAME) = @STATE AND LOB_CODE = @LOB AND ZIP=@ZIP
			AND @POL_EFFECTIVE_DATE BETWEEN MNT_TERRITORY_CODES.EFFECTIVE_FROM_DATE 
			AND ISNULL(MNT_TERRITORY_CODES.EFFECTIVE_TO_DATE,'3000-03-16')
		END
	END
END


END






















GO

