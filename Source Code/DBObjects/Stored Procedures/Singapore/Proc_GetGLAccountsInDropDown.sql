  
/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS  
Created by      : Ajit Singh Chahal  
Date            : 5/18/2005  
Purpose     : to parent a/cs in drop down  
Revison History :  
Modified by : Pravesh K Chandel   
Modified Date :16 Aug 2007  
Purpose  : Add more Condition to filter  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc dbo.Proc_GetGLAccountsInDropDown   
create  prOC [dbo].[Proc_GetGLAccountsInDropDown]   
(  
@GL_ID int,  
@accountTypeID int  
)  
as  
begin  
select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION,*  
  
from ACT_GL_ACCOUNTS AGA LEFT JOIN MNT_LOOKUP_VALUES AS MLV (NOLOCK)
ON AGA.ACC_LEVEL_TYPE =CAST(MLV.LOOKUP_UNIQUE_ID AS NCHAR) 
WHERE MLV.LOOKUP_UNIQUE_ID=14457 
and GL_ID=@GL_ID and ACC_TYPE_ID=@accountTypeID  and AGA.IS_ACTIVE = 'Y'  and ACC_RELATES_TO_TYPE=11201
  
order by ACC_DISP_NUMBER  
  
end  
  
  
  
  