CREATE PROCEDURE [dbo].[Proc_MNT_UserOrgAccessInsert]
	@p_UserId [nvarchar](50),
	@p_OrgCode [nvarchar](50),
	@p_OrgName [nvarchar](100),
	@p_CreatedDate [datetime],
	@p_CreatedBy [nvarchar](50),
    @p_ModifiedBy  [nvarchar](50)=NULL
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
   
  
BEGIN              
     
	 Insert into MNT_UserOrgAccess (UserId,OrgCode,OrgName,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate)          
     values(@p_UserId,@p_OrgCode,@p_OrgName,@p_CreatedDate,@p_CreatedBy,@p_ModifiedBy,GETDATE())
            
END


