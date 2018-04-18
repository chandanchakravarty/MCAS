
If Exists(SELECT 1 FROM MNT_Menus WHERE MenuId = 206)
 Begin
Update MNT_Menus set Hyp_Link_Address = '/ClaimProcessing/NewClaimProcessing?policyId=0&claimMode=New' where MenuId = 206
 End