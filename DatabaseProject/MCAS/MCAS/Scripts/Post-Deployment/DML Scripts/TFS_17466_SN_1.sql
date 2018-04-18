-- =============================================
-- Script Template
-- =============================================
Update MNT_Lookups Set Lookupvalue = 1 where Lookupdesc = 'Own' and Category = 'InsurerType'
Update MNT_Lookups Set Lookupvalue = 2 where Lookupdesc = 'TP' and Category = 'InsurerType'
Update MNT_Lookups Set Lookupvalue = 3 where Lookupdesc = 'Both' and Category = 'InsurerType'

Update MNT_Cedant Set InsurerType = '3' where InsurerType = 2
Update MNT_Cedant Set InsurerType = '2' where InsurerType = 1
Update MNT_Cedant Set InsurerType = '1' where InsurerType = 0

Update MNT_Adjusters Set InsurerType = '3' where InsurerType = 2 and AdjusterCode Like 'SVY%'
Update MNT_Adjusters Set InsurerType = '2' where InsurerType = 1 and AdjusterCode Like 'SVY%'
Update MNT_Adjusters Set InsurerType = '1' where InsurerType = 0 and AdjusterCode Like 'SVY%'

Update MNT_Adjusters Set InsurerType = '3' where InsurerType = 2 and AdjusterCode Like 'SOL%'
Update MNT_Adjusters Set InsurerType = '2' where InsurerType = 1 and AdjusterCode Like 'SOL%'
Update MNT_Adjusters Set InsurerType = '1' where InsurerType = 0 and AdjusterCode Like 'SOL%'

Update MNT_DepotMaster Set WorkShopType = '3' where WorkShopType = 2
Update MNT_DepotMaster Set WorkShopType = '2' where WorkShopType = 1
Update MNT_DepotMaster Set WorkShopType = '1' where WorkShopType = 0