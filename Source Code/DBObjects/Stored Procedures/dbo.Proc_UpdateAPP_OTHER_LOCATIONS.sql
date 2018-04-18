IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_OTHER_LOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_OTHER_LOCATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateAPP_OTHER_LOCATIONS    
Created by      : Swastika Gaur    
Date            : 13th Jun'06
Purpose         :
Revison History :    
Used In         : Wolverine    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_UpdateAPP_OTHER_LOCATIONS    
CREATE PROC dbo.Proc_UpdateAPP_OTHER_LOCATIONS    
(    
 @CUSTOMER_ID int,    
 @APP_ID      int,    
 @APP_VERSION_ID smallint,
 @DWELLING_ID smallint,    
 @LOCATION_ID    smallint,    
 @LOC_NUM      int,    
 @LOC_ADD1      nvarchar(150),    
 @LOC_CITY      nvarchar(150),    
 @LOC_COUNTY     nvarchar(150),    
 @LOC_STATE      nvarchar(10),    
 @LOC_ZIP      nvarchar(20),
 @PHOTO_ATTACHED int,
 @OCCUPIED_BY_INSURED int,    
 @MODIFIED_BY     int,    
 @LAST_UPDATED_DATETIME datetime,    
 @DESCRIPTION      varchar(1000)    
)    
AS    
BEGIN    
 /*Checking the duplicay of LOC_NUM field*/    
 If Not Exists(SELECT LOC_NUM FROM APP_OTHER_LOCATIONS (NOLOCK) WHERE     
  		LOC_NUM = @LOC_NUM AND     
  		CUSTOMER_ID = @CUSTOMER_ID AND    
  		APP_ID = @APP_ID AND    
  		APP_VERSION_ID = @APP_VERSION_ID AND    
  		LOCATION_ID <> @LOCATION_ID)    
	 BEGIN    
	  UPDATE APP_OTHER_LOCATIONS    
	  SET LOC_NUM = @LOC_NUM,     
	   LOC_ADD1 = @LOC_ADD1,     
	   LOC_CITY = @LOC_CITY,    
	   LOC_COUNTY = @LOC_COUNTY,     
	   LOC_STATE = @LOC_STATE,     
	   LOC_ZIP = @LOC_ZIP,     
	   PHOTO_ATTACHED = @PHOTO_ATTACHED,
	   OCCUPIED_BY_INSURED = @OCCUPIED_BY_INSURED,
	   MODIFIED_BY = @MODIFIED_BY,   
	   LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,    
	   [DESCRIPTION] = @DESCRIPTION
	  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
	 	  APP_ID = @APP_ID AND    
	 	  APP_VERSION_ID = @APP_VERSION_ID AND    
		  LOCATION_ID = @LOCATION_ID    
	 END    
END    
    
    
    
    
    
    
  











GO

