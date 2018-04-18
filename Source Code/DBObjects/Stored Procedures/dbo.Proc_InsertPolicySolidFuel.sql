IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicySolidFuel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicySolidFuel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertPolicySolidFuel      
Created by      : Anurag Verma
Date            : 11/18/2005      
Purpose         : To add record in policy solid fuel table      
Revison History :      
------   ------------       -------------------------*/      
CREATE    PROC dbo.Proc_InsertPolicySolidFuel      
(      
 @CUSTOMER_ID        int,      
 @POL_ID            int,      
 @POL_VERSION_ID         smallint,      
 @LOCATION_ID            smallint,      
 @SUB_LOC_ID             smallint,        
 @MANUFACTURER           nvarchar(200),      
 @BRAND_NAME             nvarchar(150),      
  @MODEL_NUMBER           nvarchar(70) ,      
 @FUEL                   nvarchar(70),      
 @STOVE_TYPE             nvarchar(5),      
 @HAVE_LABORATORY_LABEL  nchar(1),      
 @IS_UNIT                nvarchar(5),      
 @UNIT_OTHER_DESC        nvarchar(400) ,      
 @CONSTRUCTION           nvarchar(5),      
 @LOCATION               nvarchar(5) ,      
 @LOC_OTHER_DESC         nvarchar(400),      
 @YEAR_DEVICE_INSTALLED  smallint,      
 @WAS_PROF_INSTALL_DONE  nchar(1),      
 @INSTALL_INSPECTED_BY   nvarchar(5),      
 @INSTALL_OTHER_DESC     nvarchar(400),      
 @HEATING_USE            nvarchar(5),      
 @HEATING_SOURCE         nvarchar(5),      
 @OTHER_DESC             nvarchar(400),      
 @CREATED_BY             int,      
 @CREATED_DATETIME       datetime ,
 @STOVE_INSTALLATION_CONFORM_SPECIFICATIONS int,
 @FUEL_ID                smallint OUTPUT     
        
)      
AS      
      
BEGIN      
      
  SELECT @FUEL_ID = ISNULL(Max(FUEL_ID),0)+1      
   FROM POL_HOME_OWNER_SOLID_FUEL  where customer_id=@CUSTOMER_ID and policy_id=@POL_ID and policy_version_id=@POL_VERSION_ID    
      
 INSERT INTO POL_HOME_OWNER_SOLID_FUEL      
  (      
 FUEL_ID,    
  CUSTOMER_ID ,      
  POLICY_ID  ,      
  POLICY_VERSION_ID   ,       
  LOCATION_ID,                      
  SUB_LOC_ID ,                            
  MANUFACTURER,                         
  BRAND_NAME,                          
  MODEL_NUMBER ,                         
  FUEL ,                                
  STOVE_TYPE,                          
  HAVE_LABORATORY_LABEL  ,            
  IS_UNIT,                                
  UNIT_OTHER_DESC ,                    
  CONSTRUCTION  ,                        
  LOCATION ,                            
  LOC_OTHER_DESC  ,                    
  YEAR_DEVICE_INSTALLED ,              
  WAS_PROF_INSTALL_DONE,                   
  INSTALL_INSPECTED_BY,                 
  INSTALL_OTHER_DESC ,        
  HEATING_USE  ,                        
  HEATING_SOURCE,      
  OTHER_DESC,      
  CREATED_BY,      
  CREATED_DATETIME,
  IS_ACTIVE,
  STOVE_INSTALLATION_CONFORM_SPECIFICATIONS	      	      
          
  )         
 values  (      
 @FUEL_ID,    
  @CUSTOMER_ID,      
  @POL_ID ,        
  @POL_VERSION_ID ,             
  @LOCATION_ID,                          
  @SUB_LOC_ID ,      
  @MANUFACTURER,                                
  @BRAND_NAME,                          
  @MODEL_NUMBER ,      
  @FUEL ,               
  @STOVE_TYPE,                          
  @HAVE_LABORATORY_LABEL  ,             
  @IS_UNIT,                                
  @UNIT_OTHER_DESC ,      
  @CONSTRUCTION  ,             
  @LOCATION ,            
  @LOC_OTHER_DESC  ,      
  @YEAR_DEVICE_INSTALLED ,      
  @WAS_PROF_INSTALL_DONE,                   
  @INSTALL_INSPECTED_BY,               
  @INSTALL_OTHER_DESC ,                 
  @HEATING_USE  ,      
  @HEATING_SOURCE,      
  @OTHER_DESC,      
      
  @CREATED_BY,      
  @CREATED_DATETIME,
 'Y',
  @STOVE_INSTALLATION_CONFORM_SPECIFICATIONS	      	      
        
        )      
                    
      
END    
    
    
  







GO

