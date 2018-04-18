IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_HOME_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_HOME_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_SAVE_HOME_COVERAGES                
Created by      : GAURAV                
Date            : 5/18/2005                
Purpose     :Inserts a record in APP_DWELLING_SECTION1_COVERAGES                
Revison History :                
Used In  : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop proc Proc_SAVE_HOME_COVERAGES    
CREATE           PROC Dbo.Proc_SAVE_HOME_COVERAGES                
(                
 @CUSTOMER_ID     int,                
 @APP_ID     int,                
 @APP_VERSION_ID     smallint,                
 @DWELLING_ID smallint,                
 @COVERAGE_ID int,                
 @COVERAGE_CODE_ID int,                
 @LIMIT_1 Decimal(18,2),                
 @LIMIT_2 Decimal(18,2),                
 @LIMIT_1_TYPE NVarChar(5)=null,                
 @LIMIT_2_TYPE NVarChar(5)=null,                
 @DEDUCTIBLE_1 DECIMAL(18,2),                
 @DEDUCTIBLE_2 DECIMAL(18,2),                
 @DEDUCTIBLE_1_TYPE NVarChar(5)=null,                
 @DEDUCTIBLE_2_TYPE NVarChar(5)=null,                
 @WRITTEN_PREMIUM DECIMAL(18,2)=null,                
 @FULL_TERM_PREMIUM DECIMAL(18,2)=null,                
 @COVERAGE_TYPE nchar(10)=null,              
 @LIMIT1_AMOUNT_TEXT NVarChar(100) = NULL,                
 @LIMIT2_AMOUNT_TEXT NVarChar(100) = NULL,                
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100) = NULL,                
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100) = NULL,            
 @LIMIT_ID Int = NULL,            
 @DEDUC_ID Int = NULL,        
 @DEDUCTIBLE_AMOUNT DECIMAL(18,0)= NULL,      
 @DEDUCTIBLE_TEXT NVarChar(100)  = NULL ,     
 @ADDDEDUCTIBLE_ID int =null  ,    
 @COVERAGE_CODE VarChar(20)=null    
)                
AS                
                
DECLARE @COVERAGE_ID_MAX smallint             
DECLARE @DEDUCTIBLE Decimal(18,0)      
DECLARE @ISLIMITAPPLICABLE NChar(1)            
DECLARE @ISDEDUCTAPPLICABLE NChar(1)            
DECLARE @STATE_ID Int      
DECLARE @ISADDDEDUCTIBLE_APP NChar(1)    
DECLARE @RETVAL int  
set @RETVAL =0  
    
SET @ADDDEDUCTIBLE_ID=CASE @ADDDEDUCTIBLE_ID WHEN -1 THEN NULL    
                      ELSE @ADDDEDUCTIBLE_ID END     
BEGIN                
          
            
    
--start    
if(@COVERAGE_CODE_ID = -1 )    
BEGIN    
 EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@COVERAGE_CODE        
       
 IF EXISTS                         
 (                        
 SELECT * FROM APP_DWELLING_SECTION_COVERAGES    with(nolock)                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID and                           
 APP_ID=@APP_ID and                           
 APP_VERSION_ID = @APP_VERSION_ID                           
 and DWELLING_ID = @DWELLING_ID  AND         
 COVERAGE_CODE_ID = @COVERAGE_CODE_ID --AND                      
 --COVERAGE_TYPE = @COVERAGE_TYPE                        
 )      
 BEGIN     
  SELECT @DEDUCTIBLE_1= CASE WHEN  @DEDUCTIBLE_1 = -1     
   THEN DEDUCTIBLE_1    
   ELSE @DEDUCTIBLE_1   END    
  FROM APP_DWELLING_SECTION_COVERAGES   with(nolock)     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
  APP_ID = @APP_ID AND                          
  APP_VERSION_ID = @APP_VERSION_ID AND                 
  DWELLING_ID = @DWELLING_ID AND                         
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID         
    
  SELECT @LIMIT_1= CASE WHEN  @LIMIT_1 = -1     
   THEN LIMIT_1    
   ELSE @LIMIT_1   END    
  FROM APP_DWELLING_SECTION_COVERAGES    with(nolock)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
  APP_ID = @APP_ID AND                          
  APP_VERSION_ID = @APP_VERSION_ID AND                 
  DWELLING_ID = @DWELLING_ID AND                         
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID         
 END    
 ELSE    
 BEGIN    
      
  SET @DEDUCTIBLE_1=CASE WHEN  @DEDUCTIBLE_1 = -1 THEN NULL    
    WHEN   @DEDUCTIBLE_1 < -1 THEN (ISNULL(@DEDUCTIBLE_1,0) * -1)    
    ELSE @DEDUCTIBLE_1 END    
    
  SET @LIMIT_1=CASE WHEN  @LIMIT_1 = -1 THEN NULL    
    WHEN   @LIMIT_1 < -1 THEN (ISNULL(@LIMIT_1,0) * -1)    
    ELSE @LIMIT_1 END       
     
 END    
 IF ( @COVERAGE_CODE_ID = 0 )                      
 BEGIN                      
  RETURN -1                      
 END                     
          
     
 --Get Range ID-----------------------------------------------------------------            
 SELECT @ISLIMITAPPLICABLE = IsLimitApplicable,            
 @ISDEDUCTAPPLICABLE = IsDeductApplicable,            
 @LIMIT_1_TYPE = LIMIT_TYPE,            
 @DEDUCTIBLE_1_TYPE = DEDUCTIBLE_TYPE   ,         
 @ISADDDEDUCTIBLE_APP   = ISADDDEDUCTIBLE_APP     
 FROM MNT_COVERAGE    with(nolock)            
 WHERE COV_ID = @COVERAGE_CODE_ID            
     
 IF(@ISADDDEDUCTIBLE_APP ='1')    
 BEGIN     
  SELECT @ADDDEDUCTIBLE_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES with(nolock)               
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
  ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_AMOUNT,0) AND            
  ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,'') = ISNULL(@DEDUCTIBLE_TEXT,'') and    
  LIMIT_DEDUC_TYPE='Addded'        
      
 END     
    
 IF  ( @ISLIMITAPPLICABLE = '1' )            
 BEGIN            
  SELECT @LIMIT_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES     with(nolock)           
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
  ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@LIMIT_1,0)    
  AND ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@LIMIT_2,0)    
  AND ISNULL(LIMIT_DEDUC_AMOUNT_TEXT,'') =ISNULL(@LIMIT1_AMOUNT_TEXT,'')    
  AND ISNULL(LIMIT_DEDUC_AMOUNT1_TEXT,'') =ISNULL(@LIMIT2_AMOUNT_TEXT,'')    
    
        
 END            
 IF  ( @ISDEDUCTAPPLICABLE = '1' )            
 BEGIN            
 --Flat            
     
  SELECT @DEDUC_ID = LIMIT_DEDUC_ID            
  FROM MNT_COVERAGE_RANGES       with(nolock)         
  WHERE COV_ID = @COVERAGE_CODE_ID AND            
  ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND            
  ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0)         
     
 END    
END    
    
--end    
          
    
     
IF NOT EXISTS                
(                
 SELECT * FROM APP_DWELLING_SECTION_COVERAGES                
 where CUSTOMER_ID = @CUSTOMER_ID and                 
 APP_ID=@APP_ID and                 
 APP_VERSION_ID = @APP_VERSION_ID                 
 and DWELLING_ID = @DWELLING_ID AND                
 COVERAGE_CODE_ID = @COVERAGE_CODE_ID                
)                
          
 BEGIN                
 select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from APP_DWELLING_SECTION_COVERAGES                
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
 @APP_ID,                
 @APP_VERSION_ID,                
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
 @DEDUC_ID,          
 @DEDUCTIBLE_AMOUNT ,      
 @DEDUCTIBLE_TEXT  ,    
 @ADDDEDUCTIBLE_ID                
 )          
     
    
END                
ELSE     
BEGIN    
 --Update                
 UPDATE APP_DWELLING_SECTION_COVERAGES                
 SET                
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
 DEDUCTIBLE2_AMOUNT_TEXT  = @DEDUCTIBLE2_AMOUNT_TEXT,            
 LIMIT_ID =  @LIMIT_ID,            
 DEDUC_ID = @DEDUC_ID,          
 DEDUCTIBLE = @DEDUCTIBLE_AMOUNT ,      
 DEDUCTIBLE_TEXT = @DEDUCTIBLE_TEXT ,    
 ADDDEDUCTIBLE_ID =@ADDDEDUCTIBLE_ID            
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
 APP_ID = @APP_ID AND                
 APP_VERSION_ID = @APP_VERSION_ID AND                
 COVERAGE_CODE_ID = @COVERAGE_CODE_ID                
 AND DWELLING_ID=@DWELLING_ID     
  
  
set @RETVAL =-1  
END     
    
--Update linked endorsements          
          
EXEC Proc_UPDATE_DWELLING_ENDORSEMENTS          
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                                    
  @APP_ID,--@APP_ID     int,                                    
  @APP_VERSION_ID,--@APP_VERSION_ID     smallint,                                    
  @DWELLING_ID,--@DWELLING_ID smallint,                                    
  -1,--@COVERAGE_ID int,                 
  @COVERAGE_CODE_ID--@COVERAGE_CODE_ID int                
  
if (@RETVAL !=0)  
  return @RETVAL  
          
IF @@ERROR <> 0           
BEGIN          
   RAISERROR          
     ('Unable to update linked endorsments.',          
      16, 1)          
END          
                
END              
              
    
    

GO

