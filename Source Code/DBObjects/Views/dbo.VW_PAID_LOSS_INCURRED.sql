IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_PAID_LOSS_INCURRED]'))
DROP VIEW [dbo].[VW_PAID_LOSS_INCURRED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP VIEW VW_PAID_LOSS_INCURRED  
CREATE VIEW [dbo].[VW_PAID_LOSS_INCURRED]  
AS    
 SELECT STATE_ID,AGENCY_ID,LOB_ID,CLAIM_ID,INCURRED_YEAR,YEAR_NUMBER,MONTH_NUMBER,sum(BEGIN_RESERVE) BEGIN_RESERVE,sum(END_RESERVE) END_RESERVE,  
 sum(LOSSES_INCURRED) as LOSSES_INCURRED,sum(LOSS_PAID) as LOSS_PAID  
 FROM VW_PAID_LOSS_INCURRED_BY_COVERAGE WITH(NOLOCK) 
 GROUP BY  STATE_ID,AGENCY_ID,LOB_ID,CLAIM_ID,INCURRED_YEAR,YEAR_NUMBER,MONTH_NUMBER--,BEGIN_RESERVE,END_RESERVE  
 --LOSSES_INCURRED,LOSS_PAID

GO

