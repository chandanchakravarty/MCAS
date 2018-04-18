IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForUmbrella_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForUmbrella_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                                        
----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GetKeysForUmbrellaAuto_APP                                        
Created by      : Pravesh      
Date            : 12 Oct 2006          
Purpose         :             
Revison History :             
modify by       : Pravesh Chandel  
Date            : 10 Nov 2006          
Purpose         : To Change Policy_lob fropm text to number            
Proc_GetKeysForUmbrella_APP 626,170,1,0    
  
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------                                           
*/             
     
--drop proc dbo.Proc_GetKeysForUmbrella_APP  1426,34,1,1   
CREATE procEDURE dbo.Proc_GetKeysForUmbrella_APP        
(            
 @CUSTOMER_ID INT,            
 @APP_ID INT,            
 @APP_VERSION_ID INT,            
 @BOAT_ID INT            
)            
           
AS            
BEGIN            
SELECT            
APP_INFO.APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,            
APP_INFO.APP_LOB AS LOB_ID,            
APP_INFO.STATE_ID as STATE_ID,        
YEAR(CONVERT(VARCHAR(20),APP_INFO.APP_EFFECTIVE_DATE,109)) AS APP_YEAR,      
CASE ISNULL(HAVE_NON_OWNED_AUTO_POL,'') WHEN 'Y' THEN 1 ELSE 0 END HAVE_NON_OWNED,      
(    
 select count(*) from app_umbrella_underlying_policies WITH(NOLOCK)      
 where customer_id=@CUSTOMER_ID and       
 app_id=@APP_ID and       
 app_version_id=@APP_VERSION_ID       
 and policy_lob in (2,3)   --------('Automobile','Motorcycle')    
) ALE,      
CASE ISNULL(APP_GEN_INFO.AUTO_CYCL_TRUCKS,'') WHEN 'Y' THEN 1 ELSE 0 END AUTO_CYCL_TRUCKS,      
CASE ISNULL(APP_GEN_INFO.RECR_VEH,'')  WHEN 'Y' THEN 1 ELSE 0 END RECR_VEH,      
CASE ISNULL(APP_GEN_INFO.WAT_DWELL,'') WHEN 'Y' THEN 1 ELSE 0 END WAT_DWELL,      
(    
 SELECT COUNT(CUSTOMER_ID) FROM APP_UMBRELLA_REAL_ESTATE_LOCATION  WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND       
 APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND  IS_ACTIVE='Y' and     
 LOC_EXCLUDED=10963  
) LOC_EXCLUDED ,    
(    
 SELECT COUNT(*) FROM APP_UMBRELLA_DRIVER_DETAILS  WITH(NOLOCK)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND       
 APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE='Y' and       
 DRIVER_DRIV_TYPE='3477'    
) DRIV_TYPE,
ISNULL((SELECT COUNT(HAS_MOTORIST_PROTECTION) FROM  APP_UMBRELLA_UNDERLYING_POLICIES WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(HAS_MOTORIST_PROTECTION,0)=1 ),0)          
HAS_MOTORIST_PROTECTION,          
ISNULL((SELECT COUNT(LOWER_LIMITS) FROM  APP_UMBRELLA_UNDERLYING_POLICIES WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(LOWER_LIMITS,0)=1 ),0)          
     LOWER_LIMITS,         
ISNULL((SELECT COUNT(IS_BOAT_EXCLUDED) FROM  APP_UMBRELLA_WATERCRAFT_INFO  WITH(NOLOCK)          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(IS_BOAT_EXCLUDED,0)='10963'),0)           
IS_BOAT_EXCLUDED,        
      
ISNULL((SELECT COUNT(IS_EXCLUDED) FROM  APP_UMBRELLA_VEHICLE_INFO WITH(NOLOCK)           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(IS_EXCLUDED,0)=1),0)           
IS_VEHICLE_EXCLUDED,
CASE(SELECT COUNT(CUSTOMER_ID) FROM APP_UMBRELLA_RECREATIONAL_VEHICLES  WITH(NOLOCK)        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_BOAT_EXCLUDED=10963)      
  WHEN 0 THEN 0       
 ELSE 1 END 
IS_RV_EXCLUDED         
FROM       
 APP_LIST APP_INFO WITH(NOLOCK)            
LEFT OUTER JOIN       
APP_UMBRELLA_GEN_INFO APP_GEN_INFO WITH(NOLOCK)      
ON APP_GEN_INFO.CUSTOMER_ID=APP_INFO.CUSTOMER_ID AND      
APP_GEN_INFO.APP_ID=APP_INFO.APP_ID AND      
APP_GEN_INFO.APP_VERSION_ID=APP_INFO.APP_VERSION_ID     
WHERE            
    APP_INFO.CUSTOMER_ID = @CUSTOMER_ID            
AND APP_INFO.APP_ID=@APP_ID            
AND APP_INFO.APP_VERSION_ID  = @APP_VERSION_ID            
END         
    


GO

