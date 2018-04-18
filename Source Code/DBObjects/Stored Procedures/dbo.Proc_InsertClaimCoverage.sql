IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClaimCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClaimCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
 /*----------------------------------------------------------                                                                  
Proc Name             : Proc_InsertClaimCoverage                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 31/01/2011                                                                 
Purpose               : To insert new coverage in claim  
Revison History       :                                                                  
Used In               : claim module                        
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc Proc_InsertClaimCoverage                                                        
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[Proc_InsertClaimCoverage]                                      
                                     
                     
 @CLAIM_ID          int                        
,@CLAIM_COV_ID      int OUT          
,@COVERAGE_CODE_ID  int        
,@LIMIT_1           decimal(18,2)      
,@LIMIT_OVERRIDE    char(1)        
,@POLICY_LIMIT      decimal(18,2)         
,@MINIMUM_DEDUCTIBLE decimal(18,2)  
,@CREATED_DATETIME  datetime  
,@VICTIM_ID         int  
,@CREATED_BY        int  
,@DEDUCTIBLE1_AMOUNT_TEXT  nvarchar(1000)    
,@CREATE_MODE       VARCHAR(10) 
,@COVERAGE_SI_FLAG CHAR(1)    
   
         
                                      
AS                                      
BEGIN   
  
  SET @VICTIM_ID=ISNULL(@VICTIM_ID,0);
  
  IF(EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE  WHERE CLAIM_ID= @CLAIM_ID))       
  BEGIN
  
    -- RESERVE IS SET SO NO MORE COVERAGE CAN ADDED
	  SET @CLAIM_COV_ID=-4 
	  RETURN  
  END 
  
  IF( EXISTS(SELECT CLAIM_ID FROM CLM_PRODUCT_COVERAGES  WHERE CLAIM_ID= @CLAIM_ID AND COVERAGE_CODE_ID=@COVERAGE_CODE_ID AND 
															VICTIM_ID = CASE WHEN @VICTIM_ID=0  THEN -1000 ELSE @VICTIM_ID END ) ) 
  BEGIN  
     -- COVERAGE IS ALREADY ADDED FOR THIS VICTIM  
	  SET @CLAIM_COV_ID=-3 
	  RETURN  
  END
  
  IF( @CREATE_MODE ='CREATE' )
  BEGIN  
      IF(EXISTS(SELECT CLAIM_ID FROM CLM_PRODUCT_COVERAGES  WHERE CLAIM_ID= @CLAIM_ID AND COVERAGE_CODE_ID=@COVERAGE_CODE_ID))
      BEGIN
	   -- COVERAGE IS ALREADY ADDED FOR THIS CLAIM
		  SET @CLAIM_COV_ID=-5 
		  RETURN  
      END
  END
  
  DECLARE @RISK_ID INT  
  SELECT @CLAIM_COV_ID=(ISNULL(MAX([CLAIM_COV_ID]),0))+1  FROM [dbo].[CLM_PRODUCT_COVERAGES]  WITH(NOLOCK)                  
     
  SELECT @RISK_ID= INSURED_PRODUCT_ID   
  FROM [dbo].[CLM_INSURED_PRODUCT]  WITH(NOLOCK)      
  WHERE CLAIM_ID=@CLAIM_ID      
    
  
  
   IF(@RISK_ID IS NULL OR @RISK_ID='' )                  
   BEGIN     
       SET @CLAIM_COV_ID=-2  
       RETURN
   END    
   SELECT * FROM CLM_PRODUCT_COVERAGES ORDER BY CREATED_DATETIME DESC
   
   INSERT INTO CLM_PRODUCT_COVERAGES   
      (  
    CLAIM_ID,                   
    CLAIM_COV_ID,     
    RISK_ID,  
    COVERAGE_CODE_ID,    
    IS_RISK_COVERAGE,        
    LIMIT_1,    
    LIMIT_OVERRIDE,    
    POLICY_LIMIT,    
    MINIMUM_DEDUCTIBLE,     
    DEDUCTIBLE1_AMOUNT_TEXT  ,  
    IS_USER_CREATED,  
    IS_ACTIVE,  
    VICTIM_ID,  
    CREATED_BY,  
    CREATED_DATETIME  ,
    RI_APPLIES,
    DEDUCTIBLE_1,
    COVERAGE_SI_FLAG
    
          
      )  
     VALUES  
     (  
     @CLAIM_ID           
    ,@CLAIM_COV_ID   
    ,@RISK_ID      
    ,@COVERAGE_CODE_ID   
    ,'Y'--IS_RISK_COVERAGE  
    ,@LIMIT_1            
    ,@LIMIT_OVERRIDE     
    ,@POLICY_LIMIT       
    ,@MINIMUM_DEDUCTIBLE       
    ,@DEDUCTIBLE1_AMOUNT_TEXT  
    ,'Y'--IS_USER_CREATED  
    ,'Y' --IS_ACTIVE,  
    ,@VICTIM_ID  
    ,@CREATED_BY  
    ,@CREATED_DATETIME  
    ,'N' --RI_APPLIES     
    ,0 --DEDUCTIBLE_1 
    ,@COVERAGE_SI_FLAG   
     )        
      
       
END 
GO

