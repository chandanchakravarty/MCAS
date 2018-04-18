IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectLOB_PI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectLOB_PI]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Proc_SelectLOB_PI 14
 drop proc dbo.Proc_SelectLOB_PI    
*/
CREATE PROCEDURE dbo.Proc_SelectLOB_PI    
(    
 @STATE_ID INT    
 )    
AS    
BEGIN    
if @STATE_ID<>0    
begin    
 SELECT DISTINCT MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS    
 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID     
 WHERE MLS.LOB_ID in (1,6) AND STATE_ID=@STATE_ID ORDER BY LOB_DESC     
end    
else    
begin    
 SELECT distinct MLM.LOB_ID,MLM.LOB_DESC FROM MNT_LOB_STATE MLS    
 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID     
 WHERE MLS.PARENT_LOB IS null AND     
 STATE_ID in (select STATE_ID from MNT_COUNTRY_STATE_LIST WHERE IS_ACTIVE='Y')    
 ORDER BY LOB_DESC    
end    
END   




GO

