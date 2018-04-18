CREATE PROCEDURE [dbo].[Proc_MNT_GroupsMaster_Update]
	@p_GroupCode [varchar](5),
	@p_GroupName [varchar](100),
	@p_DeptCode [varchar](10),
	@p_AccessLevel [smallint],
	@p_IsActive [varchar](1),
	@p_ModifiedBy [varchar](20),
	@w_GroupId [varchar](5)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
IF EXISTS (SELECT * FROM MNT_GroupsMaster WHERE GroupId=@w_GroupId) 
Begin  
UPDATE [dbo].MNT_GroupsMaster SET
 GroupCode=@p_GroupCode,
 GroupName=@p_GroupName,
 DeptCode=@p_DeptCode,
 AccessLevel=@p_AccessLevel, 
 IsActive=@p_IsActive,
 ModifiedBy=getdate()  
 WHERE GroupId=@w_GroupId 
END  
else  
Begin  
Insert into MNT_GroupsMaster (GroupCode,GroupName,DeptCode,AccessLevel,IsActive,ModifiedBy,CreatedDate)  
 values(@p_GroupCode,@p_GroupName,@p_DeptCode,@p_AccessLevel,@p_IsActive,@p_ModifiedBy,GETDATE())    
END  
End


