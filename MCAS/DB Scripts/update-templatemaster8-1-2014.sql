BEGIN

update MNT_TEMPLATE_MASTER
set MappingXML_Path='/Support/MAPXML/C Stage/Discharge voucher',
--dispaly_name='Cheque_Cancellation_Or_Reissue_Memo_(1)',
MappingXML_FileName='Authority_to_Recover_PI_Payout.xml'
where template_id=1

END


BEGIN

update MNT_TEMPLATE_MASTER
set MappingXML_Path='/Support/MAPXML/C Stage/Discharge voucher',
--dispaly_name='Cheque_Cancellation_Or_Reissue_Memo_(1)',
MappingXML_FileName='DV_(Individuals).xml'
where template_id=2

END


BEGIN

update MNT_TEMPLATE_MASTER
set MappingXML_Path='/Support/MAPXML/C Stage/Discharge voucher',
--dispaly_name='Cheque_Cancellation_Or_Reissue_Memo_(1)',
MappingXML_FileName='DV (Infant).xml'
where template_id=3

END


BEGIN

update MNT_TEMPLATE_MASTER
set MappingXML_Path='/Support/MAPXML/C Stage/Discharge voucher',
--dispaly_name='Cheque_Cancellation_Or_Reissue_Memo_(1)',
MappingXML_FileName='DV_Interim_(via_email).xml'
where template_id=4

END