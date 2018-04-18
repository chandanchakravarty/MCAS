IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_DRIVER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_DRIVER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                                              
Proc Name       : dbo.Proc_GetCLM_DRIVER_INFORMATION                                        
Created by      : Sumit Chhabra                                            
Date            : 04/05/2006                                              
Purpose         : Fetch data from CLM_DRIVER_INFORMATION table for driver details screen                          
Created by      : Sumit Chhabra                                             
Revison History :                                              
Used In        : Wolverine                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_GetCLM_DRIVER_INFORMATION                                             
CREATE PROC dbo.Proc_GetCLM_DRIVER_INFORMATION                                                       
@CLAIM_ID int,          
@DRIVER_ID int                        
AS                                              
BEGIN                                              
 SELECT                                 
  CLAIM_ID,TYPE_OF_DRIVER,NAME,ADDRESS1,ADDRESS2,CITY,STATE,ZIP,HOME_PHONE,          
  WORK_PHONE,          
  MOBILE_PHONE,          
  RELATION_INSURED,          
  CONVERT(CHAR,DATE_OF_BIRTH,101)   DATE_OF_BIRTH,            
  LICENSE_NUMBER,          
  LICENSE_STATE,          
  PURPOSE_OF_USE,          
  USED_WITH_PERMISSION,          
  DESCRIBE_DAMAGE,          
  OTHER_VEHICLE_INSURANCE,          
  IS_ACTIVE,          
  EXTENSION,          
  ESTIMATE_AMOUNT,        
  OTHER_VEHICLE_INSURANCE,      
  VEHICLE_ID,    
  COUNTRY,  
  SEX,SSN,VEHICLE_OWNER,
  TYPE_OF_OWNER,
  DRIVERS_INJURY
    
 FROM                           
  CLM_DRIVER_INFORMATION                           
 WHERE                                       
  CLAIM_ID = @CLAIM_ID AND           
  DRIVER_ID = @DRIVER_ID          
END                                        
                                    
              
            
          
        
      
    
  








GO

