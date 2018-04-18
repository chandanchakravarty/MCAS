IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAviationVehicleModel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAviationVehicleModel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name          :  dbo.Proc_GetAviationVehicleModel                                
Created by         : Pravesh K Chandel
Date               : 12 Jan 2010
Purpose            : To get the Aviation vehicle  Model                                
Revison History :                               
Used In            :   Brics
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                           
-- drop proc  dbo.Proc_GetAviationVehicleModel                             
CREATE PROC dbo.Proc_GetAviationVehicleModel
AS                                
BEGIN                                
                               
select * from MNT_AVIATION_MODEL_LIST
      
END    

      
     


GO

