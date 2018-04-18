 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleModelByMake]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleModelByMake]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetVehicleModelByMake]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC Proc_GetVehicleModelByMake (
@MAKER_LOOKUP_ID INT )

AS

BEGIN

select * from [MNT_VEHICLE_MODEL_LIST]  WHERE MAKER_LOOKUP_ID = @MAKER_LOOKUP_ID

END



GO


 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleTypeByMake]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleTypeByMake]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetVehicleTypeByMake]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC Proc_GetVehicleTypeByMake (
@MAKER_LOOKUP_ID INT )

AS

BEGIN

select * from [MNT_VEHICLE_MODEL_TYPE_LIST] WHERE ID IN
(select MODEL_TYPE from [MNT_VEHICLE_MODEL_LIST]  WHERE MAKER_LOOKUP_ID = @MAKER_LOOKUP_ID)

END