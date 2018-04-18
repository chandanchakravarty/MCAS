IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwInnerQuery]'))
DROP VIEW [dbo].[vwInnerQuery]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view vwInnerQuery
   as
   SELECT 				 
		SUM(C_RSRV.PAYMENT_AMOUNT) PAYMENT_AMOUNT,C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID
		--,COV_EXT.COV_ID,CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE
		
		from CLM_PARTIES C_PT WITH(NOLOCK)
		
		INNER JOIN MNT_REIN_COMAPANY_LIST MR_LST
		  ON MR_LST.REIN_COMAPANY_ID = C_PT.SOURCE_PARTY_ID    --JOIN ON COMPANY ID
		  INNER JOIN CLM_ACTIVITY_RESERVE C_RSRV
		ON C_RSRV.CLAIM_ID =C_PT.CLAIM_ID 
		  
		INNER JOIN CLM_PRODUCT_COVERAGES WITH(NOLOCK)
						 ON C_RSRV.CLAIM_ID = CLM_PRODUCT_COVERAGES.CLAIM_ID
						 AND C_RSRV.ACTUAL_RISK_ID = CLM_PRODUCT_COVERAGES.RISK_ID
						AND C_RSRV.COVERAGE_ID = CLM_PRODUCT_COVERAGES.COVERAGE_CODE_ID
						
		LEFT JOIN MNT_COVERAGE_EXTRA COV_EXT WITH(NOLOCK)
						  ON CLM_PRODUCT_COVERAGES.COVERAGE_CODE_ID = COV_EXT.COV_ID    	 
		LEFT JOIN CLM_PAYEE C_P  WITH(NOLOCK)  
		  ON C_RSRV.CLAIM_ID = C_P.CLAIM_ID   
		  AND  C_P.ACTIVITY_ID = C_RSRV.ACTIVITY_ID 
		  AND C_P.PARTY_ID = C_PT.PARTY_ID 
		INNER JOIN CLM_TYPE_DETAIL CLM_TYPE
		 ON  CLM_TYPE.DETAIL_TYPE_ID = C_PT.PARTY_TYPE_ID
		INNER JOIN CLM_ACTIVITY C_ACT WITH(NOLOCK)
		  ON C_ACT.CLAIM_ID =  C_RSRV.CLAIM_ID
		  AND C_ACT.ACTIVITY_ID =  C_RSRV.ACTIVITY_ID 
		INNER JOIN CLM_CLAIM_INFO C_INFO WITH(NOLOCK)   
		  ON C_INFO.CLAIM_ID = C_PT.CLAIM_ID    
		INNER JOIN  POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)   
		  ON C_LST.CUSTOMER_ID = C_INFO.CUSTOMER_ID    
		  AND C_LST.POLICY_ID = C_INFO.POLICY_ID    
		  AND C_LST.POLICY_VERSION_ID = C_INFO.POLICY_VERSION_ID 
		INNER JOIN  mnt_div_list D_LST   WITH(NOLOCK) 
		  ON D_LST.DIV_ID = C_LST.DIV_ID    
		--INNER JOIN mnt_profit_center_list P_LST WITH(NOLOCK)          
		--  ON C_LST.PC_ID = P_LST.PC_ID      
		INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)       
		  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB		      
		WHERE    C_LST.CO_INSURANCE IN (14548,14549) -- FOLLOWER

		GROUP BY  C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID
GO

