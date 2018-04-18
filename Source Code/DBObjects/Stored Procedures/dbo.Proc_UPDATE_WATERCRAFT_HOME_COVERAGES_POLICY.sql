IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                              
                        
Proc Name       : dbo.Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY                                              
Created by      : SHAFI                                              
Date            : 05/05/2006                                             
Purpose      :  Updates default coverages for watercrfat within home                                           
Revison History :                                              
Used In  : Wolverine                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------*/                                              
--drop proc  dbo.Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY                    
CREATE           PROC dbo.Proc_UPDATE_WATERCRAFT_HOME_COVERAGES_POLICY                    
(                                              
 @CUSTOMER_ID     int,                                              
 @POLICY_ID     int,                                              
 @POLICY_VERSION_ID     smallint,                                              
 @BOAT_ID smallint                    
)                           
                          
AS                           
                          
BEGIN                          
                           
	DECLARE @STATEID SmallInt                                                  
	DECLARE @LOBID NVarCHar(5)                               
	DECLARE @DWELLING_ID Int                       
	DECLARE @LIABILITY_ID Int                    
	DECLARE @MEDPM_ID Int                     
	DECLARE @LIAB_AMOUNT DECIMAL(18,0)                    
	DECLARE @MED_AMOUNT DECIMAL(18,0)                     
	DECLARE @LIAB_LIMIT_ID Int                      
	DECLARE @MED_LIMIT_ID Int                      
	DECLARE @TYPE Char(10)                  
	DECLARE @LEN NVarChar(10)                   
	DECLARE @LEN_INT Int    
	DECLARE @TYPEOFWATERCRAFT INT  
	DECLARE @UMBCS INT      
	DECLARE @UMBCS_LIMIT_ID INT  

	SELECT  @TYPE = TYPE,                  
	@LEN = ISNULL(LENGTH,'0'),    
	@TYPEOFWATERCRAFT=TYPE_OF_WATERCRAFT                   
	FROM POL_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES ON            
	POL_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                  
	POLICY_ID = @POLICY_ID AND                                                  
	POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
	BOAT_ID =  @BOAT_ID                        
                   
	SET @LEN_INT =  CAST(@LEN as Int)                  
                         
	SELECT @STATEID = STATE_ID,                                                  
	@LOBID = POLICY_LOB                                                  
	FROM POL_CUSTOMER_POLICY_LIST                                                  
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                  
	POLICY_ID = @POLICY_ID AND                                                  
	POLICY_VERSION_ID = @POLICY_VERSION_ID                             
                        
	IF (  @STATEID = 14 )                    
	BEGIN                    
		SET @LIABILITY_ID = 19                    
		SET @MEDPM_ID = 21  
		SET @UMBCS=24   
		SET @UMBCS_LIMIT_ID=1037                       
	END                    
                      
	IF (  @STATEID = 22 )                    
	BEGIN                    
		SET @LIABILITY_ID = 65                    
		SET @MEDPM_ID = 68   
		SET @UMBCS=70  
		SET @UMBCS_LIMIT_ID=1040                   
	END       
                  
	IF (  @STATEID = 49 )                    
	BEGIN                    
		SET @LIABILITY_ID = 820                    
		SET @MEDPM_ID = 821   
		SET @UMBCS=822  
		SET @UMBCS_LIMIT_ID=981                   
	END       
                
   /*                    
 10  PL Coverage E - Personal Liability Each Occurrence                    
 170  PL Coverage E - Personal Liability Each Occurrence                    
 171  MEDPM Coverage F - Medical Payment Each Person                    
 13  MEDPM Coverage F - Medical Payment Each Person                    
                    
   */                    
    
	IF ( @LOBID = 1 )                    
	BEGIN                
		--Get the first dwelling for tis LOB                    
		SELECT @DWELLING_ID = MIN(DWELLING_ID)             
		FROM POL_DWELLINGS_INFO                    
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
		POLICY_ID = @POLICY_ID AND                    
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
		IS_ACTIVE = 'Y'                    
	
		IF ( @DWELLING_ID IS NULL )                    
		BEGIN                    
			RETURN                    
		END                
                      
		  --Personal Liability limit                    
		SELECT @LIAB_AMOUNT = PERSONAL_LIAB_LIMIT,                    
		@MED_AMOUNT = MED_PAY_EACH_PERSON                    
		FROM POL_DWELLING_COVERAGE                    
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
		POLICY_ID = @POLICY_ID AND                    
		POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
		DWELLING_ID = @DWELLING_ID                     

		          
		SELECT @LIAB_LIMIT_ID = R.LIMIT_DEDUC_ID                    
		FROM MNT_COVERAGE_RANGES R                    
		WHERE LIMIT_DEDUC_AMOUNT = @LIAB_AMOUNT                    
		AND R.COV_ID = @LIABILITY_ID                   
		             
		SELECT @MED_LIMIT_ID = R.LIMIT_DEDUC_ID                    
		FROM MNT_COVERAGE_RANGES R                    
		WHERE LIMIT_DEDUC_AMOUNT = @MED_AMOUNT                    
		AND R.COV_ID = @MEDPM_ID                    
		             
	  --Save the coverages                    
		EXEC Proc_Save_PolicyWATERCRAFT_COVERAGES                    
		@CUSTOMER_ID,--@CUSTOMER_ID     int,                                  
		@POLICY_ID,--@POLICY_ID     int,                                  
		@POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                                  
		@BOAT_ID,--@VEHICLE_ID smallint,                                  
		-1,--@COVERAGE_ID int,                                  
		@LIABILITY_ID,--@COVERAGE_CODE_ID int,                                  
		@LIAB_AMOUNT,--@LIMIT_1 Decimal(18,2),                                        
		NULL,--@LIMIT_2 Decimal(18,2),                                      
		NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                        
		NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                        
		NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                        
		NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                        
		NULL,--@LIMIT_1_TYPE NVarChar(5),                                        
		NULL,--@LIMIT_2_TYPE NVarChar(5),                                        
		NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                        
		NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                    
		NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                        
		NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                        
		NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                        
		NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                      
		@LIAB_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                      
		NULL--@DEDUC_ID Int = NULL            


		if (NOT (@TYPE = 'JS') OR (@TYPEOFWATERCRAFT = 11373) OR (@TYPEOFWATERCRAFT = 11386))           
		BEGIN 
			IF NOT EXISTS              
			(             
			SELECT * FROM  POL_WATERCRAFT_COVERAGE_INFO              
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
			POLICY_ID = @POLICY_ID AND              
			POLICY_VERSION_ID = @POLICY_VERSION_ID AND              
			BOAT_ID = @BOAT_ID AND              
			COVERAGE_CODE_ID = @UMBCS              
			)                          
			BEGIN                                 
	
	     
				EXEC Proc_Save_PolicyWATERCRAFT_COVERAGES                                                
				@CUSTOMER_ID,--@CUSTOMER_ID     int,                                                          
				@POLICY_ID,--@POL_ID     int,                                                          
				@POLICY_VERSION_ID,-- @POL_VERSION_ID     smallint,                                                          
				@BOAT_ID,--@VEHICLE_ID smallint,                                                          
				-1,--@COVERAGE_ID int,                                                          
				@UMBCS,--@COVERAGE_CODE_ID int,                                                          
				50000,--@LIMIT_1 Decimal(18,2),                                                                
				NULL,--@LIMIT_2 Decimal(18,2),                                      
				NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                                              
				NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                                              
				NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                           
				NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                                                                
				NULL,--@LIMIT_1_TYPE NVarChar(5),                               
				NULL,--@LIMIT_2_TYPE NVarChar(5),                                                                
				NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                                                
				NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                                                                
				NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                                                
				NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                                                                
				NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                   
				NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2)      
				NULL,    
				@UMBCS_LIMIT_ID    
			END                              
			EXEC Proc_Save_PolicyWATERCRAFT_COVERAGES                    
			@CUSTOMER_ID,--@CUSTOMER_ID     int,                                  
			@POLICY_ID,--@POLICY_ID     int,                                  
			@POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                                       
			@BOAT_ID,--@VEHICLE_ID smallint,                                  
			-1,--@COVERAGE_ID int,                                  
			@MEDPM_ID,--@COVERAGE_CODE_ID int,                                  
			@MED_AMOUNT,--@LIMIT_1 Decimal(18,2),                                        
			25000,--@LIMIT_2 Decimal(18,2),                                      
			NULL,--@LIMIT1_AMOUNT_TEXT NVarChar(100),                                        
			NULL,--@LIMIT2_AMOUNT_TEXT NVarChar(100),                                        
			NULL,--@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                                        
			NULL,--@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                       
			NULL,--@LIMIT_1_TYPE NVarChar(5),                                        
			NULL,--@LIMIT_2_TYPE NVarChar(5),                                        
			NULL,--@DEDUCTIBLE_1 DECIMAL(18,2),                                        
			NULL,--@DEDUCTIBLE_2 DECIMAL(18,2),                  
			NULL,--@DEDUCTIBLE_1_TYPE NVarChar(5),                                        
			NULL,--@DEDUCTIBLE_2_TYPE NVarChar(5),                       
			NULL,--@WRITTEN_PREMIUM DECIMAL(18,2),                                        
			NULL,--@FULL_TERM_PREMIUM DECIMAL(18,2),                      
			@MED_LIMIT_ID,--@LIMIT_ID Int  = NULL ,                      
			NULL--@DEDUC_ID Int = NULL                              
	
		   END           
	  END                    
                              
END                                             
                        
                      
                    
                  
                
              
            
          
        
      
    
  






GO

