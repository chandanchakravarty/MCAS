IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProperCaseName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetProperCaseName]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------      
Proc Name     : dbo.GetProperCaseName      
Created by      : Anurag Verma      
Date                  : 29/03/2005      
Purpose         : To get user name in proper case
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      

CREATE FUNCTION GetProperCaseName
(
@Title varchar(50),
@Fname varchar(50),
@Lname varchar(50)
)  
RETURNS varchar(250)  AS  
BEGIN 
DECLARE @PROPERNAME VARCHAR(250)	
	if (@Title <> '')
		begin
			Set @PROPERNAME= upper(left(@Title,1) ) + lower(right(@Title,len(@title)-1)) + ' '
		end
	else
		begin
			Set @PROPERNAME= ''
		end

	if (@Fname <> '')
		begin
			Set @PROPERNAME=@PROPERNAME + upper(left(@Fname,1) ) + lower(right(@Fname,len(@Fname)-1)) + ' '
		end
	else
		begin
			Set @PROPERNAME= @PROPERNAME + ''
		end

	--select @propername		
	if (@Lname <> '')
		begin
			Set @PROPERNAME=@PROPERNAME + '' + upper(left(@Lname,1) ) + lower(right(@Lname,len(@Lname)-1))
		end
	else
		begin
			Set @PROPERNAME= @PROPERNAME + ''
		end
RETURN @propername
END





GO

