IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_GetTemplateMergeInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_GetTemplateMergeInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_DOC_GetTemplateMergeInfo        
Created by      : Deepak Gupta                 
Date            : 08-29-2006                                 
Purpose         : To Get Information Related to Send Letter        
Revison History :                        
Used In         : Wolverine                        
                       
------------------------------------------------------------                        
Date      Review By           Comments                        
03-13-2007 Shailja Rampal      #1195. Changes done for Diary Item and Followup.        
        
------   ------------       -------------------------*/                        
         
CREATE PROC [dbo].[Proc_DOC_GetTemplateMergeInfo]        
(                        
 @MERGEID     varchar(500)        
)                        
AS              
DECLARE @strSQL VARCHAR(2000)        
BEGIN                        
         
 SELECT @strSQL = 'SELECT         
    SL.TEMPLATE_ID,        
    SL.MERGE_ID,        
    TL.DISPLAYNAME,        
       SL.CLIENT_ID,        
           SL.APP_ID,        
           SL.APP_VERSION_ID,        
    SL.POLICY_ID,        
    SL.POLICY_VERSION_ID,        
    SL.PRINT_LBL_ENVP,        
    SL.LBL_ENVP_ID,        
    SL.PRINT_ADDRESS,        
    SL.RETURN_ADDRESS,        
    SL.USER_ID,        
    SL.DIARY_ITEM_REQ,        
    SL.FOLLOW_UP_DATE,      
    SL.CHECK_ID ,    
 SL.APPLICANT_ID,    
 SL.CLAIM_ID,    
 SL.PARTY_ID,
 SL.DIARY_ITEM_TO,  
 TL.LETTERTYPE        
   FROM DOC_SEND_LETTER SL        
   INNER JOIN DOC_TEMPLATE_LIST TL ON SL.TEMPLATE_ID=TL.TEMPLATE_ID '        
 if @MERGEID <> 'ALL'        
  SELECT @strSQL = @strSQL + ' WHERE MERGE_ID IN (' + @MERGEID + ')'        
        
 exec (@strSQL)        
END
GO

