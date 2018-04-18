IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchCommonRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchCommonRecords]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
Proc Name        : dbo.[Proc_FetchCommonRecords]                                  
Created by       : Praveen Kumar                                
Date             : 02/06/2010                                  
Purpose          : retrieving common records from ACT_POLICY_INSTALLMENT_DETAILS andPOL_INSTALLMENT_BOLETO    
Used In          : Ebix Advantage                              
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
 --DROP PROCEDURE Proc_FetchCommonRecords     
    
CREATE PROCEDURE [dbo].[Proc_FetchCommonRecords]    
    
AS    
BEGIN    
    
 select POL.BOLETO_HTML  
from ACT_POLICY_INSTALLMENT_DETAILS AS ACT  WITH(NOLOCK)    
INNER JOIN POL_INSTALLMENT_BOLETO AS POL  WITH(NOLOCK) ON     
ACT.POLICY_ID=POL.POLICY_ID AND ACT.POLICY_VERSION_ID=POL.POLICY_VERSION_ID AND ACT.CUSTOMER_ID=POL.CUSTOMER_ID AND    
ACT.ROW_ID=POL.INSTALLEMT_ID AND ACT.INSTALLMENT_NO=POL.INSTALLMENT_NO    
end 
GO

