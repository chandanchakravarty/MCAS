/*----------------------------------------------------------                                                    
 Proc Name       : [dbo].[SP_INSERT_MANAGEMENT_REPORT]                         
 Created by      : praveer panghal           
 Date            : 29 Aug 2011                                  
 Purpose         : Stored procedure that insert the data  of a specific period.  
                                                  
 Revison History :                                                    
 Used In       : EbixAdvanatge          '  
           
 drop proc [dbo].[SP_INSERT_MANAGEMENT_REPORT] '01/01/2011','07/09/2011'  
------   ------------       -------------------------*/               
          
-- alter table Rodolfo_spPolicy add  rod_cd_endorsement varchar(6)     
-- alter table Rodolfo_spInstallments add rod_cd_endorsement varchar(6)  
-- alter table Rodolfo_spBrokerInstallments add rod_cd_endorsement varchar(6)    
alter PROC [dbo].[SP_INSERT_MANAGEMENT_REPORT]  
(          
@EFFECTIVE_START_DATE DATETIME =NULL,          
@EFFECTIVE_END_DATE DATETIME =NULL ,  
@DIV_CODE NVARCHAR(6)=NULL,            
@SUSEP_LOB_CODE NVARCHAR(20)=NULL,            
@POLICY_NUMBER NVARCHAR(75)=NULL,            
@AGENCY_CODE INT=NULL ,   
@CLAIM_NUMBER NVARCHAR(10)=NULL,     
@ORDER_BY INT=NULL            
       
)          
AS           
BEGIN         
DELETE FROM Rodolfo_spPolicy  
INSERT  INTO  Rodolfo_spPolicy  
(  
rod_cd_division,  
rod_de_division,  
rod_de_stateCodeDivision,  
rod_cd_susepLOB,  
rod_de_susepLOBDescription,  
rod_id_policy,  
rod_cd_endorsement,  
rod_cd_leaderBroker,  
rod_dt_issuanceDate,  
rod_de_coInsuranceType,  
rod_vl_issuedRiskPremium,  
rod_vl_coinsurancePremium,  
rod_vl_reinsurancePremium,  
rod_vl_reisuranceComission,  
rod_vl_interest,  
rod_vl_policyFee,  
rod_vl_taxes,  
rod_vl_brokerComission,  
rod_vl_admExpense,  
rod_vl_discounts,  
rod_vl_surcharges  
)  
EXEC SP_POLICY @EFFECTIVE_START_DATE,@EFFECTIVE_END_DATE ,@DIV_CODE,@SUSEP_LOB_CODE,@POLICY_NUMBER,@AGENCY_CODE  
     ,@ORDER_BY  
--SELECT * FROM Rodolfo_spPolicy  
------------------------------------------------------------------------------------------------------------------  
DELETE FROM Rodolfo_vwBroker  
INSERT  INTO  Rodolfo_vwBroker  
(  
rod_cd_division,  
rod_cd_susepLOB,  
rod_id_policy,  
rod_cd_endorsement,  
rod_cd_broker,   
rod_de_brokerDescription,  
rod_vl_brokerCommission  
)   
SELECT * FROM   VIW_BROKER    
--SELECT * FROM Rodolfo_vwBroker  
  
  
-----------------------------------------------------------------------------------------------------------------  
DELETE FROM Rodolfo_spInstallments  
INSERT  INTO Rodolfo_spInstallments  
(  
rod_cd_division,  
rod_cd_susepLOB,  
rod_id_policy,  
rod_cd_endorsement,  
rod_cd_installments,  
rod_dt_issuanceInstallments,  
rod_dt_paymentInstallments,  
rod_dt_cancelationInstallments,  
rod_vl_riskPremiumInstallments,  
rod_vl_coinsuranceInstallmentsPremium,  
rod_vl_reinuranceInstallmentsPremium,  
rod_vl_reinsuranceCommisionInstallments,  
rod_vl_interestInstallments,  
rod_vl_policyCostInstallments,  
rod_vl_taxes,  
rod_vl_brokerCommissionInstalments,  
rod_vl_admExpensaesInstallments  
)  
EXEC SP_INSTALLMENTS @EFFECTIVE_START_DATE,@EFFECTIVE_END_DATE ,@DIV_CODE,@SUSEP_LOB_CODE,@POLICY_NUMBER,@ORDER_BY  
--SELECT * FROM Rodolfo_spInstallments  
---------------------------------------------------------------------------------------------------------------  
DELETE FROM Rodolfo_spBrokerInstallments  
INSERT  INTO Rodolfo_spBrokerInstallments   
(  
rod_cd_division,  
rod_cd_susepLOB,  
rod_id_policy,  
rod_cd_endorsement,  
rod_cd_broker,  
rod_cd_installments,  
rod_vl_premiumInstallments,  
rod_vl_coinsuranceInstallmentsPremium,  
rod_vl_reinuranceInstallmentsPremium,  
rod_vl_reinsuranceCommisionInstallments,  
rod_vl_interestInstallments,  
rod_vl_policyCostInstallments,  
rod_vl_taxes,  
rod_vl_brokerCommissionInstalments,  
rod_vl_admExpensaesInstallments  
)  
EXEC SP_BROKER_INSTALLMENTS  @EFFECTIVE_START_DATE,@EFFECTIVE_END_DATE ,@DIV_CODE,@SUSEP_LOB_CODE,@POLICY_NUMBER  
,@AGENCY_CODE ,@ORDER_BY  
--SELECT * FROM Rodolfo_spBrokerInstallments  
----------------------------------------------------------------------------------------------------------------  
  
DELETE FROM Rodolfo_spClaim  
INSERT INTO Rodolfo_spClaim  
 (  
 rod_cd_division,  
 rod_cd_susepLOB,  
 rod_id_policy,  
 rod_id_claim,  
 rod_cd_endorsement  
,rod_vl_totalReservePending,  
rod_vl_totalReservePaid,  
rod_vl_reservePendingInsurer  
,rod_vl_reservePendingCoInsurer,  
rod_vl_reservePendingReisurer,  
rod_vl_reservePaidInsurer  
,rod_vl_reservePaidCoInsurer,  
rod_vl_reservePaidReinsurer,  
rod_vl_salvageSubrogation  
)  
EXEC SP_CLAIM @EFFECTIVE_START_DATE,@EFFECTIVE_END_DATE ,@DIV_CODE,@SUSEP_LOB_CODE,@POLICY_NUMBER  
      ,@CLAIM_NUMBER ,@ORDER_BY  
--SELECT * FROM Rodolfo_spClaim  
  
  
-----------------------------------------------------------------------------------------------------------------  
DELETE FROM Rodolfo_spClaimDetails  
INSERT INTO Rodolfo_spClaimDetails  
(  
rod_id_claim,  
rod_cd_endorsement,  
rod_dt_payment,  
rod_vl_totalPaid,  
rod_vl_totalPaidCoInsurer,  
rod_vl_totalPaidReinsurer,  
rod_vl_totalPaidInsurer  
)  
 EXEC SP_CLAIM_DETAILS @EFFECTIVE_START_DATE,@EFFECTIVE_END_DATE ,@CLAIM_NUMBER  
--SELECT * FROM Rodolfo_spClaimDetails  
  
------------------------------------------------------------------------------------------------------------------  
  
                      
END  