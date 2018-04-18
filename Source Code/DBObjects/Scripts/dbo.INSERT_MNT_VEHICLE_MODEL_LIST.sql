INSERT INTO [dbo].[MNT_VEHICLE_MODEL_LIST]
           ([ID]
           ,[MAKER_LOOKUP_ID]
           ,[MODEL]
           ,[MODEL_TYPE]
           ,[MODEL_YEAR]
           ,[EFFECTIVE_FROM_DATE]
           ,[EFFECTIVE_TO_DATE]
           ,[IS_ACTIVE]
           ,[LAST_UPDATED_DATETIME])

SELECT	[SL No#],
		[LOOKUP_UNIQUE_ID],
		[Vehicle Model],
		[Body Type ],
		NULL,
		[Effective From],
		[Effective To],
		'Y',
		GETDATE()
FROM TEMP_VEH_MODEL_LIST ML
INNER JOIN MNT_LOOKUP_VALUES LV
on ML.[Vehicle Make] = LV.LOOKUP_VALUE_DESC
where LV.LOOKUP_ID = 1308
		
GO

