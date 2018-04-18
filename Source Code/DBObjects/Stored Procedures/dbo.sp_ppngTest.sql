IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ppngTest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ppngTest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec sp_ppngTest '12/20/2010'

CREATE PROCEDURE [dbo].[sp_ppngTest]
@givendate datetime
as 
begin
set nocount on
--set @givendate = '12/20/2010'
declare @monthstartdate datetime
set @monthstartdate = (SELECT CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(@givendate)-1),@givendate),101))

--  print all days from starting of given date

create table #temp(DateCompared datetime)
declare @startdate datetime 
declare @enddate datetime
set @startdate = @monthstartdate
set @enddate= @givendate
while (@enddate >= @startdate)
begin
insert into #temp values (@startdate)
set @startdate = DATEADD(dd,1 ,@startdate)
End
--select * from #temp 


-- insert result value in table of inner join temp and policy customer list
CREATE TABLE #temp1(DateCompared datetime,CUSTOMER_ID int,POLICY_ID int, POLICY_VERSION_ID int,POLICY_EFFECTIVE_DATE datetime ,POLICY_EXPIRATION_DATE datetime, TOTAL_TERM  int ,ELAPSE_DAYS int,TIME_TO_ELAPSE decimal(25,0),TOTAL_PREMIUM decimal(25,2), COMPUTED_TIME decimal(25,2), PPNG decimal(25,2))
INSERT INTO #temp1 
SELECT DISTINCT tp.DateCompared, pc.CUSTOMER_ID, pc.POLICY_ID, pc.POLICY_VERSION_ID , 
	pc.POLICY_EFFECTIVE_DATE, pc.POLICY_EXPIRATION_DATE, 
	DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE), 
	DATEDIFF(DD,POLICY_EFFECTIVE_DATE,@givendate),
	(DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,@givendate),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE))),
	ap.TOTAL_PREMIUM,	
( CAST(DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,@givendate),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE)) AS DECIMAL(25,8)) / CAST(DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) AS DECIMAL(25,8)) ),
(ap.TOTAL_PREMIUM * ( CAST(DATEDIFF(dd,DATEDIFF(DD,POLICY_EFFECTIVE_DATE,@givendate),DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE)) AS DECIMAL(25,8)) / CAST(DATEDIFF(dd,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE) AS DECIMAL(25,8)) ))
FROM POL_CUSTOMER_POLICY_LIST pc
INNER JOIN #temp tp
ON tp.DateCompared >= POLICY_EFFECTIVE_DATE 
AND tp.DateCompared <= POLICY_EXPIRATION_DATE
INNER JOIN ACT_POLICY_INSTALL_PLAN_DATA ap
ON pc.POLICY_ID = ap.POLICY_ID 
AND pc.CUSTOMER_ID = ap.CUSTOMER_ID 
AND pc.POLICY_VERSION_ID = ap.POLICY_VERSION_ID 
WHERE pc.CUSTOMER_ID = 2727
ORDER BY tp.DateCompared 
select * from #temp1 
  
end 

GO

