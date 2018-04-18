/*----------------------------------------------------------                                                                
Proc Name             : Dbo.[PROC_IBNR_REPORT]                                                       
Created by            : PUNEET KUMAR                                      
Date                  : 23 SEPT 2011                                                      
Purpose               :   
Revison History       :                                                                
Used In               : CLAIM MODULE   
Itrack      : 1664 TFS Bug      
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc [PROC_RVNE_REPORT] '2010-11-02 13:26:54.000'                                         
------   ------------       -------------------------*/   
CREATE PROCEDURE [dbo].[PROC_RVNE_REPORT]     
--@DATETIME DATETIME   
@FROM_DATETIME DATETIME =NULL,                                  
@TO_DATETIME DATETIME =NULL 
   
AS  
BEGIN  
  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 SELECT  
 c.SUSEP_LOB_CODE AS [Cod Ramo], --Account LOB  
 a.POLICY_NUMBER AS [Apólice], -- Policy No  
 ISNULL(e.ENDORSEMENT_NO,'') as [Endosso], -- End No  
 CONVERT( VARCHAR(10),b.COMPLETED_DATETIME,103) AS [Dt Emissão], -- Commit Date  
 CONVERT( VARCHAR(10),a.POLICY_EFFECTIVE_DATE,103) AS [Dt Ini Vigência], --Effective Date  
 CONVERT( VARCHAR(10),a.POLICY_EXPIRATION_DATE,103) AS [Dt Fim Vigência], -- Expire Date  
 dbo.fun_FormatCurrency(ABS(d.TOTAL_TRAN_PREMIUM),2) AS [Vlr Prêmio], -- Risk Premium  
 dbo.fun_FormatCurrency(ABS(d.TOTAL_TRAN_FEES),2) AS [Vlr Custo Apol], -- Policy Fee  
 dbo.fun_FormatCurrency(ABS(d.TOTAL_TRAN_INTEREST_AMOUNT),2) AS [Vlr Adic Frac], --Interest Amount  
 dbo.fun_FormatCurrency(ABS(d.TOTAL_TRAN_TAXES),2) as [Vlr IOF], -- IOF  
 dbo.fun_FormatCurrency(ABS(d.TOTAL_TRAN_AMOUNT),2) as [Vlr Total], -- Total Premium  
 dbo.fun_FormatCurrency(ABS(ISNULL(Cast((((f.COINSURANCE_PERCENT)*d.TOTAL_TRAN_PREMIUM)/100) AS decimal(10,2)),0)),2) AS [Vlr Ced Cosseg], -- Ceded COI Premium    
 dbo.fun_FormatCurrency(ABS(ISNULL(((((f.COINSURANCE_PERCENT)*d.TOTAL_TRAN_PREMIUM)/100)*f.COINSURANCE_FEE)/100,0)),2) AS [Vlr Com Cosseguro], -- COI Commission Amount 
 dbo.fun_FormatCurrency(ABS(ISNULL((SELECT SUM(REIN_PREMIUM) FROM POL_REINSURANCE_BREAKDOWN_DETAILS WHERE CUSTOMER_ID= a.CUSTOMER_ID and POLICY_ID=a.POLICY_ID and POLICY_VERSION_ID=a.POLICY_VERSION_ID),0)),2) AS [Vlr Ced Resseg],  
 dbo.fun_FormatCurrency(ABS(ISNULL((SELECT SUM(COMM_AMOUNT) FROM POL_REINSURANCE_BREAKDOWN_DETAILS WHERE CUSTOMER_ID= a.CUSTOMER_ID and POLICY_ID=a.POLICY_ID and POLICY_VERSION_ID=a.POLICY_VERSION_ID),0)),2) AS [Vlr Com Resseguro],  
 dbo.fun_FormatCurrency(ABS(ISNULL(TEMP.COMM,0)),2) AS [Vlr Com],-- Broker Commission Amount  
 dbo.fun_FormatCurrency(ABS(ISNULL(TEMP.[ENFEE/PRLBR],0)),2)  AS [Vlr Com de Agenciamento e Pro-labore] --Enrollment Fee and ProLabore Amount
FROM POL_CUSTOMER_POLICY_LIST a  
INNER JOIN POL_POLICY_PROCESS b ON a.CUSTOMER_ID=b.CUSTOMER_ID  AND a.POLICY_ID=b.POLICY_ID  AND a.POLICY_VERSION_ID=b.NEW_POLICY_VERSION_ID   
INNER JOIN MNT_LOB_MASTER c ON a.POLICY_LOB=c.LOB_ID  
INNER JOIN ACT_POLICY_INSTALL_PLAN_DATA d  ON a.CUSTOMER_ID=d.CUSTOMER_ID AND a.POLICY_ID=d.POLICY_ID AND a.POLICY_VERSION_ID=d.POLICY_VERSION_ID  
LEFT OUTER JOIN POL_POLICY_ENDORSEMENTS e  ON b.CUSTOMER_ID=e.CUSTOMER_ID AND b.POLICY_ID=e.POLICY_ID AND b.NEW_POLICY_VERSION_ID=e.POLICY_VERSION_ID 
LEFT OUTER JOIN POL_CO_INSURANCE f ON a.CUSTOMER_ID=f.CUSTOMER_ID AND a.POLICY_ID=f.POLICY_ID AND a.POLICY_VERSION_ID=f.POLICY_VERSION_ID  AND F.LEADER_FOLLOWER = 14549 AND a.CO_INSURANCE = 14548
--INNER JOIN ACT_AGENCY_OPEN_ITEMS g ON a.CUSTOMER_ID=g.CUSTOMER_ID AND a.POLICY_ID=g.POLICY_ID AND a.POLICY_VERSION_ID=g.POLICY_VERSION_ID  
LEFT OUTER JOIN (SELECT [1] AS [ENFEE/PRLBR] ,[2] AS [COMM],CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID  
FROM
(SELECT CASE WHEN COMMISSION_TYPE IN('ENFEE','PRLBR') THEN 1
  ELSE 2 END AS COMMISSION_TYPE,AGENCY_COMM_AMT ,CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID  
    FROM ACT_AGENCY_OPEN_ITEMS) AS SourceTable
PIVOT
(
sum(AGENCY_COMM_AMT)
FOR COMMISSION_TYPE IN ([1],[2])
) AS PivotTable) AS TEMP ON TEMP.CUSTOMER_ID = A.CUSTOMER_ID AND TEMP.POLICY_ID = A.POLICY_ID AND TEMP.POLICY_VERSION_ID = A.POLICY_VERSION_ID  
WHERE  
 b.PROCESS_STATUS='Complete'  
 AND b.PROCESS_ID in (14,25) -- fOR ENDORSEMENT & NBS  
 --AND b.COMPLETED_DATETIME >= @DATETIME  
 AND b.COMPLETED_DATETIME BETWEEN  @FROM_DATETIME  AND @TO_DATETIME
ORDER BY  c.SUSEP_LOB_CODE
END  