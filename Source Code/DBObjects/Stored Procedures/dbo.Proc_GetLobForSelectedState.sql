IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobForSelectedState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobForSelectedState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetLobForSelectedState    
Created by         : Mohit Gupta    
Date               : 19/05/2005    
  
Modified by        : Charles Gomes  
Date               : 1/Jun/2010    
Purpose      : Multilingual Support, and @State_Id = 0, (ALL), option handling  
  
DROP PROC Proc_GetLobForSelectedState   
Proc_GetLobForSelectedState 14  
Proc_GetLobForSelectedState 0  
Proc_GetLobForSelectedState 14,2  
Proc_GetLobForSelectedState 0,2  
------   ------------       -------------------------*/    
Create  PROCEDURE [dbo].[Proc_GetLobForSelectedState]        
(        
 @State_Id int,      
 @LangID int =1       
)        
AS        
BEGIN        
 IF @State_Id != 0      
 BEGIN      
  SELECT MLS.LOB_ID ,ISNULL(LMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC       
  FROM MNT_LOB_STATE MLS WITH(NOLOCK)      
  LEFT OUTER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK)         
  ON MLS.LOB_ID=MLM.LOB_ID       
  LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL LMM WITH(NOLOCK)      
  ON LMM.LOB_ID = MLM.LOB_ID AND LMM.LANG_ID = @LangID       
  WHERE MLS.STATE_ID=@State_Id AND MLS.PARENT_LOB IS NULL AND MLM.IS_ACTIVE = 'Y'   --Added by kuldeep for TFS #2487    
  ORDER BY ISNULL(LMM.LOB_DESC,MLM.LOB_DESC)       
 END      
 ELSE      
 BEGIN      
  SELECT MLM.LOB_ID , ISNULL(LMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC FROM MNT_LOB_MASTER MLM WITH(NOLOCK)       
  LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL LMM WITH(NOLOCK)      
  ON LMM.LOB_ID = MLM.LOB_ID AND LMM.LANG_ID = @LangID      
  WHERE MLM.IS_ACTIVE = 'Y'  --Added by kuldeep for TFS #2487  
  ORDER BY ISNULL(LMM.LOB_DESC,MLM.LOB_DESC)       
 END      
END     
GO

