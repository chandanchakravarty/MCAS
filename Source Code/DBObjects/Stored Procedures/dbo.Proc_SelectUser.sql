IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /************************************************  
Modified By : Mohit Agarwal     
Date: 16-Nov-2007  
Purpose: ITrack 3029 to show only company users  
***********************************************/  
-- drop procedure dbo.Proc_SelectUser  
CREATE   procedure [dbo].[Proc_SelectUser]  
AS   
BEGIN 
DECLARE 
@CARRIER_CODE NVARCHAR(10)
--ADDED BY :PRAVEER PANGHAL 08/10/2010 FOR ITRACK 376
SELECT @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID=SYSP.SYS_CARRIER_ID

SELECT USER_SYSTEM_ID as systemid, USER_ID as userid, USER_FNAME+' '+USER_LNAME as username   
FROM MNT_USER_LIST WITH(NOLOCK)  
WHERE (USER_SYSTEM_ID=@CARRIER_CODE and IS_ACTIVE='Y') ORDER BY USER_FNAME, USER_LNAME    
  END  
    
    
  
  
  

GO

