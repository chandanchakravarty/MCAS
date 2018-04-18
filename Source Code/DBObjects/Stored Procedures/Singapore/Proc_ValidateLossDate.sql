--begin tran      
-- drop proc Proc_ValidateLossDate      
--go      
/*----------------------------------------------------------                                            
Proc Name       : proc Proc_ValidateLossDate      
Created by      : Praveen kasana      
Date            :       
Purpose         : Checks date of loss entered at page level against effective date at policy table.              
     If the claim entered is within the policy effective and expiration date then its ok. If not then              
     it searches for other versions of policy meeting the criteria. If everything fails,it returns -1              
Created by      :       
Revison History :       
Modified by  : Praveen Kasana      
Purpose   : Check for AS400 Version while adding Claim      
                               
Used In         : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------*/                                            
-- DROP PROC dbo.Proc_ValidateLossDate   2156,708,2,'2011-04-13'   
ALTER PROC [dbo].[Proc_ValidateLossDate]      
      
@CUSTOMER_ID int,      
@POLICY_ID int,      
@POLICY_VERSION_ID smallint,      
@LOSS_DATE datetime      
      
AS                           
BEGIN      
declare @NEW_POLICY_VERSION_ID int      
declare @CANCEL_POL_VER_EFFECTIVE_DATE DATETIME         
declare @NEXT_POL_VER_EFFECTIVE_DATE DATETIME         
declare @CURRENT_TERM INT      
declare @IS_CANCEL CHAR(1)          
declare @IS_CURRENT_TERM_CANCEL CHAR(1)          
declare @IS_OTHER_VERSION CHAR(1)          
declare @IS_DUPLICATE_CLAIM_EXIST INT=0       
  
SET @IS_CANCEL='N'      
SET @IS_CURRENT_TERM_CANCEL ='N'      
Declare @ADD_CLAIM int      
SET @ADD_CLAIM = 0      
  
SET @LOSS_DATE=CONVERT (date,@LOSS_DATE,101)  
  
 --ADDED BY SANTOSH KUMAR GAUTAM ON 17 JAN 2011  
 --TO CHECK LOSS DATE SHOULD NOT BE FUTURE DATE    
IF(CONVERT (date,@LOSS_DATE,101)> CONVERT (date, getdate()))  
BEGIN  
 SELECT -8 AS ADDCLAIM    
 return    
END  
      
-----------------------------------New Logic 04 feb 2009-----------------------      
declare @PRIOR_POL_VER_EFFECTIVE_DATE DATETIME      
      
DECLARE @COMMIT_REINSTATE_PROCESS_ID INT      
 SET @COMMIT_REINSTATE_PROCESS_ID = 16      
DECLARE @COMMIT_REWRITE_PROCESS_ID INT      
 SET @COMMIT_REWRITE_PROCESS_ID = 32      
      
DECLARE @VERSION_CURRENT_TERM INT      
DECLARE @PRIOR_VERSION INT      
SELECT @CURRENT_TERM=APP_TERMS FROM POL_CUSTOMER_POLICY_LIST      
   WHERE CUSTOMER_ID =@CUSTOMER_ID       
   AND  POLICY_ID = @POLICY_ID       
   AND APP_EFFECTIVE_DATE <= @LOSS_DATE AND @LOSS_DATE <= POL_VER_EXPIRATION_DATE      
      
SELECT @VERSION_CURRENT_TERM=CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST      
   WHERE CUSTOMER_ID =@CUSTOMER_ID       
   AND  POLICY_ID = @POLICY_ID       
   AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
      
      
      
IF(@CURRENT_TERM IS NULL)      
 BEGIN      
  --PRINT 'INVALID LOSS DATE'      
  SELECT -2 AS ADDCLAIM      
  --Get the Catastrophe Event Data      
  --exec Proc_GetClaimCatastropheCode @LOSS_DATE      
  RETURN      
    END      
      
--AS400 CHECK      
IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE              
 CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
 AND ISNULL(FROM_AS400,'N') = 'Y')      
BEGIN      
 SELECT -5 AS ADDCLAIM   --YOU CAN NOT ADD CLAIM AS POLICY IS FROM CONVERSION      
 SET @ADD_CLAIM = -5      
 return      
END      
ELSE      
BEGIN      
 ------      
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST      
    WHERE CUSTOMER_ID =@CUSTOMER_ID       
    AND  POLICY_ID = @POLICY_ID AND CURRENT_TERM=@CURRENT_TERM      
    AND (POLICY_STATUS IN ('CANCEL','SCANCEL','RESCIND'))      
    )      
    or      
  EXISTS(      
   SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS      
    WHERE CUSTOMER_ID =@CUSTOMER_ID       
    AND  POLICY_ID = @POLICY_ID       
    AND PROCESS_ID=12 AND PROCESS_STATUS='COMPLETE'      
    AND NEW_POLICY_VERSION_ID IN      
      (      
      SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST      
      WHERE CUSTOMER_ID =@CUSTOMER_ID       
      AND  POLICY_ID = @POLICY_ID AND CURRENT_TERM=@CURRENT_TERM      
      )      
    )      
      
  BEGIN      
  SET @IS_CURRENT_TERM_CANCEL='Y'      
    SELECT TOP 1 @CANCEL_POL_VER_EFFECTIVE_DATE=POL_VER_EFFECTIVE_DATE ,@CURRENT_TERM=CURRENT_TERM      
  FROM POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)        
  INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)  ON PPP.CUSTOMER_ID=PCPL.CUSTOMER_ID      
  AND PPP.POLICY_ID=PCPL.POLICY_ID AND PPP.NEW_POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID      
  AND       
  ( PPP.PROCESS_ID=12 AND PPP.PROCESS_STATUS='COMPLETE'      
   OR POLICY_STATUS IN ('CANCEL','SCANCEL','RESCIND')      
  )      
  WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID AND  PCPL.POLICY_ID=@POLICY_ID AND CURRENT_TERM=@CURRENT_TERM      
  ORDER BY POL_VER_EFFECTIVE_DATE DESC       
    
  IF(@LOSS_DATE  = @CANCEL_POL_VER_EFFECTIVE_DATE )      
   BEGIN      
    SELECT 0 AS ADDCLAIM -- ,'Can''t add claim, Policy is cancelled !'      
    SET @IS_CANCEL='Y'      
   END       
  ELSE IF(@LOSS_DATE <  @CANCEL_POL_VER_EFFECTIVE_DATE)      
  BEGIN      
   SELECT 1 AS ADDCLAIM      
   SET @ADD_CLAIM = 1      
  END      
  ELSE IF EXISTS( ----Check Whether any next version (Rewrite or reinstate) IF EXISTS THEN ALLOW TO ADD CLAIM      
    SELECT POL_VER_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST       
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND CURRENT_TERM=@CURRENT_TERM      
    AND POLICY_VERSION_ID>      
     (SELECT MAX(NEW_POLICY_VERSION_ID) FROM  POL_POLICY_PROCESS       
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID      
     AND PROCESS_ID=12 AND PROCESS_STATUS ='COMPLETE'--IN ('CANCEL','SCANCEL','RESCIND')      
     AND NEW_POLICY_VERSION_ID IN      
      (      
      SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST      
      WHERE CUSTOMER_ID =@CUSTOMER_ID       
      AND  POLICY_ID = @POLICY_ID AND CURRENT_TERM=@CURRENT_TERM      
      )      
     )      
    )      
   BEGIN       
    SELECT @PRIOR_POL_VER_EFFECTIVE_DATE = ISNULL(EFFECTIVE_DATETIME,'') ,@PRIOR_VERSION=NEW_POLICY_VERSION_ID      
     FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID=@CUSTOMER_ID       
     AND  POLICY_ID=@POLICY_ID       
     AND PROCESS_ID IN (@COMMIT_REINSTATE_PROCESS_ID,@COMMIT_REWRITE_PROCESS_ID)       
     AND PROCESS_STATUS='COMPLETE'      
     AND NEW_POLICY_VERSION_ID IN      
      (      
      SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST      
      WHERE CUSTOMER_ID =@CUSTOMER_ID       
      AND  POLICY_ID = @POLICY_ID AND CURRENT_TERM=@CURRENT_TERM      
      )      
    --select @PRIOR_POL_VER_EFFECTIVE_DATE POL_EFFECTIVE,@CANCEL_POL_VER_EFFECTIVE_DATE CANCELL_DATE,@LOSS_DATE LOSS_DATE      
    --IF (cast(convert(varchar,@LOSS_DATE,101) as datetime) BETWEEN cast(convert(varchar,@PRIOR_POL_VER_EFFECTIVE_DATE,101) as datetime) AND cast(convert(varchar,@CANCEL_POL_VER_EFFECTIVE_DATE,101) as datetime))      
    IF (@LOSS_DATE BETWEEN @CANCEL_POL_VER_EFFECTIVE_DATE AND @PRIOR_POL_VER_EFFECTIVE_DATE)      
    BEGIN      
     IF(@LOSS_DATE = @PRIOR_POL_VER_EFFECTIVE_DATE)      
     BEGIN      
      SELECT 1 AS ADDCLAIM --allow      
      SET @ADD_CLAIM = 1          
     END      
     ELSE      
     BEGIN      
      --SELECT -1 AS ADDCLAIM --DO NOT ALLOW      
      SELECT 0 AS ADDCLAIM --DO NOT ALLOW      
      SET @ADD_CLAIM = 0       
      SET @IS_CANCEL='Y'           
     END      
    END      
    ELSE      
    BEGIN      
     SELECT 1 AS ADDCLAIM --allow      
     SET @ADD_CLAIM = 1      
    END      
           
           
   END       
   ELSE      
   BEGIN      
   SELECT 0 AS ADDCLAIM  --,'Can''t add claim, Policy is cancelled !'      
   SET @IS_CANCEL='Y'      
   END      
         
 END      
 ELSE      
 BEGIN      
   SELECT 1 AS ADDCLAIM   --you can add claim      
   SET @ADD_CLAIM = 1        
    
-- ========================================================================================  
-- ADDED BY SANTOSH KUMAR GAUTAM ON 27 APR 2011 FOR ITRACK:1097  
-- IF CLAIM WITH SAME LOSS DATE IS ALREADY EXISTS FOR PROVIDED POLICY THEN GIVE AN ALERT   
-- ========================================================================================  
 SELECT TOP 1 @IS_DUPLICATE_CLAIM_EXIST=CLAIM_ID FROM CLM_CLAIM_INFO   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  
    POLICY_ID=@POLICY_ID AND   
    POLICY_VERSION_ID=@POLICY_VERSION_ID AND  
    CONVERT (date,LOSS_DATE,101)=@LOSS_DATE AND  
    IS_ACTIVE='Y'  
      
     
   --IF(@IS_DUPLICATE_CLAIM_EXIST IS NOT NULL AND @IS_DUPLICATE_CLAIM_EXIST!=0)  
     SELECT @IS_DUPLICATE_CLAIM_EXIST AS IS_DUPLICATE_CLAIM_EXIST  
        
  
 END      
END      
      
      
      
--------------------------------------End New Logic 2009----------------------------      
      
--Get the Catastrophe Event Data                
--exec Proc_GetClaimCatastropheCode @LOSS_DATE      
      
      
          
--Check whether the policy exists for the current input, if it does not return from the proc      
--new claim without any data is being added      
if not exists(select customer_id from pol_customer_policy_list where              
    CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)      
begin      
 select @NEW_POLICY_VERSION_ID AS RESULT      
 return 0        
end      
      
--Check loss of date against the current policy being worked upon            
if exists(select pcpl.customer_id from pol_customer_policy_list pcpl      
   left join pol_policy_process ppp on ppp.customer_id=pcpl.customer_id       
    and ppp.policy_id=pcpl.policy_id       
    and ppp.new_policy_version_id=pcpl.policy_version_id      
   where pcpl.CUSTOMER_ID=@CUSTOMER_ID and  pcpl.POLICY_ID=@POLICY_ID and pcpl.POLICY_VERSION_ID=@POLICY_VERSION_ID and current_term=@CURRENT_TERM           
   and (      
   case when ppp.process_id in (12,29) then app_effective_date else POL_VER_EFFECTIVE_DATE end <=@LOSS_DATE       
   and       
   case --WHEN  PCPL.CURRENT_TERM=isnull(@CURRENT_TERM,PCPL.CURRENT_TERM) then isnull(@CANCEL_POL_VER_EFFECTIVE_DATE ,POL_VER_EFFECTIVE_DATE)      
   when ppp.process_id in (12,29) then  POL_VER_EFFECTIVE_DATE       
   else POL_VER_EXPIRATION_DATE end > @LOSS_DATE       
   )      
 )              
begin        
 set @NEW_POLICY_VERSION_ID=0        
 select @NEW_POLICY_VERSION_ID AS RESULT        
 return 0       
end        
          
      
      
----Check for other versions of the policy        
if exists      
 (select pcpl.policy_version_id       
 from pol_customer_policy_list pcpl      
 left join pol_policy_process ppp on ppp.customer_id=pcpl.customer_id       
 and ppp.new_policy_version_id=pcpl.policy_version_id      
 where  pcpl.CUSTOMER_ID=@CUSTOMER_ID and  pcpl.POLICY_ID=@POLICY_ID and current_term=@CURRENT_TERM      
 and       
 case when ppp.process_id in (12,29) then app_effective_date else POL_VER_EFFECTIVE_DATE  end <=@LOSS_DATE        
 and      
 case when ppp.process_id in (12,29) then POL_VER_EFFECTIVE_DATE      
 else POL_VER_EXPIRATION_DATE end > @LOSS_DATE      
 and pcpl.policy_version_id not in      
 (      
  select policy_version_id from pol_policy_process pp where pp.customer_id=pcpl.customer_id       
  and pp.policy_id=pcpl.policy_id       
  and process_id=12 and upper(process_status)='COMPLETE'      
 )      
AND       
(      
 CASE WHEN @IS_CURRENT_TERM_CANCEL='Y' AND NOT       
 (@LOSS_DATE BETWEEN @CANCEL_POL_VER_EFFECTIVE_DATE AND @PRIOR_POL_VER_EFFECTIVE_DATE ) THEN 'TRUE' ELSE 'FALSE' END )='TRUE'      
      
 )      
begin        
 select @NEW_POLICY_VERSION_ID=pcpl.policy_version_id       
  from pol_customer_policy_list pcpl      
  left join pol_policy_process ppp on ppp.customer_id=pcpl.customer_id       
  and ppp.policy_id=pcpl.policy_id       
  and ppp.new_policy_version_id=pcpl.policy_version_id      
  where  pcpl.CUSTOMER_ID=@CUSTOMER_ID and  pcpl.POLICY_ID=@POLICY_ID and current_term=@CURRENT_TERM      
  and       
  case when ppp.process_id in (12,29) then app_effective_date else POL_VER_EFFECTIVE_DATE  end <=@LOSS_DATE        
  and        
  case when ppp.process_id in (12,29) then POL_VER_EFFECTIVE_DATE else POL_VER_EXPIRATION_DATE end > @LOSS_DATE      
  and pcpl.policy_version_id not in      
  (      
   select policy_version_id from pol_policy_process pp where pp.customer_id=pcpl.customer_id       
   and pp.policy_id=pcpl.policy_id       
   and process_id=12 and upper(process_status)='COMPLETE'      
  )      
  order by POL_VER_EFFECTIVE_DATE              
      
         
  select @NEW_POLICY_VERSION_ID AS RESULT        
 return @NEW_POLICY_VERSION_ID              
end        
ELSE      
 SET @IS_OTHER_VERSION ='N' -- NO OTHER VERSION EXISTS      
      
      
      
      
IF       
(      
(ISNULL(@IS_OTHER_VERSION,'')='N' AND @IS_CANCEL='Y')      
OR      
(@ADD_CLAIM = 1)      
)      
 BEGIN      
 return 0        
 END              
  
  
  
  
--Everything fails, there is some problem with loss date              
 set @NEW_POLICY_VERSION_ID=-1        
 select @NEW_POLICY_VERSION_ID AS RESULT      
 return -1    
   
      
END                                      
      
      
--go      
--exec [Proc_ValidateLossDate] 7662,1,1,'11/13/2009 12:01:00.000'      
--rollback tran 