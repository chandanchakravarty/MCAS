Set IDENTITY_INSERT MNT_PasswordSetup ON
IF NOT EXISTS (SELECT 1 FROM MNT_PasswordSetup WITH(NOLOCK) WHERE setupid=1)

BEGIN

INSERT INTO MNT_PasswordSetup
(EnforcePasswordHistory,
MaxPasswordAge,
MinPasswordAge,
MinPasswordLength,
PasswordComplexity,
AccLockoutDuration,
AccLockoutThreshold,
ResetAccCounterAfter,
EnforceLogonRestrict,
MaxLifeTimeServiceTicket,
MaxLifeTimeUserTicket,
MaxLifeTimeUserTicketRenewal,
CreatedBy,
ModifiedBy)

VALUES('1',365,0,5,'Y',600,900,0,'Y',0,0,0,'pravesh','Pravesh')

END
Set IDENTITY_INSERT MNT_PasswordSetup OFF
GO