-- =============================================
-- Script Template
-- =============================================
SELECT * INTO MNT_PasswordSetup_Backup 
FROM MNT_PasswordSetup

DELETE MNT_PasswordSetup

INSERT [dbo].[MNT_PasswordSetup] ([EnforcePasswordHistory], [MaxPasswordAge], [MinPasswordAge], [MinPasswordLength], [PasswordComplexity], [AccLockoutDuration], [AccLockoutThreshold], [ResetAccCounterAfter], [EnforceLogonRestrict], [MaxLifeTimeServiceTicket], [MaxLifeTimeUserTicket], [MaxLifeTimeUserTicketRenewal], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [SendForgetPwdThroughMail]) VALUES (NULL, 90, 1, 8, N'N', 0, 0, 0, NULL, 0, 0, 0, N'Admin', NULL, CAST(0x0000A4B8010D3261 AS DateTime), NULL, N'Y')