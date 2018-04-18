IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRV_VehicleIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRV_VehicleIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetRV_VehicleIDs_Pol      
Created by  : Manoj Rathore      
Date        : 02 JAN ,2007      
Purpose     : Get the RV_Vehicle IDs                 
   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetRV_VehicleIDs_Pol      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)          
AS               
BEGIN                
      
 SELECT   REC_VEH_ID      
 FROM       POL_HOME_OWNER_RECREATIONAL_VEHICLES       
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  AND ACTIVE='Y'    
 ORDER BY   REC_VEH_ID                  
End      
      


GO

