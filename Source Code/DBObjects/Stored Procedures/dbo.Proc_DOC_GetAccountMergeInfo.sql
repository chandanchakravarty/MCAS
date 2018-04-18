IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetAccountMergeInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetAccountMergeInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_DOC_GetAccountMergeInfo    
Created by      : Praveen kasana             
Date            : 31 May 2007                            
Purpose         : To Get Checks Related to General and Claim Account :Check printed Yes    
Revison History :                    
Used In         : Wolverine                    
                   
------------------------------------------------------------                    
------   ------------       -------------------------*/                    
    
CREATE PROC DBO.Proc_DOC_GetAccountMergeInfo    
(                    
 @CHECK_FROM_DATE DATETIME,    
 @CHECK_TO_DATE DATETIME,    
 @CHECK_NO_FROM VARCHAR(20),    
 @CHECK_NO_TO VARCHAR(20)    
)                    
AS          
    
BEGIN                    
     
   
IF(@CHECK_NO_FROM = '' OR @CHECK_NO_TO = '' )  
BEGIN  
 SELECT CHECK_ID FROM ACT_CHECK_INFORMATION WITH(NOLOCK)       
 WHERE  CHECK_DATE BETWEEN @CHECK_FROM_DATE AND     
 @CHECK_TO_DATE
 AND ISNULL(IS_BNK_RECONCILED,'N')='N' AND  ISNULL(IS_PRINTED,'N') = 'Y'
END  
ELSE IF (@CHECK_FROM_DATE = '' OR @CHECK_TO_DATE='')  
BEGIN  
 SELECT CHECK_ID FROM ACT_CHECK_INFORMATION WITH(NOLOCK)    
 WHERE CHECK_NUMBER BETWEEN @CHECK_NO_FROM AND @CHECK_NO_TO    
 AND ISNULL(IS_BNK_RECONCILED,'N')='N' AND  ISNULL(IS_PRINTED,'N') = 'Y'
END  
ELSE  
SELECT CHECK_ID FROM ACT_CHECK_INFORMATION     
 WHERE  CHECK_DATE BETWEEN @CHECK_FROM_DATE AND     
 @CHECK_TO_DATE AND CHECK_NUMBER BETWEEN @CHECK_NO_FROM AND @CHECK_NO_TO    
 AND ISNULL(IS_BNK_RECONCILED,'N')='N' AND  ISNULL(IS_PRINTED,'N') = 'Y'
END     


        
    
    
    
    
    
    
  



GO

