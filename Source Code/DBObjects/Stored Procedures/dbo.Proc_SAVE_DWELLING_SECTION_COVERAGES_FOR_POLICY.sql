IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_DWELLING_SECTION_COVERAGES_FOR_POLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_DWELLING_SECTION_COVERAGES_FOR_POLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_SAVE_DWELLING_SECTION_COVERAGES_FOR_POLICY      
      
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD_FOR_POLICY                    
Created by      : SHAFI                   
Date            : 16 Feb 2006                    
Purpose      :Inserts a record in POL_DWELLING_SECTION_COVERAGES                    
Revison History :                    
Used In  : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
CREATE           PROC Dbo.Proc_SAVE_DWELLING_SECTION_COVERAGES_FOR_POLICY                    
(                    
 @CUSTOMER_ID     int,                    
 @POL_ID     int,                    
 @POL_VERSION_ID     smallint,                    
 @DWELLING_ID smallint,                    
 @COVERAGE_ID int,                    
 @COVERAGE_CODE_ID int,                    
 @LIMIT_1 Decimal(18,2),                    
 @LIMIT_2 Decimal(18,2),     
 @DEDUCTIBLE_1 DECIMAL(18,2),                    
 @DEDUCTIBLE_2 DECIMAL(18,2),               
 @COVERAGE_TYPE nchar(10),                  
 @COVERAGE_CODE VarChar(20),    
 @LIMIT1_AMOUNT_TEXT nvarchar(100) = NULL,    
 @LIMIT2_AMOUNT_TEXT nvarchar(100) = NULL,    
 @DEDUCTIBLE1_AMOUNT_TEXT nvarchar(100) = NULL,    
 @DEDUCTIBLE2_AMOUNT_TEXT nvarchar(100) = NULL                    
)                    
AS                    
                  
DECLARE @COVERAGE_ID_MAX smallint                    
                  
BEGIN                    
                     
  -- Get  the Coverage ID                      
  EXECUTE @COVERAGE_CODE_ID =  Proc_GetCOVERAGE_IDForPolicy @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@COVERAGE_CODE                      
                   
 IF ( @COVERAGE_CODE_ID = 0 )                
BEGIN                
 --No such coverage found                
 RETURN 1                
END                
                
 IF EXISTS                   
 (                  
  SELECT * FROM POL_DWELLING_SECTION_COVERAGES                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID and                     
     POLICY_ID=@POL_ID and                     
     POLICY_VERSION_ID = @POL_VERSION_ID                     
     and DWELLING_ID = @DWELLING_ID  AND                  
 COVERAGE_CODE_ID = @COVERAGE_CODE_ID --AND                
    --COVERAGE_TYPE = @COVERAGE_TYPE                  
 )                  
 BEGIN                  
  --Update                    
  UPDATE POL_DWELLING_SECTION_COVERAGES                    
  SET                        
   LIMIT_1 = @LIMIT_1,                    
   LIMIT_2 = @LIMIT_2,     
    
   LIMIT1_AMOUNT_TEXT = @LIMIT1_AMOUNT_TEXT ,    
   LIMIT2_AMOUNT_TEXT = @LIMIT2_AMOUNT_TEXT ,    
    
   --DEDUCTIBLE_1 = @DEDUCTIBLE_1,                    
   --DEDUCTIBLE_2 = @DEDUCTIBLE_2,    
    
   DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT ,    
   DEDUCTIBLE2_AMOUNT_TEXT = @DEDUCTIBLE2_AMOUNT_TEXT     
                   
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
   POLICY_ID = @POL_ID AND                    
   POLICY_VERSION_ID = @POL_VERSION_ID AND           
   DWELLING_ID = @DWELLING_ID AND                   
   COVERAGE_CODE_ID = @COVERAGE_CODE_ID                    
                       
 END                  
 ELSE                  
 BEGIN                  
                    
  select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1                   
  from POL_DWELLING_SECTION_COVERAGES                    
  where CUSTOMER_ID = @CUSTOMER_ID and                     
   POLICY_ID=@POL_ID and                     
   POLICY_VERSION_ID = @POL_VERSION_ID                     
   and DWELLING_ID = @DWELLING_ID                    
                   
  INSERT INTO POL_DWELLING_SECTION_COVERAGES                    
  (                    
   CUSTOMER_ID,                    
   POLICY_ID,       
   POLICY_VERSION_ID,                    
   DWELLING_ID,                    
   COVERAGE_ID,                    
   COVERAGE_CODE_ID,                    
                
                 
   LIMIT_1,                    
   LIMIT_2,    
    
       
   --DEDUCTIBLE_1,     
   --DEDUCTIBLE_2,     
    
   LIMIT1_AMOUNT_TEXT,    
   LIMIT2_AMOUNT_TEXT,            
    
   DEDUCTIBLE1_AMOUNT_TEXT,    
   DEDUCTIBLE2_AMOUNT_TEXT,     
     
   COVERAGE_TYPE             
                     
                    
  )                    
  VALUES                    
  (                    
   @CUSTOMER_ID,                    
   @POL_ID,                    
   @POL_VERSION_ID,                    
   @DWELLING_ID,                    
   @COVERAGE_ID_MAX,                    
   @COVERAGE_CODE_ID,                    
                
   @LIMIT_1,                    
   @LIMIT_2,      
             
   --@DEDUCTIBLE_1,                    
   --@DEDUCTIBLE_2,      
       
   @LIMIT1_AMOUNT_TEXT,    
   @LIMIT2_AMOUNT_TEXT,           
   @DEDUCTIBLE1_AMOUNT_TEXT,    
   @DEDUCTIBLE2_AMOUNT_TEXT,    
    
   @COVERAGE_TYPE                      
                     
      
  )           
        
  --Insert Linked Endorsements here------------------------------        
 EXEC Proc_UPDATE_LINKED_HOME_ENDORSEMENTS_FOR_POLICY        
  @CUSTOMER_ID, --@CUSTOMER_ID     int,                              
  @POL_ID, --@POL_ID     int,                              
  @POL_VERSION_ID, --@POL_VERSION_ID   smallint,                              
  @DWELLING_ID, --@DWELLING_ID smallint,                              
  0, --@COVERAGE_ID int,                              
  @COVERAGE_CODE_ID --@COVERAGE_CODE_ID int        
 -----------------------------------------------------------------        
                  
 END                  
                  
                   
    RETURN 1                  
                   
END                  
                  
                
              
            
          
        
      
    
  
  
  



GO

