IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteTempCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteTempCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*------------------------------------------------------------------------   
Proc Name       : dbo.Proc_DeleteTempCustAgencyPayments   
Created by      : Swastika Gaur 
Date            :     
Purpose         :    
Revison History :    
Used In  : Wolverine    

-----        -------------------------------------------------------------*/    
-- DROP PROC dbo.Proc_DeleteTempCustAgencyPayments    
-- Proc_DeleteTempCustAgencyPayments 'W001'
CREATE PROC dbo.Proc_DeleteTempCustAgencyPayments    
(
@ROW_ID int
)   
AS    
BEGIN    
 DELETE FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY WHERE IDEN_ROW_ID = @ROW_ID	
END


GO

