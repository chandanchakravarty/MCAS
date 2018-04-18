IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertAttachment    
Created by      : Vijay    
Date            : 23 Mar,2005    
Purpose         : To add record in Attachment table    
Revison History :    
Used In         :   wolvorine    
    
Modified By  : Vijay Arora    
Modified Date : 06-10-2005    
Purpose  : To add customer id, application id and     
   application version id.    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_InsertAttachment    
(    
@LOC    nvarchar(6)  ,    
@EntityID     nvarchar(100)  ,    
@FileName       nvarchar(100)  ,    
@AttachDate      datetime ,--nvarchar(100)  ,    
@UserId       nvarchar(70)  ,    
@FileDesc  nvarchar(255)  ,    
@PolicyId       nvarchar(10) ,    
@PolVarId       nvarchar(10)  ,    
@GenFileName     nvarchar(12)  ,    
@FileType  nvarchar(5)  ,    
@EntityType  varchar(25) ,    
@CustomerID  integer  ,    
@ApplicationID  integer  ,    
@ApplicationVerID integer  ,    
@AttachmentId  numeric output  ,  
@AttachType int  
)    
AS    
BEGIN    
    
 INSERT INTO MNT_ATTACHMENT_LIST(    
  ATTACH_LOC,    
  ATTACH_ENT_ID,    
  ATTACH_FILE_NAME,    
  ATTACH_DATE_TIME,    
  ATTACH_USER_ID,    
  ATTACH_FILE_DESC,    
  ATTACH_POLICY_ID,    
  ATTACH_POLICY_VER_TRACKING_ID,    
  ATTACH_GEN_FILE_NAME,    
  ATTACH_FILE_TYPE,    
  ATTACH_ENTITY_TYPE,    
  ATTACH_CUSTOMER_ID,    
  ATTACH_APP_ID,    
  ATTACH_APP_VER_ID,    
  ATTACH_TYPE,  
  IS_ACTIVE    
  )    
 VALUES(    
  @LOC,    
  @EntityID,    
  @FileName,    
  @AttachDate,    
  @UserID,    
  @FileDesc,    
  @PolicyId,    
  @PolVarId,    
  @GenFileName,    
  @FileType,    
  @EntityType,    
  @CustomerID,    
  @ApplicationID,    
  @ApplicationVerID,  
  @AttachType,   
  'Y'    
  )    
    
 SELECT @AttachmentId = Max(ATTACH_ID) FROM MNT_ATTACHMENT_LIST    
    
 UPDATE MNT_ATTACHMENT_LIST SET SOURCE_ATTACH_ID = @AttachmentId WHERE ATTACH_ID = @AttachmentId    
    
     
END    
    



GO

