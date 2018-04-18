IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetMessage]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetMessage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Function Name :  dbo.fun_GetMessage
CREATED BY      : Pravesh K Chandel         
DATE            : 19 July 2010    
PURPOSE         : TO get Message on basis of LAnguage
REVISON HISTORY :                                      
USED IN         : Ebix Advantage
*/
create function [dbo].[fun_GetMessage]
(
@Message_ID int,
@Lang_id smallint =1
) returns  nvarchar(4000)
as
begin
declare @message nvarchar(4000)
select @message = case when @Lang_id =1 then t1.TRANS_TYPE_DESC
	else isnull(t2.TRANS_TYPE_NAME,t1.TRANS_TYPE_DESC) end
from transactiontypelist t1 with(nolock)
left outer join transactiontypelist_multilingual t2 with(nolock)
on t1.trans_type_id=t2.trans_type_id and t2.lang_id=@Lang_id
where t1.trans_type_id=@Message_ID
return @message
end

 
GO

