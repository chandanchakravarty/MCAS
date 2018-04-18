
/****** Object:  StoredProcedure [dbo].[Proc_MNT_UserOrgAccessInsert]    Script Date: 12/08/2014 16:24:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_UserOrgAccessInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_UserOrgAccessInsert]
GO


/****** Object:  StoredProcedure [dbo].[Proc_MNT_UserOrgAccessInsert]    Script Date: 12/08/2014 16:24:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Proc_MNT_UserOrgAccessInsert]        
@p_UserId nvarchar(50),        
@p_OrgCode nvarchar(50),        
@p_OrgName nvarchar(100),        
@p_CreatedDate datetime,        
@p_CreatedBy nvarchar(50)      
AS        
SET FMTONLY OFF;    
     
    
BEGIN                
       
     Insert into MNT_UserOrgAccess (UserId,OrgCode,OrgName,CreatedDate,CreatedBy)        
 values(@p_UserId,@p_OrgCode,@p_OrgName,@p_CreatedDate,@p_CreatedBy)    
              
END 




GO





