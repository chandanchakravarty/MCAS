IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : Dbo.Proc_GetApplicationInformation                  
Created by      : Anurag Verma                  
Date                    : 28/05/2005                  
Purpose         : To get the application information                   
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
Modify By : Vijay                  
Modify On : 24 May,2005                  
Purpose   : For showing the complete adddress                  
Modify By : Vijay                  
Modify On : 31 May,2005                  
Purpose   : For showing the LOB from MNT_LOB_MASTER                  
Modify By : Anurag                  
Modify On : 29 June,2005                  
Purpose   : using isnull inside query                  
                  
Modify By : Anurag Verma                  
Modify On : 28 July,2005                  
Purpose   : fetching attention_note field in the select query                  
                
Modify By : Pawan Papreja                
Modify On : 27 Dec, 2005                 
Purpose   : Included Application State code in Client top                
------   ------------       -------------------------*/                  
-- drop Proc dbo.Proc_GetApplicationInformation    28259,6,1 
CREATE  PROC [dbo].[Proc_GetApplicationInformation]              
(                  
 @CUSTOMERID  int,                  
 @APPID  int,                  
 @APPVERSIONID int                  
)                  
AS                  
BEGIN       
                 
SELECT CASE       
WHEN CLT.CUSTOMER_ADDRESS2 <> NULL       
THEN ISNULL(CLT.CUSTOMER_ADDRESS2,'') + ', '        
ELSE ''      
END       
CUSTOMER_ADDRESS2,ISNULL(CLT.CUSTOMER_FIRST_NAME,' ') +     
(CASE CLT.CUSTOMER_MIDDLE_NAME WHEN '' THEN '' ELSE + ' ' +ISNULL(CLT.CUSTOMER_MIDDLE_NAME,' ' ) END)+     
 (CASE CLT.CUSTOMER_LAST_NAME WHEN '' THEN '' ELSE + ' ' +ISNULL(CLT.CUSTOMER_LAST_NAME,' ' ) END)+    
 (CASE CLT.CUSTOMER_SUFFIX WHEN '' THEN '' ELSE + ' ' +ISNULL(CLT.CUSTOMER_SUFFIX,' ' ) END) AS CUSTOMERNAME,    
 --Done for Itrack Issue 5485 on 17 April 2009                   
 ISNULL(CLT.CUSTOMER_ADDRESS1,'') + ', '      
+ case when ISNULL(CLT.CUSTOMER_ADDRESS2,'') ='' then '' else ISNULL(CLT.CUSTOMER_ADDRESS2,'') + ', ' end        
 + IsNull(CLT.CUSTOMER_CITY,'') + ', ' + IsNull(CLT.NUMBER,'') + ', ' +  IsNull(STATE.STATE_NAME,'') + '/' + IsNull(STATE.STATE_CODE,'') + ', ' + IsNull(CLT.CUSTOMER_ZIP,'') ADDRESS,                  
 ISNULL(CLT.CUSTOMER_TYPE,'') CUSTOMER_TYPE,                  
 ISNULL(CLT.CUSTOMER_BUSINESS_PHONE,'') CUSTOMER_BUSINESS_PHONE,            
 ISNULL(CLT.CUSTOMER_HOME_PHONE,'') CUSTOMER_HOME_PHONE,            
--Commented By Pawan                
-- isnull(AL.APP_NUMBER,'') + '-' + isnull(AL.APP_STATUS,'') + '(' + isnull(LV.LOB_DESC,'') + ':' +                   
isnull(AL.APP_NUMBER,'') + ' (' + ltrim(rtrim(STATELOB.state_code)) +')-' + isnull(AL.APP_STATUS,'') + ' (' + isnull(LV.LOB_DESC,'') + ': ' +                   
----Added By Pawan  (Product)              
Isnull(PTL.LOOKUP_VALUE_DESC+',','')+                
 CONVERT(VARCHAR(15),isnull(AL.APP_EFFECTIVE_DATE,''),101) + '-' +                   
 CONVERT(VARCHAR(15),isnull(AL.APP_EXPIRATION_DATE,''),101) + ')' APPLICATION,                  
 AL.APP_VERSION, AL.APP_LOB,                  
 IsNull(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'') AS CUSTOMER_TYPE_DESC,                  
case                   
  when (CLT.CUSTOMER_ATTENTION_NOTE is null) then '0'                  
  when (CLT.CUSTOMER_ATTENTION_NOTE='') then '0'                   
  else CLT.CUSTOMER_ATTENTION_NOTE                  
  end CUSTOMER_ATTENTION_NOTE,          
AL.STATE_ID,AL.APP_STATUS,AL.APP_EFFECTIVE_DATE,AL.APP_LOB  ,AL.IS_ACTIVE      
--RAvindra:09-11-2009    
,ISNULL(AL.SHOW_QUOTE,'0') SHOW_QUOTE ,     
Al.APP_AGENCY_ID     
FROM  APP_LIST AL                 
 RIGHT OUTER JOIN CLT_CUSTOMER_LIST CLT ON AL.CUSTOMER_ID = CLT.CUSTOMER_ID                  
INNER JOIN MNT_LOB_MASTER LV ON LV.LOB_ID=AL.APP_LOB                  
LEFT JOIN MNT_COUNTRY_LIST COUNTRY ON COUNTRY.COUNTRY_ID = CLT.CUSTOMER_COUNTRY                  
LEFT JOIN MNT_COUNTRY_STATE_LIST STATE ON CLT.CUSTOMER_COUNTRY = STATE.COUNTRY_ID AND CLT.CUSTOMER_STATE = STATE.STATE_ID                  
--Added by Pawan on 27/12/2005                
LEFT JOIN MNT_COUNTRY_STATE_LIST STATELOB ON AL.STATE_ID= STATELOB.STATE_ID --AND CLT.CUSTOMER_STATE = STATE.STATE_ID                  
                
LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP ON CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                  
--Added by Pawan on 30/12/2005 for Product type              
LEFT JOIN MNT_LOOKUP_VALUES PTL ON AL.POLICY_TYPE= PTL.LOOKUP_UNIQUE_ID                
WHERE    (AL.CUSTOMER_ID = @CUSTOMERID)   AND (AL.APP_ID=@APPID) AND (AL.APP_VERSION_ID=@APPVERSIONID);                  
                  
END                  
                
      
GO

