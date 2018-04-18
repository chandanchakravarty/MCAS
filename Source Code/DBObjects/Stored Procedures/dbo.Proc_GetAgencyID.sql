IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.Proc_GetAgencyID 'w001' 
CREATE  PROC dbo.Proc_GetAgencyID  
(  
  
 @AGENCY_CODE nvarchar(16)  
)  
  
AS  
BEGIN  
   
 SELECT ISNULL(AGENCY_ID ,0) AS AGENCY_ID , @AGENCY_CODE+' - ' +  ISNULL(AGENCY_DISPLAY_NAME,'') AS AGENCY_DISP_NAME ,IS_ACTIVE,ISNULL(AGENCY_DISPLAY_NAME,'') AS AGENCY_DISPLAY_NAME 
 FROM MNT_AGENCY_LIST   
 WHERE AGENCY_CODE =@AGENCY_CODE  
  
END 










GO

