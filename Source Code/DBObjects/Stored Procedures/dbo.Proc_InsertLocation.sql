IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : Dbo.Proc_InsertLocation      
Created by      : Vijay Joshi      
Date            : 5/11/2005      
Purpose       :Evaluation      
Created by      : Sumit Chhabra    
Date            : 11/10/2005      
Purpose       :Named_Peril & Deductible field has been removed    
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date      Review By          Comments      
17,May 2005 Vijay     Checking the location no with respect to customer,application and veriosn only  
modified by		:     Pravesh chandel
date			: 15 april 2008
purpose			: add new column REPORT_STATUS
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_InsertLocation      
  
CREATE PROC Dbo.Proc_InsertLocation      
(      
 @CUSTOMER_ID int,      
 @APP_ID      int,      
 @APP_VERSION_ID smallint,      
 @LOCATION_ID    smallint OUTPUT,      
 @LOC_NUM      int,      
 @IS_PRIMARY     nchar(2),      
 @LOC_ADD1      nvarchar(150),      
 @LOC_ADD2      nvarchar(150),      
 @LOC_CITY      nvarchar(150),      
 @LOC_COUNTY     nvarchar(150)=null,      
 @LOC_STATE      nvarchar(10),      
 @LOC_ZIP      nvarchar(20),      
 @LOC_COUNTRY    nvarchar(10),      
 @PHONE_NUMBER   nvarchar(40),      
 @FAX_NUMBER     nvarchar(40),      
--commented as it will not be used    
-- @DEDUCTIBLE     nvarchar(40),      
-- @NAMED_PERILL   int,      
 @LOCATION_TYPE int,  
 @CREATED_BY     int,      
 @CREATED_DATETIME datetime,      
 @DESCRIPTION      varchar(1000),  
 @RENTED_WEEKLY  VARCHAR(10) = null,  
 @WEEKS_RENTED    VARCHAR(10) = null,
 @LOSSREPORT_ORDER int = null,
 @LOSSREPORT_DATETIME DateTime = null ,
 @REPORT_STATUS     char(1) =null
)      
AS      
BEGIN      
 /*Checking the duplicay for Location number field*/      
      
 If Not Exists(SELECT LOC_NUM FROM APP_LOCATIONS       
  WHERE LOC_NUM = @LOC_NUM AND      
  CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID)      
 BEGIN      
      
  /*Generating the maximum Location id and setting it in output prarameter*/      
  SELECT @LOCATION_ID = IsNull(Max(LOCATION_ID),0) + 1 FROM APP_LOCATIONS      
       
  INSERT INTO APP_LOCATIONS      
  (      
   CUSTOMER_ID, APP_ID, APP_VERSION_ID, LOCATION_ID,      
   LOC_NUM, IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY,      
   LOC_COUNTY, LOC_STATE, LOC_ZIP, LOC_COUNTRY, PHONE_NUMBER,      
   FAX_NUMBER,  
   LOCATION_TYPE,   
 --DEDUCTIBLE,NAMED_PERILL,      
   IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION]     
   , RENTED_WEEKLY ,WEEKS_RENTED, LOSSREPORT_ORDER, LOSSREPORT_DATETIME,REPORT_STATUS  
  )      
  VALUES      
  (      
   @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @LOCATION_ID,      
   @LOC_NUM, @IS_PRIMARY, @LOC_ADD1, @LOC_ADD2, @LOC_CITY,      
   @LOC_COUNTY, @LOC_STATE, @LOC_ZIP, @LOC_COUNTRY, @PHONE_NUMBER,      
   @FAX_NUMBER,   
   @LOCATION_TYPE,  
 --@DEDUCTIBLE,  @NAMED_PERILL,      
   'Y', @CREATED_BY, @CREATED_DATETIME,@DESCRIPTION      
   , @RENTED_WEEKLY ,@WEEKS_RENTED, @LOSSREPORT_ORDER, @LOSSREPORT_DATETIME  ,@REPORT_STATUS
  )      
 END      
      
END      
    
  
  
  
  
  
  




GO

