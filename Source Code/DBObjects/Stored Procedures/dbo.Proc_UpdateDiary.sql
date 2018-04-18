IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateDiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateDiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------    
Proc Name        : dbo.Proc_UpdateDiary    
Created by       : Anurag Verma    
Date             : 5/4/2005    
Purpose       :to update data in todolist table    
Revison History :    
Used In        : Wolverine    

Reviewed By	:	Anurag verma
Reviewed On	:	16-07-2007
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC dbo.Proc_UpdateDiary    
CREATE PROC dbo.Proc_UpdateDiary    
    
    
@RECDATE  datetime = NULL,    
@FOLLOWUPDATE  datetime = NULL,    
@LISTTYPEID  numeric(9) = NULL,    
@SUBJECTLINE  varchar(512) = NULL,    
@SYSTEMFOLLOWUPID numeric(9) = NULL,    
@PRIORITY  nchar(1) = NULL,    
@TOUSERID  numeric(9) = NULL,    
@FROMUSERID  numeric(9) = NULL,    
@STARTTIME  datetime = NULL,    
@ENDTIME  datetime = NULL,    
@NOTE   varchar(2000) = NULL,    
@LISTID   numeric(9),    
@CUSTOMER_ID  int = null,    
@APP_ID   int = null,    
@APP_VERSION_ID  smallint = null,    
@POLICY_ID  int = null,    
@POLICY_VERSION_ID smallint = null,  
@RULES_VERIFIED smallint = null ,
@QUOTEID  int = null  
AS    
BEGIN

-- Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 2.
DECLARE @CLAIMID INT
IF(@LISTTYPEID = 11) -- CLAIM_REVIEW
BEGIN
  SELECT @CLAIMID=CLAIMID FROM TODOLIST WHERE LISTID=@LISTID
  UPDATE CLM_CLAIM_INFO SET DIARY_DATE=@FOLLOWUPDATE WHERE CLAIM_ID= @CLAIMID
END

    
UPDATE TODOLIST    
SET    
RECDATE =@RECDATE,       
FOLLOWUPDATE=@FOLLOWUPDATE,    
LISTTYPEID=@LISTTYPEID,    
SUBJECTLINE=@SUBJECTLINE,    
    
SYSTEMFOLLOWUPID=@SYSTEMFOLLOWUPID,    
PRIORITY=@PRIORITY,    
TOUSERID=@TOUSERID,    
FROMUSERID=@FROMUSERID,    
STARTTIME=@STARTTIME,    
ENDTIME=@ENDTIME,    
NOTE=@NOTE,    
CUSTOMER_ID=@CUSTOMER_ID,    
APP_ID=@APP_ID,    
APP_VERSION_ID=@APP_VERSION_ID,    
POLICY_ID=@POLICY_ID,    
POLICY_VERSION_ID=@POLICY_VERSION_ID,  
RULES_VERIFIED = @RULES_VERIFIED, 
QUOTEID = @QUOTEID   
WHERE     
LISTID=@LISTID     
    
    
END    








GO

