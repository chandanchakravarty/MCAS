IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_OTHER_LOCATIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_OTHER_LOCATIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertPOL_OTHER_LOCATIONS      
Created by      : Swastika Gaur    
Date            : 13th Jun'06  
Purpose         :    
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_InsertPOL_OTHER_LOCATIONS      
CREATE PROC dbo.Proc_InsertPOL_OTHER_LOCATIONS      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID      int,      
 @POLICY_VERSION_ID smallint,  
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
      
 If Not Exists(SELECT LOC_NUM FROM POL_OTHER_LOCATIONS (NOLOCK)     
   WHERE LOC_NUM = @LOC_NUM AND      
   CUSTOMER_ID = @CUSTOMER_ID AND      
   POLICY_ID = @POLICY_ID AND      
   POLICY_VERSION_ID = @POLICY_VERSION_ID)      
 BEGIN      
      
  /*Generating the maximum Location id and setting it in output prarameter*/      
  SELECT @LOCATION_ID = IsNull(Max(LOCATION_ID),0) + 1 FROM POL_OTHER_LOCATIONS (NOLOCK)     
       
  INSERT INTO POL_OTHER_LOCATIONS      
  (      
   CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,DWELLING_ID,LOCATION_ID,      
   LOC_NUM,LOC_ADD1,LOC_CITY,      
   LOC_COUNTY, LOC_STATE, LOC_ZIP,  
   PHOTO_ATTACHED,OCCUPIED_BY_INSURED,  
   IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION]      
  )      
  VALUES      
  (      
   @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,@DWELLING_ID,@LOCATION_ID,      
   @LOC_NUM,@LOC_ADD1,@LOC_CITY,      
   @LOC_COUNTY, @LOC_STATE, @LOC_ZIP,  
   @PHOTO_ATTACHED,@OCCUPIED_BY_INSURED,  
   'Y', @CREATED_BY, @CREATED_DATETIME,@DESCRIPTION      
  )      
 END      
      
END      


GO

