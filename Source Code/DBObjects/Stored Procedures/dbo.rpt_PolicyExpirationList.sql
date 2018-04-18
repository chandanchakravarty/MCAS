IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_PolicyExpirationList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_PolicyExpirationList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran        
--DROP PROCEDURE dbo.rpt_PolicyExpirationList        
--go        
     
CREATE PROCEDURE [dbo].[rpt_PolicyExpirationList]        
 @NameAddress CHAR(1) = 1,        
 @ExpirationDateStart Datetime = '',        
 @ExpirationDateEnd Datetime = '',        
 @intCLIENT_ID varchar(8000) = '0',        
 @intBrokerId varchar(8000) = '0',        
 @UnderWriter varchar(8000) = '0',        
 @LOB varchar(8000) = '',        
 @BillType varchar(8000) = '-99',        
 @PolicyStatus varchar(8000)='' ,         
 --@ORDERBY varchar(100)=null --Commented for Itrack Issue 6183 on 30 July 2009      
 @ORDERBY varchar(100)                         
AS        
        
DECLARE @sql VARCHAR(8000),        
 @INDEX INTEGER,        
 @TEMP VARCHAR(8000),        
 @TEMP1 VARCHAR(8000)        
        
 BEGIN        
        
if @ExpirationDateStart = '' or @ExpirationDateStart is null        
 set @ExpirationDateStart = ''        
if @ExpirationDateEnd = '' or @ExpirationDateEnd is null        
 set @ExpirationDateEnd = ''        
if @intCLIENT_ID = '' or @intCLIENT_ID is null        
 set @intCLIENT_ID = '0'        
if @intBrokerId = '' or @intBrokerId is null        
 set @intBrokerId = '0'        
if @UnderWriter='' or @UnderWriter='0' or @UnderWriter is null        
 set @UnderWriter = '0'        
if @LOB='' or @LOB is null        
 set @LOB = ''        
if @BillType='' or @BillType is null        
 set @BillType = '-99'        
if @PolicyStatus='' or @PolicyStatus is null        
 set @PolicyStatus = ''  

--Case Added Fior Itrack Issue #6419.
 if(@PolicyStatus = '')
BEGIN          
  Select @sql = 'SELECT POL_CUSTOMER_POLICY_LIST.AGENCY_ID, iSNULL(AGENCY_DISPLAY_NAME,'''') AS AGENCYNAME, 
				POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID, POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER, 
				POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION, MNT_LOB_MASTER.LOB_DESC, 
				(
					SELECT SUB_LOB_DESC FROM MNT_SUB_LOB_MASTER A         
					WHERE A.SUB_LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_SUBLOB 
					AND A.LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_LOB
				) AS SUB_LOB_DESC,         
                CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, 
				CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_CODE,        
                isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'''') + '' '' +  CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + '' '' + isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'''') as CUSTOMER_NAME1,                                             
				CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS1, CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS2, 
				CLT_CUSTOMER_LIST.CUSTOMER_CITY, MNT_COUNTRY_STATE_LIST.STATE_CODE,         
                CLT_CUSTOMER_LIST.CUSTOMER_ZIP, CLT_CUSTOMER_LIST.CUSTOMER_Email, 
				POL_CUSTOMER_POLICY_LIST.APP_INCEPTION_DATE, POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE,         
                POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE, POL_CUSTOMER_POLICY_LIST.BILL_TYPE as BILL_TYPE , 
				POL_CUSTOMER_POLICY_LIST.RECEIVED_PRMIUM, POL_CUSTOMER_POLICY_LIST.APP_TERMS,         
                CLT_CUSTOMER_LIST.CUSTOMER_CONTACT_NAME, 
				(
					SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES A WHERE A.LOOKUP_ID = 940 
					AND A.LOOKUP_VALUE_CODE = POL_CUSTOMER_POLICY_LIST.BILL_TYPE
				) AS BILL_DESC, '         
				+ @NameAddress + ' AS NAMEADDRESS, POL_POLICY_STATUS_MASTER.POLICY_DESCRIPTION as POLICY_DESCRIPTION, 
				POL_CUSTOMER_POLICY_LIST.UNDERWRITER as UNDERWRITER,POL_CUSTOMER_POLICY_LIST.POLICY_STATUS as POLICY_STATUS          
                FROM POL_CUSTOMER_POLICY_LIST 
				INNER JOIN MNT_AGENCY_LIST 
					ON POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID 
				INNER JOIN CLT_CUSTOMER_LIST 
					ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID 
				INNER JOIN MNT_COUNTRY_STATE_LIST 
					ON MNT_COUNTRY_STATE_LIST.COUNTRY_ID = CLT_CUSTOMER_LIST.CUSTOMER_COUNTRY 
					AND MNT_COUNTRY_STATE_LIST.STATE_ID = CLT_CUSTOMER_LIST.CUSTOMER_STATE 
				INNER JOIN MNT_LOB_MASTER 
					ON POL_CUSTOMER_POLICY_LIST.POLICY_LOB = MNT_LOB_MASTER.LOB_ID 
				INNER JOIN POL_POLICY_STATUS_MASTER 
					ON POL_CUSTOMER_POLICY_LIST.POLICY_STATUS = POL_POLICY_STATUS_MASTER.POLICY_STATUS_CODE '        
  SELECT @sql = @sql + ' WHERE 1 = 1  
				AND POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION = 
				(
					SELECT TOP 1 max(POLICY_DISP_VERSION) FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)      
					WHERE POL.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID 
					AND POL.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID       
					AND POL.POLICY_NUMBER = POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER
				) '  
END

 ELSE

BEGIN      
    Select @sql = 'SELECT POL_CUSTOMER_POLICY_LIST.AGENCY_ID, iSNULL(AGENCY_DISPLAY_NAME,'''') AS AGENCYNAME, 
				POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID, POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER, 
				POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION, MNT_LOB_MASTER.LOB_DESC, 
				(
					SELECT SUB_LOB_DESC FROM MNT_SUB_LOB_MASTER A         
					WHERE A.SUB_LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_SUBLOB 
					AND A.LOB_ID = POL_CUSTOMER_POLICY_LIST.POLICY_LOB
				) AS SUB_LOB_DESC,         
                CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, 
				CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, CLT_CUSTOMER_LIST.CUSTOMER_CODE,        
                isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'''') + '' '' +  CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME + '' '' + isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'''') as CUSTOMER_NAME1,                                            
				CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS1, CLT_CUSTOMER_LIST.CUSTOMER_ADDRESS2, 
				CLT_CUSTOMER_LIST.CUSTOMER_CITY, MNT_COUNTRY_STATE_LIST.STATE_CODE,         
                CLT_CUSTOMER_LIST.CUSTOMER_ZIP, CLT_CUSTOMER_LIST.CUSTOMER_Email, 
				POL_CUSTOMER_POLICY_LIST.APP_INCEPTION_DATE, POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE,         
                POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE, POL_CUSTOMER_POLICY_LIST.BILL_TYPE as BILL_TYPE , 
				POL_CUSTOMER_POLICY_LIST.RECEIVED_PRMIUM, POL_CUSTOMER_POLICY_LIST.APP_TERMS,         
                CLT_CUSTOMER_LIST.CUSTOMER_CONTACT_NAME, 
				(
					SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES A WHERE A.LOOKUP_ID = 940 
					AND A.LOOKUP_VALUE_CODE = POL_CUSTOMER_POLICY_LIST.BILL_TYPE
				) AS BILL_DESC, '         
				+ @NameAddress + ' AS NAMEADDRESS, POL_POLICY_STATUS_MASTER.POLICY_DESCRIPTION as POLICY_DESCRIPTION, 
				POL_CUSTOMER_POLICY_LIST.UNDERWRITER as UNDERWRITER,POL_CUSTOMER_POLICY_LIST.POLICY_STATUS as POLICY_STATUS          
                FROM POL_CUSTOMER_POLICY_LIST 
				INNER JOIN MNT_AGENCY_LIST 
					ON POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID 
				INNER JOIN CLT_CUSTOMER_LIST 
					ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID 
				INNER JOIN MNT_COUNTRY_STATE_LIST 
					ON MNT_COUNTRY_STATE_LIST.COUNTRY_ID = CLT_CUSTOMER_LIST.CUSTOMER_COUNTRY 
					AND MNT_COUNTRY_STATE_LIST.STATE_ID = CLT_CUSTOMER_LIST.CUSTOMER_STATE 
				INNER JOIN MNT_LOB_MASTER 
					ON POL_CUSTOMER_POLICY_LIST.POLICY_LOB = MNT_LOB_MASTER.LOB_ID 
				INNER JOIN POL_POLICY_STATUS_MASTER 
					ON POL_CUSTOMER_POLICY_LIST.POLICY_STATUS = POL_POLICY_STATUS_MASTER.POLICY_STATUS_CODE '        
  SELECT @sql = @sql + ' WHERE 1 = 1  
				AND POL_CUSTOMER_POLICY_LIST.POLICY_DISP_VERSION  
                 IN(
					(SELECT max(POLICY_DISP_VERSION) FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)      
					WHERE POL.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID AND 
					POL.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID       
					AND POL.POLICY_NUMBER = POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER
					group by policy_status) 
					)'

END

      
  ---POLICY_DISP_VERSION Condition Added FOR Itrack Issue #6212.      
  IF (@ExpirationDateStart<>'' and  @ExpirationDateStart is not null)        
  BEGIN        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE >= ''' + CONVERT(VARCHAR, @ExpirationDateStart,101) + ''''        
  END        
        
  IF (@ExpirationDateEnd<>'' and @ExpirationDateEnd is not null )        
  BEGIN        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.APP_EXPIRATION_DATE <= ''' + CONVERT(VARCHAR, @ExpirationDateEnd,101) + ''''        
  END        
        
  IF (@intCLIENT_ID <> '0')        
  BEGIN        
   --SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=' + CAST(@intCLIENT_ID AS VARCHAR(10))        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID IN (' + @intCLIENT_ID + ')'        
  END        
        
  IF (@intBrokerId <> '0')        
  BEGIN        
   --SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.CUSTOMER_AGENCY_ID = ' + CAST(@intBrokerId AS VARCHAR(10))        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID IN (' + @intBrokerId + ')'        
  END        
        
  IF (@UnderWriter <> '0')        
  BEGIN        
   --SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.UNDERWRITER = ' + CAST(@UnderWriter AS VARCHAR(10))        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.UNDERWRITER IN (' + @UnderWriter + ')'        
  END        
        
  IF (ISNULL(@LOB,'0') <> '0' AND @LOB<>'' )        
  BEGIN        
   --SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.POLICY_LOB = ' + CAST(@LOB AS VARCHAR(10))        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.POLICY_LOB In (' + @LOB + ')'        
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
         
   --SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.BILL_TYPE IN (' + @BillType + ')'        
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.BILL_TYPE IN (' + @TEMP1 + ')'        
        
  END        
         
  IF (@PolicyStatus <> '')        
  BEGIN        
   SELECT @TEMP = ' '        
   SELECT  @TEMP1 = ' '        
   SELECT  @INDEX = ' '        
   SELECT @TEMP=@PolicyStatus        
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
         
   SELECT @sql=@sql + ' AND POL_CUSTOMER_POLICY_LIST.POLICY_STATUS IN (' + @TEMP1 + ')'        
  END        
        
--Added For Itrack Issue  #6059.    
        
 --Commented for Itrack Issue 6183 on 30 July 2009         
--  IF NOT @ORDERBY IS NULL            
--         
--   BEGIN      
--     SET @sql = @sql + ' ORDER BY ' + @ORDERBY                       
--  END         
--      
--    ELSE        
--                 
--    BEGIN         
--       SELECT @SQL=@SQL + 'ORDER BY AGENCY_DISPLAY_NAME, POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID,         
--       MNT_LOB_MASTER.LOB_DESC, POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER'        
--        
--    END          
        
  SET @sql = @sql + ' ORDER BY ' + @ORDERBY         
END        
        
--PRINT(@sql)         
EXEC (@sql)        
        
--go        
--exec rpt_PolicyExpirationList 1, '', '', '', '', '0', '', -99, '' ,'customer_name1'       
--rollback tran        
----    
        
        
  





GO

