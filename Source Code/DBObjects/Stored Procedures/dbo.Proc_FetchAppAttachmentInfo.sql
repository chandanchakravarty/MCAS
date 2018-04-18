IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAppAttachmentInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAppAttachmentInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_FetchAppAttachmentInfo        
Created by      : kranti singh         
Date            : 13-06-2007        
Purpose      : fetch application attachments info        
Revison History :        
Used In   : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP proc dbo.Proc_FetchAppAttachmentInfo       
CREATE  proc dbo.Proc_FetchAppAttachmentInfo        
(        
@CUSTOMER_ID int,        
@APP_ID int,        
@APP_VERSION_ID int       
       
)        
AS       
select ATTACH_FILE_NAME,ATTACH_FILE_DESC,ATTACH_FILE_TYPE,ATTACH_TYPE from MNT_ATTACHMENT_LIST  
WHERE ATTACH_CUSTOMER_ID = @CUSTOMER_ID AND ATTACH_APP_ID = @APP_ID AND ATTACH_APP_VER_ID = @APP_VERSION_ID  and IS_ACTIVE ='Y'  
  


GO

