IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus' and column_name = 'IsExists') 

BEGIN 

ALTER TABLE MNT_Menus

add IsExists nvarchar(1) default 'Y'

END 


update mnt_menus set hyp_link_address ='CedantIndex,Cedant,/MCAS.Web/Masters/VehicleUploadIndex' where tid=2


update mnt_menus set hyp_link_address ='CedantIndex,Cedant,/MCAS.Web/ClaimMasters/LossNatureMasterList' where tid=3



update mnt_menus set hyp_link_address ='CedantIndex,Cedant,/MCAS.Web/ProductBusiness/ProductBusinessIndex' where tid=5
