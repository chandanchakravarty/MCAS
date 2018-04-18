-- =============================================
-- Script Template
-- =============================================
/*2Letter to  Claimants/2LawyersWorkshopsInsurersSurveyors*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/2LawyersWorkshopsInsurersSurveyors', Template_Code = 'LTPD', Filename = 'LetterTemplate-PD.rdl' where Id = 10
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/2LawyersWorkshopsInsurersSurveyors', Template_Code = 'LTRC', Filename = 'LetterTemplate-RC.rdl' where Id = 11

/*3Faxes/1Offer*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/1Offer', Template_Code = 'OIO', Filename = 'Offer_Injury-Others.rdl' where Id = 16
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/1Offer', Template_Code = 'OITVN', Filename = 'Offer_Injury-TP_Veh_No.rdl' where Id = 15
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/1Offer', Template_Code = 'OPR', Filename = 'Offer_Property.rdl' where Id = 14

/*3Faxes/2Confirmation of Settlement*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/2Confirmation of Settlement', Template_Code = 'COSBI', Filename = 'Confirmationofsettlement-BI-Others.rdl' where Id = 20
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/2Confirmation of Settlement', Template_Code = 'COSTP', Filename = 'Confirmationofsettlement-BI-TPVehNo.rdl' where Id = 19
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/3Faxes/2Confirmation of Settlement', Template_Code = 'COSPD', Filename = 'Confirmationofsettlement-PD.rdl' where Id = 18

/*4Appointment of Law firm*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/4Appointment of Law firm', Template_Code = 'LLABI', Filename = 'Letterforlawyerappointment-BI.rdl' where Id = 22
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/4Appointment of Law firm', Template_Code = 'LLAPD', Filename = 'Letterforlawyerappointment-PD.rdl' where Id = 23
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/4Appointment of Law firm', Template_Code = 'LLARC', Filename = 'Letterforlawyerappointment-RC.rdl' where Id = 24

/*5General*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'ARPIP', Filename = 'AuthoritytoRecoverPIPayout.rdl' where Id = 38
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CBVCB', Filename = 'CBvsCB.rdl' where Id = 30
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CCTVTPL', Filename = 'CCTVfootagerelease3PLawyer.rdl' where Id = 26
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CCTVRI', Filename = 'CCTVfootagereleaseInsurer.rdl' where Id = 28
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CCTVRL', Filename = 'CCTVfootagereleaseourLawyer.rdl' where Id = 27
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CCRMBI', Filename = 'ChequeCancellationOrReissueMemo-BI.rdl' where Id = 35
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CCRMPD', Filename = 'ChequeCancellationOrReissueMemo-PD.rdl' where Id = 34
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'CPFBBI', Filename = 'CPFBoard-BI.rdl' where Id = 33
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'IVCCTV', Filename = 'InvitetoviewCCTV.rdl' where Id = 29
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'SNTT', Filename = 'SettlementNoticeToIII.rdl' where Id = 39
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'MCCDGI', Filename = 'MemotoCEO-CDGI.rdl' where Id = 36
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'MRF', Filename = 'MedicalReportForm.rdl' where Id = 31
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'LUDE', Filename = 'LetterofUndertakingfordeceasedemployee.rdl' where Id = 37
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/5General', Template_Code = 'LFMR', Filename = 'LetterforMedicalReport.rdl' where Id = 32

/*6Recovery - Letter of Demand*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/6Recovery - Letter of Demand', Template_Code = 'LODTPV', Filename = 'LOD-3PVehicle.rdl' where Id = 55
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/6Recovery - Letter of Demand', Template_Code = 'LODG', Filename = 'LOD-Grasscutting.rdl' where Id = 56
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/6Recovery - Letter of Demand', Template_Code = 'LODM', Filename = 'LOD-Misc.rdl' where Id = 57

/*7Payments/1Discharge Voucher*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/7Payments/1Discharge Voucher', Template_Code = 'DVNO', Filename = 'DVNELOthers.rdl' where Id = 42
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/7Payments/1Discharge Voucher', Template_Code = 'SBSTO', Filename = 'DVSBSTOthers.rdl' where Id = 43
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/7Payments/1Discharge Voucher', Template_Code = 'SBSTTP', Filename = 'DVSBSTTPVehNo.rdl' where Id = 44

/*7Payments/6Letter of Guarantee*/
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/7Payments/6Letter of Guarantee', Template_Code = 'LOGTP', Filename = 'LOG_TopUp.rdl' where Id = 69
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/7Payments/6Letter of Guarantee', Template_Code = 'LOG', Filename = 'LOG.rdl' where Id = 68

