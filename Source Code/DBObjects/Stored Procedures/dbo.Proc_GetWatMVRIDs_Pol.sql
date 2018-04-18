IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatMVRIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatMVRIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------              
Proc Name            : dbo.Proc_GetWatMVRIDs_Pol              
Created by           : Manoj Rathore             
Date                 : 2 Jan 2008  
Purpose              : To get the MVR ids for the Watercraft application rule implementation            
Revison History      :              
Used In              :   Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       ------------------------- */              
--DROP PROC dbo.Proc_GetWatMVRIDs_Pol 
            
CREATE PROC dbo.Proc_GetWatMVRIDs_Pol  --1009,141,1              
 @CUSTOMER_ID  INT,              
 @POLICY_ID  INT,              
 @POLICY_VERSION_ID INT             
AS              
BEGIN              
	SELECT APP_WATER_MVR_ID,DRIVER_ID FROM POL_WATERCRAFT_MVR_INFORMATION              
	WHERE  CUSTOMER_ID = @CUSTOMER_ID   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID           
	AND IS_ACTIVE='Y'       
	ORDER BY APP_WATER_MVR_ID
            
END            





GO

