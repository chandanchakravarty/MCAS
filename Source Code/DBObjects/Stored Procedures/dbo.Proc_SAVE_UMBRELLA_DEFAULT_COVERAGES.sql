IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- begin tran
-- drop proc Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES                  
-- go
/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES    
Created by      : Pravesh K Chandel  
Date            : 14 Jan 2008    
Purpose         : Saves Defaults Coverages which are From Maintenance
Revison History :                      

Used In  : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/             
--drop proc Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES                  
CREATE  proc dbo.Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES    
(                      
 @CUSTOMER_ID     int,                      
 @APP_ID     int,                      
 @APP_VERSION_ID     smallint,                      
 @VEHICLE_ID smallint, 
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
DECLARE @CREATED_DATETIME DATETIME  
SELECT @CREATED_DATETIME= GETDATE()
set @TEMP_ERROR_CODE=0
SELECT @STATE_ID = STATE_ID, @LOB_ID = APP_LOB, @APP_EFFECTIVE_DATETIME= APP_EFFECTIVE_DATE
	 FROM APP_LIST (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID
SELECT @STATE_ID , @LOB_ID ,@CREATED_DATETIME
DECLARE DEFAULT_CR CURSOR  FOR
	SELECT COV_CODE FROM MNT_COVERAGE (nolock) WHERE LOB_ID=@LOB_ID AND STATE_ID=@STATE_ID 
		AND IS_MANDATORY=1 AND IS_DEFAULT=1 AND isnull(IS_SYSTEM_GENERAED,'N')='Y' 
	AND EFFECTIVE_FROM_DATE <=@APP_EFFECTIVE_DATETIME AND @APP_EFFECTIVE_DATETIME<=isnull(EFFECTIVE_TO_DATE ,'3000-01-01')
	AND @APP_EFFECTIVE_DATETIME<=isnull(DISABLED_DATE,'3000-01-01')

	OPEN DEFAULT_CR
	FETCH NEXT FROM DEFAULT_CR INTO @COVERAGE_CODE

	WHILE @@FETCH_STATUS = 0
		BEGIN
		EXEC Proc_SAVE_UMBRELLA_COVERAGES
				 @CUSTOMER_ID ,
				 @APP_ID   ,
				 @APP_VERSION_ID   ,
				 -1,--@COVERAGE_ID ,
				 -1,--@COVERAGE_CODE_ID int=NULL,
				 @COVERAGE_CODE ,
				 @CREATED_BY , --  INT=NULL,
				 @CREATED_DATETIME ,--DATETIME=NULL,
				 NULL, --@MODIFIED_BY     INT=NULL,
				 NULL --@LAST_UPDATED_DATETIME DATETIME=NULL
						          
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

-- 
-- go
-- 
-- exec Proc_SAVE_UMBRELLA_DEFAULT_COVERAGES 1492,4,1,0,0
-- 
-- rollback tran

GO

