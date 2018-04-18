
/****** Object:  StoredProcedure [dbo].[SP_ProvisionReport_PCP]    Script Date: 09/30/2011 17:31:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_ProvisionReport_PCP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_ProvisionReport_PCP]
GO

/*
Proc Name	:[dbo].[SP_ProvisionReport_PCP]
Created By	: Pravesh K Chandel
Created Date: 26 Sep 2011
Purpose		: Fetch Provision report PCP iTrack 1664
Used in		: ALBA PCP Reports
*/  
--drop  PROCEDURE [dbo].[SP_ProvisionReport_PCP]  
CREATE  PROCEDURE [dbo].[SP_ProvisionReport_PCP]  
(
@DATETIME datetime, 
@POLICY_NUMBER nvarchar(25)=null
)
AS   
BEGIN  

/* Formula Used for UEPR Column
--Premium*(1 - (Date - Effective Date)/(Expiry Date - Effective Date))
*/

SET NOCOUNT ON  

DECLARE @MONTHSTARTDATE DATETIME  
SET @MONTHSTARTDATE = (SELECT CONVERT(VARCHAR(25),DATEADD(DD,-(DAY(@DATETIME)-1),@DATETIME),101))  

DECLARE @SqlQuery nVARCHAR(MAX) 
DECLARE @ColName VARCHAR(MAX) 
set @ColName=''
SET @SqlQuery = ''   
--  print all days from starting of given date  
  
create table #temp(DateCompared datetime)  
declare @startdate datetime   
declare @enddate datetime  
declare @maxDay int
set @startdate = @monthstartdate  
set @enddate= @DATETIME  
set @maxDay = day(@enddate)

while (@enddate >= @startdate)  
begin  
set @ColName = @ColName + ',[' + cast(day(@startdate) as varchar(2)) + '/' + cast(month(@startdate)as varchar(2)) + ']'
insert into #temp values (@startdate)  
set @startdate = DATEADD(dd,1 ,@startdate)  
End  

Set @ColName=substring(@ColName,2,len(@ColName))

SET @SqlQuery ='SELECT *
FROM 
( 
  Select SUSEP_LOB,POLICYNUMBER,[DayMonth] As [DayMonth],UEPR As UEPR,TOTAL_UEPR,AVG_UEPR,LASTDAY_UEPR,PCP
  From #tmpPCPTabel As T1
) AS AA
PIVOT ( sum(UEPR) FOR [DayMonth] IN ( ' + @ColName + ' ) ) AS pvt '

;
with PCPTabel as
(
SELECT DISTINCT tp.DateCompared, pc.CUSTOMER_ID, pc.POLICY_ID, pc.POLICY_VERSION_ID ,  POLICY_LOB AS LOB_ID, 
 pc.POLICY_EFFECTIVE_DATE, pc.POLICY_EXPIRATION_DATE,POLICY_NUMBER as POLICYNUMBER,   
 DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) as TOTAL_TERM,  
 DATEDIFF(DD,POLICY_EFFECTIVE_DATE,@DATETIME) as ELAPSE_DAYS,  
-- DATEDIFF(DD,POLICY_EFFECTIVE_DATE,tp.DateCompared),  
 
 (DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,tp.DateCompared),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE))) as TIME_TO_ELAPSE,  
 ap.TOTAL_PREMIUM as TOTAL_PREMIUM,   
( CAST(DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,tp.DateCompared),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE)) AS DECIMAL(25,8)) / CAST(DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) AS DECIMAL(25,8)) ) as COMPUTED_TIME,  
/*(ap.TOTAL_PREMIUM * ( CAST(DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,tp.DateCompared),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE)) AS DECIMAL(25,8)) / CAST(DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) AS DECIMAL(25,8)) )) 


as PPNG  */cast(day(tp.DateCompared) as varchar) + '/' + cast(month(tp.DateCompared) as varchar) as DayMonth,CAST ( 
ap.TOTAL_PREMIUM  * ( 1-(datediff(dd,POLICY_EFFECTIVE_DATE,tp.DateCompared)/(CAST(DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) AS DECIMAL(25,8))) )) AS DECIMAL(25,2))  as UEPR  
FROM POL_CUSTOMER_POLICY_LIST pc  
INNER JOIN #temp tp  
ON tp.DateCompared >= POLICY_EFFECTIVE_DATE   
AND tp.DateCompared <= POLICY_EXPIRATION_DATE  
INNER JOIN ACT_POLICY_INSTALL_PLAN_DATA ap  
ON pc.POLICY_ID = ap.POLICY_ID   
AND pc.CUSTOMER_ID = ap.CUSTOMER_ID   
AND pc.POLICY_VERSION_ID = ap.POLICY_VERSION_ID   
WHERE pc.POLICY_NUMBER = isnull(@POLICY_NUMBER,pc.POLICY_NUMBER)
--and pc.POLICY_NUMBER in ('88998201181003000944','88998201181007000945','88998201181003000946')
--and pc.POLICY_NUMBER = '88998201181003000944'
--ORDER BY tp.DateCompared   
),

--select * from PCPTabel
PCP_AVERAGE
AS(
SELECT LOB_ID,SUM(TOTAL_UEPR) TOTAL_UEPR,AVG(TOTAL_UEPR) AVG_UEPR,
MAX(LASTDAYUEPR) LASTDAY_UEPR,
AVG(TOTAL_UEPR)- MAX(LASTDAYUEPR) PCP
FROM
(
SELECT LOB_ID,DAYMONTH,MAX(UEPR) AS UEPR,SUM(UEPR) AS TOTAL_UEPR, 
(CASE WHEN CAST(DBO.PIECE(DayMonth,'/',1) AS NUMERIC) = @MAXDAY THEN SUM(UEPR) ELSE 0 END) AS LASTDAYUEPR

FROM PCPTABEL
GROUP BY LOB_ID,DayMonth
--ORDER BY CAST(DBO.PIECE(DAYMONTH,'/',1) AS NUMERIC)
)TMP 
GROUP BY LOB_ID
),

FINALPCPTABLE
AS(
SELECT LOB.LOB_DESC AS SUSEP_LOB,PCP.TOTAL_UEPR,PCP.AVG_UEPR, PCP.LASTDAY_UEPR,PCP.PCP,PCPT.* FROM PCP_AVERAGE PCP
JOIN PCPTABEL PCPT ON  PCP.LOB_ID=PCPT.LOB_ID
JOIN MNT_LOB_MASTER_MULTILINGUAL LOB ON LOB.LOB_ID=PCP.LOB_ID
)

select * into #tmpPCPTabel from FinalPCPTable

execute (@SqlQuery)
drop table #tmpPCPTabel

end  
