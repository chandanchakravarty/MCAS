IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobAndSubLobWithoutState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobAndSubLobWithoutState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name   : Dbo.Proc_GetLobAndSubLobWithoutState        
Created by   : Charles Gomes    
Date                : 16/Mar/2010        
Purpose             : To get Lob And SUBLObs from table    
     
Revison History:        
Used In             :   Wolverine            
    
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetLobAndSubLobWithoutState 2       
CREATE PROC [dbo].[Proc_GetLobAndSubLobWithoutState]  
@LANG_ID INT = 1     --Added by Charles on 12-Apr-10 for Multilingual Implementation   
AS        
BEGIN        
 SELECT MLM.LOB_ID,ISNULL(MLML.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC,MSLM.SUB_LOB_ID,RIGHT('00'+  cast(CAST(MSLM.SUSEP_CODE AS VARCHAR(50)) AS NVARCHAR(50)),2)  
 +' ' +ISNULL(MSLML.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) AS SUB_LOB_DESC  
 FROM MNT_LOB_MASTER MLM WITH(NOLOCK)    
 LEFT OUTER JOIN MNT_SUB_LOB_MASTER MSLM WITH(NOLOCK)    
 ON MSLM.LOB_ID=MLM.LOB_ID    
 LEFT OUTER  JOIN MNT_SUB_LOB_MASTER_MULTILINGUAL MSLML  WITH(NOLOCK)  
  ON MSLM.LOB_ID = MSLML.LOB_ID AND MSLM.SUB_LOB_ID = MSLML.SUB_LOB_ID AND MSLML.LANG_ID = @LANG_ID    
 LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLML WITH(NOLOCK)  
  ON MLM.LOB_ID = MLML.LOB_ID AND MLML.LANG_ID = @LANG_ID  
 WHERE MSLM.SUB_LOB_ID IS NOT NULL    
 ORDER BY LOB_DESC  
END        
GO

