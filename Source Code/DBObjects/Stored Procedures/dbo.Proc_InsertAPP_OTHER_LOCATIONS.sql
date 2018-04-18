IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_OTHER_LOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_OTHER_LOCATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertAPP_OTHER_LOCATIONS    
Created by      : Swastika Gaur  
Date            : 13th Jun'06
Purpose         :  
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_InsertAPP_OTHER_LOCATIONS    
CREATE PROC dbo.Proc_InsertAPP_OTHER_LOCATIONS    
(    
 @CUSTOMER_ID int,    
 @APP_ID      int,    
 @APP_VERSION_ID smallint,
 @DWELLING_ID smallint,    
 @LOCATION_ID    smallint OUTPUT,    
 @LOC_NUM      int,    
 @LOC_ADD1      nvarchar(150),    
 @LOC_CITY      nvarchar(150),    
 @LOC_COUNTY     nvarchar(150)=null,    
 @LOC_STATE      nvarchar(10),    
 @LOC_ZIP      nvarchar(20),
 @PHOTO_ATTACHED int,
 @OCCUPIED_BY_INSURED int,    
 @CREATED_BY     int,    
 @CREATED_DATETIME datetime,    
 @DESCRIPTION      varchar(1000)    
)    
AS    
BEGIN    
 /*Checking the duplicay for Location number field*/    
    
 If Not Exists(SELECT LOC_NUM FROM APP_OTHER_LOCATIONS     
 	 WHERE LOC_NUM = @LOC_NUM AND    
 	 CUSTOMER_ID = @CUSTOMER_ID AND    
 	 APP_ID = @APP_ID AND    
 	 APP_VERSION_ID = @APP_VERSION_ID)    
 BEGIN    
    
  /*Generating the maximum Location id and setting it in output prarameter*/    
  SELECT @LOCATION_ID = IsNull(Max(LOCATION_ID),0) + 1 FROM APP_OTHER_LOCATIONS    
     
  INSERT INTO APP_OTHER_LOCATIONS    
  (    
   CUSTOMER_ID, APP_ID, APP_VERSION_ID,DWELLING_ID,LOCATION_ID,    
   LOC_NUM,LOC_ADD1,LOC_CITY,    
   LOC_COUNTY, LOC_STATE, LOC_ZIP,
   PHOTO_ATTACHED,OCCUPIED_BY_INSURED,
   IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION]    
  )    
  VALUES    
  (    
   @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID,@DWELLING_ID,@LOCATION_ID,    
   @LOC_NUM,@LOC_ADD1,@LOC_CITY,    
   @LOC_COUNTY, @LOC_STATE, @LOC_ZIP,
   @PHOTO_ATTACHED,@OCCUPIED_BY_INSURED,
   'Y', @CREATED_BY, @CREATED_DATETIME,@DESCRIPTION    
  )    
 END    
    
END    
  







GO

