   
/*----------------------------------------------------------  
Proc Name: Dbo.PROC_GET_SUBLOB  
Created by : Aditya  
Date : 15/11/2011  
Purpose  : To get SUBLObs  from MNT_SUB_LOB_MASTER table  
Revison History :  
Used In : Ebix - Advantage  
------------------------------------------------------------  
Date     Review By          Comments  
DROP PROC PROC_GET_SUBLOB 2  
------   ------------       -------------------------*/  
CREATE PROC [dbo].[PROC_GET_SUBLOB]      
(      
 @LANG_ID  int=null      
)      
AS      
BEGIN 
  SELECT MSLM.SUB_LOB_ID AS SUB_LOB_ID,RIGHT('00'+  cast(CAST(MSLM.SUSEP_CODE AS VARCHAR(50)) AS NVARCHAR(50)),2)    
 +' ' + '- ' +ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) AS SUB_LOB_DESC FROM MNT_LOB_MASTER  MLM WITH(NOLOCK) INNER JOIN  
  MNT_SUB_LOB_MASTER MSLM WITH(NOLOCK) ON MLM.LOB_ID = MSLM.LOB_ID
  left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MSLMM WITH(NOLOCK) ON MSLM.LOB_ID = MSLMM.LOB_ID --AND MSLM.SUB_LOB_ID = MSLMM.SUB_LOB_ID
  AND MSLMM.LANG_ID = @LANG_ID
  WHERE MLM.IS_ACTIVE = 'Y' and MSLM.IS_ACTIVE = 'Y' ORDER BY ISNULL(MSLMM.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) ASC
  
  END