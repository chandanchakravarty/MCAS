IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_OWNER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_OWNER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                          
Proc Name       : dbo.Proc_UpdateCLM_OWNER_INFORMATION                                    
Created by      : Sumit Chhabra                                        
Date            : 02/05/2006                                          
Purpose         : Update data in CLM_OWNER_INFORMATION  table for claim owner screen                      
Created by      : Sumit Chhabra                                         
Revison History :                                          
Used In        : Wolverine                                          
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/ 
-- DROP PROC  dbo.Proc_UpdateCLM_OWNER_INFORMATION                                           
CREATE PROC dbo.Proc_UpdateCLM_OWNER_INFORMATION                                                                  
@OWNER_ID int,        
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
@MODIFIED_BY int,        
@LAST_UPDATED_DATETIME datetime,        
@WORK_PHONE varchar(15),        
@EXTENSION varchar(5),    
@VEHICLE_OWNER int,  
@VEHICLE_ID int,
@COUNTRY INT                      
AS                                          
BEGIN                                          
        
 UPDATE         
  CLM_OWNER_INFORMATION                    
 SET           
  TYPE_OF_OWNER = @TYPE_OF_OWNER,        
  NAME=@NAME,        
  ADDRESS1=@ADDRESS1,        
  ADDRESS2=@ADDRESS2,        
  CITY=@CITY,        
  STATE=@STATE,        
  ZIP=@ZIP,        
  HOME_PHONE=@HOME_PHONE,        
  MOBILE_PHONE=@MOBILE_PHONE,        
  DEFAULT_PHONE_TO_NOTICE=@DEFAULT_PHONE_TO_NOTICE,        
  PRODUCTS_INSURED_IS=@PRODUCTS_INSURED_IS,        
  OTHER_DESCRIPTION=@OTHER_DESCRIPTION,        
  TYPE_OF_PRODUCT=@TYPE_OF_PRODUCT,        
  WHERE_PRODUCT_SEEN=@WHERE_PRODUCT_SEEN,        
  OTHER_LIABILITY=@OTHER_LIABILITY,        
  MODIFIED_BY=@MODIFIED_BY,        
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,        
  WORK_PHONE=@WORK_PHONE,        
  EXTENSION=@EXTENSION,    
  VEHICLE_OWNER=@VEHICLE_OWNER,  
 VEHICLE_ID=@VEHICLE_ID,
 COUNTRY=@COUNTRY    
 WHERE        
  OWNER_ID=@OWNER_ID AND      
  CLAIM_ID=@CLAIM_ID        
              
END          
        
        
      
    
  





GO

