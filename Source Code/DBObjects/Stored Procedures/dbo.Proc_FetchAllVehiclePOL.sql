IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAllVehiclePOL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAllVehiclePOL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**************************************************************
Proc Name   : dbo.Proc_FetchAllVehiclePOL
Created by  : Pravesh K. Chandel
Date        : 25 march 2007
Purpose     : Get the All Vehicle asssociated with a policy
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
***************************************************************/  
--DROP PROC Proc_FetchAllVehiclePOL
CREATE PROCEDURE dbo.Proc_FetchAllVehiclePOL
(          
  @CUSTOMER_ID int,          
  @POL_ID int,          
  @POL_VERSION_ID int          
)              
AS                   
BEGIN                    
	SELECT   
	VEHICLE_ID, 
    VEHICLE_ID AS RISK_ID
	FROM  POL_VEHICLES WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
	POLICY_ID = @POL_ID AND           
	POLICY_VERSION_ID = @POL_VERSION_ID  


END 





GO

