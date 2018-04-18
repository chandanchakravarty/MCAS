IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveProductDefaultCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveProductDefaultCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_SaveProductDefaultCoverages      
Created by      : Pravesh K Chandel    
Date            : 10 May 2010      
Purpose         : Saves Defaults Coverages which are From Maintenance  
Revison History :                        
  
Used In  : EbixAdvantage                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/               
--drop proc Proc_SaveProductDefaultCoverages                    
CREATE  proc [dbo].[Proc_SaveProductDefaultCoverages]      
(                        
 @CUSTOMER_ID     int,                        
 @POLICY_ID     int,                        
 @POL_VERSION_ID     smallint,                        
 @RISK_ID smallint,   
 @CREATED_BY int=null    
)                        
as  
BEGIN   
  
BEGIN TRAN    
DECLARE @LOB_ID SMALLINT,  
 @SUB_LOB_ID SMALLINT,  
 @STATE_ID SMALLINT,  
 @APP_EFFECTIVE_DATETIME DATETIME ,@CREATED_DATETIME datetime
  
DECLARE @COVERAGE_CODE VarChar(10) ,@COV_ID int                     
DECLARE @TEMP_ERROR_CODE INT    
set @TEMP_ERROR_CODE=0  

SELECT @STATE_ID = STATE_ID, @LOB_ID = cast(POLICY_LOB as smallint),@SUB_LOB_ID= cast(POLICY_SUBLOB as smallint), @APP_EFFECTIVE_DATETIME= APP_EFFECTIVE_DATE  
  FROM POL_CUSTOMER_POLICY_LIST (NOLOCK) 
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID  
  
DECLARE DEFAULT_CR CURSOR  FOR  

 SELECT COV_CODE,COV_ID FROM MNT_COVERAGE (nolock) WHERE LOB_ID=@LOB_ID AND SUB_LOB_ID = @SUB_LOB_ID AND STATE_ID=@STATE_ID   
  --AND IS_MANDATORY=1 
  --AND IS_DEFAULT=1 
  AND
  (
	(IS_DEFAULT=1 AND DATEDIFF(DD,ISNULL(DEFAULT_DATE,'1950-01-01'),@APP_EFFECTIVE_DATETIME) >=0)
	OR 
	(IS_DEFAULT<>1 AND DATEDIFF(DD,ISNULL(NON_DEFAULT_DATE,'1950-01-01') ,@APP_EFFECTIVE_DATETIME) <=0) 
  )
  --AND isnull(IS_SYSTEM_GENERAED,'N')='Y'   
 AND EFFECTIVE_FROM_DATE <=@APP_EFFECTIVE_DATETIME AND @APP_EFFECTIVE_DATETIME<=isnull(EFFECTIVE_TO_DATE ,'3000-01-01')  
 AND @APP_EFFECTIVE_DATETIME<=isnull(DISABLED_DATE,'3000-01-01')  
  
set  @CREATED_DATETIME = GETDATE()
 OPEN DEFAULT_CR  
 FETCH NEXT FROM DEFAULT_CR INTO @COVERAGE_CODE ,@COV_ID 
  
 WHILE @@FETCH_STATUS = 0  
  BEGIN  
  EXEC Proc_SAVE_POLICY_PRODUCT_COVERAGES  
     @CUSTOMER_ID ,                        
      @POLICY_ID     ,                        
      @POL_VERSION_ID  ,                        
      @RISK_ID,   
     -1,                       
     @COV_ID, --@COVERAGE_CODE_ID ,
     null, --@RI_APPLIES                      
      NULL,     --@LIMIT_1   
      NULL, --@LIMIT_2   
     null , --@LIMIT1_AMOUNT_TEXT            
     null ,-- @LIMIT2_AMOUNT_TEXT 
     null ,--@DEDUCTIBLE1_AMOUNT_TEXT 
     null,-- @DEDUCTIBLE2_AMOUNT_TEXT,                         
     null,--@LIMIT_1_TYPE            
     null, --@LIMIT_2_TYPE,            
     null, --@@DEDUCTIBLE_1         
     null,--@DEDUCTIBLE_2            
      null,--@DEDUCTIBLE_1_TYPE
      null, --@DEDUCTIBLE_2_TYPE
      null,--@WRITTEN_PREMIUM
	null,--@FULL_TERM_PREMIUM
	null,--@LIMIT_ID
	null,--@DEDUC_ID
	null,--@ADD_INFORMATION
	null,--@MINIMUM_DEDUCTIBLE
	null,--@DEDUCTIBLE_REDUCES
	null,--@INITIAL_RATE
	null,--@FINAL_RATE
	null,--@AVERAGE_RATE
	@CREATED_BY,
	@CREATED_DATETIME,
	null,--@MODIFIED_BY
	null--@LAST_UPDATED_DATETIME
              
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
       
   FETCH NEXT FROM DEFAULT_CR INTO @COVERAGE_CODE  ,@COV_ID
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

