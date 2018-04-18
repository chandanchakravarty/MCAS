CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_Save]
	@p_GroupId [varchar](5),
	@p_MenuId [int],
	@p_Status [varchar](1),
	@p_RowId [int],
	@p_Read [bit],
	@p_Write [bit],
	@p_Delete [bit],
	@p_SplPermission [bit]
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
Insert into MNT_GroupPermission (GroupId,MenuId,[Status],[RowId],[Read],[Write],[Delete],SplPermission)  
 values(@p_GroupId,@p_MenuId,@p_Status,@p_RowId,@p_Read,@p_Write,@p_Delete,@p_SplPermission)  
END


