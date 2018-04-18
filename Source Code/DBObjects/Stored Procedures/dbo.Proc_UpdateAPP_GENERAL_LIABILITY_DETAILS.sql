IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_GENERAL_LIABILITY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_GENERAL_LIABILITY_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateAPP_GENERAL_LIABILITY_DETAILS    
Created by      : Sumit Chhabra    
Date            : 03/29/2006    
Purpose        : To update recordes at APP_GENERAL_LIABILITY_DETAILS  
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_UpdateAPP_GENERAL_LIABILITY_DETAILS  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,  
 @APP_GEN_ID int,  
 @LOCATION_ID smallint,  
 @CLASS_CODE varchar(4),  
 @BUSINESS_DESCRIPTION varchar(100),  
 @COVERAGE_TYPE int,  
 @COVERAGE_FORM int,  
 @EXPOSURE_BASE int,  
 @EXPOSURE int,  
 @RATE int,   
 @MODIFIED_BY int,  
 @LAST_UPDATED_DATETIME datetime   
)AS    
BEGIN    
update APP_GENERAL_LIABILITY_DETAILS set  
 LOCATION_ID=@LOCATION_ID,  
 CLASS_CODE=@CLASS_CODE,  
 BUSINESS_DESCRIPTION=@BUSINESS_DESCRIPTION,  
 COVERAGE_TYPE=@COVERAGE_TYPE,  
 COVERAGE_FORM=@COVERAGE_FORM,  
 EXPOSURE_BASE=@EXPOSURE_BASE,  
 EXPOSURE=@EXPOSURE,  
 RATE=@RATE,   
 MODIFIED_BY=@MODIFIED_BY,  
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
where  
  CUSTOMER_ID=@CUSTOMER_ID and  
 APP_ID=@APP_ID and  
 APP_VERSION_ID=@APP_VERSION_ID --and  
-- APP_GEN_ID=@APP_GEN_ID  
  
end    
  


GO

