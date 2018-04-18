-- =============================================
-- Script Template
-- =============================================

-----Step 1) Delete all Record From MNT_TEMPLATE_MASTER
 Delete From MNT_TEMPLATE_MASTER
-----
-----Step 2)Insert Record into MNT_TEMPLATE_MASTER

INSERT INTO [dbo].[MNT_TEMPLATE_MASTER]([Description], [Filename], [Display_Name], [Carrier_Id], [Lob_Id], [Template_Path], [Is_System_Template], [Template_Format_Id], [Is_Active], [MappingXML_Path], [MappingXML_FileName], [Template_Code], [Has_Dynamic_Data], [Has_Condition], [Has_Dynamic_Footer], [Has_Footer_Desc], [Has_Dynamic_Header_Footer], [ParentId], [ScreenId], [OutPutFormat], [Id], [Is_Header])
VALUES (N'New Claim Acknowledgement letters', N'Document Template Name', N'New Claim Acknowledgement letters', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 131, NULL, 1, N'Y'),
(N'Property damage Procedure', N'Acknowledgement PD', N'Property damage Procedure', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 131, N'docx', 2, N'N'),
(N'Bodily Injury Procedure', N'Acknowledgement BI', N'Bodily Injury Procedure', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 131, N'docx', 3, N'N'),
(N'Acknowledgement', N'Acknowledgement', N'Acknowledgement', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 131, N'docx', 4, N'N'),
(N'Letter to  Claimants', N'Document Template Name', N'Letter to  Claimants', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 131, NULL, 5, N'Y'),
(N'Individual', N'Letter Template - BI', N'Individual', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 131, NULL, 6, N'Y'),
(N'Letter Template  - MP', N'Reply letter - BI - MP', N'Letter Template  - MP', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 131, N'docx', 7, N'N'),
(N'Cover Letter with DV', N'Cover Letter with DV', N'Cover Letter with DV', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 131, N'docx', 8, N'N'),
(N'Lawyers/Workshops/Insurers/Surveyors', N'Document Template Name', N'Lawyers/Workshops/Insurers/Surveyors', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, 131, NULL, 9, N'Y'),
(N'Letter Template  – Property', N'Letter Template - PD', N'Letter Template  – Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 9, 131, N'docx', 10, N'N'),
(N'Letter Template  – Recovery', N'Letter Template - RC  ', N'Letter Template  – Recovery', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 9, 131, N'docx', 11, N'N'),
(N'Faxes', N'Document Template Name', N'Faxes', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 131, NULL, 12, N'Y'),
(N'Offer', N'Document Template Name', N'Offer', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, 131, NULL, 13, N'Y'),
(N'Property', N'Offer Property', N'Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 13, 131, N'docx', 14, N'N'),
(N'Injury (TP vehicle No)', N'Offer Injury - TP Veh No', N'Injury (TP vehicle No)', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 13, 131, N'docx', 15, N'N'),
(N'Injury (others)', N'Offer Injury - Others', N'Injury (others)', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 13, 131, N'docx', 16, N'N'),
(N'Confirmation of Settlement   ', N'Document Template Name', N'Confirmation of Settlement   ', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, 131, NULL, 17, N'Y'),
(N'Property', N'Confirmation of settlement - PD', N'Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 17, 131, N'docx', 18, N'N'),
(N'Injury (TP vehicle No)', N'Confirmation of settlement - BI - TP Veh No', N'Injury (TP vehicle No)', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 17, 131, N'docx', 19, N'N'),
(N'Injury (others)', N'Confirmation of settlement - BI - Others', N'Injury (others)', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 17, 131, N'docx', 20, N'N'),
(N'Appointment of Law firm', N'Document Template Name', N'Appointment of Law firm', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, 131, NULL, 21, N'Y'),
(N'Bodily Injury', N'Letter for lawyer appointment - BI', N'Bodily Injury', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 21, 131, N'pdf', 22, N'N'),
(N'Property', N'Letter for lawyer appointment - PD', N'Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 21, 131, N'pdf', 23, N'N'),
(N'Recovery', N'Letter for lawyer appointment - RC', N'Recovery', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 21, 131, N'pdf', 24, N'N'),
(N'General', N'Document Template Name', N'General', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, 131, NULL, 25, N'Y'),
(N'3rd Party Lawyer CCTV clauses', N'CCTV footage release 3P Lawyer', N'3rd Party Lawyer CCTV clauses', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 26, N'N'),
(N'Our Lawyer CCTV clauses', N'CCTV footage release our Lawyer', N'Our Lawyer CCTV clauses', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 27, N'N'),
(N'Insurer CCTV clauses', N'CCTV footage release Insurer', N'Insurer CCTV clauses', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 28, N'N'),
(N'Invitation to view CCTV', N'Invite to view CCTV', N'Invitation to view CCTV', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 29, N'N'),
(N'CB vs CB', N'CB vs CB', N'CB vs CB', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 30, N'N'),
(N'Medical Report Form', N'Medical Report Form', N'Medical Report Form', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'pdf', 31, N'N'),
(N'Medical Report Letter', N'Letter for Medical Report', N'Medical Report Letter', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'pdf', 32, N'N'),
(N'Reply to CPF Board', N'CPF Board - BI', N'Reply to CPF Board', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'pdf', 33, N'N'),
(N'Cheque Cancellation/Reissue Memo  - Property', N'Cheque Cancellation Or Reissue Memo - PD', N'Cheque Cancellation/Reissue Memo  - Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 34, N'N'),
(N'Cheque Cancellation/Reissue Memo  - Injury', N'Cheque Cancellation Or Reissue Memo - BI', N'Cheque Cancellation/Reissue Memo  - Injury', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 35, N'N'),
(N'Memo to CEO-CDGI', N'Memo to CEO -CDGI', N'Memo to CEO-CDGI', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 36, N'N'),
(N'Letter of Undertaking for deceased employee', N'Letter of Undertaking for deceased employee', N'Letter of Undertaking for deceased employee', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 37, N'N'),
(N'Authority to Recover PI Payout', N'Authority to Recover PI Payout', N'Authority to Recover PI Payout', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'docx', 38, N'N'),
(N'Settlement Notice To III', N'Settlement Notice To III', N'Settlement Notice To III', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, 131, N'pdf', 39, N'N'),
(N'Payments', N'Document Template Name', N'Payments', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 131, NULL, 40, N'Y'),
(N'Discharge Voucher  ', N'Document Template Name', N'Discharge Voucher  ', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 131, NULL, 41, N'Y'),
(N'NEL', N'DV NEL Others', N'NEL', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 41, 131, N'docx', 42, N'N'),
(N'SBST- Others', N'DV SBST  Others', N'SBST- Others', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 41, 131, N'docx', 43, N'N'),
(N'SBST -Third Party Vehicle No', N'SBST DV TP Veh No', N'SBST -Third Party Vehicle No', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 41, 131, N'docx', 44, N'N'),
(N'Discharge Voucher – Infant', N'Document Template Name', N'Discharge Voucher – Infant', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 131, NULL, 45, N'N'),
(N'SBST - Others', N'DV SBST BI Infant Others', N'SBST - Others', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 45, 131, N'docx', 46, N'N'),
(N'SBST - Third Party Vehicle No', N'DV SBST BI Infant TP Veh No', N'SBST - Third Party Vehicle No', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 45, 131, N'docx', 47, N'N'),
(N'Discharge Voucher – Interim Payment', N'Document Template Name', N'Discharge Voucher – Interim Payment', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 131, NULL, 48, N'N'),
(N'SBST -  Others', N'DV SBST BI Interim Payment Others', N'SBST -  Others', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 131, N'docx', 49, N'N'),
(N'SBST - Third Party Vehicle No', N'DV SBST  BI Interim Payment TP Veh No', N'SBST - Third Party Vehicle No', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, 131, N'docx', 50, N'N'),
(N'Others', N'Document Template Name', N'Others', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, 131, N'docx', 51, N'N'),
(N'Billing Memo to III', N'Billing Memo to III', N'Billing Memo to III', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 51, 131, N'docx', 52, N'N'),
(N'IRAS Lodgement Form', N'Lodgement Form', N'IRAS Lodgement Form', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 52, 131, N'docx', 53, N'N'),
(N'Recovery - Letter of Demand', N'Document Template Name', N'Recovery - Letter of Demand', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 137, NULL, 54, N'Y'),
(N'3P Vehicle', N'LOD - 3P Vehicle', N'3P Vehicle', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 54, 137, N'docx', 55, N'N'),
(N'Grasscutting', N'LOD - Grasscutting', N'Grasscutting', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 54, 137, N'docx', 56, N'N'),
(N'Miscellaneous', N'LOD - Misc', N'Miscellaneous', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 54, 137, N'docx', 57, N'N'),
(N'Bus Payment Form', N'Document Template Name', N'Bus Payment Form', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 139, NULL, 58, N'Y'),
(N'Property', N'Payment Form Bus PD', N'Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 58, 139, N'docx', 59, N'N'),
(N'Injury', N'Payment Form Bus BI', N'Injury', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 58, 139, N'docx', 60, N'N'),
(N'Rail Payment Form', N'Document Template Name', N'Rail Payment Form', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 139, N'docx', 61, N'Y'),
(N'Injury', N'Payment Form Rail BI', N'Injury', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 61, 139, N'docx', 62, N'N'),
(N'Payment Minutes', N'Document Template Name', N'Payment Minutes', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 139, N'docx', 63, N'Y'),
(N'Property', N'Payment Minute - PD', N'Property', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 63, 139, N'docx', 64, N'N'),
(N'Bodily Injury', N'Payment Minute - BI', N'Bodily Injury', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 63, 139, N'docx', 65, N'N'),
(N'Recovery', N'Payment Minute - RC', N'Recovery', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 63, 139, N'docx', 66, N'N'),
(N'Letter of Guarantee', N'Document Template Name', N'Letter of Guarantee', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 303, NULL, 67, N'Y'),
(N'Letter of Guarantee', N'LOG', N'Letter of Guarantee', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 67, 303, N'docx', 68, N'N'),
(N'Letter of Guarantee Top-up', N'LOG Topup', N'Letter of Guarantee Top-up', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 67, 303, N'docx', 69, N'N'),
(N'Letter Template', N'Letter Template - BI', N'Letter Template', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 131, N'docx', 70, N'N'),
(N'Offer letter – request documents', N'Offer Letter BI - Request documents', N'Offer letter – request documents', NULL, NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, 131, N'docx', 71, N'N')

--Update Script

update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/1New Claim Acknowledgement letters', Template_Code = 'ACKPD', Filename = 'Acknowledgement_PD.rdl' where Id = 2
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/1New Claim Acknowledgement letters', Template_Code = 'ACKBI', Filename = 'Acknowledgement_BI.rdl' where Id = 3
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/1New Claim Acknowledgement letters', Template_Code = 'ACK', Filename = 'Acknowledgement.rdl' where Id = 4


update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/1Individual', Template_Code = 'CLDV', Filename = 'Cover_Letter_with_DV.rdl' where Id = 8
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/1Individual', Template_Code = 'LTBI', Filename = 'Letter_Template BI.rdl' where Id = 70
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/1Individual', Template_Code = 'LTBIMP', Filename = 'Letter_Template-BI-MP.rdl' where Id = 7
update MNT_TEMPLATE_MASTER set Template_Path = '/Support/Templates/DocumentTemplates/2Letter to  Claimants/1Individual', Template_Code = 'OLBIR', Filename = 'Offer_Letter_BI-Request_documents.rdl' where Id = 71