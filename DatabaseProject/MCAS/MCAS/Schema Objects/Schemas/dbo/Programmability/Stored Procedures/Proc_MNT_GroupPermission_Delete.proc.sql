CREATE PROCEDURE [dbo].[Proc_MNT_GroupPermission_Delete]
	@w_GroupId [varchar](5),
	@w_MenuId [int]
WITH EXECUTE AS CALLER
AS
BEGIN    
SET FMTONLY OFF;    
IF EXISTS (SELECT * FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId)     
Begin   
DELETE FROM MNT_GroupPermission WHERE GroupId=@w_GroupId AND MenuId=@w_MenuId    

END    
   
End


