IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetYearsFromVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetYearsFromVINMASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetYearsFromVINMASTER    
Created by           : Nidhi    
Date                    : 29/04/2005    
Purpose               : To get the years from vinmaster    
Revison History :    
Used In                :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/
--drop proc dbo.Proc_GetYearsFromVINMASTER   
CREATE PROC dbo.Proc_GetYearsFromVINMASTER    
    
AS    
BEGIN    
    
select distinct MODEL_YEAR  from MNT_VIN_MASTER order by MODEL_YEAR desc   
    
END    
  


GO

