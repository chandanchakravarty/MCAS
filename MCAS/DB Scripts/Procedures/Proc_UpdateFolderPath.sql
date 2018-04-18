
/****** Object:  StoredProcedure [dbo].[Proc_UpdateFolderPath]    Script Date: 02/13/2015 15:59:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateFolderPath]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateFolderPath]
GO



/****** Object:  StoredProcedure [dbo].[Proc_UpdateFolderPath]    Script Date: 02/13/2015 15:59:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Proc_UpdateFolderPath]
@FileRefNo varchar(50),
@Filepath varchar(50)
AS
SET FMTONLY OFF;
BEGIN  
update MNT_FileUpload set UploadPath=@Filepath where FileRefNo=@FileRefNo
END  
GO


