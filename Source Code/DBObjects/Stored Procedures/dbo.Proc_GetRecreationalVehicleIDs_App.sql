IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRecreationalVehicleIDs_App]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRecreationalVehicleIDs_App]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetRecreationalVehicleIDs_App    
Created by  : Nidhi    
Date        : Aug 24,2006
Purpose     : Get the Recreational Vehicle IDs . 
	      Used for building InputXML for HO,RV part	      	
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetRecreationalVehicleIDs_App  
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int 
 
)        
AS             
BEGIN               

-- Get the LOBCODE as table name depends on the LOBCODE
DECLARE @LOB_CODE nvarchar(10) 
SET @LOB_CODE='HOME' -- by default it is home
 
SELECT @LOB_CODE= MLM.LOB_CODE 
FROM APP_LIST AL INNER JOIN MNT_LOB_MASTER MLM ON AL.APP_LOB  = MLM.LOB_ID
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID

 
BEGIN
IF(@LOB_CODE = 'HOME')
	SELECT REC_VEH_ID
    	FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
	 AND isnull(ACTIVE,'Y')='Y' ORDER BY   REC_VEH_ID    

IF (@LOB_CODE = 'UMB')
	SELECT REC_VEH_ID
    	FROM APP_UMBRELLA_RECREATIONAL_VEHICLES     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
	 AND isnull(ACTIVE,'Y')='Y' ORDER BY   REC_VEH_ID

END

END
   


  





GO

