IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_DWELLING_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_DWELLING_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --drop proc Proc_SAVE_DWELLING_COVERAGES        
        
/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_SAVE_DWELLING_COVERAGES                                    
Created by      : Pradeep                                    
Date            : 12/29/2005                                    
Purpose      :Inserts a record in Dwelling coverages              
Revison History :                                    
Used In  : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
CREATE           PROC Dbo.Proc_SAVE_DWELLING_COVERAGES                                    
(                                    
 @CUSTOMER_ID     int,                                    
 @APP_ID     int,                                    
 @APP_VERSION_ID     smallint,                                    
 @DWELLING_ID smallint,                                    
 @COVERAGE_ID int,                                    
 @COVERAGE_CODE_ID int,                                    
 @LIMIT_1 Decimal(18,2),                                    
 @LIMIT_2 Decimal(18,2),                                         
 @DEDUCTIBLE_1 DECIMAL(18,2),                                    
 @DEDUCTIBLE_2 DECIMAL(18,2),      
 @COVERAGE_TYPE NChar(10) = 'S1',    
 @LIMIT_ID Int = NULL,    
 @DEDUC_ID Int = NULL    
             
)                                    
AS                                    
                                    
DECLARE @COVERAGE_ID_MAX smallint                       
                             
BEGIN                                    
                                     
    DECLARE @STATEID SmallInt                                        
  DECLARE @LOBID NVarCHar(5)                     
                    
    SELECT @STATEID = STATE_ID,                                        
    @LOBID = APP_LOB                                        
  FROM APP_LIST                                        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                        
    APP_ID = @APP_ID AND                                        
    APP_VERSION_ID = @APP_VERSION_ID                           
                                    
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
                       
     SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                                 
    FROM APP_DWELLING_SECTION_COVERAGES                                    
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
      LIMIT_1,                                    
      LIMIT_2,                                    
      DEDUCTIBLE_1,          
      DEDUCTIBLE_2 ,      
      COVERAGE_TYPE,    
      LIMIT_ID,    
      DEDUC_ID              
    )                                    
    VALUES                                    
    (                                    
      @CUSTOMER_ID,                                    
      @APP_ID,                                    
      @APP_VERSION_ID,                                    
      @DWELLING_ID,                                    
      @COVERAGE_ID_MAX,                               
      @COVERAGE_CODE_ID,                     
      @LIMIT_1,                                    
      @LIMIT_2,                                    
      @DEDUCTIBLE_1,            
      @DEDUCTIBLE_2,      
     @COVERAGE_TYPE ,    
     @LIMIT_ID,    
      @DEDUC_ID                        
    )                                
                                 
                         
                  
                
  END                     
                                    
                   
  ELSE --End of Insert                                   
                                    
 BEGIN                
                      
  --Update                                    
  UPDATE APP_DWELLING_SECTION_COVERAGES                                    
  SET                                        
                       
   LIMIT_1 = @LIMIT_1,                                    
   LIMIT_2 = @LIMIT_2,                                    
                
   --DEDUCTIBLE_1 = @DEDUCTIBLE_1,                                    
   --DEDUCTIBLE_2 = @DEDUCTIBLE_2,                               
   LIMIT_ID = @LIMIT_ID--,    
   --DEDUC_ID = @DEDUC_ID                
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                    
   APP_ID = @APP_ID AND                                    
   APP_VERSION_ID = @APP_VERSION_ID AND                                    
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID  AND                      
   DWELLING_ID = @DWELLING_ID                                     
                   
 END                
        
  --Insert Linked Endorsements here------------------------------              
 EXEC Proc_UPDATE_LINKED_HOME_ENDORSEMENTS              
  @CUSTOMER_ID, --@CUSTOMER_ID     int,                                    
  @APP_ID, --@APP_ID     int,                                    
  @APP_VERSION_ID, --@APP_VERSION_ID   smallint,                                    
  @DWELLING_ID, --@DWELLING_ID smallint,                                    
  0, --@COVERAGE_ID int,                                    
  @COVERAGE_CODE_ID --@COVERAGE_CODE_ID int              
        
    IF (@@ERROR <> 0 )                           
   BEGIN                                 
        declare @message VarChar(100)        
  SET @message = 'Unable to save linked Endorsement for Coverage ID' + Convert(VarChar(10),@COVERAGE_CODE_ID)        
          RAISERROR (@message,16, 1)                                
          RETURN            
       END                 
 -----------------------------------------------------------------             
                 
END                 
                 
                   
                    
                
                               
                                    
                                    
                                  
                                  
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  
  
GO

