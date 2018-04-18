/*---------------------------------------------------------------    
Proc Name          : dbo.[DELETE_POL_SUP_FORM_OLD_BLD]   
Created by      : Amit Kr. Mishra            
Date            : 26/11/2011                        
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[DELETE_POL_SUP_FORM_OLD_BLD]    
------   ------------       -------------------------*/           
-- drop proc dbo.[DELETE_POL_SUP_FORM_OLD_BLD]  

CREATE PROC dbo.[DELETE_POL_SUP_FORM_OLD_BLD]  
(  
@CUSTOMER_ID    [INT],  
@POLICY_ID     [INT],  
@POLICY_VERSION_ID   [SMALLINT],  
@LOCATION_ID    [SMALLINT],  
@PREMISES_ID    [INT],  
@OLDBLD_ID    [INT]   
)  
AS  
BEGIN  
DELETE   
   FROM   
     POL_SUP_FORM_OLD_BLD   
 WHERE   
   CUSTOMER_ID    =@CUSTOMER_ID       AND  
   POLICY_ID    =@POLICY_ID         AND  
   POLICY_VERSION_ID  =@POLICY_VERSION_ID AND  
   LOCATION_ID    =@LOCATION_ID       AND  
   PREMISES_ID    =@PREMISES_ID       AND  
   OLDBLD_ID   =@OLDBLD_ID        
END 