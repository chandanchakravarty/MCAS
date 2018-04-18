IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_FORM_66_PROVISIONS_OF_CLAIMS_TO_SETTLE_WITH_LOCAL_REINSURERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_FORM_66_PROVISIONS_OF_CLAIMS_TO_SETTLE_WITH_LOCAL_REINSURERS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  ----                                                                      
Proc Name       : dbo.PROC_SUSEP_FORM_66_PROVISIONS_OF_CLAIMS_TO_SETTLE_WITH_LOCAL_REINSURERS                                                                  
Created by      : ANKIT KUMAR GOEL                                                                    
Date            : 12/04/2011                                                                        
Purpose         :      
Revison History :                                                 
------------------------------------------------------------                                                                        
Modified By  :         
Date   :      
Purpose  :       
------------------------------------------------------------ ---                                                                             
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/ 

CREATE PROC [dbo].[PROC_SUSEP_FORM_66_PROVISIONS_OF_CLAIMS_TO_SETTLE_WITH_LOCAL_REINSURERS] --'11-feb-2010'

@DATETIME DATETIME = NULL,        
@POLICY_NUMBER VARCHAR(22)= NULL

AS

BEGIN 

CREATE TABLE #CLAIM_TEMP
(
ID INT IDENTITY(1,1),
CLAIM_ID INT,
ACTIVITY_ID INT 
)

INSERT INTO #CLAIM_TEMP
SELECT CAR.CLAIM_ID ,  MAX(CAR.ACTIVITY_ID) 
	FROM CLM_ACTIVITY_RESERVE CAR WITH (NOLOCK)
		WHERE CLAIM_ID IN
			(
				SELECT DISTINCT CLAIM_ID FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB WITH (NOLOCK)
			)
			 GROUP BY CAR.CLAIM_ID--,CAR.ACTIVITY_ID
			 HAVING MAX(CAR.ACTIVITY_ID)>0
    
DECLARE     
@d VARCHAR(20),@F_DATE varchar(max)    
SET @d = DATEADD(MONTH, -1, @DATETIME)    
     
SET @F_DATE    
  = CONVERT(VARCHAR,CONVERT(DATETIME,    
 CAST(datepart(dd, dateadd(dd, -(datepart(dd, dateadd(mm, 1, @d))),dateadd(mm, 1, @d))) AS VARCHAR) + '-' +CAST(MONTH(@d)AS VARCHAR)+ '-'+ CAST(YEAR(@d) AS VARCHAR),    
 103),103)    
    
--PRINT @F_DATE    
SELECT      
    
@F_DATE AS REFERENCE_MONTH,    
 CASE WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) < 31 THEN 'Until 30'    
  WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) BETWEEN 31 AND 60 THEN '31 - 60 Days'    
  WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) BETWEEN 61 AND 90 THEN '61 - 90 Days'    
  WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) BETWEEN 91 AND 120 THEN '91 - 120 Days'    
  WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) BETWEEN 121 AND 180 THEN '121 - 180 Days'    
  WHEN DATEDIFF(DAY,CCI.FIRST_NOTICE_OF_LOSS,convert(datetime,@F_DATE ,103)) > 180 THEN 'More than 180 Days'    
     
 END  AS PERIOD,    
--ISNULL(SUM(TRAN_RESERVE_AMT),0.00),    
ISNULL(CACRB.TRAN_RESERVE_AMT,0.00) AS TRAN_RESERVE_AMT,    
MRCL.REIN_COMAPANY_NAME AS REIN_COMAPANY_NAME    
INTO #TEMP_TABLE    
     
 FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB     
INNER JOIN CLM_ACTIVITY_RESERVE CAR    
 ON CACRB.CLAIM_ID = CAR.CLAIM_ID    
 AND CACRB.ACTIVITY_ID= CAR.ACTIVITY_ID    
 AND CACRB.RESERVE_ID = CAR.RESERVE_ID    
INNER JOIN MNT_REIN_COMAPANY_LIST MRCL    
 ON MRCL.REIN_COMAPANY_ID = CACRB.COMP_ID    
INNER JOIN CLM_CLAIM_INFO CCI    
 ON CCI.CLAIM_ID = CAR.CLAIM_ID    
WHERE CACRB.COMP_TYPE ='RI'     
--AND CAR.OUTSTANDING > 0 
AND ISNULL(CCI.OFFCIAL_CLAIM_NUMBER,'')<>''   
--AND CAR.RI_RESERVE > 0
AND CAR.CLAIM_ID IN 
( SELECT CAR2.CLAIM_ID FROM CLM_ACTIVITY_RESERVE CAR2 WITH (NOLOCK)
				  INNER JOIN CLM_ACTIVITY  CA2 WITH (NOLOCK)   
		ON CA2.CLAIM_ID = CAR2.CLAIM_ID AND CAR2.ACTIVITY_ID = CA2.ACTIVITY_ID     
  WHERE  CAST(CA2.CLAIM_ID AS VARCHAR) + CAST(CA2.ACTIVITY_ID AS VARCHAR)    
     IN (SELECT CAST(CLAIM_ID AS VARCHAR) + CAST(ACTIVITY_ID AS VARCHAR)    
       FROM #CLAIM_TEMP    
       )    
  GROUP BY CAR2.CLAIM_ID , CAR2.ACTIVITY_ID    
  HAVING SUM(CA2.RI_RESERVE)>0    
  )         
 -- inserted by Rodolfo Araujo Ebix LatAm
  and CCI.FIRST_NOTICE_OF_LOSS < @DATETIME 
  -- inserted by Rodolfo Araujo Ebix LatAm    
SELECT REFERENCE_MONTH,    
 REIN_COMAPANY_NAME,    
 ISNULL([Until 30],0.00) [Until 30],ISNULL([31 - 60 Days],0.00) [31 - 60 Days],    
 ISNULL([61 - 90 Days],0.00) [61 - 90 Days],ISNULL([91 - 120 Days],0.00) [91 - 120 Days],    
 ISNULL([121 - 180 Days],0.00) [121 - 180 Days],     
 ISNULL([More than 180 Days],0.00) [More than 180 Days]    
 ,CAST(0 AS DECIMAL(18,2)) AS TOTAL     
 --,[Until 30]+[31 - 60 Days]+[61 - 90 Days]+[91 - 120 Days]+[More than 180 Days] AS TOTAL    
 INTO #TEMP2    
  FROM      
 (    
  SELECT *    
  FROM ( SELECT PERIOD,REFERENCE_MONTH,TRAN_RESERVE_AMT,REIN_COMAPANY_NAME FROM #TEMP_TABLE ) S    
      
   PIVOT     
  (     
   SUM(TRAN_RESERVE_AMT)    
   FOR PERIOD IN ([Until 30],[31 - 60 Days],[61 - 90 Days],[91 - 120 Days],[121 - 180 Days],[More than 180 Days])     
  ) p    
 ) TABLE1    
     
     
 UPDATE #TEMP2    
 SET TOTAL =[Until 30]  + [31 - 60 Days]     
   +[61 - 90 Days] +[91 - 120 Days] + [121 - 180 Days] + [More than 180 Days]     
       
 --SELECT     
 --REFERENCE_MONTH,    
 --REIN_COMAPANY_NAME,    
 --REPLACE(ISNULL([Until 30],0.00),'.',',') [Until 30],REPLACE(ISNULL([31 - 60 Days],0.00),'.',',') [31 - 60 Days],    
 --REPLACE(ISNULL([61 - 90 Days],0.00),'.',',') [61 - 90 Days],REPLACE(ISNULL([91 - 120 Days],0.00),'.',',') [91 - 120 Days],    
 --REPLACE(ISNULL([121 - 180 Days],0.00),'.',',') [121 - 180 Days],    
 --REPLACE(ISNULL([More than 180 Days],0.00),'.',',') [More than 180 Days]    
 --, REPLACE(TOTAL,'.',',') AS TOTAL FROM #TEMP2 

	SELECT       
	 REFERENCE_MONTH AS [DATA DE GERAÇÃO],      
	 REIN_COMAPANY_NAME AS [RESSEGURADORA],      
	 REPLACE(ISNULL([UNTIL 30],0.00),'.',',')   [ATÉ 30],
	 REPLACE(ISNULL([31 - 60 DAYS],0.00),'.',',')  [31 A 60],      
	 REPLACE(ISNULL([61 - 90 DAYS],0.00),'.',',')  [61 A 90],
	 REPLACE(ISNULL([91 - 120 DAYS],0.00),'.',',')  [91 A 120],      
	 REPLACE(ISNULL([121 - 180 DAYS],0.00),'.',',')  [121 A 180],      
	 REPLACE(ISNULL([MORE THAN 180 DAYS],0.00),'.',',') [MAIOR QUE 180],
	 REPLACE(TOTAL,'.',',') AS TOTAL
	 FROM #TEMP2
 
   
   DROP TABLE #CLAIM_TEMP 
   DROP TABLE #TEMP2
   DROP TABLE #TEMP_TABLE
    
END	


GO

