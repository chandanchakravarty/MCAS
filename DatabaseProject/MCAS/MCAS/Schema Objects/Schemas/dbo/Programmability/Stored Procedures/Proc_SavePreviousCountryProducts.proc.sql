CREATE PROCEDURE [dbo].[Proc_SavePreviousCountryProducts]
	@UserId [varchar](200)
WITH EXECUTE AS CALLER
AS
BEGIN  
DECLARE      @ModifiedBy    VARCHAR(300),  
       @ModifiedDate   DateTime,  
       @PrevCountryCode  VARCHAR(500),  
       @PrevProductCode  VARCHAR(1000),  
       @DeletedItemList  VARCHAR(3000),  
       @CountryName   VARCHAR(1000),  
       @CurrentItemList  VARCHAR(1000),  
       @LoginId    VARCHAR(300),  
       @CountryCode   VARCHAR(500)  
     
      
      
     
    SET @DeletedItemList = ''  
    DECLARE UserCountryProducts CURSOR FOR  
    SELECT DISTINCT CountryCode  
    FROM MNT_UserCountryProducts WHERE UserId = @UserId    
    OPEN UserCountryProducts  
    FETCH NEXT FROM UserCountryProducts  
    INTO @PrevCountryCode  
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
     BEGIN  
       SELECT  @CountryName = CountryName  
       FROM  MNT_UserCountry  
       WHERE  CountryCode=@PrevCountryCode  
       SET @DeletedItemList= @DeletedItemList + @CountryName +' '  
                   
      DECLARE UserProducts CURSOR FOR  
      SELECT CountryCode,ProductCode   
      FROM MNT_UserCountryProducts WHERE UserId = @UserId and CountryCode=@PrevCountryCode  
      OPEN UserProducts  
       FETCH NEXT FROM UserProducts  
       INTO @CountryCode,@PrevProductCode   
        WHILE @@FETCH_STATUS = 0  
         BEGIN                                                           
          SET @DeletedItemList = @DeletedItemList + @PrevProductCode +','  
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
  
    SELECT @ModifiedBy=ModifiedBy,@ModifiedDate=ModifiedDate FROM MNT_Users where UserId=@UserId  
      
    INSERT INTO UserCountryProductLog(UserId,CountryCode,PreviousCountry,ModifiedBy,ModifiedDate,TranStatus)  
    VALUES(@UserId,@PrevCountryCode,@DeletedItemList,@ModifiedBy,@ModifiedDate,'N')  
      
END


