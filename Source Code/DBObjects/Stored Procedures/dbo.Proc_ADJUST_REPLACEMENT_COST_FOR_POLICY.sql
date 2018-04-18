IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ADJUST_REPLACEMENT_COST_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ADJUST_REPLACEMENT_COST_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_ADJUST_REPLACEMENT_COST_FOR_POLICY
CREATE PROC Dbo.Proc_ADJUST_REPLACEMENT_COST_FOR_POLICY               
(                
 @CUSTOMER_ID     int,                
 @POL_ID     int,                
 @POL_VERSION_ID     smallint,                
 @DWELLING_ID smallint                
)                
                
AS                
          
DECLARE @POLICY_TYPE Int          
DECLARE @REPL_COST DECIMAL(18,0)          
DECLARE @DWELLING_REPLACE_COST NChar(1)    
    
DECLARE @COVERAGE_A DECIMAL(18,0)               
DECLARE @COVERAGE_B DECIMAL(18,0)          
DECLARE @COVERAGE_C DECIMAL(18,0)          
DECLARE @COVERAGE_D DECIMAL(18,0)          
                      
BEGIN          
  --<option selected="selected" value="11193">HO-2 Repair Cost</option>          
  --<option value="11194">HO-3 Repair Cost</option>          
           
  SELECT       
  @POLICY_TYPE= POLICY_TYPE      
 FROM POL_CUSTOMER_POLICY_LIST                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
  POLICY_ID = @POL_ID AND                      
  POLICY_VERSION_ID = @POL_VERSION_ID           
           
            
  --If Policy type is other than Repair Cost, readjust coverages          
 IF ( @POLICY_TYPE NOT IN     
 ( 11193,11194,11403,11404, 11480 , 11482, 11290, 11292)    
 )    
    
/*          
  IF ( @POLICY_TYPE <> 11193 OR @POLICY_TYPE <> 11194 OR     
 @POLICY_TYPE <> 11403 OR  @POLICY_TYPE <> 11404 )      */    
  BEGIN          
   SELECT @REPL_COST = ISNULL(REPLACEMENT_COST,0)    
   FROM POL_DWELLINGS_INFO          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POL_ID AND          
    POLICY_VERSION_ID = @POL_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID             
           
   SELECT     
   @DWELLING_REPLACE_COST = ISNULL(DWELLING_REPLACE_COST,'0')          
   FROM POL_DWELLING_COVERAGE    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POL_ID AND          
    POLICY_VERSION_ID = @POL_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID     
          
   DECLARE @ROW_COUNT Int          
           
   SELECT @ROW_COUNT = COUNT(*)          
   FROM POL_DWELLING_COVERAGE          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
    POLICY_ID = @POL_ID AND          
    POLICY_VERSION_ID = @POL_VERSION_ID AND          
    DWELLING_ID = @DWELLING_ID             
           
   --For 4, 4D, 6, 6D    
   IF ( @POLICY_TYPE = 11405 OR @POLICY_TYPE = 11407 OR     
 @POLICY_TYPE = 11406 OR @POLICY_TYPE = 11408 OR    
   @POLICY_TYPE = 11195 OR @POLICY_TYPE = 11245 OR     
 @POLICY_TYPE = 11196 OR @POLICY_TYPE = 11246     
 )    
   BEGIN    
  SET @COVERAGE_A = @REPL_COST * 0.10          
    SET @COVERAGE_D = @REPL_COST * 0.40    
    
 IF ( @COVERAGE_A < 2000 )     
 BEGIN    
  SET @COVERAGE_A = 2000    
 END    
          
   END    
   ELSE    
 BEGIN            
    --Calculate percentages     
    SET @COVERAGE_A = @REPL_COST        
    SET @COVERAGE_B = @REPL_COST * 0.10       
    
    IF ( @DWELLING_REPLACE_COST =  1 )    
    BEGIN        
    SET @COVERAGE_C = @REPL_COST * 0.70          
    END    
    ELSE    
    BEGIN    
  SET @COVERAGE_C = @REPL_COST * 0.50          
    END    
       
    SET @COVERAGE_D = @REPL_COST * 0.30          
   ----            
     END    
        
   IF (  @ROW_COUNT = 0 )          
   BEGIN          
     --Insert          
     EXEC Proc_InsertPOL_DWELLING_COVERAGE             
       @CUSTOMER_ID,--@CUSTOMER_ID     int,                    
       @POL_ID,--@POL_ID     int,                    
       @POL_VERSION_ID,--@POL_VERSION_ID     smallint,                    
       @DWELLING_ID,--@DWELLING_ID     smallint,                    
       @COVERAGE_A,--@DWELLING_LIMIT    decimal (18,2),                    
       @DWELLING_REPLACE_COST,--@DWELLING_REPLACE_COST     nchar(2),                    
       @COVERAGE_B,--@OTHER_STRU_LIMIT     decimal (18,2),                    
       NULL,--@OTHER_STRU_DESC     nvarchar(200),                    
       @COVERAGE_C,--@PERSONAL_PROP_LIMIT     decimal (18,2),                    
       NULL,--@REPLACEMENT_COST_CONTS     nchar(2),                    
       @COVERAGE_D,--@LOSS_OF_USE     decimal (18,2),                    
       100000,--@PERSONAL_LIAB_LIMIT     decimal (18,2),                    
       1000,--@MED_PAY_EACH_PERSON     decimal (18,2),                    
       NULL,--@ALL_PERILL_DEDUCTIBLE_AMT     decimal (18,2),                    
       NULL--@THEFT_DEDUCTIBLE_AMT     decimal (18,2)             
              
   END          
   ELSE          
   BEGIN          
     --Update    
       /*pass -1 to retain its previous value*/      
     EXEC Proc_UpdatePOL_DWELLING_COVERAGE                  
       @CUSTOMER_ID,--@CUSTOMER_ID     int,                  
       @POL_ID,--@POL_ID     int,                  
       @POL_VERSION_ID,--@POL_VERSION_ID     smallint,                  
       @DWELLING_ID,--@DWELLING_ID     smallint,                  
       @COVERAGE_A,--@DWELLING_LIMIT    decimal (18,2),                  
     @DWELLING_REPLACE_COST,--@DWELLING_REPLACE_COST     nchar(2),                  
       @COVERAGE_B,--@OTHER_STRU_LIMIT     decimal (18,2),                  
      -1,--@OTHER_STRU_DESC     nvarchar(200),                  
       @COVERAGE_C,--@PERSONAL_PROP_LIMIT     decimal (18,2),                  
       NULL,--@REPLACEMENT_COST_CONTS     nchar(2),                  
       @COVERAGE_D,--@LOSS_OF_USE     decimal (18,2),                  
       100000,--@PERSONAL_LIAB_LIMIT     decimal (18,2),                  
       1000,--@MED_PAY_EACH_PERSON     decimal (18,2), 
       -1,--@ALL_PERILL_DEDUCTIBLE_AMT     decimal (18,2),                  
       -1--@THEFT_DEDUCTIBLE_AMT     decimal (18,2)         END           
              
    --Update Linked Coverages           
    --Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS          
    EXEC Proc_Update_DWELLING_COVERAGES_FROM_COV_LIMITS_FOR_POLICY                                                      
       @CUSTOMER_ID,--@CUSTOMER_ID int,                                              
       @POL_ID,--@POL_ID int,                                              
       @POL_VERSION_ID,--@POL_VERSION_ID smallint,                                               
       @DWELLING_ID--@DWELLING_ID smallint                     
             
                              
    --Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS           
    EXEC Proc_UPDATE_HOME_ENDORSEMENTS_FROM_LIMITS_FOR_POLICY                                      
     @CUSTOMER_ID,--@CUSTOMER_ID     int,                                          
     @POL_ID,--@POL_ID     int,                                          
     @POL_VERSION_ID,--@POL_VERSION_ID     smallint,            
     @DWELLING_ID--@DWELLING_ID smallint                    
                         
           
   END          
 END        
END                
        
      
    
  




GO

