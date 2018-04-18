IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteMnt_OrgAcessUserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteMnt_OrgAcessUserId]
GO

CREATE procedure [dbo].[Proc_DeleteMnt_OrgAcessUserId]    
(    
@userID nvarchar(50)    
)     
as    
SET FMTONLY OFF;                  
  BEGIN                  
  delete from [MNT_UserOrgAccess] WHERE [UserId]=@userID
  END 

GO


