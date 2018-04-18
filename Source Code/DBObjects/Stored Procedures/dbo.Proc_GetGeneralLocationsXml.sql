IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGeneralLocationsXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGeneralLocationsXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetGeneralLocationsXml      
Created by         : Priya      
Date               : 23/05/2005      
Purpose            : To get details from APP_LOCATIONS      
Revison History :      
Used In                :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
create PROC Dbo.Proc_GetGeneralLocationsXml      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint      
-- @Location_Id int      
             
)      
      
AS      
BEGIN      
 SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID,      
               LOC_NUM,LOC_ADD1,LOC_ADD2,      
        LOC_CITY,LOC_COUNTY,LOC_STATE,LOC_ZIP,LOC_TERRITORY,LOC_COUNTRY        
 FROM APP_LOCATIONS      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID      
--AND Location_Id=@Location_Id      
      
END    
    
  



GO

