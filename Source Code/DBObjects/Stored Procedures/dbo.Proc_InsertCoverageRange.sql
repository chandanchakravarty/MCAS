IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCoverageRange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCoverageRange]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_InsertCoverageRange  
Created by           : Mohit Gupta  
Date                    : 5/07/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    
Modified By	: Ravindra
Modify date	: 05-10-2006
purpose  	: Added @EFFECTIVE_FROM_DATE @EFFECTIVE_TO_DATE @DISABLED_DATE 
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_InsertCoverageRange
CREATE   PROCEDURE Proc_InsertCoverageRange  
(  
 --@LIMIT_DEDUC_ID int output,  
 @COV_ID int,  
 @LIMIT_DEDUC_TYPE varchar(10),  
 @RANK  int,   
 @LIMIT_DEDUC_AMOUNT decimal(18,2),  
 @LIMIT_DEDUC_AMOUNT1 decimal(18,2), 
 @LIMIT_DEDUC_AMOUNT_TEXT NVarChar(100),  
 @LIMIT_DEDUC_AMOUNT1_TEXT NVarChar(100),  
 @IS_ACTIVE int,
 @EFFECTIVE_FROM_DATE datetime,
 @EFFECTIVE_TO_DATE datetime, 
 @DISABLED_DATE datetime	  
)  
AS  
BEGIN  
  
DECLARE @LIMIT_DEDUC_ID int  
  
select @LIMIT_DEDUC_ID= IsNull(Max(LIMIT_DEDUC_ID),0) + 1 FROM MNT_COVERAGE_RANGES  
  
  
INSERT INTO MNT_COVERAGE_RANGES  
(  
	LIMIT_DEDUC_ID,  
	COV_ID,  
	LIMIT_DEDUC_TYPE,  
	LIMIT_DEDUC_AMOUNT,  
	IS_ACTIVE,  
	RANK,  
	LIMIT_DEDUC_AMOUNT1 , 
	LIMIT_DEDUC_AMOUNT_TEXT,  
	LIMIT_DEDUC_AMOUNT1_TEXT  ,
	EFFECTIVE_FROM_DATE ,
	EFFECTIVE_TO_DATE ,
	DISABLED_DATE 
)  
VALUES  
(  
	@LIMIT_DEDUC_ID,  
	@COV_ID,  
	@LIMIT_DEDUC_TYPE,  
	@LIMIT_DEDUC_AMOUNT,  
	@IS_ACTIVE,  
	@RANK,  
	@LIMIT_DEDUC_AMOUNT1  ,
	@LIMIT_DEDUC_AMOUNT_TEXT,  
	@LIMIT_DEDUC_AMOUNT1_TEXT ,
	@EFFECTIVE_FROM_DATE,
	@EFFECTIVE_TO_DATE ,
	@DISABLED_DATE 
)  
  
END  





GO

