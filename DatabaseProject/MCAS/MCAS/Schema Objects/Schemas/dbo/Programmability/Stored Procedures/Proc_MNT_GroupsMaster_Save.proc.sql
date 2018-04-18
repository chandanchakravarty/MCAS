CREATE PROCEDURE [dbo].[Proc_MNT_GroupsMaster_Save]
	@p_GroupCode [varchar](5),
	@p_GroupName [varchar](100),
	@p_DeptCode [varchar](10),
	@p_AccessLevel [smallint],
	@p_IsActive [varchar](1),
	@p_CreatedBy [varchar](20),
	@RoleCode [varchar](5)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
Insert into MNT_GroupsMaster (GroupCode,GroupName,DeptCode,AccessLevel,IsActive,CreatedBy,CreatedDate,RoleCode)  
 values(@p_GroupCode,@p_GroupName,@p_DeptCode,@p_AccessLevel,@p_IsActive,@p_CreatedBy,GETDATE(),@RoleCode) 
END


