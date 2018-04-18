 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleCoveragesType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleCoveragesType]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetVehicleCoveragesType]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   



--Proc_GetVehicleCoveragesType -99

CREATE PROC Proc_GetVehicleCoveragesType ( @LOB_ID INT )

AS

BEGIN

	SELECT * FROM MNT_COVERAGE WHERE LOB_ID = @LOB_ID

END
