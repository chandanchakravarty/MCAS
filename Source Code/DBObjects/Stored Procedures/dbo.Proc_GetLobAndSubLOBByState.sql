IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobAndSubLOBByState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobAndSubLOBByState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------    
Proc Name   : Dbo.Proc_GetLobAndSubLOBByState    
Created by   : Anurag Verma    
Date                 : 19/07/2005    
Purpose             : To get Lob And SUBLObs from MNT_LOB_STATE table according to state    
Revison History:    
Used In             :   Wolverine    
    
Modified By   : Anurag verma    
Modified On   : 26/07/2005     
Purpose    : changing Query

Modified By   : Sonal
Modified On   : 17/06/2010
Purpose    : implemet according state and LOb id to fetch subloIDS

------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_GetLobAndSubLOBByState 0,9,1

CREATE PROC [dbo].[Proc_GetLobAndSubLOBByState] 
( 
@STATE_ID INT = null, 
@LOB_ID INT=null,
@LANG_ID INT =1
)   
AS    
BEGIN  

  IF (@STATE_ID is not null  AND  @LOB_ID is not null) 
	BEGIN
  
      IF (@STATE_ID = 0 AND @LOB_ID!=0)
       BEGIN
             SELECT Distinct MLS.parent_lob LOB_ID,MLM.LOB_DESC,MLS.LOB_ID SUB_LOB_ID, ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) as SUB_LOB_DESC,'0' as STATE_ID     
			 FROM MNT_LOB_STATE MLS    
			 inner JOIN MNT_LOB_MASTER MLM ON MLS.PARENT_LOB=MLM.lob_id    
			 inner JOIN MNT_SUB_LOB_MASTER MSLM ON MLS.LOB_ID=MSLM.Sub_lob_ID  and MLS.PARENT_LOB=MSLM.lob_id    
			 inner join MNT_COUNTRY_STATE_LIST ON  MNT_COUNTRY_STATE_LIST.STATE_ID = MLS.STATE_ID  
			 left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MSLMM ON MLS.LOB_ID=MSLMM.Sub_lob_ID  and MLS.PARENT_LOB=MSLMM.lob_id and MSLMM.LANG_ID=@LANG_ID
			 WHERE MLS.PARENT_LOB IS NOT NULL AND MSLM.LOB_ID=@LOB_ID ORDER BY LOB_DESC 
        END
        
        ELSE IF (@STATE_ID != 0  AND @LOB_ID=0)
        BEGIN
			 SELECT Distinct MLS.parent_lob LOB_ID,MLM.LOB_DESC,MLS.LOB_ID SUB_LOB_ID,ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) as SUB_LOB_DESC,MLS.STATE_ID     
			 FROM MNT_LOB_STATE MLS    
			 inner JOIN MNT_LOB_MASTER MLM ON MLS.PARENT_LOB=MLM.lob_id    
			 inner JOIN MNT_SUB_LOB_MASTER MSLM ON MLS.LOB_ID=MSLM.Sub_lob_ID  and MLS.PARENT_LOB=MSLM.lob_id    
			 inner join MNT_COUNTRY_STATE_LIST ON  MNT_COUNTRY_STATE_LIST.STATE_ID = MLS.STATE_ID 
			 left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MSLMM ON MLS.LOB_ID=MSLMM.Sub_lob_ID  and MLS.PARENT_LOB=MSLMM.lob_id and MSLMM.LANG_ID=@LANG_ID
			 WHERE MLS.PARENT_LOB IS NOT NULL  AND MLS.STATE_ID=@STATE_ID ORDER BY LOB_DESC 
        END
        
        ELSE
        BEGIN
             SELECT Distinct MLS.parent_lob LOB_ID,MLM.LOB_DESC,MLS.LOB_ID SUB_LOB_ID,ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) as SUB_LOB_DESC,MLS.STATE_ID     
			 FROM MNT_LOB_STATE MLS    
			 inner JOIN MNT_LOB_MASTER MLM ON MLS.PARENT_LOB=MLM.lob_id    
			 inner JOIN MNT_SUB_LOB_MASTER MSLM ON MLS.LOB_ID=MSLM.Sub_lob_ID  and MLS.PARENT_LOB=MSLM.lob_id    
			 inner join MNT_COUNTRY_STATE_LIST ON  MNT_COUNTRY_STATE_LIST.STATE_ID = MLS.STATE_ID  
			 left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MSLMM ON MLS.LOB_ID=MSLMM.Sub_lob_ID  and MLS.PARENT_LOB=MSLMM.lob_id and MSLMM.LANG_ID=@LANG_ID
			 WHERE MLS.PARENT_LOB IS NOT NULL  AND MLS.STATE_ID=@STATE_ID AND MSLM.LOB_ID=@LOB_ID ORDER BY LOB_DESC 
           
        END 
      
   END
  ELSE
   BEGIN 
		SELECT MLS.parent_lob LOB_ID,MLM.LOB_DESC,MLS.LOB_ID SUB_LOB_ID,ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) as SUB_LOB_DESC,MLS.STATE_ID     
		FROM MNT_LOB_STATE MLS    
		inner JOIN MNT_LOB_MASTER MLM ON MLS.PARENT_LOB=MLM.lob_id    
		inner JOIN MNT_SUB_LOB_MASTER MSLM ON MLS.LOB_ID=MSLM.Sub_lob_ID  and MLS.PARENT_LOB=MSLM.lob_id    
		inner join MNT_COUNTRY_STATE_LIST ON  MNT_COUNTRY_STATE_LIST.STATE_ID = MLS.STATE_ID  
		left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MSLMM ON MLS.LOB_ID=MSLMM.Sub_lob_ID  and MLS.PARENT_LOB=MSLMM.lob_id and MSLMM.LANG_ID=@LANG_ID
		WHERE MLS.PARENT_LOB IS NOT NULL ORDER BY LOB_DESC 
   END   
END    
  
  
  
  
  
  
  
  
GO

