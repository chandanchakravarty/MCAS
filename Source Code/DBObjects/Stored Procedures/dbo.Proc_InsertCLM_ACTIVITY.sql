IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY                        
Created by      : Vijay Arora                        
Date            : 5/24/2006                        
Purpose     : To Insert a record in table named CLM_ACTIVITY                        
Revison History :                        
Used In  : Wolverine                        
------------------------------------------------------------                        
Modified by  : Asfa      
Date  : 05-Sept-2007      
Purpose  : Not to Allow duplicate New Reserve Activity to be inserted       
----------------------------------------------------------------------                                                                                    
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
-- DROP PROC dbo.Proc_InsertCLM_ACTIVITY                        
CREATE PROC [dbo].[Proc_InsertCLM_ACTIVITY]                        
(                        
 @CLAIM_ID     int,                        
 @ACTIVITY_ID     int OUTPUT,                        
 @ACTIVITY_REASON     int,                        
 @REASON_DESCRIPTION  varchar(500) = '',                        
 @CREATED_BY     int,                
 @ACTIVITY_STATUS int = null, --optional parameter in case user wishes to specify Activity Status                
 @RESERVE_TRAN_CODE int,          
 @ACTION_ON_PAYMENT int = null,    
 @ACCOUNTING_SUPPRESSED  BIT = 0  ,
 @IS_VOID_ACTIVITY CHAR(1)='N' ,       
 @COI_TRAN_TYPE  int,
 @TEXT_ID int= NULL,
 @TEXT_DESCRIPTION varchar(2000)  = NULL,
 @INITIAL_LOAD_FLAG CHAR(1) ='N' -- ADDED BY SANTOSH GAUTAM ON 01 OCT 2011

)                        
AS                        
BEGIN     
  
  
DECLARE @LITIGATION_FILE INT =0
DECLARE @OUSTANDING_AMOUNT DECIMAL(18,2) 


---Added by Santosh Kumar Gautam on 08 Dec 2010  
-- IF CLAIM LITIGATION_FILE IS YES AND LITIGATION INFORMATION IS NOT PROVIDED THEN   
---USER CANNOT CREATE PAYMENT TYPE ACTIVTIY    
IF(@ACTIVITY_REASON =11775 AND @INITIAL_LOAD_FLAG='N')  
BEGIN  

 SELECT @LITIGATION_FILE = LITIGATION_FILE FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID

 IF(@LITIGATION_FILE=10963 AND NOT EXISTS(SELECT CLAIM_ID FROM  CLM_LITIGATION_INFORMATION WHERE CLAIM_ID=@CLAIM_ID))  
     BEGIN   
   
      SET @ACTIVITY_ID =-2 -- FOR ERROR LITIGATION INFORMATION NOT PROVIDED  
      RETURN  
   
 END  
 
	 SELECT TOP 1 @OUSTANDING_AMOUNT=CLAIM_RESERVE_AMOUNT 
	 FROM CLM_ACTIVITY  
	 WHERE CLAIM_ID= @CLAIM_ID 
	 ORDER BY ACTIVITY_ID DESC
	 
	 -- RESERVE AMOUNT IS ZERO THEN NOT ALLOWED TO ADD NEW PAYMENT ACTIVITY
	 IF(@OUSTANDING_AMOUNT IS NOT NULL AND (@OUSTANDING_AMOUNT=0.00 OR @OUSTANDING_AMOUNT=0))
	 BEGIN
	 
	  SET @ACTIVITY_ID =-3 -- FOR ERROR LITIGATION INFORMATION NOT PROVIDED  
      RETURN  
      
	 END
 
END  
                     
                    
declare @CLAIM_RESERVE_AMOUNT decimal(20,2)               
declare @CLAIM_RI_RESERVE decimal(20,2)          
declare @CLAIM_PAYMENT_AMOUNT decimal(20,2)          
--declare @ACTIVITY_STATUS INT                  
--If the user does not specify any Activity Status then  the status of activity will be incomplete (11800)                  
if @ACTIVITY_STATUS is null or @ACTIVITY_STATUS=0                
 SET @ACTIVITY_STATUS = 11800 --lookup_unique_id for Incomplete                  
                    
select @ACTIVITY_ID=isnull(Max(ACTIVITY_ID),0)+1 from CLM_ACTIVITY WHERE CLAIM_ID = @CLAIM_ID                        
                    
--find last recorded reserve amount against the current claim..                    
--We will update any new activity for that claim with the last recorded reserve amount calculate below..                    
--SELECT TOP 1 @RESERVE_AMOUNT=ISNULL(RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' ORDER BY CREATED_DATETIME DESC                    
SELECT TOP 1 @CLAIM_RESERVE_AMOUNT=ISNULL(CLAIM_RESERVE_AMOUNT,0),    
@CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0),    
@CLAIM_PAYMENT_AMOUNT=ISNULL(CLAIM_PAYMENT_AMOUNT,0)     
FROM CLM_ACTIVITY     
WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC        
  
    
          
/* Added by Asfa(05-Sept-2007)        
In Reference : iTrack issue #2371        
Purpose : Not to Allow duplicate New Reserve Activity to be inserted         
ACTIVITY_REASON=11773 AND ACTION_ON_PAYMENT =165 i.e. "New Reserve"     
-----------------------    
SELECT DETAIL_TYPE_ID, TRANSACTION_CODE, DETAIL_TYPE_DESCRIPTION     
FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_DESCRIPTION LIKE '%NEW RESERVE%'        
-----------------------    
*/        
        
IF EXISTS(SELECT ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID     
 AND ACTIVITY_REASON=11773  AND ACTIVITY_REASON=@ACTIVITY_REASON     
 AND ACTION_ON_PAYMENT =165 AND ACTION_ON_PAYMENT=@ACTION_ON_PAYMENT)        
BEGIN        
   DELETE FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=11773 AND ACTION_ON_PAYMENT =165    
end                              
  
                  
INSERT INTO CLM_ACTIVITY                        
(                        
 CLAIM_ID,                        
 ACTIVITY_ID,                        
 ACTIVITY_DATE,                        
 ACTIVITY_REASON,                        
 REASON_DESCRIPTION,                        
 IS_ACTIVE,                        
 CREATED_BY,                        
 CREATED_DATETIME,                
 CLAIM_RESERVE_AMOUNT,                  
 ACTIVITY_STATUS,              
 RESERVE_TRAN_CODE,          
 ACTION_ON_PAYMENT,          
 CLAIM_RI_RESERVE,          
 CLAIM_PAYMENT_AMOUNT,    
 ACCOUNTING_SUPPRESSED ,
 COI_TRAN_TYPE,
 TEXT_ID,
 TEXT_DESCRIPTION          
)                        
VALUES                        
(                        
 @CLAIM_ID,                        
 @ACTIVITY_ID,                        
 GETDATE(),                        
 @ACTIVITY_REASON,                        
 @REASON_DESCRIPTION,                        
 'Y',                        
 @CREATED_BY,                        
 GETDATE(),                    
 @CLAIM_RESERVE_AMOUNT,                  
 @ACTIVITY_STATUS,              
 @RESERVE_TRAN_CODE,          
 @ACTION_ON_PAYMENT,          
 @CLAIM_RI_RESERVE,          
 @CLAIM_PAYMENT_AMOUNT,    
 @ACCOUNTING_SUPPRESSED    ,
 @COI_TRAN_TYPE,
 @TEXT_ID,
 @TEXT_DESCRIPTION                  
)  



-------------------------------------------------------------
-- Added by santosh kumar gautam on 22 dec 2010
-- COPY THE RECORD OF LAST COMPLETED ACTIVITY TO NEW ACTIVITY
-------------------------------------------------------------          


IF(@IS_VOID_ACTIVITY<>'Y' AND @ACTIVITY_REASON IN (11773,11775,11776))
 BEGIN
	  -- TO COPY LAST COMPLETED ACTIVTIY RECORDS    
		EXEC [Proc_CopyReserveDetails] @CLAIM_ID,@ACTIVITY_ID,1    
 END

-------------------------------------------------------------
--- FOR CLOSE RESERVE ACTIVITY
------------------------------------------------------------
 IF(@ACTION_ON_PAYMENT =167) -- CLOSE RESERVE
 BEGIN     
    
  -- CURRENT ACTIVITY IS CLOSE ACTIVITY THEN CREATE OUTSTANDING COLUMN =0
   UPDATE CLM_ACTIVITY_RESERVE 
   SET  OUTSTANDING=0
       ,OUTSTANDING_TRAN = -1*PREV_OUTSTANDING
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID 
   
   
   -- COMPLETE CLOSE RESERVE ACTIVITY
   
   EXEC [Proc_CompleteClaimActivities]   
	 @CLAIM_ID            =@CLAIM_ID                      
	,@ACTIVITY_ID         =@ACTIVITY_ID      
	,@ACTIVITY_REASON     =@ACTIVITY_REASON                
	,@ACTION_ON_PAYMENT   =@ACTION_ON_PAYMENT 
	,@IS_VOIDED_ACTIVITY  ='Y'   
	  
    
 END           
 
END 


