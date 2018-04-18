  IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId=212)
     BEGIN
         update MNT_Menus set Hyp_Link_Address='/ClaimProcessing/ClaimPaymentProcessing?claimMode=IncompletePayment'  where MenuId ='212'
     END