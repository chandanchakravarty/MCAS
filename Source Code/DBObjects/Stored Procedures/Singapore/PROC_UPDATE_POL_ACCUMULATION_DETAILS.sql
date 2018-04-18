---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
 /*----------------------------------------------------------    
Proc Name          : Dbo.PROC_UPDATE_POL_ACCUMULATION_DETAILS    
Created by           : Ruchika Chauhan    
Date                    : 13-March-2012    
Purpose               : To update table POL_ACCUMULATION_DETAILS after policy commit.
Revison History :    
Used In                :   Singapore Implementation    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  

CREATE PROC PROC_UPDATE_POL_ACCUMULATION_DETAILS
(  
 @POLICY_ID INT,
 @POLICY_VERSION_ID INT,
 @CUSTOMER_ID INT  
)
AS
BEGIN 
		
	DECLARE @TotSumInsured INT,
	@Acc_id INT,
	@Acc_Ref_No NVARCHAR(20),
	@GrossRetainedSumInsured INT,
	@OwnAbsNetRetention INT,
	@FACULTATIVE_RI INT,
	@OWN_RETENTION INT,
	@QUOTE_SHARE INT,
	@FIRST_SURPLUS INT,
	@TotalNoOfPolicies INT,
	@OwnRetained INT,
	@OwnRetentionLimit INT,
	@AccumulationLimitAvailable INT
		
		
	SELECT @Acc_id=ACCUMULATION_ID from POL_ACCUMULATION_DETAILS where POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and
	CUSTOMER_ID=@CUSTOMER_ID
	
	SELECT @Acc_Ref_No=ACC_REF_NO FROM POL_ACCUMULATION_DETAILS WHERE ACCUMULATION_ID=@Acc_id
	------------------------------------------------------------------------------------------------------------------
	--Calculate FACULTATIVE RI AMOUNT
	SELECT @FACULTATIVE_RI = REIN_PREMIUM FROM POL_REINSURANCE_BREAKDOWN_DETAILS WHERE POLICY_ID=@POLICY_ID 
	and CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and
	CONTRACT_NUMBER ='Facultative'-- FACULTATIVE RI
	-----------------------------------------------------------------------------------------------------------------
	
	SELECT @TotSumInsured = TOTAL_SUM_INSURED FROM POL_ACCUMULATION_DETAILS
	WHERE ACCUMULATION_ID=@Acc_id
	
	SET @OWN_RETENTION = @TotSumInsured - @FACULTATIVE_RI	
		
	-----------------------------------------------------------------------------------------------------------------
	--Calculate QUOTA SHARE AMOUNT
	select @QUOTE_SHARE=sum(REIN_PREMIUM) from POL_REINSURANCE_BREAKDOWN_DETAILS where POLICY_ID=@POLICY_ID 
	and CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and
	CONTRACT_NUMBER in (select CONTRACT_NUMBER from MNT_REINSURANCE_CONTRACT where CONTRACT_TYPE=39)-- QUOTA SHARE
	-----------------------------------------------------------------------------------------------------------------	
	--Calculate 1ST SURPLUS AMOUNT
	select @FIRST_SURPLUS=sum(REIN_PREMIUM) from POL_REINSURANCE_BREAKDOWN_DETAILS where POLICY_ID=@POLICY_ID 
	and CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and
	CONTRACT_NUMBER in (select CONTRACT_NUMBER from MNT_REINSURANCE_CONTRACT where CONTRACT_TYPE=40)-- 1ST SURPLUS
	-----------------------------------------------------------------------------------------------------------------
	
	
	SET @GrossRetainedSumInsured = ISNULL((@TotSumInsured - @FACULTATIVE_RI),0)
	
	SET @OwnAbsNetRetention = ISNULL((@GrossRetainedSumInsured - @QUOTE_SHARE - @FIRST_SURPLUS),0)
	
	--Incrementing the Total_No_Of_Policies by 1, to be updated after policy commit
	SELECT @TotalNoOfPolicies = TOTAL_NO_OF_POLICIES  FROM POL_ACCUMULATION_DETAILS WHERE POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CUSTOMER_ID = @CUSTOMER_ID
	
	
	-----------------------------------------------------------------------------------------------------------------
	--Calculating the value of ACCUMULATION_AVAILABLE_LIMIT to be updated after policy commit
	
	SELECT @OwnRetained=SUM(OWN_RETENTION) FROM POL_ACCUMULATION_DETAILS
	WHERE ACC_REF_NO=@Acc_Ref_No AND POLICY_VERSION_ID IN (SELECT MAX(POLICY_VERSION_ID)FROM POL_ACCUMULATION_DETAILS 
	WHERE ACC_REF_NO =@Acc_Ref_No AND POLICY_ID <> @POLICY_ID and POLICY_ID in (select distinct POLICY_ID from POL_ACCUMULATION_DETAILS  where ACC_REF_NO=@Acc_Ref_No))
	
	SELECT @OwnRetentionLimit=OWN_RETENTION_LIMIT FROM POL_ACCUMULATION_DETAILS WHERE POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CUSTOMER_ID=@CUSTOMER_ID
	
	SET @AccumulationLimitAvailable=ISNULL((@OwnRetentionLimit - @OwnRetained - @OwnAbsNetRetention),0)
	-----------------------------------------------------------------------------------------------------------------	
	--UPDATING TABLE POL_ACCUMULATION_DETAILS
	
	UPDATE POL_ACCUMULATION_DETAILS
	SET FACULTATIVE_RI=@FACULTATIVE_RI, OWN_RETENTION = @OWN_RETENTION, QUOTA_SHARE = @QUOTE_SHARE, 
	FIRST_SURPLUS=@FIRST_SURPLUS, GROSS_RETAINED_SUM_INSURED=@GrossRetainedSumInsured,
	OWN_ABSOLUTE_NET_RETENSTION=@OwnAbsNetRetention, TOTAL_NO_OF_POLICIES=@TotalNoOfPolicies + 1,
	ACCUMULATION_LIMIT_AVAILABLE=@AccumulationLimitAvailable
	WHERE POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CUSTOMER_ID = @CUSTOMER_ID 
	AND ACCUMULATION_ID=@Acc_id
	-----------------------------------------------------------------------------------------------------------------	
END