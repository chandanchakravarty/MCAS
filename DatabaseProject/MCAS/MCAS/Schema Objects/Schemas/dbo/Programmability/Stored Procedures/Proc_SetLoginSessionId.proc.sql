CREATE PROCEDURE [dbo].[Proc_SetLoginSessionId]
(
@userId int,
@sessionId nvarchar(200)
)
AS
BEGIN
Update MNT_Users Set SessionId = @sessionId Where SNo = @userId
END