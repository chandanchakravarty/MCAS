IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLProcCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLProcCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
--BEGIN TRAN      
--DROP PROC dbo.Proc_GetXMLProcCLM_ACTIVITY      
--GO      
/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetXMLProcCLM_ACTIVITY                        
Created by      : Vijay Arora                        
Date            : 5/24/2006                        
Purpose     : To get a record in table named CLM_ACTIVITY                        
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/           
-- DROP PROC dbo.Proc_GetXMLProcCLM_ACTIVITY                    
CREATE PROC [dbo].[Proc_GetXMLProcCLM_ACTIVITY]                        
(                        
 @CLAIM_ID     int,                        
 @ACTIVITY_ID  int                        
)                        
AS                        
BEGIN                  
DECLARE @NEW_RESERVE INT              
DECLARE @NEW_RESERVE_DETAIL_TYPE_ID INT    
DECLARE @ACTION_ON_PAYMENT INT      
            
SET @NEW_RESERVE = 11773                  
SET @NEW_RESERVE_DETAIL_TYPE_ID = 165          
            
 IF (@ACTIVITY_ID=0)       
 BEGIN           
  SELECT CLAIM_ID, ACTIVITY_ID,  CONVERT(VARCHAR(10),ACTIVITY_DATE,101) AS ACTIVITY_DATE,             
  CAST(ISNULL(ACTIVITY_REASON,'') AS VARCHAR) + '^' + CAST(ISNULL(ACTION_ON_PAYMENT,'') AS VARCHAR) AS ACTIVITY_REASON,            
  REASON_DESCRIPTION, ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS USERNAME,                    
  RESERVE_AMOUNT,PAYMENT_AMOUNT,PAYEE_PARTIES_ID,EXPENSES,RECOVERY,ACTIVITY_STATUS,RESERVE_TRAN_CODE,            
  ISNULL(C.IS_ACTIVE,'N') AS IS_ACTIVE, ISNULL([DESCRIPTION],'') AS [DESCRIPTION],C.TEXT_ID,C.TEXT_DESCRIPTION      
  FROM  CLM_ACTIVITY C WITH(NOLOCK)                      
  LEFT JOIN   
 MNT_USER_LIST U WITH(NOLOCK) ON (U.[USER_ID] = C.CREATED_BY)      
              
  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_REASON=@NEW_RESERVE AND ACTION_ON_PAYMENT=@NEW_RESERVE_DETAIL_TYPE_ID          
 END      
 ELSE      
 BEGIN            
 SELECT CLAIM_ID, ACTIVITY_ID,  CONVERT(VARCHAR(10),ACTIVITY_DATE,101) AS ACTIVITY_DATE,             
  CAST(ISNULL(ACTIVITY_REASON,'') AS VARCHAR) + '^' + CAST(ISNULL(ACTION_ON_PAYMENT,'') AS VARCHAR) AS ACTIVITY_REASON,            
  REASON_DESCRIPTION, ISNULL(U.USER_TITLE,'') + ' ' + ISNULL(U.USER_FNAME,'') + ' ' + ISNULL(U.USER_LNAME,'') AS USERNAME,                    
  RESERVE_AMOUNT,PAYMENT_AMOUNT,PAYEE_PARTIES_ID,EXPENSES,RECOVERY,ACTIVITY_STATUS,RESERVE_TRAN_CODE,            
  ISNULL(C.IS_ACTIVE,'N') AS IS_ACTIVE, ISNULL([DESCRIPTION],'') AS [DESCRIPTION],      
  ESTIMATION_RECOVERY, ESTIMATION_EXPENSES,CHECK_ID, --Added for Itrack Issue 7169 -- To use check id for finding is check cleared(IS_BNK_RECONCILED) FROM ACT_CHECK_INFORMATION      
  ACCOUNTING_SUPPRESSED,ISNULL(IS_VOIDED_REVERSED_ACTIVITY,0) AS IS_VOIDED_REVERSED_ACTIVITY, --Added for Itrack Issue 7169 on 11 May 2010 -- To use for visibilty of Void & Reverse buttom      
  EXPENSE_REINSURANCE_RECOVERED,LOSS_REINSURANCE_RECOVERED ,C.COI_TRAN_TYPE ,C.TEXT_ID,C.TEXT_DESCRIPTION  --Added for Itrack Issue 6932 on 5 July 2010      
  FROM  CLM_ACTIVITY C                       
  LEFT JOIN MNT_USER_LIST U ON U.[USER_ID] = C.CREATED_BY                      
  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                        
 END      
      
SELECT TOP 1 ISNULL(CLAIM_RESERVE_AMOUNT,0)+ ISNULL(CLAIM_PAYMENT_AMOUNT,0) AS CLAIM_RESERVE_AMOUNT,      
ISNULL(CLAIM_RI_RESERVE,0) AS CLAIM_RI_RESERVE      
FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID< @ACTIVITY_ID AND IS_ACTIVE ='Y'      
ORDER BY ACTIVITY_ID DESC      
      
SELECT ESTIMATION_RECOVERY, ESTIMATION_EXPENSES      
FROM  CLM_ACTIVITY      
WHERE CLAIM_ID = @CLAIM_ID AND ACTION_ON_PAYMENT=@NEW_RESERVE_DETAIL_TYPE_ID



 --============= Added by Shubhanshu for Itrack 1356 (tfs # 870)(To adding Claim Receipt Amount into hist text) on 12/07/2011.
SELECT @ACTION_ON_PAYMENT = ACTION_ON_PAYMENT 
FROM CLM_ACTIVITY WITH(NOLOCK)
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID

 SELECT ISNULL(CASE WHEN @ACTION_ON_PAYMENT IN(180,181) THEN SUM(PAYMENT_AMOUNT) ELSE SUM(RECOVERY_AMOUNT) END,0)  AS AMOUNT        
 FROM  CLM_ACTIVITY_RESERVE WITH(NOLOCK)
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID       
END                        
                      
--GO      
--EXEC Proc_GetXMLProcCLM_ACTIVITY 2668,2      
--ROLLBACK TRAN 
GO

