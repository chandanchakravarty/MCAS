

/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupsMaster_Save]    Script Date: 09/01/2014 10:40:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_GroupsMaster_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_GroupsMaster_Save]
GO



/****** Object:  StoredProcedure [dbo].[Proc_MNT_GroupsMaster_Save]    Script Date: 09/01/2014 10:40:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 CREATE PROCEDURE [dbo].[Proc_MNT_GroupsMaster_Save]
@p_GroupCode varchar(5),
@p_GroupName varchar(100),
@p_DeptCode varchar(10),
@p_AccessLevel smallint,
@p_IsActive varchar(1),
@p_CreatedBy varchar(20),
@RoleCode varchar(5)  
AS
BEGIN
SET FMTONLY OFF;
Insert into MNT_GroupsMaster (GroupCode,GroupName,DeptCode,AccessLevel,IsActive,CreatedBy,CreatedDate,RoleCode)  
 values(@p_GroupCode,@p_GroupName,@p_DeptCode,@p_AccessLevel,@p_IsActive,@p_CreatedBy,GETDATE(),@RoleCode) 
END

GO


