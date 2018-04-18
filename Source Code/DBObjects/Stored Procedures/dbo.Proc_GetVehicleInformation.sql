IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
 /*----------------------------------------------------------                                          
Proc Name             : Dbo.GetVehicleInformation                                          
Created by            : Santosh Kumar Gautam                                         
Date                  : 10/11/2010                                         
Purpose               : To retrieve the vehicle id and vehicle information                                           
Revison History       :                                          
Used In               : To fill dropdown at risk information page.(CLAIM module)                                          
------------------------------------------------------------                                          
Date     Review By          Comments             
    
drop Proc GetVehicleInformation                                 
------   ------------       -------------------------*/                                          
--             
              
--           
        
CREATE PROCEDURE [dbo].[Proc_GetVehicleInformation]              
               
@CUSTOMER_ID         INT,                                                                          
@POLICTY_ID          INT,                                                                          
@POLICY_VERSION_ID   INT,  
@CLAIM_ID            INT                                                                          
                                                                    
              
AS              
BEGIN   
  
  
SELECT  T.VEHICLE_ID,
        (ISNULL(V.VIN,'')+'-'+ISNULL(V.VEHICLE_YEAR,'')  +'-'+ISNULL(V.MAKE,'')+'-'+ISNULL(V.MODEL,'')) 
        AS VEHICLE
FROM POL_CIVIL_TRANSPORT_VEHICLES T INNER JOIN
     POL_VEHICLES V ON T.VEHICLE_ID=V.VEHICLE_ID INNER JOIN
     CLM_CLAIM_INFO I ON I.CLAIM_ID=@CLAIM_ID  
WHERE (T.CUSTOMER_ID=@CUSTOMER_ID AND T.POLICY_ID=@POLICTY_ID AND T.POLICY_VERSION_ID= @POLICY_VERSION_ID AND T.IS_ACTIVE='Y')     


END              
              
       
GO

