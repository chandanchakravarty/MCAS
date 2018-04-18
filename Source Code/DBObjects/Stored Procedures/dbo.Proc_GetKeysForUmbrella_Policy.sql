IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForUmbrella_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForUmbrella_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                                        
----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GetKeysForUmbrella_POLICY                                        
Created by      : Pravesh      
Date            : 12 Oct 2006          
Purpose         :             
Revison History :             
Modify By  : Pravesh Chandel
Date            : 10 Nov 2006          
Purpose         :       TO Change Policy_LOB       from Text To no.

Proc_GetKeysForUmbrella_APP 837,139,1,0    
drop proc   dbo.Proc_GetKeysForUmbrella_Policy
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------                                           
*/             

CREATE procEDURE dbo.Proc_GetKeysForUmbrella_Policy        
(            
 @CUSTOMER_ID INT,            
 @POLICY_ID INT,            
 @POLICY_VERSION_ID INT,            
 @BOAT_ID INT            
)            
           
AS            
BEGIN            
SELECT            
POL_INFO.APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,            
POL_INFO.POLICY_LOB AS LOB_ID,            
POL_INFO.STATE_ID as STATE_ID,        
YEAR(CONVERT(VARCHAR(20),POL_INFO.APP_EFFECTIVE_DATE,109)) AS APP_YEAR,      
CASE ISNULL(HAVE_NON_OWNED_AUTO_POL,'') WHEN 'Y' THEN 1 ELSE 0 END HAVE_NON_OWNED,      
(    
 select count(*) from POL_umbrella_underlying_policies WITH(NOLOCK)      
 where customer_id=@CUSTOMER_ID and       
 POLICY_id=@POLICY_ID and       
 POLICY_version_id=@POLICY_VERSION_ID       
 and policy_lob in(2,3)   ----- ('Automobile','Motorcycle')    
) ALE,      
CASE ISNULL(POL_GEN_INFO.AUTO_CYCL_TRUCKS,'') WHEN 'Y' THEN 1 ELSE 0 END AUTO_CYCL_TRUCKS,      
CASE ISNULL(POL_GEN_INFO.RECR_VEH,'')  WHEN 'Y' THEN 1 ELSE 0 END RECR_VEH,      
CASE ISNULL(POL_GEN_INFO.WAT_DWELL,'') WHEN 'Y' THEN 1 ELSE 0 END WAT_DWELL,      
(    
 SELECT COUNT(CUSTOMER_ID) FROM POL_UMBRELLA_REAL_ESTATE_LOCATION  WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND       
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_ACTIVE='Y' and      
 LOC_EXCLUDED=10963  
) LOC_EXCLUDED ,    
(    
 SELECT COUNT(*) FROM POL_UMBRELLA_DRIVER_DETAILS  WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND       
 POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  IS_ACTIVE='Y' and    
 DRIVER_DRIV_TYPE='3477'    
) DRIV_TYPE ,  

ISNULL((SELECT COUNT(HAS_MOTORIST_PROTECTION) FROM  POL_UMBRELLA_UNDERLYING_POLICIES WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(HAS_MOTORIST_PROTECTION,0)=1 ),0)          
HAS_MOTORIST_PROTECTION,          

ISNULL((SELECT COUNT(LOWER_LIMITS) FROM  POL_UMBRELLA_UNDERLYING_POLICIES WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(LOWER_LIMITS,0)=1 ),0)          
     LOWER_LIMITS,         

ISNULL((SELECT COUNT(IS_BOAT_EXCLUDED) FROM  POL_UMBRELLA_WATERCRAFT_INFO  WITH(NOLOCK)          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_BOAT_EXCLUDED,0)='10963'),0)           
IS_BOAT_EXCLUDED,        
      
ISNULL((SELECT COUNT(IS_EXCLUDED) FROM  POL_UMBRELLA_VEHICLE_INFO WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_EXCLUDED,0)=1),0)           
IS_VEHICLE_EXCLUDED,

CASE(SELECT COUNT(CUSTOMER_ID) FROM POL_UMBRELLA_RECREATIONAL_VEHICLES  WITH(NOLOCK)        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_BOAT_EXCLUDED=10963)      
  WHEN 0 THEN 0       
 ELSE 1 END 
IS_RV_EXCLUDED         
    
FROM
POL_CUSTOMER_POLICY_LIST POL_INFO  WITH(NOLOCK)            
LEFT OUTER JOIN       
POL_UMBRELLA_GEN_INFO POL_GEN_INFO WITH(NOLOCK)      
ON POL_GEN_INFO.CUSTOMER_ID=POL_INFO.CUSTOMER_ID AND      
POL_GEN_INFO.POLICY_ID=POL_INFO.POLICY_ID AND      
POL_GEN_INFO.POLICY_VERSION_ID=POL_INFO.POLICY_VERSION_ID     
WHERE            
    POL_INFO.CUSTOMER_ID = @CUSTOMER_ID            
AND POL_INFO.POLICY_ID=@POLICY_ID            
AND POL_INFO.POLICY_VERSION_ID  = @POLICY_VERSION_ID            
END         
    
            
           
            
            
            
            
            
          
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
      
    
  












GO

