IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_AVIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_AVIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                        
Created By:    Pravesh Chandel                        
Modify Date : 14 jAN 2010                        
Purpose     : Convert Aviation App to Policy
*/           
--drop PROC dbo.Proc_ConvertApplicationToPolicy_AVIATION
CREATE PROC dbo.Proc_ConvertApplicationToPolicy_AVIATION
@CUSTOMER_ID INT,                                                                
@APP_ID INT,                                                                                                        
@APP_VERSION_ID SMALLINT,                                                                                                        
@CREATED_BY INT,                                                                                                        
@PARAM1 INT = NULL,                                                                                                        
@PARAM2 INT = NULL,                                                                                                        
@PARAM3 INT = NULL,                                                                          
@CALLED_FROM NVARCHAR(30),                                                                        
@RESULT INT OUTPUT                                                                 
AS                                                                                                        
BEGIN                             
                      
BEGIN TRAN                                                                         
                      
DECLARE @TEMP_ERROR_CODE INT                                                                                                        
EXEC     @RESULT=Proc_ConvertApplicationToPolicy_ALL  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,@PARAM1,@PARAM2,@PARAM3,@CALLED_FROM,@RESULT                                                                                          
                      
IF (@RESULT = -1)  GOTO PROBLEM                      
                                                                                                      
DECLARE @TEMP_POLICY_ID INT                                                                                                         
DECLARE @TEMP_POLICY_VERSION_ID INT                      
SET @TEMP_POLICY_ID=@RESULT                      
SELECT   @TEMP_POLICY_VERSION_ID = POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                       
                                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     

-- 3. POL_VEHICLES                                                                                                  
                      
INSERT INTO POL_AVIATION_VEHICLES                                                                                                         
(                                                                                                        
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
VEHICLE_ID,
INSURED_VEH_NUMBER,
USE_VEHICLE,
COVG_PERIMETER,
REG_NUMBER,
SERIAL_NUMBER,
VEHICLE_YEAR,
MAKE,
MAKE_OTHER,
MODEL,
MODEL_OTHER,
CERTIFICATION,
REGISTER,
ENGINE_TYPE,
WING_TYPE,
CREW,
PAX,
REMARKS,
DEACTIVATE_REACTIVATE_DATE,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME 
)                 
SELECT                                                                                  
 @CUSTOMER_ID,   
 @TEMP_POLICY_ID,                                                                                                      
 @TEMP_POLICY_VERSION_ID,                                                                              
 VEHICLE_ID,                                                                                                   
 INSURED_VEH_NUMBER,                                                                                                     
 USE_VEHICLE,
COVG_PERIMETER,
REG_NUMBER,
SERIAL_NUMBER,
VEHICLE_YEAR,
MAKE,
MAKE_OTHER,
MODEL,
MODEL_OTHER,
CERTIFICATION,
REGISTER,
ENGINE_TYPE,
WING_TYPE,
CREW,
PAX,
REMARKS,
DEACTIVATE_REACTIVATE_DATE,
IS_ACTIVE,
@CREATED_BY,                                          
GETDATE(),
NULL,
NULL
 FROM APP_AVIATION_VEHICLES                                                                                                         
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                           
and Is_Active = 'Y'                                                                                                        
                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                        
-- 4. POL_AVIATION_VEHICLE_COVERAGES          
INSERT INTO POL_AVIATION_VEHICLE_COVERAGES                                             
(                                                                                                        
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
VEHICLE_ID,
COVERAGE_ID,
COVERAGE_CODE_ID,
LIMIT_OVERRIDE,
LIMIT_1,
LIMIT_1_TYPE,
LIMIT_2,
LIMIT_2_TYPE,
DEDUCT_OVERRIDE,
DEDUCTIBLE_1,
DEDUCTIBLE_1_TYPE,
DEDUCTIBLE_2,
DEDUCTIBLE_2_TYPE,
WRITTEN_PREMIUM,
FULL_TERM_PREMIUM,
IS_SYSTEM_COVERAGE,
LIMIT1_AMOUNT_TEXT,
LIMIT2_AMOUNT_TEXT,
DEDUCTIBLE1_AMOUNT_TEXT,
DEDUCTIBLE2_AMOUNT_TEXT,
SIGNATURE_OBTAINED,
LIMIT_ID,
DEDUC_ID,
ADD_INFORMATION,
COVERAGE_TYPE_ID,
RATE                                                 
)                              
                                                                                     
SELECT                                                                                                         
 @CUSTOMER_ID,                                                                                             
 @TEMP_POLICY_ID,                                                                               
 @TEMP_POLICY_VERSION_ID,                                    
 VEHICLE_ID,                                                         
 COVERAGE_ID,                                                                                                        
 COVERAGE_CODE_ID,                                                                
LIMIT_OVERRIDE,
LIMIT_1,
LIMIT_1_TYPE,
LIMIT_2,
LIMIT_2_TYPE,
DEDUCT_OVERRIDE,
DEDUCTIBLE_1,
DEDUCTIBLE_1_TYPE,
DEDUCTIBLE_2,
DEDUCTIBLE_2_TYPE,
WRITTEN_PREMIUM,
FULL_TERM_PREMIUM,
IS_SYSTEM_COVERAGE,
LIMIT1_AMOUNT_TEXT,
LIMIT2_AMOUNT_TEXT,
DEDUCTIBLE1_AMOUNT_TEXT,
DEDUCTIBLE2_AMOUNT_TEXT,
SIGNATURE_OBTAINED,
LIMIT_ID,
DEDUC_ID,
ADD_INFORMATION,
COVERAGE_TYPE_ID,
RATE
 FROM APP_AVIATION_VEHICLE_COVERAGES                                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                      
 and  VEHICLE_ID in 
	(Select VEHICLE_ID from POL_AVIATION_VEHICLES                                            
	 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID 
	AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y'
	)



                                                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   

 COMMIT TRAN                                               
 SET @RESULT = @TEMP_POLICY_ID                                                                                         
 RETURN @RESULT                                                                                  
                                                                                                        
 PROBLEM:                                                            
 IF (@TEMP_ERROR_CODE <> 0)                                                
  BEGIN                                                                                                        
     ROLLBACK TRAN                                                                                                        
   SET @RESULT = -1                                                                                     
   RETURN @RESULT                                                                                                        
  END                                                                                                        
                                                                                                         
 ELSE                                                                                                        
 BEGIN                                                                    
   SET @RESULT = @TEMP_POLICY_ID                                                                       
   RETURN @RESULT                                                                                                          
  END                          
                                                                                                       
END



GO

