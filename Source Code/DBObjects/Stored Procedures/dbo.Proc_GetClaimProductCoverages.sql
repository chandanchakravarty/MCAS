IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimProductCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimProductCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.Proc_GetClaimProductCoverages                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 26/11/2010                                                             
Purpose               : To fetch the all coverages                
Revison History       :                                                              
Used In               : claim module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc Proc_GetClaimProductCoverages                                                     
------   ------------       -------------------------*/                                                              
--                                 
                                  
--                               
                            
CREATE PROCEDURE [dbo].[Proc_GetClaimProductCoverages]  
@LOB_ID INT  ,
@CLAIM_ID INT,
@LANG_ID INT,
@FETCH_MODE VARCHAR(3)

AS                                  
BEGIN                       
      
  
   IF(@FETCH_MODE='OLD')    
     BEGIN
     
		   SELECT M.COV_ID ,SUBSTRING(ISNULL(L.COV_DES,M.COV_DES),1,175) AS COV_DES
		   FROM MNT_COVERAGE M WITH(NOLOCK) LEFT OUTER JOIN
				MNT_COVERAGE_MULTILINGUAL L WITH(NOLOCK) ON L.COV_ID= M.COV_ID AND LANG_ID=@LANG_ID 
		   WHERE LOB_ID=@LOB_ID AND IS_ACTIVE='Y' 
				 AND M.COV_ID NOT IN ( SELECT COVERAGE_CODE_ID 
									   FROM CLM_PRODUCT_COVERAGES WITH(NOLOCK) 
									   WHERE CLAIM_ID=@CLAIM_ID
									  )
      END
    ELSE
      BEGIN
      
           SELECT M.COV_ID ,SUBSTRING(ISNULL(L.COV_DES,M.COV_DES),1,175) AS COV_DES
		   FROM MNT_COVERAGE M WITH(NOLOCK) LEFT OUTER JOIN
				MNT_COVERAGE_MULTILINGUAL L WITH(NOLOCK) ON L.COV_ID= M.COV_ID AND LANG_ID=@LANG_ID 
		   WHERE LOB_ID=@LOB_ID AND IS_ACTIVE='Y' 
		   
      END

      
  
END 
GO

