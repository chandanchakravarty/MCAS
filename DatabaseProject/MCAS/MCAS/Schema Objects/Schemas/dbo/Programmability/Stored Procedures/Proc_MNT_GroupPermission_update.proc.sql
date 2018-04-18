CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_update]
	@p_MenuId [int],
	@p_Read [bit],
	@p_Write [bit],
	@p_Delete [bit],
	@p_SplPermission [bit],
	@w_GroupId [varchar](5),
	@w_MenuId [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
IF EXISTS (SELECT * FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId)   
Begin  
UPDATE [dbo].MNT_GroupPermission SET MenuId=@p_MenuId,[Read]=@p_Read,[Write]=@p_Write,[Delete]=@p_Delete,SplPermission=@p_SplPermission   
 WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId  
END  
else 
Begin  
INSERT INTO [MNT_GroupPermission] ([GroupId] ,[MenuId] ,[Status] ,[RowId] ,[Read] ,[Write] ,[Delete] ,[SplPermission]) VALUES(@w_GroupId,@w_MenuId,'Y',0,@p_Read,@p_Write,@p_Delete,@p_SplPermission)  
END  
End


