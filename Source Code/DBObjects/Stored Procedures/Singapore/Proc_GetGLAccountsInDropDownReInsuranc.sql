  
  
/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetGLAccountsInDropDownReInsurance  
Created by      : Pravesh K Chandel   
Date            : 11 sep 2007  
Purpose     : to parent a/cs in drop down in Reinsurance Module  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc dbo.Proc_GetGLAccountsInDropDownReInsurance   
create  prOC [dbo].[Proc_GetGLAccountsInDropDownReInsurance]   
(  
@GL_ID int,  
@accountTypeID int  
)  
as  
begin  
select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION,*  
  
from ACT_GL_ACCOUNTS   
  
where GL_ID=1 and ACC_TYPE_ID=1 and ACC_LEVEL_TYPE!='AS'  --Changed by kuldeep on14 feb 2012 erlier it was ACC_LEVEL_TYPE='AS' 
and ACC_RELATES_TO_TYPE = 11199 AND IS_ACTIVE = 'Y'  
order by ACC_DISP_NUMBER  
  
end  
  
  
  
  