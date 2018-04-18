IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SavePriorLoss_Acord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SavePriorLoss_Acord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc dbo.Proc_SavePriorLoss_Acord
(                    
 @CUSTOMER_ID  int,                    
 @DRIVER_ID  int,                    
 @MVR_AMOUNT  decimal(20,2),          
 @MVR_DATE  datetime,
 @IS_ACTIVE nchar(2),
 @CREATED_BY int                
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
	 OTHER_DESC             
	)      
	VALUES      
	(      
	 @LOSSID,           
	 @CUSTOMER_ID,      
	 @MVR_DATE,      
	 null,      
	 '4',      
	 14246,      
	 @MVR_AMOUNT,      
	 null,      
	 null,      
	 null,      
	 null,      
	 null,      
	 null,      
	 null,      
	 null,      
	 'Y',      
	 null,      
	 getdate(),      
	 null,      
	 null,    
	 null,    
	 null,    
	 null,    
	 null,    
	 null,    
	 null,    
	 null,  
	 null,    
	 null,    
	 null,    
	 null,  
	 null                
	)      
END
GO

