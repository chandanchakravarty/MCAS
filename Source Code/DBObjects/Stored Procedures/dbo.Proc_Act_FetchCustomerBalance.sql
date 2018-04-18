IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Act_FetchCustomerBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Act_FetchCustomerBalance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran    
--drop proc dbo.Proc_Act_FetchCustomerBalance     
--go   
create  PROCEDURE [dbo].[Proc_Act_FetchCustomerBalance]        
(        
 @CUSTOMER_ID int = null,      
 @POLICY_ID INT = null,      
 @POLICY_VERSION_ID INT = null,      
 @POLICY_NUMBER VARCHAR(21)=null,      
 @CALLED_FROM CHAR(1) =null      
 )        
AS        
BEGIN        
DECLARE @POL_VER_ID INT      
DECLARE @POL_ID INT      
DECLARE @CUST_ID INT      
DECLARE @AGEN_ID INT      
DECLARE @POL_TYPE CHAR(2)     
DECLARE @POLICY_STATUS VARCHAR(20)   
      
IF(@CALLED_FROM = 'L') -- Policy Number has been entered through Lookup Window      
BEGIN      
  
    EXEC Proc_GetMinimumDueForLastNotice @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID      
       
END      
ELSE -- Policy Number has been entered through Textbox : Fetch Policy Id, Cus ID & Agency ID explicitly      
BEGIN    
  
        
    
 -- Get Pol ID , Cust Id, Agency Id, Pol Ver id      
  
 --POL_POLICY_PROCESS Condition added For itrack Isssue #5951     
  
 -- SELECT TOP 1 @POL_ID = POLICY_ID, @CUST_ID = CUSTOMER_ID,@AGEN_ID = AGENCY_ID,@POL_VER_ID = POLICY_VERSION_ID,  
-- @POL_TYPE = BILL_TYPE , @POLICY_STATUS = POLICY_STATUS --,dbo.fun_GetPolicyDisplayStatus(POL_CUSTOMER_POLICY_LIST.customer_id,POL_CUSTOMER_POLICY_LIST.policy_id,POL_CUSTOMER_POLICY_LIST.policy_version_id) policy_status       
--  FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)      
-- WHERE POLICY_NUMBER =  @POLICY_NUMBER      
-- ORDER BY POLICY_VERSION_ID DESC   
  
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
  
        
    SELECT TOP 1   
 --@POL_ID = POL.POLICY_ID, @CUST_ID = POL.CUSTOMER_ID,  
 @AGEN_ID = POL.AGENCY_ID,  
    @POL_VER_ID = POL.POLICY_VERSION_ID,  
 @POL_TYPE = BILL_TYPE , @POLICY_STATUS = POL.POLICY_STATUS --,dbo.fun_GetPolicyDisplayStatus(POL_CUSTOMER_POLICY_LIST.customer_id,POL_CUSTOMER_POLICY_LIST.policy_id,POL_CUSTOMER_POLICY_LIST.policy_version_id) policy_status       
 FROM POL_CUSTOMER_POLICY_LIST POL (NOLOCK)            
    LEFT JOIN POL_POLICY_PROCESS PPP (NOLOCK) ON  
    PPP.CUSTOMER_ID = POL.CUSTOMER_ID AND PPP.POLICY_ID = POL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID =  
    POL.POLICY_VERSION_ID     
 WHERE POL.POLICY_ID = @POL_ID AND POL.CUSTOMER_ID = @CUST_ID   
    --POL.POLICY_NUMBER =  @POLICY_NUMBER   
    --URENEW Added For Itrack Issue #6471   
     AND    
    (      
          (ISNULL(PPP.PROCESS_STATUS,'') = 'COMPLETE'  AND ISNULL(PPP.REVERT_BACK,'N') = 'N' )  
   OR   
   POL.POLICY_STATUS IN ( 'SUSPENDED','UISSUE','URENEW')      
    )      
   
    ORDER BY POL.POLICY_VERSION_ID DESC   
  
  
  
--  (SELECT TOP 1 POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST  
-- WHERE CUSTOMER_ID = A.CUSTOMER_ID AND POLICY_ID = A.POLICY_ID ORDER BY POLICY_VERSION_ID DESC)  
   
 if(@POL_TYPE = 'AB')  
 BEGIN  
  SELECT ISNULL(@POL_TYPE,'') as POL_TYPE   
 END    
    
 IF(@POL_TYPE <> 'AB') -- Only DB Policies are allowed       
 BEGIN      
    --EXEC Proc_GetTotalAndMinimumDue @CUST_ID,@POL_ID,@POL_VER_ID      
  EXEC  Proc_GetMinimumDueForLastNotice @CUST_ID,@POL_ID,@POL_VER_ID      
     
    -- Fetch Customer Name      
    SELECT ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME  ,     
    CUSTOMER_ADDRESS1,CUSTOMER_ADDRESS2,CUSTOMER_CITY,CUSTOMER_STATE,CUSTOMER_ZIP      
    FROM CLT_CUSTOMER_LIST (NOLOCK) WHERE CUSTOMER_ID = @CUST_ID       
          
    -- Get Allows EFT value for this Agency      
    --SELECT ISNULL(ALLOWS_CUSTOMER_SWEEP,'10964') AS ALLOWS_EFT  FROM MNT_AGENCY_LIST (NOLOCK) WHERE AGENCY_ID = @AGEN_ID     
    --If ACCOUNT_ISVERIFIED2 is NO then Do not Allow EFT (30 June 2008)  
 SELECT     CASE   
 WHEN ISNULL(ACCOUNT_ISVERIFIED2,'10964') = 10963   
 AND  ISNULL(ALLOWS_CUSTOMER_SWEEP,'10964')  = 10963  
 THEN 10963   
 ELSE 10964 END  
 AS ALLOWS_EFT    
 FROM MNT_AGENCY_LIST (NOLOCK) WHERE AGENCY_ID = @AGEN_ID    
  
       
 DECLARE @OUT INT        
 EXEC PROC_VALIDATEPOLICYNUM @POLICY_NUMBER ,@OUT OUT     
     
           
    -- Select NSF for that policy number      
    SELECT A.NON_SUFFICIENT_FUND_FEES,ISNULL(A.INSTALLMENT_FEES,0) INSTALLMENT_FEES    
 FROM ACT_INSTALL_PLAN_DETAIL A      
 INNER JOIN POL_CUSTOMER_POLICY_LIST B ON B.INSTALL_PLAN_ID = A.IDEN_PLAN_ID      
    WHERE IsNull(B.BILL_TYPE,'') = 'DB' AND --B.POLICY_NUMBER = @POLICY_NUMBER      
 B.CUSTOMER_ID = @CUST_ID AND B.POLICY_ID = @POL_ID AND B.POLICY_VERSION_ID = @POL_VER_ID      
       
    SELECT @POL_ID AS POLICY_ID,@CUST_ID AS CUSTOMER_ID,@AGEN_ID AS AGENCY_ID,@POL_VER_ID AS POLICY_VERSION_ID, @POL_TYPE AS BILL_TYPE ,@POLICY_STATUS AS POLICY_STATUS , @out AS STATUS      
       
   END      
 END      
END         
   
  
--go     
--exec Proc_Act_FetchCustomerBalance null,null,null,'A7002242' ,null     
--rollback tran     
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
GO

