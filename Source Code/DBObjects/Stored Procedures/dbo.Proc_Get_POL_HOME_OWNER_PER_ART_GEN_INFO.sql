IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_HOME_OWNER_PER_ART_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_HOME_OWNER_PER_ART_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_Get_POL_HOME_OWNER_PER_ART_GEN_INFO    
Created by      : Pradeep Iyer    
Date            : 11/14/2005    
Purpose     	: Gets a single record from POL_HOME_OWNER_PER_ART_GEN_INFO
Revison History :    
Used In        : Wolverine    

------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_Get_POL_HOME_OWNER_PER_ART_GEN_INFO
(    
	 @CUSTOMER_ID int,    
	 @POLICY_ID      int,    
	 @POLICY_VERSION_ID smallint    
)    
AS    
BEGIN    
 SELECT  
	 PROPERTY_EXHIBITED,    
  DESC_PROPERTY_EXHIBITED,
	 DEDUCTIBLE_APPLY, 
	DESC_DEDUCTIBLE_APPLY,    
  PROPERTY_USE_PROF_COMM, 
	OTHER_INSU_WITH_COMPANY, 
	DESC_INSU_WITH_COMPANY,    
  LOSS_OCCURED_LAST_YEARS,
	 DESC_LOSS_OCCURED_LAST_YEARS, DECLINED_CANCELED_COVERAGE,    
  ADD_RATING_COV_INFO,     
  IS_ACTIVE, 
	CREATED_BY, 
	CREATED_DATETIME,
	 MODIFIED_BY, 
	LAST_UPDATED_DATETIME,
	DESC_PROPERTY_USE_PROF_COMM    
 FROM POL_HOME_OWNER_PER_ART_GEN_INFO    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID    
END    
    
    
  



GO

