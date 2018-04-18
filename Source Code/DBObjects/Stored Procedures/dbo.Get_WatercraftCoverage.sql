IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Get_WatercraftCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Get_WatercraftCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc Get_WatercraftCoverage 
(
 @CUSTOMER_ID INT,
 @APP_ID INT,
 @APP_VERSION_ID INT,
 @APP_EFFECTIVE_DATE datetime
)

as

begin
 
--DECLARE @CUSTOMER_ID INT
--DECLARE @APP_ID INT
--DECLARE @APP_VERSION_ID INT
--DECLARE @APP_EFFECTIVE_DATE datetime
 
SET @CUSTOMER_ID =920
SET @APP_ID =272
SET @APP_VERSION_ID =1
SET @APP_EFFECTIVE_DATE ='2003-7-5'
 

CREATE TABLE #COVERAGES                                      
(                                      
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                      
 [COV_ID] [int] NOT NULL ,                                      
 [COV_DES] varchar(200),
 [REJECT]  char(1)
 
)
 

INSERT INTO #COVERAGES 
(
 COV_ID,COV_DES,REJECT
)
select MNTC.COV_ID,MNTC.COV_DES,'Y' from APP_WATERCRAFT_COVERAGE_INFO AVC
INNER JOIN MNT_COVERAGE MNTC on MNTC.COV_ID = AVC.COVERAGE_CODE_ID
WHERE 
CUSTOMER_ID=@CUSTOMER_ID
AND APP_ID=@APP_ID
AND APP_VERSION_ID=@APP_VERSION_ID
AND NOT( @APP_EFFECTIVE_DATE  BETWEEN MNTC.EFFECTIVE_FROM_DATE and ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))
 
SELECT COV_DES,REJECT FROM #COVERAGES for xml auto ,elements;
 
drop table #COVERAGES
 
end 


GO

