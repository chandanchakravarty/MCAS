IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_validateLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_validateLogin]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*
CREATED BY 		:	ANURAG VERMA
CREATED ON 		:	28/02/2005
PURPOSE		:	VALIDATE USER LOGIN
DEPENDENCIES
	TABLE		:	TEMPUSER

INPUT			:	@SYSID,@USERID,@PASSWORD
OUTPUT		:	


*/


CREATE PROCEDURE dbo.sp_validateLogin
	@sysid varchar(50),
	@username varchar(50),
	@password varchar(50),
	@result varchar(50) output
AS
 

BEGIN 
SET NOCOUNT ON
declare @lsysid varchar(50)
declare @lusername varchar(50)
declare @lpassword varchar(50)
declare @lsresult int
declare @luresult int
declare @lpresult int

set @result=''

select @lsysid=systemid from tempUser where systemID=@sysid
set @lsysid=isnull(@lsysid,'')
if @lsysid=''
begin
	set @lsresult=100
end

select @lusername=username  from tempUser where username=@username
set @lusername=isnull(@lusername,'')
if @lusername=''
begin
	set @luresult=200
end

select @lpassword=password  from tempUser where password=@password
set @lpassword=isnull(@lpassword,'')
if @lpassword=''
begin
	set @lpresult=300
end

if @lsresult=100
set	@result = @lsresult

if @luresult=200
set @result= @result + '~' + convert(varchar(50),@luresult)

if @lpresult=300
set   @result= @result + '~' + convert(varchar(50),@lpresult)

if left(@result,1)='~'
set @result=substring(@result,2,len(@result))

if @result=''
	select userid,username,systemid,imgfolder,colorscheme from tempUser where systemId=@sysid  and username=@username and password=@password
return 1

SET NOCOUNT OFF
END


GO

