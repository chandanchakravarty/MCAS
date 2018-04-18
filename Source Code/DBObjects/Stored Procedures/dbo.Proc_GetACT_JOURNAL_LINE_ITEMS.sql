IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_JOURNAL_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_JOURNAL_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : Proc_GetACT_JOURNAL_LINE_ITEMS        
Created by      : Vijay Joshi        
Date            : 7/9/2005        
Purpose     : Get values in Journal Entry Details table        
Revison History :        
Used In  : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- DROP PROC dbo.Proc_GetACT_JOURNAL_LINE_ITEMS        
-- Proc_GetACT_JOURNAL_LINE_ITEMS 638  
create PROC dbo.Proc_GetACT_JOURNAL_LINE_ITEMS        
(        
 @JE_LINE_ITEM_ID  int        
)        
AS        
BEGIN        
 SELECT JE_LINE_ITEM_ID, JOURNAL_ID, DIV_ID, DEPT_ID,        
  PC_ID, ACT_JOURNAL_LINE_ITEMS.CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,        
  convert(varchar(30),convert(money,isnull(ACT_JOURNAL_LINE_ITEMS.AMOUNT,0)) ,1) AMOUNT      
 , TYPE, REGARDING, REF_CUSTOMER, ACT_JOURNAL_LINE_ITEMS.ACCOUNT_ID,  
--GLA.ACC_DISP_NUMBER+' - '+GLA.ACC_DESCRIPTION AS ACC_DESC,  
 ( SELECT    
  CASE WHEN T1.ACC_PARENT_ID IS NULL     
  THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')      
  ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')    
  END AS ACC_DESCRIPTION    
  FROM ACT_GL_ACCOUNTS T1     
  LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID     
  WHERE T1.ACC_LEVEL_TYPE='AS'    AND T1.ACCOUNT_ID =GLA.ACCOUNT_ID  
  ) AS ACC_DESC,  
  BILL_TYPE, NOTE,
   CASE WHEN substring(POLICY_NUMBER,10,1) != ' '     
  THEN POLICY_NUMBER+'.'+'0'
  ELSE POLICY_NUMBER       
  END AS POLICY_NUMBER,        
  IsNull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'') AS REF_CUSTOMER_NAME ,         
  ACT_JOURNAL_LINE_ITEMS.IS_ACTIVE, ACT_JOURNAL_LINE_ITEMS.CREATED_BY, ACT_JOURNAL_LINE_ITEMS.CREATED_DATETIME,        
  CASE TYPE         
   WHEN 'OTH' THEN REGARDING ELSE NULL        
  END OTHER_REGARDING,        
  CASE ACT_JOURNAL_LINE_ITEMS.TYPE        
   WHEN 'CUS' THEN IsNull(REGARDING_CUSTOMER.CUSTOMER_FIRST_NAME,'') + ' ' +IsNull(REGARDING_CUSTOMER.CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(REGARDING_CUSTOMER.CUSTOMER_LAST_NAME,'')        
   WHEN 'MOR' THEN MNT_HOLDER_INTEREST_LIST.HOLDER_NAME        
   WHEN 'VEN' THEN MNT_VENDOR_LIST.COMPANY_NAME  
   WHEN 'AGN' THEN MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME        
   WHEN 'TAX' THEN MNT_TAX_ENTITY_LIST.TAX_NAME        
  END AS REGARDING_NAME,TRAN_CODE  
        
 FROM        
  ACT_JOURNAL_LINE_ITEMS   
  LEFT JOIN ACT_GL_ACCOUNTS GLA ON GLA.ACCOUNT_ID = ACT_JOURNAL_LINE_ITEMS.ACCOUNT_ID     
  LEFT JOIN CLT_CUSTOMER_LIST ON Convert(varchar, CLT_CUSTOMER_LIST.CUSTOMER_ID) = ACT_JOURNAL_LINE_ITEMS.REF_CUSTOMER        
  LEFT JOIN MNT_VENDOR_LIST ON Convert(varchar, MNT_VENDOR_LIST.VENDOR_ID) = ACT_JOURNAL_LINE_ITEMS.REGARDING  AND ACT_JOURNAL_LINE_ITEMS.TYPE = 'VEN'        
  LEFT JOIN MNT_HOLDER_INTEREST_LIST ON Convert(varchar, MNT_HOLDER_INTEREST_LIST.HOLDER_ID) = ACT_JOURNAL_LINE_ITEMS.REGARDING AND ACT_JOURNAL_LINE_ITEMS.TYPE = 'MOR'        
  LEFT JOIN CLT_CUSTOMER_LIST REGARDING_CUSTOMER ON Convert(varchar, REGARDING_CUSTOMER.CUSTOMER_ID) = ACT_JOURNAL_LINE_ITEMS.REGARDING AND ACT_JOURNAL_LINE_ITEMS.TYPE = 'CUS'         
  LEFT JOIN MNT_TAX_ENTITY_LIST ON Convert(varchar, MNT_TAX_ENTITY_LIST.TAX_ID) = ACT_JOURNAL_LINE_ITEMS.REGARDING AND ACT_JOURNAL_LINE_ITEMS.TYPE = 'TAX'        
  LEFT JOIN MNT_AGENCY_LIST ON Convert(varchar, MNT_AGENCY_LIST.AGENCY_ID) = ACT_JOURNAL_LINE_ITEMS.REGARDING AND ACT_JOURNAL_LINE_ITEMS.TYPE = 'AGN'        
        
 WHERE         
  JE_LINE_ITEM_ID = @JE_LINE_ITEM_ID        
        
END        
  




GO

