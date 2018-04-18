IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVINFromVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVINFromVINMASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name          : Dbo.Proc_GetVINFromVINMASTER                
Created by           : Nidhi                
Date                    : 04/06/2005                
Purpose               : To get VIN from VINMASTER                
Revison History :                
Used In                :   Wolverine                
                
Modified By : Anurag Verma                
Modified ON : 21/09/2005                
Purpose : fetching anti_lck_brakes field                  
              
Modified By : Sumit Chhabra              
Modified ON : 28/10/2005                
Purpose : fetching symbol field              
------------------------------------------------------------                
Date     Review By          Comments      
drop PROC Dbo.Proc_GetVINFromVINMASTER            
------   ------------       -------------------------*/                
CREATE PROC Dbo.Proc_GetVINFromVINMASTER                
(                
@MODEL_YEAR NVARCHAR (5) ,                
@MAKE_CODE  nvarchar (150),                
@SERIES_NAME nvarchar (150)=null,  
@BODY_TYPE nvarchar (150)=null              
                
)                
AS                
--if(@SERIES_NAME is not null)
if(@SERIES_NAME is not null AND @BODY_TYPE is not null)  --Added condition for Itrack Issue 5831 on 13 May 2009              
                
 BEGIN                
    
/*  SELECT   DISTINCT VIN --, ISNULL(ANTI_LCK_BRAKES,'') ANTI_LCK_BRAKES--, (VIN + '~' + SYMBOL) AS VINSYMBOL              
  FROM         MNT_VIN_MASTER                  
  WHERE    MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE  AND SERIES_NAME=@SERIES_NAME                
 order by VIN   */             
 SELECT V.VIN,(ISNULL(CAST(L.LOOKUP_UNIQUE_ID AS VARCHAR(10)),'') + '~' + V.SYMBOL + '~' + ISNULL(V.ANTI_LCK_BRAKES,'') + '~' + V.SERIES_NAME + '~' + V.BODY_TYPE) AS VINSYMBOL        
  FROM MNT_VIN_MASTER  V  LEFT JOIN MNT_LOOKUP_VALUES L              
 ON V.AIRBAG=L.LOOKUP_VALUE_CODE              
AND L.LOOKUP_ID=20 AND L.IS_ACTIVE='Y' 
  --WHERE    V.MODEL_YEAR= @MODEL_YEAR AND V.MAKE_CODE=@MAKE_CODE  AND V.SERIES_NAME=@SERIES_NAME
             
  WHERE    V.MODEL_YEAR= @MODEL_YEAR AND V.MAKE_CODE=@MAKE_CODE  AND V.SERIES_NAME=@SERIES_NAME AND   
  V.BODY_TYPE=@BODY_TYPE --Added condition for Itrack Issue 5831 on 13 May 2009              
 END   

--Added for Itrack Issue 5831 on 13 May 2009
else if(@SERIES_NAME is not null)  
BEGIN                
    
/*  SELECT   DISTINCT VIN --, ISNULL(ANTI_LCK_BRAKES,'') ANTI_LCK_BRAKES--, (VIN + '~' + SYMBOL) AS VINSYMBOL              
  FROM         MNT_VIN_MASTER                  
  WHERE    MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE  AND SERIES_NAME=@SERIES_NAME                
 order by VIN   */             
 SELECT V.VIN,(ISNULL(CAST(L.LOOKUP_UNIQUE_ID AS VARCHAR(10)),'') + '~' + V.SYMBOL + '~' + ISNULL(V.ANTI_LCK_BRAKES,'') + '~' + V.SERIES_NAME + '~' + V.BODY_TYPE) AS VINSYMBOL        
  FROM MNT_VIN_MASTER  V  LEFT JOIN MNT_LOOKUP_VALUES L              
 ON V.AIRBAG=L.LOOKUP_VALUE_CODE              
AND L.LOOKUP_ID=20 AND L.IS_ACTIVE='Y'              
  WHERE    V.MODEL_YEAR= @MODEL_YEAR AND V.MAKE_CODE=@MAKE_CODE  AND V.SERIES_NAME=@SERIES_NAME               
 END              
                
else                
                
 BEGIN                
                 
/*  SELECT   DISTINCT VIN  --, ISNULL(ANTI_LCK_BRAKES,'') ANTI_LCK_BRAKES --,(VIN + '~' + SYMBOL) AS VINSYMBOL              
  FROM         MNT_VIN_MASTER                  
  WHERE     (MODEL_YEAR= @MODEL_YEAR AND MAKE_CODE=@MAKE_CODE )                
 order by VIN        */        
 SELECT V.VIN,(ISNULL(CAST(L.LOOKUP_UNIQUE_ID AS VARCHAR(10)),'') + '~' + V.SYMBOL + '~' + ISNULL(V.ANTI_LCK_BRAKES,'') + '~' + V.SERIES_NAME + '~' + V.BODY_TYPE) AS VINSYMBOL        
  FROM MNT_VIN_MASTER  V  LEFT JOIN MNT_LOOKUP_VALUES L              
 ON V.AIRBAG=L.LOOKUP_VALUE_CODE              
AND L.LOOKUP_ID=20 AND L.IS_ACTIVE='Y'              
  WHERE     (V.MODEL_YEAR= @MODEL_YEAR AND V.MAKE_CODE=@MAKE_CODE)                
 END 
GO

