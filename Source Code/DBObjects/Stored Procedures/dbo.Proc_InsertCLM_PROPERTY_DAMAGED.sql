IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_InsertCLM_PROPERTY_DAMAGED                
Created by      : Sumit Chhabra                  
Date            : 16 May,2006                    
Purpose         : Inserts records int CLM_PROPERTY_DAMAGED               
Revison History :                    
Used In         : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------   */   
--drop proc dbo.Proc_InsertCLM_PROPERTY_DAMAGED               
                
CREATE PROCEDURE [dbo].[Proc_InsertCLM_PROPERTY_DAMAGED]                
(                
@PROPERTY_DAMAGED_ID int OUTPUT,              
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
@CREATED_BY int,              
@CREATED_DATETIME datetime,          
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
 SELECT @PROPERTY_DAMAGED_ID = ISNULL(MAX(PROPERTY_DAMAGED_ID),0) + 1 FROM CLM_PROPERTY_DAMAGED WHERE CLAIM_ID=@CLAIM_ID              
 INSERT INTO               
  CLM_PROPERTY_DAMAGED              
  (              
 PROPERTY_DAMAGED_ID,              
 CLAIM_ID,              
 DAMAGED_ANOTHER_VEHICLE,              
 NON_OWNED_VEHICLE,              
 VEHICLE_ID,              
 VEHICLE_YEAR,              
 MAKE,              
 MODEL,              
 VIN,              
 BODY_TYPE,              
 PLATE_NUMBER,              
 DESCRIPTION,              
 OTHER_INSURANCE,              
 AGENCY_NAME,              
 POLICY_NUMBER,              
 IS_ACTIVE,              
 CREATED_BY,              
 CREATED_DATETIME,          
 OWNER_ID,          
 DRIVER_ID,        
 ESTIMATE_AMOUNT,  
 PROP_DAMAGED_TYPE,  
 ADDRESS1,  
 ADDRESS2,  
 CITY,  
 STATE,  
 ZIP,  
 COUNTRY,  
    PARTY_TYPE,  
 PARTY_TYPE_DESC         
  )              
 VALUES              
 (              
 @PROPERTY_DAMAGED_ID,              
 @CLAIM_ID,              
 @DAMAGED_ANOTHER_VEHICLE,              
 @NON_OWNED_VEHICLE,              
 @VEHICLE_ID,              
 @VEHICLE_YEAR,              
 @MAKE,              
 @MODEL,              
 @VIN,              
 @BODY_TYPE,              
 @PLATE_NUMBER,              
 @DESCRIPTION,              
 @OTHER_INSURANCE,              
 @AGENCY_NAME,              
 @POLICY_NUMBER,              
 'Y',              
 @CREATED_BY,              
 @CREATED_DATETIME,          
 @OWNER_ID,          
 @DRIVER_ID,        
 @ESTIMATE_AMOUNT,  
 @PROP_DAMAGED_TYPE,  
 @ADDRESS1,  
 @ADDRESS2,  
 @CITY,  
 @STATE,  
 @ZIP,  
 @COUNTRY,  
    @PARTY_TYPE,  
 @PARTY_TYPE_DESC                   
 )             
              
END  
  
  
  
  
  
  
  
  
GO

