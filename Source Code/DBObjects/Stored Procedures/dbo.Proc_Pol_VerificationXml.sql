IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Pol_VerificationXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Pol_VerificationXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================
Proc Name       : dbo.PROC_Pol_VerificationXml  
Created by      : Ashwani   
Date            :15 Sep. 2006
Purpose       	: Return the string  against the given 
		  customerID,polID,polVersionID  
Revison History :  
Used In        :    
======================================================================================================
Date     Review By          Comments  
======================================================================================================*/
--drop proc  dbo.Proc_Pol_VerificationXml
create   proc dbo.Proc_Pol_VerificationXml
(  
 @CUSTOMER_ID int,  
 @POL_ID int,  
 @POL_VERSION_ID smallint 
)  
as  
begin  
select  APP_VERIFICATION_XML
from POL_CUSTOMER_POLICY_LIST with(nolock) --by praveash
 where   CUSTOMER_ID =@CUSTOMER_ID AND   POLICY_ID=@POL_ID AND   POLICY_VERSION_ID=@POL_VERSION_ID  
end  












GO

