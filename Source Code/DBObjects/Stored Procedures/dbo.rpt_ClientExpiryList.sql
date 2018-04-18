IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ClientExpiryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ClientExpiryList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc  dbo.rpt_ClientExpiryList
--go

CREATE PROCEDURE [dbo].[rpt_ClientExpiryList]
	@NameFormat CHAR(1) = 1,
	@NameAddress CHAR(1) = 1,
	@intCLIENTACTIVE CHAR(1)='Y',
	@intCLIENT_ID varchar(8000)='0',
	@intAgencyId varchar(8000)='0',
	@ClientStates Varchar(8000) = '-99',
	@ClientZip VARCHAR(50) = Null
AS

DECLARE @sql VARCHAR(8000),
	@INDEX INTEGER
	BEGIN

if @intCLIENT_ID='' or @intCLIENT_ID is null
	set @intCLIENT_ID='0'
if @intAgencyId='' or @intAgencyId is null
	set @intAgencyId='0'

		Select @sql = 'SELECT MNT_AGENCY_LIST.AGENCY_ID, MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME AS AGENCYNAME  , CLT_CUSTOMER_LIST.CUSTOMER_CODE, '
		IF (@NameFormat = '0')  
		BEGIN	--First Name Last Name
			Select @sql = @sql + ' 	LTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME  + '' '','''') + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME + '' '','''') + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'''')) AS CUSTOMERNAME '
		END
		ELSE
		BEGIN	--Last Name, First Name
			Select @sql = @sql + ' 	LTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME + '', '','''') + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME  + '' '','''') + ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'''')) AS CUSTOMERNAME '
		END

	        Select @sql = @sql + ' , CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS1, CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS2, 
                	      CLT_CUSTOMER_LIST.CUSTOMER_CITY, MNT_COUNTRY_STATE_LIST.STATE_CODE, CLT_CUSTOMER_LIST.CUSTOMER_ZIP, 
	                      CLT_CUSTOMER_LIST.CUSTOMER_BUSINESS_PHONE, CLT_CUSTOMER_LIST.CUSTOMER_EXT, CLT_CUSTOMER_LIST.CUSTOMER_HOME_PHONE, 
        	              CLT_CUSTOMER_LIST.CUSTOMER_MOBILE, CLT_CUSTOMER_LIST.CUSTOMER_FAX, CLT_CUSTOMER_LIST.CUSTOMER_Email,' + @NameAddress + ' as NAMEADDRESS
				FROM      CLT_CUSTOMER_LIST 
						  INNER JOIN MNT_AGENCY_LIST ON CLT_CUSTOMER_LIST.CUSTOMER_AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID 
						  INNER JOIN MNT_COUNTRY_STATE_LIST ON CLT_CUSTOMER_LIST.CUSTOMER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID 
						  AND CLT_CUSTOMER_LIST.CUSTOMER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID '
		SELECT @sql = @sql + ' WHERE 1 = 1 '

		IF (@intAgencyId <> '0')
		BEGIN
			SELECT @sql=@sql + ' AND MNT_AGENCY_LIST.AGENCY_ID IN(' + @intAgencyId +')'
		END
		IF (@intCLIENT_ID<>'0')
		BEGIN
			SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.CUSTOMER_ID In(' + @intCLIENT_ID + ')'
		END
		ELSE
		BEGIN
			IF @intClientActive='Y'
			BEGIN
				SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.IS_ACTIVE= ''Y'' '
			END
			ELSE IF @intClientActive='N'
			BEGIN
				SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.IS_ACTIVE= ''N'' '
			END
		END

		IF ( @ClientStates <> '-99')
		BEGIN
			SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.CUSTOMER_STATE IN (' + @ClientStates + ')'
		END

		IF (ISNULL(@ClientZip,'0') <> '0' AND @ClientZip<>'' )
		BEGIN			
		--SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.CUSTOMER_ZIP = ''' + @ClientZip + '''  '
		--Like Operator  Added For Itrack Issue #6208.
		SELECT @sql=@sql + ' AND CLT_CUSTOMER_LIST.CUSTOMER_ZIP like ''' + @ClientZip + '%''  '
		END
		SELECT @sql=@sql + ' ORDER BY AGENCY_DISPLAY_NAME, CUSTOMERNAME, CUSTOMER_ADDRESS1'
	END	


--PRINT(@sql) 
EXEC (@sql)


--go
--exec rpt_ClientExpiryList 1,1,'Y','0','0','-99','49301'
--rollback tran



GO

