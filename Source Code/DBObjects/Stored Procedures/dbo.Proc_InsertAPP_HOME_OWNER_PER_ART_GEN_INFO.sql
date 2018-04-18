IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_HOME_OWNER_PER_ART_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_HOME_OWNER_PER_ART_GEN_INFO]
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
Modified By    : Mohit
Modified On    : 20/10/2005
Purpose        : Adding field DESC_PROPERTY_USE_PROF_COMM
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_InsertAPP_HOME_OWNER_PER_ART_GEN_INFO  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint,  
 @PROPERTY_EXHIBITED     nchar(2),  
 @DESC_PROPERTY_EXHIBITED     nvarchar(510),  
 @DEDUCTIBLE_APPLY     nchar(2),  
 @DESC_DEDUCTIBLE_APPLY     nvarchar(510),  
 @PROPERTY_USE_PROF_COMM     nchar(2),  
 @OTHER_INSU_WITH_COMPANY     nchar(2),  
 @DESC_INSU_WITH_COMPANY     nvarchar(510),  
 @LOSS_OCCURED_LAST_YEARS     nchar(2),  
 @DESC_LOSS_OCCURED_LAST_YEARS     nvarchar(510),  
 @DECLINED_CANCELED_COVERAGE     nchar(2),  
 @ADD_RATING_COV_INFO     nvarchar(510),  
 @CREATED_BY     int,  
 @CREATED_DATETIME     datetime,
 @DESC_PROPERTY_USE_PROF_COMM nvarchar(510)  
)  
AS  
BEGIN  
 INSERT INTO APP_HOME_OWNER_PER_ART_GEN_INFO  
 (  
  CUSTOMER_ID,  
  APP_ID,  
  APP_VERSION_ID,  
  PROPERTY_EXHIBITED,  
  DESC_PROPERTY_EXHIBITED,  
  DEDUCTIBLE_APPLY,  
  DESC_DEDUCTIBLE_APPLY,  
  PROPERTY_USE_PROF_COMM,  
  OTHER_INSU_WITH_COMPANY,  
  DESC_INSU_WITH_COMPANY,  
  LOSS_OCCURED_LAST_YEARS,  
  DESC_LOSS_OCCURED_LAST_YEARS,  
  DECLINED_CANCELED_COVERAGE,  
  ADD_RATING_COV_INFO,  
  IS_ACTIVE,  
  CREATED_BY,  
  CREATED_DATETIME,
  DESC_PROPERTY_USE_PROF_COMM	  
 )  
 VALUES  
 (  
  @CUSTOMER_ID,  
  @APP_ID,  
  @APP_VERSION_ID,  
  @PROPERTY_EXHIBITED,  
  @DESC_PROPERTY_EXHIBITED,  
  @DEDUCTIBLE_APPLY,  
  @DESC_DEDUCTIBLE_APPLY,  
  @PROPERTY_USE_PROF_COMM,  
  @OTHER_INSU_WITH_COMPANY,  
  @DESC_INSU_WITH_COMPANY,  
  @LOSS_OCCURED_LAST_YEARS,  
  @DESC_LOSS_OCCURED_LAST_YEARS,  
  @DECLINED_CANCELED_COVERAGE,  
  @ADD_RATING_COV_INFO,  
  'Y',  
  @CREATED_BY,  
  @CREATED_DATETIME,
  @DESC_PROPERTY_USE_PROF_COMM	  
 )  
END  
  
  



GO

