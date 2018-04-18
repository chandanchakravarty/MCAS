update MNT_Menus set Hyp_Link_Address='/ClaimProcessing/TacFileUploadEditor?claimMode=Upload' where MenuId=270



update MNT_Menus set DisplayTitle='Claim Upload',AdminDisplayText='Claim Upload',Hyp_Link_Address='/ClaimProcessing/ClaimFileUploadEditor?claimMode=CUpload' where MenuId=271


update MNT_Menus set IsActive='Y' where MenuId in(269,270,271)