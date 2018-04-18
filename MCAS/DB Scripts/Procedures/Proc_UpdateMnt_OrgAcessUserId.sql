IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMnt_OrgAcessUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMnt_OrgAcessUserId]
GO

CREATE procedure [dbo].[Proc_UpdateMnt_OrgAcessUserId]    
(    
@OlduserID nvarchar(50),    
@NewuserID nvarchar(50)    
)     
as    
SET FMTONLY OFF;                  
  BEGIN                  
  UPDATE [MNT_UserOrgAccess] SET [UserId] = @NewuserID WHERE [UserId]=@OlduserID
  END 
GO


