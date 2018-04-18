IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_DRIVER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_DRIVER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                  
Proc Name       : dbo.Proc_InsertCLM_DRIVER_INFORMATION                                            
Created by      : Sumit Chhabra                                                
Date            : 27/04/2006                                                  
Purpose         : Insert data in CLM_DRIVER_INFORMATION  table for DRIVER screen              
Created by      : Sumit Chhabra                                                 
Revison History :                                                  
Used In        : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/
-- drop proc dbo.Proc_InsertCLM_DRIVER_INFORMATION                                                   
CREATE PROC dbo.Proc_InsertCLM_DRIVER_INFORMATION                
(                                                         
 @DRIVER_ID int output,              
 @CLAIM_ID int,              
 @TYPE_OF_DRIVER int,              
 @NAME varchar(50),              
 @ADDRESS1 varchar(50),              
 @ADDRESS2 varchar(50),              
 --@CITY varchar(10),
 @CITY varchar(50),                            
 @STATE int,              
 @ZIP varchar(10),              
 @HOME_PHONE varchar(15),              
 @WORK_PHONE varchar(15),              
 @MOBILE_PHONE varchar(15),              
 @EXTENSION varchar(5),              
 @CREATED_BY int,              
 @CREATED_DATETIME datetime,            
 @RELATION_INSURED varchar(50),        
 @DATE_OF_BIRTH datetime,            
 @LICENSE_NUMBER varchar(60),            
 @LICENSE_STATE int,            
 @PURPOSE_OF_USE varchar(50),            
 @USED_WITH_PERMISSION char(1),            
 @DESCRIBE_DAMAGE varchar(100),            
 @ESTIMATE_AMOUNT decimal(12,2),          
 @OTHER_VEHICLE_INSURANCE varchar(100),      
 @VEHICLE_ID int,    
 @COUNTRY int,  
 @SEX nchar(1),
 @SSN nvarchar(88),
 @VEHICLE_OWNER INT,
 @TYPE_OF_OWNER INT,
 @DRIVERS_INJURY varchar(500)    
)              
            
AS                                                  
BEGIN                                                  
 SELECT                               
  @DRIVER_ID = ISNULL(MAX(DRIVER_ID),0)+1                               
 FROM                               
  CLM_DRIVER_INFORMATION              
 WHERE              
 CLAIM_ID = @CLAIM_ID              
              
 INSERT INTO CLM_DRIVER_INFORMATION                              
 (                              
  DRIVER_ID,              
  CLAIM_ID,              
  TYPE_OF_DRIVER,              
  NAME,              
  ADDRESS1,              
  ADDRESS2,              
  CITY,              
  STATE,              
  ZIP,              
  HOME_PHONE,              
  WORK_PHONE,              
  MOBILE_PHONE,              
  EXTENSION,              
  CREATED_BY,              
  CREATED_DATETIME,              
  IS_ACTIVE,            
 RELATION_INSURED,            
 DATE_OF_BIRTH,            
 LICENSE_NUMBER,            
 LICENSE_STATE,            
 PURPOSE_OF_USE,            
 USED_WITH_PERMISSION,            
 DESCRIBE_DAMAGE,            
 ESTIMATE_AMOUNT,          
 OTHER_VEHICLE_INSURANCE,      
 VEHICLE_ID,    
 COUNTRY,  
 SEX,
 SSN,
 VEHICLE_OWNER,
 TYPE_OF_OWNER,
 DRIVERS_INJURY             
 )                              
 VALUES                              
 (                              
  @DRIVER_ID,              
  @CLAIM_ID,              
  @TYPE_OF_DRIVER,              
  @NAME,              
  @ADDRESS1,              
  @ADDRESS2,              
  @CITY,              
  @STATE,              
  @ZIP,       
  @HOME_PHONE,              
  @WORK_PHONE,              
  @MOBILE_PHONE,              
  @EXTENSION,              
  @CREATED_BY,              
  @CREATED_DATETIME,                
   'Y',            
  @RELATION_INSURED,            
  @DATE_OF_BIRTH,            
  @LICENSE_NUMBER,            
  @LICENSE_STATE,            
  @PURPOSE_OF_USE,            
  @USED_WITH_PERMISSION,            
  @DESCRIBE_DAMAGE,            
  @ESTIMATE_AMOUNT,          
  @OTHER_VEHICLE_INSURANCE,      
  @VEHICLE_ID,    
  @COUNTRY,  
  @SEX,
  @SSN,
 @VEHICLE_OWNER,
 @TYPE_OF_OWNER ,
 @DRIVERS_INJURY 
 )                              
                               
END 









GO

