update MNT_Menus set Hyp_Link_Address='/Home/Index',IsActive='Y' where MenuId in (282,283,284)

update MNT_Menus set IsActive='Y' where MenuId =281

update MNT_Menus set MainHeaderId=281 where MenuId = 282

update MNT_Menus set IsActive='N' where MenuId between 201 and 204