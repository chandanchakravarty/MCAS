            
 --DROP PROC DBO.Proc_QuickAppInfo            
CREATE PROC [dbo].[Proc_QuickAppInfo]            
(            
@CUSTOMER_ID INT = NULL,            
@POLICY_ID INT = NULL,            
@POLICY_VERSION_ID SMALLINT = NULL,            
@FLAG NCHAR(2), -- I/U/F            
@POLICY_LOB NVARCHAR(5) = NULL,             
@POLICY_SUBLOB NVARCHAR(5) = NULL,             
@APP_TERMS NVARCHAR(5) = NULL,             
@APP_EFFECTIVE_DATE DATETIME = NULL,             
@APP_EXPIRATION_DATE DATETIME = NULL,             
@BILL_TYPE_ID INT = NULL,             
@INSTALL_PLAN_ID INT = NULL,            
@DIV_ID_DEPT_ID_PC_ID NVARCHAR(25) = NULL,            
@POLICY_CURRENCY INT=NULL,            
@NEW_POLICY_ID INT OUT             
)            
AS            
BEGIN            
 DECLARE @BILL_TYPE NVARCHAR(2),            
 @APP_ID INT,            
 @APP_VERSION_ID SMALLINT,            
 @DIV_ID SMALLINT,            
 @DEPT_ID SMALLINT,            
 @PC_ID SMALLINT,            
 @APP_NUMBER varchar(10),            
 @APP_LOB varchar(10)             
             
 SELECT @DIV_ID = 0, @DEPT_ID = 0, @PC_ID = 0            
             
 IF @DIV_ID_DEPT_ID_PC_ID IS NOT NULL            
 BEGIN              
  SELECT @DIV_ID = dbo.Piece(@DIV_ID_DEPT_ID_PC_ID,'^',1)            
  SELECT @DEPT_ID = dbo.Piece(@DIV_ID_DEPT_ID_PC_ID,'^',2)            
  SELECT @PC_ID = dbo.Piece(@DIV_ID_DEPT_ID_PC_ID,'^',3)              
 END            
             
 SET @NEW_POLICY_ID = 0            
 SELECT @BILL_TYPE  = ISNULL([TYPE],'') FROM MNT_LOOKUP_VALUES WITH(NOLOCK) WHERE LOOKUP_UNIQUE_ID = ISNULL(@BILL_TYPE_ID,0)              
             
 /* added by sonal to fetch app number*/            
 DECLARE @APP_TABLE TABLE (            
  QQNNUMBER NVARCHAR(50))            
            
  --Change            by  kuldeep for marine cargo also on 19-3-2012
 SELECT @APP_LOB =  LOB_CODE FROM MNT_LOB_MASTER where LOB_ID=cast(@POLICY_LOB as smallint)           
 INSERT INTO @APP_TABLE exec Proc_GenerateQuickQuoteNumber @APP_LOB,null             
             
 select @APP_NUMBER =  QQNNUMBER from @APP_TABLE            
 IF @FLAG = 'I' --Insert            
 BEGIN            
  --Create Quick App Temporary Customer            
  --IF NOT EXISTS(SELECT CUSTOMER_ID FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_ID= @CUSTOMER_ID)            
  --BEGIN            
      --Will be done through scripts            
   --SET IDENTITY_INSERT CLT_CUSTOMER_LIST ON               
   --INSERT INTO CLT_CUSTOMER_LIST (CUSTOMER_ID,CUSTOMER_FIRST_NAME,IS_ACTIVE,CREATED_DATETIME,CUSTOMER_TYPE)             
   --VALUES(-1,'Quick App Customer','Y',GETDATE(),11110 /* Personal */)               
   --SET IDENTITY_INSERT CLT_CUSTOMER_LIST OFF               
   --RETURN            
  --END            
              
  SELECT @NEW_POLICY_ID = ISNULL(MAX(POLICY_ID),0)+1  FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                        
  WHERE CUSTOMER_ID = @CUSTOMER_ID            
                                                                                                               
  SET @POLICY_VERSION_ID = 1               
                    
  SELECT @APP_ID = ISNULL(MAX(APP_ID),0)+1 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID             
  DECLARE @QQ_ID INT            
              
  SELECT @QQ_ID = ISNULL(MAX(QQ_ID),0)+1 FROM CLT_QUICKQUOTE_LIST WITH(NOLOCK)            
              
  IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE            
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @NEW_POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)            
  BEGIN            
              
              
             
               
               
   INSERT INTO POL_CUSTOMER_POLICY_LIST (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, APP_ID, APP_VERSION_ID, BILL_TYPE,            
   POLICY_LOB, POLICY_SUBLOB, APP_TERMS,STATE_ID,             
   APP_INCEPTION_DATE, APP_EFFECTIVE_DATE, APP_EXPIRATION_DATE, POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE,            
   BILL_TYPE_ID, INSTALL_PLAN_ID, APP_STATUS, POLICY_STATUS,IS_ACTIVE,            
   CREATED_DATETIME,DIV_ID,DEPT_ID,PC_ID,APP_VERSION,APP_NUMBER,POLICY_CURRENCY,POLICY_DISP_VERSION)            
   VALUES            
   (@CUSTOMER_ID,@NEW_POLICY_ID,@POLICY_VERSION_ID,@APP_ID,@POLICY_VERSION_ID,@BILL_TYPE, @POLICY_LOB,@POLICY_SUBLOB,@APP_TERMS,0,            
   @APP_EFFECTIVE_DATE,@APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE,@APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE,            
   @BILL_TYPE_ID,@INSTALL_PLAN_ID, 'QAPP', NULL,'Y',GETDATE(),@DIV_ID,@DEPT_ID,@PC_ID,'1.0',@APP_NUMBER,@POLICY_CURRENCY,'1.0')            
               
   select @NEW_POLICY_ID            
               
       -- maintain quick quote list at quick application level            
        INSERT INTO [dbo].[CLT_QUICKQUOTE_LIST]            
   ([CUSTOMER_ID],[QQ_ID],[QQ_NUMBER],[QQ_TYPE],[QQ_XML],[QQ_APP_NUMBER],[IS_ACTIVE],[QQ_RATING_REPORT],[QQ_XML_TIME]            
   ,[QQ_RATING_TIME],[QQ_STATE],[APP_ID],[APP_VERSION_ID])                    
   VALUES( @CUSTOMER_ID, @QQ_ID, @APP_NUMBER, @APP_LOB, NULL, @APP_NUMBER, 'Y', NULL, NULL, NULL, NULL, @NEW_POLICY_ID, @POLICY_VERSION_ID)            
               
  END            
 END            
             
 IF @FLAG = 'U' --Update            
 BEGIN            
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE            
  CUSTOMER_ID = ISNULL(@CUSTOMER_ID,@CUSTOMER_ID) AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)            
  BEGIN            
   UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_LOB = @POLICY_LOB, POLICY_SUBLOB = @POLICY_SUBLOB, APP_TERMS = @APP_TERMS,             
   APP_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE, APP_EXPIRATION_DATE = @APP_EXPIRATION_DATE,             
   APP_INCEPTION_DATE = @APP_EFFECTIVE_DATE, POLICY_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE, POLICY_EXPIRATION_DATE = @APP_EXPIRATION_DATE,             
   BILL_TYPE_ID = @BILL_TYPE_ID,             
   INSTALL_PLAN_ID = @INSTALL_PLAN_ID, LAST_UPDATED_DATETIME = GETDATE(), BILL_TYPE = @BILL_TYPE,            
   DIV_ID = @DIV_ID, DEPT_ID = @DEPT_ID, PC_ID = @PC_ID,POLICY_CURRENCY=@POLICY_CURRENCY              
   WHERE CUSTOMER_ID = ISNULL(@CUSTOMER_ID,@CUSTOMER_ID) AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID               
  END            
 END            
             
 IF @FLAG = 'F' --Fetch            
 BEGIN            
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE            
  CUSTOMER_ID = ISNULL(@CUSTOMER_ID,@CUSTOMER_ID) AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)            
  BEGIN            
   SELECT POL.POLICY_LOB, POLICY_SUBLOB, APP_TERMS, CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,103) as APP_EFFECTIVE_DATE   ,          
   CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,103) as APP_EXPIRATION_DATE, BILL_TYPE_ID, INSTALL_PLAN_ID, APP_STATUS,            
   CAST(ISNULL(DIV_ID,0) AS NVARCHAR) + '^' + CAST(ISNULL(DEPT_ID,0) AS NVARCHAR) + '^' + CAST(ISNULL(PC_ID,0) AS NVARCHAR) AS DIV_ID_DEPT_ID_PC_ID,POLICY_CURRENCY,            
   APP_NUMBER,CLT.QQ_NUMBER,POL.IS_ACTIVE FROM POL_CUSTOMER_POLICY_LIST POL LEFT OUTER JOIN CLT_QUICKQUOTE_LIST CLT ON POL.CUSTOMER_ID=CLT.CUSTOMER_ID            
   AND POL.POLICY_ID=CLT.APP_ID AND POL.POLICY_VERSION_ID=CLT.APP_VERSION_ID            
   WHERE POL.CUSTOMER_ID = ISNULL(@CUSTOMER_ID,@CUSTOMER_ID) AND POL.POLICY_ID = @POLICY_ID AND POL.POLICY_VERSION_ID = @POLICY_VERSION_ID            
               
               
  END            
 END            
END   
  