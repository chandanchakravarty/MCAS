IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_RiskWise_SumTotal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_RiskWise_SumTotal]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC Proc_GetPDFAuto_RiskWise_SumTotal
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
			  END  AS COVERAGE_PREMIUM,     
			 CASE WHEN P1.PROCESS_TYPE = '14'  
			 THEN  
				CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
					 ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
			 ELSE ''  
			  END AS WRITTEN_PREMIUM,    
			C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID, '' AS COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*               
			--COMP_REMARKS COLUMN Added By raghav 
			 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)         
			 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
			  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
			 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) ON      
			   M.COMPONENT_CODE = C1.COMPONENT_CODE      
			  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID       
				 AND P1.RISK_ID = @VEHICLEID AND P1.RISK_TYPE = @RISKTYPE AND M.COV_CODE IS NULL AND C1.COMPONENT_CODE IN ('SUMTOTAL')      
		END
 ELSE IF(@CALLEDFROM='POLICY')
	BEGIN
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
		C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID, '' AS COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*               
		--COMP_REMARKS COLUMN Added By raghav 
		FROM CLT_PREMIUM_SPLIT  P1  with(nolock)         
		 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
		  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
		 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) 
		ON      
		   M.COMPONENT_CODE = C1.COMPONENT_CODE      
		   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID       
			 AND P1.RISK_ID = @VEHICLEID AND 
			 P1.RISK_TYPE = @RISKTYPE AND M.COV_CODE IS NULL AND C1.COMPONENT_CODE IN ('SUMTOTAL')     
		END
END

GO

