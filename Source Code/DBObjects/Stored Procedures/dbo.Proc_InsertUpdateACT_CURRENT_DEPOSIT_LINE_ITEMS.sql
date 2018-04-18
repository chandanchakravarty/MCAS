IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name        :Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS            
Created by       :Vijay Joshi            
Date             :6/23/2005            
Purpose      :Insert record in Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS            
Revison History :            
Used In   :Wolverine            
Return values            
 1 Policy saved successfully without any error            
 2 if saved sucessfully, and reconciled also            
-2  Invalid policy number            
-1 Some other error occured            
-3  Policy is not a normal            
-4  Deposit has been committed hence can not be updated            
drop proc Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS            
------------------------------------------------------------            
Date     Review By          Comments            
------------------------------------------------------------*/            
--drop proc dbo.Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS            
--GO  
create PROC [dbo].[Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS]            
(            
 @CD_LINE_ITEM_ID   int,-- OUTPUT,            
 @DEPOSIT_ID        int,            
 @LINE_ITEM_INTERNAL_NUMBER  int OUTPUT,            
 @ACCOUNT_ID        int,            
 @RECEIPT_AMOUNT        decimal(18,2),            
 @PAYOR_TYPE        nvarchar(10),            
 @RECEIPT_FROM_ID        int,            
 @RECEIPT_FROM_NAME        nvarchar(255),            
 @RECEIPT_FROM_CODE        nvarchar(14),            
 @POLICY_ID         smallint,            
 @POLICY_VERSION_ID        smallint,            
 @CREATED_BY        int =null ,            
 @CREATED_DATETIME        datetime=null,            
 @MODIFIED_BY         int=null,              
 @LAST_UPDATED_DATETIME       datetime=null,          
 @POLICY_NUMBER   nvarchar(75),            
 @CLAIM_NUMBER    nvarchar(25),            
 @POLICY_MONTH    smallint,            
 @MONTH_YEAR    INT = null,          
 @DEPOSIT_TYPE nvarchar(5) = null ,             
 @CREATED_FROM nvarchar(5) = null,          
 @PAGE_ID nvarchar(50) = null,          
 @RTL_BATCH_NUMBER nvarchar(4) =NULL,    
 @RTL_GROUP_NUMBER nvarchar(4) =NULL,      
 @DEPOSIT_DETAIL_ID   int  OUTPUT -- Will be used in EFT        
     
)            
AS            
BEGIN            
           
DECLARE @DEPOSIT_TYPE_CUSTOMER Varchar(5),          
 @DEPOSIT_TYPE_AGENCY Varchar(5),          
 @DEPOSIT_TYPE_CLAIM Varchar(5),          
 @DEPOSIT_TYPE_REINSURANCE Varchar(5),          
 @DEPOSIT_TYPE_MISC Varchar(5),        
 @REC_AMT decimal(18,2) ,  
 @POL_ID int ,  
 @CUST_ID INT      
      
SET @DEPOSIT_TYPE_CUSTOMER = 'CUST'          
SET @DEPOSIT_TYPE_AGENCY = 'AGN'          
SET @DEPOSIT_TYPE_CLAIM = 'CLAM'          
SET @DEPOSIT_TYPE_REINSURANCE = 'RINS'          
SET @DEPOSIT_TYPE_MISC ='MISC'          
          
DECLARE @OLD_AMOUNT Decimal(18,2)      
          
DECLARE @ReturnCode int            
          
IF NOT EXISTS(SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID)            
BEGIN            
 DECLARE @CUSTOMER_ID INT            
 --CHECKING IF THE THE DEPOSIT HAS BEEN COMMITTED OR NOT            
 --IF COMMITED, THEN UPDATION SHOULD BE ABORTED            
 IF (SELECT ISNULL(IS_COMMITED,'') FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'            
  RETURN -4            
            
             
 /*Retreiving the type of deposit as validationof input is defferent depending on deposit type*/            
 --Deposit type is being commented as it will come from sp params          
 Declare @POLICY_ACCOUNT_STATUS varchar(20)            
           
 /*Checking for the validatity of policy number and other validation*/             
 if @DEPOSIT_TYPE = 'CUST'            
 BEGIN            
  --Validate BIll Type in Case of RTL import      
  --If Policy is AB Bill type DO NOT IMPORT : Praveen kasana      
  IF EXISTS(SELECT    BILL_TYPE       
   FROM POL_CUSTOMER_POLICY_LIST   POL      
  WHERE      
  POLICY_NUMBER = @POLICY_NUMBER  AND BILL_TYPE = 'AB'  and      
  POL.POLICY_VERSION_ID IN      
  (SELECT TOP 1 max(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)      
    WHERE CUSTOMER_ID = POL.CUSTOMER_ID AND POLICY_ID = POL.POLICY_ID       
    AND POLICY_NUMBER = POL.POLICY_NUMBER)  )      
  BEGIN       
   RETURN -10         
  END      
          
  --Validating the inputs of customer receipts (deposit line item)            
        
  --Checking whether policy id is valid or not            
  -- Changed By Ravindra (08-03-2007) No need to have a join with APP_LIST           
  -- as Converted Policies are not going to have App_List records ,           
  -- again there is no Bill Type 'DM' in system a policy can be ' ' or 'DB'          
    
--Commented and changed by Raghav/Shikha against #6080 with reference to #5951.  
/*  SELECT             
  @POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),            
  @CUSTOMER_ID = CUSTOMER_ID            
  FROM POL_CUSTOMER_POLICY_LIST           
  WHERE POLICY_NUMBER = @POLICY_NUMBER AND BILL_TYPE = 'DB' AND             
  GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CUSTOMER_ID  */  
--Added For Itrack Issue #6244  
  SELECT DISTINCT @POL_ID = CPL.POLICY_ID , @CUST_ID = CPL.CUSTOMER_ID    
  FROM POL_CUSTOMER_POLICY_LIST CPL   
  WHERE POLICY_NUMBER = @POLICY_NUMBER   
  
  
  SELECT             
  @POLICY_ID = ISNULL(PCPL.POLICY_ID,0), @POLICY_VERSION_ID = MAX(PCPL.POLICY_VERSION_ID),            
  @CUSTOMER_ID = PCPL.CUSTOMER_ID            
  FROM POL_CUSTOMER_POLICY_LIST PCPL   
  LEFT JOIN POL_POLICY_PROCESS PPP   
  ON PPP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPP.POLICY_ID = PCPL.POLICY_ID  AND PPP.NEW_POLICY_VERSION_ID =  
  PCPL.POLICY_VERSION_ID                 
  WHERE --POLICY_NUMBER = @POLICY_NUMBER   
  PCPL.POLICY_ID = @POL_ID AND PCPL.CUSTOMER_ID = @CUST_ID     
  --URENEW Added For Itrack Issue #6471   
  AND BILL_TYPE = 'DB'              
   AND    
    (       
     (ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE' AND ISNULL(PPP.REVERT_BACK,'N') = 'N')     
     OR   
     PCPL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE','URENEW')  
    )   
  GROUP BY IsNull(PCPL.POLICY_ID,0), IsNull(PCPL.POLICY_ACCOUNT_STATUS,0), PCPL.CUSTOMER_ID   
  
          
        
          
  if IsNull(@POLICY_ID,0) = 0             
  BEGIN            
  --Policy number is not valid, hence exiting with return status            
   return -2               
  END                  
  --Updating is confirm flag as not confirm            
  IF (@CREATED_FROM = 'EFT' OR @CREATED_FROM = 'CC' OR @CREATED_FROM = 'RTL')          
  BEGIN -- For EFT and Credit card it will be confirmed by default           
   UPDATE ACT_CURRENT_DEPOSITS  SET IS_CONFIRM = 'Y'            
   WHERE DEPOSIT_ID = @DEPOSIT_ID              
  END          
  ELSE          
  BEGIN           
   UPDATE ACT_CURRENT_DEPOSITS  SET IS_CONFIRM = 'N'            
   WHERE DEPOSIT_ID = @DEPOSIT_ID              
  END          
          
  SET @RECEIPT_FROM_ID = @CUSTOMER_ID          
 END            
 ELSE            
 BEGIN            
  --Deposit will be treated as confirm by default for other type of deposit            
  UPDATE ACT_CURRENT_DEPOSITS            
  SET IS_CONFIRM = 'Y'            
  WHERE DEPOSIT_ID = @DEPOSIT_ID              
 END            
            
 SELECT @CD_LINE_ITEM_ID = IsNull(Max(CD_LINE_ITEM_ID),0) +1,             
 @LINE_ITEM_INTERNAL_NUMBER = IsNull(Max(LINE_ITEM_INTERNAL_NUMBER),0) + 1            
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS with(nolock)           
        
 INSERT INTO ACT_CURRENT_DEPOSIT_LINE_ITEMS            
 (            
 CD_LINE_ITEM_ID, DEPOSIT_ID, LINE_ITEM_INTERNAL_NUMBER, ACCOUNT_ID,            
 DEPOSIT_TYPE, RECEIPT_AMOUNT, PAYOR_TYPE, RECEIPT_FROM_ID,            
 RECEIPT_FROM_NAME, RECEIPT_FROM_CODE,            
 POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID,            
 IS_ACTIVE, CREATED_BY, CREATED_DATETIME, POLICY_NUMBER, CLAIM_NUMBER,           
 POLICY_MONTH, MONTH_YEAR , CREATED_FROM, PAGE_ID ,RTL_BATCH_NUMBER ,    
 RTL_GROUP_NUMBER          
 )            
 VALUES            
 (            
 @CD_LINE_ITEM_ID, @DEPOSIT_ID, @LINE_ITEM_INTERNAL_NUMBER, @ACCOUNT_ID,            
 @DEPOSIT_TYPE, @RECEIPT_AMOUNT, @PAYOR_TYPE, @RECEIPT_FROM_ID,             
 @RECEIPT_FROM_NAME, @RECEIPT_FROM_CODE,            
 @POLICY_ID, @POLICY_VERSION_ID, @CUSTOMER_ID,            
 'Y', @CREATED_BY, @CREATED_DATETIME, @POLICY_NUMBER, @CLAIM_NUMBER,           
 @POLICY_MONTH, @MONTH_YEAR  , @CREATED_FROM, @PAGE_ID ,@RTL_BATCH_NUMBER,    
 @RTL_GROUP_NUMBER       
 )            
            
 SET @DEPOSIT_DETAIL_ID   = @CD_LINE_ITEM_ID           
          
 if @@Error <> 0            
 BEGIN             
 --Some error occured            
  RETURN -1            
 END            
       
 --Updating the total amount            
 UPDATE ACT_CURRENT_DEPOSITS            
 SET TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT)             
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS             
 WHERE DEPOSIT_ID = @DEPOSIT_ID)            
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
          
 if @DEPOSIT_TYPE = @DEPOSIT_TYPE_AGENCY            
 BEGIN            
      
  EXECUTE @ReturnCode = Proc_AutoApplyOpenItems @CD_LINE_ITEM_ID, NULL, NULL, NULL            
  if @ReturnCode = 1            
  BEGIN          
   UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS SET IS_RECONCILED = 'Y'          
   WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID          
   return 1 --Reconciled sucessfully            
  END           
  else             
  BEGIN          
   UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS SET IS_RECONCILED = 'N'          
   WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID          
   return 2 --Unable to reconciled            
  END          
 END            
       
END            
--//UPDATING THE LINE ITEMS IN AGENCY            
ELSE            
BEGIN            
           
 --Checking if the deposit has been commited or not, if commited then can not be update            
 if (SELECT IsNull(IS_COMMITED,'') FROM ACT_CURRENT_DEPOSITS with(nolock) WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'            
  return -4 --can not proceed further as deposit has been commited            
            
 /*Checking for the validatity of policy number and other validation*/             
 if @DEPOSIT_TYPE = 'CUST'            
 BEGIN            
       
  --Validating the inputs of customer receipts (deposit line item)            
  --Checking whether policy id is valid or not            
  /*SELECT @POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = POLICY_VERSION_ID , @CUSTOMER_ID = CUSTOMER_ID           
  FROM POL_CUSTOMER_POLICY_LIST             
  WHERE POLICY_NUMBER = @POLICY_NUMBER      */      
  SELECT             
  @POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),            
  @CUSTOMER_ID = CUSTOMER_ID            
  FROM POL_CUSTOMER_POLICY_LIST           
  WHERE POLICY_NUMBER = @POLICY_NUMBER AND BILL_TYPE = 'DB'            
  GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CUSTOMER_ID         
        
  if IsNull(@POLICY_ID,0) = 0             
  BEGIN            
  --Policy number is not valid, hence exiting with return status            
   return -2               
  END            
   SET @RECEIPT_FROM_ID = @CUSTOMER_ID           
 END            
            
 SET @DEPOSIT_DETAIL_ID   = @CD_LINE_ITEM_ID          
      
 SELECT @OLD_AMOUNT = RECEIPT_AMOUNT   FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS with(nolock) WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID      
       
 UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS            
 SET CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID,             
 DEPOSIT_ID = @DEPOSIT_ID,             
 LINE_ITEM_INTERNAL_NUMBER = @LINE_ITEM_INTERNAL_NUMBER,             
 ACCOUNT_ID = @ACCOUNT_ID,            
 DEPOSIT_TYPE = @DEPOSIT_TYPE,             
 RECEIPT_AMOUNT = @RECEIPT_AMOUNT,             
 PAYOR_TYPE = @PAYOR_TYPE,             
 RECEIPT_FROM_ID = @RECEIPT_FROM_ID,             
 RECEIPT_FROM_CODE = @RECEIPT_FROM_CODE,            
 RECEIPT_FROM_NAME = @RECEIPT_FROM_NAME,        
 CUSTOMER_ID    = @CUSTOMER_ID ,            
 POLICY_ID = @POLICY_ID,             
 POLICY_VERSION_ID = @POLICY_VERSION_ID,             
 MODIFIED_BY = @MODIFIED_BY,             
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,            
 POLICY_NUMBER = @POLICY_NUMBER,            
 CLAIM_NUMBER = @CLAIM_NUMBER,            
 POLICY_MONTH = @POLICY_MONTH,            
 MONTH_YEAR = @MONTH_YEAR ,          
 CREATED_FROM  = CASE WHEN CREATED_FROM = 'RTL' THEN 'RTL' ELSE @CREATED_FROM END       
 WHERE            
 CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID            
 --Updating the total amount            
 UPDATE ACT_CURRENT_DEPOSITS            
 SET             
 TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT)             
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS             
 WHERE DEPOSIT_ID = @DEPOSIT_ID),            
 IS_CONFIRM = 'N'            
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
      
 IF @DEPOSIT_TYPE = @DEPOSIT_TYPE_AGENCY               
 BEGIN            
                 
       
  --DELETING THE OLD RECONCILIATION            
  DECLARE @GROUP_ID INT            
  SELECT @GROUP_ID = GROUP_ID FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID            
          
  --##################        
  SELECT @REC_AMT = SUM(RECON_AMOUNT) FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID        
        
  IF(@RECEIPT_AMOUNT <> @OLD_AMOUNT )         
  BEGIN        
   --UPDATING THE RECONCILED AMOUNT IN OPEN ITEM TABLE                
   DELETE FROM ACT_RECONCILIATION_GROUPS WHERE GROUP_ID = @GROUP_ID            
   DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID            
         
   DELETE ACT_AGENCY_STATEMENT            
   FROM ACT_AGENCY_OPEN_ITEMS               
   WHERE             
   ACT_AGENCY_STATEMENT.AGENCY_OPEN_ITEM_ID = ACT_AGENCY_OPEN_ITEMS.IDEN_ROW_ID            
   AND ACT_AGENCY_OPEN_ITEMS.SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND ACT_AGENCY_OPEN_ITEMS.UPDATED_FROM = 'D'            
         
   DELETE FROM ACT_AGENCY_OPEN_ITEMS WHERE SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND UPDATED_FROM = 'D'          
         
        
   EXECUTE @RETURNCODE = PROC_AUTOAPPLYOPENITEMS @CD_LINE_ITEM_ID, NULL, NULL, NULL       
         
   IF @RETURNCODE = 1            
   BEGIN           
    UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS SET IS_RECONCILED = 'Y'          
    WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID          
    RETURN 1--RECONCILED SUCESSFULLY            
   END           
   ELSE          
   BEGIN            
    UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS SET IS_RECONCILED = 'N'          
    WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID        
    RETURN 2 --UNABLE TO RECONCILED              
   END           
  END        
        
 END            
END            
      
--Ravindra  need to be handeled for individual screen          
 -- Updating TAPE_TOTAL            
IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_CUSTOMER)          
BEGIN           
 UPDATE ACT_CURRENT_DEPOSITS            
 SET TAPE_TOTAL_CUST = (          
  SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS             
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE      
 )            
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
END            
ELSE IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_CLAIM OR @DEPOSIT_TYPE = @DEPOSIT_TYPE_REINSURANCE)          
BEGIN           
 UPDATE ACT_CURRENT_DEPOSITS            
 SET TAPE_TOTAL_CLM = (          
  SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS             
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE      
 )            
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
END            
ELSE IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_MISC )          
 BEGIN           
 UPDATE ACT_CURRENT_DEPOSITS            
 SET TAPE_TOTAL_MISC = (          
  SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS             
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE      
 )            
 WHERE DEPOSIT_ID = @DEPOSIT_ID            
END            
          
--RTL PAGE ID      
/*IF(@CREATED_FROM = 'RTL')          
BEGIN      
 UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS       
 SET PAGE_ID = (SELECT  ISNULL(MAX(ISNULL(CD_LINE_ITEM_ID,0)),0)       
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK))      
         
END*/      
          
RETURN 1             
END            
      
  
  
    
    
  
  
    
  
  
  
  
  
  
GO

