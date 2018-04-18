IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationsForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationsForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*              
----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetLocationsForPolicy              
Created by      : Anurag verma    
Date            : 11 Nov,2005                  
Purpose         : Selects the location for an Policy    
Revison History :                  
Used In         : Wolverine         
                    
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------                 
*/              
              
CREATE  PROCEDURE Proc_GetLocationsForPolicy    
(              
 @CUSTOMER_ID Int,              
 @POL_ID Int,              
 @POL_VERSION_ID smallint,      
 @DWELLING_ID  int=null              
)              
              
As              
              
SELECT AL.LOCATION_ID,
	   convert(varchar(10),AL.LOCATION_ID) + '^' + convert(varchar(10),isnull(AL.LOCATION_TYPE,'0')) as LOCATIONID,                  
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +               
       ISNULL(AL.LOC_CITY,'') + ', ' +               
       ISNULL(SL.STATE_NAME,'') + ' ' +               
       ISNULL(AL.LOC_ZIP,'')           
           
 as Address,              
/* Added by nidhi. we need this field for subject of insurance screen */              
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +               
       ISNULL(AL.LOC_CITY,'') + ', ' +               
       ISNULL(SL.STATE_NAME,'') + ' ' +               
       ISNULL(AL.LOC_ZIP,'') as Address1            
/* ---- */              
             
FROM POL_LOCATIONS AL              
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 AL.LOC_COUNTRY = SL.COUNTRY_ID AND              
 AL.LOC_STATE = SL.STATE_ID              
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND              
      AL.POLICY_ID = @POL_ID AND              
      AL.POLICY_VERSION_ID = @POL_VERSION_ID AND              
      AL.IS_ACTIVE = 'Y' AND        
      AL.LOCATION_ID NOT IN (SELECT DISTINCT LOCATION_ID         
                              FROM POL_DWELLINGS_INFO         
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND         
                                    POLICY_ID=@POL_ID AND                                  
        POLICY_VERSION_ID=@POL_VERSION_ID)        
UNION      
SELECT AL.LOCATION_ID,
	   convert(varchar(10),AL.LOCATION_ID) + '^' + convert(varchar(10),isnull(AL.LOCATION_TYPE,'0')) as LOCATIONID,                       
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +               
       ISNULL(AL.LOC_CITY,'') + ', ' +               
       ISNULL(SL.STATE_NAME,'') + ' ' +               
       ISNULL(AL.LOC_ZIP,'')           
           
 as Address,              
/* Added by nidhi. we need this field for subject of insurance screen */              
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +               
       ISNULL(AL.LOC_CITY,'') + ', ' +               
       ISNULL(SL.STATE_NAME,'') + ' ' +               
       ISNULL(AL.LOC_ZIP,'') as Address1            
/* ---- */              
             
FROM POL_LOCATIONS AL              
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 AL.LOC_COUNTRY = SL.COUNTRY_ID AND              
 AL.LOC_STATE = SL.STATE_ID              
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND              
      AL.POLICY_ID = @POL_ID AND              
      AL.POLICY_VERSION_ID = @POL_VERSION_ID AND              
      AL.IS_ACTIVE = 'Y'         
AND        
    AL.LOCATION_ID IN (SELECT DISTINCT LOCATION_ID         
                              FROM POL_DWELLINGS_INFO         
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND         
                                    POLICY_ID=@POL_ID AND   
        POLICY_VERSION_ID=@POL_VERSION_ID AND DWELLING_ID=@DWELLING_ID)          
      
      
      
      
      
     
      
                
            
            
          
        
        
        
      
    
    
    
    
    
    
  







GO

