﻿IF OBJECT_ID('dbo.[DF_STG_TAC_IP_IsProcessed]', 'D') IS NULL 
ALTER TABLE [dbo].[STG_TAC_IP] ADD  CONSTRAINT [DF_STG_TAC_IP_IsProcessed]  DEFAULT ((0)) FOR [IsProcessed]