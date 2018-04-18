IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_VEHICLEMODELTYPE_LOOKUP_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_VEHICLEMODELTYPE_LOOKUP_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC PROC_GET_VEHICLEMODELTYPE_LOOKUP_ID
(@MAKE_LOOKUP_ID int)

AS

BEGIN

SELECT * FROM MNT_VEHICLE_MODEL_LIST WHERE MAKER_LOOKUP_ID = @MAKE_LOOKUP_ID

SELECT * FROM MNT_VEHICLE_MODEL_TYPE_LIST WHERE ID in
(select MODEL_TYPE from MNT_VEHICLE_MODEL_LIST where MAKER_LOOKUP_ID = @MAKE_LOOKUP_ID)


END
GO

