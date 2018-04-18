CREATE PROCEDURE [dbo].[Proc_InsertUserCountryProduct]
	@UserId [varchar](20),
	@CountryCode [varchar](6),
	@SelectedList [varchar](1000),
	@IsFirstRow [char](1)
WITH EXECUTE AS CALLER
AS
BEGIN

		SELECT * INTO #TmpTab FROM dbo.f_Split(@SelectedList,',')       

		IF @IsFirstRow ='Y'
			BEGIN	
				DELETE FROM MNT_UserCountryProducts WHERE UserId = @UserId
			END
		
		
		
		
				
	
		INSERT INTO MNT_UserCountryProducts
		SELECT @UserId,@CountryCode,Item FROM #TmpTab
			
		
END


