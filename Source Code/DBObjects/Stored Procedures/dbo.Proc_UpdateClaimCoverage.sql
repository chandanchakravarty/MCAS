IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClaimCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClaimCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


        
 /*----------------------------------------------------------                                                                
Proc Name             : Proc_UpdateClaimCoverage                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 31/01/2011                                                               
Purpose               : To update coverage in claim
Revison History       :                                                                
Used In               : claim module                      
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc Proc_UpdateClaimCoverage                                                      
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
                              
CREATE PROCEDURE [dbo].[Proc_UpdateClaimCoverage]                                    
                                   
                   
 @CLAIM_ID         			int                      
,@CLAIM_COV_ID     			int      
,@COVERAGE_CODE_ID 			int      
,@LIMIT_1          			decimal(18,2)    
,@IS_USER_CREATED  			CHAR(1)   
,@LIMIT_OVERRIDE   			CHAR(1)      
,@POLICY_LIMIT     			decimal(18,2)       
,@MINIMUM_DEDUCTIBLE		 decimal(18,2)  
,@VICTIM_ID					 int
,@MODIFIED_BY				 int
,@LAST_UPDATED_DATETIME      datetime   
,@DEDUCTIBLE1_AMOUNT_TEXT    NVARCHAR(1000) 
,@COVERAGE_SI_FLAG			 CHAR(1)='N'
,@ERROR_CODE				 INT OUT 

       
                                    
AS                                    
BEGIN 

DECLARE @IS_CREATED_BY_ACC_COI_LOAD CHAR(1)

   SELECT @IS_CREATED_BY_ACC_COI_LOAD= ACC_COI_FLG FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID

 IF(@IS_CREATED_BY_ACC_COI_LOAD='N' AND  EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WITH(NOLOCK) WHERE CLAIM_ID= @CLAIM_ID))       
 BEGIN
  SET @ERROR_CODE=-4
  RETURN
 END
 
  IF( @VICTIM_ID!=0 AND  EXISTS(SELECT CLAIM_ID FROM CLM_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CLAIM_ID= @CLAIM_ID AND COVERAGE_CODE_ID=@COVERAGE_CODE_ID AND VICTIM_ID=@VICTIM_ID AND CLAIM_COV_ID!=@CLAIM_COV_ID))  
  BEGIN  
     -- COVERAGE IS ALREADY ADDED FOR THIS VICTIM  
	  SET @ERROR_CODE=-3 
	  RETURN  
  END
 
                  
   IF( @IS_USER_CREATED='Y'  )
    BEGIN
    
      UPDATE CLM_PRODUCT_COVERAGES
		 SET LIMIT_1				 = @LIMIT_1,  
			 LIMIT_OVERRIDE			 = @LIMIT_OVERRIDE,  
			 POLICY_LIMIT			 = @POLICY_LIMIT,  
			 MINIMUM_DEDUCTIBLE      = @MINIMUM_DEDUCTIBLE, 
			 MODIFIED_BY			 = @MODIFIED_BY  ,
			 LAST_UPDATED_DATETIME   = @LAST_UPDATED_DATETIME,
			 DEDUCTIBLE1_AMOUNT_TEXT = @DEDUCTIBLE1_AMOUNT_TEXT ,
			 VICTIM_ID				 = @VICTIM_ID,
			 COVERAGE_SI_FLAG		 = @COVERAGE_SI_FLAG
      WHERE (CLAIM_ID=@CLAIM_ID AND CLAIM_COV_ID=@CLAIM_COV_ID)    
    
    END
   ELSE
    BEGIN
    
     UPDATE CLM_PRODUCT_COVERAGES
		 SET 
		     LIMIT_1				 = @LIMIT_1,  
			 POLICY_LIMIT			 = @POLICY_LIMIT,  
			 MINIMUM_DEDUCTIBLE      = @MINIMUM_DEDUCTIBLE,
			 VICTIM_ID				 = @VICTIM_ID,   
			 MODIFIED_BY			 = @MODIFIED_BY  ,
			 LAST_UPDATED_DATETIME   = @LAST_UPDATED_DATETIME,
			 COVERAGE_SI_FLAG		 = @COVERAGE_SI_FLAG			
      WHERE (CLAIM_ID=@CLAIM_ID AND CLAIM_COV_ID=@CLAIM_COV_ID) 
         
    END
   
    SET @ERROR_CODE=0


 
 
 
   
  
        
     
END 

GO

