IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBillTypeForAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBillTypeForAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
   
Proc Name       : dbo.Proc_GetBillTypeForAgency   
Created by      : Praveen     
Date            : 24 June 2008  
Purpose         : To get list  Agency Bill Type    
Revison History :    
  
--drop proc dbo.Proc_GetBillTypeForAgency  
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
Create PROC [dbo].[Proc_GetBillTypeForAgency]   
(  
 @AGENCY_ID INT,  
 @CALLED_FROM varchar(5), 
 @Lang_id int=1
)  
as  
  
CREATE TABLE #BILL_TEMP   
(  
 LOOKUP_UNIQUE_ID  bigint,  
 LOOKUP_VALUE_DESC varchar(200),  
 LOOKUP_VALUE_CODE varchar(100)  
)  
  
IF(@CALLED_FROM = 'NEW')  
BEGIN  
SELECT MLV.LOOKUP_UNIQUE_ID,    
       isnull(MLVT.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)LOOKUP_VALUE_DESC,    
       MLV.LOOKUP_VALUE_CODE    
       FROM MNT_LOOKUP_VALUES MLV  
  left Outer Join MNT_LOOKUP_VALUES_MULTILINGUAL MLVT ON     
  MLV.LOOKUP_UNIQUE_ID = MLVT.LOOKUP_UNIQUE_ID and LANG_ID=@Lang_id
  INNER JOIN MNT_LOOKUP_TABLES MLT ON     
 MLV.LOOKUP_ID = MLT.LOOKUP_ID   
   left Outer Join MNT_LOOKUP_TABLES_MULTILINGUAL MLTV ON     
 MLTV.LOOKUP_ID = MLT.LOOKUP_ID and MLTV.LANG_ID=@Lang_id   
 WHERE MLT.LOOKUP_NAME = 'BLCODE' AND MLV.LOOKUP_UNIQUE_ID IN(11150,8459,11191) 
 AND MLV.IS_ACTIVE = 'Y'     -- MLV.IS_ACTIVE = 'Y' -Added by Sibin to remove "Agency Bill All Terms" from combo box
      
 --ORDER BY MLV.LOOKUP_VALUE_DESC ASC    
END  
  
ELSE  
BEGIN  
 INSERT #BILL_TEMP   
 SELECT agency_bill_type AS LOOKUP_UNIQUE_ID, MLV.LOOKUP_VALUE_DESC + '-(Inactive)',    
 MLV.LOOKUP_VALUE_CODE  FROM MNT_AGENCY_LIST MNT  
 LEFT JOIN MNT_LOOKUP_VALUES MLV  
 ON ISNULL(MNT.AGENCY_BILL_TYPE,0) = MLV.LOOKUP_UNIQUE_ID  
 WHERE AGENCY_ID = @AGENCY_ID  
 AND MLV.IS_ACTIVE = 'N'  
   
  
 SELECT MLV.LOOKUP_UNIQUE_ID,    
 isnull(MLVT.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)LOOKUP_VALUE_DESC,    
 MLV.LOOKUP_VALUE_CODE    
 FROM MNT_LOOKUP_VALUES MLV
 left Outer Join MNT_LOOKUP_VALUES_MULTILINGUAL MLVT ON     
  MLV.LOOKUP_UNIQUE_ID = MLVT.LOOKUP_UNIQUE_ID and LANG_ID=@Lang_id    
 INNER JOIN MNT_LOOKUP_TABLES MLT ON     
 MLV.LOOKUP_ID = MLT.LOOKUP_ID  
 left Outer Join MNT_LOOKUP_TABLES_MULTILINGUAL MLTV ON     
 MLTV.LOOKUP_ID = MLT.LOOKUP_ID and MLTV.LANG_ID=@Lang_id    
 WHERE MLT.LOOKUP_NAME = 'BLCODE' AND MLV.LOOKUP_UNIQUE_ID IN(11150,8459,11191)   
 AND MLV.IS_ACTIVE = 'Y'     
    
 UNION  
  
 SELECT * FROM #BILL_TEMP  
 ORDER BY LOOKUP_VALUE_DESC ASC     
END  
  
--select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 8459  
  
--update MNT_LOOKUP_VALUES set is_Active='N' where LOOKUP_UNIQUE_ID =8459  
  
  
  
  
  
GO

