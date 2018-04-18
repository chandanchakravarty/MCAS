IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetModelFromVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetModelFromVINMASTER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetModelFromVINMASTER
Created by           : Nidhi
Date                    : 29/04/2005
Purpose               : To get Make from VINMASTER
Revison History :
Used In                :   Wolverine

  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetModelFromVINMASTER
(
@MODEL_YEAR	NVARCHAR (150),
@MAKE_CODE          nvarchar (150)

)
AS

BEGIN
	SELECT   DISTINCT Series_Name
	FROM         MNT_VIN_MASTER  
	WHERE     (MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE )
order by series_name
END


GO

