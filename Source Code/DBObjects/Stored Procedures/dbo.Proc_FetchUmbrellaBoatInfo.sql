IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchUmbrellaBoatInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchUmbrellaBoatInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------      
Proc Name      : dbo.Proc_FetchUmbrellaBoatInfo      
Created by       : Sumit Chhabra  
Date             : 24/10/2005      
Purpose       : retrieving data from app_umbrella_watercraft_info       
Revison History :      
Used In        : Wolverine      
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_FetchUmbrellaBoatInfo      
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID INT,      
@BOATID INT=NULL      
AS      
IF @BOATID IS NULL       
 BEGIN        
      
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT FROM APP_UMBRELLA_WATERCRAFT_INFO WHERE       
   APP_ID=@APP_ID AND       
  APP_VERSION_ID=@APP_VERSION_ID      
  AND CUSTOMER_ID=@CUSTOMER_ID AND
	ISNULL(IS_ACTIVE,'')='Y'      
 END      
ELSE      
 BEGIN      
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT  FROM APP_UMBRELLA_WATERCRAFT_INFO WHERE       
   APP_ID=@APP_ID AND       
  APP_VERSION_ID=@APP_VERSION_ID      
  AND CUSTOMER_ID=@CUSTOMER_ID       
  AND BOAT_ID=@BOATID AND
  	ISNULL(IS_ACTIVE,'')='Y'           
END      
      
    
  


--sp_helptext Proc_FetchBoatInfo


GO

