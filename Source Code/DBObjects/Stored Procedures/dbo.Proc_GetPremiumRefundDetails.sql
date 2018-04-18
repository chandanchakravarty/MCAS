IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPremiumRefundDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPremiumRefundDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================              
-- Author   : Pradeep Kushwaha  
-- Create date: 24-Nov-2010          
-- Description: Get Premium refund details
-- DROP PROC Proc_GetPremiumRefundDetails        
-- Proc_GetPremiumRefundDetails  
-- =============================================                        
                           
CREATE PROC [dbo].[Proc_GetPremiumRefundDetails]    
 
AS                        
BEGIN 
SELECT APPLICANT_TYPE,
FIRST_NAME,
MIDDLE_NAME,
LAST_NAME ,CPF_CNPJ,ADDRESS1,NUMBER,ADDRESS2,COMPLIMENT,
DISTRICT,
STATE,
CITY,
ZIP_CODE,
EMAIL,
BANK_NUMBER,
BANK_BRANCH, 
BANK_BRANCH,
ACCOUNT_NUMBER,
--USE ACCOUNT NUMBER
ACCOUNT_TYPE


FROM CLT_APPLICANT_LIST WITH(NOLOCK)
 
END

GO

