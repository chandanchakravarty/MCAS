IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProducers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProducers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
-- exec Proc_GetProducers    
-- drop proc dbo.Proc_GetProducers    
*/  
CREATE PROC dbo.Proc_GetProducers    
@AgencyID int    
AS    
 BEGIN    
--  DECLARE @USER_TYPE_PRODUCER VARCHAR(10)    
--  DECLARE @USER_TYPE_CSR VARCHAR(10)    
 DECLARE @WOLV_ID INT   
  
--  SET @USER_TYPE_PRODUCER= 'PRO'    
--  SET @USER_TYPE_CSR= 'CSR'    
 SET @WOLV_ID = 27  
   
 -- If logged in agency is not Wolverine then show agency specific data   
 IF @AgencyID <> @WOLV_ID  
   BEGIN  
  SELECT UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME AS USERNAME,(ISNULL(CAST(UL.USER_ID AS VARCHAR),'') +  ' ' + ISNULL(UL.USER_FNAME,'') + ' ' + ISNULL(UL.USER_LNAME,'')) AS USER_NAME_ID    
    ,UL.IS_ACTIVE    
  FROM MNT_USER_LIST UL  
  INNER JOIN MNT_AGENCY_LIST MAL ON MAL.AGENCY_CODE  = UL.USER_SYSTEM_ID    
  WHERE MAL.AGENCY_ID = @AGENCYID  ORDER BY USERNAME   
   END  
 ELSE  
   BEGIN  
  -- Users will be displayed in CSR combo for the agency, no matter whether deactivated or activated.  
  SELECT UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME AS USERNAME,(ISNULL(CAST(UL.USER_ID AS VARCHAR),'') +  ' ' + ISNULL(UL.USER_FNAME,'') + ' ' + ISNULL(UL.USER_LNAME,'')) AS USER_NAME_ID    
   ,UL.IS_ACTIVE    
  FROM MNT_USER_LIST UL  
  INNER JOIN MNT_AGENCY_LIST MAL ON MAL.AGENCY_CODE  = UL.USER_SYSTEM_ID   ORDER BY USERNAME  
   END  
--  SELECT     UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME AS USERNAME,(ISNULL(CAST(UL.USER_ID AS VARCHAR),'') +  ' ' + ISNULL(UL.USER_FNAME,'') + ' ' + ISNULL(UL.USER_LNAME,'')) AS USER_NAME_ID    
--  FROM       MNT_USER_TYPES UT INNER JOIN    
--  MNT_USER_LIST UL ON UT.USER_TYPE_ID = UL.USER_TYPE_ID    
--  INNER JOIN MNT_AGENCY_LIST MAL     
--  ON MAL.AGENCY_CODE = UL.USER_SYSTEM_ID    
--  WHERE     (UT.USER_TYPE_CODE=@USER_TYPE_PRODUCER ) AND UL.IS_ACTIVE='Y'       
--  AND MAL.AGENCY_ID=@AGENCYID    
--  ORDER BY USERNAME    
END    
  
  
  



GO

