IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.Proc_GetClaimCoverages                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 26/11/2010                                                           
Purpose               : To fetch the claim coverages              
Revison History       :                                                            
Used In               : claim module                  
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc Proc_GetClaimCoverages                                                   
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[Proc_GetClaimCoverages]                                
                               
               
@CLAIM_ID        int                  
,@LANG_ID          smallint         
,@CARRIER_CODE NVARCHAR(6)  ='W001'                                                         
                                
AS                                
BEGIN                     
    
--- dbo.fun_GetMessage(282,1) =Included    
--- dbo.fun_GetMessage(283,1) =Excluded    
--- dbo.fun_GetMessage(284,1) =Yes    
--- dbo.fun_GetMessage(285,1) =No    
     
  
  
   SELECT  C.COVERAGE_CODE_ID,C.IS_RISK_COVERAGE,ISNULL(M.COV_DES,N.COV_DES) AS COV_DES,    
  (CASE WHEN C.IS_ACTIVE='Y' THEN dbo.fun_GetMessage(282,@LANG_ID) ELSE dbo.fun_GetMessage(283,@LANG_ID)END) AS IS_ACTIVE,    
  (CASE WHEN C.RI_APPLIES='Y' THEN dbo.fun_GetMessage(284,@LANG_ID) ELSE dbo.fun_GetMessage(285,@LANG_ID)END) AS RI_APPLIES ,    
  (CASE WHEN C.LIMIT_OVERRIDE='Y' THEN dbo.fun_GetMessage(284,@LANG_ID) ELSE dbo.fun_GetMessage(285,@LANG_ID)END) AS LIMIT_OVERRIDE ,      
   C.LIMIT_1,C.DEDUCTIBLE_1,C.POLICY_LIMIT  ,C.DEDUCTIBLE1_AMOUNT_TEXT   
  FROM CLM_PRODUCT_COVERAGES C LEFT OUTER JOIN
  MNT_COVERAGE M ON COVERAGE_CODE_ID=M.COV_ID    LEFT OUTER JOIN
  MNT_COVERAGE_MULTILINGUAL N ON COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID 
  WHERE (CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='Y' )   
  
 SELECT ISNULL(A.CLM_RI_APPLIES_FLG ,0) AS CLM_RI_APPLIES_FLG
 FROM MNT_SYSTEM_PARAMS A WITH(NOLOCK) INNER JOIN
      MNT_REIN_COMAPANY_LIST B ON A.SYS_CARRIER_ID=B.REIN_COMAPANY_ID
 WHERE B.REIN_COMAPANY_CODE=@CARRIER_CODE AND B.IS_ACTIVE='Y'     
    
 
END 
GO

