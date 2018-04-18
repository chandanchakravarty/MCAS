IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetASLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetASLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetASLOB            
Created by      : Swarup                    
Date            : 28 Mar,2007                    
Purpose         : To Get The ASLOB Codes                    
Revison History :                    
Used In         : Wolverine                    
MODIFIED BY		: Agniswar
Date Modified	: 23 SEP 2011  
Used In			: EAW BRAZIL and EAW SINGAPORE                  
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
        
*/     
-- DROP PROC dbo.Proc_GetASLOB 1                    
CREATE  PROC [dbo].[Proc_GetASLOB]  (
@LANG_ID INT
)                   
AS      
BEGIN    
/*  Changed by Charles on 12-Apr-10 for Multilingual Support              
SELECT LOOKUP_UNIQUE_ID,(LOOKUP_VALUE_CODE + ' - ' + LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC     
FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID=1336  ORDER BY LOOKUP_VALUE_CODE    
*/  
--(SELECT LOOKUP_UNIQUE_ID,(LOOKUP_VALUE_CODE + ' - ' + LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC, 1 AS LANG_ID    
--FROM MNT_LOOKUP_VALUES WITH(NOLOCK)   
--WHERE LOOKUP_ID=1336   
--UNION  
--SELECT MLVL.LOOKUP_UNIQUE_ID,(ML.LOOKUP_VALUE_CODE + ' - ' + MLVL.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC ,MLVL.LANG_ID  
--FROM MNT_LOOKUP_VALUES_MULTILINGUAL MLVL WITH(NOLOCK)  
--LEFT OUTER JOIN MNT_LOOKUP_VALUES ML ON ML.LOOKUP_UNIQUE_ID = MLVL.LOOKUP_UNIQUE_ID  
--WHERE ML.LOOKUP_ID=1336) ORDER BY LOOKUP_UNIQUE_ID    


 SELECT LV.LOOKUP_UNIQUE_ID,  
 case when @LANG_ID = 2 then LTRIM(RTRIM((LOOKUP_VALUE_CODE + ' - ' + LVM.LOOKUP_VALUE_DESC))) else LTRIM(RTRIM((LOOKUP_VALUE_CODE + ' - ' + LV.LOOKUP_VALUE_DESC))) END AS LOOKUP_VALUE_DESC, ISNULL(LANG_ID,@LANG_ID) AS LANG_ID        
 FROM MNT_LOOKUP_VALUES LV WITH(NOLOCK)     
 LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL LVM WITH(NOLOCK)    
 ON LV.LOOKUP_UNIQUE_ID = LVM.LOOKUP_UNIQUE_ID     
 and LANG_ID = @LANG_ID    
 WHERE LV.LOOKUP_VALUE_DESC is NOT NULL  and LV.LOOKUP_ID = 1336  
     
 ORDER BY LV.LOOKUP_VALUE_DESC  
	
END                
      
    
    
  
  
  
  
GO

