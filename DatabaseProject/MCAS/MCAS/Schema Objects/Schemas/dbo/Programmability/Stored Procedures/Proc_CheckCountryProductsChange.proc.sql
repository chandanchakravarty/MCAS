CREATE PROCEDURE [dbo].[Proc_CheckCountryProductsChange]
	@UserId [varchar](200),
	@ProductCode [varchar](8000)
WITH EXECUTE AS CALLER
AS
BEGIN  
DECLARE @StatusChange VARCHAR(10),@CountryCode VARCHAR(20),@Product_Code VARCHAR(100),@TempBlock VARCHAR(1000),@TempCountryCode VARCHAR(50),@TempProductCode VARCHAR(8000)  
DECLARE @UserCountryProducts Table  
  (  
  UserId   VARCHAR(100),  
  CountryCode  VARCHAR(100),  
  Product_Code VARCHAR(8000)    
  )  
SET @StatusChange='N'  

  
 WHILE (LEN(@ProductCode)>0)  
 BEGIN   
  SET  @TempBlock  = SUBSTRING(@ProductCode,0,CHARINDEX('#',@ProductCode)+1)  
  SELECT  @ProductCode    = REPLACE(@ProductCode,@TempBlock,'')    
  SELECT  @TempBlock  = REPLACE(@TempBlock,'#','')   
   WHILE(LEN(@TempBlock)>0)  
   BEGIN  
      
    SELECT @TempCountryCode = SUBSTRING(@TempBlock,0,CHARINDEX('~',@TempBlock)+1)  
    SELECT @TempProductCode = SUBSTRING(@TempBlock,CHARINDEX('~',@TempBlock)+1,LEN(@TempBlock))  
    SELECT @TempCountryCode = REPLACE(@TempCountryCode,'~','')      
    INSERT INTO @UserCountryProducts(UserId,CountryCode,Product_Code)  
    SELECT @UserId AS UserId,@TempCountryCode AS CountryCode,Item AS ProductCode  
    FROM F_SPLIT(@TempProductCode,',')  
    SET @TempBlock=''  
   END    
 END  
    
   
  
 DECLARE CheckCountryChange CURSOR FOR  
 SELECT CountryCode  FROM  @UserCountryProducts  
 WHERE UserId=@UserId   
 OPEN CheckCountryChange   
 FETCH NEXT FROM CheckCountryChange   
 INTO @CountryCode  
 WHILE @@FETCH_STATUS = 0  
 BEGIN      
  IF NOT EXISTS(SELECT 1 FROM MNT_UserCountryProducts WHERE UserId = @UserId and CountryCode = @CountryCode)   
  BEGIN     
   SET @StatusChange='Y'  
  END   
 FETCH NEXT FROM CheckCountryChange   
 INTO @CountryCode  
 END  
 CLOSE CheckCountryChange   
 DEALLOCATE CheckCountryChange  
  
 IF(@StatusChange='N')  
 BEGIN  
  DECLARE CheckProductChange CURSOR FOR  
  SELECT Product_Code,CountryCode  FROM  @UserCountryProducts  
  WHERE UserId=@UserId   
  OPEN CheckProductChange  
  FETCH NEXT FROM CheckProductChange  
  INTO @Product_Code,@CountryCode  
  WHILE @@FETCH_STATUS = 0  
  BEGIN      
   IF NOT EXISTS(SELECT 1 FROM MNT_UserCountryProducts WHERE UserId = @UserId and CountryCode = @CountryCode and ProductCode=@Product_Code)   
   BEGIN     
    SET @StatusChange='Y'  
   END   
  FETCH NEXT FROM CheckProductChange  
  INTO @Product_Code,@CountryCode  
  END  
  CLOSE CheckProductChange  
  DEALLOCATE CheckProductChange  
 END  
  
 IF(@StatusChange='N')  
 BEGIN  
  DECLARE CheckCountryProductChange CURSOR FOR  
  SELECT ProductCode,CountryCode  FROM MNT_UserCountryProducts  
  WHERE UserId=@UserId   
  OPEN CheckCountryProductChange  
  FETCH NEXT FROM CheckCountryProductChange  
  INTO @Product_Code,@CountryCode  
  WHILE @@FETCH_STATUS = 0  
  BEGIN      
   IF NOT EXISTS(SELECT 1 FROM @UserCountryProducts WHERE UserId = @UserId and CountryCode = @CountryCode and Product_Code=@Product_Code)   
   BEGIN     
    SET @StatusChange='Y'  
   END   
   FETCH NEXT FROM CheckCountryProductChange  
   INTO @Product_Code,@CountryCode  
  END  
  CLOSE CheckCountryProductChange  
  DEALLOCATE CheckCountryProductChange  
 END  
  
SELECT @StatusChange AS StatusChange  
END


