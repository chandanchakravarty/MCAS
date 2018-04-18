IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_HOME_OWNER_PER_ART_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_HOME_OWNER_PER_ART_GEN_INFO]
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
----------------------------------
Modified By    : Mohit
Modified On    : 20/10/2005
Purpose        : Adding field DESC_PROPERTY_USE_PROF_COMM     
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_UpdateAPP_HOME_OWNER_PER_ART_GEN_INFO  
(  
 @CUSTOMER_ID int,  
 @APP_ID      int,  
 @APP_VERSION_ID smallint,  
 @PROPERTY_EXHIBITED      nchar(2),  
 @DESC_PROPERTY_EXHIBITED      nvarchar(510),  
 @DEDUCTIBLE_APPLY       nchar(2),  
 @DESC_DEDUCTIBLE_APPLY      nvarchar(510),  
 @PROPERTY_USE_PROF_COMM      nchar(2),  
 @OTHER_INSU_WITH_COMPANY      nchar(2),  
 @DESC_INSU_WITH_COMPANY      nvarchar(510),  
 @LOSS_OCCURED_LAST_YEARS      nchar(2),  
 @DESC_LOSS_OCCURED_LAST_YEARS   nvarchar(510),  
 @DECLINED_CANCELED_COVERAGE     nchar(2),  
 @ADD_RATING_COV_INFO      nvarchar(510),  
 @MODIFIED_BY       int,  
 @LAST_UPDATED_DATETIME      datetime,
 @DESC_PROPERTY_USE_PROF_COMM nvarchar(510)  
)  
AS  
BEGIN  
 UPDATE APP_HOME_OWNER_PER_ART_GEN_INFO  
 SET PROPERTY_EXHIBITED = @PROPERTY_EXHIBITED,  
  DESC_PROPERTY_EXHIBITED = @DESC_PROPERTY_EXHIBITED,  
  DEDUCTIBLE_APPLY = @DEDUCTIBLE_APPLY,  
  DESC_DEDUCTIBLE_APPLY = @DESC_DEDUCTIBLE_APPLY,  
  PROPERTY_USE_PROF_COMM = @PROPERTY_USE_PROF_COMM,  
  OTHER_INSU_WITH_COMPANY = @OTHER_INSU_WITH_COMPANY,  
  DESC_INSU_WITH_COMPANY = @DESC_INSU_WITH_COMPANY,  
  LOSS_OCCURED_LAST_YEARS = @LOSS_OCCURED_LAST_YEARS,  
  DESC_LOSS_OCCURED_LAST_YEARS = @DESC_LOSS_OCCURED_LAST_YEARS,  
  DECLINED_CANCELED_COVERAGE = @DECLINED_CANCELED_COVERAGE,  
  ADD_RATING_COV_INFO = @ADD_RATING_COV_INFO,  
  MODIFIED_BY = @MODIFIED_BY,  
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,
  DESC_PROPERTY_USE_PROF_COMM=@DESC_PROPERTY_USE_PROF_COMM  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
END  
  
  



GO

