IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTempCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTempCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop PROC dbo.Proc_GetCreditCardSweepHistory                   
--go 
/*------------------------------------------------------------------------     
Proc Name       : dbo.Proc_UpdateTempCustAgencyPayments     
Created by      :   
Date            :       
Purpose         :      
Revison History :      
Used In  : Wolverine      
  
-----        -------------------------------------------------------------*/      
-- DROP PROC dbo.Proc_UpdateTempCustAgencyPayments      
-- Proc_UpdateTempCustAgencyPayments 'W001'  
CREATE PROC dbo.Proc_UpdateTempCustAgencyPayments      
(  
@IDEN_ROW_ID int,  
@POLICY_ID int,  
@AGENCY_ID int,  
@POLICY_VERSION_ID int,  
@CUSTOMER_ID int,  
@POLICY_NUMBER varchar(15),  
@TOTAL_DUE decimal(18,2),  
@MIN_DUE decimal(18,2),  
@MODE int,  
@AMOUNT decimal(18,2),  
@CREATED_BY_USER INT  
)     
AS      
BEGIN  
      declare @pol_agency_id int
      select @pol_agency_id = agency_id  from pol_customer_policy_list
      where CUSTOMER_ID = @CUSTOMER_ID and
      policy_id   = @Policy_id and 
      POLICY_VERSION_ID = @POLICY_VERSION_ID        
if (@pol_agency_id = @AGENCY_ID)   
BEGIN  
 UPDATE ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY  
 SET POLICY_ID = @POLICY_ID,  
  AGENCY_ID = @AGENCY_ID,POLICY_VERSION_ID= @POLICY_VERSION_ID,CUSTOMER_ID=@CUSTOMER_ID,  
  POLICY_NUMBER=@POLICY_NUMBER,TOTAL_DUE=@TOTAL_DUE,MIN_DUE=@MIN_DUE,MODE=@MODE,AMOUNT=@AMOUNT,CREATED_BY_USER=@CREATED_BY_USER  
 WHERE IDEN_ROW_ID = @IDEN_ROW_ID 
END
ELSE
BEGIN
      set @IDEN_ROW_ID = -1
END   
END  
  
  
  
  
  
  
  
GO

