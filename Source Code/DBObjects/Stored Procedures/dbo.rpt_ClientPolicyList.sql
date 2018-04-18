IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ClientPolicyList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ClientPolicyList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.rpt_ClientPolicyList
--go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- DROP PROCEDURE dbo.rpt_ClientPolicyList 1,'', '', '', '', '', '', '0', '0', '0', '', '-99'
CREATE PROCEDURE [dbo].[rpt_ClientPolicyList]
	@NameAddress CHAR(1) = 1,
	@InceptionDateStart Datetime = '',
	@InceptionDateEnd Datetime = '',
	@EffectiveDateStart Datetime = '',
	@EffectiveDateEnd Datetime = '',
	@ExpirationDateStart Datetime = '',
	@ExpirationDateEnd Datetime = '',
	@intCLIENT_ID varchar(8000)='0',	
	@intBrokerId varchar(8000)='0',
	@UnderWriter varchar(8000)='0',
	@LOB varchar(8000) = '',
	@BillType varchar(8000) = '-99'
AS

DECLARE @sql VARCHAR(8000),
	@INDEX INTEGER,
	@TEMP VARCHAR(8000),
	@TEMP1 VARCHAR(8000)

	BEGIN
if @intCLIENT_ID = '' or @intCLIENT_ID is null  
 set @intCLIENT_ID = '0'  
if @intBrokerId = '' or @intBrokerId is null  
 set @intBrokerId = '0'  

		Select @sql = 'SELECT POL_CUSTOMER_POLICY_LIST.AGENCY_ID, iSNULL(AGENCY_DISPLAY_NAME,'''') AS AGENCYNAME, POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID,POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER, POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION, MNT_LOB_MASTER.LOB_DESC,
 
							   (SELECT SUB_LOB_DESC FROM MNT_SUB_LOB_MASTER A 
							   WHERE A.SUB_LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_SUBLOB AND A.LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_LOB) AS SUB_LOB_DESC, 
                               CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_CODE, 
                               CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS1, CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS2, CLT_CUSTOMER_LIST.CUSTOMER_CITY, MNT_COUNTRY_STATE_LIST.STATE_CODE, 
                               CLT_CUSTOMER_LIST.CUSTOMER_ZIP, CLT_CUSTOMER_LIST.CUSTOMER_Email, POL_CUSTOMER_POLICY_LIST.APP_INCEPTION_DATE, POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE, 
                               POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE, POL_CUSTOMER_POLICY_LIST.BILL_TYPE, POL_CUSTOMER_POLICY_LIST.RECEIVED_PRMIUM, POL_CUSTOMER_POLICY_LIST.APP_TERMS, 
                               CLT_CUSTOMER_LIST.CUSTOMER_CONTACT_NAME, (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES A WHERE A.LOOKUP_ID = 940 AND A.LOOKUP_VALUE_CODE = POL_CUSTOMER_POLICY_LIST.BILL_TYPE) AS BILL_DESC, 
							   ' + @NameAddress + ' AS NAMEADDRESS, POL_POLICY_STATUS_MASTER.POLICY_DESCRIPTION 
                               FROM POL_CUSTOMER_POLICY_LIST INNER JOIN
                               MNT_AGENCY_LIST ON POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID INNER JOIN  
							   CLT_CUSTOMER_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID INNER JOIN
                               MNT_COUNTRY_STATE_LIST ON MNT_COUNTRY_STATE_LIST.COUNTRY_ID = CLT_CUSTOMER_LIST.CUSTOMER_COUNTRY AND 
                               MNT_COUNTRY_STATE_LIST.STATE_ID = CLT_CUSTOMER_LIST.CUSTOMER_STATE INNER JOIN 
                               MNT_LOB_MASTER ON POL_CUSTOMER_POLICY_LIST.POLICY_LOB = MNT_LOB_MASTER.LOB_ID INNER JOIN
                               POL_POLICY_STATUS_MASTER ON POL_CUSTOMER_POLICY_LIST.POLICY_STATUS = POL_POLICY_STATUS_MASTER.POLICY_STATUS_CODE '
		SELECT @sql = @sql + ' WHERE 1 = 1 AND POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION = (SELECT TOP 1 max(POLICY_DISP_VERSION) FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)      
    WHERE POL.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID AND POL.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID       
    AND POL.POLICY_NUMBER = POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER)'

---POLICY_DISP_VERSION Condition Added FOR Itrack Issue #6294.     

		IF (@InceptionDateStart<>'' )
		BEGIN
			SELECT @sql = @sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_INCEPTION_DATE >= '''  + CONVERT(VARCHAR, @InceptionDateStart,101) + ''''
		END

		IF (@InceptionDateEnd<>'' )
		BEGIN
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_INCEPTION_DATE <= ''' + CONVERT(VARCHAR, @InceptionDateEnd,101) + ''''
		END

		IF (@EffectiveDateStart<>'' )
		BEGIN
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE >= ''' + CONVERT(VARCHAR, @EffectiveDateStart,101) + ''''
		END

		IF (@EffectiveDateEnd<>'' )
		BEGIN
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE <= ''' + CONVERT(VARCHAR, @EffectiveDateEnd,101) + ''''
		END

		IF (@ExpirationDateStart<>'' )
		BEGIN
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE >= ''' + CONVERT(VARCHAR, @ExpirationDateStart,101) + ''''
		END

		IF (@ExpirationDateEnd<>'' )
		BEGIN
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE <= ''' + CONVERT(VARCHAR, @ExpirationDateEnd,101) + ''''
		END

		--IF (@intCLIENT_ID<>0)
		IF (@intCLIENT_ID<>'0')
		BEGIN
			--SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=' + CAST(@intCLIENT_ID AS VARCHAR(10))
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID IN(' + @intCLIENT_ID + ')'
		END

		--IF (@intBrokerId <> 0)
		IF (@intBrokerId <> '0')
		BEGIN
			--SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_AGENCY_ID = ' + CAST(@intBrokerId AS VARCHAR(10))
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID IN (' + @intBrokerId + ')'
		END

		--IF (@UnderWriter <> 0)
		IF (@UnderWriter <> '0')

		BEGIN
			--SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.UNDERWRITER = ' + CAST(@UnderWriter AS VARCHAR(10))
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.UNDERWRITER IN (' + @UnderWriter + ')'
		END

		IF (ISNULL(@LOB,'0') <> '0' AND @LOB<>'' )
		BEGIN
			--SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.POLICY_LOB = ' + CAST(@LOB AS VARCHAR(10))
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.POLICY_LOB IN (' + @LOB + ')'
		END

		IF (@BillType <> '-99')
		BEGIN
			SELECT @TEMP = ' '
			SELECT  @TEMP1 = ' '
			SELECT @TEMP=@BillType
			WHILE 1=1
			BEGIN
				SELECT @INDEX=CHARINDEX(',',@TEMP)
			IF @INDEX=0
			BEGIN
				SELECT @TEMP1  = @TEMP1 +  '''' + @TEMP + ''''
				BREAK
			END
			SELECT @TEMP1  = @TEMP1 + '''' +  LEFT(@TEMP,@INDEX-1) + ''','
			SELECT @TEMP=SUBSTRING(@TEMP,@INDEX+1,LEN(@TEMP)-@INDEX)
			END	
	
			SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.BILL_TYPE IN (' + @TEMP1 + ')'
			--SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.BILL_TYPE IN (' + @BillType + ')'
		END

		SELECT @sql=@sql + 'ORDER BY AGENCY_DISPLAY_NAME, POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID,   
							MNT_LOB_MASTER.LOB_DESC, POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER' 
	END	

--PRINT(@sql) 
EXEC (@sql)
--go
--exec rpt_ClientPolicyList 1,'08/25/2009', '08/25/2009', '', '', '', '', '0', '0', '0', '', '-99'
--rollback tran 


GO

