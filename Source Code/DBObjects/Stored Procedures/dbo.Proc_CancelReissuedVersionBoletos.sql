IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CancelReissuedVersionBoletos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CancelReissuedVersionBoletos]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*----------------------------------------------------------                                                              
Proc Name       : Proc_CancelReissuedVersionBoletos    
                  
Modified by   : Lalit Kr Chauhan          
Modified On   : May 25,2011          
Purpose       : To Cancel Re-issued  endorsement Boletos.    
drop proc Proc_CancelReissuedVersionBoletos 28070,408,3,2,398,'05/26/2011'    
*/          
CREATE Proc [dbo].[Proc_CancelReissuedVersionBoletos]    
(          
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,          
 @POLICY_VERSION_ID INT,    
 @RE_ISSUED_VERSION_ID INT,    
 @MODIFIED_BY INT = null ,    
 @LAST_UPDATED_DATETIME DATETIME = null ,    
 @NO_CANCELLLED_BOLETOS INT = NULL OUT    
)          
AS      
BEGIN     
      
DECLARE @CURRENT_TERM INT,@TRAN_TYPE INT,@CO_APPLICANT INT,    
@CANCELLED_PREMIUM DECIMAL(25,2) ,@CANCELLED_INTEREST DECIMAL(25,2),@CANELLED_FEES DECIMAL(25,2),    
@CANCELLED_TAXES DECIMAL(25,2),@CANCELLED_TOTAL DECIMAL(25,2),@INSTALLMENT_NO INT    
  DECLARE @RE_ISSUE_VERSION_PREMIUM DECIMAL(25,2)  
  DECLARE @PLAN_TYPE NVARCHAR(10)  
  DECLARE @INSTALL_PLAN_ID INT  
    
      
  SELECT @CURRENT_TERM = CURRENT_TERM,@TRAN_TYPE = TRANSACTION_TYPE ,  
  @INSTALL_PLAN_ID = INSTALL_PLAN_ID  
  FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) wHERE CUSTOMER_ID = @CUSTOMER_ID     
  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
  if(@TRAN_TYPE = 14560)  --if master policy    
  BEGIN    
   SELECT @CO_APPLICANT = CO_APPLICANT_ID FROM POL_POLICY_PROCESS wITH(NOLOCK)    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID    
  END    
  ELSE     
   SELECT @CO_APPLICANT = APPLICANT_ID FROM POL_APPLICANT_LIST wITH(NOLOCK)    
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_PRIMARY_APPLICANT = 1    
      
SELECT @PLAN_TYPE = PLAN_TYPE FROM ACT_INSTALL_PLAN_DETAIL WHERE IDEN_PLAN_ID =  @INSTALL_PLAN_ID  
  
      
 set @INSTALLMENT_NO=0     
      
 IF EXISTS (SELECT * FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)    
  WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND     
  POLICY_VERSION_ID = @RE_ISSUED_VERSION_ID )    
  BEGIN        
   UPDATE ACT_POLICY_INSTALLMENT_DETAILS   
   SET RELEASED_STATUS = 'C',    
   MODIFIED_BY = @MODIFIED_BY,LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME        
   WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND     
   POLICY_VERSION_ID = @RE_ISSUED_VERSION_ID   
   AND ISNULL(RELEASED_STATUS,'N') = 'N'     
    SET @NO_CANCELLLED_BOLETOS = @@ROWCOUNT    
      
   
        
    --set boleto inactive of cancelled installment    
    UPDATE POL_INSTALLMENT_BOLETO  SET IS_ACTIVE = 'N'    
    WHERE CUSTOMER_ID = @CUSTOMER_ID     
    AND POLICY_ID = @POLICY_ID     
    AND POLICY_VERSION_ID = @RE_ISSUED_VERSION_ID    
    AND INSTALLEMT_ID IN (SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)     
    WHERE INS.CUSTOMER_ID = POL_INSTALLMENT_BOLETO.CUSTOMER_ID AND      
    INS.POLICY_ID = POL_INSTALLMENT_BOLETO.POLICY_ID AND     
    INS.POLICY_VERSION_ID = POL_INSTALLMENT_BOLETO.POLICY_VERSION_ID    
    AND RELEASED_STATUS = 'C')     
  
      
 IF (@PLAN_TYPE ='MMANNUAL')  
 BEGIN  
   
  IF((SELECT SUM(WRITTEN_PREMIUM) FROM POL_PRODUCT_COVERAGES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID= @POLICY_ID  
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID ) = 0)  
   BEGIN  
     
    SELECT      
      @CANCELLED_PREMIUM = SUM(INS.INSTALLMENT_AMOUNT)*-1,    
      @CANCELLED_INTEREST = SUM(INS.INTEREST_AMOUNT)*-1,    
      @CANELLED_FEES =  SUM(INS.FEE)*-1,    
      @CANCELLED_TAXES =  SUM(INS.TAXES)*-1,    
      @CANCELLED_TOTAL = SUM(INS.TOTAL)*-1    
      FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)          
      WHERE INS.CUSTOMER_ID = @CUSTOMER_ID   
      AND INS.POLICY_ID =@POLICY_ID   
      AND INS.POLICY_VERSION_ID  = @RE_ISSUED_VERSION_ID  
        
       
        
      ----insert into installment data table  
     INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA(  
     POLICY_ID,  
     POLICY_VERSION_ID,  
     CUSTOMER_ID,  
     PLAN_ID,  
     APP_ID,  
     APP_VERSION_ID,  
     PLAN_DESCRIPTION,  
     PLAN_TYPE,  
     NO_OF_PAYMENTS,  
     MONTHS_BETWEEN,  
     PERCENT_BREAKDOWN1,  
     PERCENT_BREAKDOWN2,  
     PERCENT_BREAKDOWN3,  
     PERCENT_BREAKDOWN4,  
     PERCENT_BREAKDOWN5,  
     PERCENT_BREAKDOWN6,  
     PERCENT_BREAKDOWN7,  
     PERCENT_BREAKDOWN8,  
     PERCENT_BREAKDOWN9,  
     PERCENT_BREAKDOWN10,  
     PERCENT_BREAKDOWN11,  
     PERCENT_BREAKDOWN12,  
     MODE_OF_DOWN_PAYMENT,  
     INSTALLMENTS_IN_DOWN_PAYMENT,  
     MODE_OF_PAYMENT,  
     CURRENT_TERM,  
     IS_ACTIVE_PLAN,  
     TOTAL_PREMIUM,  
     TOTAL_INTEREST_AMOUNT,  
     TOTAL_FEES,  
     TOTAL_TAXES,  
     TOTAL_AMOUNT,  
     TRAN_TYPE,  
     TOTAL_TRAN_PREMIUM,  
     TOTAL_TRAN_INTEREST_AMOUNT,  
     TOTAL_TRAN_FEES,  
     TOTAL_TRAN_TAXES,  
     TOTAL_TRAN_AMOUNT,  
     CREATED_BY,  
     CREATED_DATETIME,       
     TOTAL_CHANGE_INFORCE_PRM,  
     PRM_DIST_TYPE,  
     TOTAL_INFO_PRM,  
     TOTAL_STATE_FEES,  
     TOTAL_TRAN_STATE_FEES  
     )  
             
      SELECT TOP 1   
       @POLICY_ID,  
    @POLICY_VERSION_ID,  
    @CUSTOMER_ID,  
    PLAN_ID,  
    @POLICY_ID,  
    @POLICY_VERSION_ID,  
    PLAN_DESCRIPTION,  
    PLAN_TYPE,  
    NO_OF_PAYMENTS,  
    MONTHS_BETWEEN,  
    PERCENT_BREAKDOWN1,  
    PERCENT_BREAKDOWN2,  
    PERCENT_BREAKDOWN3,  
    PERCENT_BREAKDOWN4,  
    PERCENT_BREAKDOWN5,  
    PERCENT_BREAKDOWN6,  
    PERCENT_BREAKDOWN7,  
    PERCENT_BREAKDOWN8,  
    PERCENT_BREAKDOWN9,  
    PERCENT_BREAKDOWN10,  
    PERCENT_BREAKDOWN11,  
    PERCENT_BREAKDOWN12,  
    MODE_OF_DOWN_PAYMENT,  
    INSTALLMENTS_IN_DOWN_PAYMENT,  
    MODE_OF_PAYMENT,  
    CURRENT_TERM,  
    IS_ACTIVE_PLAN,  
    (TOTAL_PREMIUM+@CANCELLED_PREMIUM),  
    (TOTAL_INTEREST_AMOUNT+@CANCELLED_INTEREST),  
    (TOTAL_FEES+@CANELLED_FEES),  
    (TOTAL_TAXES+@CANCELLED_TAXES),  
    (TOTAL_AMOUNT+@CANCELLED_TOTAL),  
    TRAN_TYPE,  
    @CANCELLED_PREMIUM,  
    @CANCELLED_INTEREST,  
    @CANELLED_FEES,  
    @CANCELLED_TAXES,  
    @CANCELLED_TOTAL,  
    CREATED_BY,  
    CREATED_DATETIME,      
    @CANCELLED_TOTAL,  
    PRM_DIST_TYPE,  
    TOTAL_INFO_PRM,  
    TOTAL_STATE_FEES,  
    TOTAL_TRAN_STATE_FEES  
      
        FROM  ACT_POLICY_INSTALL_PLAN_DATA   
      WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
      ORDER BY POLICY_VERSION_ID DESC  
        
        
        
      /*======================================  
      Insert into installment details table  
      ======================================*/  
        
     INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS(  
    POLICY_ID,  
    POLICY_VERSION_ID,  
    CUSTOMER_ID,  
    APP_ID,  
    APP_VERSION_ID,  
    INSTALLMENT_AMOUNT,  
    INSTALLMENT_EFFECTIVE_DATE,  
    RELEASED_STATUS,  
    INSTALLMENT_NO,  
    RISK_ID,  
    RISK_TYPE,  
    PAYMENT_MODE,  
    CURRENT_TERM,  
    PERCENTAG_OF_PREMIUM,  
    INTEREST_AMOUNT,  
    FEE,  
    TAXES,  
    TOTAL,  
    TRAN_INTEREST_AMOUNT,  
    TRAN_FEE,  
    TRAN_TAXES,  
    TRAN_TOTAL,  
    BOLETO_NO,  
    IS_BOLETO_GENRATED,  
    CREATED_BY,  
    CREATED_DATETIME,  
    MODIFIED_BY,  
    LAST_UPDATED_DATETIME,  
    TRAN_PREMIUM_AMOUNT,  
    CO_APPLICANT_ID,  
    PAID_AMOUNT,  
    RECEIVED_AMOUNT,  
    RECEIVED_DATE,  
    INSTALLMENT_EXPIRE_DATE,  
    ACC_CO_DISCOUNT,  
    IS_COMMISSION_PROCESS,  
    IS_PAID_TO_PAGNET,  
    PAGNET_DATE  
  
     )  
        
   SELECT    
    @POLICY_ID,  
    @POLICY_VERSION_ID,  
    @CUSTOMER_ID ,   
    @POLICY_ID,  
    @POLICY_VERSION_ID,    
    SUM(@CANCELLED_PREMIUM),  
    GETDATE(),  
    'U',    
    1,  
    NULL,  
    '',  
    0,  
    @CURRENT_TERM,    
    0,  
    @CANCELLED_INTEREST  
    ,@CANELLED_FEES,  
    @CANCELLED_TAXES,  
    @CANCELLED_TOTAL,  
    @CANCELLED_INTEREST,    
    @CANELLED_FEES,  
    @CANCELLED_TAXES,  
    @CANCELLED_TOTAL,  
    NULL,  
    NULL,  
    @MODIFIED_BY,    
    getdate(),  
    null,  
    null,  
    @CANCELLED_INTEREST,    
    @CO_APPLICANT,  
    0,  
    NULL,  
    null,  
    null  ,  
    null  ,  
    null  ,  
    null  ,  
    null   
  
  
        /*FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)    
        JOIN     
        POL_CUSTOMER_POLICY_LIST PCPLC WITH(NOLOCK)     
        ON PCPLC.CUSTOMER_ID = INS.CUSTOMER_ID AND     
        PCPLC.POLICY_ID = INS.POLICY_ID AND PCPLC.POLICY_VERSION_ID = INS.POLICY_VERSION_ID    
        WHERE INS.CUSTOMER_ID = @CUSTOMER_ID AND INS.POLICY_ID =@POLICY_ID AND PCPLC.CURRENT_TERM =@CURRENT_TERM    
        AND UPPER(INS.RELEASED_STATUS) = 'C'    
  */      
        
     
   END  
    
   
 END  
        
      /* commented by Lalit OCt 24 ,2011  
      tfs # 2236, 2239, 2240   
      re-issue version create duplicated undo item in installment table  
      
   IF EXISTS(SELECT * FROM ACT_POLICY_INSTALL_PLAN_DATA WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID    
      AND POLICY_VERSION_ID = @POLICY_VERSION_ID)     
      BEGIN    
           
       SELECT      
       @CANCELLED_PREMIUM = SUM(INS.INSTALLMENT_AMOUNT)*-1,    
       @CANCELLED_INTEREST = SUM(INS.INTEREST_AMOUNT)*-1,    
       @CANELLED_FEES =  SUM(INS.FEE)*-1,    
       @CANCELLED_TAXES =  SUM(INS.TAXES)*-1,    
       @CANCELLED_TOTAL = SUM(INS.TOTAL)*-1    
       FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)    
       JOIN     
       POL_CUSTOMER_POLICY_LIST PCPLC WITH(NOLOCK)     
       ON PCPLC.CUSTOMER_ID = INS.CUSTOMER_ID AND     
       PCPLC.POLICY_ID = INS.POLICY_ID AND PCPLC.POLICY_VERSION_ID = INS.POLICY_VERSION_ID    
       WHERE INS.CUSTOMER_ID = @CUSTOMER_ID AND INS.POLICY_ID =@POLICY_ID AND PCPLC.CURRENT_TERM =@CURRENT_TERM    
       AND UPPER(INS.RELEASED_STATUS) = 'C'    
           
       --UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET     
       UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET     
       TOTAL_PREMIUM = TOTAL_PREMIUM + @CANCELLED_PREMIUM,    
       TOTAL_INTEREST_AMOUNT = @CANCELLED_INTEREST + TOTAL_INTEREST_AMOUNT,    
       TOTAL_FEES = @CANELLED_FEES + TOTAL_FEES,    
       TOTAL_TAXES = @CANCELLED_TAXES + TOTAL_TAXES,    
       TOTAL_AMOUNT = @CANCELLED_TOTAL + TOTAL_AMOUNT,    
       TOTAL_TRAN_PREMIUM = @CANCELLED_PREMIUM + TOTAL_TRAN_PREMIUM,    
       TOTAL_TRAN_INTEREST_AMOUNT = @CANCELLED_INTEREST + TOTAL_TRAN_INTEREST_AMOUNT,    
       TOTAL_TRAN_FEES = @CANELLED_FEES + TOTAL_TRAN_FEES,    
       TOTAL_TRAN_TAXES = @CANCELLED_TAXES + TOTAL_TRAN_TAXES,    
       TOTAL_TRAN_AMOUNT = TOTAL_PREMIUM + @CANCELLED_TOTAL    
       WHERE CUSTOMER_ID = @CUSTOMER_ID  -- where clause added by Pravesh on 16 June 11    
       AND POLICY_ID =@POLICY_ID     
       AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
           
       SELECT  @INSTALLMENT_NO= ISNULL(MAX(INSTALLMENT_NO),0) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)    
       WHERE CUSTOMER_ID = @CUSTOMER_ID aND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
         
      END    
          
   ELSE    
   BEGIN      
       
   SELECT * INTO #tempPOLICY_VERSION    
     FROM ( SELECT TOP 1 * FROM       
    ACT_POLICY_INSTALL_PLAN_DATA WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID           
    AND POLICY_VERSION_ID < @POLICY_VERSION_ID ORDER BY POLICY_VERSION_ID DESC       
   )A    
         
    UPDATE   #tempPOLICY_VERSION SET POLICY_VERSION_ID = @POLICY_VERSION_ID ,APP_VERSION_ID = @POLICY_VERSION_ID    
        
   INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA SELECT *  FROM #tempPOLICY_VERSION    
         
    -- INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA       
    -- SELECT TOP 1 * FROM       
    --ACT_POLICY_INSTALL_PLAN_DATA WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID           
    --AND POLICY_VERSION_ID < @POLICY_VERSION_ID ORDER BY POLICY_VERSION_ID DESC       
          
    SELECT  @CANCELLED_PREMIUM = SUM(INS.INSTALLMENT_AMOUNT)*-1    
      ,@CANCELLED_INTEREST = SUM(INS.INTEREST_AMOUNT)*-1    
      ,@CANELLED_FEES =  SUM(INS.FEE)*-1    
      ,@CANCELLED_TAXES =  SUM(INS.TAXES)*-1    
     ,@CANCELLED_TOTAL = SUM(INS.TOTAL)*-1    
    FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)    
    JOIN     
    POL_CUSTOMER_POLICY_LIST PCPLC WITH(NOLOCK)     
    ON PCPLC.CUSTOMER_ID = INS.CUSTOMER_ID AND     
    PCPLC.POLICY_ID = INS.POLICY_ID AND PCPLC.POLICY_VERSION_ID = INS.POLICY_VERSION_ID    
    WHERE INS.CUSTOMER_ID = @CUSTOMER_ID AND INS.POLICY_ID =@POLICY_ID AND PCPLC.CURRENT_TERM =@CURRENT_TERM    
    AND UPPER(INS.RELEASED_STATUS) = 'C'      
        
       
        
    UPDATE ACT_POLICY_INSTALL_PLAN_DATA     
    SET POLICY_VERSION_ID = @POLICY_VERSION_ID     
       ,APP_VERSION_ID = @POLICY_VERSION_ID    
       ,TOTAL_PREMIUM = TOTAL_PREMIUM + @CANCELLED_PREMIUM    
      ,TOTAL_INTEREST_AMOUNT = TOTAL_INTEREST_AMOUNT + @CANCELLED_INTEREST    
      ,TOTAL_FEES = TOTAL_FEES + @CANELLED_FEES    
      ,TOTAL_TAXES = TOTAL_TAXES +  @CANCELLED_TAXES    
      ,TOTAL_AMOUNT = TOTAL_AMOUNT+ @CANCELLED_TOTAL    
      ,TOTAL_TRAN_PREMIUM = @CANCELLED_PREMIUM    
      ,TOTAL_TRAN_INTEREST_AMOUNT = @CANCELLED_INTEREST    
      ,TOTAL_TRAN_FEES = @CANELLED_FEES    
      ,TOTAL_TRAN_TAXES = @CANCELLED_TAXES    
      ,TOTAL_TRAN_AMOUNT = @CANCELLED_TOTAL    
           
      WHERE CUSTOMER_ID = @CUSTOMER_ID     
      AND POLICY_ID = @POLICY_ID     
      AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
           
     END       
      
   SET @INSTALLMENT_NO = @INSTALLMENT_NO + 1     
     
     
   INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS    
   (      
   POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,    
   INSTALLMENT_AMOUNT,INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,        
   INSTALLMENT_NO,RISK_ID, RISK_TYPE, PAYMENT_MODE,CURRENT_TERM,    
   PERCENTAG_OF_PREMIUM,INTEREST_AMOUNT,FEE,TAXES,TOTAL,TRAN_INTEREST_AMOUNT,        
   TRAN_FEE,TRAN_TAXES,TRAN_TOTAL,BOLETO_NO,IS_BOLETO_GENRATED,CREATED_BY,    
   CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME, TRAN_PREMIUM_AMOUNT,    
   CO_APPLICANT_ID,PAID_AMOUNT,RECEIVED_AMOUNT,RECEIVED_DATE,INSTALLMENT_EXPIRE_DATE )      
        SELECT  @POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID , @POLICY_ID,@POLICY_VERSION_ID,    
      SUM(INS.INSTALLMENT_AMOUNT)*-1,GETDATE(),'U',    
        @INSTALLMENT_NO,NULL,'',0,@CURRENT_TERM,    
        0,SUM(INS.INTEREST_AMOUNT)*-1,SUM(INS.FEE)*-1,SUM(INS.TAXES)*-1,SUM(INS.TOTAL)*-1,SUM(INS.INTEREST_AMOUNT)*-1,    
        SUM(INS.FEE)*-1, SUM(INS.TAXES)*-1,SUM(INS.TOTAL)*-1,NULL,NULL, @MODIFIED_BY,    
        getdate(),null,null,SUM(INS.INSTALLMENT_AMOUNT)*-1,    
        @CO_APPLICANT,  0,NULL,null,null    
        FROM ACT_POLICY_INSTALLMENT_DETAILS INS WITH(NOLOCK)    
        JOIN     
        POL_CUSTOMER_POLICY_LIST PCPLC WITH(NOLOCK)     
        ON PCPLC.CUSTOMER_ID = INS.CUSTOMER_ID AND     
        PCPLC.POLICY_ID = INS.POLICY_ID AND PCPLC.POLICY_VERSION_ID = INS.POLICY_VERSION_ID    
        WHERE INS.CUSTOMER_ID = @CUSTOMER_ID AND INS.POLICY_ID =@POLICY_ID AND PCPLC.CURRENT_TERM =@CURRENT_TERM    
        AND UPPER(INS.RELEASED_STATUS) = 'C'    
        
       
       
      */  
       
    
  END    
    
--SELECT * FROM  ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID =  @POLICY_ID   --and POLICY_VERSION_ID = @POLICY_VERSION_ID     
--SELECT * FROM  ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID =  @POLICY_ID   and POLICY_VERSION_ID = @POLICY_VERSION_ID     
END    
--GO  
--DECLARE @RET_VAL INT    
--EXEC Proc_CancelReissuedVersionBoletos 27987,205,3,2,398,'05/26/2011',@RET_VAL OUT    
--SELECT @RET_VAL  
--ROLLBACK tran    
    
--DELETE  from ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID = 28070 and POLICY_ID =  532  and POLICY_VERSION_ID = 4    
--DELETE from ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID = 28070 and POLICY_ID =  532   and POLICY_VERSION_ID = 4    
---- DELETE from POL_INSTALLMENT_BOLETO where CUSTOMER_ID = 28070 and POLICY_ID =  532   and POLICY_VERSION_ID = 3    