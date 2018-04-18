IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES      
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES          
Created by      : Pravesh K Chandel
Date            : 14 Jan 2010
Purpose			:Inserts a record in POL_AVIATION_VEHICLE_COVERAGES          
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE   PROC [dbo].[Proc_SAVE_POL_AVIATION_VEHICLE_DEFAULT_COVERAGES]    
(          
 @CUSTOMER_ID     int,          
 @POLICY_ID     int,          
 @POLICY_VERSION_ID     smallint,          
 @VEHICLE_ID smallint,          
 @COVERAGE_ID int,          
 @COVERAGE_CODE NVarChar(10), 
 @COVERAGE_CODE_ID int=null,         
 @LIMIT_1 Decimal(18,2),          
 @LIMIT_2 Decimal(18,2),          
 @LIMIT_1_TYPE NVarChar(5)=null,          
 @LIMIT_2_TYPE NVarChar(5)=null,          
 @DEDUCTIBLE_1 DECIMAL(18,2),          
 @DEDUCTIBLE_2 DECIMAL(18,2),          
 @DEDUCTIBLE_1_TYPE NVarChar(5)=null,          
 @DEDUCTIBLE_2_TYPE NVarChar(5)=null,          
 @LIMIT1_AMOUNT_TEXT NVarChar(100),        
 @LIMIT2_AMOUNT_TEXT NVarChar(100),          
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),        
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100), 
 @RATE DECIMAL(12,2)=NULL,  
 @COVERAGE_TYPE_ID int=null        
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
          
BEGIN          
 -- Get  the Coverage ID          
 EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@COVERAGE_CODE          

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
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@LIMIT2_AMOUNT_TEXT)),'')     
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

END    
---------------------------------------------------------------------------------    
    
         
 IF NOT EXISTS          
 (           
  SELECT * FROM POL_AVIATION_VEHICLE_COVERAGES          
  where CUSTOMER_ID = @CUSTOMER_ID and           
   POLICY_ID=@POLICY_ID and           
   POLICY_VERSION_ID = @POLICY_VERSION_ID           
   and VEHICLE_ID = @VEHICLE_ID AND          
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID          
 )          
 BEGIN          
            
  select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from POL_AVIATION_VEHICLE_COVERAGES          
  where CUSTOMER_ID = @CUSTOMER_ID and           
   POLICY_ID=@POLICY_ID and           
   POLICY_VERSION_ID = @POLICY_VERSION_ID           
   and VEHICLE_ID = @VEHICLE_ID          
          
  INSERT INTO POL_AVIATION_VEHICLE_COVERAGES          
  (          
   CUSTOMER_ID,          
   POLICY_ID,          
   POLICY_VERSION_ID,          
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
   DEDUC_ID    ,
   RATE ,
  COVERAGE_TYPE_ID 
       
  )          
  VALUES          
  (          
   @CUSTOMER_ID,          
   @POLICY_ID,          
   @POLICY_VERSION_ID,          
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
   @DEDUCT_ID   ,
	@RATE ,
	@COVERAGE_TYPE_ID 
  )          
 --RETURN @COVERAGE_CODE_ID         

 END           
    
IF @@ERROR <> 0    
BEGIN    
 RAISERROR ('Unable to add linked endorsments.', 16, 1)    
    
END         
--************************************************************   

 RETURN @COVERAGE_CODE_ID          
            
END          
          
          
          
          
          
          
          
          
          
          
          
          
  
          
          
          
        
      
    
  

















GO

