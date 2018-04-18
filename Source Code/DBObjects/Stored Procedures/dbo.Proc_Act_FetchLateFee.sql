IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Act_FetchLateFee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Act_FetchLateFee]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--drop proc dbo.Proc_Act_FetchLateFee   
--go  
CREATE PROCEDURE [dbo].[Proc_Act_FetchLateFee]        
(        
 @POLICY_NUMBER VARCHAR(20) = NULL        
)        
        
AS        
BEGIN        
        
DECLARE @POL_VER_ID INT              
DECLARE @POL_ID INT              
DECLARE @CUST_ID INT          
DECLARE @BILL_TYPE Char(2)         
--        
--SELECT TOP 1 @BILL_TYPE  = BILL_TYPE , @POL_ID = POLICY_ID, @POL_VER_ID = POLICY_VERSION_ID, @CUST_ID = CUSTOMER_ID        
--FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)        
--WHERE POLICY_NUMBER = @POLICY_NUMBER        
--ORDER BY POLICY_VERSION_ID DESC  

--Added For Itrack Issue #6244.
IF(@POL_ID IS NULL OR @POL_ID = '') 	 
     BEGIN 
		SELECT DISTINCT @POL_ID = CPL.POLICY_ID    
		FROM POL_CUSTOMER_POLICY_LIST CPL 
		WHERE POLICY_NUMBER = @POLICY_NUMBER 
     END  
     
IF(@CUST_ID IS NULL OR @CUST_ID = '') 	 
     BEGIN 
		SELECT DISTINCT @CUST_ID = CPL.CUSTOMER_ID    
		FROM POL_CUSTOMER_POLICY_LIST CPL 
		WHERE POLICY_NUMBER = @POLICY_NUMBER 
     END



--Code Commented and Added For Itrack Issue #5915.
SELECT TOP 1 @BILL_TYPE  = POL.BILL_TYPE , 
--@POL_ID = POL.POLICY_ID, 
@POL_VER_ID = POL.POLICY_VERSION_ID 
--@CUST_ID = POL.CUSTOMER_ID      
FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)      
LEFT JOIN POL_POLICY_PROCESS PPP (NOLOCK) ON
PPP.CUSTOMER_ID = POL.CUSTOMER_ID AND PPP.POLICY_ID = POL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID =
POL.POLICY_VERSION_ID 
WHERE 
--POL.POLICY_NUMBER = @POLICY_NUMBER 
--Commented and Added For Itrack Issue #6244
--URENEW Added For Itrack Issue #6471  
POL.POLICY_ID = @POL_ID AND POL.CUSTOMER_ID = @CUST_ID
AND  
		(	
			(ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE' 	AND ISNULL(PPP.REVERT_BACK,'N') = 'N' )
			OR 
			POL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE','URENEW')
		)
ORDER BY POL.POLICY_VERSION_ID DESC 

      
        
IF(ISNULL(@POL_ID,0) = 0 )     
BEGIN     
 SELECT '0' AS LATE_FEES   ,'0' AS STATUS ,       
     '0' as CUSTOMER_ID ,'0' as POLICY_ID, '0' as POLICY_VERSION_ID        
END    
ELSE IF(@BILL_TYPE = 'AB' )         
BEGIN               
 SELECT '-1' AS LATE_FEES   ,'-1' AS STATUS ,    
      '-1' as CUSTOMER_ID ,'-1' as POLICY_ID, '-1' as POLICY_VERSION_ID             
END        
ELSE        
BEGIN         
 declare @fee decimal(18,2)        
 declare @out int        
 exec Proc_ValidatePolicyNum @POLICY_NUMBER ,@out out        
    
 SELECT @fee =  A.LATE_FEES            
 FROM ACT_INSTALL_PLAN_DETAIL A              
 INNER JOIN POL_CUSTOMER_POLICY_LIST B ON B.INSTALL_PLAN_ID = A.IDEN_PLAN_ID              
 WHERE  B.POLICY_NUMBER = @POLICY_NUMBER AND            
 B.CUSTOMER_ID = @CUST_ID AND B.POLICY_ID = @POL_ID AND B.POLICY_VERSION_ID = @POL_VER_ID         
    
    
 select @fee as LATE_FEES ,@out as STATUS , @CUST_ID as CUSTOMER_ID ,@POL_ID as POLICY_ID,@POL_VER_ID as POLICY_VERSION_ID     
    
    --select @CUST_ID as CUSTOMER_ID ,@POL_ID as POLICY_ID,@POL_VER_ID as POLICY_VERSION_ID          
        
END        
END  

--go   
--rollback tran     
--exec Proc_Act_FetchLateFee 'a7001420'     








GO

