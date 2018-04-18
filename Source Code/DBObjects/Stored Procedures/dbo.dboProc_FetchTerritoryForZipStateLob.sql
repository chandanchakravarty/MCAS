IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dboProc_FetchTerritoryForZipStateLob]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dboProc_FetchTerritoryForZipStateLob]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------  
Proc Name          : dbo.Proc_FetchTerritoryForZipStateLob  
Created by           : Sumit Chhabra
Date                    : 22/02/2007  
Purpose               :   Search for a territory based on state,zip and lob_id
Revison History :  
 
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE dboProc_FetchTerritoryForZipStateLob  
(  
 @ZIP_ID VARCHAR(10),  
 @LOB_ID smallint,
 @STATE_ID smallint
)  
AS  
BEGIN  
SELECT TERR FROM MNT_TERRITORY_CODES WHERE ZIP=SUBSTRING(@ZIP_ID,1,5) AND LOBID=@LOB_ID and STATE=@STATE_ID    
END  
  



GO

