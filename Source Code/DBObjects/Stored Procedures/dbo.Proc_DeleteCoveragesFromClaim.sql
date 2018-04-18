IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCoveragesFromClaim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCoveragesFromClaim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.Proc_DeleteCoveragesFromClaim                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 02/02/2011                                                            
Purpose               : To detete user added coverage from claim          
Revison History       :                                                              
Used In               : claim module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc Proc_DeleteCoveragesFromClaim                                                     
------   ------------       -------------------------*/                                                              
--                                 
                                  
--                               
                            
CREATE PROCEDURE [dbo].[Proc_DeleteCoveragesFromClaim]  
@CLAIM_COV_ID INT  ,
@CLAIM_ID INT ,
@ERROR_CODE INT OUT

AS                                  
BEGIN                       
      
  
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_ID=@CLAIM_COV_ID)
  BEGIN
    
    DELETE FROM CLM_PRODUCT_COVERAGES 
    WHERE  CLAIM_ID=@CLAIM_ID AND CLAIM_COV_ID=@CLAIM_COV_ID
    SET @ERROR_CODE=0
    
    
  END
  ELSE
   SET @ERROR_CODE=1
  
   
  
END 
GO

