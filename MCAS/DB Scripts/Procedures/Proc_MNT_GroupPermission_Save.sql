

/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_Save]    Script Date: 09/01/2014 10:40:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_GroupPermission_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_GroupPermission_Save]
GO



/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupPermission_Save]    Script Date: 09/01/2014 10:40:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_Save]  
@p_GroupId varchar(5),  
@p_MenuId int,  
@p_Status varchar(1),  
@p_RowId int,  
@p_Read bit,  
@p_Write bit,  
@p_Delete bit,  
@p_SplPermission bit    
AS  
BEGIN  
SET FMTONLY OFF;  
Insert into MNT_GroupPermission (GroupId,MenuId,[Status],[RowId],[Read],[Write],[Delete],SplPermission)  
 values(@p_GroupId,@p_MenuId,@p_Status,@p_RowId,@p_Read,@p_Write,@p_Delete,@p_SplPermission)  
END  

GO


