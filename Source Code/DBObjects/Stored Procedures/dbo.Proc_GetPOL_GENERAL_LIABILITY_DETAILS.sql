IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_GENERAL_LIABILITY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_GENERAL_LIABILITY_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPOL_GENERAL_LIABILITY_DETAILS        
Created by      : Ravindra     
Date            : 03/29/2006        
Purpose        : To get recodes of POL_GENERAL_LIABILITY_DETAILS      
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc Proc_GetPOL_GENERAL_LIABILITY_DETAILS    
create PROC Dbo.Proc_GetPOL_GENERAL_LIABILITY_DETAILS      
(      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID SMALLINT      
--@POLICY_GEN_ID INT      
)AS        
BEGIN        
select       
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_GEN_ID,      
  CLASS_CODE,BUSINESS_DESCRIPTION,COVERAGE_TYPE,    
  COVERAGE_FORM,EXPOSURE_BASE,EXPOSURE,RATE,IS_ACTIVE      
from      
  POL_GENERAL_LIABILITY_DETAILS      
where       
  CUSTOMER_ID  = @CUSTOMER_ID AND       
  POLICY_ID     =  @POLICY_ID AND      
  POLICY_VERSION_ID= @POLICY_VERSION_ID 
  --POLICY_GEN_ID  =  @POLICY_GEN_ID    
end      
  



GO

