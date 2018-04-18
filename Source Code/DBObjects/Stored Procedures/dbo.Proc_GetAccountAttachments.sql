IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccountAttachments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccountAttachments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*--Created by : ajit singh chahal  
Purpose to fill drop down of attachments of cash accounts  */
-- drop proc dbo.Proc_GetAccountAttachments
CREATE proc dbo.Proc_GetAccountAttachments  
(  
@ACCOUNT_ID int  
)as  
begin  
	/*select  ATTACH_FILE_DESC,ATTACH_ID from MNT_ATTACHMENT_LIST where ATTACH_ENTITY_TYPE='ACT_BANK_INFORMATION'  
	and ATTACH_ENT_ID = @ACCOUNT_ID  */
	SELECT SIGN_FILE_1,SIGN_FILE_2 FROM ACT_BANK_INFORMATION WHERE ACCOUNT_ID = @ACCOUNT_ID
end  


GO

