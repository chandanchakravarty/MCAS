/****** Object:  StoredProcedure [dbo].[Proc_GetUserId]    Script Date: 06/03/2014 12:15:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetUserId]

@BranchCode       NVARCHAR(10),

@LoginName        NVARCHAR(20),

@Password         NVARCHAR(100) = '',

@CountryCode      VARCHAR(5)

AS

DECLARE @UserBranch VARCHAR(10)

BEGIN

      -- SET NOCOUNT ON added to prevent extra result sets from

      -- interfering with SELECT statements.

      SET NOCOUNT ON;

 

    SET @UserBranch = ''

      IF EXISTS(SELECT 1 FROM MNT_Users WHERE UserId = @LoginName)

      begin

            IF EXISTS(  SELECT  1 FROM MNT_Users U

                              --INNER     JOIN MNT_UserCountryProducts UCP

                              --ON        U.UserId = UCP.UserId

                              WHERE U.UserId = @LoginName

                              AND         LoginPassword = @Password

                              --AND       CountryCode =@CountryCode

                              )

            BEGIN

                  SELECT @UserBranch = BranchCode FROM MNT_Users WHERE UserId = @LoginName

                  --IF EXISTS(SELECT 1 FROM MNT_UserBranches WHERE BranchCode = @BranchCode AND UserId = @LoginName) OR @UserBranch = @BranchCode

                  Begin

                        SELECT UserId = CASE ISNULL(IsActive, 'N') WHEN 'N' THEN '-2' WHEN 'Y' THEN UserId END

                        FROM MNT_Users WHERE UserId = @LoginName

                  END

                  --ELSE

                  --BEGIN

                  --    SELECT '-3' AS 'UserID'

                  --END

            END

            ELSE

            BEGIN

                  IF NOT EXISTS (SELECT 1 FROM MNT_Users where UserId  = @LoginName AND      LoginPassword = @Password)

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
GO
