IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*****************************************************************    
Modified  by      : Shafee    
Date              : 8-8-2006    
Purpose           : Adjust Replacecost of HO-2,HO-3,Replacement Indiana According to Location Type    
Revison History   :        
Used In           : Wolverine      
    
*******************************************************************/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
  
    
-- drop PROC dbo.Proc_UpdatePolicyLocation       
CREATE PROC [dbo].[Proc_UpdatePolicyLocation]        
(        
 @CUSTOMER_ID int,        
 @POL_ID      int,        
 @POL_VERSION_ID smallint,        
 @LOCATION_ID    smallint,        
 @LOC_NUM      bigint,        
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
-- @DEDUCTIBLE     nvarchar(40),        
-- @NAMED_PERILL   int,        
 @MODIFIED_BY     int,        
 @LAST_UPDATED_DATETIME datetime,        
 @DESCRIPTION      varchar(1000) ,    
 @LOCATION_TYPE  INT,    
 @RENTED_WEEKLY      nvarchar(20),        
 @WEEKS_RENTED      nvarchar(20),        
 @LOSSREPORT_ORDER int = null,  
 @LOSSREPORT_DATETIME DateTime = null,  
 @REPORT_STATUS char(1)=null,
 --added by Chetna
 @CAL_NUM nvarchar(20),
  --@NAME nvarchar(30) =null, Changed by Amit K Mishra for tfs bug#1212
 @NAME nvarchar(100) =null,
 @NUMBER nvarchar(50),
 @DISTRICT nvarchar(50),
 @OCCUPIED int,
 @EXT nvarchar(10),
 @CATEGORY nvarchar(20),
 @ACTIVITY_TYPE int,
 @CONSTRUCTION int,
 @IS_BILLING nchar(1),
  @RET SMALLINT OUTPUT  
)        
AS        
BEGIN   
 If Exists(SELECT LOC_NUM FROM POL_LOCATIONS WITH(NOLOCK)              
   WHERE CUSTOMER_ID=@CUSTOMER_ID   
   AND POLICY_ID=@POL_ID   
   AND POLICY_VERSION_ID=@POL_VERSION_ID   
   AND ISNULL(IS_BILLING,'N')='Y'  
   AND @IS_BILLING ='Y' AND LOCATION_ID<>@LOCATION_ID AND IS_ACTIVE='Y')         
 BEGIN  
      SET @RET=1  
      RETURN  
    END  
     
 /*Checking the duplicay of LOC_NUM field*/        
 If Not Exists(SELECT LOC_NUM FROM POL_LOCATIONS WITH(NOLOCK) WHERE         
  LOC_NUM = @LOC_NUM AND         
  CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POL_ID AND        
  POLICY_VERSION_ID = @POL_VERSION_ID AND        
  LOCATION_ID <> @LOCATION_ID AND IS_ACTIVE='Y')        
 BEGIN        
  UPDATE POL_LOCATIONS        
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
--   DEDUCTIBLE = @DEDUCTIBLE,         
--   NAMED_PERILL = @NAMED_PERILL,        
   [DESCRIPTION] = @DESCRIPTION ,    
   LOCATION_TYPE = @LOCATION_TYPE,    
RENTED_WEEKLY  = @RENTED_WEEKLY,    
WEEKS_RENTED = @WEEKS_RENTED,    
   LOSSREPORT_ORDER = @LOSSREPORT_ORDER,   
   LOSSREPORT_DATETIME = @LOSSREPORT_DATETIME  ,  
REPORT_STATUS= @REPORT_STATUS, 
 --added by Chetna
  CAL_NUM=@CAL_NUM,
  NAME=@NAME,
  NUMBER=@NUMBER,
  DISTRICT=@DISTRICT,
  OCCUPIED=@OCCUPIED,
  EXT=@EXT,
  CATEGORY=@CATEGORY,
  ACTIVITY_TYPE=@ACTIVITY_TYPE,
  CONSTRUCTION=@CONSTRUCTION,
  IS_BILLING=@IS_BILLING
  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POL_ID AND        
   POLICY_VERSION_ID = @POL_VERSION_ID AND        
   LOCATION_ID = @LOCATION_ID     
    --Update Replacement In Case Of Indiana    
   --For other products, update repl cost if required      
    DECLARE @PRODUCT_ID INT    
    SELECT @PRODUCT_ID = POLICY_TYPE       
   FROM POL_CUSTOMER_POLICY_LIST    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   POLICY_ID = @pol_ID AND              
   POLICY_VERSION_ID = @pol_VERSION_ID       
    DECLARE @EXISTING_REPL_COST DECIMAL    
    DECLARE @REPL_COST INT    
    IF (@LOCATION_TYPE=11812)    
       SET   @REPL_COST=50000    
    ELSE    
        SET  @REPL_COST=40000    
    IF (@PRODUCT_ID=11148 OR @PRODUCT_ID = 11192)    
    BEGIN     
  SELECT @EXISTING_REPL_COST = ISNULL(REPLACEMENT_COST,0)        
  FROM POL_DWELLINGS_INFO        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @pol_ID AND              
  POLICY_VERSION_ID = @pol_VERSION_ID       AND        
  LOCATION_ID = @LOCATION_ID        
      
  IF ( @EXISTING_REPL_COST < @REPL_COST )        
  BEGIN        
   UPDATE POL_DWELLINGS_INFO        
   SET REPLACEMENT_COST = @REPL_COST        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   POLICY_ID = @POL_ID AND              
   POLICY_VERSION_ID = @POL_VERSION_ID       AND        
   LOCATION_ID = @LOCATION_ID        
  END     
    END      
 --For Ho-2,Ho-3 Repair Cost    
 IF (@PRODUCT_ID=11193 OR @PRODUCT_ID = 11194)    
    BEGIN     
  SELECT @EXISTING_REPL_COST = ISNULL(MARKET_VALUE,0)        
  FROM POL_DWELLINGS_INFO        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @POL_ID AND              
  POLICY_VERSION_ID = @POL_VERSION_ID       AND        
  LOCATION_ID = @LOCATION_ID        
      
  IF ( @EXISTING_REPL_COST < @REPL_COST )        
  BEGIN        
   UPDATE POL_DWELLINGS_INFO        
   SET MARKET_VALUE = @REPL_COST        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
   POLICY_ID = @POL_ID AND              
   POLICY_VERSION_ID = @POL_VERSION_ID       AND        
   LOCATION_ID = @LOCATION_ID        
  END     
    END           
 END 
  SET @RET=2       
END        
        
        
        
        
        
        
        
        
      
    
    
    
    
    
    
    
  
  
  
GO

