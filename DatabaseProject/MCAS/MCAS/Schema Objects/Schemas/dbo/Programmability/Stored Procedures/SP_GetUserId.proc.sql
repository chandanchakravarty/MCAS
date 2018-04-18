CREATE PROCEDURE [dbo].[SP_GetUserId]
	@BranchCode [nvarchar](10),
	@LoginName [nvarchar](20),
	@Password [nvarchar](100) = '',
	@CountryCode [varchar](5)
WITH EXECUTE AS CALLER
AS
DECLARE @UserBranch VARCHAR(10)  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    SET @UserBranch = ''  
 IF EXISTS(SELECT 1 FROM MNT_Users WHERE UserId = @LoginName)  
 begin  
  IF EXISTS( SELECT  1 FROM MNT_Users U  
     INNER JOIN MNT_UserCountryProducts UCP  
     ON  U.UserId = UCP.UserId   
     WHERE U.UserId = @LoginName   
     AND  LoginPassword = @Password  
     AND  CountryCode =@CountryCode)  
  BEGIN   
   SELECT @UserBranch = BranchCode FROM MNT_Users WHERE UserId = @LoginName  
   IF EXISTS(SELECT 1 FROM MNT_UserBranches WHERE BranchCode = @BranchCode AND UserId = @LoginName) OR @UserBranch = @BranchCode  
   Begin  
    SELECT UserId = CASE ISNULL(IsActive, 'N') WHEN 'N' THEN '-2' WHEN 'Y' THEN UserId END   
    FROM MNT_Users WHERE UserId = @LoginName  
   END  
   ELSE  
   BEGIN  
    SELECT '-3' AS 'UserID'  
   END  
  END  
  ELSE   
  BEGIN  
   IF NOT EXISTS (SELECT 1 FROM MNT_Users where UserId  = @LoginName AND LoginPassword = @Password)  
   BEGIN   
    SELECT '-1' AS 'UserID'  
   END  
   ELSE  
   BEGIN  
    SELECT '-4' AS 'UserID'  
   END  
  END  
 END  
 ELSE SELECT '0' AS 'UserID'   
END


