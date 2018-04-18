IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DepositReceipt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DepositReceipt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  --select * from ACT_CURRENT_DEPOSITS where DEPOSIT_NUMBER=5621
/*----------------------------------------------------------                              
Proc Name    : dbo.Proc_DepositReceipt                            
Created by   : Shubhanshu Pandey           
Date         : 10-2-2010                  
Purpose      :        
                              
 ------------------------------------------------------------                                          
Date     Review By          Comments                                        
 drop proc dbo.Proc_DepositReceipt 300                
------   ------------       -------------------------*/                             
CREATE PROC [dbo].[Proc_DepositReceipt]  
(  
 @DEPOSIT_ID INT,  
 @LANG_ID INT=1  
)    
AS  
BEGIN  
  
SELECT   
 CASE WHEN @LANG_ID =1   
  THEN   
   CASE   
    WHEN ACDLI.IS_APPROVE = 'A' THEN 'Approved'   
       WHEN ACDLI.IS_APPROVE = 'R' THEN 'Refund'  -- CHANGE IT TO Y TO R
    ELSE '--'  
   END  
 ELSE   
      CASE   
    WHEN ACDLI.IS_APPROVE = 'A' THEN 'Aprovado'  
       WHEN ACDLI.IS_APPROVE = 'R' THEN 'Recusado'  
       ELSE '--'  
   END   
 END AS IS_APPROVE,  
    
 CASE WHEN @LANG_ID =1 then  TL.TRANS_TYPE_DESC      
 ELSE TLM.TRANS_TYPE_NAME END  AS EXCEPTION_REASON,    
 PCPL.POLICY_NUMBER,  
 ENDO.ENDORSEMENT_NO,  
 INSTALL.INSTALLMENT_NO,  
 ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME,  
 IB.OUR_NUMBER,  
ACDLI.IS_APPROVE,  
 dbo.fun_FormatCurrency(ACDLI.RISK_PREMIUM,@LANG_ID) RISK_PREMIUM,  
 dbo.fun_FormatCurrency(ACDLI.FEE,@LANG_ID) FEE,  
 dbo.fun_FormatCurrency(ACDLI.TAX,@LANG_ID) TAX,  
 dbo.fun_FormatCurrency(ACDLI.INTEREST,@LANG_ID) INTEREST,  
 dbo.fun_FormatCurrency(ACDLI.LATE_FEE,@LANG_ID) LATE_FEE,  
 dbo.fun_FormatCurrency(ISNULL(ACDLI.TOTAL_PREMIUM_COLLECTION,0),@LANG_ID) RECEIPT_AMOUNT,  
    ACDLI.INSTALLMENT_NO,  
    ACDLI.CD_LINE_ITEM_ID,  
    ACDLI.RECEIPT_NUM,  
    ACDLI.DEPOSIT_ID,  
    ACDLI.CUSTOMER_ID,  
    ACDLI.POLICY_ID,  
    ACDLI.POLICY_VERSION_ID,
    ACDLI.PAYMENT_DATE   --itrack 1323
FROM   
 ACT_CURRENT_DEPOSIT_LINE_ITEMS ACDLI WITH(NOLOCK)  
INNER JOIN   
 POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID = ACDLI.CUSTOMER_ID AND PCPL.POLICY_ID = ACDLI.POLICY_ID AND PCPL.POLICY_VERSION_ID = ACDLI.POLICY_VERSION_ID     
INNER JOIN  
  POL_INSTALLMENT_BOLETO IB WITH(NOLOCK) ON (IB.BOLETO_ID = ACDLI.BOLETO_NO)  
LEFT OUTER JOIN   
  TRANSACTIONTYPELIST TL WITH(NOLOCK) ON (TL.TRANS_TYPE_ID = ACDLI.EXCEPTION_REASON)   
LEFT OUTER JOIN   
  TRANSACTIONTYPELIST_MULTILINGUAL TLM WITH(NOLOCK) ON (TL.TRANS_TYPE_ID = TLM.TRANS_TYPE_ID AND TLM.LANG_ID=@LANG_ID)     
LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON  
ACDLI.CUSTOMER_ID=ENDO.CUSTOMER_ID AND   
ACDLI.POLICY_ID   =ENDO.POLICY_ID AND   
ACDLI.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID   
LEFT JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON   
IB.INSTALLEMT_ID=INSTALL.ROW_ID     
LEFT JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)                       
ON INSTALL.CO_APPLICANT_ID = T1.APPLICANT_ID      
WHERE ACDLI.DEPOSIT_ID = @DEPOSIT_ID    
END  

GO

