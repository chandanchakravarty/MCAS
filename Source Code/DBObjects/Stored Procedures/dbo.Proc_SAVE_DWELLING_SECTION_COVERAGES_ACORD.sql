IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_SAVE_HOME_COVERAGES_ACORD                      
Created by      : Pradeep Iyer                      
Date            : 11/15/2005                      
Purpose      :Inserts a record in APP_DWELLING_SECTION_COVERAGES                      
Revison History :                      
Used In  : Wolverine        
  
  
Modified by  : shafi     
Date         : 27 April. 2006    
Purpose      : To Avert the override of values IN Make app in case of H0-4,ho-4 delux  
               In case the value is null As it Is Mandatory  
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/     
--drop PROC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD    
--go                   
CREATE           PROC dbo.Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD                      
(                      
 @CUSTOMER_ID     int,                      
 @APP_ID     int,                      
 @APP_VERSION_ID     smallint,                      
 @DWELLING_ID smallint,                      
 @COVERAGE_ID int,                      
 @COVERAGE_CODE_ID int,                      
 @LIMIT_1 Decimal(18,0),                      
 @LIMIT_2 Decimal(18,0),       
 @DEDUCTIBLE_1 DECIMAL(18,0),                      
 @DEDUCTIBLE_2 DECIMAL(18,0),                 
 @COVERAGE_TYPE nchar(10),                    
 @COVERAGE_CODE VarChar(20),      
 @LIMIT1_AMOUNT_TEXT nvarchar(100) = NULL,      
 @LIMIT2_AMOUNT_TEXT nvarchar(100) = NULL,        
 @DEDUCTIBLE1_AMOUNT_TEXT nvarchar(100) = NULL,      
 @DEDUCTIBLE2_AMOUNT_TEXT nvarchar(100) = NULL,
 @DEDUCTIBLE_AMOUNT DECIMAL(18,0)= NULL,  
 @DEDUCTIBLE_TEXT NVarChar(100)  = NULL  

                      
)                      
AS                      
BEGIN                      

	DECLARE @COVERAGE_ID_MAX smallint                      
	DECLARE @ISLIMITAPPLICABLE NChar(1)        
	DECLARE @ISDEDUCTAPPLICABLE NChar(1)        
	DECLARE @LIMIT_TYPE NChar(1)        
	DECLARE @DEDUCTIBLE_TYPE NChar(1)        
	DECLARE @LIMIT_ID Int        
	DECLARE @DEDUCT_ID Int        
	DECLARE @STATE_ID Int  
	DECLARE @ADDDEDUCTIBLE_ID int
	DECLARE @ISADDDEDUCTIBLE_APP NChar(1)
	
	-- Get  the Coverage ID                        
	EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COVERAGE_CODE    
	DECLARE @POLICYTYPE INT     
	SELECT @POLICYTYPE=POLICY_TYPE FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  

	DECLARE @FLAG INT
	IF EXISTS                     
	(                    
	SELECT * FROM APP_DWELLING_SECTION_COVERAGES                    
	WHERE CUSTOMER_ID = @CUSTOMER_ID and                       
	APP_ID=@APP_ID and                       
	APP_VERSION_ID = @APP_VERSION_ID                       
	and DWELLING_ID = @DWELLING_ID  AND     
	COVERAGE_CODE_ID = @COVERAGE_CODE_ID --AND                  
	--COVERAGE_TYPE = @COVERAGE_TYPE                    
	)  
	BEGIN 
		SET @FLAG=1
		SELECT @DEDUCTIBLE_1= CASE WHEN  @DEDUCTIBLE_1 = -1 
				THEN DEDUCTIBLE_1
				ELSE @DEDUCTIBLE_1   END
		FROM APP_DWELLING_SECTION_COVERAGES
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
		APP_ID = @APP_ID AND                      
		APP_VERSION_ID = @APP_VERSION_ID AND             
		DWELLING_ID = @DWELLING_ID AND                     
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID     
		
	END
	ELSE
	BEGIN
		SET @FLAG=2
		SET @DEDUCTIBLE_1=CASE WHEN  @DEDUCTIBLE_1 = -1 THEN NULL
				WHEN   @DEDUCTIBLE_1 < -1 THEN (ISNULL(@DEDUCTIBLE_1,0) * -1)
				ELSE @DEDUCTIBLE_1 END
 
	END
  
                
                     
	IF ( @COVERAGE_CODE_ID = 0 )                  
	BEGIN                  
	 --No such coverage found                  
		RETURN 1                  
	END                 
     
       
--End of ranges ID-----------------------------------------    
     
    
    

 --Get Range ID-----------------------------------------------------------------        
	SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,        
	@ISDEDUCTAPPLICABLE = IsDeductApplicable,        
	@LIMIT_TYPE = LIMIT_TYPE,        
	@DEDUCTIBLE_TYPE = DEDUCTIBLE_TYPE   ,     
	@ISADDDEDUCTIBLE_APP   = ISADDDEDUCTIBLE_APP 
	FROM MNT_COVERAGE        
	WHERE COV_ID = @COVERAGE_CODE_ID        

	IF(@ISADDDEDUCTIBLE_APP ='1')
	BEGIN 
		SELECT @ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID        
		FROM MNT_COVERAGE_RANGES        
		WHERE COV_ID = @COVERAGE_CODE_ID AND        
		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_AMOUNT,0) AND        
		ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,0) = ISNULL(@DEDUCTIBLE_TEXT,0)    

	END 
        
	IF  ( @ISLIMITAPPLICABLE = '1' )        
	BEGIN        
		
		SELECT @LIMIT_ID = LIMIT_DEDUC_ID        
		FROM MNT_COVERAGE_RANGES        
		WHERE COV_ID = @COVERAGE_CODE_ID AND        
		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@LIMIT_1,0) AND        
		ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@LIMIT_2,0)    
		
	END        
        
	IF  ( @ISDEDUCTAPPLICABLE = '1' )        
	BEGIN        
	--Flat        
	
	SELECT @DEDUCT_ID = LIMIT_DEDUC_ID        
	FROM MNT_COVERAGE_RANGES        
	WHERE COV_ID = @COVERAGE_CODE_ID AND        
	ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND        
	ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0)     
	
	END 
                
	IF @FLAG=1                 
	BEGIN   
    
		IF @POLICYTYPE = 11195 or @POLICYTYPE = 11405 OR @POLICYTYPE=11245 OR @POLICYTYPE=11407 AND @COVERAGE_CODE='EBUSPP'         
		BEGIN  
			IF ISnull(@LIMIT_1,0) <> 0  
			--Update                      
			BEGIN  
				UPDATE APP_DWELLING_SECTION_COVERAGES                      
				SET                          
				LIMIT_1 = @LIMIT_1,                      
				LIMIT_2 = @LIMIT_2,       
				LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT ,      
				LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT ,      
				DEDUCTIBLE_1 = @DEDUCTIBLE_1,                      
				DEDUCTIBLE_2 = @DEDUCTIBLE_2,      
				DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT ,      
				DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT ,    
				LIMIT_ID = @LIMIT_ID,    
				DEDUC_ID =  @DEDUCT_ID      
				WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
				APP_ID = @APP_ID AND                      
				APP_VERSION_ID = @APP_VERSION_ID AND             
				DWELLING_ID = @DWELLING_ID AND                     
				COVERAGE_CODE_ID = @COVERAGE_CODE_ID     
			END  
		END  
		ELSE  
		BEGIN  
			UPDATE APP_DWELLING_SECTION_COVERAGES                      
			SET                          
			LIMIT_1 = @LIMIT_1,                      
			LIMIT_2 = @LIMIT_2,       
			
			LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT ,      
			LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT ,      
			DEDUCTIBLE_1 =  @DEDUCTIBLE_1 ,         
			DEDUCTIBLE_2 = @DEDUCTIBLE_2,      
			DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT ,      
			DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT ,    
			LIMIT_ID = @LIMIT_ID,    
			DEDUC_ID =  @DEDUCT_ID      
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
			APP_ID = @APP_ID AND                      
			APP_VERSION_ID = @APP_VERSION_ID AND             
			DWELLING_ID = @DWELLING_ID AND                     
			COVERAGE_CODE_ID = @COVERAGE_CODE_ID                      
		END  
	 END                    
	 ELSE                    
	 BEGIN                    
		select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                     
		from APP_DWELLING_SECTION_COVERAGES                      
		where CUSTOMER_ID = @CUSTOMER_ID and                       
		APP_ID=@APP_ID and                       
		APP_VERSION_ID = @APP_VERSION_ID                       
		and DWELLING_ID = @DWELLING_ID                      
                     
		INSERT INTO APP_DWELLING_SECTION_COVERAGES                      
		(                      
		CUSTOMER_ID,                      
		APP_ID,                      
		APP_VERSION_ID,                      
		DWELLING_ID,                      
		COVERAGE_ID,                      
		COVERAGE_CODE_ID,                          
		LIMIT_1,                      
		LIMIT_2,  
		DEDUCTIBLE_1,       
		DEDUCTIBLE_2,       
		LIMIT1_AMOUNT_TEXT,      
		LIMIT2_AMOUNT_TEXT,              
		DEDUCTIBLE1_AMOUNT_TEXT,      
		DEDUCTIBLE2_AMOUNT_TEXT,       
		COVERAGE_TYPE,    
		LIMIT_ID,    
		DEDUC_ID,
		DEDUCTIBLE,
		DEDUCTIBLE_TEXT,
		ADDDEDUCTIBLE_ID
		)                      
		VALUES                      
		(                   
		@CUSTOMER_ID,                      
		@APP_ID,                      
		@APP_VERSION_ID,                      
		@DWELLING_ID,                      
		@COVERAGE_ID_MAX,                      
		@COVERAGE_CODE_ID,                          
		@LIMIT_1,                      
		@LIMIT_2,
		@DEDUCTIBLE_1,           
		@DEDUCTIBLE_2,        
		@LIMIT1_AMOUNT_TEXT,      
		@LIMIT2_AMOUNT_TEXT,             
		@DEDUCTIBLE1_AMOUNT_TEXT,      
		@DEDUCTIBLE2_AMOUNT_TEXT,      
		@COVERAGE_TYPE,    
		@LIMIT_ID,    
		@DEDUCT_ID,
		@DEDUCTIBLE_AMOUNT,
		@DEDUCTIBLE_TEXT,
		@ADDDEDUCTIBLE_ID    
		)             
          
		--Insert Linked Endorsements here------------------------------          
		EXEC Proc_UPDATE_LINKED_HOME_ENDORSEMENTS          
		@CUSTOMER_ID, --@CUSTOMER_ID     int,                                
		@APP_ID, --@APP_ID     int,                                
		@APP_VERSION_ID, --@APP_VERSION_ID   smallint,                                
		@DWELLING_ID, --@DWELLING_ID smallint,                                
		0, --@COVERAGE_ID int,                                
		@COVERAGE_CODE_ID --@COVERAGE_CODE_ID int          
		-----------------------------------------------------------------          
                    
	END                    
	RETURN 1                    
                     
END                    
                    
                  
                
              
            
          
        
      
    
    
    
  
  
  
  







GO

