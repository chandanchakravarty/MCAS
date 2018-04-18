IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXML_ACT_PREMIUM_PROCESS_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXML_ACT_PREMIUM_PROCESS_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------        
CREATED BY   : Vijay Joshi        
CREATED DATE TIME : 05-01-2006        
PURPOSE    :  Get the Premium XML from Table named ACT_PREMIUM_PROCESS_DETAILS
REVIEW HISTORY        
REVIEW BY  DATE  PURPOSE        
        
---------------------------------------------------------------------*/        
CREATE PROCEDURE dbo.Proc_GetXML_ACT_PREMIUM_PROCESS_DETAILS
AS        
BEGIN        
	SELECT POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, PREMIUM_XML 
	FROM ACT_PREMIUM_PROCESS_DETAILS WHERE ISNULL(PROCESS_STATUS,'') = ''
END        
        
      
    
  



GO

