IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetInsuranceScore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetInsuranceScore]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_SetInsuranceScore  
Created by       : Anurag Verma  
Date             : 6/07/2005  
Purpose          : To set Insurance Score  
Revison History :  
modified by		: pravesh K Chandel
date			: 6 may 2008
purpose			: to update recieved date if case of hit but no score (score=0) OR SCORE=-1
Used In         :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
drop proc dbo.Proc_SetInsuranceScore  
------   ------------       -------------------------*/  
CREATE       PROC [dbo].[Proc_SetInsuranceScore]  
(  
	@CUSTOMER_ID INT,  
	@RESULT INT output  ,
	@SCORE Numeric(18,0),
	@REASON_CODE VarChar(3),
	@REASON_CODE2 VarChar(3),
	@REASON_CODE3 VarChar(3),
	@REASON_CODE4 VarChar(3),
	@CREATED_BY   int =null
)  
AS  



DECLARE @FACTOR1 nvarchar(10)
DECLARE @FACTOR2 nvarchar(10) 
DECLARE @FACTOR3 nvarchar(10) 
DECLARE @FACTOR4 nvarchar(10) 

DECLARE @OLD_SCORE Int ,@OLD_RECIEVED_DATE datetime
declare @OLD_REASON_CODE nvarchar(10)
	,@OLD_REASON_CODE2 nvarchar(10)
	,@OLD_REASON_CODE3 nvarchar(10)
	,@OLD_REASON_CODE4 nvarchar(10)
BEGIN  
--	
--	EXECUTE @FACTOR1 = Proc_GetLookupID 'RCFC',@REASON_CODE
--    EXECUTE @FACTOR2 = Proc_GetLookupID 'RCFC',@REASON_CODE2
--	EXECUTE @FACTOR3 = Proc_GetLookupID 'RCFC',@REASON_CODE3
--	EXECUTE @FACTOR4 = Proc_GetLookupID 'RCFC',@REASON_CODE4      

	set @FACTOR1 = @REASON_CODE
    set @FACTOR2 = @REASON_CODE2
	set @FACTOR3 = @REASON_CODE3
	set @FACTOR4 = @REASON_CODE4 

SELECT	@OLD_REASON_CODE	=CUSTOMER_REASON_CODE,
		@OLD_REASON_CODE2	=CUSTOMER_REASON_CODE2,
		@OLD_REASON_CODE3	=CUSTOMER_REASON_CODE3,
		@OLD_REASON_CODE4	=CUSTOMER_REASON_CODE4,
		@OLD_SCORE			=CUSTOMER_INSURANCE_SCORE,
		@OLD_RECIEVED_DATE	=CUSTOMER_INSURANCE_RECEIVED_DATE

FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID 

    
if (@SCORE = 0)
begin 
   UPDATE CLT_CUSTOMER_LIST 
	SET LAST_INSURANCE_SCORE_FETCHED = GETDATE() ,
			CUSTOMER_INSURANCE_RECEIVED_DATE = GetDate()
	WHERE CUSTOMER_ID=@CUSTOMER_ID  
end
else
begin
 
   UPDATE CLT_CUSTOMER_LIST 
	SET LAST_INSURANCE_SCORE_FETCHED = GETDATE() ,
		CUSTOMER_INSURANCE_SCORE = @SCORE,
		CUSTOMER_REASON_CODE =  @FACTOR1,
		CUSTOMER_REASON_CODE2 = @FACTOR2,
		CUSTOMER_REASON_CODE3 = @FACTOR3,
		CUSTOMER_REASON_CODE4 = @FACTOR4,
		CUSTOMER_INSURANCE_RECEIVED_DATE = GetDate()
	WHERE CUSTOMER_ID=@CUSTOMER_ID  
--update insurace score of app and pol whereever applicable
execute Proc_UpdateCustomer_InsuranceScore @CUSTOMER_ID, @SCORE

--maintain transaction log of Score, Recieved Date and reason codes
DECLARE @TRAN_XML NVARCHAR(4000),@RECORDED_BY_NAME nvarchar(50)
SET @TRAN_XML = '<LabelFieldMapping>'

if (@OLD_SCORE!=@SCORE)
	SET @TRAN_XML = @TRAN_XML + '<Map label="Insurance Score" field="CUSTOMER_INSURANCE_SCORE" OldValue="' + convert(varchar,@OLD_SCORE) + '" NewValue="'+ convert(varchar,@SCORE) + '" />'
if (convert(varchar,isnull(@OLD_RECIEVED_DATE,''),101)!=convert(varchar,GETDATE(),101))
	SET @TRAN_XML = @TRAN_XML + '<Map label="Received Date" field="CUSTOMER_INSURANCE_RECEIVED_DATE" OldValue="'+ case when isnull(@OLD_RECIEVED_DATE,'') = '' then '' else convert(varchar,@OLD_RECIEVED_DATE,101) end + '" NewValue="'+ convert(varchar,GETDATE(),101) + '" />'
if (@OLD_REASON_CODE!=@REASON_CODE)
	SET @TRAN_XML = @TRAN_XML + '<Map label="Reason Code 1" field="CUSTOMER_REASON_CODE" OldValue="' + @OLD_REASON_CODE +'" NewValue="'+ @REASON_CODE + '" />'
if (@OLD_REASON_CODE2!=@REASON_CODE2)
	SET @TRAN_XML = @TRAN_XML + '<Map label="Reason Code 2" field="CUSTOMER_REASON_CODE2" OldValue="'+ @OLD_REASON_CODE2 +'" NewValue="'+ @REASON_CODE2 +'" />'
if (@OLD_REASON_CODE3!=@REASON_CODE3)
	SET @TRAN_XML = @TRAN_XML + '<Map label="Reason Code 3" field="CUSTOMER_REASON_CODE3" OldValue="'+ @OLD_REASON_CODE3 +'" NewValue="'+ @REASON_CODE3 +'" />'
if (@OLD_REASON_CODE4!=@REASON_CODE4)
	SET @TRAN_XML = @TRAN_XML + '<Map label="Reason Code 4" field="CUSTOMER_REASON_CODE4" OldValue="'+ @OLD_REASON_CODE4 +'" NewValue="'+ @REASON_CODE4 + '" />'

SET @TRAN_XML = @TRAN_XML + '</LabelFieldMapping>'

DECLARE @TRANS_ID INT

SELECT @RECORDED_BY_NAME = ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') FROM MNT_USER_LIST WITH(NOLOCK) WHERE USER_ID=@CREATED_BY

SELECT @TRANS_ID=ISNULL(MAX(TRANS_ID),0) + 1 FROM MNT_TRANSACTION_LOG WITH(NOLOCK)

INSERT INTO MNT_TRANSACTION_LOG (TRANS_ID,TRANS_TYPE_ID,CLIENT_ID,POLICY_ID,POLICY_VER_TRACKING_ID,RECORDED_BY,RECORDED_BY_NAME,RECORD_DATE_TIME,TRANS_DESC,ENTITY_ID,ENTITY_TYPE,IS_COMPLETED,APP_ID,APP_VERSION_ID,QUOTE_ID,QUOTE_VERSION_ID,ADDITIONAL_INFO)
						 VALUES(@TRANS_ID,1,		@CUSTOMER_ID ,		0  ,0					,  @CREATED_BY,@RECORDED_BY_NAME,GETDATE(),'Insurance Score has been fetched.',0,0,NULL,0,0,0,0,'')
INSERT INTO MNT_TRANSACTION_XML(TRANS_ID,TRANS_DETAILS,IS_VALIDXML)
	VALUES(@TRANS_ID,@TRAN_XML,NULL)
--end here for transaction log
end
   SET @RESULT=1      
 
END  





GO

