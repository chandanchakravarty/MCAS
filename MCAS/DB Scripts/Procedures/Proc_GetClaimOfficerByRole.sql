IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimOfficerByRole]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimOfficerByRole]
GO

Create Procedure Proc_GetClaimOfficerByRole   
(  
 @ROLE varchar(5)
)  
As  

 IF(@ROLE='CO')  
  BEGIN  
    Select MNT_Users.SNo,MNT_Users.UserDispName 
    From MNT_Users inner join  MNT_GroupsMaster gp   
    on MNT_Users.GroupId=gp.GroupId   
    where (gp.RoleCode in ('CO','COSP'))
    order by UserDispName  
 
  END  
 ELSE IF(@ROLE='SP')  
  BEGIN  
    Select MNT_Users.SNo,MNT_Users.UserDispName
    From MNT_Users inner join  MNT_GroupsMaster gp   
    on MNT_Users.GroupId=gp.GroupId   
    where (gp.RoleCode in ('SP','COSP'))
     order by UserDispName  
  END  
 ELSE IF(@ROLE='ALL' or @ROLE='')  
  BEGIN  
    Select MNT_Users.SNo,MNT_Users.UserDispName  
    From MNT_Users inner join  MNT_GroupsMaster gp   
    on MNT_Users.GroupId=gp.GroupId   
     order by UserDispName  
  END  
 ELSE IF(@ROLE='NONE')  
  BEGIN  
    Select MNT_Users.SNo,MNT_Users.UserDispName 
    From MNT_Users inner join  MNT_GroupsMaster gp   
    on MNT_Users.GroupId=gp.GroupId   
    where (gp.RoleCode ='NON')
    order by UserDispName  
  END  



