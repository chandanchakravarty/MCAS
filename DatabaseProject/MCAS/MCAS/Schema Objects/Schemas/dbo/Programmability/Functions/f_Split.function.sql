CREATE FUNCTION [dbo].[f_Split](@SplitText [varchar](1000), @SplitChar [char](1))
RETURNS @SplitList TABLE (
	[Item] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) WITH EXECUTE AS CALLER
AS 
BEGIN
DECLARE 
	@tmpFld  VARCHAR(1000),
	@tmpSelFlds VARCHAR(1000)
	SELECT @SplitText = @SplitText + @SplitChar
	SELECT @tmpFld = @SplitText
	IF(CHARINDEX(@SplitChar,@SplitText) >0)
	begin
		while(len(@tmpFld)>0)
		begin 
			select @tmpSelFlds=substring(@tmpFld,1,charindex(@SplitChar,@tmpFld)-1) 
			select @tmpFld=substring(@tmpFld,charindex(@SplitChar,@tmpFld)+1,len(@tmpFld)) 
			INSERT INTO @SplitList VALUES (@tmpSelFlds )
		END
	END
	RETURN
END


