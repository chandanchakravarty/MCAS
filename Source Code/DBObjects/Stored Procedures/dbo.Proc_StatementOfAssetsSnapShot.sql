IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_StatementOfAssetsSnapShot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_StatementOfAssetsSnapShot]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC [Proc_StatementOfAssetsSnapShot]
--GO
/*----------------------------------------------------------          
Proc Name        : dbo.Proc_StatementOfAssetsSnapShot          
Created by       : Raghav Gupta         
Date             : 08/01/2010
Purpose          : Procedure to Save Assets and Liabilities (Balance Sheet) Itrack Issue 6898.           
Revison History  :          
Used In          : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC [dbo].[Proc_StatementOfAssetsSnapShot]
(
@CURRENT_DATE DATETIME
)

AS 

DECLARE @MONTH INT 
DECLARE @YEAR INT
DECLARE @DAY INT 
DECLARE @GL_ID INT
DECLARE @YEARFROM INT
DECLARE @YEARTO INT


SET @MONTH = DATEPART(MM,@CURRENT_DATE)
SET @YEAR = DATEPART(YY,@CURRENT_DATE)
SET @YEARFROM = @YEAR
SET @YEARTO = @YEAR
SET @DAY = DATEPART(DD,@CURRENT_DATE)
SELECT   @GL_ID =   GL_ID  FROM ACT_GENERAL_LEDGER_TOTALS WHERE FISCAL_START_YEAR = @YEAR

BEGIN

IF  EXISTS (SELECT * FROM RPT_STATEMENT_ASSETS with(nolock) 
			WHERE  GL_ID =  @GL_ID and RPT_MONTH = @MONTH  and RPT_YEAR = @YEAR and RPT_DAY = @DAY)

BEGIN
            DELETE RPT_STATEMENT_ASSETS 
			WHERE  GL_ID =  @GL_ID and RPT_MONTH = @MONTH  and RPT_YEAR = @YEAR and RPT_DAY = @DAY              
END
		

BEGIN
		INSERT INTO RPT_STATEMENT_ASSETS		
        (  
		GL_ID ,	ACCOUNT_ID ,ACC_NUMBER ,ACC_TYPE ,ACC_TYPE_DESC ,YEAR_MTD ,PRIOR_YEAR_MTD ,VARIANCE_MTD ,
		CHNG_MTD ,YEAR_YTD ,PRIOR_YEAR_YTD ,VARIANCE_YTD ,CHNG_YTD ,LEDGER_NAME ,ACC_DESC 
         ) 
		EXEC rpt_StatementofAssets @GL_ID, @YEARFROM ,@YEARTO,@MONTH

			UPDATE RPT_STATEMENT_ASSETS  
			SET RPT_MONTH = @MONTH , RPT_YEAR = @YEAR , RPT_DAY  = @DAY 
            WHERE GL_ID =  @GL_ID 
			 	
	
END

   
END

--GO
--EXEC Proc_StatementOfAssetsSnapShot '01/01/2009'
--ROLLBACK TRAN
















GO

