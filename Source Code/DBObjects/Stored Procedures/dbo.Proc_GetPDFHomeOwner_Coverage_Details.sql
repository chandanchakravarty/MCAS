IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFHomeOwner_Coverage_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFHomeOwner_Coverage_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE dbo.Proc_GetPDFHomeOwner_Coverage_Details  
(                                                
 @CUSTOMERID   int,                                                
 @POLID                int,                                                
 @VERSIONID   int,                                               
 @DWELLINGID   int,                                                
 @CALLEDFROM  VARCHAR(20)                                                
)                                                
AS                                                
BEGIN                                                
 IF (@CALLEDFROM='APPLICATION')                                                
 BEGIN                                                
 DECLARE @LOBID NVARCHAR(20)
	SELECT @LOBID = APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID
  --------- LOB CHECK START  
  IF(@LOBID=6)
	BEGIN                                       
                                               
		  SELECT DISTINCT MC.rank AS RANK,                                                
		  COV_DES,COV_CODE,                                
		  --ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),1),'')
		ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                            
		   --ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'') DEDUCTIBLE_1,                        
		   ISNULL(convert(varchar,convert(money,deductible_1),1),'')   DEDUCTIBLE_1 ,                                
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                           
		  (ISNULL(convert(varchar,convert(MONEY,(ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0))),101),0)  + ' ' +                                 
		 ISNULL(LIMIT1_AMOUNT_TEXT,'')) + CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' +                                 
		 ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END END LIMIT_1,                                
		                                
		                                
		COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,'N' AS ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--, isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		   ,EA.EDITION_DATE, EA.ATTACH_FILE , ED.PRINT_ORDER,                               
		  ISNULL(convert(varchar,convert(money,DEDUCTIBLE),1),'') DEDUCTIBLE ,
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		CI.DWELLING_ID
		  FROM  APP_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK)                                                
		   INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                             
		   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON MC.COV_ID=ED.SELECT_COVERAGE                                      
		   LEFT OUTER JOIN APP_DWELLING_ENDORSEMENTS DE WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                                    
			AND DE.APP_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                          
		  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID AND CI.APP_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID,CI.DWELLING_ID)     
				AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y' OR COV_CODE = 'OS')             
		      
		-----------------------------                
		  UNION                
		                  
		   SELECT DISTINCT MC.rank AS RANK,                                        
		  ED.DESCRIPTION AS COV_DES,COV_CODE,           
		  ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                           
		   --ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'') DEDUCTIBLE_1,                        
		   ISNULL(convert(varchar,convert(money,deductible_1),1),'')   DEDUCTIBLE_1 ,                                
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                           
		  (ISNULL(convert(varchar,CONVERT(MONEY,(ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0))),101),0)  + ' ' +                                 
		 ISNULL(LIMIT1_AMOUNT_TEXT,'')) + CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' +                                 
		 ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END END LIMIT_1,                                
		                                
		                                
		COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--, isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		   ,EA.EDITION_DATE, EA.ATTACH_FILE , ED.PRINT_ORDER,                                 
		  ISNULL(convert(varchar,convert(money,DEDUCTIBLE),1),'') DEDUCTIBLE ,                               
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		DE.DWELLING_ID
		  FROM  APP_DWELLING_ENDORSEMENTS DE WITH (NOLOCK)                                                 
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		   LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK) ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                 
			AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID                                    
			AND CI.APP_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID ,CI.DWELLING_ID)
		   LEFT OUTER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                             
		                                               
		  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID AND DE.APP_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
					 AND ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y'                                  
		/*  UNION                                                
		                                                  
		  SELECT ED.rank AS RANK,                                                
		   DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE,                                 
		   '' LIMIT_2,                                 
		   '' DEDUCTIBLE_1,                                
		   '' LIMIT_1,'' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                                
		  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER FORM_NUMBER,isnull(left(convert(varchar(12),ed.edition_date,101),2) + '/' + right(convert(varchar(12),ed.edition_date,101),2),'N/A') edition_date, EA.ATTACH_FILE ,    '' deductible                       
  
		--'('  + left(convert(varchar(15),ED.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),ED.EDITION_DATE,1),2) + ')' EDITION_DATE                                         
		  FROM APP_DWELLING_ENDORSEMENTS WE                                                
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                       
		   LEFT OUTER JOIN APP_DWELLING_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                                    
			AND DE.APP_VERSION_ID=@VERSIONID                                           
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                                            
		  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.APP_ID=@POLID AND WE.APP_VERSION_ID=@VERSIONID AND WE.DWELLING_ID=@DWELLINGID                                           
		  AND ENDORS_ASSOC_COVERAGE='N'                                                 
		  */                                              
		  order by rank                               
		                    
		SELECT           
		   CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        
		   AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,         
		  C1.COMPONENT_CODE,P1.RISK_TYPE, P1.RISK_ID,        
		  M.COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		C.DWELLING_ID                    
		FROM APP_DWELLING_SECTION_COVERAGES C WITH (NOLOCK)                     
		INNER JOIN APP_DWELLINGS_INFO P WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P.CUSTOMER_ID                     
		AND C.APP_ID = P.APP_ID                     
		AND C.APP_VERSION_ID = P.APP_VERSION_ID                     
		AND C.DWELLING_ID = P.DWELLING_ID AND P.IS_ACTIVE = 'Y'                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.APP_ID = P1.APP_ID                     
		AND C.APP_VERSION_ID = P1.APP_VERSION_ID LEFT                     
		OUTER JOIN MNT_COVERAGE M WITH (NOLOCK) ON M.COV_ID = C.COVERAGE_CODE_ID                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)                     
		ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                                     
		  WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.APP_ID=@POLID AND C.APP_VERSION_ID=@VERSIONID AND C.DWELLING_ID=isnull(@DWELLINGID,C.DWELLING_ID)
				 AND P1.RISK_ID = isnull(@DWELLINGID,C.DWELLING_ID) AND C1.COMPONENT_CODE IS NOT NULL                        
		                                                
		 UNION    
		    
		 SELECT   
		 CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			 CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END  
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END      
		  END        AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		P1.RISK_ID as DWELLING_ID
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND M.COV_CODE IS NULL --C1.COMPONENT_CODE IN ('SUMTOTAL')  
		UNION    
		    
		 SELECT 
			CASE WHEN  C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) ='' 
				THEN ''
			ELSE 
			   CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			 END AS  COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD, 
		P1.RISK_ID as  DWELLING_ID          
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE_EXTRA M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_CODE IN ('PRP_EXPNS_FEE')   
		 union
		 SELECT   
		 CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END      
		  END        AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		 P1.RISK_ID  as  DWELLING_ID           
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID)  AND C1.COMPONENT_type in ('S','D') 
	END
ELSE -- HOMEOWNER LOB
	BEGIN
		
                                               
		  SELECT DISTINCT MC.rank AS RANK,                                                
		  COV_DES,COV_CODE,                                
		  --ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),1),'')
			ISNULL(CAST(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0) AS VARCHAR),'') AS LIMIT_2,
		--ISNULL(CONVERT(VARCHAR,isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0)),'') AS LIMIT_2,                                            
		   ISNULL(CAST(isnull(DEDUCTIBLE_1,0) AS VARCHAR),'') DEDUCTIBLE_1,                        
		  -- ISNULL(convert(varchar,convert(money,deductible_1),1),'')   DEDUCTIBLE_1 ,                                
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE       
		 	(ISNULL(convert(varchar,ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0)),0)  + ' ' +                                 
		 ISNULL(LIMIT1_AMOUNT_TEXT,'')) + CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' +                                 
		 ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END END LIMIT_1,                                
			                                
		COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,'N' AS ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--, isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		   ,EA.EDITION_DATE, EA.ATTACH_FILE , ED.PRINT_ORDER,                               
		  ISNULL(convert(varchar,DEDUCTIBLE,1),'') DEDUCTIBLE ,
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		CI.DWELLING_ID
		  FROM  APP_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK)                                                
		   INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                             
		   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON MC.COV_ID=ED.SELECT_COVERAGE                                      
		   LEFT OUTER JOIN APP_DWELLING_ENDORSEMENTS DE WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                                    
			AND DE.APP_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                                               
		  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID AND CI.APP_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID,CI.DWELLING_ID)     
				AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y' OR COV_CODE = 'OS')             
		      
		-----------------------------                
		  UNION                
		                  
		   SELECT DISTINCT MC.rank AS RANK,                                        
		  ED.DESCRIPTION AS COV_DES,COV_CODE,   
			ISNULL(CAST(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0) AS VARCHAR),'') AS LIMIT_2,        
		  --ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                           
		   ISNULL(CAST(DEDUCTIBLE_1 AS VARCHAR),'') DEDUCTIBLE_1,                        
		   --ISNULL(convert(varchar,convert(money,deductible_1),1),'')   DEDUCTIBLE_1 ,                                
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                           
		  (ISNULL(convert(varchar,ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0)),0)  + ' ' +                                 
		 ISNULL(LIMIT1_AMOUNT_TEXT,'')) + CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' +                                 
		 ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END END LIMIT_1,                                
		                                
			COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--, isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		   ,EA.EDITION_DATE, EA.ATTACH_FILE , ED.PRINT_ORDER,                                 
		  ISNULL(convert(varchar,DEDUCTIBLE,1),'') DEDUCTIBLE ,                               
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		DE.DWELLING_ID
		  FROM  APP_DWELLING_ENDORSEMENTS DE WITH (NOLOCK)                                                 
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		   LEFT OUTER JOIN APP_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK) ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                 
			AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID                                    
			AND CI.APP_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID ,CI.DWELLING_ID)
		   LEFT OUTER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                             
		                                               
		  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID AND DE.APP_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
					 AND ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y'                                  
		/*  UNION                                                
		                                                  
		  SELECT ED.rank AS RANK,                                                
		   DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE,                                 
		   '' LIMIT_2,                                 
		   '' DEDUCTIBLE_1,                                
		   '' LIMIT_1,'' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                                
		  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER FORM_NUMBER,isnull(left(convert(varchar(12),ed.edition_date,101),2) + '/' + right(convert(varchar(12),ed.edition_date,101),2),'N/A') edition_date, EA.ATTACH_FILE ,    '' deductible                       
  
		--'('  + left(convert(varchar(15),ED.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),ED.EDITION_DATE,1),2) + ')' EDITION_DATE                                         
		  FROM APP_DWELLING_ENDORSEMENTS WE                                                
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                       
		   LEFT OUTER JOIN APP_DWELLING_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                                    
			AND DE.APP_VERSION_ID=@VERSIONID                                           
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                                            
		  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.APP_ID=@POLID AND WE.APP_VERSION_ID=@VERSIONID AND WE.DWELLING_ID=@DWELLINGID                                           
		  AND ENDORS_ASSOC_COVERAGE='N'                                                 
		  */                                              
		  order by rank                               
		                    
		SELECT           
		   CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        
		   AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,         
		  C1.COMPONENT_CODE,P1.RISK_TYPE, P1.RISK_ID,        
		  M.COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		C.DWELLING_ID                    
		FROM APP_DWELLING_SECTION_COVERAGES C WITH (NOLOCK)                     
		INNER JOIN APP_DWELLINGS_INFO P WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P.CUSTOMER_ID                     
		AND C.APP_ID = P.APP_ID                     
		AND C.APP_VERSION_ID = P.APP_VERSION_ID                     
		AND C.DWELLING_ID = P.DWELLING_ID AND P.IS_ACTIVE = 'Y'                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.APP_ID = P1.APP_ID                     
		AND C.APP_VERSION_ID = P1.APP_VERSION_ID LEFT                     
		OUTER JOIN MNT_COVERAGE M WITH (NOLOCK) ON M.COV_ID = C.COVERAGE_CODE_ID                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)                     
		ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                                     
		  WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.APP_ID=@POLID AND C.APP_VERSION_ID=@VERSIONID AND C.DWELLING_ID=isnull(@DWELLINGID,C.DWELLING_ID)
				 AND P1.RISK_ID = isnull(@DWELLINGID,C.DWELLING_ID) AND C1.COMPONENT_CODE IS NOT NULL                        
		                                                
		 UNION    
		    
		 SELECT   
		 CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END  
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END      
		  END        AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		P1.RISK_ID as DWELLING_ID
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND M.COV_CODE IS NULL --C1.COMPONENT_CODE IN ('SUMTOTAL')  
		UNION    
		    
		 SELECT 
			CASE WHEN  C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) ='' 
				THEN ''
			ELSE 
			   CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			 END AS  COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD, 
		P1.RISK_ID as  DWELLING_ID          
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE_EXTRA M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_CODE IN ('PRP_EXPNS_FEE')   
		 union
		 SELECT   
		 CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END      
		  END        AS COVERAGE_PREMIUM,    
		  CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,    
		 C1.COMPONENT_CODE,P1.RISK_TYPE,P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		 P1.RISK_ID  as  DWELLING_ID           
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID)  AND C1.COMPONENT_type in ('S','D')   
	END
END                                                
 ELSE IF (@CALLEDFROM='POLICY')                                                
 BEGIN   

	SELECT @LOBID = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID
			--------- LOB CHECK START                                    
	 IF(@LOBID=6)
	BEGIN 
                                      
		  SELECT DISTINCT MC.rank AS RANK,                                                
		  COV_DES,COV_CODE,                                
		  ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                            
		  ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'')   DEDUCTIBLE_1  ,                             
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                         
		   ISNULL(convert(varchar,convert(MONEY,(ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0))),101),0) + ' ' +                                 
		   (CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' + ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END) END LIMIT_1,                                
		                                
		 COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,'N' AS ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--,isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date        
		  ,EA.EDITION_DATE, EA.ATTACH_FILE, ED.PRINT_ORDER,                                
			ISNULL(convert(varchar,convert(money,DEDUCTIBLE),1),'') DEDUCTIBLE ,
			CI.DWELLING_ID                           
		                                
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		  FROM  POL_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK)                                            
		   INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                
		   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON MC.COV_ID=ED.SELECT_COVERAGE                                            
		   LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS DE WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                                    
		AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                      
		  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID,CI.DWELLING_ID)
				AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'  OR COV_CODE = 'OS')                                        
		                
		------------------------------                
		   UNION                
		                
		SELECT DISTINCT MC.rank AS RANK,                                                
		  ED.DESCRIPTION AS COV_DES,COV_CODE,                                
		  ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                            
		  ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'')   DEDUCTIBLE_1  ,                             
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                         
		   ISNULL(convert(varchar,CONVERT(MONEY,(ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0))),101),0) + ' ' +                                 
		   (CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' + ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END) END LIMIT_1,                                
		                                
		 COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--,isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		  ,EA.EDITION_DATE, EA.ATTACH_FILE, ED.PRINT_ORDER,                                
			ISNULL(convert(varchar,convert(money,DEDUCTIBLE),1),'') DEDUCTIBLE,                                
			DE.DWELLING_ID                            
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		  FROM POL_DWELLING_ENDORSEMENTS DE WITH (NOLOCK)                                                 
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                              
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		   LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK) ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                                            
			AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID                       
			AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID ,CI.DWELLING_ID)
		   LEFT OUTER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID      
		                                   
		  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
			AND ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y'            
		/*  UNION                                                
		  SELECT ED.rank AS RANK,                                                
		   DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE, '' LIMIT_2, '' DEDUCTIBLE_1,  '' LIMIT_1,                                
		'' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                                
		  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER FORM_NUMBER,isnull(left(convert(varchar(12),ed.edition_date,101),2) + '/' + right(convert(varchar(12),ed.edition_date,101),2),'N/A') EDITION_DATE, EA.ATTACH_FILE, '' deductible     		  FROM POL_DWELLING_
END
		ORSEMENTS WE                                                
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                                                 
		   LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                 
		   AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID                        
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		--      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                        
		  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.POLICY_ID=@POLID AND WE.POLICY_VERSION_ID=@VERSIONID AND WE.DWELLING_ID=@DWELLINGID                                                
		  AND ENDORS_ASSOC_COVERAGE='N'                                                 
		  */                              
		 order by rank                          
		                    
		SELECT         
		CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END         
		  AS COVERAGE_PREMIUM,      
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,       
		  C1.COMPONENT_CODE,P1.RISK_TYPE, P1.RISK_ID,        
		  M.COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD , C.DWELLING_ID as DWELLING_ID                  
		FROM POL_DWELLING_SECTION_COVERAGES C WITH (NOLOCK)                     
		INNER JOIN POL_DWELLINGS_INFO P WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P.CUSTOMER_ID                     
		AND C.POLICY_ID = P.POLICY_ID                     
		AND C.POLICY_VERSION_ID = P.POLICY_VERSION_ID                     
		AND C.DWELLING_ID = P.DWELLING_ID AND P.IS_ACTIVE = 'Y'                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.POLICY_ID = P1.POLICY_ID                     
		AND C.POLICY_VERSION_ID = P1.POLICY_VERSION_ID LEFT                     
		OUTER JOIN MNT_COVERAGE M WITH (NOLOCK) ON M.COV_ID = C.COVERAGE_CODE_ID                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)                     
		ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                                     
		  WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.POLICY_ID=@POLID AND C.POLICY_VERSION_ID=@VERSIONID AND C.DWELLING_ID=isnull(@DWELLINGID ,C.DWELLING_ID)
				 AND P1.RISK_ID = isnull(@DWELLINGID,C.DWELLING_ID) AND C1.COMPONENT_CODE IS NOT NULL                        
		                                          
		 UNION    
		    
		 SELECT   
		  CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
			P1.RISK_ID as  DWELLING_ID         
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND M.COV_CODE IS NULL --OR C1.COMPONENT_CODE IN ('SUMTOTAL')    

		UNION       
		 SELECT  
				CASE   WHEN  C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) ='' 
				THEN ''
			ELSE 
				 CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			END AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		 P1.RISK_ID as DWELLING_ID           
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE_EXTRA M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_CODE IN ('PRP_EXPNS_FEE')  
		UNION
		 SELECT   
		  CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
			 P1.RISK_ID as DWELLING_ID        
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_type in ('S','D')
	END
	ELSE	---- HOMEOWNER LOB
	BEGIN
			SELECT DISTINCT MC.rank AS RANK,                                                
		  COV_DES,COV_CODE,   
			ISNULL(CAST(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0) AS VARCHAR),'') AS LIMIT_2,                              
		  --ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                            
		  ISNULL(CONVERT(VARCHAR,DEDUCTIBLE_1,1),'')   DEDUCTIBLE_1  ,                             
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                         
		   (ISNULL(convert(varchar,ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0)),0) + ' ' +  ISNULL(LIMIT1_AMOUNT_TEXT,'')) +                               
		   (CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' + ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END) END LIMIT_1,                                
		  COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,'N' AS ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--,isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date        
		  ,EA.EDITION_DATE, EA.ATTACH_FILE, ED.PRINT_ORDER,                                
			ISNULL(convert(varchar,DEDUCTIBLE,1),'') DEDUCTIBLE ,
			CI.DWELLING_ID                           
		                                
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		  FROM  POL_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK)                                            
		   INNER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                
		   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON MC.COV_ID=ED.SELECT_COVERAGE                                            
		   LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS DE WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
			AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                                    
		AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		                      
		  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID,CI.DWELLING_ID)
				AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'  OR COV_CODE = 'OS')                                        
		                
		------------------------------                
		   UNION                
		                
		SELECT DISTINCT MC.rank AS RANK,                                                
		  ED.DESCRIPTION AS COV_DES,COV_CODE,                                
		  ISNULL(CAST(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0) AS VARCHAR),'') AS LIMIT_2,  
		  --ISNULL(CONVERT(VARCHAR,CONVERT(MONEY,(isnull(LIMIT_2,0) + isnull(DEDUCTIBLE_1,0))),101),'') AS LIMIT_2,                                            
		  ISNULL(CONVERT(VARCHAR,DEDUCTIBLE_1,1),'')   DEDUCTIBLE_1  ,                             
		  CASE WHEN COV_CODE IN ('APOBI','APRPR','IOPSO','IOPSI','IOPSS','IOPSL','PERIJ','REEMN','BPCES','BPSCM','BPTAL','BPTNO','FLIFP','FLOFO','FLOFR','EOP17','AROF1','AROF2')                          
		  THEN '' ELSE                         
		   (ISNULL(convert(varchar,ISNULL(LIMIT_1,0) + ISNULL(DEDUCTIBLE_1,0)),0) + ' ' + ISNULL(LIMIT1_AMOUNT_TEXT,'')) +                                 
		   (CASE WHEN LIMIT_2 IS NOT NULL THEN '/' +  ISNULL(CAST(LIMIT_2 AS VARCHAR),'') + ' ' + ISNULL(LIMIT2_AMOUNT_TEXT,'') ELSE '' END) END LIMIT_1,                                
		                                
		  COMPONENT_CODE, ED.ENDORS_PRINT                                                
		  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER FORM_NUMBER--,isnull(left(convert(varchar(12),mc.edition_date,101),2) + '/' + right(convert(varchar(12),mc.edition_date,101),2),'N/A') edition_date                              
		  ,EA.EDITION_DATE, EA.ATTACH_FILE, ED.PRINT_ORDER,                                
			ISNULL(convert(varchar,DEDUCTIBLE,1),'') DEDUCTIBLE,                                
			DE.DWELLING_ID                            
		--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                                
		  FROM POL_DWELLING_ENDORSEMENTS DE WITH (NOLOCK)                                                 
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED WITH (NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH (NOLOCK)                                              
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		   LEFT OUTER JOIN POL_DWELLING_SECTION_COVERAGES CI WITH (NOLOCK) ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                                            
			AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID                       
			AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.DWELLING_ID=isnull(@DWELLINGID ,CI.DWELLING_ID)
		   LEFT OUTER JOIN MNT_COVERAGE MC WITH (NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID      
		                                   
		  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.DWELLING_ID=isnull(@DWELLINGID,DE.DWELLING_ID)
			AND ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y'            
		/*  UNION                                                
		  SELECT ED.rank AS RANK,                                    
		   DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE, '' LIMIT_2, '' DEDUCTIBLE_1,  '' LIMIT_1,                                
		'' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                                
		  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER FORM_NUMBER,isnull(left(convert(varchar(12),ed.edition_date,101),2) + '/' + right(convert(varchar(12),ed.edition_date,101),2),'N/A') EDITION_DATE, EA.ATTACH_FILE, '' deductible     		  FROM POL_DWELLING
_END
		ORSEMENTS WE                                                
		   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                                                 
		   LEFT OUTER JOIN POL_DWELLING_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                 
		   AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID                        
		   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
			  ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
		--      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                        
		  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.POLICY_ID=@POLID AND WE.POLICY_VERSION_ID=@VERSIONID AND WE.DWELLING_ID=@DWELLINGID                                                
		  AND ENDORS_ASSOC_COVERAGE='N'                                                 
		  */                              
		 order by rank                          
		                    
		SELECT         
		CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END         
		  AS COVERAGE_PREMIUM,      
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END AS WRITTEN_PREMIUM,       
		  C1.COMPONENT_CODE,P1.RISK_TYPE, P1.RISK_ID,        
		  M.COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD , C.DWELLING_ID as DWELLING_ID                  
		FROM POL_DWELLING_SECTION_COVERAGES C WITH (NOLOCK)                     
		INNER JOIN POL_DWELLINGS_INFO P WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P.CUSTOMER_ID                     
		AND C.POLICY_ID = P.POLICY_ID                     
		AND C.POLICY_VERSION_ID = P.POLICY_VERSION_ID                     
		AND C.DWELLING_ID = P.DWELLING_ID AND P.IS_ACTIVE = 'Y'                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1 WITH (NOLOCK)                     
		ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.POLICY_ID = P1.POLICY_ID                     
		AND C.POLICY_VERSION_ID = P1.POLICY_VERSION_ID LEFT                     
		OUTER JOIN MNT_COVERAGE M WITH (NOLOCK) ON M.COV_ID = C.COVERAGE_CODE_ID                     
		LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH (NOLOCK)                     
		ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                                     
		  WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.POLICY_ID=@POLID AND C.POLICY_VERSION_ID=@VERSIONID AND C.DWELLING_ID=isnull(@DWELLINGID ,C.DWELLING_ID)
				 AND P1.RISK_ID = isnull(@DWELLINGID,C.DWELLING_ID) AND C1.COMPONENT_CODE IS NOT NULL                        
		                           
		 UNION    
		    
		 SELECT   
		  CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
			P1.RISK_ID as  DWELLING_ID         
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND M.COV_CODE IS NULL --OR C1.COMPONENT_CODE IN ('SUMTOTAL')    

		UNION       
		 SELECT  
				CASE   WHEN  C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) ='' 
				THEN ''
			ELSE 
				 CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			END AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
		 P1.RISK_ID as DWELLING_ID           
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE_EXTRA M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_CODE IN ('PRP_EXPNS_FEE')  
		UNION
		 SELECT   
		  CASE        
			WHEN M.SHOW_ACT_PREMIUM='10963'         
			  THEN         
			  CASE         
			   WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
				THEN C1.PREMIUM        
			  ELSE        
				  CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
			  END        
			WHEN M.SHOW_ACT_PREMIUM IS NULL        
			 THEN         
			  CASE         
			   WHEN ISNUMERIC(C1.PREMIUM) = 0         
				THEN C1.PREMIUM         
			   ELSE         
				C1.PREMIUM + '.00'         
			  END         
		  END        AS COVERAGE_PREMIUM,   
		 CASE WHEN P1.PROCESS_TYPE = '14'  
		 THEN  
			CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
				 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
		 ELSE ''  
		  END  
		   AS WRITTEN_PREMIUM,     
		 C1.COMPONENT_CODE, P1.RISK_TYPE, P1.RISK_ID, '' AS COV_CODE,COMPONENT_TYPE,COMP_REMARKS,COM_EXT_AD,
			 P1.RISK_ID as DWELLING_ID        
		 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)      
		  INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)        
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID     
		 LEFT OUTER JOIN MNT_COVERAGE M ON    
		   M.COMPONENT_CODE = C1.COMPONENT_CODE    
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID     
			 AND P1.RISK_ID = isnull(@DWELLINGID,P1.RISK_ID) AND C1.COMPONENT_type in ('S','D')
	END
 END                             
END                                    


GO

