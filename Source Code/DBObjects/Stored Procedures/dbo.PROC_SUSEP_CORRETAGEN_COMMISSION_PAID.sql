IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_CORRETAGEN_COMMISSION_PAID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_CORRETAGEN_COMMISSION_PAID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




  
  --ITRACK - 1066
 --DROP PROC [PROC_SUSEP_CORRETAGEN_COMMISSION_PAID]
  
CREATE PROC [dbo].[PROC_SUSEP_CORRETAGEN_COMMISSION_PAID] --'07/01/2011'  
@DATETIME DATETIME=NULL,  
@POLICY_NUMBER VARCHAR(30)=NULL  
AS  
BEGIN    
    
 DECLARE @MONTH VARCHAR(2)    
 DECLARE @YEAR VARCHAR(4)   
 DECLARE @TEMP NVARCHAR(500)  
 SET @MONTH= DATEPART(MM, @DATETIME)   
 SET @YEAR=(SELECT DATEPART(YYYY, @DATETIME))   
    
 SELECT     
 --Numeric sequence in file       
 RIGHT('0000000000'+ CONVERT(VARCHAR, ROW_NUMBER() OVER (ORDER BY SEQUENCIA),10),10) AS SEQUENCIA,        
          
        
       
 --Company Code in SUSEP       
 '05045' AS COD_CIA,      
       
 --REFERENCE MONTH      
 CASE    
 WHEN @MONTH IN (1,2,3,4,5,6,7,8,9) THEN  @YEAR + '0' + @MONTH   
 WHEN @MONTH IN (10,11,12) THEN  @YEAR + @MONTH  
 END AS DT_BASE,      
        
 --SUSEP LOL (Line of Business)      
  CASE WHEN COD_RAMO = '0520' THEN '0982' 
  ELSE COD_RAMO END AS COD_RAMO,       
       
 --Policy Number       
   RIGHT('00000000000000'+NUM_APOL,21) as NUM_APOL,      
       
 -- Endorsement number       
    RIGHT('000000000000000000000' +ISNULL(CONVERT(VARCHAR(10),NUM_EN),''),21) AS NUM_EN,          
       
   --Sequence number of installment        
    RIGHT('00'+CONVERT(VARCHAR,ISNULL(PARCELA,0)),2) AS PARCELA,        
         
 --Number of installments      
        
  RIGHT('00'+CONVERT(VARCHAR,ISNULL(QTDE_PARC,0)),2)as QTDE_PARC,       
       
--BROKER COMMISSION        
  RIGHT('0000000000000000'+ CONVERT(varchar,REPLACE(ABS(ISNULL(VR_PG_COR,0)) ,'.',',')),16) AS VR_PG_COR,     
        
 --BROKER NAME        
   RIGHT('                              '+ISNULL(NOM_CORAG,''),30) AS NOM_CORAG, 
       
--CPF/CNPG        
  RIGHT('00000000000000'+ CONVERT(varchar,REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(CPF_CNPJ_C,''),'.',''),',',''),'/',''),'-','') ),14)AS  CPF_CNPJ_C,  
       
--SUSEP code of broker, agents, canvassers or partners        
 RIGHT('00000000000000'+ CONVERT(varchar,RTRIM(ISNULL(COD_COR,''))),14) AS COD_COR 
      
--,A_smt_det.*       
 FROM VW_SUSEP_BROKER_COMMISSION VW    
 WHERE  (@POLICY_NUMBER IS NULL OR VW.POLICY_NUMBER = @POLICY_NUMBER)    
 AND MONTH(VW.PAYMENT_DATE) = MONTH(@DATETIME)    
 AND YEAR(VW.PAYMENT_DATE) = YEAR(@DATETIME)    
END    
GO  


