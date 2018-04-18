IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRemainingLOBForLossCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRemainingLOBForLossCodes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 /*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetRemainingLOBForLossCodes           
Created by  : Sumit Chhabra            
Date        : 12 May ,2006    
Purpose     : To fetch LOBs not yet assigned to LOSS CODES    
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
 Drop proc   dbo.Proc_GetRemainingLOBForLossCodes         
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_GetRemainingLOBForLossCodes  
@Lang_Id int=1          
AS           
BEGIN            
 SELECT            
  ML.LOB_ID,isnull(MLM.LOB_DESC,ML.LOB_DESC)LOB_DESC           
 FROM  MNT_LOB_MASTER ML  
 left outer join MNT_LOB_MASTER_MULTILINGUAL  MLM  on MLM.LOB_ID=ML.LOB_ID and MLM.LANG_ID=@Lang_Id
 WHERE     
  ML.LOB_ID NOT IN    
 (SELECT LOB_ID FROM CLM_LOSS_CODES) and ML.IS_ACTIVE<>'N'
 --ORDER BY ML.LOB_DESC      
                      
End     