  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetBillType_TEST          
Created by      : Nidhi          
Date            : 31/05/2005          
Purpose         : To get list           
Revison History :          
Used In         :   Wolverine          
Modified By  : Praveen        
Modified Date : 06 May 2008        
drop proc Proc_GetBillType      
--drop proc dbo.Proc_GetBillType  17,-1,-1,-1,'POL',2      
------------------------------------------------------------          
Date     Review By          Comments          
      
------   ------------       -------------------------*/          
ALTER PROC [dbo].[Proc_GetBillType]        
(          
 @LOB_ID  int,        
 @CUSTOMER_ID int,        
 @APP_POL_ID int,        
 @APP_POL_VERSION_ID int,        
 @CALLED_FROM varchar(10) ,      
 @LANG_ID INT = NULL          
)          
AS        
BEGIN      
 -----------------------------Commnnted on 06 May 2008        
 --BEGIN          
 --IF  (@LOB_ID = 1 OR @LOB_ID =6)          
 --BEGIN          
 --SELECT MLV.LOOKUP_UNIQUE_ID,          
 --   MLV.LOOKUP_VALUE_DESC,          
 --   MLV.LOOKUP_VALUE_CODE          
 --  FROM MNT_LOOKUP_VALUES MLV          
 --  INNER JOIN MNT_LOOKUP_TABLES MLT ON           
 --   MLV.LOOKUP_ID = MLT.LOOKUP_ID          
 --  WHERE MLT.LOOKUP_NAME = 'BLCODE' AND MLV.Lookup_unique_id in(8459,11150,11191,11276,11277,11278)          
 --  ORDER BY MLV.LOOKUP_VALUE_DESC ASC          
 --          
 --          
 --END          
 --ELSE          
 ----BEGIN          
 --SELECT MLV.LOOKUP_UNIQUE_ID,          
 --   MLV.LOOKUP_VALUE_DESC,          
 --   MLV.LOOKUP_VALUE_CODE   ,*        
 --  FROM MNT_LOOKUP_VALUES MLV         
 --  INNER JOIN MNT_LOOKUP_TABLES MLT ON           
 --   MLV.LOOKUP_ID = MLT.LOOKUP_ID          
 --  WHERE MLT.LOOKUP_NAME = 'BLCODE' AND MLV.Lookup_unique_id in(8459,8460,11191)          
 --  ORDER BY MLV.LOOKUP_VALUE_DESC ASC          
 --        
 -- SELECT BILL_TYPE_ID AS LOOKUP_UNIQUE_ID, MLV.LOOKUP_VALUE_DESC,          
 --    MLV.LOOKUP_VALUE_CODE  FROM APP_LIST APP        
 -- LEFT JOIN MNT_LOOKUP_VALUES MLV        
 -- ON ISNULL(APP.BILL_TYPE_ID,0) = MLV.LOOKUP_UNIQUE_ID        
 -- WHERE CUSTOMER_ID=901 AND APP_ID=27  AND APP_VERSION_ID = 1        
 --END          
 --END          
 ------------------------------------------------------        
         
 CREATE TABLE tempBILL_TEMP         
 (        
  LOOKUP_UNIQUE_ID  bigint,        
  LOOKUP_VALUE_DESC varchar(200),        
  LOOKUP_VALUE_CODE varchar(100)        
 )        
         
 IF (@CALLED_FROM = 'APP')        
  BEGIN        
    INSERT tempBILL_TEMP        
    SELECT BILL_TYPE_ID AS LOOKUP_UNIQUE_ID,      
    CASE WHEN @LANG_ID = 2 THEN  MLV_MULTI.LOOKUP_VALUE_DESC + '-(Inactive)'       
    ELSE MLV.LOOKUP_VALUE_DESC + '-(Inactive)' END LOOKUP_VALUE_DESC,          
    MLV.LOOKUP_VALUE_CODE  FROM APP_LIST APP  WITH(NOLOCK)       
    LEFT JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK)        
    ON ISNULL(APP.BILL_TYPE_ID,0) = MLV.LOOKUP_UNIQUE_ID        
    LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV_MULTI ON      
    ISNULL(APP.BILL_TYPE_ID,0) = MLV_MULTI.LOOKUP_UNIQUE_ID        
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_POL_ID          
    AND APP_VERSION_ID = @APP_POL_VERSION_ID        
    AND MLV.IS_ACTIVE = 'N'        
           
  END        
 IF (@CALLED_FROM = 'POL')        
  BEGIN        
    INSERT tempBILL_TEMP        
    SELECT BILL_TYPE_ID AS LOOKUP_UNIQUE_ID,       
     CASE WHEN @LANG_ID = 2 THEN  MLV_MULTI.LOOKUP_VALUE_DESC + '-(Inactive)'       
    ELSE MLV.LOOKUP_VALUE_DESC + '-(Inactive)' END LOOKUP_VALUE_DESC,        
    MLV.LOOKUP_VALUE_CODE  FROM POL_CUSTOMER_POLICY_LIST POL  WITH(NOLOCK)       
    LEFT JOIN MNT_LOOKUP_VALUES MLV  WITH(NOLOCK)       
    ON ISNULL(POL.BILL_TYPE_ID,0) = MLV.LOOKUP_UNIQUE_ID       
     LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV_MULTI ON      
    ISNULL(POL.BILL_TYPE_ID,0) = MLV_MULTI.LOOKUP_UNIQUE_ID         
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@APP_POL_ID          
    AND POLICY_VERSION_ID = @APP_POL_VERSION_ID        
    AND MLV.IS_ACTIVE = 'N'        
         
  END        
         
 IF  (@LOB_ID = 1 OR @LOB_ID =6)          
  BEGIN          
    SELECT MLV.LOOKUP_UNIQUE_ID,          
    MLV.LOOKUP_VALUE_DESC,          
    MLV.LOOKUP_VALUE_CODE          
    FROM MNT_LOOKUP_VALUES MLV          
    INNER JOIN MNT_LOOKUP_TABLES MLT WITH(NOLOCK) ON           
    MLV.LOOKUP_ID = MLT.LOOKUP_ID          
    WHERE MLT.LOOKUP_NAME = 'BLCODE'         
    AND MLV.LOOKUP_UNIQUE_ID IN(114327,114328,114329)         
    AND MLV.IS_ACTIVE = 'Y'        
    UNION        
    SELECT * FROM tempBILL_TEMP        
    ORDER BY LOOKUP_VALUE_DESC ASC          
          
    --DROP TABLE tempBILL_TEMP      
  END          
 ELSE          
  BEGIN          
       
    SELECT MLV.LOOKUP_UNIQUE_ID,          
    CASE WHEN @LANG_ID = 2 THEN MLV_MULTI.LOOKUP_VALUE_DESC ELSE MLV.LOOKUP_VALUE_DESC END LOOKUP_VALUE_DESC      
    ,          
    MLV.LOOKUP_VALUE_CODE          
    FROM MNT_LOOKUP_VALUES MLV          
    INNER JOIN MNT_LOOKUP_TABLES MLT ON           
    MLV.LOOKUP_ID = MLT.LOOKUP_ID          
    LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV_MULTI ON      
    MLV.LOOKUP_UNIQUE_ID = MLV_MULTI.LOOKUP_UNIQUE_ID        
    WHERE MLT.LOOKUP_NAME = 'BLCODE'         
    AND MLV.LOOKUP_UNIQUE_ID IN(114327,114328,114329) --   8459,8460,11191 Removed on 1-Feb-2012 for TFS Bug # 1211   
    AND MLV.IS_ACTIVE = 'Y'         
    UNION        
    SELECT * FROM tempBILL_TEMP        
    ORDER BY LOOKUP_VALUE_DESC ASC          
       
   --DROP TABLE tempBILL_TEMP    
        
  END          
        
        
    DROP TABLE tempBILL_TEMP    
        
END        
        
        
  