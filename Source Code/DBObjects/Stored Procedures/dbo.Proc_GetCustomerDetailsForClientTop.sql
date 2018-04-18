IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerDetailsForClientTop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerDetailsForClientTop]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                            
Proc Name   : Dbo.Proc_GetCustomerDetailsForClientTop                                            
Created by   : Ravindra Gupta    
Date    : 08-26-2009    
Purpose    : To get Account Information  from CLT_CUSTOMER_LIST table                                            
Revison History  :                                            
Used In             :   Wolverine                                            
------------------------------------------------------------                                            
Date Review By          Comments                                            
------   ------------       -------------------------*/                                            
--DROP PROC dbo.Proc_GetCustomerDetailsForClientTop 2727,1   
CREATE PROC [dbo].[Proc_GetCustomerDetailsForClientTop]                                            
(                                            
 @CustomerID  int ,       
 @LANG_ID INT = 1               
)                             
AS                                            
BEGIN                                            
                         
SELECT    
ISNULL(CLT.CUSTOMER_TYPE,'') CUSTOMER_TYPE, 
ISNULL(CLT.CUSTOMER_FIRST_NAME,'')CUSTOMER_FIRST_NAME,                                            
ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') CUSTOMER_MIDDLE_NAME,                                            
ISNULL(CLT.CUSTOMER_LAST_NAME,'') CUSTOMER_LAST_NAME,                         
ISNULL(CLT.CUSTOMER_SUFFIX,'') CUSTOMER_SUFFIX,                                            
ISNULL(CLT.CUSTOMER_ADDRESS1,'') CUSTOMER_ADDRESS1,ISNULL(CLT.CUSTOMER_ADDRESS2,'') CUSTOMER_ADDRESS2,                                            
ISNULL(CLT.CUSTOMER_CITY,'') CUSTOMER_CITY,    
ISNULL(CLT.CUSTOMER_COUNTRY,'') CUSTOMER_COUNTRY,    
ISNULL(CLT.CUSTOMER_STATE,'') CUSTOMER_STATE,    
ISNULL(MNT_COUNTRY_LIST.COUNTRY_NAME,'') CUSTOMER_COUNTRY_NAME,                                          
ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') CUSTOMER_STATE_NAME,                                            
ISNULL(MNT_COUNTRY_STATE_LIST.STATE_CODE,'') as CUSTOMER_STATE_CODE,                                          
ISNULL(CLT.CUSTOMER_ZIP,'') CUSTOMER_ZIP,                                        
ISNULL(CLT.CUSTOMER_BUSINESS_PHONE,'') CUSTOMER_BUSINESS_PHONE,    
ISNULL(CLT.CUSTOMER_HOME_PHONE,'') CUSTOMER_HOME_PHONE,                                            
ISNULL(CLT.IS_ACTIVE,'') IS_ACTIVE,                                            
ISNULL(CLT.PREFIX,'')PREFIX ,
ISNULL(CUSTOMER_PREFIX_LOOKUP_MULTI.LOOKUP_VALUE_DESC,IsNull(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,''))  AS CUSTOMER_TYPE_DESC,                                            
IsNull(MNT_ACTIVITY_MASTER.ACTIVITY_DESC,'') As PREFIX_DESC,    
IsNull(PC.CUSTOMER_FIRST_NAME,'') +                                      
CASE ISNULL(PC.CUSTOMER_MIDDLE_NAME ,'') WHEN '' THEN '' ELSE ' ' + ISNULL(PC.CUSTOMER_MIDDLE_NAME,'') END +                       
 case when (CLT.CUSTOMER_ATTENTION_NOTE is null) then '0'                      
   when (CLT.CUSTOMER_ATTENTION_NOTE='') then '0'                                             
  else CLT.CUSTOMER_ATTENTION_NOTE                                            
 end CUSTOMER_ATTENTION_NOTE,    
ISNULL(AG.AGENCY_PHONE,'') AS AGENCY_PHONE     
    
FROM  CLT_CUSTOMER_LIST CLT WITH(NOLOCK)                                           
/*INNER JOIN*/ LEFT OUTER JOIN MNT_COUNTRY_LIST WITH(NOLOCK) --Changed from INNER JOIN, Charles, 3-Jun-10, Itrack 122      
 ON CLT.CUSTOMER_COUNTRY = MNT_COUNTRY_LIST.COUNTRY_ID                                            
/*INNER JOIN*/ LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) --Changed from INNER JOIN, Charles, 3-Jun-10, Itrack 122       
 ON CLT.CUSTOMER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID 
  LEFT JOIN MNT_ACTIVITY_MASTER WITH(NOLOCK)      
 ON  MNT_ACTIVITY_MASTER.ACTIVITY_ID = CLT.PREFIX        
/*INNER JOIN*/ LEFT OUTER JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP WITH(NOLOCK) --Changed from INNER JOIN, Charles, 3-Jun-10, Itrack 122     
 ON  CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                                 
LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL CUSTOMER_PREFIX_LOOKUP_MULTI WITH(NOLOCK)    
 ON  CUSTOMER_PREFIX_LOOKUP_MULTI.LOOKUP_UNIQUE_ID = CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID --CUSTOMER_PREFIX_LOOKUP.LOOKUP_UNIQUE_ID 
  AND CUSTOMER_PREFIX_LOOKUP_MULTI.LANG_ID= @LANG_ID
 
LEFT JOIN CLT_CUSTOMER_LIST PC WITH(NOLOCK)     
 ON CLT.CUSTOMER_PARENT = PC.CUSTOMER_ID     
 AND  CLT.CUSTOMER_ID  =  @CustomerID      
LEFT JOIN MNT_AGENCY_LIST AG WITH(NOLOCK)    
 ON CLT.CUSTOMER_AGENCY_ID = AG.AGENCY_ID    
                                                  
WHERE CLT.CUSTOMER_ID  =  @CustomerID                                     
                                            
                                       
END           
GO

