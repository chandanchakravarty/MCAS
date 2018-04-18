IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POLICY_PRODUCT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POLICY_PRODUCT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
-- drop proc Proc_SAVE_POLICY_PRODUCT_COVERAGES                
                
/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_SAVE_POLICY_PRODUCT_COVERAGES                                      
Created by      : Pravesh K Chandel                                    
Date            : 31 March 2010      
Purpose     :Inserts a record in Product Coverage Table      
Revison History :                                      
Used In  : Ebix Advantage      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
CREATE  PROC [dbo].[Proc_SAVE_POLICY_PRODUCT_COVERAGES]                                      
(                                      
 @CUSTOMER_ID     int,                                      
 @POLICY_ID     int,                                      
 @POLICY_VERSION_ID     smallint,                                      
 @RISK_ID smallint,                                      
 @COVERAGE_ID int,                                      
 @COVERAGE_CODE_ID int,      
 @RI_APPLIES char(1),                                      
 @LIMIT_1 Decimal(18,2),                                      
 @LIMIT_2 Decimal(18,2),                                    
 @LIMIT1_AMOUNT_TEXT NVarChar(100),                                      
 @LIMIT2_AMOUNT_TEXT NVarChar(100),                                      
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                      
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                      
 @LIMIT_1_TYPE NVarChar(5),                                      
 @LIMIT_2_TYPE NVarChar(5),                                      
 @DEDUCTIBLE_1 DECIMAL(18,2),                                      
 @DEDUCTIBLE_2 DECIMAL(18,2),                                      
 @DEDUCTIBLE_1_TYPE NVarChar(6),                                      
 @DEDUCTIBLE_2_TYPE NVarChar(6),                                      
 @WRITTEN_PREMIUM DECIMAL(18,2),                                      
 @FULL_TERM_PREMIUM DECIMAL(18,2) ,                    
 @LIMIT_ID Int = NULL,            
 @DEDUC_ID Int = NULL ,            
 @ADD_INFORMATION varchar(20) = null   ,      
        
  @MINIMUM_DEDUCTIBLE DECIMAL(18,2),         
  @DEDUCTIBLE_REDUCES char(1),      
  @INITIAL_RATE DECIMAL(8,4),      
  @FINAL_RATE DECIMAL(8,4),      
  @AVERAGE_RATE char(1),      
  @CREATED_BY int,      
  @CREATED_DATETIME datetime  ,      
  @MODIFIED_BY  int =null,      
  @LAST_UPDATED_DATETIME datetime=null,   
  @INDEMNITY_PERIOD int =null   ,
  @POL_STATUS NVARCHAR(10) = NULL   ,
  @TRANSACTION_TYPE INT = NULL,
  @IS_ACTIVE NCHAR(1)=NULL
)                                      
AS                                      
                                      
DECLARE @COVERAGE_ID_MAX smallint    ,
@CHANGE_INFORCE_PREM DECIMAL(25,2),
@POLICY_TERM INT,
@END_EFF_DAY INT,
@POL_EFF_DATE DATETIME,
@POL_EXP_DATE DATETIME,
@OPEN_POLICY INT =  14560    ,
@CURRENT_TERM INT	 ,
@COV_FULL_PREMIUM  DECIMAL(25,2)                       
BEGIN                                  
  --DECLARE @STATEID SmallInt                                          
  --DECLARE @LOBID NVarCHar(5)                       
  --SELECT @STATEID = STATE_ID,                                          
  --  @LOBID = POLICY_LOB                                          
  --FROM POL_CUSTOMER_POLICY_LIST  with(nolock)                                        
  --WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                          
  --  POLICY_ID = @POLICY_ID AND                                          
  --  POLICY_VERSION_ID = @POLICY_VERSION_ID      
  
  --Added By Lalit April 21,2011 for New implimentation
  --set premium parameters
 IF(ISNULL(@TRANSACTION_TYPE,0) <> @OPEN_POLICY) 
 BEGIN
  IF (CHARINDEX('END',ISNULL(@POL_STATUS,''),0)<>0) --for endorsement
   BEGIN   
	SELECT @POLICY_TERM =  APP_TERMS,@POL_EFF_DATE = ISNULL(POLICY_EFFECTIVE_DATE,APP_EFFECTIVE_DATE),
	@POL_EXP_DATE  = ISNULL(POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE)
	FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
   
   SELECT @END_EFF_DAY   = DATEDIFF(DAY,ISNULL(P.EFFECTIVE_DATETIME,@POL_EFF_DATE),ISNULL(P.[EXPIRY_DATE],@POL_EXP_DATE))
	FROM POL_POLICY_PROCESS P WITH(NOLOCK) WHERE  P.CUSTOMER_ID = @CUSTOMER_ID AND P.POLICY_ID = @POLICY_ID
	AND P.NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID
   
   
   SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST 
   WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID
   
   
   SELECT @COV_FULL_PREMIUM = FULL_TERM_PREMIUM from POL_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID in(
   SELECT MAX(POLICY_VERSION_ID) FROM POL_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CUSTOMER_ID =  @CUSTOMER_ID AND POLICY_ID= @POLICY_ID and POLICY_VERSION_ID in(
   SELECT POLICY_VERSION_ID from POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
   AND CURRENT_TERM = @CURRENT_TERM) and POLICY_VERSION_ID <> @POLICY_VERSION_ID and COVERAGE_CODE_ID = @COVERAGE_CODE_ID) and COVERAGE_CODE_ID = @COVERAGE_CODE_ID
     
   
   
   IF(ISNULL(@END_EFF_DAY,0)<>0 )
   SELECT  @CHANGE_INFORCE_PREM = @WRITTEN_PREMIUM
          ,@WRITTEN_PREMIUM = @WRITTEN_PREMIUM
	      ,@FULL_TERM_PREMIUM =ISNULL((@WRITTEN_PREMIUM * @POLICY_TERM / @END_EFF_DAY),0)+ISNULL(@COV_FULL_PREMIUM,0)
  
  --SELECT @CHANGE_INFORCE_PREM = @WRITTEN_PREMIUM--//@FULL_TERM_PREMIUM - @WRITTEN_PREMIUM
		
   END
   ELSE IF(@POL_STATUS IS NULL OR @POL_STATUS ='' ) --if aplication
	  BEGIN
		SELECT   @FULL_TERM_PREMIUM = @WRITTEN_PREMIUM,
		         @CHANGE_INFORCE_PREM = @WRITTEN_PREMIUM
		
	  END

END
	
                                      
  IF NOT EXISTS                                      
  (                                      
    SELECT COVERAGE_CODE_ID FROM POL_PRODUCT_COVERAGES with(nolock)                                     
    WHERE CUSTOMER_ID  = @CUSTOMER_ID       
  AND POLICY_ID  = @POLICY_ID       
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID       
  AND RISK_ID   = @RISK_ID       
  AND COVERAGE_CODE_ID = @COVERAGE_CODE_ID                                      
  )                                      
                                        
  BEGIN                           
                         
     SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                                   
   FROM POL_PRODUCT_COVERAGES with(nolock)       
   where CUSTOMER_ID = @CUSTOMER_ID and                                       
    POLICY_ID=@POLICY_ID and                                       
    POLICY_VERSION_ID = @POLICY_VERSION_ID                                 
    and RISK_ID = @RISK_ID 
    
                                
                                     
    INSERT INTO POL_PRODUCT_COVERAGES                                      
    (                                      
      CUSTOMER_ID,                                      
      POLICY_ID,                                      
      POLICY_VERSION_ID,                                      
      RISK_ID,                                      
      COVERAGE_ID,                                      
      COVERAGE_CODE_ID,     
      RI_APPLIES,                                     
      LIMIT_1_TYPE,                                      
      LIMIT_2_TYPE,                                      
      DEDUCTIBLE_1_TYPE,                                      
      DEDUCTIBLE_2_TYPE,                                      
      LIMIT_1,                                      
      LIMIT_2,                 
      DEDUCTIBLE_1,                                      
      DEDUCTIBLE_2,                                       
      WRITTEN_PREMIUM,                                      
      FULL_TERM_PREMIUM,                                    
      LIMIT1_AMOUNT_TEXT,                          
      LIMIT2_AMOUNT_TEXT,                                    
      DEDUCTIBLE1_AMOUNT_TEXT,                                    
      DEDUCTIBLE2_AMOUNT_TEXT ,                    
      LIMIT_ID,            
      DEDUC_ID,            
      ADD_INFORMATION  ,      
            
      MINIMUM_DEDUCTIBLE,         
      DEDUCTIBLE_REDUCES,      
   INITIAL_RATE,      
   FINAL_RATE,      
   AVERAGE_RATE,      
   CREATED_BY,      
   CREATED_DATETIME,  
   INDEMNITY_PERIOD ,
   CHANGE_INFORCE_PREM,
   IS_ACTIVE      
    )                                      
    VALUES                                      
    (                                      
      @CUSTOMER_ID,                                      
      @POLICY_ID,                                      
      @POLICY_VERSION_ID,                                      
      @RISK_ID,                                      
      @COVERAGE_ID_MAX,                                      
      @COVERAGE_CODE_ID,     
      @RI_APPLIES,                                     
      @LIMIT_1_TYPE,                                      
      @LIMIT_2_TYPE,                                      
      @DEDUCTIBLE_1_TYPE,                                  
      @DEDUCTIBLE_2_TYPE,                                      
      @LIMIT_1,                                      
      @LIMIT_2,                                      
      @DEDUCTIBLE_1,                                      
      @DEDUCTIBLE_2,                                       
      @WRITTEN_PREMIUM,                                      
     @FULL_TERM_PREMIUM,                                    
      @LIMIT1_AMOUNT_TEXT,                                    
      @LIMIT2_AMOUNT_TEXT,                                    
      @DEDUCTIBLE1_AMOUNT_TEXT,                                    
      @DEDUCTIBLE2_AMOUNT_TEXT,                    
      @LIMIT_ID,            
      @DEDUC_ID,            
   @ADD_INFORMATION  ,      
   @MINIMUM_DEDUCTIBLE,      
   @DEDUCTIBLE_REDUCES,      
   @INITIAL_RATE,      
   @FINAL_RATE,      
   @AVERAGE_RATE,      
   @CREATED_BY,      
   @CREATED_DATETIME,  
   @INDEMNITY_PERIOD ,
   @CHANGE_INFORCE_PREM,
   @IS_ACTIVE                       
    )                                  
  END                       
ELSE --End of Insert                                     
 BEGIN                  
  --Update                                      
  UPDATE POL_PRODUCT_COVERAGES                                      
  SET                                          
   LIMIT_1_TYPE = @LIMIT_1_TYPE,                                      
   LIMIT_2_TYPE = @LIMIT_2_TYPE,                                      
   LIMIT_1 = @LIMIT_1,                                      
   LIMIT_2 = @LIMIT_2,                                      
   DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,                                      
   DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,                                      
   DEDUCTIBLE_1 = @DEDUCTIBLE_1,                                      
   DEDUCTIBLE_2 = @DEDUCTIBLE_2,                                      
   WRITTEN_PREMIUM = @WRITTEN_PREMIUM,                
   FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM,                                    
   LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                                    
   LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,                                    
   DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,               
   DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT,                      
   LIMIT_ID = @LIMIT_ID,            
   DEDUC_ID  =  @DEDUC_ID,            
   ADD_INFORMATION= @ADD_INFORMATION   ,      
   RI_APPLIES=@RI_APPLIES     ,    
  MINIMUM_DEDUCTIBLE=@MINIMUM_DEDUCTIBLE,      
  DEDUCTIBLE_REDUCES =@DEDUCTIBLE_REDUCES,      
  INITIAL_RATE =@INITIAL_RATE,      
  FINAL_RATE =@FINAL_RATE,      
  AVERAGE_RATE =@AVERAGE_RATE,      
  MODIFIED_BY =@MODIFIED_BY,      
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,  
  INDEMNITY_PERIOD=@INDEMNITY_PERIOD   ,
  CHANGE_INFORCE_PREM  = @CHANGE_INFORCE_PREM,    
   IS_ACTIVE=@IS_ACTIVE                                
                     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                         
   POLICY_ID = @POLICY_ID AND                                      
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND                                      
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND                        
   RISK_ID = @RISK_ID      
                     
 END                  
                
IF @@ERROR <> 0                
BEGIN                
 RAISERROR ('Unable to add linked endorsments.', 16, 1)                
                
END                     
--************************************************************                
END                   
                   
GO

