IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name   : dbo.Proc_GetVehicleIDs      
Created by  : nidhi      
Date        : 06 October,2005      
Purpose     : Get the Vehicle IDs                 
Revison History  :       
Modified By  : Ashwani     
Date   :17 Nov.,2005                    
Desc.  :Select only active vechiles     
 ------------------------------------------------------------                            
Date     Review By          Comments                          
drop proc    dbo.Proc_GetVehicleIDs               
------   ------------       -------------------------*/                
CREATE PROCEDURE dbo.Proc_GetVehicleIDs  
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID int      
)          
AS               
BEGIN                
      
SELECT   VEHICLE_ID,ISNULL(MAKE,'') AS MAKE,ISNULL(MODEL,'') AS MODEL,ISNULL(VIN,'') AS VIN, ISNULL(VEHICLE_YEAR,'') AS VEHICLE_YEAR     
FROM       APP_VEHICLES   with(nolock)    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID      
 AND IS_ACTIVE='Y'    
   ORDER BY   VEHICLE_ID   

DECLARE @EXCLUDED_DRIVER NVARCHAR(50)
SET @EXCLUDED_DRIVER='FALSE'
IF EXISTS(SELECT DRIVER_ID FROM APP_DRIVER_DETAILS  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_DRIV_TYPE = 3477)
BEGIN
	SET @EXCLUDED_DRIVER='TRUE'
END
DECLARE @GARRAGEZIPSTATE NVARCHAR(100)
BEGIN 
	SELECT @GARRAGEZIPSTATE =CONVERT(NVARCHAR(100),STATE)  FROM APP_VEHICLES WITH (NOLOCK)  INNER JOIN MNT_TERRITORY_CODES WITH (NOLOCK)  ON 
	GRG_ZIP = ZIP WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID
END
IF(@GARRAGEZIPSTATE = '22')
	BEGIN 
		SELECT 'WARNING - When a named excluded person operates a vehicle all liability coverage is void - no one is insured. Owners of the vehicle and others legally responsible for the acts of the named excluded person remain fully personally liable.' AS EXCLUDED_DRIVER_MESSAGE, @EXCLUDED_DRIVER AS EXCLUDED_DRIVER
	END 
ELSE IF(@GARRAGEZIPSTATE = '14')
	BEGIN                 
		SELECT 'WARNING - The excluded person(s), owner and/or registrant will not be covered for the insurance afforded by the policy when your covered auto is being used or operated by or at the direction of the excluded driver.' AS EXCLUDED_DRIVER_MESSAGE, @EXCLUDED_DRIVER AS EXCLUDED_DRIVER
	END

END      
      
    
    
    
  




GO

