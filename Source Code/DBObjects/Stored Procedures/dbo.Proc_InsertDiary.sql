IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--drop  procedure dbo.Proc_InsertDiary    
CREATE          procedure dbo.Proc_InsertDiary    
(    
	@RECBYSYSTEM   nchar(1) = NULL,    
	@RECDATE  datetime = NULL,    
	@FOLLOWUPDATE  datetime = NULL,    
	@LISTTYPEID  numeric(9) = NULL,    
	@POLICYBROKERID  numeric(9) = NULL,    
	@SUBJECTLINE  varchar(512) = NULL,    
	@LISTOPEN  nchar(1) = NULL,    
	@SYSTEMFOLLOWUPID numeric(9) = NULL,    
	@PRIORITY  nchar(1) = NULL,    
	@TOUSERID  numeric(9) = NULL,    
	@FROMUSERID  numeric(9) = NULL,    
	@STARTTIME  datetime = NULL,    
	@ENDTIME  datetime = NULL,    
	@NOTE   varchar(2000) = NULL,    
	@PROPOSALVERSION numeric(9) = NULL,    
	@QUOTEID  numeric(9) = NULL,    
	@CLAIMID  numeric(9) = NULL,    
	@CLAIMMOVEMENTID numeric(9) = NULL,    
	@TOENTITYID  numeric(9) = NULL,    
	@FROMENTITYID  int,    
	@LISTID   numeric(9) OUTPUT,    
	@CUSTOMER_ID  int = NULL,     
	@APP_ID   int  = null,    
	@APP_VERSION_ID  smallint = null,    
	@POLICY_ID  int = null,    
	@POLICY_VERSION_ID smallint = null,
	@RULES_VERIFIED smallint = null,
	@PROCESS_ROW_ID int = null,
	@MODULE_ID int =null

)    
as  

DECLARE  @newlistid numeric(9)    
BEGIN    
 --use if identity is false    
 --SELECT @newlistid = (IsNull(Max(listid),0)+1) FROM TODOLIST    

-- Adding Module ID parameter by Anurag Verma on 15/03/2007
 

-- Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 2.
IF (@LISTTYPEID=11) -- CLAIM REVIEW
BEGIN
  UPDATE CLM_CLAIM_INFO SET DIARY_DATE=@FOLLOWUPDATE WHERE CLAIM_ID= @CLAIMID
END


    
 INSERT into TODOLIST    
 (    
  RECBYSYSTEM,    
  RECDATE,    
  FOLLOWUPDATE,    
  LISTTYPEID,    
  POLICYBROKERID,    
  SUBJECTLINE,    
  LISTOPEN,    
  SYSTEMFOLLOWUPID,    
  PRIORITY,    
  TOUSERID,    
  FROMUSERID,    
  STARTTIME,    
  ENDTIME,    
  NOTE,    
  PROPOSALVERSION,    
  QUOTEID,    
  CLAIMID,    
  CLAIMMOVEMENTID,    
  TOENTITYID,    
  FROMENTITYID,    
  CUSTOMER_ID,    
  APP_ID,    
  APP_VERSION_ID,    
  POLICY_ID,    
  POLICY_VERSION_ID,
  RULES_VERIFIED,
  PROCESS_ROW_ID,     
  MODULE_ID 	
 )    
 values    
 (    
  @RECBYSYSTEM,    
  @RECDATE,    
  @FOLLOWUPDATE,    
  @LISTTYPEID,    
  @POLICYBROKERID,    
  @SUBJECTLINE,    
  @LISTOPEN,    
  @SYSTEMFOLLOWUPID,    
  @PRIORITY,    
  @TOUSERID,    
  @FROMUSERID,    
  @STARTTIME,    
  @ENDTIME,    
  @NOTE,    
  @PROPOSALVERSION,    
  @QUOTEID,    
  @CLAIMID,    
  @CLAIMMOVEMENTID,    
  @TOENTITYID,    
  @FROMENTITYID,    
  @CUSTOMER_ID,    
  @APP_ID,    
  @APP_VERSION_ID,    
  @POLICY_ID,    
  @POLICY_VERSION_ID,
  @RULES_VERIFIED,
  @PROCESS_ROW_ID,
  @MODULE_ID 	     
 )    
     
 /*List ID Output parameter is passed by the user hence fetching    
 the maximum List ID from table and assigning it in that variable*/    
 SELECT @LISTID = Max(IsNull(LISTID,0))  FROM TODOLIST  with(nolock)  
END    
  















GO

