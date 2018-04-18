IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_UserLicenseList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_UserLicenseList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE dbo.rpt_UserLicenseList 
	--@UserID INTEGER
	@UserID VARCHAR(8000)
AS
	BEGIN
		DECLARE @sql VARCHAR(8000)
		
			Select @sql = 'SELECT LTRIM(RTRIM(MNT_USER_LIST.USER_FNAME)) + '' '' + LTRIM(RTRIM(MNT_USER_LIST.USER_LNAME)) + '' ('' + LTRIM(RTRIM(MNT_USER_LIST.USER_LOGIN_ID)) + '')'' AS USER_NAME, MNT_USER_LIST.USER_ADD1, 
			MNT_USER_LIST.USER_ADD2, MNT_USER_LIST.USER_CITY, MNT_COUNTRY_STATE_LIST.STATE_CODE, MNT_USER_LIST.USER_ZIP, 
			MNT_USER_LIST.DRIVER_DRIV_TYPE, MNT_USER_LIST.DATE_EXPIRY, (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = MNT_USER_LIST.LICENSE_STATUS) AS LICENSE_STATUS, MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME, MNT_USER_TYPES.USER_TYPE_DESC
			FROM MNT_USER_LIST INNER JOIN
			MNT_USER_TYPES ON MNT_USER_TYPES.USER_TYPE_ID = MNT_USER_LIST.USER_TYPE_ID  INNER JOIN
			MNT_AGENCY_LIST ON MNT_AGENCY_LIST.AGENCY_CODE = MNT_USER_LIST.USER_SYSTEM_ID INNER JOIN
			MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.COUNTRY_ID = MNT_USER_LIST.COUNTRY AND 
			MNT_COUNTRY_STATE_LIST.STATE_ID = MNT_USER_LIST.USER_STATE '
		IF (@UserID <> '0')
		BEGIN
			--SELECT @sql=@sql + ' WHERE MNT_USER_LIST.USER_ID = ' + CAST(@UserID AS VARCHAR(10))
			SELECT @sql=@sql + ' WHERE MNT_USER_LIST.USER_ID IN (' +  @UserID + ')' 
		END
		END	


--PRINT(@sql) 
EXEC (@sql)


GO

