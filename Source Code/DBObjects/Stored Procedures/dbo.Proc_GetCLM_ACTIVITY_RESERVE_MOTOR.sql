IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_RESERVE_MOTOR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_RESERVE_MOTOR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                                                                                    
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_RESERVE_MOTOR                                                                                                                                        
Created by      : Sumit Chhabra                                                                                                                                                  
Date            : 30/05/2006                                                                                                                                                    
Purpose         : Fetch data from CLM_ACTIVITY_RESERVE table for claim motor reserve screen                                                                                                                                
Created by      : Sumit Chhabra                                                                                                                                                   
Revison History :                                                                                                                                                    
Used In        : Wolverine                                                                                                                                                    
------------------------------------------------------------                                                                                                                                                    
Date     Review By          Comments                                                                                                                                                    
------   ------------       -------------------------*/                                                                                                                                                    
                    
CREATE   PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_MOTOR                                                                                                                                          
@CLAIM_ID int                                                                                
AS                                                                                                                                                    
BEGIN                                                                          
                                                                       
                                                                                                  
           
declare @POLICY_COVERAGES varchar(10)                                                                                   
SET @POLICY_COVERAGES = 'PL'                                                                        
--Select Policy Level Coverages                                              
	SELECT 
		DISTINCT 
			CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,
		  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,
		  CAR.COVERAGE_ID AS COV_ID, MC.COV_CODE,MC.COV_DES AS COV_DESC,
			CASE MC.LIMIT_TYPE  
				WHEN 2 THEN substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0, + 
										charindex('.',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +  
										CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'')) + '/' +  
										substring(convert(varchar(30),convert(money,PVC.LIMIT_2),1),0,charindex('.',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) +   
										CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))  
				ELSE 
										substring(convert(varchar(30),convert(money,PVC.LIMIT_1),1),0,charindex('.',convert(varchar(30),convert(money,PVC.LIMIT_1),1),0)) + 
										CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,''))  
			END AS LIMIT,
			CASE MC.DEDUCTIBLE_TYPE  
				WHEN 2 THEN substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ' '+ 
										CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +  
										substring(convert(varchar(30),convert(money,PVC.Deductible_2),1),0,charindex('.',convert(varchar(30),convert(money,PVC.Deductible_2),1),0))  +  
										CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,''))  
				ELSE 
										substring(convert(varchar(30),convert(money,PVC.Deductible_1),1),0,charindex('.',convert(varchar(30),convert(money,PVC.Deductible_1),1),0)) + ' '+ 
										CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,''))  
			END AS DEDUCTIBLE, 
			MC.COVERAGE_TYPE  
	FROM 
		CLM_CLAIM_INFO CCI  
	LEFT JOIN 
		CLM_ACTIVITY_RESERVE CAR 
	ON 
		CAR.CLAIM_ID = CCI.CLAIM_ID  
	LEFT JOIN 
		POL_VEHICLE_COVERAGES PVC 
	ON 
		PVC.CUSTOMER_ID = CCI.CUSTOMER_ID AND 
		PVC.POLICY_ID = CCI.POLICY_ID AND 
		PVC.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND 
		CAR.COVERAGE_ID = PVC.COVERAGE_CODE_ID  
	LEFT JOIN 
		MNT_COVERAGE MC 
	ON 
		MC.COV_ID = CAR.COVERAGE_ID   
	LEFT OUTER JOIN 
		MNT_LOOKUP_VALUES MLV  ON MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER  
	WHERE 
		CAR.CLAIM_ID = @CLAIM_ID AND 
		(MC.COVERAGE_TYPE=CAST(@POLICY_COVERAGES AS VARCHAR) OR MC.COVERAGE_TYPE IS NULL) 
		ORDER BY CAR.RESERVE_ID 

--Select Vehicle Level Coverages
	SELECT 
		CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,
		CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES, 
		PVC.VEHICLE_ID,MC.COV_ID, MC.COV_CODE, COV_DES AS COV_DESC ,
		(CAST(CIV.VIN AS VARCHAR)+'-'+CAST(CIV.VEHICLE_YEAR AS VARCHAR)+'-'+CAST(CIV.MAKE AS VARCHAR)+'-'+CAST(CIV.MODEL AS VARCHAR)) AS VEHICLE,
		CASE MC.LIMIT_TYPE  
			WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, + 
									CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +  
									CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))+ '/' +  
									SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +   
									CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))  
			ELSE 	
									SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) + 
									CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,''))  
		END AS LIMIT , 
		CASE MC.DEDUCTIBLE_TYPE  
			WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' '+ 
									CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +  
									SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0))  +  
									CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,''))  
			ELSE 
								SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' '+ 
								CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,''))  
		END AS DEDUCTIBLE,  
		MC.COVERAGE_TYPE  
	FROM 
		CLM_CLAIM_INFO CCI 
	JOIN 
		CLM_ACTIVITY_RESERVE CAR  
	ON 
		CCI.CLAIM_ID = CAR.CLAIM_ID  
	JOIN 
		POL_VEHICLE_COVERAGES PVC 
	ON 
		CCI.POLICY_ID = PVC.POLICY_ID AND  
		CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND  
		CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND 
		CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND  
		CAR.VEHICLE_ID = PVC.VEHICLE_ID  
	JOIN 
		MNT_COVERAGE MC 
	ON 
		MC.COV_ID = CAR.COVERAGE_ID  
	LEFT OUTER JOIN 
		CLM_INSURED_VEHICLE CIV 
	ON 
		CAR.VEHICLE_ID = CIV.POLICY_VEHICLE_ID 
	LEFT OUTER JOIN 
		MNT_LOOKUP_VALUES MLV  
	ON 
		MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER  
	WHERE  
		CCI.CLAIM_ID=@CLAIM_ID AND 
		MC.COVERAGE_TYPE<>CAST(@POLICY_COVERAGES AS VARCHAR) 
		AND CIV.CLAIM_ID=@CLAIM_ID 
	ORDER BY 
		CAR.RESERVE_ID                                                                         
END


GO

