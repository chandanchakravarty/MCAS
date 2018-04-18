IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertHome_Loss_Acord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertHome_Loss_Acord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_InsertHome_Loss_Acord
--go
 /*    
Author  :Praveen Kasana
Dated   : 2 Dec 2009    
Purpose : Insert Loss Information for Home : QQ to MAKE APP 
drop proc dbo.Proc_InsertHome_Loss     
*/   

create proc dbo.Proc_InsertHome_Loss_Acord
(    
	@CUSTOMER_ID  INT,
	@WATERBACKUP_SUMPPUMP_LOSS INT = NULL

)    
as    
begin    

declare @PRIOR_LOSS_ID int
select @PRIOR_LOSS_ID=isnull(max(PRIOR_LOSS_ID),0)+1 from PRIOR_LOSS_HOME    

--Get Loss ID

declare @DATE datetime
set @DATE = CONVERT(VARCHAR,getdate(),101)

declare @LOSS_ID int
 
exec Proc_InsertAPP_PRIOR_LOSS_INFO 
@CUSTOMER_ID = @CUSTOMER_ID,
@OCCURENCE_DATE = @DATE  ,
@CLAIM_DATE = null,
@LOB = '1',
@LOSS_TYPE = null,
@AMOUNT_PAID= null,
@AMOUNT_RESERVED= null,
@CLAIM_STATUS =  null,
@LOSS_DESC = null,
@REMARKS= null,
@MOD= null,
@LOSS_RUN= null,
@CAT_NO= null,
@CLAIMID= null,
@IS_ACTIVE= null,
@CREATED_BY= null,
@CREATED_DATETIME= null,
@MODIFIED_BY= null,
@LAST_UPDATED_DATETIME= null,
@LOSS_ID= null,
@APLUS_REPORT_ORDERED= null,
@DRIVER_ID= null,
@DRIVER_NAME= null,
@RELATIONSHIP= null,
@CLAIMS_TYPE= null,
@AT_FAULT= null,
@CHARGEABLE= null,
@LOSS_LOCATION= null,
@CAUSE_OF_LOSS= null,
@POLICY_NUM= null,
@LOSS_CARRIER= null,
@OTHER_DESC= null,
@NAME_MATCH= null

SELECT @LOSS_ID=ISNULL(MAX(LOSS_ID),-1) FROM APP_PRIOR_LOSS_INFO with(nolock) where customer_id=@CUSTOMER_ID     

--

insert into PRIOR_LOSS_HOME    
(    
PRIOR_LOSS_ID  ,    
LOSS_ID   ,    
CUSTOMER_ID  ,    
LOCATION_ID ,    
LOSS_ADD1    ,    
LOSS_ADD2   ,    
LOSS_CITY   ,    
LOSS_STATE  ,    
LOSS_ZIP    ,     
CURRENT_ADD1,    
CURRENT_ADD2,    
CURRENT_CITY,     
CURRENT_STATE ,    
CURRENT_ZIP  ,     
POLICY_TYPE  ,     
POLICY_NUMBER,  
WATERBACKUP_SUMPPUMP_LOSS 
)    
values    
(    
@PRIOR_LOSS_ID  ,    
@LOSS_ID   ,    
@CUSTOMER_ID  ,    
0 ,    
null    ,    
null   ,    
null   ,    
null  ,    
null    ,     
null,    
null,    
null,     
null ,    
null  ,     
null  ,     
null,  
@WATERBACKUP_SUMPPUMP_LOSS   
)    
    
end    
    
--
--go 
--exec dbo.Proc_InsertHome_Loss_Acord 1692,10964
----
----select * from PRIOR_LOSS_HOME where customer_id=2023
----select * from APP_PRIOR_LOSS_INFO  where customer_id=2023
----
--rollback tran




GO

