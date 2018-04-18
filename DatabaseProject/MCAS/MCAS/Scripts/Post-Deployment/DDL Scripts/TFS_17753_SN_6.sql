update MNT_TEMPLATE_MASTER set HasPartyType='Y' where Template_Id not in (2,3,4)

update MNT_TEMPLATE_MASTER set HasPartyType='N' where Template_Id in (2,3,4)