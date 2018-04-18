IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRemainingLocations_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRemainingLocations_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*        
----------------------------------------------------------            
Proc Name       : dbo.Proc_GetRemainingLocations_FOR_POLICY        
Created by      : shafi          
Date            : 22 may 2006     
Purpose         : Selects the remaining locations/subloations for an policy        
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------           
*/        

CREATE      PROCEDURE Proc_GetRemainingLocations_FOR_POLICY        
(        
 @CUSTOMER_ID Int,        
 @POLICY_ID Int,        
 @POLICY_VERSION_ID smallint        
)        
        
As        
        
        
 SELECT AL.LOCATION_ID,        
  CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +            
  ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +           
  ISNULL(AL.LOC_CITY,'') + ', ' +           
         ISNULL(SL.STATE_NAME,'') + ' ' +           
         ISNULL(AL.LOC_ZIP,'')       
       
   as Address--,           
 FROM POL_LOCATIONS AL        
-- LEFT OUTER JOIN POL_SUB_LOCATIONS ASL ON        
--  AL.LOCATION_ID = ASL.LOCATION_ID         
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON        
  AL.LOC_COUNTRY = SL.COUNTRY_ID AND        
  AL.LOC_STATE = SL.STATE_ID        
 WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND        
       AL.POLICY_ID = @POLICY_ID AND        
       AL.POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
       AL.IS_ACTIVE = 'Y' AND           
   AL.LOCATION_ID NOT  IN    
  (        
  SELECT        
   LOCATION_ID FROM POL_DWELLINGS_INFO  
   WHERE CUSTOMER_ID =  @CUSTOMER_ID AND  
 POLICY_ID = @POLICY_ID AND  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND
 IS_ACTIVE='Y'
  
)         
        
        
        
        
        
        
      
    
  





GO

