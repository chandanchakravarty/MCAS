IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_SelectLOB   

create  PROC [dbo].[Proc_SelectLOB]            
(            
 @STATE_ID INT = null,
 @Lang_Id INT = null       
 )            
AS            
BEGIN            
if (@STATE_ID <>0)           
begin            
 SELECT DISTINCT MLM.LOB_ID,ISNULL(MLMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC,MLM.LOB_CODE FROM MNT_LOB_STATE MLS            
 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID   
 LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLMM ON MLMM.LOB_ID=MLS.LOB_ID AND MLMM.LANG_ID=@Lang_Id
 WHERE MLS.PARENT_LOB IS NULL AND STATE_ID=@STATE_ID AND MLM.IS_ACTIVE='Y' ORDER BY LOB_DESC            
end   
  
 else if (@STATE_ID=0)      
  begin  
  SELECT DISTINCT MLM.LOB_ID,ISNULL(MLMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC,MLM.LOB_CODE FROM MNT_LOB_MASTER MLM          
  LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLMM ON MLMM.LOB_ID=MLM.LOB_ID AND MLMM.LANG_ID=@Lang_Id WHERE MLM.IS_ACTIVE='Y'
  ORDER BY LOB_DESC    
  end     
else            
begin            
 SELECT DISTINCT MLM.LOB_ID,ISNULL(MLMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC,MLS.STATE_ID ,MLM.LOB_CODE FROM MNT_LOB_STATE MLS            
 INNER JOIN MNT_LOB_MASTER MLM ON MLS.LOB_ID=MLM.LOB_ID 
 LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLMM ON MLMM.LOB_ID=MLS.LOB_ID AND MLMM.LANG_ID=@Lang_Id        
 WHERE MLS.PARENT_LOB IS null AND             
 STATE_ID in (select STATE_ID from MNT_COUNTRY_STATE_LIST WHERE IS_ACTIVE='Y') AND MLM.IS_ACTIVE='Y'            
 ORDER BY LOB_DESC            
end            
END       
  
  
        
    
    
GO

