IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorCycle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorCycle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetMotorCycle      
Created by           : Mohit Gupta      
Date                    : 22/09/2005      
Purpose               : For fetching the motor cycles for the input parameters.        
Revison History  :      
Used In                :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
create PROCEDURE Proc_GetMotorCycle      
(      
@APP_ID int,      
@CUSTOMER_ID int,      
@APP_VERSION_ID int       
)      
AS      
BEGIN      
SELECT VEHICLE_ID, CONVERT(VARCHAR,VEHICLE_ID) +'-'+ CONVERT(VARCHAR,MAKE)  VEHICLE_DESC       
FROM APP_VEHICLES WHERE       
CUSTOMER_ID=@CUSTOMER_ID   AND  APP_ID= @APP_ID    AND APP_VERSION_ID= @APP_VERSION_ID  and upper(isnull(is_active,'N'))='Y'     
END    
  



GO

