IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Clm_InsertDiaryEntryForPinkSlipUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Clm_InsertDiaryEntryForPinkSlipUsers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_Clm_InsertDiaryEntryForPinkSlipUsers                        
Created by      : Sumit Chhabra                        
Date            : 18/11/2006                        
Purpose         : Insert diary entry for users who have been selected at claim info screen      
Created by      : Sumit Chhabra                        
Revison History :                        
Used In        : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
-- DROP PROC dbo.Proc_Clm_InsertDiaryEntryForPinkSlipUsers      
CREATE PROC dbo.Proc_Clm_InsertDiaryEntryForPinkSlipUsers                        
@CLAIM_ID int,      
@CUSTOMER_ID int,      
@POLICY_ID int,      
@POLICY_VERSION_ID smallint,      
@NEW_RECIEVE_PINK_SLIP_USERS_LIST varchar(200),      
@CREATED_BY int      
AS                        
BEGIN                        
       
 DECLARE @CURRENT_USER VARCHAR(10)                      
 DECLARE @COUNT INT                           
 DECLARE @UNDERWRITERS VARCHAR(100)      
 DECLARE @PINK_SLIP_NOTE_TYPE smallint      
 DECLARE @CLAIM_MODULE_ID SMALLINT
 DECLARE @APP_ID INT
 DECLARE @APP_VERSION_ID SMALLINT
 

 SET @COUNT=2      
 SET @PINK_SLIP_NOTE_TYPE = 19      
 SET @CLAIM_MODULE_ID = 5

 IF(@CUSTOMER_ID != '' AND @CUSTOMER_ID != '0')  
   BEGIN
	SELECT @APP_ID = APP_ID, @APP_VERSION_ID= APP_VERSION_ID 
	FROM POL_CUSTOMER_POLICY_LIST 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
	
    --Added For Itrack Issue #5137.
	Declare @QUOTEID int
	set @QUOTEID =0
	if exists (select 1 from clt_quickquote_list where CUSTOMER_ID = @CUSTOMER_ID and app_id = @APP_ID and APP_VERSION_ID = @APP_VERSION_ID)
	begin
		select @QUOTEID = ISNULL(QQ_ID,0) from clt_quickquote_list with(nolock)
		where CUSTOMER_ID = @CUSTOMER_ID and app_id = @APP_ID and APP_VERSION_ID = @APP_VERSION_ID
	end

	
   END

 if @NEW_RECIEVE_PINK_SLIP_USERS_LIST is null or @NEW_RECIEVE_PINK_SLIP_USERS_LIST=''  
 return  
 
-----------------------------------------
/* Asfa (21-Apr-2008) - iTrack issue #4065
 1. While doing a pink slip this goes to User's diary but setting 5 Days for followup 
that should not happen. Also in diary messge it no where specifies the pink slip type 
was chosen at claim. 
*/
-- Putting chosen Pink Slip Type at claim into Diary Notes field 
DECLARE @PINK_SLIP_TYPE_LIST VARCHAR(200)
DECLARE @LOOKUP_VALUE_DESC VARCHAR(200)
DECLARE @NOTE	NVARCHAR(4000)
DECLARE @PINK_SLIP_TYPE INT
DECLARE @CNT INT

SET @NOTE =''
SELECT @PINK_SLIP_TYPE_LIST=PINK_SLIP_TYPE_LIST FROM CLM_CLAIM_INFO WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID

SET @CNT = 1
SET @PINK_SLIP_TYPE = DBO.PIECE(@PINK_SLIP_TYPE_LIST,',',@CNT)

WHILE @PINK_SLIP_TYPE IS NOT NULL
BEGIN
SELECT @LOOKUP_VALUE_DESC = LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WITH (NOLOCK) WHERE 
LOOKUP_UNIQUE_ID = @PINK_SLIP_TYPE

SET @NOTE = @NOTE + @LOOKUP_VALUE_DESC + ', '
SET @CNT = @CNT+1
SET @PINK_SLIP_TYPE = DBO.PIECE(@PINK_SLIP_TYPE_LIST,',',@CNT)

END

SELECT @NOTE = SUBSTRING(@NOTE, 0,LEN(@NOTE))

-----------------------------------------      
 SET @CURRENT_USER = DBO.PIECE(@NEW_RECIEVE_PINK_SLIP_USERS_LIST,',',1)                                    
 --LOOP THROUGH THE STRING TO FIND APPLICATIONS ASSIGNED TO EACH UNDERWRITER                      
 WHILE @CURRENT_USER IS NOT NULL                                    
 BEGIN                            
       
  --ADD DIARY ENTRY 
  INSERT INTO TODOLIST                        
  (                        
   RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                        
   POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                        
   FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                        
   FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                        
  )                        
  VALUES                        
  ( 
/* Asfa (21-Apr-2008) - iTrack issue #4065
1. While doing a pink slip this goes to User's diary but setting 5 Days for followup 
that should not happen.
*/
-- NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@PINK_SLIP_NOTE_TYPE,                        
   NULL,GETDATE(),GETDATE(),@PINK_SLIP_NOTE_TYPE,                        
   NULL,'New Pink Slip Notification','Y',                        

/* Asfa (21-Apr-2008) - iTrack issue #4065
   Also in diary messge it no where specifies the pink slip type was chosen at claim. 
   So putting chosen Pink Slip Types at claim into Diary Notes field 
*/
-- NULL,'M',@CURRENT_USER,@CREATED_BY,NULL,NULL,'New Pink Slip Notification from Claim',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                        
   NULL,'M',@CURRENT_USER,@CREATED_BY,NULL,NULL,@NOTE,NULL,@QUOTEID,@CLAIM_ID,NULL,NULL,NULL,                        
   @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE_ID                        
  )            
       
  SET @CURRENT_USER=DBO.PIECE(@NEW_RECIEVE_PINK_SLIP_USERS_LIST,',',@COUNT)                      
  SET @COUNT=@COUNT+1           
 END         
      
END      




GO

