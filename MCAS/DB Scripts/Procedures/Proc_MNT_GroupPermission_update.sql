

/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_update]    Script Date: 09/01/2014 10:41:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_GroupPermission_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_GroupPermission_update]
GO



/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_update]    Script Date: 09/01/2014 10:41:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_update]
@p_MenuId int,
@p_Read bit,
@p_Write bit,
@p_Delete bit,
@p_SplPermission bit ,
@w_GroupId varchar(5),
@w_MenuId int 
AS
BEGIN
SET FMTONLY OFF;
IF EXISTS (SELECT * FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId)     Begin
UPDATE [dbo].MNT_GroupPermission SET MenuId=@p_MenuId,[Read]=@p_Read,[Write]=@p_Write,[Delete]=@p_Delete,SplPermission=@p_SplPermission 
	WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId
END
else
Begin
INSERT INTO [MNT_GroupPermission] ([GroupId] ,[MenuId] ,[Status] ,[RowId] ,[Read] ,[Write] ,[Delete] ,[SplPermission]) VALUES(@w_GroupId,@w_MenuId,'Y',0,@p_Read,@p_Write,@p_Delete,@p_SplPermission)
END
End
GO


