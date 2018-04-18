ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN PROCESS_STATUS	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN PARENT_APP_VERSION_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN STATE_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN DIV_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN DEPT_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN PC_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN TO_BE_AUTO_RENEWED	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN DWELLING_ID	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN CURRENT_TERM	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN PREFERENCE_DAY	INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN POLICY_VERIFY_DIGIT	INT

ALTER TABLE MNT_REINSURANCE_CONTRACT ADD MAX_NO_INSTALLMENT INT
ALTER TABLE MNT_REINSURANCE_CONTRACT ADD RI_CONTRACTUAL_DEDUCTIBLE DECIMAL
ALTER TABLE POL_REINSURANCE_INFO ADD MAX_NO_INSTALLMENT INT
ALTER TABLE POL_CUSTOMER_POLICY_LIST ALTER COLUMN AGENCY_ID INT


ALTER TABLE POL_CUSTOMER_POLICY_LIST ADD DISREGARD_RI_CONTRACT INT
ALTER TABLE POL_REINSURANCE_INFO ADD LAYER_AMOUNT DECIMAL(18,2)
ALTER TABLE POL_REINSURANCE_INFO ADD RISK_ID INT

