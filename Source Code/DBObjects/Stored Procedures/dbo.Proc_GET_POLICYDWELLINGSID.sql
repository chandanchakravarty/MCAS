IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GET_POLICYDWELLINGSID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GET_POLICYDWELLINGSID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_GET_POLICYDWELLINGSID                 
Created by  : shafi                  
Date        : 02 March 2006      
Purpose     : GET THE DWELLINGS's ID                   
Revison History  :                        
 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       -------------------------*/      
--DROP PROC Proc_GET_POLICYDWELLINGSID                
CREATE PROCEDURE dbo.Proc_GET_POLICYDWELLINGSID        
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
)            
AS                 
BEGIN                  
        
SELECT     POL_DWELLINGS_INFO.DWELLING_ID, ISNULL(POL_LOCATIONS.LOC_ADD1,'') + ' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'') + ' ' + ISNULL(POL_LOCATIONS.LOC_CITY,'') + ' ' + ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') + ' ' + ISNULL(POL_LOCATIONS.LOC_ZIP,'')              
FROM         POL_DWELLINGS_INFO INNER JOIN        
                      POL_LOCATIONS ON POL_DWELLINGS_INFO.CUSTOMER_ID = POL_LOCATIONS.CUSTOMER_ID AND         
                      POL_DWELLINGS_INFO.POLICY_ID = POL_LOCATIONS.POLICY_ID AND         
                      POL_DWELLINGS_INFO.POLICY_VERSION_ID = POL_LOCATIONS.POLICY_VERSION_ID AND         
                      POL_DWELLINGS_INFO.LOCATION_ID = POL_LOCATIONS.LOCATION_ID        
                     LEFT OUTER  JOIN MNT_COUNTRY_STATE_LIST with (nolock) ON POL_LOCATIONS.LOC_STATE=MNT_COUNTRY_STATE_LIST.STATE_ID     
   WHERE POL_DWELLINGS_INFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_DWELLINGS_INFO.POLICY_ID=@POLICY_ID AND POL_DWELLINGS_INFO.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND POL_DWELLINGS_INFO.IS_ACTIVE='Y'  
      
   ORDER BY   POL_DWELLINGS_INFO.DWELLING_ID                    
End        
      
    
  
  
  


GO

