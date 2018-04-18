


CREATE PROCEDURE [dbo].[Proc_SaveCurrentCountryProducts]
	@UserId [varchar](200)
WITH EXECUTE AS CALLER
AS
BEGIN
	DECLARE					@ModifiedBy				VARCHAR(300),
							@ModifiedDate			DateTime,
							@CountryCode			VARCHAR(500),
							@PrevProductCode		VARCHAR(1000),
							@PrevCountryCode		VARCHAR(500),
							@CurrentItemList		VARCHAR(3000),
							@CountryName			VARCHAR(1000),
							@LoginId				VARCHAR(300)
				
				SELECT @ModifiedBy=ModifiedBy,@ModifiedDate=ModifiedDate 
				FROM MNT_Users where UserId=@UserId
	
				SELECT @CountryCode=CountryCode
				FROM MNT_UserCountryProducts WHERE UserId = @UserId	
				
				SELECT	 @CountryName = CountryName
				FROM	 MNT_UserCountry
				WHERE	 CountryCode=@CountryCode
				
			    SET @CurrentItemList = ''
				DECLARE UserCountryProducts CURSOR FOR
				SELECT DISTINCT CountryCode 
				FROM MNT_UserCountryProducts WHERE UserId = @UserId		
				OPEN UserCountryProducts
				FETCH NEXT FROM UserCountryProducts
				INTO @PrevCountryCode
				WHILE @@FETCH_STATUS = 0
				BEGIN
					BEGIN
							SELECT	 @CountryName = CountryName
							FROM	 MNT_UserCountry
							WHERE	 CountryCode=@PrevCountryCode
						SET @CurrentItemList = @CurrentItemList  + @CountryName +' ' 
						DECLARE UserProducts CURSOR FOR
						SELECT CountryCode,ProductCode 
						FROM MNT_UserCountryProducts WHERE UserId = @UserId and CountryCode=@PrevCountryCode
						OPEN UserProducts
						FETCH NEXT FROM UserProducts
						INTO @CountryCode,@PrevProductCode 
						WHILE @@FETCH_STATUS = 0
						BEGIN														
							SET @CurrentItemList = @CurrentItemList  + @PrevProductCode +','
						FETCH NEXT FROM UserProducts
						INTO @CountryCode,@PrevProductCode
						END
						CLOSE UserProducts
						DEALLOCATE UserProducts
							
					END
					FETCH NEXT FROM UserCountryProducts
					INTO @PrevCountryCode
				END
				CLOSE UserCountryProducts
				DEALLOCATE UserCountryProducts
				
				UPDATE UserCountryProductLog
				SET CurrentCountry = @CurrentItemList,
					TranStatus	   = 'Y'
				WHERE UserId = @UserId and ModifiedBy=@ModifiedBy 
				and 
				Convert(datetime,Convert(varchar(10),ModifiedDate,103),103)=Convert(datetime,Convert(varchar(10),@ModifiedDate,103),103)
				AND isnull(TranStatus,'N')	   = 'N'

END


