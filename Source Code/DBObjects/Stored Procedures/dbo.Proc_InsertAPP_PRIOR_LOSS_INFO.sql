IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_PRIOR_LOSS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_PRIOR_LOSS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name      : dbo.Proc_InsertAPP_PRIOR_LOSS_INFO    
Created by       : Anurag Verma    
Date             : 4/2/2005    
Purpose       : Insert data to APP_PRIOR_LOSS_INFO    
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments  
drop PROC dbo.Proc_InsertAPP_PRIOR_LOSS_INFO    
------   ------------       -------------------------*/    
CREATE  PROC dbo.Proc_InsertAPP_PRIOR_LOSS_INFO    
(    
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
	@CLAIMID nvarchar,    
	@IS_ACTIVE     nchar(2),    
	@CREATED_BY     int,    
	@CREATED_DATETIME     datetime,    
	@MODIFIED_BY INT,    
	@LAST_UPDATED_DATETIME DATETIME,     
	@LOSS_ID     int  OUTPUT,  
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
	@OTHER_DESC varchar(50),
	@NAME_MATCH int  --Done for Itrack Issue 6723 on 27 Nov 09             
)    
AS    
    
BEGIN    
DECLARE @LOSSID INT     
SELECT @LOSSID=ISNULL(max(LOSS_id),0)+1 from APP_PRIOR_LOSS_INFO where CUSTOMER_ID=@CUSTOMER_ID    
    
INSERT INTO APP_PRIOR_LOSS_INFO    
(    
	LOSS_ID,    
	CUSTOMER_ID,    
	OCCURENCE_DATE,    
	CLAIM_DATE,    
	LOB,    
	LOSS_TYPE,    
	AMOUNT_PAID,    
	AMOUNT_RESERVED,    
	CLAIM_STATUS,    
	LOSS_DESC,    
	REMARKS,    
	MOD,    
	LOSS_RUN,    
	CAT_NO,    
	CLAIMID,    
	IS_ACTIVE,    
	CREATED_BY,    
	CREATED_DATETIME,    
	MODIFIED_BY ,    
	LAST_UPDATED_DATETIME,  
	APLUS_REPORT_ORDERED,  
	DRIVER_ID,  
	DRIVER_NAME,  
	RELATIONSHIP,  
	CLAIMS_TYPE,  
	AT_FAULT,  
	CHARGEABLE,
	LOSS_LOCATION,  
	CAUSE_OF_LOSS,  
	POLICY_NUM,  
	LOSS_CARRIER ,
	OTHER_DESC,
	NAME_MATCH  --Done for Itrack Issue 6723 on 27 Nov 09                      
)    
VALUES    
(    
	@LOSSID,         
	@CUSTOMER_ID,    
	@OCCURENCE_DATE,    
	@CLAIM_DATE,    
	@LOB,    
	@LOSS_TYPE,    
	@AMOUNT_PAID,    
	@AMOUNT_RESERVED,    
	@CLAIM_STATUS,    
	@LOSS_DESC,    
	@REMARKS,    
	@MOD,    
	@LOSS_RUN,    
	@CAT_NO,    
	@CLAIMID,    
	@IS_ACTIVE,    
	@CREATED_BY,    
	@CREATED_DATETIME,    
	@MODIFIED_BY,    
	@LAST_UPDATED_DATETIME,  
	@APLUS_REPORT_ORDERED,  
	@DRIVER_ID,  
	@DRIVER_NAME,  
	@RELATIONSHIP,  
	@CLAIMS_TYPE,  
	@AT_FAULT,  
	@CHARGEABLE,
	@LOSS_LOCATION,  
	@CAUSE_OF_LOSS,  
	@POLICY_NUM,  
	@LOSS_CARRIER,
	@OTHER_DESC,
	@NAME_MATCH  --Done for Itrack Issue 6723 on 27 Nov 09                         
)    
    
SELECT @LOSSID=ISNULL(MAX(LOSS_ID),-1) FROM APP_PRIOR_LOSS_INFO where customer_id=@CUSTOMER_ID     
SET @LOSS_ID=@LOSSID    
    
END    
  






GO

