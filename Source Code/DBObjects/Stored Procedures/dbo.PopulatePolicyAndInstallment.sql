IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PopulatePolicyAndInstallment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PopulatePolicyAndInstallment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                              
Proc Name        : dbo.[PopulatePolicyAndInstallment]                              
Created by       : Praveen Kumar                            
Date             : 25/05/2010                              
Purpose          : retrieving data from POL_CUSTOMER_POLICY_LIST,ACT_POLICY_INSTALLMENT_DETAILS                                                 
Used In          : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
 --DROP PROCEDURE PopulatePolicyAndInstallment '2010450196000001'  
  
CREATE PROCEDURE [dbo].[PopulatePolicyAndInstallment] --'2010450196000001'  
@POLICY_NUMBER NVARCHAR(75) = NULL, 
@POLICY_VERSION_ID int=null
AS  
BEGIN  
  

SELECT DISTINCT(POLICY_NUMBER)+ ' ' + 'Ver'+' ' + convert(nvarchar, POLICY_VERSION_ID) as POLICY,POLICY_NUMBER,
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) ORDER BY POLICY_NUMBER ASC  
   
select ACT.INSTALLMENT_NO,ACT.ROW_ID,  POL.POLICY_NUMBER   
from ACT_POLICY_INSTALLMENT_DETAILS AS ACT  WITH(NOLOCK)  
LEFT JOIN POL_CUSTOMER_POLICY_LIST AS POL ON ACT.POLICY_ID=POL.POLICY_ID     
AND ACT.POLICY_VERSION_ID=POL.POLICY_VERSION_ID AND ACT.CUSTOMER_ID=POL.CUSTOMER_ID   
  
WHERE pol.POLICY_NUMBER=@POLICY_NUMBER  and ACT.POLICY_VERSION_ID=@POLICY_VERSION_ID
  
END


 

GO

