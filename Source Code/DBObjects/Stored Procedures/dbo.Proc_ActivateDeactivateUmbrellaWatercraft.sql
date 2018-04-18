IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_ActivateDeactivateUmbrellaWatercraft          
Created by      : Priya Arora            
Date            : 27/12/2005                            
Purpose        :Activate/ Deactivate Umbrella Watercraft         
Revison History :                  
Used In        : Wolverine                  
              
                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE PROC DBO.Proc_ActivateDeactivateUmbrellaWatercraft                  
(                  
@CUSTOMER_ID     INT,                  
@APP_ID     INT,                  
@APP_VERSION_ID     SMALLINT,                  
@BOAT_ID     SMALLINT,          
@IS_ACTIVE NCHAR(2)          
)                  
AS                  
BEGIN                  
          
UPDATE app_umbrella_watercraft_info SET IS_ACTIVE=@IS_ACTIVE WHERE          
 CUSTOMER_ID=@CUSTOMER_ID AND           
 APP_ID=@APP_ID AND          
 APP_VERSION_ID=@APP_VERSION_ID AND          
 BOAT_ID=@BOAT_ID          
  
IF(UPPER(@IS_ACTIVE)='N')
	UPDATE APP_UMBRELLA_DRIVER_DETAILS SET OP_VEHICLE_ID=NULL WHERE  
	 CUSTOMER_ID=@CUSTOMER_ID AND           
	 APP_ID=@APP_ID AND          
	 APP_VERSION_ID=@APP_VERSION_ID AND          
	 OP_VEHICLE_ID=@BOAT_ID 

	
  
  
END          
        
      
    
  



GO

