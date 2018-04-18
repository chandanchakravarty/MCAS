IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCountByLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCountByLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[Proc_GetClaimCountByLOB]
(
	@As_on_date	datetime
) 
as
begin

declare @As_on_date_Prev datetime
set @As_on_date_Prev = cast(cast(datepart(month,@As_on_date) as varchar(5))+'/'+ cast(datepart(day,@As_on_date) as varchar(5))+'/'+ cast((datepart(year,@As_on_date)-1) as varchar(5))as datetime)

declare @from_date_YTD	datetime
declare @from_date_Prev_YTD datetime
set @from_date_YTD = cast('1/1/'+ cast(datepart(year,@As_on_date)as varchar(5))as datetime)
set @from_date_Prev_YTD = cast('1/1/' + cast((datepart(year,@As_on_date)-1) as varchar(5))as datetime)

declare @from_date_MTD	datetime
declare @from_date_Prev_MTD datetime
set @from_date_MTD = cast(datepart(month,@As_on_date) as varchar(5))+'/'+ cast('1' as varchar(5))+'/'+ cast(datepart(year,@As_on_date) as varchar(5))
set @from_date_Prev_MTD = cast(datepart(month,@As_on_date) as varchar(5))+'/'+ '1'+'/'+ cast((datepart(year,@As_on_date)-1) as varchar(5))

select MLM.LOB_DESC, A1.YTD, A2.MTD, A3.PrevYTD, A4.PrevMTD from clm_claim_info CCI 
left outer join (
	select CCI.lob_id, MLM.LOB_DESC, count(CCI.lob_id) as YTD from clm_claim_info CCI 
	inner join MNT_LOB_MASTER MLM
	on CCI.lob_id = MLM.lob_id
	--where CCI.created_datetime between cast(@from_date_YTD as datetime) and cast(@As_on_date as datetime)
	where cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) >= @from_date_YTD and cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) <= @As_on_date
	group by CCI.lob_id, MLM.LOB_DESC)
AS A1 on A1.lob_id = CCI.lob_id

left outer join (
	select CCI.lob_id, MLM.LOB_DESC, count(CCI.lob_id) as MTD from clm_claim_info CCI 
	inner join MNT_LOB_MASTER MLM
	on CCI.lob_id = MLM.lob_id
	--where CCI.created_datetime between  cast(@from_date_MTD as datetime) and cast(@As_on_date as datetime)
	where cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) >= @from_date_MTD and cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) <= @As_on_date
	group by CCI.lob_id, MLM.LOB_DESC)
AS A2 on A2.lob_id = CCI.lob_id

left outer join (
	select CCI.lob_id, MLM.LOB_DESC, count(CCI.lob_id) as PrevYTD from clm_claim_info CCI 
	inner join MNT_LOB_MASTER MLM
	on CCI.lob_id = MLM.lob_id
	--where CCI.created_datetime between cast(@from_date_Prev_YTD as datetime) and cast(@As_on_date_Prev as datetime)
	where cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) >= @from_date_Prev_YTD and cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) <= @As_on_date_Prev
	group by CCI.lob_id, MLM.LOB_DESC)
AS A3 on A3.lob_id = CCI.lob_id

left outer join (
	select CCI.lob_id, MLM.LOB_DESC, count(CCI.lob_id) as PrevMTD from clm_claim_info CCI 
	inner join MNT_LOB_MASTER MLM
	on CCI.lob_id = MLM.lob_id
	--where CCI.created_datetime between cast(@from_date_Prev_MTD as datetime) and cast(@As_on_date_Prev as datetime)
	where cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) >= @from_date_Prev_MTD and cast(CONVERT(varchar,CCI.created_datetime,101)as datetime) <= @As_on_date_Prev
	group by CCI.lob_id, MLM.LOB_DESC)
AS A4 on A4.lob_id = CCI.lob_id

inner join MNT_LOB_MASTER MLM
on CCI.lob_id = MLM.lob_id
group by CCI.LOB_id,MLM.LOB_DESC,A1.YTD,A2.MTD, A3.PrevYTD, A4.PrevMTD
order by MLM.LOB_DESC


end


GO

