IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY    
Created by      : Pravesh K Chandel  
Date            : 14 Jan 2008    
Purpose         : Saves Defaults Coverages which are From Maintenance
Revison History :                      

Used In  : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/             
--drop proc Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY                  
CREATE  proc dbo.Proc_SAVE_HOME_DEFAULT_COVERAGES_POLICY    
(                      
 @CUSTOMER_ID     int,                      
 @POLICY_ID     int,                      
 @POL_VERSION_ID     smallint,                      
 @DWELLING_ID smallint, 
 @CREATED_BY int=null  
)                      
as
BEGIN 

BEGIN TRAN  
DECLARE @LOB_ID SMALLINT,
	@STATE_ID SMALLINT,
	@APP_EFFECTIVE_DATETIME DATETIME

DECLARE @COVERAGE_CODE VarChar(10)                    
DECLARE @TEMP_ERROR_CODE INT  
set @TEMP_ERROR_CODE=0
SELECT @STATE_ID = STATE_ID, @LOB_ID = POLICY_LOB, @APP_EFFECTIVE_DATETIME= APP_EFFECTIVE_DATE
	 FROM POL_CUSTOMER_POLICY_LIST (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID

DECLARE DEFAULT_CR CURSOR  FOR
	SELECT COV_CODE FROM MNT_COVERAGE (nolock) WHERE LOB_ID=@LOB_ID AND STATE_ID=@STATE_ID 
		AND IS_MANDATORY=1 AND IS_DEFAULT=1 AND isnull(IS_SYSTEM_GENERAED,'N')='Y' 
	AND EFFECTIVE_FROM_DATE <=@APP_EFFECTIVE_DATETIME AND @APP_EFFECTIVE_DATETIME<=isnull(EFFECTIVE_TO_DATE ,'3000-01-01')
	AND @APP_EFFECTIVE_DATETIME<=isnull(DISABLED_DATE,'3000-01-01')


	OPEN DEFAULT_CR
	FETCH NEXT FROM DEFAULT_CR INTO @COVERAGE_CODE

	WHILE @@FETCH_STATUS = 0
		BEGIN
		EXEC Proc_SavePolicyHomeCoverage
				@CUSTOMER_ID  ,            
				@POLICY_ID    ,
				@POL_VERSION_ID  ,
				@DWELLING_ID,
				@COVERAGE_CODE ,
				NULL,--@LIMIT_1 Decimal(18,2),                        
				NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                        
				NULL,--@LIMIT_2 Decimal(18,2),                      
				'',--@LIMIT2_AMOUNT_TEXT NVarChar(100),                        
				NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                        
				'',--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                        
				NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                        
				'',--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),    
			    	NULL,--@DEDUCTIBLE_AMOUNT DECIMAL(18,0)= NULL,  
				'',--@DEDUCTIBLE_TEXT NVarChar(100)  = NULL , 
				NULL --@ADDDEDUCTIBLE_ID int =null         
		          
			SELECT @TEMP_ERROR_CODE = @@ERROR                                                                
			IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   
 		  
			FETCH NEXT FROM DEFAULT_CR INTO @COVERAGE_CODE
		END

	CLOSE DEFAULT_CR
	DEALLOCATE DEFAULT_CR
 COMMIT TRAN                
   RETURN 1

 PROBLEM:                                        
   ROLLBACK TRAN                                      
	   RETURN -1       

END




GO

