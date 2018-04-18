IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_R_DPVAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_R_DPVAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc [PROC_SUSEP_R_DPVAT] '07-30-2011'
  
CREATE PROC [dbo].[PROC_SUSEP_R_DPVAT]  
@DATETIME DATETIME, --=NULL      
@POLICY_NUMBER   VARCHAR(30)=NULL  
   
AS     
BEGIN    
DECLARE @MONTH INT,        
@DATE DATETIME      
    
SET @MONTH= MONTH(DATEADD(MONTH,-1,@DATETIME))     
SET @DATE = DATEADD(MONTH, -1, @DATETIME)     
    
    
SELECT         
 RIGHT('00000000000000000000'+ CAST(TICKET_CODE AS VARCHAR),20) AS 'BILHETE', 
 --CHANGED BY ANKIT AS PER DISCUSSION
 ----------------------------------------
      
 --CAST(COMPETENCE_DATE AS DATE) AS 'DATA_COMP',       
 --CONVERT(VARCHAR,YEAR(getdate()),101)+    
 --CASE        --CHANGED BY SHUBHANSHU FOR ITRACK 1076     
 --WHEN MONTH(getdate()) IN (1,2,3,4,5,6,7,8,9) THEN '0' + CONVERT(VARCHAR,MONTH(getdate()),101)    
 --ELSE CONVERT(VARCHAR,MONTH(getdate()),101)    
 --END AS 'DATA_COMP',  --CONVERT(VARCHAR,getdate(),101) AS 'DATA_COMP', 
 CONVERT(VARCHAR,YEAR(@DATETIME),101)+ 
CASE --CHANGED BY RODOLFO ARAUJO EbixLatm 
WHEN MONTH(DATEADD(MONTH,-1,@DATETIME)) IN (1,2,3,4,5,6,7,8,9) THEN '0' + CONVERT(VARCHAR,MONTH(DATEADD(MONTH,-1,@DATETIME)),101) 
ELSE CONVERT(VARCHAR,MONTH(DATEADD(MONTH,-1,@DATETIME)) ,101) 
END AS 'DATA_COMP',
     
 --CONVERT(VARCHAR,MONTH(getdate()),101) AS 'DATA_COMP',--CHANGED BY SHUBHANSHU FOR ITRACK 1076     
 ISNULL(VW.CATEGORY,'') AS 'CATEGORIA',      
 --RIGHT('0000000000000000'+ CAST(RISK_PREMIUM AS VARCHAR),16) AS 'PREMIO',
 CAST(RIGHT('00000000000000000'+ REPLACE(CAST(RISK_PREMIUM AS VARCHAR),'.', ''),17) AS DECIMAL(15,2)) AS 'PREMIO',      
 ISNULL([STATE],'') AS 'REGIAO'--,    
 --POLICY_NUMBER      
      
FROM             
 VW_SUSEP_R_DPVAT VW      
 WHERE      
 --MONTH(COMPETENCE_DATE) = MONTH(@DATE)      
 --AND YEAR(COMPETENCE_DATE) = YEAR(@DATE) AND      
 MONTH(COMPETENCE_DATE) = MONTH(@DATE)   ----CHANGED BY SHUBHANSHU FOR ITRACK 1076     
 AND YEAR(COMPETENCE_DATE) = YEAR(@DATE) AND   ----CHANGED BY SHUBHANSHU FOR ITRACK 1076     
 (@POLICY_NUMBER IS NULL OR POLICY_NUMBER = @POLICY_NUMBER)      
 AND ISNULL(RISK_PREMIUM,0) > 0 
 --CHANGED BY ANKIT AS PER DISCUSSION     
 AND POLICY_STATUS NOT IN ('UISSUE','UCANCL')  ----CHANGED BY SHUBHANSHU FOR ITRACK 1076   
      
END  
  
  

GO


