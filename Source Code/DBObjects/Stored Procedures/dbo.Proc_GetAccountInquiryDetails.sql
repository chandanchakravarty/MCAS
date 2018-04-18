
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccountInquiryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccountInquiryDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------
--Proc Name          : dbo.Proc_GetAccountInquiryDetails 
--Created by         :          
--Date               :  5 August 2011       
--------------------------------------------------------
--Date     Review By          Comments        
------   ------------       -------------------------*/       
-- drop proc dbo.[Proc_GetAccountInquiryDetails]      
CREATE  PROCEDURE [dbo].[Proc_GetAccountInquiryDetails]      
(        
    @CUSTOMER_ID INT =NULL,
	@POLICY_ID INT =NULL,
	@POLICY_NUMBER VARCHAR(20),
	@LANG_ID INT, 
	@CARRIER_CODE NVARCHAR(20)
)        
AS       
--BEGIN      

--End