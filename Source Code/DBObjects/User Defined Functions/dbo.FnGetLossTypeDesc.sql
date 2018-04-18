IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetLossTypeDesc]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetLossTypeDesc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP FUNCTION dbo.FnGetLossTypeDesc
--go
CREATE FUNCTION dbo.FnGetLossTypeDesc
(
	@LOSS_TYPE AS VARCHAR(8000)
)
---'51','258','31','32','55','259','257'
RETURNS VARCHAR(8000)
	AS
BEGIN
	DECLARE @TEMP TABLE
	(
		IDENN_ROW_ID int identity(1,1),
		DESC_LOSS varchar(1000)
	)
	
	--Added as we were not able to get desc for all the values
	set @LOSS_TYPE =  @LOSS_TYPE + ','

	--*************
	DECLARE @ITEM varchar(8000)
	WHILE CHARINDEX(',',@LOSS_TYPE,0) <> 0
	BEGIN
		SELECT
		@Item=RTRIM(LTRIM(SUBSTRING(@LOSS_TYPE,1,CHARINDEX(',',@LOSS_TYPE,0)-1))),
					@LOSS_TYPE=RTRIM(LTRIM(SUBSTRING(@LOSS_TYPE,CHARINDEX(',',@LOSS_TYPE,0)+1,LEN(@LOSS_TYPE))))

		

		IF LEN(@Item) > 0
		begin
			INSERT INTO @TEMP 
			SELECT DETAIL_TYPE_DESCRIPTION FROM CLM_TYPE_DETAIL WITH(NOLOCK) 
			WHERE DETAIL_TYPE_ID = @Item
		end
		
	END	
	
--************	
	DECLARE @DESC VARCHAR(8000)
	SET @DESC = ''

	declare  @count int 
	SET @COUNT = 1
	while(1=1)
	BEGIN
		IF NOT EXISTS(SELECT IDENN_ROW_ID FROM @TEMP WHERE IDENN_ROW_ID =  @COUNT)
		BEGIN
			BREAK;
		END
		select @DESC = @DESC + ',' + DESC_LOSS from @TEMP where IDENN_ROW_ID = @count	
		

		set @count = @count + 1

	END
RETURN @DESC
END
--GO
--SELECT DBO.FnGetLossTypeDesc('51,258,31,32,55,259,257');
--rollback tran

--SELECT DETAIL_TYPE_DESCRIPTION FROM CLM_TYPE_DETAIL WITH(NOLOCK) 
--		WHERE DETAIL_TYPE_ID = 259



--
--------------
/*DECLARE @LOSS_TYPE  VARCHAR(8000)
DECLARE @ITEM varchar(8000)
SET @LOSS_TYPE = '51,258,31,32,55,259,257'
		
	DECLARE @TEMP TABLE
	(
		IDENN_ROW_ID int identity(1,1),
		DESC_LOSS varchar(1000)
	)
	
	WHILE CHARINDEX(',',@LOSS_TYPE,0) <> 0
	BEGIN
		SELECT
		@Item=RTRIM(LTRIM(SUBSTRING(@LOSS_TYPE,1,CHARINDEX(',',@LOSS_TYPE,0)-1))),
					@LOSS_TYPE=RTRIM(LTRIM(SUBSTRING(@LOSS_TYPE,CHARINDEX(',',@LOSS_TYPE,0)+1,LEN(@LOSS_TYPE))))

		IF LEN(@Item) > 0
		INSERT INTO @TEMP 
		SELECT DETAIL_TYPE_DESCRIPTION FROM CLM_TYPE_DETAIL WITH(NOLOCK) 
		WHERE DETAIL_TYPE_ID = @Item
	END	
SELECT * FROM @TEMP*/
--

GO

