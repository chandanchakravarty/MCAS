IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyPhoneNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyPhoneNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
 Proc Name       : dbo.Proc_GetAgencyPhoneNo            
 Created by      : Ashwani            
 Date            : 21 Dec.,2005    
 Purpose        : Return Agency Phone No    
 Revison History :            
 Used In   : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   
--drop PROC Dbo.Proc_GetAgencyPhoneNo           
CREATE PROC Dbo.Proc_GetAgencyPhoneNo            
(            
 @CUSTOMERID  INT,      
 @POLICYID  INT,    
 @POLICYVERSIONID INT      
)            
AS            
BEGIN            
 SELECT AGN.AGENCY_PHONE,POL.BILL_TYPE    
 FROM POL_CUSTOMER_POLICY_LIST POL WITH (NOLOCK) INNER JOIN    
 MNT_AGENCY_LIST AGN WITH (NOLOCK) ON POL.AGENCY_ID=AGN.AGENCY_ID    
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID    
END          
      
    
  



GO

