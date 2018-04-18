

 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetManagementReport_Name]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetManagementReport_Name]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : Proc_GetManagementReport_Name    
Created by      : Lalit Chauhan
Date            : 08/30/2011    
Purpose			: Get RDL Report Name
Revison History :    
Used In			: Ebix Advantage Web (Brasil)
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  
CREATE PROCEDURE [dbo].[Proc_GetManagementReport_Name] 
(
@REPORT_TYPE_ID INT,
@GROUPBY_ID INT
)  
AS
BEGIN
SELECT * FROM MNT_MANAGEMENT_REPORTS  WHERE REPORT_TYPE_ID = @REPORT_TYPE_ID
AND GROUP_ID = @GROUPBY_ID

END
