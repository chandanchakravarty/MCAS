IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperatorDiscountsForPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperatorDiscountsForPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : Proc_GetOperatorDiscountsForPol  
Created by      : Sumit Chhabra  
Date            : Dec 08,2005  
Purpose       : Fetches operator level discount information for policy
Revison History :                  
Used In  : Wolverine                  
------------------------------------------------------------                  
Date        Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE   PROC Dbo.Proc_GetOperatorDiscountsForPol                  
(                  
 @CUSTOMER_ID      int,                  
 @POLICY_ID     smallint,                  
 @POLICY_VERSION_ID  int,                  
 @DRIVER_ID       smallint               
)                  
AS                 
BEGIN                  
  
--determine the equipment type corresponding to associated boat                  
select EQUIP_TYPE from POL_WATERCRAFT_EQUIP_DETAILLS  
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
and associated_boat in  
(select vehicle_id from POL_WATERCRAFT_DRIVER_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and   
POLICY_VERSION_ID=@POLICY_VERSION_ID and driver_id=@DRIVER_ID)  
  
--determine the fuel type corresponding to customer,app,version and driver  
select a.fuel_type  from POL_WATERCRAFT_INFO a join POL_WATERCRAFT_DRIVER_DETAILS d  
on a.customer_id=d.customer_id and a.POLICY_ID=d.POLICY_ID and a.POLICY_VERSION_ID=d.POLICY_VERSION_ID  
and a.boat_id=d.vehicle_id  
where   
d.customer_id=@CUSTOMER_ID and d.POLICY_ID=@POLICY_ID and d.POLICY_VERSION_ID=@POLICY_VERSION_ID and   
d.driver_id=@DRIVER_ID  
  
--finding if there are multiple boats for the given customer,application and version  
select customer_id from pol_watercraft_info where   
customer_id=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID   
  
                
END                  



GO

