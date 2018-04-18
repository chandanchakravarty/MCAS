update MNT_Lookups set Lookupdesc='3rd Party''s Documents',[Description]='3rd Party''s Documents' where LookupID=253
update MNT_Lookups set Lookupdesc='Insured''s Documents',[Description]='Insured''s Documents' where LookupID=254
update MNT_Lookups set Lookupdesc='Correspondences',[Description]='Correspondences' where LookupID=255
update MNT_Lookups set Lookupdesc='Internal Documents',[Description]='Internal Documents' where LookupID=256

update MNT_Menus set DisplayTitle='3rd Party''s Documents' where MenuId=221
update MNT_Menus set DisplayTitle='Insured''s Documents' where MenuId=222
update MNT_Menus set DisplayTitle='Correspondences' where MenuId=223
update MNT_Menus set DisplayTitle='Internal Documents' where MenuId=224

update MNT_Menus set AdminDisplayText='3rd Party''s Documents' where MenuId=221
update MNT_Menus set AdminDisplayText='Insured''s Documents' where MenuId=222
update MNT_Menus set AdminDisplayText='Correspondences' where MenuId=223
update MNT_Menus set AdminDisplayText='Internal Documents' where MenuId=224