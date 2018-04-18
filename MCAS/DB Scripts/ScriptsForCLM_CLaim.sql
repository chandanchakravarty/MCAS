ALTER TABLE [dbo].[CLM_Claims] ADD ReserveCurr varchar(20),ReserveExRate decimal(18,9),
ReserveAmt int,ExpensesCurr varchar(20),ExpensesExRate decimal(18,9),TotalReserve int,
AdjusterAppointed int,LawyerAppointed int,SurveyorAppointed int,DepotWorkshop int


ALTER TABLE [dbo].[CLM_Claims]
ADD CONSTRAINT FK_CLM_Claims_MNT_Adjusters
FOREIGN KEY (AdjusterAppointed)
REFERENCES MNT_Adjusters(AdjusterId)

ALTER TABLE [dbo].[CLM_Claims]
ADD CONSTRAINT FK_CLM_Claims_MNT_Adjusters1
FOREIGN KEY (LawyerAppointed)
REFERENCES MNT_Adjusters(AdjusterId)


ALTER TABLE [dbo].[CLM_Claims]
ADD CONSTRAINT FK_CLM_Claims_MNT_Adjusters2
FOREIGN KEY (SurveyorAppointed)
REFERENCES MNT_Adjusters(AdjusterId)


ALTER TABLE [dbo].[CLM_Claims]
ADD CONSTRAINT FK_CLM_Claims_MNT_Department
FOREIGN KEY (DepotWorkshop)
REFERENCES MNT_Department(DeptId)