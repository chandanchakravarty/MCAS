IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertTempCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertTempCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*------------------------------------------------------------------------   
Proc Name       : dbo.Proc_InsertTempCustAgencyPayments   
Created by      :  
Date            :     
Purpose         :    
Revison History :    
Used In  : Wolverine    

-----        -------------------------------------------------------------*/    
-- DROP PROC dbo.Proc_InsertTempCustAgencyPayments    
-- Proc_InsertTempCustAgencyPayments 'W001'
CREATE PROC dbo.Proc_InsertTempCustAgencyPayments    
(
@IDEN_ROW_ID int OUT,
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
	INSERT INTO ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY
	VALUES(@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID,@POLICY_NUMBER,@AGENCY_ID,@AMOUNT,
		   GETDATE(),NULL,NULL,NULL,NULL,@MIN_DUE,@TOTAL_DUE,@MODE,@CREATED_BY_USER)
	SELECT @IDEN_ROW_ID = ISNULL(MAX(IDEN_ROW_ID),1) FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY (NOLOCK)
END  
else
BEGIN
      Set @IDEN_ROW_ID = -1
END
END








GO

