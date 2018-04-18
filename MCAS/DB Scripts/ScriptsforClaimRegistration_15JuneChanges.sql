Insert Into [dbo].[MNT_Lookups] 
Values ('TPV','Third Party Vehicle','Third Party Vehicle','CLMTYPE')

Update [dbo].[MNT_Lookups] Set Lookupdesc = 'Third Party Property Damage' Where LookupId = 206

Alter Table [dbo].[CLM_ThirdParty] ADD VehicleRegnNo nvarchar(10), VehicleMake varchar(30), VehicleModel varchar(20), LossDamageDesc nvarchar(500),
TPAdjuster nvarchar(50), TPLawyer nvarchar(50), TPWorkShop nvarchar(50), Remarks nvarchar(500), AttachedFile nvarchar(100), ReserveCurr varchar(20),
ReserveExRate decimal(18,9), ReserveAmt int, ExpensesCurr varchar(20), ExpensesExRate decimal(18,9),ExpensesAmt int, TotalReserve int, 
ClaimAmount numeric(18,0), PaidDate datetime, BalanceLOG numeric(18,0), LOGAmount numeric(18,0), LOURate numeric(18,0), LOUDays numeric(18,0)


Alter Table [dbo].[CLM_ThirdParty] ADD ClaimAmtCurr varchar(20), ClaimAmtExRate decimal(18,9),ClaimAmt int, PayableTo nvarchar(50)

Alter Table [dbo].[CLM_ThirdParty] ADD ExpensesAmount int, ReserveAmount int

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ExpensesAmount decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ReserveAmount decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ReserveAmt decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ExpensesAmt decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN TotalReserve decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ClaimAmount decimal(18,9)

ALTER TABLE [dbo].[CLM_ThirdParty]
ALTER COLUMN ClaimAmt decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ReserveAmt decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN TotalReserve decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ExpensesAmt decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ClaimAmtPayout decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ExpensesAmount decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ReserveAmount decimal(18,9)

ALTER TABLE [dbo].[CLM_Claims]
ALTER COLUMN ClaimAmount decimal(18,9)