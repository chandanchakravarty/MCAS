IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_VEHICLE_COVERAGES_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_VEHICLE_COVERAGES_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : Dbo.Proc_SAVE_VEHICLE_COVERAGES_ACORD                                
Created by      :                                 
Date            : 4/25/2005         
Purpose        :                                 
Revison History :                                
Used In         : Wolverine                                
                                
Modified By  :                                 
Modified On  :                                 
Purpose   : save vehicle coverages Acord                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                   
--drop PROC Dbo.Proc_SAVE_VEHICLE_COVERAGES_ACORD      
CREATE   PROC Dbo.Proc_SAVE_VEHICLE_COVERAGES_ACORD                  
(                  
 @CUSTOMER_ID     int,                  
 @APP_ID     int,                  
 @APP_VERSION_ID     smallint,                  
 @VEHICLE_ID smallint,                  
 @COVERAGE_ID int,                  
 @COVERAGE_CODE NVarChar(10)=null,         
 @COVERAGE_CODE_ID int=null,                 
 @LIMIT_1 Decimal(18,2)=null,                  
 @LIMIT_2 Decimal(18,2)=null,                  
 @LIMIT_1_TYPE NVarChar(5)=null,                  
 @LIMIT_2_TYPE NVarChar(5)=null,                  
 @DEDUCTIBLE_1 DECIMAL(18,2)=null,                  
 @DEDUCTIBLE_2 DECIMAL(18,2)=null,                  
 @DEDUCTIBLE_1_TYPE NVarChar(5)=null,                  
 @DEDUCTIBLE_2_TYPE NVarChar(5)=null,                  
 @LIMIT1_AMOUNT_TEXT NVarChar(100)=null,                
 @LIMIT2_AMOUNT_TEXT NVarChar(100)=null,                  
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100)=null,                
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100)=null ,        
 @SIGNATURE_OBTAINED NChar(1)=null,
 @ADD_INFORMATION NVarchar(20)=null          
)                  
AS                  
                  
DECLARE @COVERAGE_ID_MAX smallint                  
--DECLARE @COVERAGE_CODE_ID Int                  
DECLARE @ISLIMITAPPLICABLE NChar(1)            
DECLARE @ISDEDUCTAPPLICABLE NChar(1)            
DECLARE @LIMIT_TYPE NChar(1)            
DECLARE @DEDUCTIBLE_TYPE NChar(1)            
DECLARE @LIMIT_ID Int            
DECLARE @DEDUCT_ID Int        
DECLARE @APP_EFFECTIVE_DATE DateTime -- Mohit Agarwal 10-Jul-2007       
                  
BEGIN                  
 -- Get  the Coverage ID                  
 EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID_ACORD @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COVERAGE_CODE                  
                   
 IF ( @COVERAGE_CODE_ID = 0 )                  
 BEGIN                  
  --RAISERROR ('Coverage Code not found in MNT_COVERAGES. Could not insert into Coverages', 16, 1)                  
  RETURN -1                  
 END     
               
 SELECT @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE FROM APP_LIST WHERE CUSTOMER_ID= @CUSTOMER_ID
				AND APP_ID= @APP_ID AND APP_VERSION_ID= @APP_VERSION_ID
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
          
  print(@LIMIT1_AMOUNT_TEXT)          
          
  SELECT @LIMIT_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES            
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@LIMIT_1,0) AND   
   ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@LIMIT_2,0) AND            
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT1_AMOUNT_TEXT)),'') AND            
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT2_AMOUNT_TEXT)),'') AND
   ISNULL(EFFECTIVE_TO_DATE, '01/01/3000') >= @APP_EFFECTIVE_DATE AND 
   ISNULL(EFFECTIVE_FROM_DATE, '01/01/1950') <= @APP_EFFECTIVE_DATE
            
 print (@COVERAGE_CODE_ID)          
 print(@LIMIT_ID)          
 --END            
           
END            
            
IF  ( @ISDEDUCTAPPLICABLE = '1' )            
BEGIN        
 --Flat            
 IF ( @DEDUCTIBLE_TYPE = 1 OR @DEDUCTIBLE_TYPE = 2)            
 BEGIN            
  SELECT @DEDUCT_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES            
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND            
      ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND            
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND            
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')            
 END            
             
 /*            
 --Split             
 IF ( @DEDUCTIBLE_TYPE = 2 )            
 BEGIN            
  SELECT @DEDUCT_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES            
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND            
   ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND            
   ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,'') = ISNULL(@DEDUCTIBLE1_AMOUNT_TEXT,'')  AND            
   ISNULL(LIMIT_DEDUC_AMOUNT1_TEXT,'') = ISNULL(@DEDUCTIBLE2_AMOUNT_TEXT,'')            
 END*/            
END            
---------------------------------------------------------------------------------            
            
                 
 IF NOT EXISTS                  
 (                   
  SELECT * FROM APP_VEHICLE_COVERAGES                  
  where CUSTOMER_ID = @CUSTOMER_ID and                   
   APP_ID=@APP_ID and                   
   APP_VERSION_ID = @APP_VERSION_ID                   
   and VEHICLE_ID = @VEHICLE_ID AND                  
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID                  
 )                  
 BEGIN                  
                    
  select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from APP_VEHICLE_COVERAGES                  
  where CUSTOMER_ID = @CUSTOMER_ID and                   
   APP_ID=@APP_ID and                   
   APP_VERSION_ID = @APP_VERSION_ID                   
   and VEHICLE_ID = @VEHICLE_ID                  
                  
  INSERT INTO APP_VEHICLE_COVERAGES                  
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
   LIMIT1_AMOUNT_TEXT,                  
   LIMIT2_AMOUNT_TEXT,                
   DEDUCTIBLE1_AMOUNT_TEXT,                
   DEDUCTIBLE2_AMOUNT_TEXT,            
   LIMIT_ID,            
   DEDUC_ID  ,           
   SIGNATURE_OBTAINED,
   ADD_INFORMATION              
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
   @LIMIT1_AMOUNT_TEXT,                  
   @LIMIT2_AMOUNT_TEXT,                
   @DEDUCTIBLE1_AMOUNT_TEXT,                
   @DEDUCTIBLE2_AMOUNT_TEXT,            
   @LIMIT_ID,            
   @DEDUCT_ID,        
   @SIGNATURE_OBTAINED,
   @ADD_INFORMATION                         
  )                  
 --RETURN @COVERAGE_CODE_ID                 
    
IF EXISTS                  
 (                   
  SELECT * FROM APP_VEHICLE_COVERAGES AVC   INNER JOIN MNT_COVERAGE MC
  ON AVC.COVERAGE_CODE_ID= MC.COV_ID              
  where CUSTOMER_ID = @CUSTOMER_ID and                   
   APP_ID=@APP_ID and                   
   APP_VERSION_ID = @APP_VERSION_ID                   
   and VEHICLE_ID = @VEHICLE_ID AND                  
   MC.COV_CODE = 'SLL'                 
 )                  
	BEGIN
		DELETE FROM APP_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND COVERAGE_CODE_ID IN (2,114,4,115)
	END
              
 END                   
                   
 --Update                  
 UPDATE APP_VEHICLE_COVERAGES                  
 SET                  
  CUSTOMER_ID = @CUSTOMER_ID,                  
  APP_ID =  @APP_ID,                  
  APP_VERSION_ID = @APP_VERSION_ID,                  
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID,                  
  LIMIT_1_TYPE = @LIMIT_1_TYPE,                  
  LIMIT_2_TYPE = @LIMIT_2_TYPE,                  
  LIMIT_1 = @LIMIT_1,                  
  LIMIT_2 = @LIMIT_2,                  
  DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,                  
  DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,                  
  DEDUCTIBLE_1 = @DEDUCTIBLE_1,                  
  DEDUCTIBLE_2 = @DEDUCTIBLE_2,                  
  LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT,                  
  LIMIT2_AMOUNT_TEXT =  @LIMIT2_AMOUNT_TEXT,                
  DEDUCTIBLE1_AMOUNT_TEXT =  @DEDUCTIBLE1_AMOUNT_TEXT,                
  DEDUCTIBLE2_AMOUNT_TEXT =  @DEDUCTIBLE2_AMOUNT_TEXT ,            
  LIMIT_ID = @LIMIT_ID,            
  DEDUC_ID = @DEDUCT_ID ,        
  SIGNATURE_OBTAINED=@SIGNATURE_OBTAINED,
	ADD_INFORMATION=@ADD_INFORMATION           
        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
  APP_ID = @APP_ID AND                  
  APP_VERSION_ID = @APP_VERSION_ID AND                  
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID AND          
  VEHICLE_ID = @VEHICLE_ID                    
                   
        
--*****--Insert dependent Endorsements for this coverage**********            
EXEC Proc_UPDATE_VEHICLE_LINKED_ENDORSEMENTS                    
                        
 @CUSTOMER_ID,--@CUSTOMER_ID     int,                                        
 @APP_ID,--@APP_ID     int,                                        
 @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                        
 @VEHICLE_ID,--@VEHICLE_ID smallint,                                        
 0,--@COVERAGE_ID int,                                        
 @COVERAGE_CODE_ID--@COVERAGE_CODE_ID int             
            
IF @@ERROR <> 0            
BEGIN            
 RAISERROR ('Unable to add linked endorsments.', 16, 1)            
            
END                 
--************************************************************           
        
 RETURN @COVERAGE_CODE_ID                  
                    
END                  
                  
        
                  
                  
                  
                  
                  
                  
                  
                  
                  

           
                  
                  
                  
                
        
        
      
    
  







GO

