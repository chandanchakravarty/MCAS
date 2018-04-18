IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateHomeCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateHomeCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_UpdateHomeCoverage      
Created by      : Ravindra      
Date            : 06-23-2006      
Purpose         : Saves records in Home/Rental Coverages       
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/               
--- drop proc Proc_UpdateHomeCoverage      
CREATE  proc Proc_UpdateHomeCoverage      
(                        
 @CUSTOMER_ID     int,                        
 @APP_ID     int,                        
 @APP_VERSION_ID     smallint,                        
 @DWELLING_ID smallint,                        
 @COVERAGE_CODE VarChar(10),                       
 @LIMIT_1 Decimal(18,2),                              
 @LIMIT1_AMOUNT_TEXT NVarChar(100),                              
 @LIMIT_2 Decimal(18,2),                            
 @LIMIT2_AMOUNT_TEXT NVarChar(100),                              
 @DEDUCTIBLE_1 DECIMAL(18,2),                              
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                              
 @DEDUCTIBLE_2 DECIMAL(18,2),                              
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100)                              
)                        
AS                        
                        
DECLARE @COVERAGE_ID_MAX smallint       
DECLARE @LIMIT_1_TYPE NVarChar(10)                              
DECLARE @LIMIT_2_TYPE NVarChar(10)         
DECLARE @DEDUCTIBLE_1_TYPE NVarChar(10)                              
DECLARE @DEDUCTIBLE_2_TYPE NVarChar(10)         
DECLARE @COVERAGE_ID int            
DECLARE @COVERAGE_CODE_ID int     
-------  
declare  @OLD_LIMIT_1 Decimal(18,2)                              
declare @OLD_LIMIT1_AMOUNT_TEXT NVarChar(100)      
declare @OLD_LIMIT_2 Decimal(18,2)                            
declare @OLD_LIMIT2_AMOUNT_TEXT NVarChar(100)                              
declare @OLD_DEDUCTIBLE_1 DECIMAL(18,2)                              
declare @OLD_DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100)                              
declare @OLD_DEDUCTIBLE_2 DECIMAL(18,2)                              
declare @OLD_DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100)                              
  
          
  
BEGIN                        
            
 SET @COVERAGE_CODE_ID = 0      
      
 SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )      
 FROM MNT_COVERAGE MNT INNER JOIN APP_LIST APP ON            
    MNT.LOB_ID = APP.APP_LOB AND      
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
   --added by pravesh  
 SELECT  @OLD_LIMIT_1   =LIMIT_1,  
  @OLD_LIMIT1_AMOUNT_TEXT =LIMIT1_AMOUNT_TEXT,                              
  @OLD_LIMIT_2    =LIMIT_2,                            
  @OLD_LIMIT2_AMOUNT_TEXT  =LIMIT2_AMOUNT_TEXT,  
  @OLD_DEDUCTIBLE_1   =DEDUCTIBLE_1,                              
  @OLD_DEDUCTIBLE1_AMOUNT_TEXT  =DEDUCTIBLE1_AMOUNT_TEXT,                              
  @OLD_DEDUCTIBLE_2   =DEDUCTIBLE_2,                              
  @OLD_DEDUCTIBLE2_AMOUNT_TEXT  = DEDUCTIBLE2_AMOUNT_TEXT  
 FROM APP_DWELLING_SECTION_COVERAGES                      
   WHERE CUSTOMER_ID = @CUSTOMER_ID and                         
      APP_ID=@APP_ID and                         
      APP_VERSION_ID = @APP_VERSION_ID                         
      and DWELLING_ID = @DWELLING_ID  AND       
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID   
  
 IF (isnull(@OLD_LIMIT_1,0) = isnull(@LIMIT_1,0)  
     AND isnull(@OLD_LIMIT1_AMOUNT_TEXT,'') =isnull(@LIMIT1_AMOUNT_TEXT,'')                              
    AND isnull(@OLD_LIMIT_2,0)   =isnull(@LIMIT_2,0)                            
    AND isnull(@OLD_LIMIT2_AMOUNT_TEXT,'')  =isnull(@LIMIT2_AMOUNT_TEXT,'')  
    AND isnull(@OLD_DEDUCTIBLE_1,0)  =isnull(@DEDUCTIBLE_1,0)                              
    AND isnull(@OLD_DEDUCTIBLE1_AMOUNT_TEXT,'')  =isnull(@DEDUCTIBLE1_AMOUNT_TEXT,'')                              
    AND isnull(@OLD_DEDUCTIBLE_2,0)   =isnull(@DEDUCTIBLE_2,0)                              
    AND isnull(@OLD_DEDUCTIBLE2_AMOUNT_TEXT,'')  =isnull(@DEDUCTIBLE2_AMOUNT_TEXT,'')  
  )  
   begin   
     RETURN -1  -- NO CHANNGE NO UPDATATION  
   end  
   ---end here  
     
  SET @FLAG=1  
  SELECT @DEDUCTIBLE_1=  
    CASE WHEN  @DEDUCTIBLE_1 = -1 THEN DEDUCTIBLE_1  
           ELSE @DEDUCTIBLE_1  
           END,
  @LIMIT_1=  
    CASE WHEN  @LIMIT_1 = -1 THEN LIMIT_1  
           ELSE @LIMIT_1  
           END
  FROM APP_DWELLING_SECTION_COVERAGES  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND                        
     APP_ID = @APP_ID AND                        
     APP_VERSION_ID = @APP_VERSION_ID AND               
     DWELLING_ID = @DWELLING_ID AND                       
     COVERAGE_CODE_ID = @COVERAGE_CODE_ID       
   
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
   SELECT @DEDUCT_ID = LIMIT_DEDUC_ID        
   FROM MNT_COVERAGE_RANGES        
   WHERE COV_ID = @COVERAGE_CODE_ID AND        
   ISNULL(LIMIT_DEDUC_AMOUNT,0) = ISNULL(@DEDUCTIBLE_1,0) AND        
   ISNULL(LIMIT_DEDUC_AMOUNT1,0) = ISNULL(@DEDUCTIBLE_2,0) AND        
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE1_AMOUNT_TEXT)),'') AND        
   ISNULL(RTRIM(LTRIM(LIMIT_DEDUC_AMOUNT1_TEXT)),'') = ISNULL(RTRIM(LTRIM(@DEDUCTIBLE2_AMOUNT_TEXT)),'')        
   END        
 END      
            
                         
 if @FLAG=1                    
  BEGIN                        
                           
   --Update                        
   UPDATE APP_DWELLING_SECTION_COVERAGES                        
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
   DEDUC_ID = @DEDUCT_ID                            
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND                        
   APP_ID = @APP_ID AND                        
   APP_VERSION_ID = @APP_VERSION_ID AND                        
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND              
   DWELLING_ID = @DWELLING_ID          
 END      
  
 IF @@ERROR <> 0            
 BEGIN            
   RAISERROR ('Unable to Update Home/Rental Coverage.', 16, 1)            
            
 END                 
 --************************************************************           
  
   RETURN @COVERAGE_CODE_ID                  
                    
END      
      
      
  



GO

