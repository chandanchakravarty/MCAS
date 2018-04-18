  
  /*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS  
Created by      : Ajit Singh Chahal  
Date            : 5/18/2005  
Purpose     : to parent a/cs in drop down  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
drop proc Proc_GetParentAcsGroups 
------   ------------       -------------------------*/  
create   prOC Dbo.Proc_GetParentAcsGroups  
as  
begin  
select ACCOUNT_ID,ACC_TYPE_ID   
from ACT_GL_ACCOUNTS   
--where ACC_PARENT_ID is null and ACC_LEVEL_TYPE='AS'  
where ACC_PARENT_ID is null and  ACC_LEVEL_TYPE=cast(14457 as NCHAR) 
end  
  
  
  
  
  
  
  
    
    
    
  
  
  
  
  
  
  