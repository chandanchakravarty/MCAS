IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTempCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTempCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*------------------------------------------------------------------------       
Proc Name       : dbo.Proc_FetchTempCustAgencyPayments       
Created by      :  Praveen kasana    
Date            :  27 Dec 2007       
Purpose         :  To Capturer TL before Deleting Records       
Revison History :        
Used In  : Wolverine        
    
-----        -------------------------------------------------------------*/        
-- DROP PROC dbo.Proc_FetchTempCustAgencyPayments        
-- Proc_FetchTempCustAgencyPayments 'W001'    
CREATE PROC dbo.Proc_FetchTempCustAgencyPayments        
(    
 @IDEN_ROW_ID int  
)       
AS        
BEGIN        
  
SELECT   
 upper(POLICY_NUMBER) as POLICY_NUMBER,  
 CUSTOMER_ID,  
 AMOUNT,  
 MIN_DUE,  
 TOTAL_DUE,  
 MODE,
 POLICY_ID,
 POLICY_VERSION_ID  
FROM  
ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY WITH(NOLOCK)  
WHERE IDEN_ROW_ID = @IDEN_ROW_ID  
END




GO

