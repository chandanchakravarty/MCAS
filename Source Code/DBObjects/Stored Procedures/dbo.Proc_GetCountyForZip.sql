IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountyForZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountyForZip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_FetchTerritoryForZip  
Created by         : Pradeep Iyer  
Date               : 23/06/2005  
Purpose            :   
Revison History    :  
Used In            :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE    PROCEDURE dbo.Proc_GetCountyForZip  
(  
 @ZIP_ID VARCHAR(10)  
)  
AS  
BEGIN  
  
SELECT distinct COUNTY FROM MNT_TERRITORY_CODES   
WHERE ZIP like (SUBSTRING(@ZIP_ID,1,5) + '%')
  
END  


GO

