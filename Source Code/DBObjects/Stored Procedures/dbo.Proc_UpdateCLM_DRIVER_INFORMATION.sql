IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_DRIVER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_DRIVER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name       : dbo.Proc_UpdateCLM_DRIVER_INFORMATION                                            
Created by      : Sumit Chhabra                                                
Date            : 04/05/2006                                                  
Purpose         : Update data at CLM_DRIVER_INFORMATION  table for DRIVER screen              
Created by      : Sumit Chhabra                                                 
Revison History :                                                  
Used In        : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/ 
-- drop proc dbo.Proc_UpdateCLM_DRIVER_INFORMATION                                                  
CREATE PROC dbo.Proc_UpdateCLM_DRIVER_INFORMATION                
(                                                         
 @DRIVER_ID int,              
 @CLAIM_ID int,              
 @TYPE_OF_DRIVER int,              
 @NAME varchar(50),              
 @ADDRESS1 varchar(50),              
 @ADDRESS2 varchar(50),              
 @CITY varchar(50),              
 @STATE int,              
 @ZIP varchar(10),              
 @HOME_PHONE varchar(15),              
 @WORK_PHONE varchar(15),              
 @MOBILE_PHONE varchar(15),              
 @EXTENSION varchar(5),              
 @MODIFIED_BY int,              
 @LAST_UPDATED_DATETIME datetime,            
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
              
 UPDATE               
  CLM_DRIVER_INFORMATION               
 SET                                            
  TYPE_OF_DRIVER=@TYPE_OF_DRIVER,              
  NAME=@NAME,              
  ADDRESS1=@ADDRESS1,              
  ADDRESS2=@ADDRESS2,              
  CITY=@CITY,              
  STATE=@STATE,              
  ZIP=@ZIP,              
  HOME_PHONE=@HOME_PHONE,              
  WORK_PHONE=@WORK_PHONE,              
  MOBILE_PHONE=@MOBILE_PHONE,              
  EXTENSION=@EXTENSION,              
  MODIFIED_BY=@MODIFIED_BY,              
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,            
 RELATION_INSURED=@RELATION_INSURED,            
 DATE_OF_BIRTH=@DATE_OF_BIRTH,            
 LICENSE_NUMBER=@LICENSE_NUMBER,            
 LICENSE_STATE= @LICENSE_STATE,            
 PURPOSE_OF_USE= @PURPOSE_OF_USE,            
 USED_WITH_PERMISSION= @USED_WITH_PERMISSION,            
 DESCRIBE_DAMAGE= @DESCRIBE_DAMAGE,            
 ESTIMATE_AMOUNT= @ESTIMATE_AMOUNT,          
 OTHER_VEHICLE_INSURANCE = @OTHER_VEHICLE_INSURANCE,      
 VEHICLE_ID=@VEHICLE_ID,    
 COUNTRY = @COUNTRY,  
 SEX = @SEX,
 SSN=@SSN,
 VEHICLE_OWNER = @VEHICLE_OWNER,
 TYPE_OF_OWNER = @TYPE_OF_OWNER ,
 DRIVERS_INJURY= @DRIVERS_INJURY            
 WHERE              
  CLAIM_ID=@CLAIM_ID and              
  DRIVER_ID = @DRIVER_ID                 
                          
END                  
                
            
          
        
      
    
  













GO

