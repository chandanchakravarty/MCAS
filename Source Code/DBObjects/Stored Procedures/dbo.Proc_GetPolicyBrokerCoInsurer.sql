IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyBrokerCoInsurer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyBrokerCoInsurer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------    
 Crated By  : Pravesh K Chandel    
 Created On : 21 Dec 2010    
 Purpose : Fetch Policy Broker and Co Insurer
  -- Proc_GetPolicyBrokerCoInsurer 2126,752,1               
 ------------------------------------------------------*/    
  --DROP PROC Proc_GetPolicyBrokerCoInsurer
CREATE PROCEDURE [dbo].[Proc_GetPolicyBrokerCoInsurer]
(                                                  
 @CUSTOMER_ID   int,                                                  
 @POLICY_ID                int,                                                  
 @POLICY_VERSION_ID   int,                                                  
 @LANG_ID INT=NULL
)                                                  
AS      
BEGIN        

 DECLARE @AGENCY_CODE VARCHAR(10)
 DECLARE @END_CO_APPLICANT_ID int,@PRIMARY_CO_APPLICANT_ID int
 DECLARE @LOB_ID int,@TRANSACTION_TYPE INT,@PRODUCT_TYPE INT,@PROCESS_ID int
 
--PRODUCT TYPES
--14680	Master Policy
--14681	Simple Policy

--TRANSATION TYPES
--14559	Single Policy
--14560	Open Policy
--14561	Adjustable Policy
--14679	Fronting Premium Policy

  SELECT @AGENCY_CODE	= ISNULL(AGENCY_CODE,''),
  @TRANSACTION_TYPE		= TRANSACTION_TYPE,
  @LOB_ID				= CAST(POLICY_LOB AS INT),
  @PRODUCT_TYPE			= LOB.PRODUCT_TYPE
  FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)
  JOIN MNT_AGENCY_LIST AG (NOLOCK) ON POL.AGENCY_ID=AG.AGENCY_ID
  JOIN MNT_LOB_MASTER LOB (NOLOCK) ON POL.POLICY_LOB=CONVERT(VARCHAR,LOB.LOB_ID)
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
  
  
  select 
  @END_CO_APPLICANT_ID = CO_APPLICANT_ID,
  @PROCESS_ID		   = PROCESS_ID
  from POL_POLICY_PROCESS with(nolock) 
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID
  AND PROCESS_STATUS<>'ROLLBACK'
  -- GETTING PRIMARY APPLICANT ID FOR NBS 
  SELECT @PRIMARY_CO_APPLICANT_ID = APPLICANT_ID
  FROM POL_APPLICANT_LIST  WITH(NOLOCK) 
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
  AND IS_PRIMARY_APPLICANT = 1
    
 -- co-applicant list Table 0
  SELECT DISTINCT CO_APPLICANT_ID 
  FROM POL_REMUNERATION (nolock)
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
--  AND COMMISSION_TYPE ='43' -- ONLY FOR COMMISSION BROKER NOT FOR EN FEE AND PROLABORE
  AND IS_ACTIVE='Y'
  AND CO_APPLICANT_ID = CASE 
						WHEN @PROCESS_ID in (3,14) 
							AND @PRODUCT_TYPE = 14680 
							AND @TRANSACTION_TYPE = 14560 
							THEN @END_CO_APPLICANT_ID 
						WHEN @PROCESS_ID in (24,25)  -- ADDED AS PER iTRACK 1270 nOTE PART - DOC ONLY FOR pRIMARY aPPLICANT
							AND @LOB_ID in (21,22,34)
							THEN ISNULL(@PRIMARY_CO_APPLICANT_ID,CO_APPLICANT_ID)
						ELSE CO_APPLICANT_ID END
  
-- broker list Table 1
  IF (@PROCESS_ID in (3,14) AND  @TRANSACTION_TYPE = 14560  ) ---for master policy select co-applicant leader  broker
	  SELECT DISTINCT BROKER_ID					
	  FROM POL_REMUNERATION (nolock)
	  WHERE CUSTOMER_ID=@CUSTOMER_ID 
	  AND POLICY_ID=@POLICY_ID
	  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
	  AND COMMISSION_TYPE ='43' -- ONLY FOR COMMISSION BROKER NOT FOR EN FEE AND PROLABORE
	  AND LEADER= 10963 --generate Policy Doc only for Leader Broker as per Itrack 964 NOte# 20 added on 20/04/2011
	  AND IS_ACTIVE='Y'
	  AND CO_APPLICANT_ID = @END_CO_APPLICANT_ID
  ELSE
	  SELECT DISTINCT BROKER_ID 
	  FROM POL_REMUNERATION (nolock)
	  WHERE CUSTOMER_ID=@CUSTOMER_ID 
	  AND POLICY_ID=@POLICY_ID
	  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
	  AND COMMISSION_TYPE ='43' -- ONLY FOR COMMISSION BROKER NOT FOR EN FEE AND PROLABORE
	  AND LEADER= 10963 --generate Policy Doc only for Leader Broker as per Itrack 964 NOte# 20 added on 20/04/2011
	  AND IS_ACTIVE='Y'
  
  -- Co-Insurance List Table 2
  SELECT Distinct COMPANY_ID,LEADER_FOLLOWER
  FROM POL_CO_INSURANCE (nolock)
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
  --AND LEADER_FOLLOWER =
  AND IS_ACTIVE='Y'
  -- CONDITION ADDED AS CARRIER TABLE IS BEING FETCHED IN TABLE 3
  AND COMPANY_ID <>(
				   SELECT SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS (nolock)
				   )
  
  --carrier and Agency Info
 
  -- table 3
  SELECT REIN_COMAPANY_CODE AS  CARRIER_CODE,@AGENCY_CODE AS AGENCY_CODE,REIN_COMAPANY_ID as COMPANY_ID 
  FROM  MNT_REIN_COMAPANY_LIST  (nolock)
  WHERE 
  REIN_COMAPANY_ID = (
				SELECT SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS (nolock)
				)
  
  -- ReInsurer List table 4 --itrack 964 not # 20
  SELECT Distinct MAJOR_PARTICIPANT as COMPANY_ID
  FROM POL_REINSURANCE_BREAKDOWN_DETAILS (nolock)
  WHERE CUSTOMER_ID=@CUSTOMER_ID 
  AND POLICY_ID=@POLICY_ID
  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
 
  
END 


GO

