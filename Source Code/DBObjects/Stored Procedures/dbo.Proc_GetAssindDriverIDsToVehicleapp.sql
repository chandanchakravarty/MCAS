IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAssindDriverIDsToVehicleapp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAssindDriverIDsToVehicleapp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetAssindDriverIDsToVehicleapp]                                           
(                                              
@CUSTOMER_ID    int,                                              
@APP_ID    int,                                              
@APP_VERSION_ID   int,                                              
@VEHICLE_ID int                                             
)                                              
AS                                              
                                              
BEGIN                                              
 set quoted_identifier off                                              
SELECT                 
  A.DRIVER_ID   
 FROM                 
  APP_DRIVER_DETAILS D  WITH (NOLOCK)                     
 JOIN                 
  APP_DRIVER_ASSIGNED_VEHICLE A WITH (NOLOCK)                     
 ON                 
  A.CUSTOMER_ID = D.CUSTOMER_ID AND                
  A.APP_ID = D.APP_ID AND                
  A.APP_VERSION_ID = D.APP_VERSION_ID AND                
  A.DRIVER_ID = D.DRIVER_ID                
 LEFT OUTER JOIN              
 MNT_LOOKUP_VALUES V  WITH (NOLOCK)                    
 ON              
 A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID     
    
 WHERE                
  D.CUSTOMER_ID = @CUSTOMER_ID AND                
  D.APP_ID = @APP_ID AND                
  D.APP_VERSION_ID = @APP_VERSION_ID AND                
  ISNULL(D.IS_ACTIVE,'Y')='Y'  AND
  D.DRIVER_DRIV_TYPE = 11603 --Driver Type is Licensed    
  and ( A.APP_VEHICLE_PRIN_OCC_ID in (11398,11925,11927,11929)  )  
  and A.VEHICLE_ID = @VEHICLE_ID

END   









GO

