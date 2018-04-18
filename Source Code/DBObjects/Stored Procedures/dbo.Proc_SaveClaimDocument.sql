IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveClaimDocument]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveClaimDocument]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
/*----------------------------------------------------------                                                        
Proc Name             : Dbo.[Proc_SaveClaimDocument]                                                        
Created by            : Shubhanshu Pandey                                                   
Date                  : 16/02/2011                                       
Purpose               :          
Revison History       :                                                        
Used In               :           
------------------------------------------------------------                                                        
Date     Review By          Comments                           
 Proc_SaveClaimDocument 907,3,0,'','CLM_LETTER_25'     
drop Proc [Proc_SaveClaimDocument]  980,2,0,'','CLM_LETTER_20'                                           
------   ------------       -------------------------*/          
        
CREATE PROC [dbo].[Proc_SaveClaimDocument]                  
  (        
  @CLAIM_ID INT,        
  @ACTIVITY_ID INT,        
  @PAYEE_ID INT,        
  @DOC_TEXT TEXT,        
  @PROCESS_TYPE VARCHAR(20)  
   )                
AS                                                                                            
BEGIN         
        
  DECLARE @PROCESS_ID INT             
  DECLARE @BRANCH_CODE INT  
  DECLARE @LETTER_SEQUENCE_NO INT 

    --------------------------------shubh
    
      DECLARE @BRANCH_CODE_CLM VARCHAR(8)--SH 
      DECLARE @LETTER_SEQUENCE_NO_CLM VARCHAR(10) 

      
    SELECT @BRANCH_CODE_CLM = (SELECT MDL.BRANCH_CODE  FROM CLM_CLAIM_INFO CCI WITH(NOLOCK)    
							   INNER JOIN CLM_ACTIVITY CA WITH(NOLOCK) ON CCI.CLAIM_ID = CA.CLAIM_ID                
							   INNER JOIN MNT_USER_LIST MUL  WITH(NOLOCK) ON MUL.[USER_ID] = CA.CREATED_BY            
							   INNER JOIN MNT_DIV_LIST MDL  WITH(NOLOCK) ON MDL.DIV_ID = MUL.USER_DEF_DIV_ID    
							   WHERE CCI.CLAIM_ID = @CLAIM_ID AND CA.ACTIVITY_ID = @ACTIVITY_ID)  
				
				
 SELECT @LETTER_SEQUENCE_NO_CLM=(SELECT ISNULL(MAX(LETTER_SEQUENCE_NO),0) +1     
    FROM   CLM_PROCESS_LOG WITH(NOLOCK)      
    WHERE  PROCESS_TYPE='CLM_RECEIPT'  AND   
			BRANCH_CODE = @BRANCH_CODE_CLM )

  
    				
				
--------------------------------------------
    
  SELECT @PROCESS_ID=(ISNULL(MAX([PROCESS_ID]),0)+1)  FROM [dbo].[CLM_PROCESS_LOG] WITH(NOLOCK)  
    
  SELECT @BRANCH_CODE=  
      (  
       SELECT PCPL.DIV_ID FROM CLM_CLAIM_INFO CCI   
       LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID=PCPL.CUSTOMER_ID AND CCI.POLICY_ID=PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID   
       LEFT OUTER JOIN CLM_ACTIVITY CA ON CA.CLAIM_ID=CCI.CLAIM_ID    
       WHERE CCI.CLAIM_ID =@CLAIM_ID AND CA.ACTIVITY_ID=@ACTIVITY_ID  
      )  
  
IF EXISTS(SELECT CLAIM_ID,ACTIVITY_ID,PROCESS_TYPE FROM CLM_PROCESS_LOG WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PROCESS_TYPE=@PROCESS_TYPE)  
BEGIN  
 SELECT @LETTER_SEQUENCE_NO=(SELECT  ISNULL(LETTER_SEQUENCE_NO,0) FROM CLM_PROCESS_LOG WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PROCESS_TYPE=@PROCESS_TYPE)  
END  
ELSE  
BEGIN   
 SELECT @LETTER_SEQUENCE_NO=ISNULL(MAX(LETTER_SEQUENCE_NO),0)+1  FROM CLM_PROCESS_LOG WHERE PROCESS_TYPE=@PROCESS_TYPE  AND CLAIM_ID IN   
 (  
  SELECT CCI.CLAIM_ID FROM CLM_CLAIM_INFO CCI   
  INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CCI.CUSTOMER_ID=PCPL.CUSTOMER_ID AND CCI.POLICY_ID=PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID   
  WHERE PCPL.DIV_ID=@BRANCH_CODE   
 )  
END  
    
         
IF(@PROCESS_TYPE='CLM_REMIND' or  @PROCESS_TYPE='CLM_RECEIPT')  
BEGIN  
  
 INSERT INTO CLM_PROCESS_LOG         
      (        
        CLAIM_ID,        
        ACTIVITY_ID,        
        PAYEE_ID,        
        PROCESS_ID,        
        DOC_TEXT,        
        PROCESS_TYPE ,  
        LETTER_SEQUENCE_NO,
        BRANCH_CODE     --SH  
      )          
      VALUES                        
      (        
        @CLAIM_ID,        
        @ACTIVITY_ID,        
        @PAYEE_ID,        
        @PROCESS_ID,        
        @DOC_TEXT,        
        @PROCESS_TYPE ,  
        @LETTER_SEQUENCE_NO_CLM ,
        @BRANCH_CODE_CLM  --SH  
      )         
 END   
  
ELSE  
BEGIN   
 IF NOT EXISTS (SELECT * FROM CLM_PROCESS_LOG WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND  PROCESS_TYPE=@PROCESS_TYPE)  
  BEGIN    
   INSERT INTO CLM_PROCESS_LOG         
     (        
       CLAIM_ID,        
       ACTIVITY_ID,        
       PAYEE_ID,        
       PROCESS_ID,        
       DOC_TEXT,        
       PROCESS_TYPE,  
       LETTER_SEQUENCE_NO      
     )          
     VALUES                        
     (        
       @CLAIM_ID,        
       @ACTIVITY_ID,        
       @PAYEE_ID,        
       @PROCESS_ID,        
       @DOC_TEXT,        
       @PROCESS_TYPE ,  
       @LETTER_SEQUENCE_NO        
     )         
  END  
    
 ELSE  
  BEGIN  
    
    UPDATE CLM_PROCESS_LOG   
     SET DOC_TEXT=@DOC_TEXT ,LETTER_SEQUENCE_NO=@LETTER_SEQUENCE_NO  
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PROCESS_TYPE=@PROCESS_TYPE   
  END  
END   
  
   
END  
      
-- IF(@PROCESS_TYPE='CLM_LETTER')                      
--      BEGIN  
   
--         INSERT INTO CLM_PROCESS_LOG         
--    (        
--      CLAIM_ID,        
--      ACTIVITY_ID,        
--      PAYEE_ID,        
--      PROCESS_ID,        
--      DOC_TEXT,        
--      PROCESS_TYPE,  
--      LETTER_SEQUENCE_NO      
--    )          
--    VALUES                        
--    (        
--      @CLAIM_ID,        
--      @ACTIVITY_ID,        
--      @PAYEE_ID,        
--      @PROCESS_ID,        
--      @DOC_TEXT,        
--      @PROCESS_TYPE ,  
--      @LETTER_SEQUENCE_NO        
--    )         
--   END        
-- ELSE  
--     BEGIN  
--   INSERT INTO CLM_PROCESS_LOG         
--     (        
--       CLAIM_ID,        
--       ACTIVITY_ID,        
--       PAYEE_ID,        
--       PROCESS_ID,        
--       DOC_TEXT,        
--       PROCESS_TYPE        
--     )          
--     VALUES                        
--     (        
--       @CLAIM_ID,        
--       @ACTIVITY_ID,        
--       @PAYEE_ID,        
--       @PROCESS_ID,        
--       @DOC_TEXT,        
--       @PROCESS_TYPE        
--     )         
-- END           
--END          

GO

