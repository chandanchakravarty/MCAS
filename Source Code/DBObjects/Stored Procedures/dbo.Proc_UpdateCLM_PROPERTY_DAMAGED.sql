IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateCLM_PROPERTY_DAMAGED          
Created by      : Sumit Chhabra            
Date            : 16 May,2006              
Purpose         : Updates records at CLM_PROPERTY_DAMAGED         
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------   */          
--DROP PROC dbo.Proc_UpdateCLM_PROPERTY_DAMAGED           
CREATE PROCEDURE [dbo].[Proc_UpdateCLM_PROPERTY_DAMAGED]          
(          
@PROPERTY_DAMAGED_ID int,        
@CLAIM_ID int,        
@DAMAGED_ANOTHER_VEHICLE INT,        
@NON_OWNED_VEHICLE char(1),        
@VEHICLE_ID int,        
@VEHICLE_YEAR varchar(4),        
@MAKE varchar(150),        
@MODEL varchar(150),        
@VIN varchar(150),        
@BODY_TYPE varchar(150),        
@PLATE_NUMBER varchar(10),        
@DESCRIPTION varchar(256),        
@OTHER_INSURANCE INT,        
@AGENCY_NAME varchar(50),        
@POLICY_NUMBER varchar(8),        
@MODIFIED_BY int,        
@LAST_UPDATED_DATETIME datetime,      
@OWNER_ID int,      
@DRIVER_ID int,    
@ESTIMATE_AMOUNT decimal(12,2),  
@PROP_DAMAGED_TYPE smallint,  
@ADDRESS1 varchar(50),  
@ADDRESS2 varchar(50),  
@CITY varchar(50),  
@STATE int,  
@ZIP varchar(10),  
@COUNTRY int,  
@PARTY_TYPE smallint,  
@PARTY_TYPE_DESC varchar(300)=null  
)          
          
As          
BEGIN          
         
 UPDATE CLM_PROPERTY_DAMAGED        
  SET            
 DAMAGED_ANOTHER_VEHICLE=@DAMAGED_ANOTHER_VEHICLE,        
 NON_OWNED_VEHICLE=@NON_OWNED_VEHICLE,        
 VEHICLE_ID=@VEHICLE_ID,        
 VEHICLE_YEAR=@VEHICLE_YEAR,        
 MAKE=@MAKE,        
 MODEL=@MODEL,        
 VIN=@VIN,        
 BODY_TYPE=@BODY_TYPE,        
 PLATE_NUMBER=@PLATE_NUMBER,        
 DESCRIPTION=@DESCRIPTION,        
 OTHER_INSURANCE=@OTHER_INSURANCE,        
 AGENCY_NAME=@AGENCY_NAME,        
 POLICY_NUMBER=@POLICY_NUMBER,        
 MODIFIED_BY=@MODIFIED_BY,        
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,      
 OWNER_ID = @OWNER_ID,      
 DRIVER_ID = @DRIVER_ID,    
 ESTIMATE_AMOUNT = @ESTIMATE_AMOUNT,  
 PROP_DAMAGED_TYPE = @PROP_DAMAGED_TYPE,  
 ADDRESS1 = @ADDRESS1,  
 ADDRESS2 = @ADDRESS2,  
 CITY = @CITY,  
 STATE = @STATE,  
 ZIP = @ZIP,  
 COUNTRY = @COUNTRY,  
 PARTY_TYPE = @PARTY_TYPE,  
 PARTY_TYPE_DESC = @PARTY_TYPE_DESC        
 WHERE        
  PROPERTY_DAMAGED_ID=@PROPERTY_DAMAGED_ID AND         
  CLAIM_ID=@CLAIM_ID        
END          
        
      
    
  
  
  
  
  
  
  
  
GO

