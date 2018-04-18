IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_RiskWise_Coverage_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_RiskWise_Coverage_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Proc_GetPDFAuto_RiskWise_Coverage_Details]                    
(                                
 @CUSTOMERID   int,                                
 @POLID        int,                                
 @VERSIONID   int,                                
 @VEHICLEID   int,  
 @RISKTYPE nvarchar(20),                                
 @CALLEDFROM  VARCHAR(20)                                
)                                
AS                                
BEGIN     
IF (@CALLEDFROM='APPLICATION')                                
	BEGIN                                
	  SELECT DISTINCT MC.rank RANK,                                 
	  COV_DES,COV_CODE,               
	  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
	  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,   
	 CASE deductible1_amount_text  
	  WHEN 'LIMITED'  
	   THEN ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
	  ELSE  
	   ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
	  END  
	 DEDUCTIBLE_1 ,                  
	  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
	  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text                                
	  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                               
	  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
	--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
	  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
	  ED.PRINT_ORDER                                
	  FROM  APP_VEHICLE_COVERAGES CI  with(nolock)                                
	  INNER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
	   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED ON MC.COV_ID=ED.SELECT_COVERAGE                        
	   LEFT OUTER JOIN APP_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                            
		AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                    
		AND DE.APP_VERSION_ID=@VERSIONID AND DE.vehicle_ID=@VEHICLEID                          
	   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
		  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
	  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID AND CI.APP_VERSION_ID=@VERSIONID AND CI.vehicle_ID=@VEHICLEID                                
	  and MC.IS_ACTIVE='Y' AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'                               
	  OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND (SELECT MED.SELECT_COVERAGE FROM MNT_ENDORSMENT_DETAILS MED  with(nolock)  INNER JOIN 
		APP_VEHICLE_ENDORSEMENTS ADE  with(nolock)  ON MED.ENDORSMENT_ID=ADE.ENDORSEMENT_ID                            
		AND ADE.CUSTOMER_ID=@CUSTOMERID AND ADE.APP_ID=@POLID                    
		AND ADE.APP_VERSION_ID=@VERSIONID AND ADE.vehicle_ID=@VEHICLEID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE and MED.IS_ACTIVE='Y') IS NULL)) 
	        
	------------------------------       
	UNION          
	          
	  SELECT DISTINCT MC.rank RANK,                                 
	  ED.DESCRIPTION AS COV_DES,COV_CODE,               
	  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
	  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,   
	 CASE deductible1_amount_text  
	  WHEN 'LIMITED'  
	   THEN ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
	  ELSE  
	   ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
	 END  
	 DEDUCTIBLE_1 ,                  
	  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
	  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text       
	  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                                
	  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE          
	--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                             
	  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
	  ED.PRINT_ORDER                                
	  FROM APP_VEHICLE_ENDORSEMENTS DE   with(nolock)                                
	   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                        
	   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
		  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
	   LEFT OUTER JOIN APP_VEHICLE_COVERAGES CI ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                            
		AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID                    
		AND CI.APP_VERSION_ID=@VERSIONID AND CI.vehicle_ID=@VEHICLEID                          
	   LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
	  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID AND DE.APP_VERSION_ID=@VERSIONID AND DE.vehicle_ID=@VEHICLEID                                
	  and MC.IS_ACTIVE='Y'                                   
	 order by rank               
                
	END                                
 ELSE IF (@CALLEDFROM='POLICY')                                
	BEGIN                                
	  SELECT DISTINCT MC.rank RANK,                                
	  COV_DES,COV_CODE,                            
	  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
	  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,     
	 CASE deductible1_amount_text  
	 WHEN 'LIMITED'  
	  THEN   ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')  
	 ELSE                        
	  ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
	 END DEDUCTIBLE_1 ,                  
	  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
	  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text            
	  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE               
	  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
	--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
	  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
	  ED.PRINT_ORDER                                
	  FROM  POL_VEHICLE_COVERAGES  CI with(nolock)                     
	  INNER JOIN MNT_COVERAGE  MC with(nolock) ON MC.COV_ID=CI.COVERAGE_CODE_ID       
	   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED ON MC.COV_ID=ED.SELECT_COVERAGE                        
	   LEFT OUTER JOIN POL_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                            
		AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                    
		AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.VEHICLE_ID=@VEHICLEID                          
	   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
		  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
	  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.VEHICLE_ID=@VEHICLEID                                
	  and MC.IS_ACTIVE='Y' AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'                                  
	  OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND (SELECT MED.SELECT_COVERAGE 
	FROM MNT_ENDORSMENT_DETAILS MED  with(nolock)  INNER JOIN POL_VEHICLE_ENDORSEMENTS PDE  with(nolock) ON MED.ENDORSMENT_ID=PDE.ENDORSEMENT_ID                            
		AND PDE.CUSTOMER_ID=@CUSTOMERID AND PDE.POLICY_ID=@POLID                    
		AND PDE.POLICY_VERSION_ID=@VERSIONID AND PDE.vehicle_ID=@VEHICLEID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE and MED.IS_ACTIVE='Y') IS NULL))      
	             
	UNION          
	          
	  SELECT DISTINCT MC.rank RANK,                                
	  ED.DESCRIPTION AS COV_DES,COV_CODE,                            
	  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                   
	  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,    
	 CASE deductible1_amount_text  
	  WHEN 'LIMITED'  
	   THEN ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
	  ELSE   
	   ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
	 END DEDUCTIBLE_1 ,                  
	  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
	  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text                                
	  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                                
	  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
	--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
	  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
	  ED.PRINT_ORDER              
	  FROM  POL_VEHICLE_ENDORSEMENTS  DE with(nolock)                               
	   INNER JOIN MNT_ENDORSMENT_DETAILS  ED with(nolock) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                        
	   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
		  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
	   LEFT OUTER JOIN POL_VEHICLE_COVERAGES CI ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                            
		AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID                    
		AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.VEHICLE_ID=@VEHICLEID                          
	   LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
	  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.VEHICLE_ID=@VEHICLEID                                
	  and MC.IS_ACTIVE='Y'       
	 order by rank    
	END 
END     
GO

