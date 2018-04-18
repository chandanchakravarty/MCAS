IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetLookupDesc]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetLookupDesc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Function Name :  dbo.fun_GetLookupDesc
CREATED BY      : Pravesh K Chandel         
DATE            : 11 Dec 2010    
PURPOSE         : TO get LookupDesc on basis of LAnguage
REVISON HISTORY :                                      
USED IN         : Ebix Advantage
*/
create function [dbo].[fun_GetLookupDesc]
(
@LookUpUnique_ID int,
@Lang_id smallint =1
) returns  nvarchar(4000)
as
begin
declare @Desc nvarchar(4000)
select @Desc = case when @Lang_id =1 then t1.LOOKUP_VALUE_DESC
	else isnull(t2.LOOKUP_VALUE_DESC,t1.LOOKUP_VALUE_DESC) end
from MNT_LOOKUP_VALUES t1 with(nolock)
left outer join MNT_LOOKUP_VALUES_MULTILINGUAL t2 with(nolock)
on t1.LOOKUP_UNIQUE_ID=t2.LOOKUP_UNIQUE_ID and t2.lang_id=@Lang_id
where t1.LOOKUP_UNIQUE_ID =@LookUpUnique_ID
return @Desc
end

 

GO

