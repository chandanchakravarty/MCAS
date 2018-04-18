IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_HOME_COVERAGES_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_HOME_COVERAGES_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_SAVE_HOME_COVERAGES_FOR_POLICY    
    
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_SAVE_HOME_COVERAGES_FOR_POLICY          
Created by      : SHAFI          
Date            : 20/02/2006          
Purpose     :Inserts a record in POL_DWELLING_SECTION1_COVERAGES          
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE  PROC Dbo.Proc_SAVE_HOME_COVERAGES_FOR_POLICY          
(          
	@CUSTOMER_ID     int,          
	@POLICY_ID     int,          
	@POLICY_VERSION_ID     smallint,          
	@DWELLING_ID smallint,          
	@COVERAGE_ID int,          
	@COVERAGE_CODE_ID int,          
	@LIMIT_1 Decimal(18,2),          
	@LIMIT_2 Decimal(18,2),          
	@LIMIT_1_TYPE NVarChar(5),          
	@LIMIT_2_TYPE NVarChar(5),          
	@DEDUCTIBLE_1 DECIMAL(18,2),          
	@DEDUCTIBLE_2 DECIMAL(18,2),          
	@DEDUCTIBLE_1_TYPE NVarChar(5),          
	@DEDUCTIBLE_2_TYPE NVarChar(5),          
	@WRITTEN_PREMIUM DECIMAL(18,2),          
	@FULL_TERM_PREMIUM DECIMAL(18,2),          
	@COVERAGE_TYPE nchar(10),        
	@LIMIT1_AMOUNT_TEXT NVarChar(100) = NULL,          
	@LIMIT2_AMOUNT_TEXT NVarChar(100) = NULL,          
	@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100) = NULL,          
	@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100) = NULL,    
	@LIMIT_ID Int = NULL,    
	@DEDUC_ID Int = NULL,
	@DEDUCTIBLE_AMOUNT DECIMAL(18,0)= NULL,  
	@DEDUCTIBLE_TEXT NVarChar(100)  = NULL,
	@ADDDEDUCTIBLE_ID int =null                 
)          
AS          
          
DECLARE @COVERAGE_ID_MAX smallint         
DECLARE @DEDUCTIBLE Decimal(18,0)

BEGIN
	
	SELECT @DEDUCTIBLE = ALL_PERILL_DEDUCTIBLE_AMT      
	FROM POL_DWELLING_COVERAGE      
	WHERE CUSTOMER_ID = @CUSTOMER_ID and             
	POLICY_ID = @POLICY_ID and             
	POLICY_VERSION_ID = @POLICY_VERSION_ID             
	and DWELLING_ID = @DWELLING_ID           
      
	IF ( @COVERAGE_CODE_ID =  785 OR  @COVERAGE_CODE_ID = 805 OR       
		@COVERAGE_CODE_ID = 806 OR @COVERAGE_CODE_ID = 786 OR   @COVERAGE_CODE_ID =  784       
		OR @COVERAGE_CODE_ID = 804 OR @COVERAGE_CODE_ID = 787 OR @COVERAGE_CODE_ID = 807      
		OR @COVERAGE_CODE_ID = 799 OR @COVERAGE_CODE_ID = 779        
		OR @COVERAGE_CODE_ID = 800 OR @COVERAGE_CODE_ID = 780       
		OR @COVERAGE_CODE_ID = 782 OR @COVERAGE_CODE_ID = 802       
		OR @COVERAGE_CODE_ID = 803 OR @COVERAGE_CODE_ID = 783        
	)      
	BEGIN      
	IF ( @DEDUCTIBLE_1 IS NULL )      
		BEGIN      
			SET @DEDUCTIBLE = NULL      
		END      
	END      
      
	IF ( @COVERAGE_CODE_ID = 810 OR @COVERAGE_CODE_ID = 790       
	OR @COVERAGE_CODE_ID = 791 OR @COVERAGE_CODE_ID = 811 )      
	BEGIN      
		SET @DEDUCTIBLE = 500      
	END       
           
	IF ( @COVERAGE_ID = -1 )          
	BEGIN          
            
		select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from POL_DWELLING_SECTION_COVERAGES          
		where CUSTOMER_ID = @CUSTOMER_ID and           
		POLICY_ID=@POLICY_ID and           
		POLICY_VERSION_ID = @POLICY_VERSION_ID           
		and DWELLING_ID = @DWELLING_ID          
          
		IF NOT EXISTS          
		(          
			SELECT * FROM POL_DWELLING_SECTION_COVERAGES          
			where CUSTOMER_ID = @CUSTOMER_ID and           
			POLICY_ID=@POLICY_ID and           
			POLICY_VERSION_ID = @POLICY_VERSION_ID           
			and DWELLING_ID = @DWELLING_ID AND          
			COVERAGE_CODE_ID = @COVERAGE_CODE_ID         
			)          
		
		BEGIN          
			INSERT INTO POL_DWELLING_SECTION_COVERAGES          
			(          
			CUSTOMER_ID,          
			POLICY_ID,          
			POLICY_VERSION_ID,          
			DWELLING_ID,          
			COVERAGE_ID,          
			COVERAGE_CODE_ID,          
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
			COVERAGE_TYPE,          
			LIMIT1_AMOUNT_TEXT,          
			LIMIT2_AMOUNT_TEXT,          
			DEDUCTIBLE1_AMOUNT_TEXT,          
			DEDUCTIBLE2_AMOUNT_TEXT,    
			LIMIT_ID,    
			DEDUC_ID,
			DEDUCTIBLE ,  
			DEDUCTIBLE_TEXT,
			ADDDEDUCTIBLE_ID                                
			)          
			VALUES          
			(          
			@CUSTOMER_ID,          
			@POLICY_ID,          
			@POLICY_VERSION_ID,          
			@DWELLING_ID,          
			@COVERAGE_ID_MAX,          
			@COVERAGE_CODE_ID,          
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
			@COVERAGE_TYPE ,          
			@LIMIT1_AMOUNT_TEXT,          
			@LIMIT2_AMOUNT_TEXT,          
			@DEDUCTIBLE1_AMOUNT_TEXT,          
			@DEDUCTIBLE2_AMOUNT_TEXT,    
			@LIMIT_ID,    
			@DEDUC_ID ,
			@DEDUCTIBLE_AMOUNT ,  
			@DEDUCTIBLE_TEXT,
			@ADDDEDUCTIBLE_ID             
			
			)          
		END          
	 END           
           
 --Update          
	UPDATE POL_DWELLING_SECTION_COVERAGES          
	SET          
	CUSTOMER_ID = @CUSTOMER_ID,          
	POLICY_ID =  @POLICY_ID,          
	POLICY_VERSION_ID = @POLICY_VERSION_ID,          
	COVERAGE_CODE_ID = @COVERAGE_CODE_ID,          
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
	COVERAGE_TYPE=@COVERAGE_TYPE  ,          
	LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,          
	LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,          
	DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,          
	DEDUCTIBLE2_AMOUNT_TEXT  = @DEDUCTIBLE2_AMOUNT_TEXT ,    
	LIMIT_ID = @LIMIT_ID ,    
	DEDUC_ID = @DEDUC_ID ,
	DEDUCTIBLE = @DEDUCTIBLE_AMOUNT ,  
	DEDUCTIBLE_TEXT = @DEDUCTIBLE_TEXT ,
	ADDDEDUCTIBLE_ID = @ADDDEDUCTIBLE_ID            
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
	POLICY_ID = @POLICY_ID AND          
	POLICY_VERSION_ID = @POLICY_VERSION_ID AND          
	COVERAGE_CODE_ID = @COVERAGE_CODE_ID          
	AND DWELLING_ID = @DWELLING_ID 
	
	--Update linked endorsements  
	EXEC Proc_UPDATE_DWELLING_ENDORSEMENTSFORPOLICY            
	@CUSTOMER_ID,--@CUSTOMER_ID     int,                                
	@POLICY_ID,--@POL_ID     int,                                
	@POLICY_VERSION_ID, --@POL_VERSION_ID     smallint,                                
	@DWELLING_ID,--@DWELLING_ID smallint,                                
	-1,--@COVERAGE_ID int,                                
	@COVERAGE_CODE_ID--@COVERAGE_CODE_ID int           

	IF @@ERROR <> 0       
	BEGIN      
		RAISERROR      
		('Unable to update linked endorsments.',      
		16, 1)      
	END  
END   
        
      
    
  









GO

