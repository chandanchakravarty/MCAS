IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_WATERCRAFT_COVERAGES_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_WATERCRAFT_COVERAGES_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_Save_WATERCRAFT_COVERAGES_ACORD
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
CREATE PROC dbo.Proc_Save_WATERCRAFT_COVERAGES_ACORD                  
(                  
	@CUSTOMER_ID     int,                  
	@APP_ID     int,                  
	@APP_VERSION_ID     smallint,                  
	@VEHICLE_ID smallint,                  
	@COVERAGE_ID int = 0,                  
	@COVERAGE_CODE_ID int,       
	@COVERAGE_CODE VarChar(10),                 
	@LIMIT_1 Decimal(18,2),                        
	@LIMIT_2 Decimal(18,2),                      
	@LIMIT1_AMOUNT_TEXT NVarChar(100),                        
	@LIMIT2_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                        
	@DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                        
	@LIMIT_1_TYPE NVarChar(10),                        
	@LIMIT_2_TYPE NVarChar(10),   
	@DEDUCTIBLE_1 DECIMAL(18,2),                        
	@DEDUCTIBLE_2 DECIMAL(18,2),                        
	@DEDUCTIBLE_1_TYPE NVarChar(10),                        
	@DEDUCTIBLE_2_TYPE NVarChar(10)   
)                  
AS                  
                  
DECLARE @COVERAGE_ID_MAX smallint 
      
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )
	FROM MNT_COVERAGE MNT INNER JOIN APP_LIST APP ON      
  		MNT.LOB_ID = 4 AND	--For watercraft only
  		MNT.STATE_ID=APP.STATE_ID 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
		APP.APP_ID = @APP_ID AND      
		APP.APP_VERSION_ID = @APP_VERSION_ID and     
		MNT.COV_CODE = @COVERAGE_CODE AND       
		MNT.IS_ACTIVE = 'Y'      
      

      
	IF (  @COVERAGE_CODE_ID = 0 )      
	BEGIN      
		RETURN      
	END


	
	declare @ISLIMITAPPLICABLE int, @ISDEDUCTAPPLICABLE INT, @LIMIT_ID INT, @DEDUCT_ID INT
	DECLARE @LIMIT_TYPE INT, @DEDUCTIBLE_TYPE INT
	
	--Get Range ID-----------------------------------------------------------------  
	SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,  
	@ISDEDUCTAPPLICABLE = IsDeductApplicable,  
	@LIMIT_TYPE = LIMIT_TYPE,  
	@DEDUCTIBLE_TYPE = DEDUCTIBLE_TYPE  
	FROM MNT_COVERAGE  
	WHERE COV_ID = @COVERAGE_CODE_ID  
	
	IF  ( @ISLIMITAPPLICABLE = '1' )  
	BEGIN  
	 --Flat  
	 --IF ( @LIMIT_TYPE = 1 OR @LIMIT_TYPE = 2)  
	 --BEGIN  
	

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
		IF ( @DEDUCTIBLE_TYPE = 1 OR @DEDUCTIBLE_TYPE = 2)  
  		BEGIN  
		--by Praveen
		DECLARE @BOAT_TYPE VARCHAR(22)
		SELECT @BOAT_TYPE=LKP.TYPE FROM APP_WATERCRAFT_INFO WAT_INFO WITH(NOLOCK)    
		 INNER JOIN MNT_LOOKUP_VALUES LKP WITH(NOLOCK) ON                
		   WAT_INFO.TYPE_OF_WATERCRAFT = LKP.LOOKUP_UNIQUE_ID        
		 AND  WAT_INFO.CUSTOMER_ID=@CUSTOMER_ID    
		 AND WAT_INFO.APP_ID=@APP_ID    
		 AND WAT_INFO.APP_VERSION_ID = @APP_VERSION_ID    
		 AND WAT_INFO.BOAT_ID = @VEHICLE_ID 
		     
		IF (@BOAT_TYPE='JS' AND @COVERAGE_CODE='BDEDUC' )    
		  SELECT @DEDUCT_ID = LIMIT_DEDUC_ID        
		   FROM MNT_COVERAGE_RANGES  WITH(NOLOCK)        
		   WHERE COV_ID = @COVERAGE_CODE_ID AND      
		   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND        
		   ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND        
		   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND        
		   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')      
		  AND LIMIT_DEDUC_ID IN (1433,1434,1435,1436,1437,1438)    
		 ELSE IF (@BOAT_TYPE!='JS' AND @COVERAGE_CODE='BDEDUC' )    
	 	SELECT @DEDUCT_ID = LIMIT_DEDUC_ID        
	   	FROM MNT_COVERAGE_RANGES  WITH(NOLOCK)      
		   WHERE COV_ID = @COVERAGE_CODE_ID AND      
		   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND        
		   ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND        
		   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND        
		   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')      
		  AND LIMIT_DEDUC_ID NOT IN(1433,1434,1435,1436,1437,1438)    
		 --end here  
		END

 		--Flat  Commented on 10 jan 2007
--  		IF ( @DEDUCTIBLE_TYPE = 1 OR @DEDUCTIBLE_TYPE = 2)  
--  		BEGIN  
-- 			SELECT @DEDUCT_ID = LIMIT_DEDUC_ID  
-- 			FROM MNT_COVERAGE_RANGES  
-- 			WHERE COV_ID = @COVERAGE_CODE_ID AND  
-- 			ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND  
-- 			ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND  
-- 			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND  
-- 			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')  
--  		END  
	END
      
                   
	IF NOT EXISTS          
	(          
		SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		APP_ID=@APP_ID and                   
		APP_VERSION_ID = @APP_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)                  
	BEGIN                  
                    
		SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                   
		FROM APP_WATERCRAFT_COVERAGE_INFO                  
		where CUSTOMER_ID = @CUSTOMER_ID and                   
		APP_ID=@APP_ID and                   
		APP_VERSION_ID = @APP_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID                  
                  
		INSERT INTO APP_WATERCRAFT_COVERAGE_INFO                  
		(                  
			CUSTOMER_ID, 			
			APP_ID,                  
			APP_VERSION_ID,                  
			BOAT_ID,                  
			COVERAGE_ID,                  
			COVERAGE_CODE_ID,                  
			LIMIT_1_TYPE,                        
			LIMIT_2_TYPE,                              
			LIMIT_1,                        
			LIMIT_2,                        
			DEDUCTIBLE_1,                        
			DEDUCTIBLE_2 ,    
			LIMIT1_AMOUNT_TEXT ,    
			LIMIT2_AMOUNT_TEXT ,    
			DEDUCTIBLE1_AMOUNT_TEXT ,    
			DEDUCTIBLE2_AMOUNT_TEXT ,
			LIMIT_ID,      
			DEDUC_ID                           
		                   
		)                  
		VALUES                  
		(                  
			@CUSTOMER_ID,                  
			@APP_ID,                  
			@APP_VERSION_ID,                  
			@VEHICLE_ID,                  
			@COVERAGE_ID_MAX,     
			@COVERAGE_CODE_ID,                   
			@LIMIT_1_TYPE,                        
			@LIMIT_2_TYPE,       
			@LIMIT_1,      
			@LIMIT_2,            
			@DEDUCTIBLE_1,                        
			@DEDUCTIBLE_2,    
			@LIMIT1_AMOUNT_TEXT ,    
			@LIMIT2_AMOUNT_TEXT ,    
			@DEDUCTIBLE1_AMOUNT_TEXT ,    
			@DEDUCTIBLE2_AMOUNT_TEXT,
			@LIMIT_ID,      
			@DEDUCT_ID                                  
			   
		)                
	END                   
    ELSE
	BEGIN               
		--Update                  
		UPDATE APP_WATERCRAFT_COVERAGE_INFO                  
		SET                  
		LIMIT_1_TYPE = @LIMIT_1_TYPE,                        
		LIMIT_2_TYPE = @LIMIT_2_TYPE,                        
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
		DEDUC_ID = @DEDUCT_ID                      
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
		APP_ID = @APP_ID AND                  
		APP_VERSION_ID = @APP_VERSION_ID AND                  
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND        
		BOAT_ID = @VEHICLE_ID                
	END
                    
	IF @@ERROR <> 0      
	BEGIN      
 		RAISERROR ('Unable to add linked endorsments.', 16, 1)      
      
	END           
	--************************************************************     
  
 	RETURN @COVERAGE_CODE_ID            
              
END






GO

