

/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_Delete]    Script Date: 09/17/2014 17:13:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_GroupPermission_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_GroupPermission_Delete]
GO



/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_Delete]    Script Date: 09/17/2014 17:13:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_Delete]    
@w_GroupId varchar(5),    
@w_MenuId int     
AS    
BEGIN    
SET FMTONLY OFF;    
IF EXISTS (SELECT * FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId)     
Begin   
DELETE FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId    
END    
   
End
GO


