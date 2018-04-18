IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_ClaimRecrVehiclesByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_ClaimRecrVehiclesByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name   : dbo.Proc_Get_ClaimRecrVehiclesByID                       
Created by  : Sumit Chhabra                        
Date        : 11/08/2006  
Purpose     :                         
Revison History  :              
        
Modified By   :   
Modified On   :   
Purpose       :   
                       
 ------------------------------------------------------------                                    
Date     Review By          Comments                                  
                         
------   ------------       -------------------------*/                  
--drop proc Proc_Get_ClaimRecrVehiclesByID                          
CREATE     PROC dbo.Proc_Get_ClaimRecrVehiclesByID                
(                
                 
 @CLAIM_ID Int,                             
 @REC_VEH_ID SmallInt                
)                
                
As                
BEGIN                
SELECT                 
 CLAIM_ID,                        
 REC_VEH_ID,                
 COMPANY_ID_NUMBER,                
 YEAR,                
 MAKE,                
 MODEL,                
 SERIAL,                
 STATE_REGISTERED,    
 HORSE_POWER,                
 REMARKS,                         
 ACTIVE,
 VEHICLE_TYPE              
      
FROM CLM_RECREATIONAL_VEHICLES   
WHERE CLAIM_ID = @CLAIM_ID AND                
      REC_VEH_ID = @REC_VEH_ID                    
    
END    
  



GO

