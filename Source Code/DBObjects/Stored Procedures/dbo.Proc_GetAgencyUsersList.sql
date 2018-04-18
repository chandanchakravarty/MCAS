IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyUsersList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyUsersList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_GetAgencyUsersList              
Created by         : Sumit Chhabra            
Date               : 05/08/2005            
Purpose            : To get Agency Users List from the user_list table  corresponding to the carrier id provided             
Modified by         : Sumit Chhabra            
Date               : 15/03/2007            
Purpose            : Added the check to fetch only those users who have been assigned the role of Claims Adjuster          
Revison History :              
Used In                :   Wolverine              
------------------------------------------------------------              
Modified By  : Asfa Praveen        
Date   : 29/Aug/2007        
Purpose  : To add Adjuster_ID column        
------------------------------------------------------------                                                                        
Date     Review By          Comments              
------   ------------       -------------------------*/              
-- Drop PROC dbo.Proc_GetAgencyUsersList         
CREATE PROC [dbo].[Proc_GetAgencyUsersList]            
(              
@CARRIER_ID varchar(8)             
              
)              
AS              
            
BEGIN              
DECLARE @USER_TYPE_CLAIMS_ADJUSTER INT          
SET @USER_TYPE_CLAIMS_ADJUSTER = 46          
 SELECT             
 ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS USER_NAME, MNT_USER_LIST.ADJUSTER_CODE,             
 CAST(MNT_USER_LIST.USER_ID AS VARCHAR) + '^'           
 + CAST(ISNULL(MNT_USER_LIST.ADJUSTER_CODE,0) AS VARCHAR)         
 + '^' + ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'')         
/* Commented by Asfa 29-Aug-2007        
+ '^' + ISNULL(USER_ADD1,'')  + '^' + ISNULL(USER_ADD2,'')  + '^' +           
 ISNULL(USER_CITY,'') + '^' + ISNULL(COUNTRY,'')  + '^' + ISNULL(USER_STATE,'') + '^' +  ISNULL(USER_ZIP,'') + '^' + ISNULL(USER_PHONE,'') + '^' +           
 ISNULL(USER_FAX,'') + '^' + ISNULL(USER_EMAIL,'')*/  AS USER_ID             
 FROM             
    MNT_USER_LIST              
 WHERE                 
   (UPPER(MNT_USER_LIST.USER_SYSTEM_ID) = UPPER(@CARRIER_ID) OR USER_TYPE_ID = @USER_TYPE_CLAIMS_ADJUSTER)     
   AND IS_ACTIVE = 'Y' AND  MNT_USER_LIST.USER_TYPE_ID =46
   --Done for Itrack Issue 6285 on 7 Sept 09     
  --UPPER(MNT_USER_LIST.USER_SYSTEM_ID) = UPPER(@CARRIER_ID) AND USER_TYPE_ID = 46         
 ORDER BY             
  USER_NAME              
END
GO

