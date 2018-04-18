IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMakeFromVINMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMakeFromVINMASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetMakeFromVINMASTER    
Created by           : Nidhi    
Date                    : 29/04/2005    
Purpose               : To get Make from VINMASTER    
Revison History :    
Used In                :   Wolverine    
Modified By  : Anurag Verma    
Modified ON : 29/06/2005    
Purpose : changing query with using isnull check    
      
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    

--drop proc dbo.Proc_GetMakeFromVINMASTER

CREATE PROC dbo.Proc_GetMakeFromVINMASTER    
(    
@MODEL_YEAR NVARCHAR (5)    
    
)    
AS    
    
BEGIN    
-- SELECT  distinct MAKE_CODE, isnull(MAKE_CODE,'') +' - ' + isnull(MAKE_NAME,'') AS MAKE    
 SELECT  distinct MAKE_CODE,   
 --isnull(MAKE_NAME,'') AS MAKE    
 ISNULL(V.LOOKUP_VALUE_DESC,'') AS MAKE  
 FROM   
  MNT_VIN_MASTER  M  
 LEFT OUTER JOIN   
  MNT_LOOKUP_VALUES V   
 ON   
  V.LOOKUP_VALUE_CODE = M.MAKE_CODE  
  AND  
  V.LOOKUP_ID = 1308  
 WHERE      
  (MODEL_YEAR= @MODEL_YEAR)   
  order by MAKE    
END    


  
GO

