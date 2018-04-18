  
 /*----------------------------------------------------------          
Proc Name   : Dbo.Proc_GetLobAndSubLobWithoutState          
Created by   : Charles Gomes      
Date                : 16/Mar/2010          
Purpose             : To get Lob And SUBLObs from table      

Modified by: Kuldeep/Ruchika
Modified on: 14-Jan-2012
Purpose:	 To retrieve SUB LOBs for Singapore implementation	  
       
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
IF @LANG_ID=3 --If condition added for Singapore implementation on 14-Jan-2012
BEGIN
SELECT SUB_LOBS.LOB_ID AS LOB_ID,LOBS.LOB_DESC,SUB_LOB_ID AS SUB_LOB_ID,SUB_LOB_DESC FROM MNT_SUB_LOB_MASTER  SUB_LOBS INNER JOIN MNT_LOB_MASTER  LOBS ON SUB_LOBS.LOB_ID=LOBS.LOB_ID
END
ELSE
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
END      
--exec [Proc_GetLobAndSubLobWithoutState] 3

