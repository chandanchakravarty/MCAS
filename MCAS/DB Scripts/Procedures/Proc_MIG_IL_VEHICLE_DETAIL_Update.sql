

/****** Object:  StoredProcedure [dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]    Script Date: 08/28/2014 11:22:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]
GO


/****** Object:  StoredProcedure [dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]    Script Date: 08/28/2014 11:22:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]
@w_FileName nvarchar(200) 
AS
BEGIN
SET FMTONLY OFF;
	UPDATE [dbo].MIG_IL_VEHICLE_DETAIL SET UPLOAD_FILE_ID=(select UploadFileId from MNT_VehicleListingUpload where UploadFileName = @w_FileName
) 
	WHERE FileName=@w_FileName
END



GO


