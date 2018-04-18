IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MNT_REIN_GETXML_TIVGROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MNT_REIN_GETXML_TIVGROUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   
/*----------------------------------------------------------                  
Proc Name       : dbo.[MNT_REIN_GETXML_TIVGROUP]                  
Created by      : Harmanjeet Singh                 
Date            : April 27, 2007                
Purpose         : To insert the data into Reinsurer Construction Translation table.                  
Revison History :                  
Used In         : Wolverine           
drop proc dbo.PROC_MNT_REIN_GETXML_TIVGROUP                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE proc [dbo].[PROC_MNT_REIN_GETXML_TIVGROUP]  
(                
 @REIN_TIV_GROUP_ID int    
  )                  
AS                  
BEGIN        
                  
               
    SELECT  
  
 ISNULL(REIN_TIV_GROUP_ID,0) REIN_TIV_GROUP_ID,  
  
 ISNULL(REIN_TIV_CONTRACT_NUMBER,'') REIN_TIV_CONTRACT_NUMBER,  
  REIN_TIV_EFFECTIVE_DATE,        
 ISNULL(REIN_TIV_FROM,'') REIN_TIV_FROM,  
 ISNULL(REIN_TIV_TO,'') REIN_TIV_TO,   
 ISNULL(REIN_TIV_GROUP_CODE,'') REIN_TIV_GROUP_CODE,   
 ISNULL(CREATED_BY,0) CREATED_BY,  
 CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,  
 ISNULL(MODIFIED_BY,0) MODIFIED_BY,  
 CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,        
 ISNULL(IS_ACTIVE,'') IS_ACTIVE  
  
 FROM MNT_REIN_TIV_GROUP  
      
 WHERE  
 REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID;  
   
                 
  END  
  
  


GO

