IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetActivityCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetActivityCodes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetActivityCodes                  
Created by      : Sumit Chhabra    
Date            : 09/02/2006                  
Purpose     : To fetch transaction codes (action on payment) and corresponding activity reason for various activities    
Revison History :                  
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--DROP PROC dbo.Proc_GetActivityCodes                  
CREATE PROC [dbo].[Proc_GetActivityCodes]     
(    
  @RECORD_STATUS VARCHAR(20)=null  ,  
  @LANG_ID INT =1  
)                 
AS                  
BEGIN                  
  IF(@RECORD_STATUS = 'NEW')              
    BEGIN    
    
    SELECT ISNULL(D.TYPE_DESC, C.DETAIL_TYPE_DESCRIPTION )  AS TransCodeDesc ,  
           CAST(ISNULL(C.TRANSACTION_CODE,'') AS VARCHAR) + '^' + CAST(ISNULL(C.DETAIL_TYPE_ID,'') AS VARCHAR) AS TransCodeId  
    FROM   CLM_TYPE_DETAIL C  WITH(NOLOCK)LEFT OUTER JOIN  
           CLM_TYPE_DETAIL_MULTILINGUAL  D  WITH(NOLOCK)  ON C.DETAIL_TYPE_ID=D.DETAIL_TYPE_ID AND D.LANG_ID= @LANG_ID
    WHERE  (C.TYPE_ID=8 AND (C.ALLOW_MANUAL = 'Y' OR C.ALLOW_MANUAL IS NULL) AND C.IS_ACTIVE='Y' AND C.IS_SYSTEM_GENERATED='N')  
     
     END    
   ELSE    
     BEGIN       
      SELECT ISNULL(D.TYPE_DESC, C.DETAIL_TYPE_DESCRIPTION )  AS TransCodeDesc ,  
             CAST(ISNULL(C.TRANSACTION_CODE,'') AS VARCHAR) + '^' + CAST(ISNULL(C.DETAIL_TYPE_ID,'') AS VARCHAR) AS TransCodeId  
	  FROM   CLM_TYPE_DETAIL C WITH(NOLOCK) LEFT OUTER JOIN  
			 CLM_TYPE_DETAIL_MULTILINGUAL  D WITH(NOLOCK) ON C.DETAIL_TYPE_ID=D.DETAIL_TYPE_ID AND D.LANG_ID=@LANG_ID  
      WHERE   (C.TYPE_ID=8 AND C.IS_ACTIVE='Y')  
     
     END      
END                           
    
    
    
    
    
    
    
    
GO

