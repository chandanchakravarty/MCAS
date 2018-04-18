---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
 /*----------------------------------------------------------    
Proc Name          : Dbo.PROC_GET_POL_ACCUMULATION_DETAILS    
Created by           : Ruchika Chauhan    
Date                    : 13-March-2012    
Purpose               : To fetch data from table POL_ACCUMULATION_DETAILS    
Revison History :    
Used In                :   Singapore Implementation    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  

CREATE PROC PROC_GET_POL_ACCUMULATION_DETAILS
(
@POLICY_ID INT,
 @POLICY_VERSION_ID INT,
 @CUSTOMER_ID INT
)
AS
BEGIN

SELECT A.ACC_REF_NO,A.ACCUMULATION_CODE,A.TOTAL_NO_OF_POLICIES,A.OWN_RETENTION_LIMIT,A.TREATY_CAPACITY_LIMIT,
A.ACCUMULATION_LIMIT_AVAILABLE,A.TOTAL_SUM_INSURED,A.FACULTATIVE_RI,A.GROSS_RETAINED_SUM_INSURED,A.OWN_RETENTION,
A.FIRST_SURPLUS,A.QUOTA_SHARE,A.OWN_ABSOLUTE_NET_RETENSTION, * 
FROM POL_ACCUMULATION_DETAILS A WHERE
A.POLICY_ID=@POLICY_ID AND A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND A.CUSTOMER_ID=@CUSTOMER_ID

END


--EXEC PROC_GET_POL_ACCUMULATION_DETAILS 520, 1, 28814


