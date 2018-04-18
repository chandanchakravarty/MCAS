IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcGetStateId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcGetStateId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[ProcGetStateId]  

              
as  
BEGIN              
SELECT DISTINCT L.STATE_ID,S.STATE_NAME FROM MNT_LOB_STATE L   
INNER JOIN   
MNT_COUNTRY_STATE_LIST S ON L.STATE_ID=S.STATE_ID 
   
END            
  
    
GO

