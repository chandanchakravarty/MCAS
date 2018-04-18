IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_GENERAL_LIABILITY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_GENERAL_LIABILITY_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertAPP_GENERAL_LIABILITY_DETAILS        
Created by      : Sumit Chhabra        
Date            : 03/29/2006        
Purpose        : To insert recordes in APP_GENERAL_LIABILITY_DETAILS      
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_InsertAPP_GENERAL_LIABILITY_DETAILS      
(      
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint,      
 @APP_GEN_ID int out,      
 @LOCATION_ID smallint,      
 @CLASS_CODE varchar(4),      
 @BUSINESS_DESCRIPTION varchar(100),      
 @COVERAGE_TYPE int,      
 @COVERAGE_FORM int,      
 @EXPOSURE_BASE int,      
 @EXPOSURE int,      
 @RATE int,       
 @CREATED_BY int,      
 @CREATED_DATETIME datetime       
)AS        
BEGIN        
SELECT @APP_GEN_ID=isnull(max(APP_GEN_ID),0)+1 from APP_GENERAL_LIABILITY_DETAILS where    
  CUSTOMER_ID=@CUSTOMER_ID and      
  APP_ID=@APP_ID and      
  APP_VERSION_ID=@APP_VERSION_ID     
    
insert into APP_GENERAL_LIABILITY_DETAILS      
(      
  CUSTOMER_ID,      
 APP_ID,      
 APP_VERSION_ID,      
 APP_GEN_ID,      
 LOCATION_ID,      
 CLASS_CODE,      
 BUSINESS_DESCRIPTION,      
 COVERAGE_TYPE,      
 COVERAGE_FORM,      
 EXPOSURE_BASE,      
 EXPOSURE,      
 RATE,       
  IS_ACTIVE,      
 CREATED_BY,      
 CREATED_DATETIME       
)      
values      
(      
 @CUSTOMER_ID,      
 @APP_ID,      
 @APP_VERSION_ID,      
 @APP_GEN_ID,      
 @LOCATION_ID,      
 @CLASS_CODE,      
 @BUSINESS_DESCRIPTION,      
 @COVERAGE_TYPE,      
 @COVERAGE_FORM,      
 @EXPOSURE_BASE,      
 @EXPOSURE,      
 @RATE,       
 'Y',      
 @CREATED_BY,      
 @CREATED_DATETIME        
)      
      
end        


GO

