IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.[Proc_InsertPolicyLocation]                  
Created by      :                          
Date            :   
Purpose         : To Insert the loaction                       
Revison History :   
Modified by  : Pradeep Kushwaha  
Date   : 13-July-2010  
Purpose   : To make unique location for the perticular customer                         
Used In         : Brazil      
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------          
            
*/         
-- DROP PROC dbo.[Proc_InsertPolicyLocation]      
CREATE PROC [dbo].[Proc_InsertPolicyLocation]            
(            
 @CUSTOMER_ID int,            
 @POL_ID      int,            
 @POL_VERSION_ID smallint,            
 @LOCATION_ID    smallint OUTPUT,            
 @LOC_NUM      bigint,            
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
-- @DEDUCTIBLE     nvarchar(40),            
-- @NAMED_PERILL   int,            
 @CREATED_BY     int,            
 @CREATED_DATETIME datetime,            
 @DESCRIPTION      varchar(1000),        
 @LOCATION_TYPE    INT ,        
 @RENTED_WEEKLY     nvarchar(150)=null,            
 @WEEKS_RENTED       nvarchar(150)=null,      
 @LOSSREPORT_ORDER int = null,      
 @LOSSREPORT_DATETIME DateTime = null ,           
 @REPORT_STATUS char(1)=null,    
 --added By Chetna    
 @CAL_NUM nvarchar(20) =null,    
 --@NAME nvarchar(30) =null, Changed by Amit K Mishra for tfs bug#1212
 @NAME nvarchar(100) =null,
 @NUMBER nvarchar(50) =null,    
 @DISTRICT nvarchar(50) =null,    
 @OCCUPIED int=0,    
 @EXT nvarchar(10)=null,    
 @CATEGORY nvarchar(20)=null,    
 @ACTIVITY_TYPE int=0,    
 @CONSTRUCTION int=0 ,  
 @SOURCE_LOCATION_ID bigint=0 ,  
 @IS_BILLING nchar(1)=null,  
 @RET SMALLINT OUTPUT  
)            
AS            
BEGIN            
 /*Checking the duplicay for Location number field*/      
   
  If Exists(SELECT LOC_NUM FROM POL_LOCATIONS WITH(NOLOCK)              
  WHERE LOC_NUM = @LOC_NUM AND            
  CUSTOMER_ID = @CUSTOMER_ID AND
    POLICY_ID = @POL_ID AND        
  POLICY_VERSION_ID = @POL_VERSION_ID )            
 BEGIN  
      SET @RET=1  
      RETURN  
    END  
 /*Checking the duplicay for IS BILLING TO YES*/      
  If Exists(SELECT LOC_NUM FROM POL_LOCATIONS WITH(NOLOCK)              
   WHERE CUSTOMER_ID=@CUSTOMER_ID   
   AND POLICY_ID=@POL_ID   
   AND POLICY_VERSION_ID=@POL_VERSION_ID   
   AND ISNULL(IS_BILLING,'N')='Y'  
   ) AND @IS_BILLING ='Y'           
 BEGIN  
      SET @RET=2  
      RETURN  
    END  
      
 /*Generating the maximum Location id and setting it in output prarameter*/            
 SELECT @LOCATION_ID = IsNull(Max(LOCATION_ID),0) + 1 FROM POL_LOCATIONS  WITH(NOLOCK) WHERE  CUSTOMER_ID = @CUSTOMER_ID          
  
 INSERT INTO POL_LOCATIONS            
 (            
 CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID,            
 LOC_NUM, IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY,            
 LOC_COUNTY, LOC_STATE, LOC_ZIP, LOC_COUNTRY, PHONE_NUMBER,            
 FAX_NUMBER,           
 --DEDUCTIBLE, NAMED_PERILL,            
 IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION],LOCATION_TYPE,        
 RENTED_WEEKLY,        
 WEEKS_RENTED, LOSSREPORT_ORDER, LOSSREPORT_DATETIME,          
 REPORT_STATUS,CAL_NUM,NAME,NUMBER,DISTRICT,OCCUPIED,EXT,CATEGORY,ACTIVITY_TYPE,CONSTRUCTION ,SOURCE_LOCATION_ID,IS_BILLING   
 )            
 VALUES            
 (            
 @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @LOCATION_ID,            
 @LOC_NUM, @IS_PRIMARY, @LOC_ADD1, @LOC_ADD2, @LOC_CITY,            
 @LOC_COUNTY, @LOC_STATE, @LOC_ZIP, @LOC_COUNTRY, @PHONE_NUMBER,            
 @FAX_NUMBER,           
 --@DEDUCTIBLE, @NAMED_PERILL,            
 'Y', @CREATED_BY, @CREATED_DATETIME,@DESCRIPTION ,@LOCATION_TYPE,        
 @RENTED_WEEKLY,        
 @WEEKS_RENTED, @LOSSREPORT_ORDER, @LOSSREPORT_DATETIME ,         
 @REPORT_STATUS,@CAL_NUM,@NAME,@NUMBER,@DISTRICT,@OCCUPIED,@EXT,@CATEGORY,@ACTIVITY_TYPE,@CONSTRUCTION,@SOURCE_LOCATION_ID, @IS_BILLING          
 )            
 SET @RET=3  
END    
      
GO

