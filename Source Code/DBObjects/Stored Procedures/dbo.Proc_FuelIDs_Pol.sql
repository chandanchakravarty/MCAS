IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FuelIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FuelIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name     : dbo.Proc_FuelIDs_Pol            
Created by    : Ashwani            
Date          : 02 Mar 2006        
Purpose       : Get the fuel IDs  for HO policy rules        
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/                      
--DROP PROC Proc_FuelIDs_Pol  
CREATE PROCEDURE dbo.Proc_FuelIDs_Pol            
(            
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID int            
)                
AS                     
BEGIN     
  /*For  SECONDARY HEAT TYPE   
 - Coal Non Professionally Installed   
 - Coal Professionally Installed   
 - Wood Stove - Professionally Installed   
 - Pellet/Corn Burner   
 - Wood Stove - Non Professionally installed   
 - Stove Fireplace Insert   
 - Add on Furnace */                   
 IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  POL_HOME_RATING_INFO                                                                           
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                           
  AND(SECONDARY_HEAT_TYPE=6223 OR SECONDARY_HEAT_TYPE=6224 OR SECONDARY_HEAT_TYPE=6212   
  OR SECONDARY_HEAT_TYPE=6213 OR SECONDARY_HEAT_TYPE=11806 OR SECONDARY_HEAT_TYPE=11807  
  OR SECONDARY_HEAT_TYPE=11808  ))      
 BEGIN    
  SELECT FUEL_ID            
  FROM POL_HOME_OWNER_SOLID_FUEL      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID           
  AND IS_ACTIVE='Y'             
  ORDER BY  FUEL_ID                        
 END    
 /*IF EXISTS(SELECT ANY_HEATING_SOURCE FROM POL_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID     
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND ANY_HEATING_SOURCE=1  )     
 BEGIN        
  SELECT FUEL_ID            
  FROM POL_HOME_OWNER_SOLID_FUEL      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID           
  AND IS_ACTIVE='Y'             
  ORDER BY  FUEL_ID    
 END    */ 
   
  END   
  
  
  
  


GO

