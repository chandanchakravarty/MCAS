IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateLocation      
Created by      : Vijay Joshi      
Date            : 5/11/2005      
Purpose       :Evaluation      
Modified  by      : Sumit Chhabra    
Date            : 11/10/2005      
Purpose       :Named Peril has been removed    
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date      Review By          Comments      
17 May,2005 Vijay     Checking the duplicacy of loc nu for specified client, app and version      
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_UpdateLocation      
CREATE PROC dbo.Proc_UpdateLocation      
(      
 @CUSTOMER_ID int,      
 @APP_ID      int,      
 @APP_VERSION_ID smallint,      
 @LOCATION_ID    smallint,      
 @LOC_NUM      int,      
 @IS_PRIMARY     nchar(2),      
 @LOC_ADD1      nvarchar(150),      
 @LOC_ADD2      nvarchar(150),      
 @LOC_CITY      nvarchar(150),      
 @LOC_COUNTY     nvarchar(150),      
 @LOC_STATE      nvarchar(10),      
 @LOC_ZIP      nvarchar(20),      
 @LOC_COUNTRY    nvarchar(10),      
 @PHONE_NUMBER   nvarchar(40),      
 @FAX_NUMBER     nvarchar(40),      
 @LOCATION_TYPE int,  
-- @DEDUCTIBLE     nvarchar(40),  @NAMED_PERILL   int,      
 @MODIFIED_BY     int,      
 @LAST_UPDATED_DATETIME datetime,      
 @DESCRIPTION      varchar(1000),  
 @RENTED_WEEKLY  VARCHAR(10) = null,  
 @WEEKS_RENTED    VARCHAR(10) = null,    
 @LOSSREPORT_ORDER int = null,
 @LOSSREPORT_DATETIME DateTime = null ,
 @REPORT_STATUS  char(1)=null     
)      
AS      
BEGIN      
  
 /*Checking the duplicay of LOC_NUM field*/      
If Not Exists(  
 SELECT LOC_NUM FROM APP_LOCATIONS WHERE       
 LOC_NUM = @LOC_NUM AND       
 CUSTOMER_ID = @CUSTOMER_ID AND      
 APP_ID = @APP_ID AND      
 APP_VERSION_ID = @APP_VERSION_ID AND      
 LOCATION_ID <> @LOCATION_ID  
 )      
BEGIN      
 UPDATE APP_LOCATIONS      
 SET LOC_NUM = @LOC_NUM,       
 IS_PRIMARY = @IS_PRIMARY,       
 LOC_ADD1 = @LOC_ADD1,       
 LOC_ADD2 = @LOC_ADD2,       
 LOC_CITY = @LOC_CITY,      
 LOC_COUNTY = @LOC_COUNTY,       
 LOC_STATE = @LOC_STATE,       
 LOC_ZIP = @LOC_ZIP,       
 LOC_COUNTRY = @LOC_COUNTRY,       
 PHONE_NUMBER = @PHONE_NUMBER,      
 FAX_NUMBER = @FAX_NUMBER,       
 LOCATION_TYPE = @LOCATION_TYPE,  
 [DESCRIPTION]  = @DESCRIPTION,      
 RENTED_WEEKLY  = @RENTED_WEEKLY,  
 WEEKS_RENTED   = @WEEKS_RENTED,
 LOSSREPORT_ORDER = @LOSSREPORT_ORDER, 
 LOSSREPORT_DATETIME = @LOSSREPORT_DATETIME  ,
  REPORT_STATUS=@REPORT_STATUS    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
 APP_ID = @APP_ID AND      
 APP_VERSION_ID = @APP_VERSION_ID AND      
 LOCATION_ID = @LOCATION_ID     
  --Update Replacement In Case Of Indiana  
   --For other products, update repl cost if required    
 DECLARE @PRODUCT_ID INT  
 SELECT @PRODUCT_ID = POLICY_TYPE     
 FROM APP_LIST  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID     
 DECLARE @EXISTING_REPL_COST DECIMAL  
 DECLARE @REPL_COST INT  
 IF (@LOCATION_TYPE=11812)  
  SET   @REPL_COST=50000  
 ELSE  
         SET  @REPL_COST=40000  
   --For Ho-2,Ho-3 Replacement Cost  
 IF (@PRODUCT_ID=11148 OR @PRODUCT_ID = 11192)  
 BEGIN   
  SELECT @EXISTING_REPL_COST = ISNULL(REPLACEMENT_COST,0)      
  FROM APP_DWELLINGS_INFO      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID       AND      
  LOCATION_ID = @LOCATION_ID      
    
  IF ( @EXISTING_REPL_COST < @REPL_COST )      
  BEGIN      
   UPDATE APP_DWELLINGS_INFO      
   SET REPLACEMENT_COST = @REPL_COST      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID       AND      
   LOCATION_ID = @LOCATION_ID      
  END   
 END     
   --For Ho-2,Ho-3 Repair Cost  
 IF (@PRODUCT_ID=11193 OR @PRODUCT_ID = 11194)  
 BEGIN   
  SELECT @EXISTING_REPL_COST = ISNULL(MARKET_VALUE,0)      
  FROM APP_DWELLINGS_INFO      
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
  APP_ID = @APP_ID AND            
  APP_VERSION_ID = @APP_VERSION_ID       AND      
  LOCATION_ID = @LOCATION_ID      
    
  IF ( @EXISTING_REPL_COST < @REPL_COST )      
  BEGIN      
   UPDATE APP_DWELLINGS_INFO      
   SET MARKET_VALUE = @REPL_COST      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID       AND      
   LOCATION_ID = @LOCATION_ID      
  END   
 END     
END      
END      
      
      
      
      
      
      
    
  
  
  
  
  
  
  
  
  
  
  




GO

