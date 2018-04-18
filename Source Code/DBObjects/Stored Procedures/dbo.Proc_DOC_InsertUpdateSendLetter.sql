IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_InsertUpdateSendLetter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_InsertUpdateSendLetter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_DOC_InsertUpdateSendLetter    
Created by      : Deepak Gupta             
Date            : 09-15-2006                             
Purpose         : To Insert Or Update Merge Info    
Revison History :                    
Used In         : Wolverine                    
  
Reviewed By : Anurag Verma  
Reviewed On : 04-07-2007  

Reviewed By : Praveen kasana  
Reviewed On : 22-07-2009  
Purpose : Doc Mail 
                   
------------------------------------------------------------                    
Date      Review By           Comments                    
03-13-2007 Shailja Rampal      #1195. Changes done for Diary Item and Followup.    
------   ------------       -------------------------*/                    
--drop PROC DBO.Proc_DOC_InsertUpdateSendLetter           
CREATE PROC [dbo].[Proc_DOC_InsertUpdateSendLetter]    
(                    
 @MERGE_ID INT,    
 @TEMPLATE_ID INT,    
 @CLIENT_ID INT,    
 @APP_ID INT,    
 @APP_VERSION_ID INT,    
 @POLICY_ID INT ,    
 @POLICY_VERSION_ID INT ,    
 @USER_ID INT,    
 @MERGE_STATUS NVARCHAR(2),    
 @DIARY_ITEM_REQ CHAR(1) = '',    
 @FOLLOW_UP_DATE DATETIME = NULL,    
 @CHECK_ID INT = null,    
 @APPLICANT_ID INT =  null,    
 @HOLDER_ID INT =  null,
 @DIARY_ITEM_TO int = null    ,
 @CLAIM_ID int = null,
 @PARTY_ID int = null
)                    
AS    
DECLARE @NEWMERGEID NUMERIC    
BEGIN            
        
 --IF @MERGE_STATUS='S'    
  --DELETE FROM DOC_SEND_LETTER WHERE USER_ID=@USER_ID AND MERGE_STATUS='S' AND CONVERT(DATETIME,MERGE_DATE,101) <= CONVERT(DATETIME,GETDATE(),101)-5    
     
 IF @MERGE_ID=-1    
 BEGIN    
  SELECT @NEWMERGEID=ISNULL(MAX(MERGE_ID),0)+1 FROM DOC_SEND_LETTER    
  INSERT INTO DOC_SEND_LETTER (MERGE_ID,    
      TEMPLATE_ID,    
      CLIENT_ID,    
      APP_ID,    
      APP_VERSION_ID,    
      POLICY_ID,    
      POLICY_VERSION_ID,    
      PRINT_LBL_ENVP,    
      USER_ID,    
      MERGE_STATUS,    
      MERGE_DATE,    
      DIARY_ITEM_REQ,    
      FOLLOW_UP_DATE,    
      CHECK_ID,    
      APPLICANT_ID,    
      HOLDER_ID,
	  DIARY_ITEM_TO,
	  CLAIM_ID,
	  PARTY_ID)     
     VALUES    
      (@NEWMERGEID,    
      @TEMPLATE_ID,    
      @CLIENT_ID,    
      @APP_ID,    
      @APP_VERSION_ID,    
      @POLICY_ID,    
      @POLICY_VERSION_ID,    
      'N',    
      @USER_ID,    
      @MERGE_STATUS,    
      GETDATE(),    
      @DIARY_ITEM_REQ,    
      @FOLLOW_UP_DATE,    
      @CHECK_ID,    
      @APPLICANT_ID,    
      @HOLDER_ID,@DIARY_ITEM_TO,
	  @CLAIM_ID,
	  @PARTY_ID	)     
 END    
 ELSE    
 BEGIN    
  UPDATE DOC_SEND_LETTER SET  TEMPLATE_ID   = @TEMPLATE_ID,    
      CLIENT_ID   = @CLIENT_ID,    
      APP_ID       = @APP_ID,    
      APP_VERSION_ID  = @APP_VERSION_ID,    
      POLICY_ID     = @POLICY_ID,    
      POLICY_VERSION_ID   = @POLICY_VERSION_ID,    
      PRINT_LBL_ENVP  = 'N',    
      USER_ID    = @USER_ID,    
      MERGE_DATE   = GetDate(),    
      DIARY_ITEM_REQ  = @DIARY_ITEM_REQ,    
      FOLLOW_UP_DATE  = @FOLLOW_UP_DATE,    
      CHECK_ID  = @CHECK_ID,    
      APPLICANT_ID  = @APPLICANT_ID,    
      HOLDER_ID  = @HOLDER_ID,    
      DIARY_ITEM_TO = @DIARY_ITEM_TO,
	  CLAIM_ID = @CLAIM_ID,
	  PARTY_ID = @PARTY_ID	
      WHERE MERGE_ID = @MERGE_ID;    
    
  SELECT @NEWMERGEID=@MERGE_ID;    
 END    
 SELECT @NEWMERGEID;    
END    






GO

