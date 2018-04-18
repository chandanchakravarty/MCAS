IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_getDriverName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_getDriverName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop FUNCTION dbo.fun_getDriverName
/* Function 
created By		: Pravesh K chandel
Created Date	: 5 Jan 09
Purpose			: To get a string driverName as in prior loss Table
*/
CREATE FUNCTION dbo.fun_getDriverName(@List Text) 
RETURNS nvarchar(50)
AS  
BEGIN 

Declare @Pos int
Declare @SPos int
Declare @COUNTER int
declare @Delimiter char(1)
set @Delimiter ='^'
declare @Table Table 
(
CUSTOMER_ID int,
APP_POL_ID INT,
VERSION_ID INT,
DRIVER_ID  INT
)

declare @driverName nvarchar(50)

Set @Pos = CharIndex(@Delimiter,@List,0)
Set @SPos = 1
SET @COUNTER=1
While @Pos > 0
Begin
    IF(@COUNTER=1)
		Insert Into @Table(CUSTOMER_ID) Values(Substring(@List,@SPos,@Pos-@SPos))
	ELSE IF(@COUNTER=2)
	UPDATE @Table SET APP_POL_ID= Substring(@List,@SPos,@Pos-@SPos)
	ELSE IF(@COUNTER=3)
	UPDATE @Table SET VERSION_ID= Substring(@List,@SPos,@Pos-@SPos)
	ELSE IF(@COUNTER=4)
	UPDATE @Table SET DRIVER_ID = Substring(@List,@SPos,@Pos-@SPos)

    Set @SPos = @Pos + 1
	Set @COUNTER = @COUNTER + 1
    Set @Pos = CharIndex(@Delimiter,@List,@Pos+1)
End

if (@List like '%APP%')
	select @driverName=DRIVER_FNAME + 
	case when isnull(DRIVER_MNAME,'')='' then ' ' else isnull(' ' + DRIVER_MNAME + ' ','' ) end
	+ isnull(DRIVER_LNAME ,'') from app_driver_details dd with(nolock)
	inner join @Table t on t.customer_id=dd.customer_id 
	and t.app_pol_id	=dd.app_id 
	and t.version_id	=dd.app_version_id
	and t.driver_id		=dd.driver_id
else
	select @driverName=DRIVER_FNAME + 
	case when isnull(DRIVER_MNAME,'')='' then ' ' else isnull(' ' + DRIVER_MNAME + ' ','' ) end	
	+ isnull(DRIVER_LNAME ,'') from pol_driver_details dd with(nolock)
	inner join @Table t on t.customer_id=dd.customer_id 
	and t.app_pol_id	=dd.policy_id 
	and t.version_id	=dd.policy_version_id
	and t.driver_id		=dd.driver_id

Return isnull(@driverName,'')

END




GO

