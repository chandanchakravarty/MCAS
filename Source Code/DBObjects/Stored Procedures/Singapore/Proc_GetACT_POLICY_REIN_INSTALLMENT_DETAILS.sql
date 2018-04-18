    
 /*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetACT_POLICY_REIN_INSTALLMENT_DETAILS          
Created by      : Aditya Goel   
Date            : 04/11/2011        
Purpose         : To fetch record FROM ACT_POLICY_REIN_INSTALLMENT_DETAILS          
Revison History :          
Used In         : Ebix Advantage web    
      
------------------------------------------------------------          
Date     Review By          Comments          
--Drop Proc dbo.Proc_GetACT_POLICY_REIN_INSTALLMENT_DETAILS  28070,922,1     
------   ------------       -------------------------*/             
ALTER PROCEDURE [dbo].[Proc_GetACT_POLICY_REIN_INSTALLMENT_DETAILS]                                  
(  
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT  
)                                 
AS                                  
BEGIN                                  
SELECT  IDEN_ROW_ID,MRCL.REIN_COMAPANY_NAME as REIN_COMAPANY_NAME ,CONTRACT_NUMBER,INSTALLMENT_NO,  
INSTALLMENT_AMOUNT,CONVERT(VARCHAR(10),INSTALLMENT_EFFECTIVE_DATE,111) INSTALLMENT_EFFECTIVE_DATE ,RELEASED_STATUS,  
REIN_INSTALLMENT_NO  
    
 from ACT_POLICY_REIN_INSTALLMENT_DETAILS APRID with(nolock) INNER JOIN MNT_REIN_COMAPANY_LIST MRCL with(nolock)  
  ON APRID.REIN_COMPANY_ID = MRCL.REIN_COMAPANY_ID        
  INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL with(nolock)  
  ON APRID.CUSTOMER_ID = PCPL.CUSTOMER_ID AND APRID.POLICY_ID = PCPL.POLICY_ID AND APRID.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID    
  WHERE APRID.CUSTOMER_ID = @CUSTOMER_ID AND APRID.POLICY_ID = @POLICY_ID AND APRID.POLICY_VERSION_ID = @POLICY_VERSION_ID                     
END         


--select * from ACT_POLICY_REIN_INSTALLMENT_DETAILS where CUSTOMER_ID = 28718
      
      
      