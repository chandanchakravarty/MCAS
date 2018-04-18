IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SelectUserForDiarySetup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SelectUserForDiarySetup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Proc_SelectUserForDiarySetup] 
     
      AS BEGIN
    DECLARE
     @CARRIER_CODE nvarchar(10) 
      --Added by praveer panghal on 26-Aug-2010 for Itrack 294 
SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)         
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID  

SELECT USER_ID as userid,USER_FNAME+' '+USER_LNAME as username FROM MNT_USER_LIST   
where (is_active='Y' and user_system_id=@CARRIER_CODE) 
ORDER BY USER_FNAME, USER_LNAME 

END





GO

