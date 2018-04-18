IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_PRIOR_LOSS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_PRIOR_LOSS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateAPP_PRIOR_INFO    
Created by      : Anurag Verma    
Date            : 4/28/2005    
Purpose       :to update data in App_PRIOR_INFO table    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments 
drop PROC dbo.Proc_UpdateAPP_PRIOR_LOSS_INFO   
------   ------------       -------------------------*/    
CREATE  PROC dbo.Proc_UpdateAPP_PRIOR_LOSS_INFO    
	@LOSS_ID INT,    
	@CUSTOMER_ID int,    
	@OCCURENCE_DATE datetime,    
	@CLAIM_DATE datetime,    
	@LOB nvarchar(10),    
	@LOSS_TYPE int,    
	@AMOUNT_PAID decimal,    
	@AMOUNT_RESERVED decimal,    
	@CLAIM_STATUS nvarchar(10),    
	@LOSS_DESC text,    
	@REMARKS nvarchar(50),    
	@MOD nvarchar(50),    
	@LOSS_RUN nchar(2),    
	@CAT_NO nvarchar(20),    
	@MODIFIED_BY INT,    
	@LAST_UPDATED_DATETIME DATETIME,  
	@APLUS_REPORT_ORDERED int,   
	@DRIVER_ID int,  
	@DRIVER_NAME nvarchar(200),  
	@RELATIONSHIP int,  
	@CLAIMS_TYPE int,  
	@AT_FAULT int,  
	@CHARGEABLE int,
	@LOSS_LOCATION varchar(70),  
	@CAUSE_OF_LOSS varchar(50),  
	@POLICY_NUM varchar(50),  
	@LOSS_CARRIER varchar(50),
	@OTHER_DESC VARCHAR(50),
	@NAME_MATCH INT --Done for Itrack Issue 6723 on 27 Nov 09
     
    
AS    
    
BEGIN    
UPDATE APP_PRIOR_LOSS_INFO    
SET    
	OCCURENCE_DATE=@OCCURENCE_DATE,    
	CLAIM_DATE=@CLAIM_DATE,    
	LOB=@LOB,    
	LOSS_TYPE=@LOSS_TYPE,    
	AMOUNT_PAID=@AMOUNT_PAID,    
	AMOUNT_RESERVED=@AMOUNT_RESERVED,    
	CLAIM_STATUS=@CLAIM_STATUS,    
	LOSS_DESC=@LOSS_DESC,    
	REMARKS=@REMARKS,    
	MOD=@MOD,    
	LOSS_RUN=@LOSS_RUN,    
	CAT_NO=@CAT_NO,    
	MODIFIED_BY =@MODIFIED_BY,    
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
	APLUS_REPORT_ORDERED=@APLUS_REPORT_ORDERED,  
	DRIVER_ID=@DRIVER_ID,  
	DRIVER_NAME=@DRIVER_NAME,  
	RELATIONSHIP=@RELATIONSHIP,  
	CLAIMS_TYPE=@CLAIMS_TYPE,  
	AT_FAULT=@AT_FAULT,  
	CHARGEABLE=@CHARGEABLE,
	LOSS_LOCATION=@LOSS_LOCATION,  
	CAUSE_OF_LOSS=@CAUSE_OF_LOSS,  
	POLICY_NUM=@POLICY_NUM,  
	LOSS_CARRIER=@LOSS_CARRIER,
	OTHER_DESC=@OTHER_DESC,
	NAME_MATCH = @NAME_MATCH  --Done for Itrack Issue 6723 on 27 Nov 09             
  
  
WHERE LOSS_ID=@LOSS_ID and CUSTOMER_ID=@CUSTOMER_ID     
END    
  






GO

