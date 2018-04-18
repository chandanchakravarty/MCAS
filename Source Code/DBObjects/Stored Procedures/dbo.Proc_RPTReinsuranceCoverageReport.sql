IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RPTReinsuranceCoverageReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RPTReinsuranceCoverageReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------------------------      
CREATE BY   	:    Arun Dhingra	
CREATE DATETIME :   10 August,2007
PURPOSE    	: 
REVIOSN HISTORY      
Revised By  Date  Reason      

select * from MNT_COUNTRY_STATE_LIST
select * from MNT_REINSURANCE_COVERAGE

Proc_RPTReinsuranceCoverageReport '2','14'

----------------------------------------------------------------------------*/      
--drop proc dbo.Proc_RPTReinsuranceCoverageReport 
CREATE  PROC dbo.Proc_RPTReinsuranceCoverageReport
(     
 @LOB varchar(8000) = '',
 @STATE  varchar(8000) = '' 
)
      
AS    
  
DECLARE @sql VARCHAR(8000)

BEGIN   

	Select @sql = 'SELECT COV_DES,STATE_CODE,LOB_CODE,LOOKUP_LOB.LOOKUP_VALUE_DESC AS REINSURANCE_LOB,
CASE WHEN REINSURANCE_LOB=10963  THEN LOOKUP_COV.LOOKUP_VALUE_DESC 
     WHEN REINSURANCE_LOB=10964  THEN ''Not Applicable''
ELSE NULL END AS REINSURANCE_COV ,
LOOKUP_ASLOB.LOOKUP_VALUE_DESC AS ASLOB,LOOKUP_CALC.LOOKUP_VALUE_DESC AS REINSURANCE_CALC, ''N''
			 as Always_Display,MC.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,MC.EFFECTIVE_TO_DATE as EFFECTIVE_TO_DATE,LOOKUP_BUCKET.LOOKUP_VALUE_DESC as REIN_REPORT_BUCK From MNT_COVERAGE MC Inner Join MNT_COUNTRY_STATE_LIST SL 
			ON MC.STATE_ID = SL.STATE_ID INNER JOIN MNT_LOB_MASTER LOB ON MC.LOB_ID = LOB.LOB_ID 
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_LOB ON LOOKUP_LOB.LOOKUP_UNIQUE_ID = MC.REINSURANCE_LOB
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_COV ON LOOKUP_COV.LOOKUP_UNIQUE_ID = MC.REINSURANCE_COV
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_CALC ON LOOKUP_CALC.LOOKUP_UNIQUE_ID = MC.REINSURANCE_CALC
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_ASLOB ON LOOKUP_ASLOB.LOOKUP_UNIQUE_ID = MC.ASLOB
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_BUCKET ON LOOKUP_BUCKET.LOOKUP_UNIQUE_ID = MC.REIN_REPORT_BUCK
			Where 1=1'
			
	
	IF (ISNULL(@LOB,'0') <> '0' AND @LOB<>'' )
	
	BEGIN
		SELECT @sql=@sql + ' AND MC.LOB_ID In (' + @LOB + ')'
	END

	IF (ISNULL(@STATE,'0') <> '0' AND @STATE<>'' )
	
	BEGIN
		SELECT @sql=@sql + ' AND MC.STATE_ID In (' + @STATE + ')'
	END

	Select @sql =@sql + ' UNION SELECT COV_DES,STATE_CODE,LOB_CODE,LOOKUP_LOB.LOOKUP_VALUE_DESC AS REINSURANCE_LOB,
--LOOKUP_COV.LOOKUP_VALUE_DESC AS REINSURANCE_COV,
CASE WHEN REINSURANCE_LOB=10963  THEN LOOKUP_COV.LOOKUP_VALUE_DESC 
     WHEN REINSURANCE_LOB=10964  THEN ''Not Applicable''
ELSE NULL END AS REINSURANCE_COV ,
LOOKUP_ASLOB.LOOKUP_VALUE_DESC AS ASLOB,LOOKUP_CALC.LOOKUP_VALUE_DESC AS REINSURANCE_CALC,''Y''
 as Always_Display ,MC.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,MC.EFFECTIVE_TO_DATE as EFFECTIVE_TO_DATE,LOOKUP_BUCKET.LOOKUP_VALUE_DESC as REIN_REPORT_BUCK From MNT_REINSURANCE_COVERAGE MC Inner Join MNT_COUNTRY_STATE_LIST SL 
			ON MC.STATE_ID = SL.STATE_ID INNER JOIN MNT_LOB_MASTER LOB ON MC.LOB_ID = LOB.LOB_ID 
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_LOB ON LOOKUP_LOB.LOOKUP_UNIQUE_ID = MC.REINSURANCE_LOB
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_COV ON LOOKUP_COV.LOOKUP_UNIQUE_ID = MC.REINSURANCE_COV
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_CALC ON LOOKUP_CALC.LOOKUP_UNIQUE_ID = MC.REINSURANCE_CALC
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_ASLOB ON LOOKUP_ASLOB.LOOKUP_UNIQUE_ID = MC.ASLOB
			LEFT OUTER JOIN mnt_lookup_values LOOKUP_BUCKET ON LOOKUP_BUCKET.LOOKUP_UNIQUE_ID = MC.REIN_REPORT_BUCK
			Where 1=1'
			
	
	IF (ISNULL(@LOB,'0') <> '0' AND @LOB<>'' )
	
	BEGIN
		SELECT @sql=@sql + ' AND MC.LOB_ID In (' + @LOB + ')'
	END

	IF (ISNULL(@STATE,'0') <> '0' AND @STATE<>'' )
	
	BEGIN
		SELECT @sql=@sql + ' AND MC.STATE_ID In (' + @STATE + ')'
	END

END

--PRINT(@sql) 
EXEC (@sql)





GO

