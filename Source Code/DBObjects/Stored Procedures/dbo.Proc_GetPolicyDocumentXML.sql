IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDocumentXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDocumentXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
 Crated By  : Praveen Kumar       
 Created On : 20 Aug 2010     
 Purpose    : Fetch XML FOR GENERATE POLICY PDF  
 DROP PROC Proc_GetPolicyDocumentXML
*/
CREATE proc [dbo].[Proc_GetPolicyDocumentXML]                  
(      
 @CUSTOMER_ID int=null,    
 @POLICY_ID int=null,    
 @POLICY_VERSION_ID int=null  
 )  
 AS  
 BEGIN  
 select DEC_CUSTOMERXML from ACT_PREMIUM_NOTICE_PROCCESS_LOG   
 where CUSTOMER_ID=@CUSTOMER_ID   
 AND POLICY_ID=@POLICY_ID   
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  ISNULL(CALLED_FOR,'')<>'PREM_NOTICE'
 END 

GO

