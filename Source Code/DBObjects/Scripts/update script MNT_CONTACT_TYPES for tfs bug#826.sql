IF EXISTS (SELECT * FROM MNT_CONTACT_TYPES WHERE CONTACT_TYPE_ID=7 and CONTACT_TYPE_DESC='Tax Entity')
BEGIN
	UPDATE MNT_CONTACT_TYPES SET IS_ACTIVE='N' WHERE CONTACT_TYPE_ID=7 and CONTACT_TYPE_DESC='Tax Entity'
END
