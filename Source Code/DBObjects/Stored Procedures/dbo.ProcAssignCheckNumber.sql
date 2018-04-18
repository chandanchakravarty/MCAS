IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcAssignCheckNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcAssignCheckNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*---------------------------------------------------------------------------        
CREATE BY    : Ravindra Gupta        
CREATE DATETIME : 12-13-2006    
PURPOSE     : To assign Check number to check & Update General Ledger accordingly   
  -- will be called before Printing the check  
Modified By : Praveen
purpose : Update Print_Date while Assigning the Check Number for positive pay process.
  
----------------------------------------------------------------------------*/        
--drop proc dbo.ProcAssignCheckNumber  
CREATE  PROC [dbo].[ProcAssignCheckNumber]   
(        
	@CHECK_ID  INT  
)        
AS        
BEGIN      
DECLARE @START_CHECK_NUMBER Int,  
		@MAXCHECKNO Int,  
		@ACCOUNT_ID Int,   
		@CHECK_NUMBER Int                

  
SELECT @ACCOUNT_ID = ACCOUNT_ID FROM ACT_CHECK_INFORMATION   WHERE CHECK_ID = @CHECK_ID  
  
SELECT  @START_CHECK_NUMBER=START_CHECK_NUMBER, @MAXCHECKNO=END_CHECK_NUMBER    
FROM ACT_BANK_INFORMATION WITH (NOLOCK)   WHERE ACCOUNT_ID=@ACCOUNT_ID                
  
  
SELECT @CHECK_NUMBER = ISNULL(MAX(CAST(CHECK_NUMBER AS INT)),@START_CHECK_NUMBER-1)+1   
FROM   ACT_CHECK_INFORMATION WITH (NOLOCK)   
WHERE ACCOUNT_ID=@ACCOUNT_ID      
AND ISNULL(IS_IN_CURRENT_SEQUENCE,'Y') = 'Y'       
--Ravindra(04-14-2009): Do not consider Manual checks 
AND ISNULL(MANUAL_CHECK,'N') <> 'Y' 
        
IF ( @CHECK_NUMBER > @MAXCHECKNO )               
BEGIN --AUTO RESET                
	UPDATE ACT_CHECK_INFORMATION SET IS_IN_CURRENT_SEQUENCE='N' WHERE ACCOUNT_ID=@ACCOUNT_ID                
	AND CHECK_ID <> @CHECK_ID   AND ISNULL(IS_PRINTED ,'') = 'Y'
	SET @CHECK_NUMBER=@START_CHECK_NUMBER          
END                

IF @CHECK_NUMBER IS NOT NULL
BEGIN  
	UPDATE ACT_CHECK_INFORMATION   	SET CHECK_NUMBER = @CHECK_NUMBER,  
	IS_PRINTED = 'Y',	PRINT_DATE = getdate()    
	WHERE CHECK_ID = @CHECK_ID   
END  

--Update   
UPDATE ACT_ACCOUNTS_POSTING_DETAILS SET SOURCE_NUM = @CHECK_NUMBER  
WHERE UPDATED_FROM = 'C' AND SOURCE_ROW_ID = @CHECK_ID  
  
UPDATE ACT_ACCOUNTS_POSTING_DETAILS   SET SOURCE_NUM = @CHECK_NUMBER  
WHERE SOURCE_ROW_ID = @CHECK_ID   AND   ITEM_TRAN_CODE_TYPE = 'CHK'  
  
UPDATE ACT_CUSTOMER_OPEN_ITEMS     SET SOURCE_NUM = @CHECK_NUMBER  
WHERE SOURCE_ROW_ID = @CHECK_ID   AND  ITEM_TRAN_CODE_TYPE = 'CHK'  AND  UPDATED_FROM = 'C'   
  
  
END  

 /*
select * from ACT_CHECK_INFORMATION with(nolock) where CHECK_ID=2762
select SOURCE_NUM,* from ACT_ACCOUNTS_POSTING_DETAILS where SOURCE_ROW_ID =2762  and UPDATED_FROM = 'C' 
select SOURCE_NUM,* from ACT_ACCOUNTS_POSTING_DETAILS where  SOURCE_ROW_ID = 2762   AND   ITEM_TRAN_CODE_TYPE = 'CHK'
select SOURCE_NUM,* from ACT_CUSTOMER_OPEN_ITEMS where SOURCE_ROW_ID =2762    AND  ITEM_TRAN_CODE_TYPE = 'CHK'  AND  UPDATED_FROM = 'C'  
  
  */            
  






GO

