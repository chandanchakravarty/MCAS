IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_REOPEN_CLAIM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_REOPEN_CLAIM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertCLM_REOPEN_CLAIM    
Created by      : Vijay Arora    
Date            : 6/19/2006    
Purpose     : To insert a record in table named CLM_REOPEN_CLAIM    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Proc_InsertCLM_REOPEN_CLAIM    
CREATE PROC [dbo].[Proc_InsertCLM_REOPEN_CLAIM]    
(    
@CLAIM_ID     int,    
@REOPEN_ID     int OUTPUT,    
@REOPEN_DATE     datetime,    
@REOPEN_BY     int,    
@REASON     varchar(500),    
@CREATED_BY     int    
)    
AS    
BEGIN   
declare @CLAIM_STATUS_REOPEN int 
   
set @CLAIM_STATUS_REOPEN = 11739

SELECT @REOPEN_ID=isnull(Max(REOPEN_ID),0)+1 FROM CLM_REOPEN_CLAIM WHERE CLAIM_ID = @CLAIM_ID    
INSERT INTO CLM_REOPEN_CLAIM    
(    
CLAIM_ID,    
REOPEN_ID,    
REOPEN_DATE,    
REOPEN_BY,    
REASON,    
IS_ACTIVE,    
CREATED_BY,    
CREATED_DATETIME    
)    
VALUES    
(    
@CLAIM_ID,    
@REOPEN_ID,    
@REOPEN_DATE,    
@REOPEN_BY,    
@REASON,    
'Y',    
@CREATED_BY,    
GETDATE()    
)    
--Re-open the claim and update the reopen_count field at clm_claim_info   

-- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008
UPDATE CLM_CLAIM_INFO SET CLAIM_STATUS=@CLAIM_STATUS_REOPEN,REOPEN_COUNT = ISNULL(REOPEN_COUNT,0)+1 
, REOPENED_DATE=GETDATE() 
WHERE CLAIM_ID=@CLAIM_ID  
  
-- select * From mnt_lookup_values where lookup_id=1260


/*- Added by Asfa Praveen (06-Feb-2008) - iTrack issue #3545
11739 - Open
11740 - Closed
*/


  IF EXISTS(SELECT LISTID FROM TODOLIST WHERE CLAIMID=@CLAIM_ID)
   BEGIN
	UPDATE TODOLIST SET LISTOPEN ='Y' WHERE CLAIMID=@CLAIM_ID 
   END

END 






GO

