IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMVRIDs_App]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMVRIDs_App]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------              
Proc Name            : dbo.Proc_GetMVRIDs_App              
Created by           : Manoj Rathore             
Date                 : 2 Jan 2008  
Purpose              : To get the MVR ids for the Motorcycle application rule implementation            
Revison History      :              
Used In              :   Wolverine                
MODIFIED by           : Pravesh K Chandel
Date                 : 26 nov 2008  
Purpose              : To get the MVR ids for the Motorcycle application rule implementation for those violations whose is required
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       ------------------------- */              
--DROP PROC dbo.Proc_GetMVRIDs_App 
CREATE PROC dbo.Proc_GetMVRIDs_App  --1009,141,1              
 @CUSTOMER_ID  INT,              
 @APPID  INT,              
 @APPVERSIONID INT             
AS              
BEGIN              
	SELECT PMI.APP_MVR_ID as APP_MVR_ID FROM APP_MVR_INFORMATION  PMI WITH(NOLOCK)
	INNER JOIN APP_DRIVER_DETAILS PDD WITH(NOLOCK)ON PDD.CUSTOMER_ID=PMI.CUSTOMER_ID AND PDD.APP_ID=PMI.APP_ID AND PDD.APP_VERSION_ID=PMI.APP_VERSION_ID 
	AND PDD.DRIVER_ID=PMI.DRIVER_ID AND ISNULL(PDD.IS_ACTIVE,'') ='Y'
   	INNER JOIN  VIW_DRIVER_VIOLATIONS  ON PMI.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID    
	WHERE  PMI.CUSTOMER_ID = @CUSTOMER_ID  AND PMI.APP_ID=@APPID AND PMI.APP_VERSION_ID=@APPVERSIONID  
	AND ( CAST(VIOLATION_TYPE   AS INT)<15000 OR VIOLATION_CODE ='SUSPN' )
	AND PMI.IS_ACTIVE='Y'       
	ORDER BY PMI.APP_MVR_ID
           
END            




GO

