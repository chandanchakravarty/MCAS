USE [CDGI]
GO
/****** Object:  StoredProcedure [dbo].[Proc_MNTAttachment_Delete]    Script Date: 10/07/2014 13:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Proc_MNTAttachment_Delete]
  (
  @AttachId int
  )
  as
  BEGIN
  SET FMTONLY OFF; 
  Delete from MNT_AttachmentList  where AttachId = @AttachId 
  END