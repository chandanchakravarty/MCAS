IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyNumCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyNumCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[Proc_GetAgencyNumCode]
(    
    
 @AGENCY_CODE nvarchar(16)    
)    
    
AS    
BEGIN    
     
 SELECT ISNULL(AGENCY_COMBINED_CODE ,0) AS AGENCY_COMBINED_CODE    
 FROM MNT_AGENCY_LIST  with(nolock)   
 WHERE AGENCY_CODE =@AGENCY_CODE    
    
END   
  
  
  
  
  
  
  
  


GO

