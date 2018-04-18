IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_REIN_COMAPANY_NAME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_REIN_COMAPANY_NAME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Created by      : Deepak Batra    
Date            : 19 Jan 2006    
Purpose     : For getting Reinsurance Company Names    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_Get_REIN_COMAPANY_NAME    
CREATE PROC [dbo].[Proc_Get_REIN_COMAPANY_NAME]    
    
AS    
    
BEGIN    
    
SELECT REIN_COMAPANY_ID, REIN_COMAPANY_NAME     
FROM MNT_REIN_COMAPANY_LIST     
WHERE REIN_COMAPANY_ID IN     
(SELECT DISTINCT REINSURANCE_COMPANY FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE ISNULL(IS_ACTIVE,'Y')='Y')    
AND ISNULL(IS_ACTIVE,'Y')='Y'     
ORDER BY REIN_COMAPANY_NAME DESC
    
END    
    
    
    
    
    
GO

