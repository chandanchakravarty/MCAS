IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SavePriorLossVehicle_Acord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SavePriorLossVehicle_Acord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*Proc Name     : Proc_SavePriorLossVehicle_Acord                      
Created by      : Praveen kasana           
Date            : 10 - Feb- 2009                      
Purpose         : Insert Prior Los AUTO / MOTOR CYCLE                      
Revison History :                      
Used In         : Wolverine                      
------------------------------------------------------------    
------   ------------       -------------------------*/            
--drop proc Dbo.Proc_SavePriorLossVehicle_Acord   
create proc [dbo].[Proc_SavePriorLossVehicle_Acord]
(                    
 @CUSTOMER_ID  int,                    
 @DRIVER_ID  int, 
 @APP_ID int,                   
 @APP_VERSION_ID int,
 @VIOLATION_CODE nvarchar(100),
 @MVR_AMOUNT  decimal(20,2),          
 @MVR_DATE  datetime,
 @APP_LOB   varchar(10),
 @IS_ACTIVE nchar(2),
 @CREATED_BY int                
)   
                 
AS                    
BEGIN  

--<option value="11924">Chargeable</option>
--APLUS_REPORT_ORDERED
DECLARE @CHARGEABLE	INT
SET @CHARGEABLE = 11924 --chargable
DECLARE @APLUS_REPORT_ORDERED INT
SET @APLUS_REPORT_ORDERED = 0 --MANUAL
--<option value="10963">Yes</option>At Fault
DECLARE @AT_FAULT INT
IF(@VIOLATION_CODE ='42210' OR @VIOLATION_CODE = '42230' OR @VIOLATION_CODE='42220' OR @VIOLATION_CODE='42200' OR @VIOLATION_CODE='COMP')
	BEGIN
		SET @AT_FAULT = 10964
		SET @CHARGEABLE=11923
	END
ELSE
	BEGIN
		SET @AT_FAULT = 10963
	END

--1557^6^1^1^APP
DECLARE @DRIVER_NAME NVARCHAR(100)
SET @DRIVER_NAME = CAST(@CUSTOMER_ID AS VARCHAR) + '^' + CAST(@APP_ID AS VARCHAR) + '^' + CAST(@APP_VERSION_ID AS VARCHAR) + '^' + CAST(@DRIVER_ID AS VARCHAR) + '^APP'


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
     CASE 
		WHEN @APP_LOB = 'AUTOP' THEN '2'
		WHEN @APP_LOB = 'CYCL' THEN '3'
	 END,	  
	 --null,      
	CASE
		WHEN @VIOLATION_CODE = 'COMP' THEN 9765
		ELSE NULL
	END,
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
	 @APLUS_REPORT_ORDERED,    
	 @DRIVER_ID,    
	 @DRIVER_NAME,    
	 null,    
	 null,    
	 @AT_FAULT,    
	 @CHARGEABLE,  
	 null,    
	 null,    
	 null,    
	 null,  
	 null                
	)      
END

--select * from APP_PRIOR_LOSS_INFO where lob = '2' and AMOUNT_PAID= 99999
--sp_find 'prior',p






GO

