IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_AVIATION_VEHICLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_AVIATION_VEHICLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_SAVE_AVIATION_VEHICLE_COVERAGES                          
Created by      : Pravesh k Chandel                
Date            : 12 Jan 2010
Purpose     :Inserts a record in APP_AVIATION_VEHICLE_COVERAGES
Used In  : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
---drop proc dbo.Proc_SAVE_AVIATION_VEHICLE_COVERAGES                          
CREATE PROC dbo.Proc_SAVE_AVIATION_VEHICLE_COVERAGES                          
(                          
 @CUSTOMER_ID     int,                          
 @APP_ID     int,                          
 @APP_VERSION_ID     smallint,                          
 @VEHICLE_ID smallint,                          
 @COVERAGE_ID int=NULL,                          
 @COVERAGE_CODE_ID int=NULL,
 @COVERAGE_CODE NVARCHAR(20)=NULL,                          
 @LIMIT_1 Decimal(18,0)=NULL,                          
 @LIMIT_2 Decimal(18,0)=NULL,                        
 @LIMIT1_AMOUNT_TEXT NVarChar(100)=NULL,                          
 @LIMIT2_AMOUNT_TEXT NVarChar(100)=NULL,                          
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                          
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                          
 @LIMIT_1_TYPE NVarChar(5),                          
 @LIMIT_2_TYPE NVarChar(5),                          
 @DEDUCTIBLE_1 DECIMAL(18,0),                          
 @DEDUCTIBLE_2 DECIMAL(18,0),                          
 @DEDUCTIBLE_1_TYPE NVarChar(5)=null,                          
 @DEDUCTIBLE_2_TYPE NVarChar(5)=null,                          
 @WRITTEN_PREMIUM DECIMAL(18,2)=null,                          
 @FULL_TERM_PREMIUM DECIMAL(18,2)=null ,        
 @SIGNATURE_OBTAINED NChar(1)=null,  
 @LIMIT_ID Int = NULL,  
 @DEDUC_ID Int = NULL ,  
 @ADD_INFORMATION varchar(50) =null   ,
 @RATE  DECIMAL(14,4),  
 @COVERAGE_TYPE_ID     INT = NULL
)                          
AS                          
                          
DECLARE @COVERAGE_ID_MAX smallint             
                   
BEGIN                      
   
  DECLARE @STATEID SmallInt                              
  DECLARE @LOBID NVarCHar(5)           
   SELECT @STATEID = STATE_ID, @LOBID = APP_LOB                              
	FROM APP_LIST                              
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
    APP_ID = @APP_ID AND                              
    APP_VERSION_ID = @APP_VERSION_ID 

DECLARE @ISLIMITAPPLICABLE NChar(1)    
DECLARE @ISDEDUCTAPPLICABLE NChar(1)
DECLARE @LIMIT_TYPE NChar(1)    
DECLARE @DEDUCTIBLE_TYPE NChar(1)  

--Begin <01>
IF  @COVERAGE_CODE <> NULL
BEGIN

	EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COVERAGE_CODE          
	
	IF ( @COVERAGE_CODE_ID = 0 )          
	BEGIN          
		--RAISERROR ('Coverage Code not found in MNT_COVERAGES. Could not insert into Coverages', 16, 1)          
		RETURN -1          
	END          

	--Get Range ID-----------------------------------------------------------------    
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
	--Flat    
		IF ( @DEDUCTIBLE_TYPE = 1 OR @DEDUCTIBLE_TYPE = 2)    
		BEGIN    
			SELECT @DEDUC_ID = LIMIT_DEDUC_ID    
			FROM MNT_COVERAGE_RANGES    
			WHERE COV_ID = @COVERAGE_CODE_ID AND    
			ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND    
			ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND    
			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND    
			ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')    
		END 
	END 
END

--End <01>                
-------------                   
  IF NOT EXISTS                          
  (                          
    SELECT * FROM APP_AVIATION_VEHICLE_COVERAGES                          
    where CUSTOMER_ID = @CUSTOMER_ID and                           
     APP_ID=@APP_ID and         
     APP_VERSION_ID = @APP_VERSION_ID                           
     and VEHICLE_ID = @VEHICLE_ID AND          
     COVERAGE_CODE_ID = @COVERAGE_CODE_ID                          
  )                          
                            
  BEGIN               
           
     SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                       
   FROM APP_AVIATION_VEHICLE_COVERAGES                          
   where CUSTOMER_ID = @CUSTOMER_ID 
	and APP_ID=@APP_ID and                           
    APP_VERSION_ID = @APP_VERSION_ID                           
    and VEHICLE_ID = @VEHICLE_ID                 
                         
    INSERT INTO APP_AVIATION_VEHICLE_COVERAGES                          
    (                          
      CUSTOMER_ID,                          
      APP_ID,                          
      APP_VERSION_ID,                          
      VEHICLE_ID,                          
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
      LIMIT1_AMOUNT_TEXT,              
      LIMIT2_AMOUNT_TEXT,                        
      DEDUCTIBLE1_AMOUNT_TEXT,                        
      DEDUCTIBLE2_AMOUNT_TEXT ,        
      SIGNATURE_OBTAINED,  
      LIMIT_ID,  
      DEDUC_ID,  
      ADD_INFORMATION        ,
	  RATE,      
	  COVERAGE_TYPE_ID     
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
      @SIGNATURE_OBTAINED ,  
     @LIMIT_ID,  
      @DEDUC_ID ,  
      @ADD_INFORMATION   ,
	  @RATE,
	  @COVERAGE_TYPE_ID
    )                      
                       
                        
        
      
  END           
                          
         
  ELSE --End of Insert                         
                          
 BEGIN      
                                          
  --Update                          
  UPDATE APP_AVIATION_VEHICLE_COVERAGES                          
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
   SIGNATURE_OBTAINED= @SIGNATURE_OBTAINED,  
   LIMIT_ID = @LIMIT_ID,  
   DEDUC_ID =   @DEDUC_ID ,  
   ADD_INFORMATION = @ADD_INFORMATION ,
   RATE = @RATE,
   COVERAGE_TYPE_ID =  @COVERAGE_TYPE_ID
         
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
   APP_ID = @APP_ID AND                          
   APP_VERSION_ID = @APP_VERSION_ID AND                          
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND            
   VEHICLE_ID = @VEHICLE_ID                        
         
 END      
    
   print  @COVERAGE_CODE_ID

END       
       
         
          




GO

