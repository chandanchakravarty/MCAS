IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[FETCH_ACCIDENT]'))
DROP VIEW [dbo].[FETCH_ACCIDENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.FETCH_ACCIDENT AS                       
                      
                      
SELECT                       
CUSTOMER_ID, LOB, null as POLICY_ID ,null as POLICY_VERSION_ID, OCCURENCE_DATE,                  
LOSS_TYPE, CAST(AT_FAULT AS VARCHAR) AT_FAULT,DRIVER_ID,DRIVER_NAME,ISNULL(CHARGEABLE,0) AS CHARGEABLE ,AMOUNT_PAID as PAID_LOSS, NULL AS DRIVER_DOB ,AMOUNT_PAID      
FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)        
WHERE AMOUNT_PAID > 499.99--999.99 -->=1000.00  --Itrack # 5081                            

          
                      
UNION                      
                      
SELECT C.CUSTOMER_ID,LOB_ID AS LOB,C.POLICY_ID ,C.POLICY_VERSION_ID, LOSS_DATE AS OCCURENCE_DATE,                  
NULL as LOSS_TYPE,case isnull(AT_FAULT_INDICATOR,0) when 2 then '10963' else '10964' end  AS AT_FAULT,
D.DRIVER_ID,ISNULL(CDI.NAME,'')  AS DRIVER_NAME, 0 AS CHARGEABLE ,      
C.PAID_LOSS ,DATE_OF_BIRTH AS DRIVER_DOB ,C.PAID_LOSS as AMOUNT_PAID      
           
FROM CLM_CLAIM_INFO C WITH (NOLOCK)                  
      
JOIN CLM_INSURED_VEHICLE V  WITH (NOLOCK)                          
ON C.CLAIM_ID = V.CLAIM_ID              
      
JOIN POL_VEHICLES P  WITH (NOLOCK)            
ON C.CUSTOMER_ID = P.CUSTOMER_ID AND              
C.POLICY_ID = P.POLICY_ID AND              
C.POLICY_VERSION_ID = P.POLICY_VERSION_ID AND              
V.POLICY_VEHICLE_ID = P.VEHICLE_ID              
      
JOIN POL_DRIVER_DETAILS D WITH (NOLOCK)                           
ON P.CUSTOMER_ID = D.CUSTOMER_ID AND              
P.POLICY_ID = D.POLICY_ID AND              
P.POLICY_VERSION_ID = D.POLICY_VERSION_ID               
--AND P.VEHICLE_ID = D.VEHICLE_ID              
      
JOIN CLM_DRIVER_INFORMATION CDI WITH (NOLOCK)      
ON CDI.CLAIM_ID=C.CLAIM_ID AND D.DRIVER_DOB=CDI.DATE_OF_BIRTH      
      
AND (ISNULL(ltrim(rtrim(D.DRIVER_FNAME)),'') + ' ' + ISNULL(rtrim(ltrim(D.DRIVER_LNAME)),'')      
 =rtrim(ltrim(CDI.NAME))      
 OR      
 ISNULL(ltrim(rtrim(D.DRIVER_FNAME)),'')       
 + ' ' +       
 isnull(rtrim(ltrim(D.DRIVER_MNAME)),'') +  CASE WHEN len(isnull(rtrim(ltrim(D.DRIVER_MNAME)),''))>0 THEN ' ' ELSE '' END       
 +  ISNULL(rtrim(ltrim(D.DRIVER_LNAME)),'')      
 =rtrim(ltrim(CDI.NAME))      
)      
AND VEHICLE_OWNER in (14151,11753) --rated driver      
      
WHERE ISNULL(C.IS_ACTIVE,'Y')='Y' AND              
ISNULL(P.IS_ACTIVE,'Y')='Y' AND              
ISNULL(D.IS_ACTIVE,'Y')='Y'       
AND C.PAID_LOSS > 499.99-->999.99 -->= 1000.00    --Itrack # 5081            
and isnull(AT_FAULT_INDICATOR,0) =2  -- at fault      

GO

