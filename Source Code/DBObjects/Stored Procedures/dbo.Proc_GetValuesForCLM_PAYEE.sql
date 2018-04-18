IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesForCLM_PAYEE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesForCLM_PAYEE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetValuesForCLM_PAYEE                            
Created by      : Vijay Arora                            
Date            : 6/1/2006                            
Purpose     : To get the values from table named CLM_PAYEE                            
Revison History :                            
Used In  : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
-- drop PROC dbo.Proc_GetValuesForCLM_PAYEE   992,3                         
CREATE PROC [dbo].[Proc_GetValuesForCLM_PAYEE]                             
(                            
 @CLAIM_ID int,                            
 @ACTIVITY_ID int,     
                      
-- @EXPENSE_ID int,    -------------------------------------------------------------------
 --@PAYEE_ID int      ,-- MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK:1029)
  @LANG_ID int    =1                       
                      
)                            
AS                            
BEGIN                            
 SELECT P.CLAIM_ID,                            
 P.PAYEE_ID,        
 P.EXPENSE_ID,                        
 P.ACTIVITY_ID,                        
 P.PARTY_ID,                            
 P.PAYMENT_METHOD, -- 11787 as PAYMENT_METHOD,                            
 P.ADDRESS1,                            
 P.ADDRESS2,                            
 P.CITY,                            
 P.STATE,                            
 P.ZIP,                            
 P.COUNTRY,                            
 P.NARRATIVE,                            
 /*CP.REFERENCE,                            
 CP.[NAME],*/        
 CP.BANK_NUMBER,  --Modified by Santosh Kumar Gautam on 16 March 2011 itrck:974          
 CP.BANK_BRANCH,  --Modified by Santosh Kumar Gautam on 16 March 2011 itrck:974      
 P.INVOICE_DUE_DATE,  --Added by Santosh Kumar Gautam on 15 Nov 2010 
 P.INVOICE_NUMBER,  
 P.INVOICE_SERIAL_NUMBER,   --Added by Santosh Kumar Gautam on 10 Dec 2010 
 P.PAYEE_BANK_ID,             --Added by Santosh Kumar Gautam on 10 Dec 2010 
 P.INVOICE_DATE,  
 CP.ACCOUNT_NUMBER,                            
 CP.ACCOUNT_NAME,                    
 P.AMOUNT,         
-- Added by Asfa (24-Mar-2008) - iTrack issue #3936        
 ACI.CHECK_NUMBER,        
 --CASE ISNULL(ACI.GL_UPDATE,0) WHEN 0 THEN '' WHEN 2 THEN 'Void' ELSE 'Cleared' END AS STATUS,        
CASE  --COMMENTED BY SHUBHANSHU PANDEY ON 23/09/2011(PAGE CRASH ISSUE(GL_UPDATE COLUMN NOT FOUND.))        
  /*when ISNULL(ACI.GL_UPDATE,0) = 2 THEN CASE WHEN @LANG_ID=1 THEN 'Void' ELSE 'Vazio' END         
  WHEN ISNULL(ACI.GL_UPDATE,0) = 1 and isnull(aci.is_bnk_reconciled, 'N') = 'Y' THEN  CASE WHEN @LANG_ID=1 THEN 'Cleared' ELSE 'Cancelado' END   
  WHEN ISNULL(ACI.GL_UPDATE,0) = 1 and isnull(aci.is_bnk_reconciled, 'N') = 'N' THEN  CASE WHEN @LANG_ID=1 THEN 'Issued' ELSE 'Emitido' END        
  ELSE CASE*/
   WHEN @LANG_ID=1 THEN 'Not Issued' ELSE 'Não Emitidos' --END                
 END AS [STATUS],        
        
-- P.INVOICED_BY,                  
 P.INVOICE_NUMBER,                  
 Convert(varchar(10),P.INVOICE_DATE,101) AS INVOICE_DATE,                
 P.SERVICE_TYPE,                  
 P.SERVICE_DESCRIPTION,            
 P.SECONDARY_PARTY_ID,      
 ISNULL(p.PAYEE_PARTY_ID,'') AS PAYEE_PARTY_ID ,         
 ISNULL(P.FIRST_NAME,'') AS FIRST_NAME,        
 ISNULL(P.LAST_NAME,'') AS LAST_NAME,                            
 ISNULL(P.TO_ORDER_DESC,'') AS   TO_ORDER_DESC   ,
  dbo.fun_GetLookupDesc (CP.PARTY_TYPE,@LANG_ID) AS  [TYPE],
  CA.ACTIVITY_STATUS,
  P.REIN_RECOVERY_NUMBER     ,
  P.RECOVERY_TYPE
 FROM CLM_PAYEE P                            
 LEFT JOIN CLM_PARTIES CP ON CP.PARTY_ID = P.PARTY_ID AND CP.CLAIM_ID = P.CLAIM_ID                            
 LEFT JOIN CLM_ACTIVITY CA ON CA.CLAIM_ID = P.CLAIM_ID AND CA.ACTIVITY_ID= P.ACTIVITY_ID        
 LEFT JOIN ACT_CHECK_INFORMATION ACI ON ACI.CHECK_ID= CA.CHECK_ID        
 WHERE P.CLAIM_ID = @CLAIM_ID  AND  P.ACTIVITY_ID=@ACTIVITY_ID-- AND EXPENSE_ID=@EXPENSE_ID                           
END                            
        
        

GO

