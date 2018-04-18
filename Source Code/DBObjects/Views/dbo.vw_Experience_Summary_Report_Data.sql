IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_Experience_Summary_Report_Data]'))
DROP VIEW [dbo].[vw_Experience_Summary_Report_Data]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*-----------------------
object		: View
Created By	: Pravesh K chandel
Created date:28 April 2009
Purpose		: to fetch records for Experience Summary Report
Modified By		:Pravesh K Chandel
Modified date	:3 july 09
Purpose		: to fetch begining.ending,paid loss columns
drop view vw_Experience_Summary_Report_Data
---------------------------*/
create view vw_Experience_Summary_Report_Data
as

select aep.agency_id,aep.state_id,aep.customer_id,aep.policy_id,aep.version_for_risk as policy_version_id,clm.claim_id,
(select policy_lob from pol_customer_policy_list with(nolock)
where customer_id=aep.customer_id and policy_id=aep.policy_id 
	and current_term=aep.current_term
	and policy_version_id=aep.version_for_risk
) as  LOB_ID,
current_term,
APP_USE_VEHICLE_ID,
coverageid as Coverage_id,risk_id,risk_type,aep.month_number,aep.year_number,
(select count(distinct risk_id)
	from act_earned_premium a
	where customer_id=aep.customer_id and policy_id=aep.policy_id 
	and current_term=aep.current_term
	and coverageid=aep.coverageid
	and risk_type=aep.risk_type
	and year_number=aep.year_number
	and month_number=aep.month_number
	and 
		(select sum(earned_premium)
		from act_earned_premium b
		where customer_id=a.customer_id and policy_id=a.policy_id 
		and current_term=a.current_term
		and coverageid=a.coverageid
		and risk_type=a.risk_type
		and risk_id=a.risk_id
		and year_number=aep.year_number
		and month_number=aep.month_number
		)<>0
)Vehicle_Count,

(select count(distinct policy_number)
	from act_earned_premium a
	where customer_id=aep.customer_id and policy_id=aep.policy_id 
	and current_term=aep.current_term
	and coverageid=aep.coverageid
	and risk_type=aep.risk_type
	and year_number=aep.year_number
	and month_number=aep.month_number
	and 
		(select sum(earned_premium)
		from act_earned_premium b
		where customer_id=a.customer_id and policy_id=a.policy_id 
		and current_term=a.current_term
		and coverageid=a.coverageid
		and year_number=aep.year_number
		and month_number=aep.month_number
		)<>0

)POLICY_Count,

sum(written_premium) written_premium,
sum(inforce_premium) inforce_premium,
sum(beginning_unearned)beginning_unearned,
sum(ending_unearned) ending_unearned,
sum(earned_premium) earned_premium,

case when
(select sum(written_premium) from act_earned_premium 
where customer_id=aep.customer_id and policy_id=aep.policy_id 
and current_term=aep.current_term
and year_number=aep.year_number
and month_number=aep.month_number
) <> 0 then 1 else 0 end as Written_Exposure,

sum(case when unearned_factor_beg=0 then 1.00000 else unearned_factor_beg end -unearned_factor_end) as Earned_Exposure,
case when
(select sum(earned_premium)
	from act_earned_premium 
	where customer_id=aep.customer_id and policy_id=aep.policy_id 
	and current_term=aep.current_term
	and year_number=aep.year_number
	and month_number=aep.month_number
)>0 then 1 else 0 end 
as InForce_Exposure,
SUM(ISNULL(PLI.BEGIN_RESERVE,0))BEGIN_RESERVE,
SUM(ISNULL(PLI.END_RESERVE,0))END_RESERVE,
SUM(ISNULL(PLI.LOSSES_INCURRED,0))LOSSES_INCURRED,
ISNULL(SUM(ISNULL(PLI.LOSS_PAID,0)),0) LOSS_PAID,
(
   (SELECT COUNT(DISTINCT CLAIM_ID)
	FROM VW_LOSS_PAYMENT_SCHEDULE_P_SUMMUARY 
	WHERE COVERAGE_ID=AEP.COVERAGEID
	AND CLAIM_ID=CLM.CLAIM_ID
	and month_number=aep.month_number
	AND YEAR_NUMBER=AEP.YEAR_NUMBER
	AND (ISNULL(PAID_LOSS,0) + ISNULL(SALVAGE_LESS,0) + ISNULL(LESS_RECOVERY,0)) <>0 
   )
) PAID_CLAIMS,
(
SELECT ISNULL(SUM(ISNULL(LOSS_ADJUST_EXPENSE,0)),0) FROM
	VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY
	WHERE COVERAGE_ID=AEP.COVERAGEID
	AND CLAIM_ID=CLM.CLAIM_ID
	and month_number=aep.month_number
	AND YEAR_NUMBER=AEP.YEAR_NUMBER

)LOSS_ADJUST_EXPENSE


from act_earned_premium aep
left outer join pol_vehicles pv on pv.customer_id=aep.customer_id and pv.policy_id=aep.policy_id and pv.policy_version_id=aep.version_for_risk and pv.vehicle_id=aep.risk_id 

LEFT OUTER JOIN CLM_CLAIM_INFO  CLM WITH(NOLOCK) ON CLM.CUSTOMER_ID=AEP.CUSTOMER_ID 
AND CLM.POLICY_ID=AEP.POLICY_ID --AND CLM.POLICY_VERSION_ID=AEP.VERSION_FOR_RISK

LEFT OUTER JOIN VW_PAID_LOSS_INCURRED_BY_COVERAGE PLI
ON PLI.AGENCY_ID=aep.AGENCY_ID AND PLI.STATE_ID=aep.STATE_ID 
AND PLI.COVERAGE_ID=aep.COVERAGEID
AND PLI.MONTH_NUMBER=AEP.MONTH_NUMBER 
AND PLI.YEAR_NUMBER=AEP.YEAR_NUMBER
and PLI.CLAIM_ID=CLM.CLAIM_ID
AND PLI.POLICY_RISK_ID=AEP.RISK_ID

group by  aep.agency_id,aep.state_id,aep.customer_id,aep.policy_id,version_for_risk,clm.claim_id,APP_USE_VEHICLE_ID,current_term,coverageid,risk_id,risk_type,aep.month_number,aep.year_number






GO

