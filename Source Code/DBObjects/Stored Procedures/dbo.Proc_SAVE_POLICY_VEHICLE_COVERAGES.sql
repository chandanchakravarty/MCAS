IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POLICY_VEHICLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POLICY_VEHICLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


     
-- drop proc Proc_SAVE_POLICY_VEHICLE_COVERAGES        
        
/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_SAVE_VEHICLE_COVERAGES                              
Created by      : Pradeep                              
Date            : 5/18/2005                              
Purpose     :Inserts a record in POLICY_HOME_OWNER_SUB_INSU                              
Revison History :                              
Used In  : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
CREATE           PROC dbo.Proc_SAVE_POLICY_VEHICLE_COVERAGES                              
(                              
 @CUSTOMER_ID     int,                              
 @POLICY_ID     int,                              
 @POLICY_VERSION_ID     smallint,                              
 @VEHICLE_ID smallint,                              
 @COVERAGE_ID int,                              
 @COVERAGE_CODE_ID int,                              
 @LIMIT_1 Decimal(18,2),                              
 @LIMIT_2 Decimal(18,2),                            
 @LIMIT1_AMOUNT_TEXT NVarChar(100),                              
 @LIMIT2_AMOUNT_TEXT NVarChar(100),                              
 @DEDUCTIBLE1_AMOUNT_TEXT NVarChar(100),                              
 @DEDUCTIBLE2_AMOUNT_TEXT NVarChar(100),                              
 @LIMIT_1_TYPE NVarChar(5),                              
 @LIMIT_2_TYPE NVarChar(5),                              
 @DEDUCTIBLE_1 DECIMAL(18,2),                              
 @DEDUCTIBLE_2 DECIMAL(18,2),                              
 @DEDUCTIBLE_1_TYPE NVarChar(5),                              
 @DEDUCTIBLE_2_TYPE NVarChar(5),                              
 @WRITTEN_PREMIUM DECIMAL(18,2),                              
 @FULL_TERM_PREMIUM DECIMAL(18,2) ,            
 @SIGNATURE_OBTAINED NChar(1),    
 @LIMIT_ID Int = NULL,    
 @DEDUC_ID Int = NULL ,    
 @ADD_INFORMATION varchar(20) = null                       
)                              
AS                              
                              
DECLARE @COVERAGE_ID_MAX smallint                 
                       
BEGIN                              
                               
    DECLARE @STATEID SmallInt                                  
  DECLARE @LOBID NVarCHar(5)               
              
    SELECT @STATEID = STATE_ID,                                  
    @LOBID = POLICY_LOB                                  
  FROM POL_CUSTOMER_POLICY_LIST                                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
    POLICY_ID = @POLICY_ID AND                                  
    POLICY_VERSION_ID = @POLICY_VERSION_ID                     
                              
  IF NOT EXISTS                              
  (                              
    SELECT * FROM POL_VEHICLE_COVERAGES                              
    where CUSTOMER_ID = @CUSTOMER_ID and                               
     POLICY_ID = @POLICY_ID and                               
     POLICY_VERSION_ID = @POLICY_VERSION_ID                               
     and VEHICLE_ID = @VEHICLE_ID AND                              
     COVERAGE_CODE_ID = @COVERAGE_CODE_ID                              
  )                              
                                
  BEGIN                   
                 
     SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                           
   FROM POL_VEHICLE_COVERAGES                              
   where CUSTOMER_ID = @CUSTOMER_ID and                               
    POLICY_ID=@POLICY_ID and                               
    POLICY_VERSION_ID = @POLICY_VERSION_ID                         
    and VEHICLE_ID = @VEHICLE_ID                     
                             
    INSERT INTO POL_VEHICLE_COVERAGES                              
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
      WRITTEN_PREMIUM,                              
      FULL_TERM_PREMIUM,                            
      LIMIT1_AMOUNT_TEXT,                  
      LIMIT2_AMOUNT_TEXT,                            
      DEDUCTIBLE1_AMOUNT_TEXT,                            
      DEDUCTIBLE2_AMOUNT_TEXT ,            
      SIGNATURE_OBTAINED,    
      LIMIT_ID,    
      DEDUC_ID,    
      ADD_INFORMATION                      
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
      @WRITTEN_PREMIUM,                              
     @FULL_TERM_PREMIUM,                            
      @LIMIT1_AMOUNT_TEXT,                            
      @LIMIT2_AMOUNT_TEXT,                            
      @DEDUCTIBLE1_AMOUNT_TEXT,                            
      @DEDUCTIBLE2_AMOUNT_TEXT,            
      @SIGNATURE_OBTAINED,    
      @LIMIT_ID,    
      @DEDUC_ID,    
   @ADD_INFORMATION                  
                      
    )                          
                           
                            
            
          
  END               
                              
             
  ELSE --End of Insert                             
                              
 BEGIN          
                                              
  --Update                              
  UPDATE POL_VEHICLE_COVERAGES                              
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
             
     SIGNATURE_OBTAINED= @SIGNATURE_OBTAINED ,    
   LIMIT_ID = @LIMIT_ID,    
      DEDUC_ID  =  @DEDUC_ID,    
    ADD_INFORMATION= @ADD_INFORMATION                            
             
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                 
   POLICY_ID = @POLICY_ID AND                              
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND                              
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND                
   VEHICLE_ID = @VEHICLE_ID                            
             
 END          
        
  --Update POL_CUSTOMER_POLICY_LIST Table   for all_data_valid    
      
 UPDATE POL_CUSTOMER_POLICY_LIST SET ALL_DATA_VALID =0    
 WHERE CUSTOMER_ID=@CUSTOMER_ID     
 AND POLICY_ID=@POLICY_ID     
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
     
        
--*****--Insert dependent Endorsements for this coverage**********        
EXEC Proc_UPDATE_POLICY_VEHICLE_LINKED_ENDORSEMENTS                
                                    
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                                    
  @POLICY_ID,--@POLICY_ID     int,                                    
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                                    
  @VEHICLE_ID,--@VEHICLE_ID smallint,                                    
  0,--@COVERAGE_ID int,                                    
  @COVERAGE_CODE_ID--@COVERAGE_CODE_ID int         
        
IF @@ERROR <> 0        
BEGIN        
 RAISERROR ('Unable to add linked endorsments.', 16, 1)        
        
END             
--************************************************************        
END           
           
             
              
          
                         
                              
                              
                            
                            
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
    
    
    
    
  
  
GO

