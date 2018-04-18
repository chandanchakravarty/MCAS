IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_GL_ACCOUNT_RANGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_GL_ACCOUNT_RANGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : 	dbo.Proc_DeleteACT_GL_ACCOUNT_RANGES
Created by      : 	Ajit Singh Chahal
Date            : 	6/17/2005
Purpose         : 	To delete records of ACT_GL_ACCOUNT_RANGES
Revison History :
Used In         :  	Wolverine
-------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE proc Proc_DeleteACT_GL_ACCOUNT_RANGES
@CATEGORY_ID int
as
begin
declare @count int
select @count=count(*) from ACT_GL_ACCOUNTS where GROUP_TYPE = @CATEGORY_ID
if @count<=0
	begin
		delete from ACT_GL_ACCOUNT_RANGES where CATEGORY_ID = @CATEGORY_ID
	end
end





GO

