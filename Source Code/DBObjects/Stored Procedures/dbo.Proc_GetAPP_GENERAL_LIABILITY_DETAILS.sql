IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_GENERAL_LIABILITY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_GENERAL_LIABILITY_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetAPP_GENERAL_LIABILITY_DETAILS        
Created by      : Sumit Chhabra        
Date            : 03/29/2006        
Purpose        : To get recodes of APP_GENERAL_LIABILITY_DETAILS      
Revison History :        
Used In        : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc Proc_GetAPP_GENERAL_LIABILITY_DETAILS    
create PROC Dbo.Proc_GetAPP_GENERAL_LIABILITY_DETAILS      
(      
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID SMALLINT      
--@APP_GEN_ID INT      
)AS        
BEGIN        
select       
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,  
  --LOCATION_ID,
	CLASS_CODE,BUSINESS_DESCRIPTION,COVERAGE_TYPE,    
  COVERAGE_FORM,EXPOSURE_BASE,EXPOSURE,RATE,IS_ACTIVE      
from      
  APP_GENERAL_LIABILITY_DETAILS      
where       
  CUSTOMER_ID  = @CUSTOMER_ID AND       
  APP_ID     =  @APP_ID AND      
  APP_VERSION_ID= @APP_VERSION_ID --AND      
--  APP_GEN_ID  =  @APP_GEN_ID    
end      
  



GO

