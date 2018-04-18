IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMVRIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMVRIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------              
Proc Name            : dbo.Proc_GetMVRIDs_Pol              
Created by           : Manoj Rathore             
Date                 : 2 Jan 2008  
Purpose              : To get the MVR ids for the Motorcycle policy rule implementation            
Revison History      :              
Used In              :   Wolverine 
Modified by           : Pravesh K Chandel
Date                 : 26 Nov 2008  
Purpose              : To get the MVR ids for the Motorcycle policy rule implementation  for those where required          
              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       ------------------------- */              
--DROP PROC dbo.Proc_GetMVRIDs_Pol 
CREATE PROC dbo.Proc_GetMVRIDs_Pol  --1009,141,1              
 @CUSTOMER_ID  INT,              
 @POLICY_ID  INT,              
 @POLICY_VERSION_ID INT             
AS              
BEGIN              
	SELECT PMI.POL_MVR_ID as POL_MVR_ID 
	FROM POL_MVR_INFORMATION   PMI WITH(NOLOCK)
    INNER JOIN POL_DRIVER_DETAILS PDD WITH(NOLOCK)ON PDD.CUSTOMER_ID=PMI.CUSTOMER_ID AND PDD.POLICY_ID=PMI.POLICY_ID AND PDD.POLICY_VERSION_ID=PMI.POLICY_VERSION_ID 
	AND PDD.DRIVER_ID=PMI.DRIVER_ID AND ISNULL(PDD.IS_ACTIVE,'') ='Y'
	INNER JOIN  VIW_DRIVER_VIOLATIONS  ON PMI.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID    
	WHERE  PMI.CUSTOMER_ID = @CUSTOMER_ID   AND PMI.POLICY_ID=@POLICY_ID AND PMI.POLICY_VERSION_ID=@POLICY_VERSION_ID           
	AND ( CAST(VIOLATION_TYPE   AS INT)<15000 OR VIOLATION_CODE ='SUSPN' )
	AND PMI.IS_ACTIVE='Y'       
	ORDER BY PMI.POL_MVR_ID            
END            









GO

