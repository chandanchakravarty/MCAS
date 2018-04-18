IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAgencyExistsCapitalrater]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAgencyExistsCapitalrater]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_CheckAgencyExistsCapitalrater                                      
Created by      : NEERAJ sINGH                                     
Date            : 18 july 2007                               
Purpose         :Check if AGENCY Exists fOR Capital rater Implementation                                       
Revison History :                                      
Used In        : Wolverine                                      
                                      
                             
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                         
    
CREATE PROC [dbo].[Proc_CheckAgencyExistsCapitalrater]  
(                                      
 @AGENCYCODE varchar(100)                     
)
AS                   
BEGIN   
DECLARE @AGENCY_CODE VARCHAR(100)
DECLARE @AGENCY_ID INT
SELECT @AGENCY_CODE=AGENCY_CODE, @AGENCY_ID = AGENCY_ID FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE
AGENCY_CODE= @AGENCYCODE and IS_ACTIVE='Y'

IF(@AGENCY_CODE IS NULL)
BEGIN
  SELECT '1' AS STATUS, @AGENCY_ID as AGENCY_ID --AGENCY DOES NOT EXISTS
END
ELSE
BEGIN
 SELECT '2' AS STATUS , @AGENCY_ID as AGENCY_ID --AGENCY EXISTS
 END
END

















GO

