IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_HOME_OWNER_PER_ART_GEN_INFO_XML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_HOME_OWNER_PER_ART_GEN_INFO_XML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.tAPP_HOME_OWNER_PER_ART_GEN_INFO  
Created by      : Vijay Joshi  
Date            : 5/19/2005  
Purpose     :Evaluation  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------
Modified By    : Mohit Gupta
Modified On    : 20/10/2005
Purpose        : Adding field DESC_PROPERTY_USE_PROF_COMM.  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_GetAPP_HOME_OWNER_PER_ART_GEN_INFO_XML
CREATE PROC Dbo.Proc_GetAPP_HOME_OWNER_PER_ART_GEN_INFO_XML  
(  
 @CUSTOMER_ID int,  
 @APP_ID      int,  
 @APP_VERSION_ID smallint  
)  
AS  
BEGIN  
 SELECT  CUSTOMER_ID, APP_ID, APP_VERSION_ID, PROPERTY_EXHIBITED,  
  DESC_PROPERTY_EXHIBITED, DEDUCTIBLE_APPLY, DESC_DEDUCTIBLE_APPLY,  
  PROPERTY_USE_PROF_COMM, OTHER_INSU_WITH_COMPANY, DESC_INSU_WITH_COMPANY,  
  LOSS_OCCURED_LAST_YEARS, DESC_LOSS_OCCURED_LAST_YEARS, DECLINED_CANCELED_COVERAGE,  
  ADD_RATING_COV_INFO,   
  IS_ACTIVE,DESC_PROPERTY_USE_PROF_COMM  
 FROM APP_HOME_OWNER_PER_ART_GEN_INFO  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
END  
  
  





GO

