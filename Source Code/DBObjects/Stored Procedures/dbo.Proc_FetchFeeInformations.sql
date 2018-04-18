IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchFeeInformations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchFeeInformations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* -----------------------------------------------------------------------------------------------                        
Proc Name             : Dbo.Proc_FetchFeeInformations                          
Created by            : Ashwani                         
Date                  :                           
Purpose               : 19 June, 2006                         
Revison History       :                          
modified by	      : Pravesh K Chandel
modified date	      : 17 may 2007
purpose		      : IS_TEMP_ENTRY is nchar and it was compare to 1 (numeric) change the compare to '1'

modified by		: Praveen kasana
modified date		: 14 nov 2007
purpose			: Date recd will show data only when amount is fully recd or partially recd
Used In               : Wolverine         
Used In               : Wolverine   

modified by		: Praveen kasana
modified date		: 30 May 2008
purpose			: Undo Fee Charge 
Used In               	: Wolverine              
                
exec Proc_FetchFeeInformations  W1002736,null,null,'ALL'
---------------------------------------------------------------------------------------------------                        
Date     Review By          Comments                          
------   ------------       ------------------------- ---------------------------------------------*/        
-- drop proc dbo.Proc_FetchFeeInformations                                             
create PROCEDURE [dbo].[Proc_FetchFeeInformations]                         
(                        
 @POLICYNO VARCHAR(15),                        
 @FROMDATE varchar(30),                        
 @TODATE varchar(30),                        
 @FEETYPE varchar(15)                        
)                        
AS                          
BEGIN             
IF (@FEETYPE='ALL')
BEGIN
	SELECT DISTINCT OI.IDEN_ROW_ID, isnull(CLT.CUSTOMER_FIRST_NAME,'')+ '  ' +isnull(CLT.CUSTOMER_MIDDLE_NAME,'') +                                                                          
	'  ' + isnull(CLT.CUSTOMER_LAST_NAME,'') + '/' +PCL.POLICY_NUMBER AS POLICY_NUMBER,                      
	CASE ITEM_TRAN_CODE 
		WHEN 'INSF' THEN 'Installment' 
		WHEN 'RENSF' THEN 'Reinstatement' 
		WHEN 'NSFF' THEN 'NSF Fees' 
		WHEN 'LF' THEN 'Late Fees'  
	END AS FEES_TYPE,  
	ITEM_TRAN_CODE AS ITEM_CODE,                      
-- 	CASE ITEM_TRAN_CODE 
-- 		WHEN 'INSF' THEN CONVERT(char,SOURCE_EFF_DATE,101) 
-- 		WHEN 'RNSF' THEN convert(char,SOURCE_TRAN_DATE,101) 
-- 		WHEN 'NSFF' THEN convert(char,SOURCE_TRAN_DATE,101) 
-- 		WHEN 'LF' THEN convert(char,SOURCE_TRAN_DATE,101)  
-- 	END AS DUE_DATE,  
	--Done for Itrack Issue 6799 on 4 Dec 09         
	OI.DUE_DATE AS DUE_DATE1, 
	CONVERT(char,OI.DUE_DATE,101) AS DUE_DATE,
	CASE  
		WHEN ISNULL(TOTAL_DUE,0)-ISNULL(TOTAL_PAID,0)=0 THEN 'Fully Recd'                      
		WHEN ISNULL(TOTAL_DUE,0)>ISNULL(TOTAL_PAID,0) AND  ISNULL(TOTAL_PAID,0)<>0 THEN 'Partially Recd'                      
		WHEN ISNULL(TOTAL_PAID,0) =0 THEN 'Not Recd'
	END AS FEES_STATUS, 
	(
		SELECT ISNULL(CONVERT(VARCHAR,MAX(DATE_COMMITTED)) ,'')
		FROM  ACT_RECONCILIATION_GROUPS 
		WHERE GROUP_ID IN (	
				SELECT GROUP_ID FROM ACT_CUSTOMER_RECON_GROUP_DETAILS 
				WHERE ITEM_REFERENCE_ID = OI.IDEN_ROW_ID
				)
					
	) AS DATE_RECD,
	
	CASE 
		WHEN (SELECT SUM(FEES_REVERSE) FROM ACT_FEE_REVERSAL 
			WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1 ) IS NULL
			THEN ISNULL(OI.TOTAL_DUE,0) 
		ELSE (SELECT MAX(FEES_AMOUNT) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
			AND ISNULL(IS_COMMITTED,0)=1
		)   
	END 	AS FEES_AMOUNT,
        ISNULL(TOTAL_PAID,0) AS FEES_AMT_RECD,
	
	CASE 
		WHEN (SELECT SUM(FEES_REVERSE) FROM ACT_FEE_REVERSAL 
			WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1) IS NULL
			THEN ISNULL(OI.TOTAL_DUE,0) 
		ELSE
		( (SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
			AND ISNULL(IS_COMMITTED,0)=1)   -
			(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) FROM ACT_FEE_REVERSAL 
 			WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1 )
		)
			
	END AS MAX_REVERSE,
	ITEM_TRAN_CODE,OI.CUSTOMER_ID,OI.POLICY_ID,OI.POLICY_VERSION_ID,
	--AFR.IDEN_ROW_ID AFR_IDEN_ROW_ID,
	NULL AS  AFR_IDEN_ROW_ID,
	TRANS_DESC  ,    
	(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) 
	 FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
	 AND ISNULL(IS_COMMITTED,0)=1 )AS TOTAL_REVERSED ,

	ISNULL(OI.TOTAL_DUE,0) AS TOTAL_DUE,
	ISNULL(PD.INSTALLMENT_FEES,0) AS INSTALLMENT_FEES
	
	FROM ACT_CUSTOMER_OPEN_ITEMS OI                      
	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL 
		ON OI.CUSTOMER_ID = PCL.CUSTOMER_ID AND                      
		PCL.POLICY_ID = OI.POLICY_ID AND                      
		PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID                       
	LEFT JOIN CLT_CUSTOMER_LIST CLT 
		ON OI.CUSTOMER_ID = CLT.CUSTOMER_ID                    
	LEFT JOIN ACT_FEE_REVERSAL AFR 
		ON   OI.CUSTOMER_ID = AFR.CUSTOMER_ID 
		AND  AFR.POLICY_ID = OI.POLICY_ID                   
		AND  AFR.POLICY_VERSION_ID = OI.POLICY_VERSION_ID  
		AND OI.IDEN_ROW_ID = AFR.CUSTOMER_OPEN_ITEM_ID     
	LEFT JOIN ACT_INSTALL_PLAN_DETAIL PD
		ON PD.IDEN_PLAN_ID = PCL.INSTALL_PLAN_ID                 
--		AND isnull(IS_COMMITTED,0)=0     
	WHERE  (PCL.POLICY_NUMBER=@POLICYNO or @POLICYNO is null)                
	AND ISNULL(IS_TEMP_ENTRY,'0') <> '1'    
	AND (CAST(CONVERT(VARCHAR,SOURCE_EFF_DATE,101) AS DATETIME) >= CAST(CONVERT(VARCHAR,@FROMDATE,101) AS DATETIME) or @FROMDATE is null) 
	AND (CAST(CONVERT(VARCHAR,SOURCE_EFF_DATE,101) AS DATETIME) <= CAST(CONVERT(VARCHAR,@TODATE,101) AS DATETIME) or @TODATE is null) 
	AND (ITEM_TRAN_CODE IN ('INSF', 'RENSF', 'NSFF', 'LF'))
	AND ISNULL(OI.TOTAL_DUE,0) >= 0 
	--Commented on 30 May 2008 : As we are Displaying all record
	/*AND (
		(SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL 
		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID )    =  0)
	    	OR
		(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) FROM ACT_FEE_REVERSAL 
 		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=0 )   --1 to 0
		<
		(SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL 
		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID)
		)*/ 
END
ELSE
BEGIN 
	SELECT DISTINCT OI.IDEN_ROW_ID, isnull(CLT.CUSTOMER_FIRST_NAME,'')+ '  ' +isnull(CLT.CUSTOMER_MIDDLE_NAME,'') +                                                                          
	'  ' + isnull(CLT.CUSTOMER_LAST_NAME,'') + '/' +PCL.POLICY_NUMBER AS POLICY_NUMBER,                      
	CASE ITEM_TRAN_CODE 
		WHEN 'INSF' THEN 'Installment' 
		WHEN 'RENSF' THEN 'Reinstatement' 
		WHEN 'NSFF' THEN 'NSF Fees' 
		WHEN 'LF' THEN 'Late Fees'  
	END AS FEES_TYPE,                      
	ITEM_TRAN_CODE AS ITEM_CODE,                      
-- 	CASE ITEM_TRAN_CODE 
-- 		WHEN 'INSF' THEN convert(char,SOURCE_EFF_DATE,101)  
-- 		WHEN 'RNSF' THEN convert(char,SOURCE_TRAN_DATE,101) 
-- 		WHEN 'NSFF' THEN convert(char,SOURCE_TRAN_DATE,101) 
-- 		WHEN 'LF' THEN convert(char,SOURCE_TRAN_DATE,101)   
-- 	END AS DUE_DATE,      
	--Done for Itrack Issue 6799 on 4 Dec 09         
	OI.DUE_DATE AS DUE_DATE1, 
	CONVERT(char,OI.DUE_DATE,101) AS DUE_DATE,     
	--OI.DUE_DATE AS DUE_DATE,	CASE  
		WHEN ISNULL(TOTAL_DUE,0)-ISNULL(TOTAL_PAID,0)=0 THEN 'Fully Recd'                      
		WHEN ISNULL(TOTAL_DUE,0)>ISNULL(TOTAL_PAID,0) AND  ISNULL(TOTAL_PAID,0)<>0 THEN 'Partially Recd'                      
		WHEN ISNULL(TOTAL_PAID,0) =0 THEN 'Not Recd'
	END AS FEES_STATUS, 
	(
		SELECT ISNULL(CONVERT(VARCHAR,MAX(DATE_COMMITTED)) ,'')
		FROM  ACT_RECONCILIATION_GROUPS 
		WHERE GROUP_ID IN (	
				SELECT GROUP_ID FROM ACT_CUSTOMER_RECON_GROUP_DETAILS 
				WHERE ITEM_REFERENCE_ID = OI.IDEN_ROW_ID
				)
					
	) AS DATE_RECD,                   
	CASE 
		WHEN (SELECT SUM(FEES_REVERSE) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1) 
			IS NULL THEN ISNULL(OI.TOTAL_DUE,0) 
		ELSE (SELECT MAX(FEES_AMOUNT) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
			AND ISNULL(IS_COMMITTED,0)=1)   
	END AS FEES_AMOUNT,
	ISNULL(TOTAL_PAID,0) AS FEES_AMT_RECD,

	CASE 
		WHEN (SELECT SUM(FEES_REVERSE) FROM ACT_FEE_REVERSAL 
			WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1) IS NULL
			THEN ISNULL(OI.TOTAL_DUE,0) 
		ELSE
		( (SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
			AND ISNULL(IS_COMMITTED,0)=1)   -
			(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) FROM ACT_FEE_REVERSAL 
 			WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1 )
		)
			
	END AS MAX_REVERSE,

	ITEM_TRAN_CODE,               
	OI.CUSTOMER_ID,OI.POLICY_ID,OI.POLICY_VERSION_ID,
	-- AFR.FEES_REVERSE,
	--AFR.IDEN_ROW_ID AFR_IDEN_ROW_ID,
	NULL AS  AFR_IDEN_ROW_ID,
	TRANS_DESC  ,    
	(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) FROM ACT_FEE_REVERSAL WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID    
	AND ISNULL(IS_COMMITTED,0)=1 )AS TOTAL_REVERSED ,

	ISNULL(OI.TOTAL_DUE,0) AS TOTAL_DUE,
	ISNULL(PD.INSTALLMENT_FEES,0) AS INSTALLMENT_FEES 
	
	FROM ACT_CUSTOMER_OPEN_ITEMS OI                      
	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL 
		ON OI.CUSTOMER_ID = PCL.CUSTOMER_ID 
		AND PCL.POLICY_ID = OI.POLICY_ID AND   
		PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID                       
	LEFT JOIN CLT_CUSTOMER_LIST CLT 
		ON OI.CUSTOMER_ID = CLT.CUSTOMER_ID         
	LEFT JOIN ACT_FEE_REVERSAL AFR 
		ON   OI.CUSTOMER_ID = AFR.CUSTOMER_ID 
		AND  AFR.POLICY_ID = OI.POLICY_ID                   
		AND  AFR.POLICY_VERSION_ID = OI.POLICY_VERSION_ID  
		AND OI.IDEN_ROW_ID = AFR.CUSTOMER_OPEN_ITEM_ID        
		AND isnull(IS_COMMITTED,0)=0 
	LEFT JOIN ACT_INSTALL_PLAN_DETAIL PD
		ON PD.IDEN_PLAN_ID = PCL.INSTALL_PLAN_ID        
	
	WHERE  (PCL.POLICY_NUMBER=@POLICYNO or @POLICYNO is null)                        
	 AND ISNULL(IS_TEMP_ENTRY,'0') <> '1'    
	 AND (CAST(CONVERT(VARCHAR,SOURCE_EFF_DATE,101) AS DATETIME) >= CAST(CONVERT(VARCHAR,@FROMDATE,101) AS DATETIME) or @FROMDATE is null) 
	AND (CAST(CONVERT(VARCHAR,SOURCE_EFF_DATE,101) AS DATETIME) <= CAST(CONVERT(VARCHAR,@TODATE,101) AS DATETIME) or @TODATE is null) 
	AND  (ITEM_TRAN_CODE = @FEETYPE )  
	 AND ISNULL(TOTAL_DUE,0)>=0    
	--Commented on 30 May 2008 --Commented on 30 May 2008 : As we are Displaying all record
	/*AND (
		(SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL 
		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID )    =  0)
	    	OR
		(SELECT ISNULL(SUM(ISNULL(FEES_REVERSE,0)),0) FROM ACT_FEE_REVERSAL 
 		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID AND ISNULL(IS_COMMITTED,0)=1 )    
		<
		(SELECT ISNULL(MAX(FEES_AMOUNT),0) FROM ACT_FEE_REVERSAL 
		WHERE CUSTOMER_OPEN_ITEM_ID =  OI.IDEN_ROW_ID)*/
	  
END
END


GO

