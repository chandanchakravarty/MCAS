IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_OWNER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_OWNER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                            
Proc Name       : dbo.Proc_InsertCLM_OWNER_INFORMATION                                      
Created by      : Sumit Chhabra                                          
Date            : 02/05/2006                                            
Purpose         : Insert data in CLM_OWNER_INFORMATION  table for claim owner screen                        
Created by      : Sumit Chhabra                                           
Revison History :                                            
Used In        : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------*/
--DROP PROC dbo.Proc_InsertCLM_OWNER_INFORMATION                                             
CREATE PROC dbo.Proc_InsertCLM_OWNER_INFORMATION                                                                    
@OWNER_ID int output,          
@CLAIM_ID int,          
@TYPE_OF_OWNER int,          
@NAME varchar(50),          
@ADDRESS1 varchar(50),          
@ADDRESS2 varchar(50),          
@CITY varchar(50),          
@STATE int,          
@ZIP varchar(10),          
@HOME_PHONE varchar(15),          
@MOBILE_PHONE varchar(15),          
@DEFAULT_PHONE_TO_NOTICE varchar(15),          
@PRODUCTS_INSURED_IS int,          
@OTHER_DESCRIPTION varchar(25),          
@TYPE_OF_PRODUCT varchar(256),          
@WHERE_PRODUCT_SEEN varchar(256),          
@OTHER_LIABILITY varchar(256),          
@CREATED_BY int,          
@CREATED_DATETIME datetime,          
@WORK_PHONE varchar(15),          
@EXTENSION varchar(5),      
@VEHICLE_OWNER int,    
@TYPE_OF_HOME char(1),  
@VEHICLE_ID int,
@COUNTRY int    
AS                                            
BEGIN                                            
 SELECT                         
  @OWNER_ID = ISNULL(MAX(OWNER_ID),0)+1                         
 FROM                         
  CLM_OWNER_INFORMATION         
 WHERE        
 CLAIM_ID = @CLAIM_ID                          
                      
           
 INSERT INTO CLM_OWNER_INFORMATION          
 (                        
  OWNER_ID,          
  CLAIM_ID,          
  TYPE_OF_OWNER,          
  NAME,          
  ADDRESS1,          
  ADDRESS2,          
  CITY,          
  STATE,          
  ZIP,          
  HOME_PHONE,          
  MOBILE_PHONE,          
  DEFAULT_PHONE_TO_NOTICE,          
  PRODUCTS_INSURED_IS,          
  OTHER_DESCRIPTION,          
  TYPE_OF_PRODUCT,          
  WHERE_PRODUCT_SEEN,          
  OTHER_LIABILITY,          
  CREATED_BY,          
  CREATED_DATETIME,          
  WORK_PHONE,          
  EXTENSION,          
  IS_ACTIVE,      
  VEHICLE_OWNER,    
  TYPE_OF_HOME,  
  VEHICLE_ID,
	COUNTRY    
  )                        
 VALUES                        
  (                        
  @OWNER_ID,          
  @CLAIM_ID,          
  @TYPE_OF_OWNER,          
  @NAME,          
  @ADDRESS1,          
  @ADDRESS2,          
  @CITY,          
  @STATE,          
  @ZIP,          
  @HOME_PHONE,          
  @MOBILE_PHONE,          
  @DEFAULT_PHONE_TO_NOTICE,          
  @PRODUCTS_INSURED_IS,          
  @OTHER_DESCRIPTION,          
  @TYPE_OF_PRODUCT,          
  @WHERE_PRODUCT_SEEN,          
  @OTHER_LIABILITY,          
  @CREATED_BY,          
  @CREATED_DATETIME,          
  @WORK_PHONE,          
  @EXTENSION,          
  'Y',      
  @VEHICLE_OWNER,    
  @TYPE_OF_HOME,  
  @VEHICLE_ID,
	@COUNTRY
   )                          
                
END            
          
          
        
      
    
  





GO

