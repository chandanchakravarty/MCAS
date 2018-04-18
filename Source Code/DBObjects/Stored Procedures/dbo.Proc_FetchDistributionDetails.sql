IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchDistributionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchDistributionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Proc_FetchDistributionDetails    
Created by      : Praveen K  
Date            : 10 Dec 2007  
Purpose       : Get Distribution Details 
Revison History :    
  
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       --------------------------------*/    
--drop proc dbo.Proc_FetchDistributionDetails    
-- DROP PROC dbo.Proc_FetchDistributionDetails    
CREATE PROCEDURE dbo.Proc_FetchDistributionDetails 
(    
 @CD_LINE_ITEM_ID  INT,
 @CALLED_FROM VARCHAR(10)
)  
AS  
Begin

if(@CALLED_FROM = 'CHQ')
BEGIN
	SELECT
	ISNULL(ACCOUNT_ID,0) AS ACCOUNT_ID,
	CAST(ISNULL(DISTRIBUTION_PERCT,0) AS INTEGER) AS DISTRIBUTION_PERCT,
	ISNULL(DISTRIBUTION_AMOUNT,0) AS DISTRIBUTION_AMOUNT
	FROM 
	TEMP_ACT_DISTRIBUTION_DETAILS AD with (nolock)
	WHERE AD.IDEN_ROW_ID = @CD_LINE_ITEM_ID
END
ELSE
BEGIN
	SELECT
	ISNULL(ACCOUNT_ID,0) AS ACCOUNT_ID,
	CAST(ISNULL(DISTRIBUTION_PERCT,0) AS INTEGER) AS DISTRIBUTION_PERCT,
	ISNULL(DISTRIBUTION_AMOUNT,0) AS DISTRIBUTION_AMOUNT
	FROM 
	ACT_DISTRIBUTION_DETAILS AD with (nolock)
	WHERE AD.IDEN_ROW_ID = @CD_LINE_ITEM_ID

END

end











GO

