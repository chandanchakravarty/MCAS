IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCoverageRange]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCoverageRange]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_UpdateCoverageRange  
Created by           : Mohit Gupta  
Date                    : 5/07/2005  
Purpose               :   
Revison History :  
Used In                :   Wolverine    

Modified By	: Ravindra
Modified Date	: 05-08-2006
Purpose		: To Add  @EFFECTIVE_FROM_DATE  @EFFECTIVE_TO_DATE  @DISABLED_DATE 
			and remove other columns from Update Query

------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  

--drop proc Proc_UpdateCoverageRange
CREATE   PROCEDURE Proc_UpdateCoverageRange  
(  
 @LIMIT_DEDUC_ID int output,  
 @COV_ID int,  
 @LIMIT_DEDUC_AMOUNT decimal(18,2),  
 @LIMIT_DEDUC_TYPE varchar(10),  
 @IS_ACTIVE int,  
 @RANK int,  
 @LIMIT_DEDUC_AMOUNT1 decimal(18,2),
 @LIMIT_DEDUC_AMOUNT_TEXT NVarChar(100),  
 @LIMIT_DEDUC_AMOUNT1_TEXT NVarChar(100),
 @EFFECTIVE_FROM_DATE datetime,
 @EFFECTIVE_TO_DATE datetime,
 @DISABLED_DATE datetime  

)  
AS  
BEGIN  
UPDATE MNT_COVERAGE_RANGES  
SET   
	/*LIMIT_DEDUC_AMOUNT=@LIMIT_DEDUC_AMOUNT,  
	IS_ACTIVE=@IS_ACTIVE,  
	RANK=@RANK,  
	LIMIT_DEDUC_AMOUNT1=@LIMIT_DEDUC_AMOUNT1,
	LIMIT_DEDUC_AMOUNT_TEXT = @LIMIT_DEDUC_AMOUNT_TEXT,
	LIMIT_DEDUC_AMOUNT1_TEXT = @LIMIT_DEDUC_AMOUNT1_TEXT  */
	EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE,
	EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE,
	DISABLED_DATE 	    = @DISABLED_DATE
WHERE   
LIMIT_DEDUC_ID=@LIMIT_DEDUC_ID and 
	LIMIT_DEDUC_TYPE=@LIMIT_DEDUC_TYPE  
END  







GO

