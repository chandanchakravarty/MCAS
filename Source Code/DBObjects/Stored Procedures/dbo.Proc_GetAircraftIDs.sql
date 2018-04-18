IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAircraftIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAircraftIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetAircraftIDs      
Created by  : Neeraj      
Date        : 18 October,2010      
Purpose     : Get the Vehicle IDs                 
Revison History  :       
Modified By  :      
Date   :                   
Desc.  :    
 ------------------------------------------------------------                            
Date     Review By          Comments                          
drop proc    dbo.Proc_GetVehicleIDs               
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetAircraftIDs  
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)          
AS               
BEGIN                
      
SELECT   VEHICLE_ID--,ISNULL(MAKE,'') AS MAKE,ISNULL(MODEL,'') AS MODEL,ISNULL(VIN,'') AS VIN, ISNULL(VEHICLE_YEAR,'') AS VEHICLE_YEAR     
FROM       APP_AVIATION_VEHICLES   with(nolock)    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID      
 AND IS_ACTIVE='Y'    
   ORDER BY   VEHICLE_ID   

END      
      
    
    
    
  





GO

