-- =============================================
-- Script Template
-- =============================================
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_MNT_OrgCountry_InsurerType')   
    DROP INDEX IX_MNT_OrgCountry_InsurerType ON dbo.MNT_OrgCountry;   
GO  
CREATE NONCLUSTERED INDEX IX_MNT_OrgCountry_InsurerType   
    ON dbo.MNT_OrgCountry (InsurerType);   
GO 

  
IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'IX_MNT_OrgCountry_CountryOrgazinationCode')   
    DROP INDEX IX_MNT_OrgCountry_CountryOrgazinationCode ON dbo.MNT_OrgCountry;   
GO  
CREATE NONCLUSTERED INDEX IX_MNT_OrgCountry_CountryOrgazinationCode  
    ON dbo.MNT_OrgCountry (CountryOrgazinationCode);   
GO 


CREATE STATISTICS [_dta_stat_780738034_1_54_27_2] ON [dbo].[ClaimAccidentDetails]([AccidentClaimId], [LinkedAccidentClaimId], [Organization], [PolicyId])
CREATE STATISTICS [_dta_stat_780738034_2_1_27] ON [dbo].[ClaimAccidentDetails]([PolicyId], [AccidentClaimId], [Organization])

CREATE STATISTICS [_dta_stat_780738034_54_2_1] ON [dbo].[ClaimAccidentDetails]([LinkedAccidentClaimId], [PolicyId], [AccidentClaimId])


IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'_dta_index_ClaimAccidentDetails_8_780738034__K1_K54')   
    DROP INDEX  [_dta_index_ClaimAccidentDetails_8_780738034__K1_K54] ON [dbo].[ClaimAccidentDetails] ;   
GO
CREATE NONCLUSTERED INDEX [_dta_index_ClaimAccidentDetails_8_780738034__K1_K54] ON [dbo].[ClaimAccidentDetails] 
(
	[AccidentClaimId] ASC,
	[LinkedAccidentClaimId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'_dta_index_ClaimAccidentDetails_8_780738034__K54')   
    DROP INDEX  [_dta_index_ClaimAccidentDetails_8_780738034__K54] ON [dbo].[ClaimAccidentDetails] ;   
GO
CREATE NONCLUSTERED INDEX [_dta_index_ClaimAccidentDetails_8_780738034__K54] ON [dbo].[ClaimAccidentDetails] 
(
	[LinkedAccidentClaimId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO


IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'_dta_index_ClaimAccidentDetails_8_780738034__K27_K1_K54_K2_3_4_6_7_17_21_45_47_53')   
    DROP INDEX  [_dta_index_ClaimAccidentDetails_8_780738034__K27_K1_K54_K2_3_4_6_7_17_21_45_47_53] ON [dbo].[ClaimAccidentDetails] ;   
GO
CREATE NONCLUSTERED INDEX [_dta_index_ClaimAccidentDetails_8_780738034__K27_K1_K54_K2_3_4_6_7_17_21_45_47_53] ON [dbo].[ClaimAccidentDetails] 
(
	[Organization] ASC,
	[AccidentClaimId] ASC,
	[LinkedAccidentClaimId] ASC,
	[PolicyId] ASC
)
INCLUDE ( [IPNo],
[ClaimNo],
[VehicleNo],
[AccidentDate],
[DutyIO],
[DriverName],
[IsComplete],
[IsReported],
[IsReadOnly]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO

CREATE STATISTICS [_dta_stat_686833709_45_3_2] ON [dbo].[CLM_Claims]([ClaimsOfficer], [PolicyId], [AccidentClaimId])

CREATE STATISTICS [_dta_stat_686833709_48_45] ON [dbo].[CLM_Claims]([ClaimantStatus], [ClaimsOfficer])

CREATE STATISTICS [_dta_stat_686833709_45_1] ON [dbo].[CLM_Claims]([ClaimsOfficer], [ClaimID])


IF EXISTS (SELECT name FROM sys.indexes  
            WHERE name = N'_dta_index_CLM_Claims_8_686833709__K3_K2_K48_K45_K1_4_5_7_26_37')   
    DROP INDEX  [_dta_index_CLM_Claims_8_686833709__K3_K2_K48_K45_K1_4_5_7_26_37] ON [dbo].[CLM_Claims] ;   
GO
CREATE NONCLUSTERED INDEX [_dta_index_CLM_Claims_8_686833709__K3_K2_K48_K45_K1_4_5_7_26_37] ON [dbo].[CLM_Claims] 
(
	[PolicyId] ASC,
	[AccidentClaimId] ASC,
	[ClaimantStatus] ASC,
	[ClaimsOfficer] ASC,
	[ClaimID] ASC
)
INCLUDE ( [ClaimType],
[ClaimDate],
[ClaimStatus],
[ClaimantName],
[VehicleRegnNo]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

CREATE STATISTICS [_dta_stat_686833709_3_48_45_2_1] ON [dbo].[CLM_Claims]([PolicyId], [ClaimantStatus], [ClaimsOfficer], [AccidentClaimId], [ClaimID])
