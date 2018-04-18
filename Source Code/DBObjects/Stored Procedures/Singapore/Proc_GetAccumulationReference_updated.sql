  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
 /*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetAccumulationReference    
Created by           : Ruchika Chauhan    
Date                    : 5-March-2012    
Purpose               : To retrieve accumulation data corresponding to the Accumulation ID 
Revison History :    
Used In                :   Singapore Implementation    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetAccumulationReference  
 (  
 @Acc_id int,  
 @Acc_Ref nvarchar(25),  
 @TOTAL_POLICIES INT OUTPUT,
 @TotSumInsured INT OUTPUT,
 @AvailableLimit INT OUTPUT,
 @POLICY_ID INT,
 @POLICY_VERSION_ID INT,
 @CUSTOMER_ID INT
   
 )  
AS    
BEGIN    

 DECLARE @Used_Limit int,
 @REIN_CEDED int,
 @OwnRetentionLimit int,
 @OwnRetention int,
 @OwnAbsNetRetention int
 
    
SELECT @Used_Limit= sum(TOTAL_SUM_INSURED)from POL_ACCUMULATION_DETAILS where ACC_REF_NO=@Acc_Ref  

 
SELECT @TOTAL_POLICIES=COUNT(DISTINCT A.POLICY_ID) FROM POL_ACCUMULATION_DETAILS A INNER JOIN POL_POLICY_PROCESS B ON A.POLICY_ID=B.POLICY_ID AND A.POLICY_VERSION_ID=B.POLICY_VERSION_ID and A.CUSTOMER_ID=B.CUSTOMER_ID 
WHERE A.ACC_REF_NO=@Acc_Ref and B.PROCESS_ID in(25, 14, 18)

 
 
select PAD.CUSTOMER_ID,PAD.POLICY_ID,PAD.POLICY_VERSION_ID,MAR.TREATY_CAPACITY_LIMIT,MAR.CRITERIA_VALUE,(MAR.TREATY_CAPACITY_LIMIT - @Used_Limit) AS 'AVAILABLE_LIMIT' from POL_ACCUMULATION_DETAILS PAD INNER JOIN  
MNT_ACCUMULATION_REFERENCE MAR  ON PAD.ACC_REF_NO=MAR.ACC_REF_NO where PAD.ACC_REF_NO=@Acc_Ref  
   
   
--Query to calculate the Total Sum Insured   
select @TotSumInsured=SUM(Cov.LIMIT_1) from POL_DWELLING_SECTION_COVERAGES Cov 
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID	
	
	
--Calculate Accumulation Limit Available
SELECT @OwnRetentionLimit=CRITERIA_VALUE FROM MNT_ACCUMULATION_REFERENCE WHERE ACC_REF_NO=@Acc_Ref
SELECT @OwnRetention=ISNULL(SUM(OWN_RETENTION),0) FROM POL_ACCUMULATION_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=(SELECT MAX(POLICY_VERSION_ID) from POL_ACCUMULATION_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID AND ACC_REF_NO=@Acc_Ref) and POLICY_ID=@POLICY_ID AND ACC_REF_NO=@Acc_Ref
SELECT @OwnAbsNetRetention=ISNULL(SUM(OWN_ABSOLUTE_NET_RETENSTION),0)FROM POL_ACCUMULATION_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=(SELECT MAX(POLICY_VERSION_ID) from POL_ACCUMULATION_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID AND ACC_REF_NO=@Acc_Ref) and POLICY_ID=@POLICY_ID	AND ACC_REF_NO=@Acc_Ref
SELECT @AvailableLimit=	ISNULL((@ownRetentionLimit - @OwnRetention - @OwnAbsNetRetention),0)
	
END    
  
  
  
--DROP proc Proc_GetAccumulationReference  
  