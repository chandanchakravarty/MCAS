IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : Dbo.Proc_UpdateAttachment        
Created by      : Sumit Chhabra    
Date            : 24 Jan,2006      
Purpose         : To update record in Attachment table      
Revison History :      
Used In         :   wolvorine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/   
-- drop proc dbo.Proc_UpdateAttachment      
CREATE PROC dbo.Proc_UpdateAttachment      
(      
@ATTACH_ID int,    
@ATTACH_FILE_DESC varchar(200)  ,  
@ATTACH_TYPE int  
)      
AS      
BEGIN      
      
 UPDATE MNT_ATTACHMENT_LIST   
 SET ATTACH_FILE_DESC=@ATTACH_FILE_DESC,  
     ATTACH_TYPE = @ATTACH_TYPE,
    ATTACH_DATE_TIME = getdate() 
 WHERE ATTACH_ID=@ATTACH_ID            
END      
  
  
  




GO

