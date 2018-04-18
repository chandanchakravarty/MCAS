IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLCLM_INSURED_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLCLM_INSURED_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : Proc_GetXMLCLM_INSURED_VEHICLE                  
Created by      : Amar                  
Date            : 5/1/2006                  
Purpose       :Evaluation                  
Revison History :                  
Used In        : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
 --DROP PROC dbo.Proc_GetXMLCLM_INSURED_VEHICLE           
CREATE PROC dbo.Proc_GetXMLCLM_INSURED_VEHICLE                  
(                  
@CLAIM_ID     int      ,            
@INSURED_VEHICLE_ID int        
)                  
AS                  
BEGIN                  
Select INSURED_VEHICLE_ID AS INSURED_VEH_NUMBER,          
CLAIM_ID,                  
NON_OWNED_VEHICLE,                  
VEHICLE_YEAR,                  
MAKE,                  
MODEL,                  
VIN,                  
BODY_TYPE,                  
PLATE_NUMBER,                  
STATE,                  
OWNER_ID As OWNER,                
DRIVER_ID  As DRIVER,      
WHERE_VEHICLE_SEEN,      
WHEN_VEHICLE_SEEN,    
DESCRIBE_DAMAGE,    
USED_WITH_PERMISSION,    
PURPOSE_OF_USE,    
ESTIMATE_AMOUNT,    
OTHER_VEHICLE_INSURANCE,  
POLICY_VEHICLE_ID,
IS_ACTIVE    
From  CLM_INSURED_VEHICLE                  
Where  CLAIM_ID = @CLAIM_ID  AND INSURED_VEHICLE_ID = @INSURED_VEHICLE_ID        
END                  
                  
                
              
            
          
        
      
    
  
  
GO

