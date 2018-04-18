IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 100)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Cedant.Cedant'
  WHERE MenuId = 100
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 106)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.DepotMaster'
  WHERE MenuId = 106
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 109)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.AdjusterMasters.Surveyor'
  WHERE MenuId = 109
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 115)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.CountryMaster.Country'
  WHERE MenuId = 115
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 125)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.CountryMaster.OrgCountry'
  WHERE MenuId = 125
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 127)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.LOU'
  WHERE MenuId = 127
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 128)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimMasters.Hospital'
  WHERE MenuId = 128
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 131)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor'
  WHERE MenuId = 131
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 132)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider'
  WHERE MenuId = 132
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 133)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimNotesEditor'
  WHERE MenuId = 133
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 134)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.TaskEditor'
  WHERE MenuId = 134
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 135)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAttachmentsEditor'
  WHERE MenuId = 135
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 136)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor'
  WHERE MenuId = 136
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 137)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimReserve'
  WHERE MenuId = 137
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 138)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimMandate'
  WHERE MenuId = 138
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 139)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment'
  WHERE MenuId = 139
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 206)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAccident'
  WHERE MenuId = 206
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 207)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAccident'
  WHERE MenuId = 207
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 208)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAccident'
  WHERE MenuId = 208
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 220)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimMasters.Hospital'
  WHERE MenuId = 220
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 231)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Cedant.Cedant'
  WHERE MenuId = 231
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 236)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.DepotMaster'
  WHERE MenuId = 236
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 241)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.AdjusterMasters.Surveyor'
  WHERE MenuId = 241
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 246)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.CountryMaster.Country'
  WHERE MenuId = 246
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 253)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.CountryMaster.OrgCountry'
  WHERE MenuId = 253
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 255)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.LOU'
  WHERE MenuId = 255
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 259)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserGroupsIndex'
  WHERE MenuId = 259
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 260)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserGroupsIndex'
  WHERE MenuId = 260
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 261)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserAdminMasters'
  WHERE MenuId = 261
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 262)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserAdminMasters'
  WHERE MenuId = 262
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 263)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserDeptIndex'
  WHERE MenuId = 263
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 264)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.UserDeptIndex'
  WHERE MenuId = 264
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 277)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.InterChange'
  WHERE MenuId = 277
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 278)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.InterChange'
  WHERE MenuId = 278
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 279)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.Deductible'
  WHERE MenuId = 279
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 280)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.Deductible'
  WHERE MenuId = 280
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 283)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor'
  WHERE MenuId = 283
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 294)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimRecoveryProcessing.ClaimRecoveryEditor'
  WHERE MenuId = 294
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 295)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.FAL'
  WHERE MenuId = 295
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 296)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.FAL'
  WHERE MenuId = 296
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 297)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.CommonMaster'
  WHERE MenuId = 297
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 298)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.CommonMaster'
  WHERE MenuId = 298
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 299)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.VehicleListingMaster'
  WHERE MenuId = 299
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 300)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.VehicleListingMaster'
  WHERE MenuId = 300
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 301)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.BusCaptain'
  WHERE MenuId = 301
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 302)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.Masters.BusCaptain'
  WHERE MenuId = 302
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 303)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.ClaimProcessing.ClaimLogRequest'
  WHERE MenuId = 303
END
IF EXISTS (SELECT TOP 1
    *
  FROM MNT_Menus
  WHERE MenuId = 304)
BEGIN
  UPDATE MNT_Menus
  SET VirtualSource = 'MCAS.Web.Objects.Resources.UserAdmin.OrganizationAccess'
  WHERE MenuId = 304
END