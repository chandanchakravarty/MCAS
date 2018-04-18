  
/*----------------------------------------------------------  
Proc Name: Dbo.Proc_GetSubLOBs  
Created by : Nidhi  
Date : 26/04/2005  
Purpose  : To get SUBLObs  from MNT_SUB_LOB_MASTER table  
Revison History :  
Used In : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
DROP PROC Proc_GetSubLOBs 9,2  
------   ------------       -------------------------*/  
CREATE PROC [dbo].[Proc_GetSubLOBs]      
(      
@LOBID  int,      
@LANG_ID  int=1      
)      
AS      
BEGIN      
IF @LANG_ID=3
BEGIN
SELECT SUB_LOB_ID,SUB_LOB_DESC      
FROM  MNT_SUB_LOB_MASTER
WHERE LOB_ID = 38 

END
ELSE
BEGIN      
SELECT SUBLOB.SUB_LOB_ID SUB_LOB_ID,     
RIGHT('00'+  cast(CAST(SUBLOB.SUSEP_CODE AS VARCHAR(50)) AS NVARCHAR(50)),2)    
+'  ' +isnull(SUBLOBM.SUB_LOB_DESC,SUBLOB.SUB_LOB_DESC) as SUB_LOB_DESC      
FROM  MNT_SUB_LOB_MASTER SUBLOB with(nolock)      
left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL  SUBLOBM with(nolock)      
on SUBLOB.LOB_ID=SUBLOBM.LOB_ID       
and SUBLOB.SUB_LOB_ID=SUBLOBM.SUB_LOB_ID       
AND SUBLOBM.LOB_ID = 38 AND SUBLOBM.LANG_ID=3    
      
WHERE     (SUBLOB.LOB_ID = 38)   ORDER BY  SUB_LOB_DESC      
END   
END