IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetCLM_PROPERTY_DAMAGED        
Created by      : Sumit Chhabra          
Date            : 16 May,2006            
Purpose         : Selects records from CLM_PROPERTY_DAMAGED for a property damaged id and claim id      
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------   */        
        
CREATE PROCEDURE dbo.Proc_GetCLM_PROPERTY_DAMAGED        
(        
 @PROPERTY_DAMAGED_ID int,        
 @CLAIM_ID int       
)        
        
As        
BEGIN        
 SELECT      
  DAMAGED_ANOTHER_VEHICLE,NON_OWNED_VEHICLE,VEHICLE_ID,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,PLATE_NUMBER,      
  DESCRIPTION,OTHER_INSURANCE,AGENCY_NAME,POLICY_NUMBER,IS_ACTIVE,OWNER_ID,DRIVER_ID,ESTIMATE_AMOUNT,
  PROP_DAMAGED_TYPE,ADDRESS1,ADDRESS2,CITY,STATE,ZIP,COUNTRY,ISNULL(PARTY_TYPE,'') AS PARTY_TYPE,
  ISNULL(PARTY_TYPE_DESC,'') AS PARTY_TYPE_DESC      
 FROM      
  CLM_PROPERTY_DAMAGED      
 WHERE      
  PROPERTY_DAMAGED_ID = @PROPERTY_DAMAGED_ID AND      
   CLAIM_ID =  @CLAIM_ID       
       
      
END        
      
    
  







GO

