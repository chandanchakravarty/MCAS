IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetKeysForAviation_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetKeysForAviation_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                                      
----------------------------------------------------------                                          
Proc Name       : dbo.Proc_GetKeysForAviation_APP                                      
Created by      : Pravesh K Chandel
Date            : 13 Jan 2010
Purpose         :           
Revison History :         
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------                                         
*/           
-- drop proc Proc_GetKeysForAviation_APP       
CREATE proc [dbo].[Proc_GetKeysForAviation_APP]          
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
APP_INFO.STATE_ID as STATE_ID  ,
VEH_INFO.ENGINE_TYPE,
VEH_INFO.WING_TYPE    
      
FROM APP_AVIATION_VEHICLES VEH_INFO WITH(NOLOCK)          
          
INNER JOIN APP_LIST APP_INFO WITH(NOLOCK)          
 ON APP_INFO.CUSTOMER_ID = VEH_INFO.CUSTOMER_ID          
 AND APP_INFO.APP_ID  = VEH_INFO.APP_ID          
 AND APP_INFO.APP_VERSION_ID  = VEH_INFO.APP_VERSION_ID       
WHERE          
    VEH_INFO.CUSTOMER_ID = @CUSTOMER_ID        
AND VEH_INFO.APP_ID=@APP_ID          
AND VEH_INFO.APP_VERSION_ID  = @APP_VERSION_ID          
AND VEH_INFO.VEHICLE_ID = @BOAT_ID          
END      
       
    
    
    
    



GO

