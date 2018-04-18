IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatMVRIDs_App]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatMVRIDs_App]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------              
Proc Name            : dbo.Proc_GetWatMVRIDs_App              
Created by           : Manoj Rathore             
Date                 : 2 Jan 2008  
Purpose              : To get the MVR ids for the Watercraft/Homeowners application rule implementation            
Revison History      :              
Used In              :   Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       ------------------------- */              
--DROP PROC dbo.Proc_GetWatMVRIDs_App 
            
CREATE PROC dbo.Proc_GetWatMVRIDs_App             
 @CUSTOMER_ID  INT,              
 @APPID  INT,              
 @APPVERSIONID INT             
AS              
BEGIN              
	SELECT APP_WATER_MVR_ID,DRIVER_ID FROM APP_WATER_MVR_INFORMATION              
	WHERE  CUSTOMER_ID = @CUSTOMER_ID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID           
	AND IS_ACTIVE='Y'       
	ORDER BY APP_WATER_MVR_ID
            
END            




GO

