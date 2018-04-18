IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_BEGINING_ENDING_RESERVE_BY_COVERAGE]'))
DROP VIEW [dbo].[VW_BEGINING_ENDING_RESERVE_BY_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP VIEW dbo.VW_BEGINING_ENDING_RESERVE_BY_COVERAGE 
--GO
/*------------------------------------------------------------------
View Name                : dbo.VW_BEGINING_ENDING_RESERVE_BY_COVERAGE
Created by               : Pravesh K Chandel                                                                      
Date                     : 28 Aug 2008
Purpose                  : To get the BEGINING ENDING RESERVE coverge wise
Revison History          :  
Modified by				 : Pravesh K chandel
date					 : 3 July 09	 	
purpose					 : fetch risk id column	
Used In                  : Wolverine  
drop VIEW dbo.VW_BEGINING_ENDING_RESERVE_BY_COVERAGE 
*/
CREATE VIEW [dbo].[VW_BEGINING_ENDING_RESERVE_BY_COVERAGE]
AS

WITH BEGIN_END_RESERVE AS
(
	select CAR.CLAIM_ID,CAR.COVERAGE_ID,YEAR(COMPLETED_DATE)YEAR_NUMBER,MONTH(COMPLETED_DATE) AS MONTH_NUMBER,
--	VD.PURPOSE_OF_USE,VD.POLICY_RISK_ID,
	
	CAR.ACTUAL_RISK_ID AS POLICY_RISK_ID, CAR.ACTUAL_RISK_TYPE,
	/*(
		SELECT TOP 1 RISK.PURPOSE_OF_USE 
		FROM CLM_ACTIVITY_RESERVE CR  WITH(NOLOCK)  
		INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  
			ON C.CLAIM_ID=CR.CLAIM_ID 
			AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID
			AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)    
		INNER JOIN CLM_INSURED_VEHICLE RISK WITH(NOLOCK)  
			ON CASE CR.VEHICLE_ID WHEN 0 THEN 1 ELSE CR.VEHICLE_ID  END = RISK.INSURED_VEHICLE_ID
			AND RISK.CLAIM_ID = CR.CLAIM_ID 
		WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
		AND C.ACTIVITY_ID=    
				(
					SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WHERE CLAIM_ID=C.CLAIM_ID 
					AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
					AND MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) 
					AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE)   
					AND ACTION_ON_PAYMENT IN (165,166,167,168,205)    
				)    
		AND ISNULL(RISK.POLICY_VEHICLE_ID,0) <> 0 
	) AS PURPOSE_OF_USE ,
	(
	  SELECT TOP 1 POLICY_RISK_ID FROM
		(
			SELECT TOP 1 RISK.POLICY_VEHICLE_ID AS POLICY_RISK_ID
			FROM CLM_ACTIVITY_RESERVE CR  WITH(NOLOCK)  
			INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  
				ON C.CLAIM_ID=CR.CLAIM_ID 
				AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID
				AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)    
			INNER JOIN CLM_INSURED_VEHICLE RISK WITH(NOLOCK)  
				ON CASE CR.VEHICLE_ID WHEN 0 THEN 1 ELSE CR.VEHICLE_ID  END = RISK.INSURED_VEHICLE_ID
				AND RISK.CLAIM_ID = CR.CLAIM_ID 
			WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
			AND C.ACTIVITY_ID=    
					(
						SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WHERE CLAIM_ID=C.CLAIM_ID 
						AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
						AND MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) 
						AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE)   
						AND ACTION_ON_PAYMENT IN (165,166,167,168,205)    
					)    
			AND ISNULL(RISK.POLICY_VEHICLE_ID,0) <> 0 
		UNION 
    		SELECT TOP 1 RISK.POLICY_BOAT_ID AS POLICY_RISK_ID
			FROM CLM_ACTIVITY_RESERVE CR  WITH(NOLOCK)  
			INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  
				ON C.CLAIM_ID=CR.CLAIM_ID 
				AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID
				AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)    
			INNER JOIN CLM_INSURED_BOAT RISK WITH(NOLOCK)  
				ON CASE CR.VEHICLE_ID WHEN 0 THEN 1 ELSE CR.VEHICLE_ID  END = RISK.BOAT_ID
				AND RISK.CLAIM_ID = CR.CLAIM_ID 
			WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
			AND C.ACTIVITY_ID=    
					(
						SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WHERE CLAIM_ID=C.CLAIM_ID 
						AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
						AND MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) 
						AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE)   
						AND ACTION_ON_PAYMENT IN (165,166,167,168,205)    
					)    
			AND ISNULL(RISK.POLICY_BOAT_ID,0) <> 0 
		UNION 
    		SELECT TOP 1 RISK.POLICY_LOCATION_ID AS POLICY_RISK_ID
			FROM CLM_ACTIVITY_RESERVE CR  WITH(NOLOCK)  
			INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  
				ON C.CLAIM_ID=CR.CLAIM_ID 
				AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID
				AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)    
			INNER JOIN CLM_INSURED_LOCATION RISK WITH(NOLOCK)  
				ON CASE CR.VEHICLE_ID WHEN 0 THEN 1 ELSE CR.VEHICLE_ID  END = RISK.INSURED_LOCATION_ID
				AND RISK.CLAIM_ID = CR.CLAIM_ID 
			WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
			AND C.ACTIVITY_ID=    
					(
						SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WHERE CLAIM_ID=C.CLAIM_ID 
						AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
						AND MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) 
						AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE)   
						AND ACTION_ON_PAYMENT IN (165,166,167,168,205)    
					)    
			AND ISNULL(RISK.POLICY_LOCATION_ID,0) <> 0
		)TMP

	) AS POLICY_RISK_ID ,*/

	(
		SELECT SUM(OUTSTANDING) FROM CLM_ACTIVITY_RESERVE CR WITH(NOLOCK)  
		INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  
		ON C.CLAIM_ID=CR.CLAIM_ID 
		AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID 
		AND (	C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986	)
		WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
     /*     AND MONTH(C.COMPLETED_DATE)=   --CASE WHEN MONTH(CA.COMPLETED_DATE)=1 THEN 12 ELSE MONTH(CA.COMPLETED_DATE)-1 END    
					(
					SELECT top 1 MONTH(COMPLETED_DATE) FROM CLM_ACTIVITY WHERE CLAIM_ID=C.CLAIM_ID 
					AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
					AND ACTION_ON_PAYMENT IN (165,166,167,168,205)    
					and COMPLETED_DATE < CAST( CONVERT(VARCHAR, MONTH(CA.COMPLETED_DATE) ) + '/01/'  + CONVERT(VARCHAR, YEAR(CA.COMPLETED_DATE)) AS DATETIME) 
						order by MONTH(CA.COMPLETED_DATE) desc
				 )
	*/

		 and datediff(dd, (C.COMPLETED_DATE) ,(    
		 SELECT top 1 (CLM_ACTIVITY.COMPLETED_DATE) FROM CLM_ACTIVITY   WITH(NOLOCK)  
		 WHERE CLM_ACTIVITY.CLAIM_ID = C.CLAIM_ID  
		 AND CLM_ACTIVITY.ACTIVITY_STATUS = C.ACTIVITY_STATUS  
		 AND CLM_ACTIVITY.ACTION_ON_PAYMENT IN (165,166,167,168,205)        
		 and CLM_ACTIVITY.COMPLETED_DATE <   
		 CAST( CONVERT(VARCHAR, MONTH(CA.COMPLETED_DATE) ) + '/01/'  + CONVERT(VARCHAR, YEAR(CA.COMPLETED_DATE)) AS DATETIME)      
		  order by (CLM_ACTIVITY.COMPLETED_DATE) desc    
		) ) = 0  


		--AND YEAR(C.COMPLETED_DATE)=CASE WHEN MONTH(CA.COMPLETED_DATE)=1 THEN YEAR(CA.COMPLETED_DATE)-1 ELSE YEAR(CA.COMPLETED_DATE) END 
		AND C.ACTIVITY_ID=	
			(SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WITH(NOLOCK)   WHERE CLAIM_ID=C.CLAIM_ID AND ACTIVITY_STATUS=C.ACTIVITY_STATUS 
			AND	MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE)
			AND ACTION_ON_PAYMENT IN (165,166,167,168,205)  
			)
		AND CR.COVERAGE_ID=CAR.COVERAGE_ID
	)BEGIN_RESERVE,

	(
		SELECT SUM(OUTSTANDING) FROM CLM_ACTIVITY_RESERVE CR WITH(NOLOCK)  
		INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  ON C.CLAIM_ID=CR.CLAIM_ID 
		AND CASE CR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID END  = C.ACTIVITY_ID
		AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)
		WHERE CR.CLAIM_ID=CAR.CLAIM_ID AND MONTH(C.COMPLETED_DATE) = MONTH(CA.COMPLETED_DATE)
		AND YEAR(C.COMPLETED_DATE)=YEAR(CA.COMPLETED_DATE)
		AND C.ACTIVITY_ID=
		(
			SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WITH(NOLOCK) WHERE CLAIM_ID=C.CLAIM_ID AND ACTIVITY_STATUS=C.ACTIVITY_STATUS 
			AND	MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE) 
			AND ACTION_ON_PAYMENT IN (165,166,167,168,205)
		)
		AND CR.COVERAGE_ID=CAR.COVERAGE_ID
	)END_RESERVE,

	(
		SELECT SUM(CR.RI_RESERVE) FROM CLM_ACTIVITY_RESERVE CR WITH(NOLOCK)  
		INNER JOIN CLM_ACTIVITY C WITH(NOLOCK) ON C.CLAIM_ID=CR.CLAIM_ID 
		AND C.ACTIVITY_ID=CR.ACTIVITY_ID 
		AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)
		WHERE CR.CLAIM_ID=CAR.CLAIM_ID 
		AND MONTH(C.COMPLETED_DATE) = --CASE WHEN MONTH(CA.COMPLETED_DATE)=1 THEN 12 ELSE MONTH(CA.COMPLETED_DATE)-1 END
					(
					SELECT top 1 MONTH(COMPLETED_DATE) FROM CLM_ACTIVITY WITH(NOLOCK)  WHERE CLAIM_ID=C.CLAIM_ID 
					AND ACTIVITY_STATUS=C.ACTIVITY_STATUS     
					AND ACTION_ON_PAYMENT IN (169,171,170,172)    
					and COMPLETED_DATE < CAST( CONVERT(VARCHAR, MONTH(CA.COMPLETED_DATE) ) + '/01/'  + CONVERT(VARCHAR, YEAR(CA.COMPLETED_DATE)) AS DATETIME) 
						order by MONTH(CA.COMPLETED_DATE) desc
				)
		AND YEAR(C.COMPLETED_DATE)=CASE WHEN MONTH(CA.COMPLETED_DATE)=1 THEN YEAR(CA.COMPLETED_DATE)-1 ELSE YEAR(CA.COMPLETED_DATE) END 
		AND C.ACTIVITY_ID=
		(
			SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WITH(NOLOCK)  WHERE CLAIM_ID=C.CLAIM_ID AND ACTIVITY_STATUS=C.ACTIVITY_STATUS 
			AND	MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE) 
			AND ACTION_ON_PAYMENT IN (169,171,170,172)    
		)
		AND CR.COVERAGE_ID=CAR.COVERAGE_ID
	)BEGIN_RESERVE_REINSURANCE,

	(
		SELECT SUM(CR.RI_RESERVE) FROM CLM_ACTIVITY_RESERVE CR WITH(NOLOCK)  
		INNER JOIN CLM_ACTIVITY C WITH(NOLOCK)  ON C.CLAIM_ID=CR.CLAIM_ID AND C.ACTIVITY_ID=CR.ACTIVITY_ID AND (C.ACTIVITY_STATUS=11801 OR C.ACTIVITY_STATUS=11986)
		WHERE CR.CLAIM_ID=CAR.CLAIM_ID AND MONTH(C.COMPLETED_DATE) = MONTH(CA.COMPLETED_DATE)
		AND YEAR(C.COMPLETED_DATE)=YEAR(CA.COMPLETED_DATE)
		AND C.ACTIVITY_ID=
		(
			SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY WITH(NOLOCK) WHERE CLAIM_ID=C.CLAIM_ID AND ACTIVITY_STATUS=C.ACTIVITY_STATUS 
			AND	MONTH(COMPLETED_DATE)=MONTH(C.COMPLETED_DATE) AND YEAR(COMPLETED_DATE)=YEAR(C.COMPLETED_DATE) 
			AND ACTION_ON_PAYMENT IN (169,171,170,172)
		)
		AND CR.COVERAGE_ID=CAR.COVERAGE_ID
	)END_RESERVE_REINSURANCE


from CLM_ACTIVITY_RESERVE CAR WITH(NOLOCK)
INNER JOIN CLM_ACTIVITY CA WITH(NOLOCK) 
	ON CA.CLAIM_ID=CAR.CLAIM_ID 
	AND CASE CAR.ACTIVITY_ID WHEN 0 THEN 1 ELSE CAR.ACTIVITY_ID END  = CA.ACTIVITY_ID
	AND (CA.ACTIVITY_STATUS=11801 OR CA.ACTIVITY_STATUS=11986)
--INNER JOIN VW_RISK_DETAILS VD WITH(NOLOCK)
--	ON CAR.CLAIM_ID = VD.CLAIM_ID
WHERE COVERAGE_ID NOT IN (20007,20008,20009,20010,20011)--YEAR(COMPLETED_DATE)=YEAR(GETDATE())
group by CAR.CLAIM_ID,COVERAGE_ID,YEAR(CA.COMPLETED_DATE),MONTH(CA.COMPLETED_DATE), --VD.PURPOSE_OF_USE, VD.POLICY_RISK_ID
CAR.ACTUAL_RISK_ID, CAR.ACTUAL_RISK_TYPE
),

--SELECT PCPL.STATE_ID,PCPL.AGENCY_ID,PCPL.POLICY_LOB AS LOB_ID,PCPL.POLICY_ID,PCPL.POLICY_NUMBER,VWBE.* FROM 
--BEGIN_END_RESERVE VWBE
--INNER JOIN CLM_CLAIM_INFO CLM ON VWBE.CLAIM_ID=CLM.CLAIM_ID
--INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.CUSTOMER_ID=CLM.CUSTOMER_ID
--AND PCPL.POLICY_ID=CLM.POLICY_ID AND PCPL.POLICY_VERSION_ID=CLM.POLICY_VERSION_ID


-----CHANGED FROM HERE
BEGINING_ENDING_RESERVE  AS
( 
SELECT PCPL.STATE_ID,PCPL.AGENCY_ID,PCPL.POLICY_LOB AS LOB_ID,PCPL.POLICY_ID,
CLM.POLICY_VERSION_ID,
PCPL.POLICY_NUMBER,CLM.CREATED_DATETIME,
case when claim_status ='11739' then null else CLOSED_DATE end as CLOSED_DATE,
VWBE.* 
,
case when clm.lob_id = 2 then (select purpose_of_use from clm_insured_vehicle where claim_id = vwbe.claim_id AND POLICY_VEHICLE_ID = VWBE.POLICY_RISK_ID)
	else null
end as purpose_of_use
FROM BEGIN_END_RESERVE VWBE WITH(NOLOCK)   
INNER JOIN CLM_CLAIM_INFO CLM WITH(NOLOCK) ON VWBE.CLAIM_ID=CLM.CLAIM_ID    
Left Outer JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID=CLM.CUSTOMER_ID    
AND PCPL.POLICY_ID=CLM.POLICY_ID AND PCPL.POLICY_VERSION_ID=CLM.POLICY_VERSION_ID    
 ) ,
YEAR_TABLE1 as
(
SELECT YEAR(GETDATE()) AS CUR_YEAR
),
YEAR_TABLE AS
(
SELECT distinct YEAR_NUMBER AS NEW_YEAR FROM BEGINING_ENDING_RESERVE,year_table1
)

---------FINAL QUERY
SELECT distinct * FROM
(
	SELECT distinct
		STATE_ID,AGENCY_ID,LOB_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_NUMBER,
		CLAIM_ID,COVERAGE_ID,tt.month as MONTH_NUMBER,YT.NEW_YEAR AS YEAR_NUMBER,

	ISNULL(
		isnull(
				(
					SELECT BEGIN_RESERVE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND COVERAGE_ID=T.COVERAGE_ID 
					AND MONTH_NUMBER=TT.MONTH AND YEAR_NUMBER=YT.NEW_YEAR AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
				) ,
				(
					SELECT TOP 1 END_RESERVE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND   COVERAGE_ID=T.COVERAGE_ID
					AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
					AND CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/' + '01' + '/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME)
					<= CAST(CONVERT(VARCHAR,TT.MONTH) + '/' + '01' + '/' + CONVERT(VARCHAR,YT.NEW_YEAR ) AS DATETIME)
					ORDER BY YEAR_NUMBER DESC, MONTH_NUMBER DESC
				) 
			  )
		  ,0) BEGIN_RESERVE,

	ISNULL(
		isnull(
				(
					SELECT END_RESERVE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND COVERAGE_ID=T.COVERAGE_ID 
					AND MONTH_NUMBER=TT.MONTH AND YEAR_NUMBER=YT.NEW_YEAR AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
				),
				(
					SELECT TOP 1 END_RESERVE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND   COVERAGE_ID=T.COVERAGE_ID
					AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
					AND CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/' + '01' + '/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME)
					<= CAST(CONVERT(VARCHAR,TT.MONTH) + '/' + '01' + '/' + CONVERT(VARCHAR,YT.NEW_YEAR ) AS DATETIME)
					ORDER BY YEAR_NUMBER DESC,MONTH_NUMBER DESC
				) 
			  )
		 ,0) END_RESERVE,

	ISNULL(
		isnull(
				(
					SELECT BEGIN_RESERVE_REINSURANCE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND COVERAGE_ID=T.COVERAGE_ID 
					AND MONTH_NUMBER=TT.MONTH AND YEAR_NUMBER=YT.NEW_YEAR AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
				) ,
				(
					SELECT TOP 1 END_RESERVE_REINSURANCE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND   COVERAGE_ID=T.COVERAGE_ID
					AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
					AND CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/' + '01' + '/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME)
					<= CAST(CONVERT(VARCHAR,TT.MONTH) + '/' + '01' + '/' + CONVERT(VARCHAR,YT.NEW_YEAR ) AS DATETIME)
					ORDER BY YEAR_NUMBER DESC, MONTH_NUMBER DESC
				)
			  ) 
		  ,0) BEGIN_RESERVE_REINSURANCE,

	ISNULL(
		ISNULL(
				(
					SELECT END_RESERVE_REINSURANCE FROM BEGINING_ENDING_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID AND COVERAGE_ID=T.COVERAGE_ID 
					AND MONTH_NUMBER=TT.MONTH AND YEAR_NUMBER=YT.NEW_YEAR AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
				),
				(
					SELECT TOP 1 BEG_END_IN.END_RESERVE_REINSURANCE FROM BEGINING_ENDING_RESERVE BEG_END_IN WITH(NOLOCK) WHERE CLAIM_ID=T.CLAIM_ID 
					AND COVERAGE_ID=T.COVERAGE_ID AND POLICY_RISK_ID = T.POLICY_RISK_ID AND ACTUAL_RISK_TYPE = T.ACTUAL_RISK_TYPE
					AND CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/' + '01' + '/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME)
					<= CAST(CONVERT(VARCHAR,TT.MONTH) + '/' + '01' + '/' + CONVERT(VARCHAR,YT.NEW_YEAR ) AS DATETIME)
					AND ISNULL(BEG_END_IN.END_RESERVE_REINSURANCE,0) <> 0
					ORDER BY YEAR_NUMBER DESC,MONTH_NUMBER DESC
				) 
			  )
		  ,0) END_RESERVE_REINSURANCE,
	 STARTDATE,ENDDATE,PURPOSE_OF_USE,POLICY_RISK_ID,ACTUAL_RISK_TYPE
	 FROM
		(  SELECT DISTINCT MONTH_NUMBER,CLAIM_ID,COVERAGE_ID,
				STATE_ID,
				AGENCY_ID,
				LOB_ID,
				POLICY_ID,
				POLICY_VERSION_ID,
				POLICY_NUMBER,
				YEAR_NUMBER,
				PURPOSE_OF_USE,
				POLICY_RISK_ID,	
				ACTUAL_RISK_TYPE,
				[YEAR_NUMBER] IYEAR,
				CREATED_DATETIME AS STARTDATE,
				CLOSED_DATE AS ENDDATE 
			FROM BEGINING_ENDING_RESERVE WITH(NOLOCK)
		)T, MONTHS_TABLE TT,YEAR_TABLE YT

) FINAL
WHERE BEGIN_RESERVE IS NOT NULL 
AND DATEADD(DD,-1,DATEADD(MM,1,CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/01/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME))) >=cast(convert(varchar,STARTDATE,101) as datetime)
AND   
(  
	DATEADD(DD,-1,DATEADD(MM,1,CAST( CONVERT(VARCHAR,MONTH( isnull(ENDDATE,dateadd(dd,-1,dateadd(m,1,getdate()))) ) ) + '/01/' + CONVERT(VARCHAR,YEAR(isnull(ENDDATE,dateadd(dd,-1,dateadd(m,1,getdate()))) )) AS DATETIME))) >=   
	DATEADD(DD,-1,DATEADD(MM,1,CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/01/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME)))   
)  

--AND 
--(
--isnull(ENDDATE,dateadd(dd,-1,dateadd(m,1,getdate()))) >=  DATEADD(DD,-1,DATEADD(MM,1,CAST(CONVERT(VARCHAR,MONTH_NUMBER) + '/01/' + CONVERT(VARCHAR,YEAR_NUMBER) AS DATETIME))) 
--)
--ORDER BY FINAL.YEAR_NUMBER, FINAL.CLAIM_ID, FINAL.[MONTH_NUMBER]


--GO
--SELECT * FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE  where POLICY_NUMBER = 'A2813230' and BEGIN_RESERVE <> 0
--order by YEAR_NUMBER, MONTH_NUMBER  
----WHERE YEAR_NUMBER = 2010 --and purpose_of_use is not null --AND MONTH_NUMBER = 6 --AND LOB_ID = 4 --AND POLICY_RISK_ID IS NULL
----SELECT SUM(BEGIN_RESERVE)BEGIN_RESERVE, SUM(END_RESERVE)END_RESERVE, SUM(BEGIN_RESERVE_REINSURANCE)BEGIN_RESERVE_REINSURANCE, SUM(END_RESERVE_REINSURANCE)END_RESERVE_REINSURANCE  
----FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE  WHERE YEAR_NUMBER = 2010 AND MONTH_NUMBER = 5 --AND CLAIM_ID = 2445
--ROLLBACK TRAN

GO

