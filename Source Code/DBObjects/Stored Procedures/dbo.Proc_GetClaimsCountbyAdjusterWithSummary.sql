IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimsCountbyAdjusterWithSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimsCountbyAdjusterWithSummary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_GetClaimsCountbyAdjusterWithSummary
--go
/*------------------------------------------------------------                    
Proc Name       : dbo.Proc_GetClaimsCountbyAdjusterWithSummary                  
Created by      : Sukhveer Singh                  
Date            : 17-08-2006                  
Purpose         : Get the data for Report named Claims Count by Ajduster with Summary                  
Revison History :                    
Used In        : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
-- DROP PROC dbo.Proc_GetClaimsCountbyAdjusterWithSummary 
 --Proc_GetClaimsCountbyAdjusterWithSummary '1','2009','0','0','0','0','0','0'                 
CREATE PROC dbo.Proc_GetClaimsCountbyAdjusterWithSummary                  
--@DATE_FROM datetime = '',     -- Valid format : mm/dd/yyyy                  
--@DATE_THROUGH datetime = '',        -- Valid format : mm/dd/yyyy            
@MONTH  INT = '',    
@YEAR   INT = '',
@SELECTED_ADJUSTER varchar(8000),               
--@SELECTED_PARTY_TYPES varchar(8000),                  
@SELECTED_CLAIM_STATUS varchar(200) = '',                  
@SELECTED_LOB varchar(8000),            
@FIRST_SORT varchar(50) = '',                  
@SECOND_SORT varchar(50) = ''         
         
/* Variables used to find count for seperater*/                
--@COMMA_POS int = 0,                
--@CNT INT =1,                
--@CHAR_POS INT = 1,                 
                  
/* variables used to seperate parameter values*/                
--@FIRST_PARA varchar(100) = NULL,                
--@SEC_PARA varchar(100) = NULL                
                
                 
AS                   
DECLARE @sql VARCHAR(8000)                  
DECLARE @WHERECLAUSE VARCHAR(8000)                  
                  
BEGIN                    
 DECLARE @COMMA_POS int,                
@CNT INT,                
@CHAR_POS INT ,                 
                  
/* variables used to seperate parameter values*/                
@FIRST_PARA varchar(100),                
@SEC_PARA varchar(100)              

set @COMMA_POS = 0                 
set @CNT = 1                 
set @CHAR_POS = 1                 


while (@CNT <= len(@SELECTED_CLAIM_STATUS))                
BEGIN                
  If (substring(@SELECTED_CLAIM_STATUS,@CHAR_POS,1) = ',')                
 BEGIN                
                
  SELECT @COMMA_POS = @COMMA_POS +1                
 END                
  SELECT @CHAR_POS = @CHAR_POS +1                
  SELECT @CNT = @CNT +1                
END                
   
IF ((@SELECTED_LOB<>'0') AND (@SELECTED_ADJUSTER<>'0'))                  
BEGIN            
   SELECT @SELECTED_LOB = REPLACE(@SELECTED_LOB,',',''',''')
   SELECT @SELECTED_ADJUSTER = REPLACE(@SELECTED_ADJUSTER,',',''',''')                 
   SELECT @WHERECLAUSE=' WHERE MNT_LOB_MASTER.LOB_ID IN (''' +  @SELECTED_LOB  + ''') AND ADJUSTER_ID IN (''' + @SELECTED_ADJUSTER  + ''')'                  
 END  
--ELSE IF ((@SELECTED_LOB <> '0' ) AND (@SELECTED_ADJUSTER ='0' OR @SELECTED_PARTY_TYPES ='0'))                                  
ELSE IF ((@SELECTED_LOB <> '0' ) AND (@SELECTED_ADJUSTER ='0'))                  
BEGIN                  
   SELECT @SELECTED_LOB = REPLACE(@SELECTED_LOB,',',''',''')            
   SELECT @WHERECLAUSE=' WHERE MNT_LOB_MASTER.LOB_ID IN (''' +  @SELECTED_LOB  + ''') '                  
END
ELSE IF ((@SELECTED_LOB = '0' ) AND (@SELECTED_ADJUSTER ='0'))                  
BEGIN  	
   SELECT @WHERECLAUSE='' 
END
ELSE IF (@SELECTED_ADJUSTER <> '0')                  
BEGIN    
  SELECT @SELECTED_ADJUSTER = REPLACE(@SELECTED_ADJUSTER,',',''',''')          
   SELECT @WHERECLAUSE= ' WHERE ADJUSTER_ID IN (''' + @SELECTED_ADJUSTER  + ''')'                  
END  
/*                
COMMENTED BY PAWAN    
    
select @sql = 'SELECT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,LOOKUP.lookup_value_desc,SUB1.MO,SUB2.YTD                  
FROM CLM_CLAIM_INFO INFO INNER JOIN CLM_ADJUSTER ADJ ON INFO.ADJUSTER_CODE = ADJ.ADJUSTER_ID                  
INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID                  
INNER JOIN MNT_LOB_MASTER LMAST ON INFO.LOB_ID = LMAST.LOB_ID                  
INNER JOIN CLM_PARTIES PARTIES ON INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_ID = TYPE.DETAIL_TYPE_ID '                  
*/    
    
/*select @sql = 'SELECT DISTINCT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,MO_OPEN,YTD_OPEN,
'''' as MO_REOPEN,'''' as YTD_REOPEN,'''' as MO_CLOSE,'''' as YTD_CLOSE
FROM CLM_CLAIM_INFO 
INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON CLM_CLAIM_INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID                  
INNER JOIN CLM_ADJUSTER ADJ ON CLM_CLAIM_INFO.ADJUSTER_ID = ADJ.ADJUSTER_ID                  
INNER JOIN MNT_LOB_MASTER LMAST ON CLM_CLAIM_INFO.LOB_ID = LMAST.LOB_ID                  
INNER JOIN CLM_PARTIES PARTIES ON CLM_CLAIM_INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_TYPE_ID = TYPE.DETAIL_TYPE_ID'*/

select @sql = 'SELECT DISTINCT MAIN.ADJUSTER_NAME,MAIN.LOB_DESC,MO_OPEN,YTD_OPEN,'''' as MO_REOPEN,'''' as YTD_REOPEN,'''' as MO_CLOSE,'''' as YTD_CLOSE
FROM ( SELECT MNT_LOB_MASTER.LOB_ID , MNT_LOB_MASTER.LOB_DESC ,ADJ.ADJUSTER_ID , ADJ.ADJUSTER_NAME FROM MNT_LOB_MASTER,CLM_ADJUSTER ADJ'
SELECT @sql = @sql +@WHERECLAUSE +	') MAIN ' 
	     
               
/*IF (@DATE_FROM = '')       
 BEGIN         
 SELECT  @DATE_FROM = GETDATE()                
 END                
                
IF (@DATE_THROUGH = '')                
 BEGIN                
 SELECT  @DATE_THROUGH = GETDATE()                
 END */       
                
                
IF (@SELECTED_CLAIM_STATUS <>'' AND @COMMA_POS = 0 )                  
BEGIN                    
  SELECT @SQL=@SQL + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,LOB_ID,COUNT(CLAIM_ID) AS MO_OPEN FROM CLM_CLAIM_INFO,MNT_LOOKUP_VALUES 
  WHERE CLM_CLAIM_INFO.CLAIM_STATUS= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID AND UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = '''+  @SELECTED_CLAIM_STATUS + ''' '                  
  --   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'  
  --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
 + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + ''''                  
 -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
 --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
 + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + ' group by lob_id,ADJUSTER_ID) SUB1' 
 --Done for Itrack Issue 6342 on 13 Jan 2010
 -- ON CLM_CLAIM_INFO.lob_id = SUB1.lob_id AND SUB1.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID	
 + ' ON MAIN.lob_id = SUB1.lob_id AND SUB1.ADJUSTER_ID=MAIN.ADJUSTER_ID '                  
                   
 SELECT @SQL=@SQL + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,LOB_ID,COUNT(CLAIM_ID) AS YTD_OPEN FROM CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
 WHERE CLM_CLAIM_INFO.CLAIM_STATUS= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID AND UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = '''+  @SELECTED_CLAIM_STATUS + ''' '                  
 --   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')' 
 -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
 --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
 + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + ' group by lob_id,ADJUSTER_ID) SUB2' 

 --Done for Itrack Issue 6342 on 13 Jan 2010
 --ON CLM_CLAIM_INFO.lob_id = SUB2.lob_id  AND SUB2.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
 +' ON MAIN.lob_id = SUB2.lob_id  AND SUB2.ADJUSTER_ID=MAIN.ADJUSTER_ID' 
 --where Lookup.LOOKUP_UNIQUE_ID= ''' + @SELECTED_CLAIM_STATUS + '''' 
--	IF @MONTH <> ''
--	BEGIN
--	--LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
--    SELECT @sql=@sql + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
--		   -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
--		   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + ''''                    
--
--	END
--	IF @YEAR <> ''
--	BEGIN 
--	--LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
--    SELECT @sql=@sql + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' 
--		-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
--		   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + ''''                  
-- 
--	END                
  END                
ELSE IF (@SELECTED_CLAIM_STATUS <>'' AND @COMMA_POS = 1 )                
  BEGIN                
 SELECT @FIRST_PARA = SUBSTRING(@SELECTED_CLAIM_STATUS,1,CHARINDEX( ',', @SELECTED_CLAIM_STATUS)-1)                
 SELECT @SEC_PARA = SUBSTRING(@SELECTED_CLAIM_STATUS,CHARINDEX( ',', @SELECTED_CLAIM_STATUS)+1,LEN(@SELECTED_CLAIM_STATUS))                
                 
 SELECT @sql = ''    
/*    
COMMENTED BY PAWAN    
    
 select @sql = 'SELECT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,LOOKUP.lookup_value_desc,SUB1.MO,SUB2.YTD,                
 SUB3.MO,SUB4.YTD                 
 FROM CLM_CLAIM_INFO INFO INNER JOIN CLM_ADJUSTER ADJ ON INFO.ADJUSTER_CODE = ADJ.ADJUSTER_ID                  
 INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID                  
 INNER JOIN MNT_LOB_MASTER LMAST ON INFO.LOB_ID = LMAST.LOB_ID                  
 INNER JOIN CLM_PARTIES PARTIES ON INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
 INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_ID = TYPE.DETAIL_TYPE_ID '                
*/    
    
 /*select @sql = 'SELECT DISTINCT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,SUB1.MO,SUB2.YTD,                
 SUB3.MO,SUB4.YTD
 FROM CLM_CLAIM_INFO 
 INNER JOIN CLM_ADJUSTER ADJ ON CLM_CLAIM_INFO.ADJUSTER_ID = ADJ.ADJUSTER_ID                  
 INNER JOIN MNT_LOB_MASTER LMAST ON CLM_CLAIM_INFO.LOB_ID = LMAST.LOB_ID                  
 INNER JOIN CLM_PARTIES PARTIES ON CLM_CLAIM_INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
 INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_TYPE_ID = TYPE.DETAIL_TYPE_ID'*/

 select @sql = 'SELECT DISTINCT MAIN.ADJUSTER_NAME,MAIN.LOB_DESC,SUB1.MO,SUB2.YTD,SUB3.MO,SUB4.YTD 
		FROM ( SELECT MNT_LOB_MASTER.LOB_ID , MNT_LOB_MASTER.LOB_DESC ,ADJ.ADJUSTER_ID , ADJ.ADJUSTER_NAME FROM MNT_LOB_MASTER,CLM_ADJUSTER ADJ'
	    SELECT @sql = @sql +@WHERECLAUSE + ') MAIN ' 
    
                
 SELECT @sql=@sql + ' LEFT JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS MO from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = ''' +  @FIRST_PARA + ''' '          
    --    + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'   
    --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
   + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + ''''
   -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.    
  + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + 'group by ADJUSTER_ID,lob_id) SUB1'
  --Done for Itrack Issue 6342 on 13 Jan 2010
 --ON CLM_CLAIM_INFO.lob_id = SUB1.lob_id AND SUB1.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
 +' ON MAIN.lob_id = SUB1.lob_id AND SUB1.ADJUSTER_ID=MAIN.ADJUSTER_ID '                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS YTD from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = ''' +  @FIRST_PARA + ''' '                 
  --  + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.    
   + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + ''''                  
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'group by ADJUSTER_ID,lob_id) SUB2' 
   --Done for Itrack Issue 6342 on 13 Jan 2010
 --ON CLM_CLAIM_INFO.lob_id = SUB2.lob_id AND SUB2.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
 +' ON MAIN.lob_id = SUB2.lob_id AND SUB2.ADJUSTER_ID=MAIN.ADJUSTER_ID '   
       
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS MO from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = ''' + @SEC_PARA + ''' '                  
--    + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.  
   + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
   -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + 'group by ADJUSTER_ID,lob_id) SUB3'
--Done for Itrack Issue 6342 on 13 Jan 2010
--ON CLM_CLAIM_INFO.lob_id = SUB3.lob_id AND SUB3.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
 + ' ON MAIN.lob_id = SUB3.lob_id AND SUB3.ADJUSTER_ID=MAIN.ADJUSTER_ID '   
      
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS YTD from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID) = ''' + + @SEC_PARA + ''' '                  
  --  + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
  --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.       
  + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'group by lob_id,ADJUSTER_ID) SUB4'
--Done for Itrack Issue 6342 on 13 Jan 2010
--ON CLM_CLAIM_INFO.lob_id = SUB4.lob_id AND SUB4.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID 
 + ' ON MAIN.lob_id = SUB4.lob_id AND SUB4.ADJUSTER_ID=MAIN.ADJUSTER_ID '                
 END                
ELSE IF (@SELECTED_CLAIM_STATUS ='' OR @COMMA_POS = 2)                 
  BEGIN 
 SELECT @sql = ''                
/*     
COMMENTED BY PAWAN    
select @sql = 'SELECT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,LOOKUP.lookup_value_desc,SUB1_OPEN.MO_OPEN,SUB2_OPEN.YTD_OPEN,                
 SUB1_REOPEN.MO_REOPEN,SUB2_REOPEN.YTD_REOPEN,SUB1_CLOSE.MO_CLOSE,SUB2_CLOSE.YTD_CLOSE                 
 FROM CLM_CLAIM_INFO INFO INNER JOIN CLM_ADJUSTER ADJ ON INFO.ADJUSTER_CODE = ADJ.ADJUSTER_ID                  
 INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID                  
 INNER JOIN MNT_LOB_MASTER LMAST ON INFO.LOB_ID = LMAST.LOB_ID                  
 INNER JOIN CLM_PARTIES PARTIES ON INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
 INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_ID = TYPE.DETAIL_TYPE_ID '                
  */       
    
-- select @sql = 'SELECT DISTINCT ADJ.ADJUSTER_NAME,LMAST.LOB_DESC,SUB1_OPEN.MO_OPEN,SUB2_OPEN.YTD_OPEN,                
-- SUB1_REOPEN.MO_REOPEN,SUB2_REOPEN.YTD_REOPEN,SUB1_CLOSE.MO_CLOSE,SUB2_CLOSE.YTD_CLOSE
-- FROM CLM_CLAIM_INFO 
-- INNER JOIN CLM_ADJUSTER ADJ ON CLM_CLAIM_INFO.ADJUSTER_ID = ADJ.ADJUSTER_ID     
-- INNER JOIN MNT_LOB_MASTER LMAST ON CLM_CLAIM_INFO.LOB_ID = LMAST.LOB_ID                  
-- INNER JOIN CLM_PARTIES PARTIES ON CLM_CLAIM_INFO.CLAIM_ID = PARTIES.CLAIM_ID                  
-- INNER JOIN CLM_TYPE_DETAIL TYPE ON PARTIES.PARTY_TYPE_ID = TYPE.DETAIL_TYPE_ID   
--  
--'       
    
 select @sql = 'SELECT DISTINCT MAIN.ADJUSTER_NAME,MAIN.LOB_DESC,SUB1_OPEN.MO_OPEN,SUB2_OPEN.YTD_OPEN,SUB1_REOPEN.MO_REOPEN,
 SUB2_REOPEN.YTD_REOPEN,SUB1_CLOSE.MO_CLOSE,SUB2_CLOSE.YTD_CLOSE
 FROM (SELECT MNT_LOB_MASTER.LOB_ID , MNT_LOB_MASTER.LOB_DESC ,ADJ.ADJUSTER_ID , ADJ.ADJUSTER_NAME FROM MNT_LOB_MASTER,CLM_ADJUSTER ADJ'
 SELECT @sql = @sql +@WHERECLAUSE +	') MAIN ' 
         
 SELECT @sql=@sql + ' LEFT JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS MO_OPEN from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Open'' '                  
--    + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.    
 + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
   -- Added by Asfa (01-Feb-2008) - iTrack issue #3019
 + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + 'group by lob_id,ADJUSTER_ID) SUB1_OPEN'
	--ON CLM_CLAIM_INFO.lob_id = SUB1_OPEN.lob_id AND SUB1_OPEN.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID 
 + ' ON MAIN.LOB_ID = SUB1_OPEN.LOB_ID AND SUB1_OPEN.ADJUSTER_ID=MAIN.ADJUSTER_ID'                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS YTD_OPEN from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Open'' '                  
 --   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
  --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.     
  + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
  + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'group by lob_id,ADJUSTER_ID) SUB2_OPEN '
	--ON CLM_CLAIM_INFO.lob_id = SUB2_OPEN.lob_id AND SUB2_OPEN.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
	+ ' ON MAIN.LOB_ID = SUB2_OPEN.LOB_ID AND SUB2_OPEN.ADJUSTER_ID=MAIN.ADJUSTER_ID '                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS MO_REOPEN from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Re-open Reserve'' '                  
--  + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'  
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   	
   + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + 'group by lob_id,ADJUSTER_ID) SUB1_REOPEN '
	--ON CLM_CLAIM_INFO.lob_id = SUB1_REOPEN.lob_id AND SUB1_REOPEN.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
   + ' ON MAIN.LOB_ID = SUB1_REOPEN.LOB_ID AND SUB1_OPEN.ADJUSTER_ID=MAIN.ADJUSTER_ID '                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS YTD_REOPEN from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Re-open Reserve'' '                  
--   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'    
  --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
  + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'group by lob_id,ADJUSTER_ID) SUB2_REOPEN '
	--ON CLM_CLAIM_INFO.lob_id = SUB2_REOPEN.lob_id AND SUB2_REOPEN.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID 
	+ ' ON MAIN.LOB_ID = SUB2_REOPEN.LOB_ID AND SUB2_OPEN.ADJUSTER_ID=MAIN.ADJUSTER_ID'                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS MO_CLOSE from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES      
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Closed'' '                  
--    + 'AND ' + 'MONTH(CLM_CLAIM_INFO.LOSS_DATE) = MONTH(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')'                  
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.    
   + ' AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' + 'group by lob_id,ADJUSTER_ID) SUB1_CLOSE ' 
	--ON CLM_CLAIM_INFO.lob_id = SUB1_CLOSE.lob_id AND SUB1_CLOSE.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID 
	+ ' ON MAIN.LOB_ID = SUB1_CLOSE.LOB_ID AND SUB1_CLOSE.ADJUSTER_ID=MAIN.ADJUSTER_ID '                
                
 SELECT @sql=@sql + ' LEFT OUTER JOIN (SELECT ADJUSTER_ID,lob_id,count(claim_id) AS YTD_CLOSE from CLM_CLAIM_INFO,MNT_LOOKUP_VALUES                   
    where CLM_CLAIM_INFO.claim_status= MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID and UPPER(MNT_LOOKUP_VALUES.lookup_value_desc) = ''' + 'Closed'' '                  
--    + 'AND ' + 'YEAR(CLM_CLAIM_INFO.LOSS_DATE) = YEAR(''' + CONVERT(VARCHAR, @DATE_FROM,101) + ''')' 
   --LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.    
   + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + '''' 
-- Added by Asfa (01-Feb-2008) - iTrack issue #3019
   + 'AND ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) <= ''' + CONVERT(VARCHAR,@MONTH) + '''' + 'group by ADJUSTER_ID,lob_id) SUB2_CLOSE '
	--ON CLM_CLAIM_INFO.lob_id = SUB2_CLOSE.lob_id AND SUB2_CLOSE.ADJUSTER_ID=CLM_CLAIM_INFO.ADJUSTER_ID
  + 'ON MAIN.LOB_ID = SUB2_CLOSE.LOB_ID AND SUB2_CLOSE.ADJUSTER_ID=MAIN.ADJUSTER_ID '
	            
                
  END                
                  
/*IF (@SELECTED_CLAIM_STATUS ='' OR @COMMA_POS <> 0 )
BEGIN                  	
    IF @MONTH <> '' AND @YEAR <> ''
	BEGIN    
	--LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
--    SELECT @sql=@sql + ' WHERE ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
--	SELECT @sql=@sql + ' AND ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + ''''  
	
	select @sql = @sql + 'WHERE SUB1_OPEN.MO_OPEN IS NOT NULL AND SUB2_OPEN.YTD_OPEN IS NOT NULL
	AND SUB1_CLOSE.MO_CLOSE IS NOT NULL AND SUB2_CLOSE.YTD_CLOSE IS NOT NULL'
	END    
	ELSE IF @MONTH <> '' AND @YEAR = ''
	BEGIN     
	--LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
    SELECT @sql=@sql + ' WHERE ' + 'MONTH(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
	--SELECT @sql=@sql + ' AND ' + 'YEAR(INFO.LOSS_DATE) = ''' + CONVERT(VARCHAR,@YEAR) + ''''  
	END                
	ELSE IF @MONTH = '' AND @YEAR <> ''
	BEGIN    
	--SELECT @sql=@sql + ' WHERE ' + 'MONTH(INFO.LOSS_DATE) = ''' + CONVERT(VARCHAR,@MONTH) + '''' 
	--LOSS_DATE changed in to CREATED_DATETIME Itrack #issue 5742.   
    SELECT @sql=@sql + ' WHERE ' + 'YEAR(CLM_CLAIM_INFO.CREATED_DATETIME) = ''' + CONVERT(VARCHAR,@YEAR) + ''''  
	END                
          
END       */                 
                
                  
/* IF (@SELECTED_ADJUSTER <> '0')                  
  BEGIN    
 SELECT @SELECTED_ADJUSTER = REPLACE(@SELECTED_ADJUSTER,',',''',''')          
 SELECT @sql=@sql + ' WHERE MAIN.ADJUSTER_ID IN (''' + @SELECTED_ADJUSTER  + ''')'                  
  END                  
                  
 --Done for Itrack Issue 6342 on 13 Jan 2010
                 
IF (@SELECTED_PARTY_TYPES <> '0' AND @SELECTED_ADJUSTER <> '0')                  
  BEGIN      
 SELECT @SELECTED_PARTY_TYPES = REPLACE(@SELECTED_PARTY_TYPES,',',''',''')                  
 SELECT @sql=@sql + ' AND TYPE.DETAIL_TYPE_ID IN (''' + @SELECTED_PARTY_TYPES + ''')'     
  END                  
ELSE IF (@SELECTED_PARTY_TYPES <> '0' AND @SELECTED_ADJUSTER = '0')                  
  BEGIN     
 SELECT @SELECTED_PARTY_TYPES = REPLACE(@SELECTED_PARTY_TYPES,',',''',''')                  
 SELECT @sql=@sql + ' AND TYPE.DETAIL_TYPE_ID IN (''' + @SELECTED_PARTY_TYPES + ''')'                   
  END     */             
              
--Done for Itrack Issue 6342 on 13 Jan 2010
--IF ((@SELECTED_LOB<>'0') AND (@SELECTED_ADJUSTER<>'0' OR @SELECTED_PARTY_TYPES <>'0'))                              
/*IF ((@SELECTED_LOB<>'0') AND (@SELECTED_ADJUSTER<>'0'))                  
  BEGIN            
 SELECT @SELECTED_LOB = REPLACE(@SELECTED_LOB,',',''',''')                  
  SELECT @sql=@sql + ' AND MAIN.LOB_ID IN (''' +  @SELECTED_LOB  + ''')'                  
  END  
--ELSE IF ((@SELECTED_LOB <> '0' ) AND (@SELECTED_ADJUSTER ='0' OR @SELECTED_PARTY_TYPES ='0'))                                  
ELSE IF ((@SELECTED_LOB <> '0' ) AND (@SELECTED_ADJUSTER ='0'))                  
  BEGIN                  
  SELECT @SELECTED_LOB = REPLACE(@SELECTED_LOB,',',''',''')            
 SELECT @sql=@sql + ' WHERE MAIN.LOB_ID IN (''' +  @SELECTED_LOB  + ''')'                  
  END 
*/
IF(@SELECTED_CLAIM_STATUS <> '' AND ( @COMMA_POS = 0 OR @COMMA_POS = 2))
  SELECT @sql=@sql + ' WHERE YTD_OPEN IS NOT NULL'    
ELSE IF(@SELECTED_CLAIM_STATUS <> '' AND ( @COMMA_POS = 1))
  SELECT @sql=@sql + ' WHERE SUB2.YTD IS NOT NULL AND SUB3.MO IS NOT NULL OR SUB4.YTD IS NOT NULL'
ELSE     
  SELECT @sql=@sql + ' WHERE YTD_OPEN IS NOT NULL OR MO_CLOSE IS NOT NULL OR YTD_CLOSE IS NOT NULL'  
                 
--Condition Added  @SECOND_SORT <> @FIRST_SORT For Claim Count by Adjuster Report                  
IF (@FIRST_SORT<>'' and (@SECOND_SORT <> @FIRST_SORT))                  
BEGIN                  
	SELECT @sql=@sql + ' ORDER BY ' + @FIRST_SORT                   
END                  
                
IF (@SECOND_SORT<>'' AND @FIRST_SORT <> '')             
  BEGIN

	IF(@SECOND_SORT = @FIRST_SORT)
	BEGIN    	          
		SELECT @SQL =@SQL + ' ORDER BY ' + @SECOND_SORT   
		--print @SQL             
	END
	ELSE
	BEGIN
		SELECT @SQL=@SQL + ', ' + @SECOND_SORT     		
	END 
  END                 
ELSE IF (@SECOND_SORT<>'' AND @FIRST_SORT = '')                
  BEGIN                
	SELECT @sql=@sql + ' ORDER BY ' + @SECOND_SORT                
  END          
 print @sql                
EXEC(@sql)                  
END             
            
----Proc_GetClaimsCountbyAdjusterWithSummary '11/01/2007','11/15/2007','0','0','11739','0','LOB_DESC','LOOKUP_VALUE_DESC'
--go
----Proc_GetClaimsCountbyAdjusterWithSummary 5, 2008, '0', '0', '11739', '0', 'adjuster_name' , 'LOB_DESC'
--Proc_GetClaimsCountbyAdjusterWithSummary 12,2009,'0', '', '0', '',''
--rollback tran

GO

