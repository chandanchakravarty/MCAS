IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_Cedant_CompanyName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_Cedant_CompanyName]
GO


CREATE PROCEDURE [dbo].[Proc_GetMNT_Cedant_CompanyName]
@w_InsurerType char(1),
@w_PartyTypeId nvarchar(10)
AS
SET FMTONLY OFF;
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
DROP TABLE #mytemptable
CREATE TABLE #mytemptable(
CedantId int not null,
CedantName nvarchar(200))

BEGIN

IF @w_PartyTypeId = '1'
Begin
Insert INTO #mytemptable (CedantId,CedantName) select CedantId ,CedantName from MNT_Cedant where InsurerType in(@w_InsurerType,'2')
END

IF @w_PartyTypeId = '2'
Begin
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SVY%' and InsurerType in (@w_InsurerType,'2')
END

IF @w_PartyTypeId = '3'
Begin
Insert INTO #mytemptable (CedantId,CedantName) select AdjusterId,AdjusterName from MNT_Adjusters where AdjusterCode Like 'SOL%' and InsurerType in(@w_InsurerType,'2')
END

IF @w_PartyTypeId = '4'
Begin
Insert INTO #mytemptable (CedantId,CedantName) select DepotId,CompanyName from MNT_DepotMaster
 where WorkShopType in(@w_InsurerType,'2')
END

select CedantId,CedantName from #mytemptable

END

GO


