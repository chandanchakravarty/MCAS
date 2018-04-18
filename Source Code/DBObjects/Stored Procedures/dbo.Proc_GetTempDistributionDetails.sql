IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTempDistributionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTempDistributionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : dbo.Proc_GetDistributionDetails
Created by           : ajit singh chahal
Date                    : 13/09/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_GetTempDistributionDetails 
CREATE   PROCEDURE dbo.Proc_GetTempDistributionDetails
(
	@CD_LINE_ITEM_ID int,
	@CALLEDFROM varchar(5),
	@CurrentPage int,
	@PageSize int,
	@TotalRecords int output
		
)
AS
CREATE TABLE #TempTable
(
	RowId int Identity,
	IDEN_ROW_ID int,
	GROUP_ID int,
      	GROUP_TYPE varchar(5),
      	ACCOUNT_ID int,
      	DISTRIBUTION_PERCT decimal(18,2),
      	DISTRIBUTION_AMOUNT decimal(18,2) ,
      	NOTE nvarchar(255)
)
BEGIN
INSERT INTO #TempTable 
(
	IDEN_ROW_ID,
	GROUP_ID,
      	GROUP_TYPE,
      	ACCOUNT_ID,
      	DISTRIBUTION_PERCT,
      	DISTRIBUTION_AMOUNT ,
      	NOTE
)
SELECT  IDEN_ROW_ID,GROUP_ID,GROUP_TYPE,ACCOUNT_ID,DISTRIBUTION_PERCT,DISTRIBUTION_AMOUNT,NOTE
FROM  TEMP_ACT_DISTRIBUTION_DETAILS
WHERE GROUP_ID=@CD_LINE_ITEM_ID AND GROUP_TYPE=@CALLEDFROM


DECLARE @FirstRec int, @LastRec int
SELECT @FirstRec = (@CurrentPage - 1) * @PageSize
SELECT @LastRec = (@CurrentPage * @PageSize + 1)


SELECT 
 IDEN_ROW_ID,GROUP_ID,GROUP_TYPE,ACCOUNT_ID,DISTRIBUTION_PERCT,DISTRIBUTION_AMOUNT,NOTE
FROM 
#TempTable
WHERE 
	RowId > @FirstRec 
AND
	RowId < @LastRec

---Itrack #3823 : 
 SELECT 
  ISNULL(SUM(DISTRIBUTION_AMOUNT),0)  DISTRIBUTION_AMOUNT_SUM
 FROM 
 #TempTable
 WHERE 
  RowId <= @FirstRec 
 or
  RowId >= @LastRec



SELECT @TotalRecords = COUNT(*) FROM #TempTable 

END


GO

