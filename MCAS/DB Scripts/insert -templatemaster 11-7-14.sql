IF NOT EXISTS (SELECT 1 FROM [MNT_TEMPLATE_MASTER] WITH(NOLOCK) WHERE template_id=1)

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Authority_to_Recover_PI_Payout.pdf','Authority to recover PI Payout',
'/Uploads/Templates/C Stage/Discharge voucher',1,1,1,'C','New Claim Registration',null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','DV_(Individuals).pdf','DV (Individuals)',
'/Uploads/Templates/C Stage/Discharge voucher',1,1,1,'C','At the time of Payment',null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','DV(Infant).pdf','DV (Infant)',
'/Uploads/Templates/C Stage/Discharge voucher',1,1,1,'C','At the time of Payment',null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','DV_Interim_(via_email).pdf','DV Interim (via email)',
'/Uploads/Templates/C Stage/Discharge voucher',1,1,1,'C','At the time of Payment',null,null,null,null,null,null)

END


BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','First_Minute_Template($2756).pdf','First Minute Template($2756)',
'/Uploads/Templates/C Stage/File minutes',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','First_Minute_Template(Inj).pdf','First Minute Template(Inj)',
'/Uploads/Templates/C Stage/File minutes',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','First_Minute_Template(Ppt).pdf','First Minute Template(Ppt)',
'/Uploads/Templates/C Stage/File minutes',1,1,1,'C',null,null,null,null,null,null,null)

END


BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Memo_to_CEO_-CDGI.pdf','Memo to CEO – CDGI',
'/Uploads/Templates/C Stage/File minutes',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Cheque_Delivery_Cover_Letter.pdf','Cheque Delivery Cover Letter',
'/Uploads/Templates/C Stage/letter to 3P/Cheque letter',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','chq_letter_to_CPF_-_Letter_(Medisave_Refund).pdf','chq letter to CPF - Letter (Medisave Refund)',
'/Uploads/Templates/C Stage/letter to 3P/Cheque letter',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Confirm_settlement_for_property_claim.pdf','Confirm settlement for property claim',
'/Uploads/Templates/C Stage/letter to 3P/Confirm settlement',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Confirm_Settlement_DV_(exgratia_-I).pdf','Confirm Settlement+DV (exgratia -I)',
'/Uploads/Templates/C Stage/letter to 3P/Confirm settlement',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Confirm_Settlement_DV_(exgratia_-II).pdf','Confirm Settlement+DV (exgratia -II)',
'/Uploads/Templates/C Stage/letter to 3P/Confirm settlement',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','SBS-Confirm_Settlement_DV_(Law_Firms).pdf','SBS-Confirm Settlement+DV (Law Firms)',
'/Uploads/Templates/C Stage/letter to 3P/Confirm settlement',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Medical_Report_Consent_Form.pdf','Medical Report Consent Form',
'/Uploads/Templates/C Stage/letter to 3P/letters for Medical report',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','MedRpt_Request_to_Hospital.pdf','MedRpt Request to Hospital',
'/Uploads/Templates/C Stage/letter to 3P/letters for Medical report',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','LOG_with_cancellation_clause_for_previous_LOG.pdf','LOG with cancellation clause for previous LOG',
'/Uploads/Templates/C Stage/letter to 3P/LOG',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','LOG.pdf','LOG',
'/Uploads/Templates/C Stage/letter to 3P/LOG',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Letter_of_Undetraking_for_deceased_employee.pdf','Letter of Undetraking for deceased employee',
'/Uploads/Templates/C Stage/letter to 3P/Others',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Reinspection_Cover_Letter.pdf','Reinspection Cover Letter',
'/Uploads/Templates/C Stage/letter to 3P/Others',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','reply_to_MP_letter.pdf','reply to MP letter',
'/Uploads/Templates/C Stage/letter to 3P/Others',1,1,1,'C',null,null,null,null,null,null,null)

END
BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','SBS-fax inviting CCTV viewing.pdf','SBS-fax inviting CCTV viewing',
'/Uploads/Templates/C Stage/letter to 3P/Others',1,1,1,'C',null,null,null,null,null,null,null)

END


BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Redirect claim to  3P Insurers.pdf','Redirect claim to 3Ps Insurers',
'/Uploads/Templates/C Stage/letter to 3P/Reject claim',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Referral to Motor Insurers Bureau.pdf','Referral to Motor Insurers Bureau',
'/Uploads/Templates/C Stage/letter to 3P/Reject claim',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Reject Claim.pdf','Reject Claim',
'/Uploads/Templates/C Stage/letter to 3P/Reject claim',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','Cover letter to Insurer for Reimb (III).pdf','Cover letter to Insurer for Reimb (III)',
'/Uploads/Templates/C Stage/letter to 3P/Letter to our insurers',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','letter_to_III_furnishing_CCTV.pdf','letter to III furnishing CCTV',
'/Uploads/Templates/C Stage/letter to 3P/Letter to our insurers',1,1,1,'C',null,null,null,null,null,null,null)

END

BEGIN

INSERT INTO [MNT_TEMPLATE_MASTER]
([Description],[Filename],[Display_Name],[Template_Path],[Is_System_Template],[Template_Format_Id],[Is_Active],[Template_Code],[MappingXML_Path],[MappingXML_FileName],[Has_Dynamic_Data],[Has_Condition],[Has_Dynamic_Footer],
[Has_Footer_Desc],[Has_Dynamic_Header_Footer])

VALUES('Defense','letter_to_our_lawyer_-_with_CCTV_clause.pdf','letter to our lawyer - with CCTV clause',
'/Uploads/Templates/C Stage/letter to 3P/Letter to our lawyer',1,1,1,'C',null,null,null,null,null,null,null)

END


GO