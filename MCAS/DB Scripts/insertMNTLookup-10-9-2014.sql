IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='F1' and Category='ATTACHMENT') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'F1', N'Folder 1', N'Folder 1', N'ATTACHMENT')

END 

--2
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='F2' and Category='ATTACHMENT') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'F2', N'Folder 2', N'Folder 2', N'ATTACHMENT')

END 

--3
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='F3' and Category='ATTACHMENT') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'F3', N'Folder 3', N'Folder 3', N'ATTACHMENT')

END 
--4
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='F4' and Category='ATTACHMENT') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'F4', N'Folder 4', N'Folder 4', N'ATTACHMENT')

END 

IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LOE'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LOE', N'LossofEarning', N'LossofEarning', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'COR'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'COR', N'CostofRepairs', N'CostofRepairs', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LOU'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LOU', N'LossofUse', N'LossofUse', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LOUUN'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LOUUN', N'LossofUseUn', N'LossofUseUn', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LOR'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LOR', N'LossofRental', N'LossofRental', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'Ex'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'Ex', N'Excess', N'Excess', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'OE'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'OE', N'OtherExpenses', N'OtherExpenses', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'RE'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'RE', N'ReportFees', N'ReportFees', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'SF'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'SF', N'SurveyFee', N'SurveyFee', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'RSF'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'RSF', N'ReSurveyFee', N'ReSurveyFee', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LPRF'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LPRF', N'LGPolRepFee', N'LGPolRepFee', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'PLC'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'PLC', N'ParLawCost3rd', N'ParLawCost3rd', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'PLD'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'PLD', N'ParLawDisbursements3rd', N'ParLawDisbursements3rd', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'OLC'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'OLC', N'OurLawyerCost', N'OurLawyerCost', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'OLD'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'OLD', N'OurLawDisbursements', N'OurLawDisbursements', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'GD'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'GD', N'GeneralDamages', N'GeneralDamages', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'ME'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'ME', N'MedicalExpenses', N'MedicalExpenses', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'FME'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'FME', N'FutureMedicalExpenses', N'FutureMedicalExpenses', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LME'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LME', N'LOGMedicalExpenses', N'LOGMedicalExpenses', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LEC'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LEC', N'LossofEarningsCapacity', N'LossofEarningsCapacity', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LOEAR'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LOEAR', N'LossofEarnings', N'LossofEarnings', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'LODE'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'LODE', N'LossofFutureEarnings', N'LossofFutureEarnings', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'TRAN'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'TRAN', N'Transport', N'Transport', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'MR'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'MR', N'MedicalRecord', N'MedicalRecord', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'PTF'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'PTF', N'PublicTrusteeFee', N'PublicTrusteeFee', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'OPEF'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'OPEF', N'OurProfessionalExpertFee', N'OurProfessionalExpertFee', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'RPD'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'RPD', N'Rateperday', N'Rateperday', 'TranComponent')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = 'TOTAL'
  AND Category = 'TranComponent')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'TOTAL', N'Total', N'Total', 'TranComponent')
END

IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'1', N'START WORK', 'START WORK', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'2', N'WORKDED 30 MINS', 'WORKDED 30 MINS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'3', N'WORKDED 1 HR', 'WORKDED 1 HR', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'4', N'WORKDED 1 HR 30 MINS', 'WORKDED 1 HR 30 MINS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '5'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'5', N'WORKDED 2 HRS', 'WORKDED 2 HRS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '6'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'6', N'WORKDED 2 HRS 30 MINS', 'WORKDED 2 HRS 30 MINS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '7'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'7', N'WORKDED 3 HRS', 'WORKDED 3 HRS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '8'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'8', N'WORKDED 3 HRS 30 MINS', 'WORKDED 3 HRS 30 MINS', 'Y', 'OperatingHours')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '9'
  AND Category = 'OperatingHours')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [IsActive], [Category])
    VALUES (N'9', N'WORKDED 4 HRS', 'WORKDED 4 HRS', 'Y', 'OperatingHours')
END
