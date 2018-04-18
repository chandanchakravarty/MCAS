IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SavePolicyHomeCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SavePolicyHomeCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_SavePolicyHomeCoverage
Created by      : Ravindra
Date            : 06-23-2006
Purpose         : Saves records in Watercraft Coverages( Policy Level)
Revison History :                  
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--- drop proc Proc_SavePolicyHomeCoverage
CREATE   proc Proc_SavePolicyHomeCoverage
(                  
	@CUSTOMER_ID     int,                  
	@POLICY_ID     int,                  
	@POLICY_VERSION_ID     smallint,                  
	@DWELLING_ID smallint,                  
	@COVERAGE_CODE VarChar(10),                 
	@LIMIT_1 Decimal(18,2),                        
	@LIMIT1_AMOUNT_TEXT NVarChar(100),                        
	@LIMIT_2 Decimal(18,2),                      
	@LIMIT2_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE_1 DECIMAL(18,2),                        
	@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE_2 DECIMAL(18,2),                        
	@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),    
    	@DEDUCTIBLE_AMOUNT DECIMAL(18,0)= NULL,  
	@DEDUCTIBLE_TEXT NVarChar(100)  = NULL , 
	@ADDDEDUCTIBLE_ID int =null                     
)                  
AS                  
                  
DECLARE @COVERAGE_ID_MAX smallint 
DECLARE @LIMIT_1_TYPE NVarChar(10)                        
DECLARE @LIMIT_2_TYPE NVarChar(10)   
DECLARE @DEDUCTIBLE_1_TYPE NVarChar(10)                        
DECLARE @DEDUCTIBLE_2_TYPE NVarChar(10)   
DECLARE @COVERAGE_ID int      
DECLARE	@COVERAGE_CODE_ID int  
DECLARE @ISADDDEDUCTIBLE_APP NChar(1)  
DECLARE @DEDUCTIBLE Decimal(18,0)    
      
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )
	FROM MNT_COVERAGE MNT INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON      
  		MNT.LOB_ID = POL.POLICY_LOB AND 
  		MNT.STATE_ID=POL.STATE_ID 
	WHERE   POL.CUSTOMER_ID = @CUSTOMER_ID AND      
		POL.POLICY_ID   = @POLICY_ID AND      
		POL.POLICY_VERSION_ID = @POLICY_VERSION_ID and     
		MNT.COV_CODE = @COVERAGE_CODE AND       
		MNT.IS_ACTIVE = 'Y'      

      
	IF (  @COVERAGE_CODE_ID = 0 )      
	BEGIN      
		RETURN      
	END


	
	declare @ISLIMITAPPLICABLE int, @ISDEDUCTAPPLICABLE INT, @LIMIT_ID INT, @DEDUCT_ID INT
	DECLARE @LIMIT_TYPE INT, @DEDUCTIBLE_TYPE INT

    --start
    IF EXISTS                     
	(                    
		SELECT * FROM POL_DWELLING_SECTION_COVERAGES                    
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                       
		POLICY_ID=@POLICY_ID and                       
		POLICY_VERSION_ID = @POLICY_VERSION_ID                       
		and DWELLING_ID = @DWELLING_ID  AND     
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID --AND                  
	--COVERAGE_TYPE = @COVERAGE_TYPE                    
	)  
	BEGIN 
		SELECT @DEDUCTIBLE_1= CASE WHEN  @DEDUCTIBLE_1 = -1 
			THEN DEDUCTIBLE_1
			ELSE @DEDUCTIBLE_1   END
		FROM POL_DWELLING_SECTION_COVERAGES
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
		POLICY_ID = @POLICY_ID AND                      
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND             
		DWELLING_ID = @DWELLING_ID AND                     
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID     
	END
	ELSE
	BEGIN
		
		SET @DEDUCTIBLE_1=CASE WHEN  @DEDUCTIBLE_1 = -1 THEN NULL
				WHEN   @DEDUCTIBLE_1 < -1 THEN (ISNULL(@DEDUCTIBLE_1,0) * -1)
				ELSE @DEDUCTIBLE_1 END
 
	END
	         
	     
	
	--Get Range ID-----------------------------------------------------------------        
	SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,        
	@ISDEDUCTAPPLICABLE = IsDeductApplicable,        
	@LIMIT_1_TYPE = LIMIT_TYPE,        
	@DEDUCTIBLE_1_TYPE = DEDUCTIBLE_TYPE   ,     
	@ISADDDEDUCTIBLE_APP   = ISADDDEDUCTIBLE_APP 
	FROM MNT_COVERAGE        
	WHERE COV_ID = @COVERAGE_CODE_ID        
	
	IF(@ISADDDEDUCTIBLE_APP ='1')
	BEGIN 
		SELECT @ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID        
		FROM MNT_COVERAGE_RANGES        
		WHERE COV_ID = @COVERAGE_CODE_ID AND        
		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_AMOUNT,0) AND        
		ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,0) = ISNULL(@DEDUCTIBLE_TEXT,0) and
		LIMIT_DEDUC_TYPE='Addded'    
		
	END 

	SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,  
	@ISDEDUCTAPPLICABLE = IsDeductApplicable,  
	@LIMIT_TYPE = LIMIT_TYPE,  
	@DEDUCTIBLE_TYPE = DEDUCTIBLE_TYPE  
	FROM MNT_COVERAGE  
	WHERE COV_ID = @COVERAGE_CODE_ID  
	
	IF  ( @ISLIMITAPPLICABLE = '1' )  
	BEGIN  
		SELECT @LIMIT_ID = LIMIT_DEDUC_ID  
  		FROM MNT_COVERAGE_RANGES  
  		WHERE COV_ID = @COVERAGE_CODE_ID AND  
   		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@LIMIT_1,0) AND  
   		ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@LIMIT_2,0) AND  
   		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT1_AMOUNT_TEXT)),'') AND  
   		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT2_AMOUNT_TEXT)),'')  
 
	END  
  
	IF  ( @ISDEDUCTAPPLICABLE = '1' )  
	BEGIN  
 		
		SELECT @DEDUCT_ID = LIMIT_DEDUC_ID  
		FROM MNT_COVERAGE_RANGES  
		WHERE COV_ID = @COVERAGE_CODE_ID AND  
		ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND  
		ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND  
		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND  
		ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')  
 		
	END
      
                   
	IF NOT EXISTS          
	(          
		SELECT COVERAGE_CODE_ID FROM POL_DWELLING_SECTION_COVERAGES
		WHERE   CUSTOMER_ID = @CUSTOMER_ID and                   
			POLICY_ID=@POLICY_ID and                   
			POLICY_VERSION_ID = @POLICY_VERSION_ID                   
			and DWELLING_ID = @DWELLING_ID AND          
			COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)                  
	BEGIN                  
                    
		SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                   
		FROM POL_DWELLING_SECTION_COVERAGES                  
		where CUSTOMER_ID = @CUSTOMER_ID and                   
		POLICY_ID=@POLICY_ID and                   
		POLICY_VERSION_ID = @POLICY_VERSION_ID                   
		and DWELLING_ID = @DWELLING_ID                  
                  
		INSERT INTO POL_DWELLING_SECTION_COVERAGES                  
		(                  
			CUSTOMER_ID, 			
			POLICY_ID,                  
			POLICY_VERSION_ID,                  
			DWELLING_ID,                  
			COVERAGE_ID,                  
			COVERAGE_CODE_ID,                  
			LIMIT_1,                        
			LIMIT_2,                        
			DEDUCTIBLE_1,                        
			DEDUCTIBLE_2 ,    
			LIMIT1_AMOUNT_TEXT ,    
			LIMIT2_AMOUNT_TEXT ,    
			DEDUCTIBLE1_AMOUNT_TEXT ,    
			DEDUCTIBLE2_AMOUNT_TEXT ,
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
			@LIMIT_1,      
			@LIMIT_2,            
			@DEDUCTIBLE_1,              
			@DEDUCTIBLE_2,    
			@LIMIT1_AMOUNT_TEXT ,    
			@LIMIT2_AMOUNT_TEXT ,    
			@DEDUCTIBLE1_AMOUNT_TEXT ,    
			@DEDUCTIBLE2_AMOUNT_TEXT,
			@LIMIT_ID,      
			@DEDUCT_ID, 
           		@DEDUCTIBLE_AMOUNT ,  
			@DEDUCTIBLE_TEXT,
			@ADDDEDUCTIBLE_ID                                         
			   
		)                
	END                   
    ELSE
	BEGIN               
		--Update                  
		UPDATE POL_DWELLING_SECTION_COVERAGES                  
		SET                  
		LIMIT_1 = @LIMIT_1,                        
		LIMIT_2 = @LIMIT_2,                        
		DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,                        
		DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,                    
		DEDUCTIBLE_1 = @DEDUCTIBLE_1,                        
		DEDUCTIBLE_2 = @DEDUCTIBLE_2,                          
		LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                      
		LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT,                      
		DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT,                      
		DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT,
		LIMIT_ID = @LIMIT_ID,      
		DEDUC_ID = @DEDUCT_ID,
        	DEDUCTIBLE = @DEDUCTIBLE_AMOUNT ,  
		DEDUCTIBLE_TEXT = @DEDUCTIBLE_TEXT ,
		ADDDEDUCTIBLE_ID =@ADDDEDUCTIBLE_ID                              
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
		POLICY_ID = @POLICY_ID AND                  
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND        
		DWELLING_ID = @DWELLING_ID                
	END
                    
	IF @@ERROR <> 0      
	BEGIN      
 		RAISERROR ('Unable to save Home/Rental  Coverage.', 16, 1)      
      
	END           
	--************************************************************   

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
  
 	RETURN @COVERAGE_CODE_ID            
              
END









GO

