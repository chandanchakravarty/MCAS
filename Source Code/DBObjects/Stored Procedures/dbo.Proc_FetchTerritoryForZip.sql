IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTerritoryForZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTerritoryForZip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_FetchTerritoryForZip  
Created by           : Mohit Gupta  
Date                    : 19/05/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
Modified By           : Mohit Gupta  
Modified On           : 6/10/2005  
Purpose               : Adding LOBID in the where condition.   

Modified By           : Pawan Papreja  
Modified On           : 26/10/2005  
Purpose               : ZIP search on 5 chars
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_FetchTerritoryForZip  
(  
 @ZIP_ID VARCHAR(10),  
 @LOBID  INT  
)  
AS  
BEGIN  
SELECT TERR FROM MNT_TERRITORY_CODES WHERE ZIP=SUBSTRING(@ZIP_ID,1,5) AND LOBID=@LOBID    
END  
  


GO

