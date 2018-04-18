IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertTransactionLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertTransactionLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Proc Name  : dbo.Proc_InsertTransactionLog                                      
Created by      : Ashwani                              
Date            : 21/04/2005                                   
Purpose         : To insert the data into Transaction Log     
                               
Revison History :                                                            
                              
Modified By : Anshuman                              
Modified Date : 04/06/2005                              
Comments : 1. insert values in added column APP_ID, APP_VERSION_ID, QUOTE_ID,QUOTE_VERSION_ID                              
     2. RECORDED_BY_NAME is being fetched from table MNT_USER_LIST depending on RECORDED_BY_ID                              
                              
Modified By : Anshuman                              
Modified Date : 13/07/2005                              
Comments : Additional info is recorded as Application Number, Policy Number and Quote Number                              
                              
Modified By : Gaurav                              
Modified Date : 19/07/2005                              
Comments : App Version, Policy Version, Quote version is also needed in additional info                              
                            
Modified By : Sumit Chhabra                            
Modified Date : 08/11/2005                              
Comments : Decimal point has been added to Application Version                                   
            
Modified By : Anurag Verma            
Modified Date : 27/02/2007            
Comments : Insert in MNT_TRANSACTION_XML will not be executed if change_XML is null            
          
Modified By : Pradeep Kushwaha            
Modified Date : 15/04/2010            
Comments : Select TRANS_TYPE_NAME based on the LANG_ID and TRANS_TYPE_ID          
        
Modified By : Charles Gomes        
Modified Date : 20/04/2010            
Comments : Multilingual Support        
          
*/                                      
            
--drop proc dbo.Proc_InsertTransactionLog         
             
CREATE  PROC [dbo].[Proc_InsertTransactionLog]                                
(                                      
 @TRANS_TYPE_ID SMALLINT,                              
 @CLIENT_ID INT,                              
 @POLICY_ID INT,                              
 @POLICY_VER_TRACKING_ID SMALLINT,                              
 @RECORDED_BY  SMALLINT,                              
 @RECORDED_BY_NAME NVARCHAR(75),                              
 @RECORD_DATE_TIME DATETIME,                              
 @TRANS_DESC NVARCHAR(500),                              
 @ENTITY_ID INT,                              
 @ENTITY_TYPE NVARCHAR(5),                              
 @IS_COMPLETED NCHAR(1),                              
 @CHANGE_XML TEXT,                              
 @APP_ID  INT,                              
 @APP_VERSION_ID SMALLINT,                              
 @QUOTE_ID INT,                              
 @QUOTE_VERSION_ID SMALLINT,             
 -- @CUSTOM_INFO AND @ADDITIONAL_INFO SET MAX FOR ITRACK ISSUE #6891                           
 @CUSTOM_INFO NVARCHAR(MAX) = NULL,            
 @LANG_ID INT =1 ,        
 @ADDITIONAL_INFO NVARCHAR(MAX) = NULL   --Added by Charles on 20-Apr-2010 for Multilingual Implementation                          
)                                      
AS                                 
SET NOCOUNT OFF                                
BEGIN          
                        
DECLARE @TRANS_ID int,                              
@APPLICATION_CONVERT_CHECK int,                
@TEMPQQ_ID int,              
@TRANSDESC NVARCHAR(500)     
 --DECLARE @ADDITIONAL_INFO varchar(max) --Commented by Charles on 20-Apr-2010 for Multilingual Implementation                                           
                
 SELECT @APPLICATION_CONVERT_CHECK=-1                              
 --Select @TRANS_ID = isnull(max(TRANS_ID),0)+1 from MNT_TRANSACTION_LOG with(nolock)                             
 --Select @RECORDED_BY_NAME = RTRIM(USER_FNAME + ' ' + USER_LNAME) from MNT_USER_LIST with(nolock) where USER_ID=@RECORDED_BY                              
         
 --IF @ADDITIONAL_INFO IS NULL  --Added by Charles on 20-Apr-2010 for Multilingual Implementation                  
 --BEGIN                              
 SET @ADDITIONAL_INFO = '';                             
 --END        
       
 --Added by Charles on 23-Apr-2010 for Multilingual Implementation           
        
 SELECT * INTO #ADD_INFO FROM
 ( SELECT MLV.LOOKUP_UNIQUE_ID, ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)
  AS LOOKUP_VALUE_DESC      
 FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK) 
 LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM 
 WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID = MLVM.LOOKUP_UNIQUE_ID      
 AND MLVM.LANG_ID = @LANG_ID )T      
       
 DECLARE @APP_NUM_STR NVARCHAR(20), @APP_VER_STR NVARCHAR(20), @POL_NUM_STR NVARCHAR(20), @POL_VER_STR NVARCHAR(20), @Q_APP_STR NVARCHAR(20),      
 @STATE_STR NVARCHAR(20), @PRODUCT_STR NVARCHAR(20)      
       
 IF EXISTS(SELECT LOOKUP_UNIQUE_ID FROM #ADD_INFO WITH(NOLOCK))      
 BEGIN        
 SELECT @APP_NUM_STR = L1.LOOKUP_VALUE_DESC, 
  @APP_VER_STR = L2.LOOKUP_VALUE_DESC,
  @PRODUCT_STR = L3.LOOKUP_VALUE_DESC, 
  @POL_NUM_STR = L4.LOOKUP_VALUE_DESC,      
 @POL_VER_STR = L5.LOOKUP_VALUE_DESC, 
 @Q_APP_STR = L6.LOOKUP_VALUE_DESC, 
 @STATE_STR = L7.LOOKUP_VALUE_DESC 
 FROM #ADD_INFO L1 WITH(NOLOCK),      
 #ADD_INFO L2 WITH(NOLOCK),
 #ADD_INFO L3 WITH(NOLOCK),
 #ADD_INFO L4 WITH(NOLOCK),
 #ADD_INFO L5 WITH(NOLOCK),
 #ADD_INFO L6 WITH(NOLOCK),
 #ADD_INFO L7 WITH(NOLOCK)      
 WHERE L1.LOOKUP_UNIQUE_ID = 14562 AND L2.LOOKUP_UNIQUE_ID = 14563 AND 
 L3.LOOKUP_UNIQUE_ID = 14566 AND L4.LOOKUP_UNIQUE_ID = 14564 AND      
 L5.LOOKUP_UNIQUE_ID = 14565 AND L6.LOOKUP_UNIQUE_ID = 14568 AND L7.LOOKUP_UNIQUE_ID = 14567      
 END      
 ELSE      
 BEGIN      
 SELECT @APP_NUM_STR = 'Application Number', @APP_VER_STR = 'Application Version', @PRODUCT_STR = 'Product', @POL_NUM_STR = 'Policy Number',      
 @POL_VER_STR = 'Policy Version', @Q_APP_STR = 'Quick App', @STATE_STR = 'State'      
 END      
       
 DROP TABLE #ADD_INFO      
 --Added till here      
         
 IF(@CLIENT_ID != NULL OR @CLIENT_ID != 0)                              
 BEGIN                              
  --check if the request is having both application and policy no information..                
  --that is whether the request is coming when the application is being converted into policy                
   --in that case, let the LOB name be not specified in transaction log                
  IF((@APP_ID != NULL OR @APP_ID != 0) AND (@POLICY_ID != NULL OR @POLICY_ID != 0))                
   SELECT @APPLICATION_CONVERT_CHECK=1         
         
    --Added by Charles on 23-Apr-2010 for Multilingual Implementation            
    DECLARE @POLICY_STATUS NVARCHAR(25)      
    SELECT @POLICY_STATUS = UPPER(POLICY_STATUS) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID= @CLIENT_ID AND      
    POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID        
  --Added till here           
                 
   IF/*(@APP_ID != NULL OR @APP_ID != 0) OR*/ @POLICY_STATUS = 'APPLICATION'                                
   BEGIN             
   IF(@APPLICATION_CONVERT_CHECK=-1)                
   SELECT @ADDITIONAL_INFO = @ADDITIONAL_INFO + @APP_NUM_STR +' = ' + A.APP_NUMBER + ';'+@APP_VER_STR+' = ' + A.APP_VERSION + ';' + @PRODUCT_STR +      
   ' = ' + ISNULL(L_MULTI.LOB_DESC,L.LOB_DESC) + ';'         
   FROM /*APP_LIST*/POL_CUSTOMER_POLICY_LIST  A with(nolock)       
   LEFT OUTER JOIN MNT_LOB_MASTER L with(nolock)  ON A.POLICY_LOB=L.LOB_ID /*APP_LOB*/ 
   LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL L_MULTI with(nolock)  
   ON L_MULTI.LOB_ID=L.LOB_ID /*APP_LOB*/               
   AND LANG_ID = @LANG_ID
   WHERE A.CUSTOMER_ID=@CLIENT_ID AND A.POLICY_ID=@POLICY_ID and A.POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID --A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                     
   ELSE                
   SELECT @ADDITIONAL_INFO = @ADDITIONAL_INFO + @APP_NUM_STR +' = ' +
    A.APP_NUMBER + ';'+@APP_VER_STR+' = ' + A.APP_VERSION + ';'          
   FROM /*APP_LIST*/POL_CUSTOMER_POLICY_LIST  A with(nolock)
    LEFT OUTER JOIN MNT_LOB_MASTER L 
    with(nolock)  ON A.POLICY_LOB=L.LOB_ID /*APP_LOB*/          
    LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL L_MLT WITH(NOLOCK) ON L_MLT.LOB_ID = L.LOB_ID AND 
    LANG_ID = @LANG_ID
   WHERE A.CUSTOMER_ID=@CLIENT_ID AND A.POLICY_ID=@POLICY_ID and A.POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID --A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                     
   END           
                              
   IF(@POLICY_ID != NULL OR @POLICY_ID != 0) AND @POLICY_STATUS != 'APPLICATION'                             
   BEGIN                                   
  IF(@APPLICATION_CONVERT_CHECK=-1)       
  BEGIN               
   SELECT @ADDITIONAL_INFO = @ADDITIONAL_INFO +
    @POL_NUM_STR +' = ' + P.POLICY_NUMBER + ';'+
    @POL_VER_STR+' = ' + P.POLICY_DISP_VERSION + ';'       
   + @PRODUCT_STR +' = ' + ISNULL(L_MULTI.LOB_DESC,L.LOB_DESC) + ';'             
   FROM POL_CUSTOMER_POLICY_LIST  P with(nolock) 
   JOIN MNT_LOB_MASTER L with(nolock) ON P.POLICY_LOB=L.LOB_ID 
   JOIN MNT_LOB_MASTER_MULTILINGUAL L_MULTI with(nolock) ON 
   L_MULTI.LOB_ID=L.LOB_ID  AND           
    LANG_ID = @LANG_ID
   WHERE  P.POLICY_ID=@POLICY_ID and P.POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID and P.CUSTOMER_ID=@CLIENT_ID                       
  END      
  ELSE                
  BEGIN      
   SELECT @ADDITIONAL_INFO = @ADDITIONAL_INFO 
   + @POL_NUM_STR +' = ' + P.POLICY_NUMBER + ';'+@POL_VER_STR+' = ' 
   + P.POLICY_DISP_VERSION +  ';'       
   FROM POL_CUSTOMER_POLICY_LIST P  with(nolock) 
   JOIN MNT_LOB_MASTER L with(nolock) ON P.POLICY_LOB=L.LOB_ID   
   JOIN MNT_LOB_MASTER_MULTILINGUAL L_MULTI with(nolock) ON 
   L_MULTI.LOB_ID=L.LOB_ID  AND           
    LANG_ID = @LANG_ID      
   WHERE P.POLICY_ID=@POLICY_ID and P.POLICY_VERSION_ID=@POLICY_VER_TRACKING_ID and P.CUSTOMER_ID=@CLIENT_ID       
  END                     
   END                              
             
 if(@QUOTE_ID != NULL AND @QUOTE_ID != 0)                              
 begin              
    select @ADDITIONAL_INFO = @ADDITIONAL_INFO + @Q_APP_STR +' = ' + QQ_NUMBER + ';'+@STATE_STR+' = ' + QQ_STATE + ';'+@PRODUCT_STR+' = ' + QQ_TYPE + ';'       
    from CLT_QUICKQUOTE_LIST with(nolock) where QQ_ID=@QUOTE_ID and CUSTOMER_ID=@CLIENT_ID                              
 end                              
 else             
 begin            
 --added by pravesh            
             
 select @TEMPQQ_ID=QQ_ID from CLT_QUICKQUOTE_LIST with(nolock) where customer_id=@CLIENT_ID and app_id=@APP_ID and app_version_id=@APP_VERSION_ID            
 --                     
  if (@TEMPQQ_ID != null and @TEMPQQ_ID != 0)            
  begin            
     select @ADDITIONAL_INFO = @ADDITIONAL_INFO + @Q_APP_STR +' = ' + QQ_NUMBER + ';'+@STATE_STR+' = ' + QQ_STATE + ';'+@PRODUCT_STR+      
     ' = ' + QQ_TYPE + ';' from CLT_QUICKQUOTE_LIST with(nolock) where QQ_ID=@TEMPQQ_ID and CUSTOMER_ID=@CLIENT_ID                              
  end            
 end            
end                                          
            
IF (@CUSTOM_INFO!=NULL OR @CUSTOM_INFO!='')                            
BEGIN                            
   SELECT @ADDITIONAL_INFO = @ADDITIONAL_INFO + @CUSTOM_INFO                    
END              
             
 SELECT @TRANS_ID = ISNULL(MAX(TRANS_ID),0)+1 FROM MNT_TRANSACTION_LOG WITH(NOLOCK)                             
 SELECT @RECORDED_BY_NAME = RTRIM(USER_FNAME + ' ' + USER_LNAME) FROM MNT_USER_LIST WITH(NOLOCK) WHERE USER_ID=@RECORDED_BY                              
       
--Added by pradeep Kushwaha               
IF(@TRANS_TYPE_ID!=0 AND (@TRANS_DESC IS NULL OR RTRIM(LTRIM(ISNULL(@TRANS_DESC,''))) = '') )  --@TRANSDESC check added by Charles on 25-May-2010         
BEGIN               
 SELECT @TRANSDESC=ISNULL(ML.TRANS_TYPE_NAME,TRANSACTIONTYPELIST.TRANS_TYPE_NAME)          
 FROM TRANSACTIONTYPELIST WITH(NOLOCK)          
 LEFT OUTER JOIN TRANSACTIONTYPELIST_MULTILINGUAL ML WITH(NOLOCK)           
 ON ML.LANG_ID = @LANG_ID AND ML.TRANS_TYPE_ID = TRANSACTIONTYPELIST.TRANS_TYPE_ID          
 WHERE TRANSACTIONTYPELIST.TRANS_TYPE_ID=@TRANS_TYPE_ID                 
END            
ELSE          
BEGIN          
 SELECT @TRANSDESC=@TRANS_DESC          
END           
              
            
 INSERT INTO MNT_TRANSACTION_LOG                              
 (                                
  TRANS_ID,              
   TRANS_TYPE_ID,                              
  CLIENT_ID,                
  POLICY_ID,                              
  POLICY_VER_TRACKING_ID,                              
  RECORDED_BY,                              
  RECORDED_BY_NAME,                 
  RECORD_DATE_TIME,                              
  TRANS_DESC,                              
  ENTITY_ID,                              
  ENTITY_TYPE,                              
  IS_COMPLETED,                     
  APP_ID,                              
  APP_VERSION_ID,                              
  QUOTE_ID,                              
  QUOTE_VERSION_ID,                              
  ADDITIONAL_INFO  ,            
  LANG_ID                            
  )                                
 VALUES                                 
  (                                
  @TRANS_ID,                              
  @TRANS_TYPE_ID,                              
  @CLIENT_ID,                              
  @POLICY_ID,                              
  @POLICY_VER_TRACKING_ID,                              
  @RECORDED_BY,                              
  @RECORDED_BY_NAME,                              
  @RECORD_DATE_TIME,                              
  @TRANSDESC,                              
  @ENTITY_ID,                              
  @ENTITY_TYPE,                              
  @IS_COMPLETED,                              
  @APP_ID,                              
  @APP_VERSION_ID,                              
  @QUOTE_ID,                          
  @QUOTE_VERSION_ID,                              
  @ADDITIONAL_INFO,            
  @LANG_ID                              
 )                                    
            
IF (@CHANGE_XML IS NOT NULL)            
BEGIN             
 INSERT INTO MNT_TRANSACTION_XML                              
 (                              
  TRANS_ID,                              
  TRANS_DETAILS                              
 )                              
 VALUES                              
 (                              
  @TRANS_ID,                              
  @CHANGE_XML                              
 )                              
                 
END               
END                    
RETURN @TRANS_ID                    
SET NOCOUNT ON 
GO

