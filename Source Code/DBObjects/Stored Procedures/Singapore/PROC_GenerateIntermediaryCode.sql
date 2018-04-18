/*----------------------------------------------------------        
Proc Name       : PROC_GenerateIntermediaryCode        
Created by      : RUCHIKA CHAUHAN     
Date            : 1 FEB 2012  
Purpose   : Generates Intermediary Code  

Revison History :        
Used In  : EbixAdvantage - Singapore  
        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       --------------------------------*/        
-- DROP PROC PROC_GenerateIntermediaryCode
--/*  
  
  
CREATE PROC PROC_GenerateIntermediaryCode

AS  
  
BEGIN  
  
SELECT CAST(COUNT(agency_code)+1 as nvarchar)FROM MNT_AGENCY_LIST

END  

 --select * from MNT_AGENCY_LIST
