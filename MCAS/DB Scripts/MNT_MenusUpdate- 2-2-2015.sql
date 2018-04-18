IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus') 

BEGIN 

update MNT_Menus set DisplayTitle ='File Upload',AdminDisplayText='File Upload' where MenuId=270
update MNT_Menus set DisplayTitle ='Claim Upload',AdminDisplayText='Claim Upload' where MenuId=271

END 