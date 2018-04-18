IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY        
        
/*----------------------------------------------------------              
Proc Name       : dbo.Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY              
Created by      : SHAFI              
Date            : 02/10/2006             
Purpose         : Adjust coverages when dwelling is updated/insrted        
Revison History :              
Used In         :   Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/    
          
CREATE               PROC Dbo.Proc_ADJUST_DWELLING_COVERAGES_FOR_POLICY             
(              
 @CUSTOMER_ID     int,              
 @POL_ID     int,              
 @POL_VERSION_ID     smallint,              
 @DWELLING_ID smallint              
)              
              
AS              
        
DECLARE @POLICY_TYPE Int     
DECLARE @STATE_ID Int     
DECLARE @MARKET_VALUE DECIMAL(18,2)        
        
DECLARE @COVERAGE_B DECIMAL(18,2)        
DECLARE @COVERAGE_C DECIMAL(18,2)        
DECLARE @COVERAGE_D DECIMAL(18,2)        
                    
BEGIN        
  --<option selected="selected" value="11193">HO-2 Repair Cost</option>        
  --<option value="11194">HO-3 Repair Cost</option>        
         
  SELECT     
  @POLICY_TYPE= POLICY_TYPE ,  
  @STATE_ID = STATE_ID   
 FROM POL_CUSTOMER_POLICY_LIST                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
  POLICY_ID = @POL_ID AND                    
  POLICY_VERSION_ID = @POL_VERSION_ID         
         
          
  --If Policy type is Repair Cost, readjust coverages        
IF ( @POLICY_TYPE = 11193 OR @POLICY_TYPE = 11194 OR     
 @POLICY_TYPE = 11403 OR  @POLICY_TYPE = 11404 OR    
 @POLICY_TYPE = 11290 OR  @POLICY_TYPE = 11292 OR    
  @POLICY_TYPE = 11480 OR  @POLICY_TYPE = 11482     
 )        
    
  BEGIN        
   SELECT @MARKET_VALUE = ISNULL(MARKET_VALUE,0)        
   FROM POL_DWELLINGS_INFO        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    POLICY_ID = @POL_ID AND        
    POLICY_VERSION_ID = @POL_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID           
         
           
   DECLARE @ROW_COUNT Int        
         
   SELECT @ROW_COUNT = COUNT(CUSTOMER_ID)        
   FROM POL_DWELLING_COVERAGE        
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
    POLICY_ID = @POL_ID AND        
    POLICY_VERSION_ID = @POL_VERSION_ID AND        
    DWELLING_ID = @DWELLING_ID           
      
   --Set min value for Market Value-----------------------    
  IF ( @MARKET_VALUE = 0 OR @MARKET_VALUE IS NULL )    
  BEGIN    
  
--MICHIGAN----------------  
 IF ( @STATE_ID = 22 )    
 BEGIN    
  --HO-2 Repair Cost    
  IF ( @POLICY_TYPE = 11403 )    
  BEGIN    
   SET @MARKET_VALUE = 15000    
  END    
      
  --HO-3 Repair Cost    
  IF ( @POLICY_TYPE = 11404 )    
  BEGIN    
   SET @MARKET_VALUE = 40000    
  END    
      
      
  --Rental    
  --DP-2 Repair Cost    
  IF ( @POLICY_TYPE = 11290)    
  BEGIN    
   SET @MARKET_VALUE = 10000    
  END    
      
  --DP-3 Repair Cost    
  IF ( @POLICY_TYPE = 11292  )    
  BEGIN    
   SET @MARKET_VALUE = 75000    
  END    
      
 END --OF MICHIGAN----------------------------------   
     
--INDIANA----------------------------------  
 IF ( @STATE_ID = 14 )    
  BEGIN    
   --HO-2 Repair Cost    
   IF ( @POLICY_TYPE = 11193 )    
   BEGIN    
    SET @MARKET_VALUE = 50000    
   END    
       
   --HO-3 Repair Cost    
   IF ( @POLICY_TYPE = 11194 )    
   BEGIN    
    SET @MARKET_VALUE = 50000    
   END    
       
       
   --Rental    
   --DP-2 Repair Cost    
   IF ( @POLICY_TYPE = 11480)    
   BEGIN    
    SET @MARKET_VALUE = 30000    
   END    
       
   --DP-3 Repair Cost    
   IF ( @POLICY_TYPE = 11482  )    
   BEGIN    
    SET @MARKET_VALUE = 75000    
   END    
      
 END  --OF INDIANA  
    
  END     
   -----------------------------------------------------    
       
   --Calculate percentages     
   SET @COVERAGE_B = @MARKET_VALUE * 0.10        
   SET @COVERAGE_C = @MARKET_VALUE * 0.50        
   SET @COVERAGE_D = @MARKET_VALUE * 0.30        
   ----          
           
   IF (  @ROW_COUNT = 0 )        
   BEGIN        
     --Insert        
     EXEC Proc_InsertPOL_DWELLING_COVERAGE                  
       @CUSTOMER_ID,--@CUSTOMER_ID     int,                  
       @POL_ID,--@POL_ID     int,                  
       @POL_VERSION_ID,--@POL_VERSION_ID     smallint,                  
       @DWELLING_ID,--@DWELLING_ID     smallint,                  
       @MARKET_VALUE,--@DWELLING_LIMIT    decimal (18,2),                  
       NULL,--@DWELLING_REPLACE_COST     nchar(2),                  
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
 
     EXEC Proc_UpdatePOL_DWELLING_COVERAGE                
       @CUSTOMER_ID,--@CUSTOMER_ID     int,                
       @POL_ID,--@POL_ID     int,                
       @POL_VERSION_ID,--@POL_VERSION_ID     smallint,                
       @DWELLING_ID,--@DWELLING_ID     smallint,                
       @MARKET_VALUE,--@DWELLING_LIMIT    decimal (18,2),                
       NULL,--@DWELLING_REPLACE_COST     nchar(2),                
       @COVERAGE_B,--@OTHER_STRU_LIMIT     decimal (18,2),                
       NULL,--@OTHER_STRU_DESC     nvarchar(200),                
       @COVERAGE_C,--@PERSONAL_PROP_LIMIT     decimal (18,2),                
       NULL,--@REPLACEMENT_COST_CONTS     nchar(2),                
       @COVERAGE_D,--@LOSS_OF_USE     decimal (18,2),                
       100000,--@PERSONAL_LIAB_LIMIT     decimal (18,2),                
       1000,--@MED_PAY_EACH_PERSON     decimal (18,2),                
       NULL,--@ALL_PERILL_DEDUCTIBLE_AMT     decimal (18,2),                
       NULL--@THEFT_DEDUCTIBLE_AMT     decimal (18,2)         END         
            
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

