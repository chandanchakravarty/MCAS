
IF EXISTS (SELECT 1 FROM [MNT_Menus] WHERE TId=2 AND MenuId=101)
BEGIN
UPDATE [MNT_Menus] SET Hyp_Link_Address='/Masters/VehicleListingMaster' WHERE TId=2 AND MenuId=101
END