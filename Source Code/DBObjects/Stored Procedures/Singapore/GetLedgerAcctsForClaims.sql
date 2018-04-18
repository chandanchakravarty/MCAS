 -- drop proc GetLedgerAcctsForClaims      
CREATE PROC dbo.GetLedgerAcctsForClaims        
as        
--       
-- SELECT  ACC_DESCRIPTION,ACC_NUMBER,      
-- CASE       
-- WHEN ACC_PARENT_ID IS NULL      
--  THEN CAST(ACC_NUMBER AS VARCHAR) + ' - ' + ACC_DESCRIPTION       
--  ELSE CAST(ACC_DISP_NUMBER AS VARCHAR) + ' - ' + ACC_DESCRIPTION       
-- END AS DESC_TO_SHOW       
-- FROM ACT_GL_ACCOUNTS WHERE ACC_RELATES_TO_TYPE IN ('11200','11199') AND IS_ACTIVE='Y'     
         
SELECT t1.ACC_DISP_NUMBER as ACC_NUMBER,      
case when t1.acc_parent_id is null       
 then isnull(t1.ACC_DISP_NUMBER,'') + ': ' + t1.ACC_DESCRIPTION        
 else isnull(t1.ACC_DISP_NUMBER,'') + ': ' + isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION       
 end as DESC_TO_SHOW, t1.ACCOUNT_ID      
FROM ACT_GL_ACCOUNTS t1       
LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id       
WHERE t1.ACC_RELATES_TO_TYPE IN ('11200') AND t1.IS_ACTIVE='Y'      
ORDER BY DESC_TO_SHOW  

select * from ACT_GL_ACCOUNTS 
  
  
  
  
       
    
    
      
      
    
    
    