IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_WATERCRAFT_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_WATERCRAFT_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertCLM_WATERCRAFT_PROPERTY_DAMAGED         
Created by      : Sumit Chhabra                
Date            : 24 May,2006                  
Purpose         : Inserts records int CLM_WATERCRAFT_PROPERTY_DAMAGED             
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------   */              
              
CREATE PROCEDURE dbo.Proc_InsertCLM_WATERCRAFT_PROPERTY_DAMAGED          
(              
@PROPERTY_DAMAGED_ID int OUTPUT,            
@CLAIM_ID int,            
@DESCRIPTION varchar(300),    
@OTHER_VEHICLE char(1),    
@OTHER_INSURANCE_NAME varchar(150),    
@OTHER_OWNER_NAME varchar(150),    
@ADDRESS1 varchar(50),    
@ADDRESS2 varchar(50),    
@CITY varchar(10),    
@STATE int,    
@ZIP varchar(10),    
@HOME_PHONE varchar(15),    
@WORK_PHONE varchar(15),    
@CREATED_BY int,    
@CREATED_DATETIME datetime
)              
              
As              
BEGIN              
 SELECT     
  @PROPERTY_DAMAGED_ID = ISNULL(MAX(PROPERTY_DAMAGED_ID),0) + 1     
 FROM     
  CLM_WATERCRAFT_PROPERTY_DAMAGED           
 WHERE     
  CLAIM_ID=@CLAIM_ID            
    
 INSERT INTO             
  CLM_WATERCRAFT_PROPERTY_DAMAGED                  
  (            
  PROPERTY_DAMAGED_ID,    
  CLAIM_ID,    
  DESCRIPTION,    
  OTHER_VEHICLE,    
  OTHER_INSURANCE_NAME,    
  OTHER_OWNER_NAME,    
  ADDRESS1,    
  ADDRESS2,    
  CITY,    
  STATE,    
  ZIP,    
  HOME_PHONE,    
  WORK_PHONE,    
  CREATED_BY,    
  CREATED_DATETIME,    
  IS_ACTIVE         
  )            
 VALUES            
 (            
  @PROPERTY_DAMAGED_ID,    
  @CLAIM_ID,    
  @DESCRIPTION,    
  @OTHER_VEHICLE,    
  @OTHER_INSURANCE_NAME,    
  @OTHER_OWNER_NAME,    
  @ADDRESS1,    
  @ADDRESS2,    
  @CITY,    
  @STATE,    
  @ZIP,    
  @HOME_PHONE,    
  @WORK_PHONE,    
  @CREATED_BY,    
  @CREATED_DATETIME,    
  'Y'
 )           
             
            
END              
            
          
        
      
    
  



GO

