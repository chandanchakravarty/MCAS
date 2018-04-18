IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerDetailsManager]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerDetailsManager]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*------------------------------------------------------------      
 Proc Name    : Dbo.Proc_GetCustomerDetailsManager      
 Created by    : Gaurav      
 Date     : 24/06/2005      
 Purpose     : To get Account Information  from CLT_CUSTOMER_LIST table      
 Revison History   :      
 ModifiedBy    : Anurag Verma      
 Modified On    : 29/06/2005      
 Purpose     : putting isnull check in query over customer_name      
       
 ------------------------------------------------------------      
 DROP PROC   dbo.Proc_GetCustomerDetailsManager 28163     
 Date     Review By          Comments      
 ------   ------------       -------------------------*/      
 CREATE   PROC [dbo].[Proc_GetCustomerDetailsManager]      
 (      
  @CustomerID  int      
 )      
 AS      
 BEGIN      
       
 SELECT isnull(CUSTOMER_FIRST_NAME,'') +' '+ isnull(CUSTOMER_MIDDLE_NAME,'') +' '+ isnull(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME ,    
 isnull(CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,    
 ISNULL(CUSTOMER_ADDRESS1,'')+ CASE WHEN CUSTOMER_ADDRESS1!='' THEN CASE WHEN NUMBER !='' THEN ', ' ELSE '' END ELSE '' END +ISNULL(NUMBER,'') AS CUSTOMER_ADDRESS1      
 ,ISNULL(CUSTOMER_ADDRESS2,'') AS CUSTOMER_ADDRESS2,ISNULL(CUSTOMER_HOME_PHONE,'') AS CUSTOMER_HOME_PHONE,      
 ISNULL(CUSTOMER_FAX,'') AS CUSTOMER_FAX,    
 --CASE CUSTOMER_TYPE WHEN '11110' THEN  ISNULL(EMPLOYER_HOMEPHONE,'')    
 --  ELSE    
   ISNULL(CUSTOMER_BUSINESS_PHONE,'')    
   --END      
 AS CUSTOMER_BUSINESS_PHONE,      
 CASE WHEN CUSTOMER_CITY!='' THEN ISNULL(CUSTOMER_CITY,'') ELSE '' END+CASE WHEN CUSTOMER_CITY != '' THEN  CASE WHEN MCSL.STATE_CODE !='' THEN  '/' ELSE '' END ELSE '' END  +CASE WHEN MCSL.STATE_CODE !='' THEN ISNULL(MCSL.STATE_CODE,'') ELSE '  ' END+CASE WHEN MCSL.STATE_CODE !='' THEN  CASE WHEN CUSTOMER_ZIP!='' THEN ' - ' ELSE '' END ELSE '' END +ISNULL(CUSTOMER_ZIP,'') AS CITY_STATE_ZIP ,      
 --ISNULL(MUL.USER_FNAME,'')+' '+ISNULL(MUL.USER_LNAME,'') AS USER_NAME,      
 --ISNULL(MUL1.USER_FNAME,'')+' '+ISNULL(MUL1.USER_LNAME,'') AS USER_NAME_ACC,      
 --ISNULL(MUL2.USER_FNAME,'')+' '+ISNULL(MUL2.USER_LNAME,'') AS USER_NAME_CSR,      
 case       
   when (CLT.CUSTOMER_ATTENTION_NOTE is null) then '0'      
   when (CLT.CUSTOMER_ATTENTION_NOTE='') then '0'       
   else CLT.CUSTOMER_ATTENTION_NOTE      
  end CUSTOMER_ATTENTION_NOTE      
 --WHERE CUSTOMER_ID=100      
 FROM CLT_CUSTOMER_LIST CLT WITH(NOLOCK) 
 /*INNER*/ LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK) ON CLT.CUSTOMER_STATE = MCSL.STATE_ID  --Changed from INNER JOIN, Charles, 3-Jun-10, Itrack 122    
 --LEFT JOIN MNT_USER_LIST MUL ON MUL.USER_ID = CLT.CUSTOMER_PRODUCER_ID      
 --LEFT JOIN MNT_USER_TYPES MUT ON MUL.USER_TYPE_ID =MUT.USER_TYPE_ID      
 --LEFT JOIN MNT_USER_LIST MUL1 ON MUL1.USER_ID =CLT.CUSTOMER_ACCOUNT_EXECUTIVE_ID      
 --LEFT JOIN MNT_USER_LIST MUL2 ON MUL2.USER_ID =CLT.CUSTOMER_CSR      
 WHERE CUSTOMER_ID= @CustomerID       
     
 END      

GO

