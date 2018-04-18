CREATE PROCEDURE [dbo].[Proc_GetClaimOfficerByRole]
	@ROLE [varchar](5)
WITH EXECUTE AS CALLER
AS
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


