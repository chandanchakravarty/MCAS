IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoverageRangeXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoverageRangeXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetCoverageRangeXml  
Created by           : Mohit Gupta  
Date                    : 5/07/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_GetCoverageRangeXml  
CREATE   PROCEDURE Proc_GetCoverageRangeXml  
(  
	 @COV_ID int,  
	 --@IS_ACTIVE int,  
	 @LIMIT_DEDUC_TYPE varchar(10),  
	 @Is_DeactiveInclude varchar(1),  
	 @CurrentPage int,  
	 @PageSize int,  
	 @TotalRecords int output  
)  
AS  
CREATE TABLE #TempTable  
(  
   
 RowId int Identity,  
 LIMIT_DEDUC_ID int,  
 COV_ID int,  
 LIMIT_DEDUC_TYPE varchar(10),  
 LIMIT_DEDUC_AMOUNT decimal(18,2),  
 IS_ACTIVE bit,  
 RANK int,  
 LIMIT_DEDUC_AMOUNT1 decimal(18,2) ,  
 LIMIT_DEDUC_AMOUNT_TEXT NVarChar(100),
 LIMIT_DEDUC_AMOUNT1_TEXT NVarChar(100),
 EFFECTIVE_FROM_DATE datetime,
 EFFECTIVE_TO_DATE datetime,
 DISABLED_DATE datetime
)  
BEGIN  
--declare @IS_ACTIVE  
  
INSERT INTO #TempTable   
(  
 	LIMIT_DEDUC_ID,
	COV_ID,LIMIT_DEDUC_TYPE,LIMIT_DEDUC_AMOUNT,
	IS_ACTIVE,RANK,
	LIMIT_DEDUC_AMOUNT1,
	LIMIT_DEDUC_AMOUNT_TEXT,
	LIMIT_DEDUC_AMOUNT1_TEXT,
	EFFECTIVE_FROM_DATE ,
	EFFECTIVE_TO_DATE,
	DISABLED_DATE   
)  
SELECT   
	LIMIT_DEDUC_ID,COV_ID,LIMIT_DEDUC_TYPE,
	LIMIT_DEDUC_AMOUNT,
	IS_ACTIVE,RANK,LIMIT_DEDUC_AMOUNT1,
	LIMIT_DEDUC_AMOUNT_TEXT,
	LIMIT_DEDUC_AMOUNT1_TEXT,   
	CONVERT(varchar,ISNULL(EFFECTIVE_FROM_DATE,'1950-01-01 16:50:49.333'),101) as EFFECTIVE_FROM_DATE ,
	CONVERT(varchar,EFFECTIVE_TO_DATE,101) AS EFFECTIVE_TO_DATE,
	CONVERT(varchar,DISABLED_DATE,101) AS DISABLED_DATE  
 FROM  MNT_COVERAGE_RANGES  
 WHERE  COV_ID=@COV_ID AND LIMIT_DEDUC_TYPE=@LIMIT_DEDUC_TYPE  
  
  
DECLARE @FirstRec int, @LastRec int  
SELECT @FirstRec = (@CurrentPage - 1) * @PageSize  
SELECT @LastRec = (@CurrentPage * @PageSize + 1)  
  
IF (@Is_DeactiveInclude = 'Y')  
	BEGIN  
		SELECT  
		LIMIT_DEDUC_ID, COV_ID, LIMIT_DEDUC_TYPE, 
		LIMIT_DEDUC_AMOUNT, IS_ACTIVE,RANK,LIMIT_DEDUC_AMOUNT1,
		LIMIT_DEDUC_AMOUNT_TEXT,
		LIMIT_DEDUC_AMOUNT1_TEXT,
		CONVERT(varchar,ISNULL(EFFECTIVE_FROM_DATE,'3000-03-16 16:59:06.630'),101) as EFFECTIVE_FROM_DATE ,
		CONVERT(varchar,EFFECTIVE_TO_DATE,101) AS EFFECTIVE_TO_DATE,
		CONVERT(varchar,DISABLED_DATE,101) AS DISABLED_DATE     	     
		FROM #TempTable  
		WHERE RowId  > @FirstRec  AND  RowId < @LastRec ORDER BY IS_ACTIVE DESC  
	END   
ELSE  
	BEGIN    
		SELECT  
		LIMIT_DEDUC_ID, COV_ID, LIMIT_DEDUC_TYPE, 
		LIMIT_DEDUC_AMOUNT, IS_ACTIVE,RANK,LIMIT_DEDUC_AMOUNT1,
		LIMIT_DEDUC_AMOUNT_TEXT,
		LIMIT_DEDUC_AMOUNT1_TEXT,
		CONVERT(varchar,ISNULL(EFFECTIVE_FROM_DATE,'3000-03-16 16:59:06.630'),101) as EFFECTIVE_FROM_DATE ,
		CONVERT(varchar,EFFECTIVE_TO_DATE,101) AS EFFECTIVE_TO_DATE,
		CONVERT(varchar,DISABLED_DATE,101) AS DISABLED_DATE        
		FROM #TempTable  
		WHERE IS_ACTIVE='1' AND RowId  > @FirstRec  AND  RowId < @LastRec  
		END  
SELECT @TotalRecords = COUNT(*) FROM #TempTable   
END  









GO

