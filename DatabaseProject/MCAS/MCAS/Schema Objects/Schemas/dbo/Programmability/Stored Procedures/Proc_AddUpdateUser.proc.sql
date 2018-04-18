CREATE PROCEDURE [dbo].[Proc_AddUpdateUser]
	@PageMode [int],
	@UserId [varchar](20),
	@LoginPassword [varchar](50),
	@UserFullName [varchar](100),
	@UserDispName [varchar](20),
	@BranchCode [varchar](10),
	@DeptCode [varchar](10),
	@GroupId [varchar](5),
	@PaymentLimit [decimal](18, 2),
	@CreditNoteLimit [decimal](18, 2),
	@IsActive [varchar](1),
	@AccessLevel [int],
	@MainClass [varchar](20),
	@LoginId [varchar](100)
WITH EXECUTE AS CALLER
AS
DECLARE   
  @Result  VARCHAR(1),    
  @ResultDesc VARCHAR(100)    
 DECLARE @CheckPasswordHistory VARCHAR(5),@OldPW VARCHAR(100)  
 DECLARE @ModifiedDate DATETIME  
 DECLARE @CurrentPassword VARCHAR(500)  
  
 BEGIN    
  SET @Result = 'Y'    
  SET @ResultDesc = ''    
  SELECT @CheckPasswordHistory=EnforcePasswordHistory from PasswordSetup  
  IF @PageMode = 1     
  BEGIN    
   IF Exists(SELECT 1 FROM MNT_Users WHERE UserId = @UserId)    
   BEGIN    
    SET @Result = 'N'    
    SET @ResultDesc = 'User with LoginId "' + ltrim(rtrim(@UserId)) + '" Already Exist. Please Choose Another LoginId.'     
   END    
   ELSE    
   BEGIN   
    SELECT @ModifiedDate=GetDate()   
    INSERT INTO MNT_Users(UserId,LoginPassword,GroupId,    
    UserFullName,UserDispName,DeptCode,BranchCode,PaymentLimit,    
    CreditNoteLimit,IsActive,AccessLevel,MainClass,CreatedBy,CreatedDate)    
    VALUES(@UserId,@LoginPassword,@GroupId,@UserFullName,    
    @UserDispName,@DeptCode,@BranchCode,@PaymentLimit,@CreditNoteLimit,    
    @IsActive,@AccessLevel,@MainClass,@LoginId,GetDate())    
  
    Insert INTo MNT_UserPasswordHistory values (@UserId,@LoginPassword,getdate())    
    SET @Result = 'Y'    
    SET @ResultDesc = 'New User with LoginId "' + ltrim(rtrim(@UserId)) + '" Successfully Created '    
      
    IF NOT EXISTS(SELECT 1 FROM TrackModifiedPassword Where UserId=@UserId)  
    BEGIN  
     IF EXISTS(SELECT 1 FROM PasswordSetup)  
      BEGIN  
       INSERT INTO TrackModifiedPassword(UserId,IsPwdModified,ModifiedDate)  
       VALUES(@UserId,'Y',@ModifiedDate)  
      END  
    END  
      
   END    
  END    
  ELSE    
  BEGIN    
   IF Exists(SELECT 1 FROM MNT_Users WHERE UserId = @UserId)    
   BEGIN    
    SELECT @OldPW=LoginPassword from MNT_Users WHERE UserId = @UserId      
    SELECT @ModifiedDate=GetDate()      
  
    UPDATE MNT_Users   
    SET UserId   = @UserId,    
     DeptCode  = @DeptCode,    
     GroupId   = @GroupId,    
     UserFullName = @UserFullName,    
     UserDispName = @UserDispName,    
     BranchCode  = @BranchCode,      
     PaymentLimit = @PaymentLimit,    
     CreditNoteLimit = @CreditNoteLimit,    
     IsActive  = @IsActive,    
     AccessLevel  = @AccessLevel,  
     MainClass  = @MainClass,  
     ModifiedBy  = @LoginId,  
     ModifiedDate = @ModifiedDate  
    WHERE UserId = @UserId    
   IF @LoginPassword!=''    
   BEGIN    
    --IF not exists (Select 1 From TM_UserPasswordHistory Where LoginPassword Not IN (@LoginPassword))    
    -- BEGIN     
    UPDATE MNT_Users SET LoginPassword = @LoginPassword WHERE UserId = @UserId   
    --IF(UPPER(RTRIM(LTRIM(@CheckPasswordHistory)))='Y')  
    --BEGIN  
    IF(RTRIM(LTRIM(@OldPW))<>LTRIM(RTRIM(@LoginPassword)))  
    BEGIN  
     INSERT INTO TM_PasswordHistory(UserId,LoginPassword,ModifiedDate)  
           VALUES(@UserId,@OldPW,@ModifiedDate)   
    END  
    --END   
    -- END    
   END    
   SET @Result = 'Y'    
   SET @ResultDesc = 'User Details Successfully Updated for LoginId "' + ltrim(rtrim(@UserId)) + '" '    
  END    
  ELSE    
  BEGIN    
   SET @Result = 'N'    
   SET @ResultDesc = 'User Not Found'    
  END    
 END    
 SELECT @Result as Result, @ResultDesc as ResultDesc    
END


