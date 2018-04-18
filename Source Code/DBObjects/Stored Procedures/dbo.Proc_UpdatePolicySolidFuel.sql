IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicySolidFuel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicySolidFuel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdatePolicySolidFuel  
Created by      : Anurag Verma  
Date            : 11/18/2005  
Purpose         : To update record in policy solid fuel table  
Revison History :  
Used In         :   Wolverine  
------   ------------       -------------------------*/  
CREATE    PROC dbo.Proc_UpdatePolicySolidFuel  
(  
 @CUSTOMER_ID          int,  
 @POL_ID            int,  
 @POL_VERSION_ID         smallint,  
 @FUEL_ID                smallint,  
 @LOCATION_ID            smallint,  
 @SUB_LOC_ID             smallint,   
 @MANUFACTURER           nvarchar(200),  
 @BRAND_NAME             nvarchar(150),  
 @MODEL_NUMBER           nvarchar(70),  
 @FUEL                   nvarchar(70),  
 @STOVE_TYPE             nvarchar(5),  
 @HAVE_LABORATORY_LABEL   nchar(1),  
 @IS_UNIT                nvarchar(5),  
 @UNIT_OTHER_DESC        nvarchar(400),  
 @CONSTRUCTION           nvarchar(5),  
 @LOCATION               nvarchar(5),  
 @LOC_OTHER_DESC         nvarchar(400),  
 @YEAR_DEVICE_INSTALLED   smallint,  
 @WAS_PROF_INSTALL_DONE   nchar(1),  
 @INSTALL_INSPECTED_BY    nvarchar(5),  
 @INSTALL_OTHER_DESC      nvarchar(400),  
 @HEATING_USE             nvarchar(5),  
 @HEATING_SOURCE          nvarchar(5),    
 @OTHER_DESC       nvarchar(400),  
        @MODIFIED_BY             int,  
 @LAST_UPDATED_DATETIME   datetime  ,
@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS int
)  
AS  
  
BEGIN  
  
 UPDATE POL_HOME_OWNER_SOLID_FUEL  
  SET   
       
  LOCATION_ID = @LOCATION_ID ,                  
  SUB_LOC_ID =@SUB_LOC_ID  ,                        
  MANUFACTURER = @MANUFACTURER ,                     
  BRAND_NAME = @BRAND_NAME ,                      
  MODEL_NUMBER = @MODEL_NUMBER ,                     
  FUEL = @FUEL ,                            
  STOVE_TYPE = @STOVE_TYPE ,                      
  HAVE_LABORATORY_LABEL = @HAVE_LABORATORY_LABEL  ,        
  IS_UNIT = @IS_UNIT ,                            
  UNIT_OTHER_DESC =@UNIT_OTHER_DESC  ,                
  CONSTRUCTION =  @CONSTRUCTION ,                    
  LOCATION = @LOCATION ,                        
  LOC_OTHER_DESC = @LOC_OTHER_DESC  ,                
  YEAR_DEVICE_INSTALLED = @YEAR_DEVICE_INSTALLED ,          
  WAS_PROF_INSTALL_DONE =@WAS_PROF_INSTALL_DONE  ,               
  INSTALL_INSPECTED_BY = @INSTALL_INSPECTED_BY ,              
  INSTALL_OTHER_DESC =  @INSTALL_OTHER_DESC ,    
  HEATING_USE = @HEATING_USE  ,                    
  HEATING_SOURCE = @HEATING_SOURCE ,  
  OTHER_DESC = @OTHER_DESC,  
  MODIFIED_BY = @MODIFIED_BY,  
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
  STOVE_INSTALLATION_CONFORM_SPECIFICATIONS = @STOVE_INSTALLATION_CONFORM_SPECIFICATIONS 
      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POL_ID AND   
  POLICY_VERSION_ID = @POL_VERSION_ID AND  
  FUEL_ID = @FUEL_ID   
END  
  
  
  
  
    
          
  
   
  
  
  
  
  
  








GO

