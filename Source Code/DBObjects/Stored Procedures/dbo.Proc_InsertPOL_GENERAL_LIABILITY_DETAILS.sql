IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_GENERAL_LIABILITY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_GENERAL_LIABILITY_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertPOL_GENERAL_LIABILITY_DETAILS          
Created by      : Ravindra Gupta  
Date            : 04-03-2006  
Purpose         : To insert records in POL_GENERAL_LIABILITY_DETAILS        
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
create PROC Dbo.Proc_InsertPOL_GENERAL_LIABILITY_DETAILS        
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID smallint,        
-- @POLICY_GEN_ID int out,        
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
declare @POLICY_GEN_ID int
SELECT @POLICY_GEN_ID=isnull(max(POLICY_GEN_ID),0)+1 from POL_GENERAL_LIABILITY_DETAILS where      
  CUSTOMER_ID=@CUSTOMER_ID and        
  POLICY_ID=@POLICY_ID and        
  POLICY_VERSION_ID=@POLICY_VERSION_ID       
      
insert into POL_GENERAL_LIABILITY_DETAILS        
(        
 CUSTOMER_ID,        
 POLICY_ID,        
 POLICY_VERSION_ID,        
 POLICY_GEN_ID,        
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
 @POLICY_ID,        
 @POLICY_VERSION_ID,        
 @POLICY_GEN_ID,        
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

