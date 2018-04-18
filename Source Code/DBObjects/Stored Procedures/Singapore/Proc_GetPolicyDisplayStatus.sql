/*----------------------------------------------------------                                      
PROC NAME  : DBO.Proc_GetPolicyDisplayStatus                                      
CREATED BY      : Sumit Chhabra            
DATE            : 30-01-2007                                
PURPOSE         : TO GET THE POLICY display status INFORMATION                                       
REVISON HISTORY :                                      
USED IN         : WOLVERINE        
modified by  : Pravesh k. Chandel    
modified date :10 april 2007    
purpose` : to display status for non renewed        
                              
modified by  : Pravesh k. Chandel    
modified date :3 Sep 2007     
purpose` : to display status if cancelled NSF    
   
modified by  : Pravesh k. Chandel    
modified date :22 nov 2007     
purpose` : to display status if reinstate and then Endorsement    
    
modified by  : Pravesh k. Chandel    
modified date :1 SEP 2009     
purpose` : to display status if ANY PROCESS REVERTED    
    
------------------------------------------------------------         
NOTE: Please Also Update the Function 'fun_GetPolicyDisplayStatus' if this proc is modified                                 
*/                                
-- drop proc dbo.Proc_GetPolicyDisplayStatus   28723,15,1,null,null,2         
ALTER PROC dbo.Proc_GetPolicyDisplayStatus                                
(                                      
                                
 @CUSTOMER_ID  INT,                                      
 @POLICY_ID  INT,                                      
 @POLICY_VERSION_ID INT,            
 @POLICY_STATUS VARCHAR(100)=null OUTPUT,            
 @POLICY_STATUS_CODE varchar(100)=null OUTPUT ,  
 @LANG_ID smallint =1  
)             
AS            
BEGIN              
DECLARE @POLICY_STATUS_CANCELLED SMALLINT,@POLICY_STATUS_RESCINDED SMALLINT , @POLICY_STATUS_NONRENEWD SMALLINT,@CURRENT_TERM smallint,          
 @POLICY_STATUS_REINSTATE SMALLINT,@LAPSE_COVERAGE INT,@NO_LAPSE_COVERAGE int,@POLICY_STATUS_ENDORSE smallint,@POLICY_STATUS_CORRECTIVE smallint,    
 @POLICY_STATUS_REVERT smallint,@REVERTED_PROCESS_ID SMALLINT    
SET @POLICY_STATUS_RESCINDED = 29            
SET @POLICY_STATUS_CANCELLED = 12            
set @POLICY_STATUS_NONRENEWD =20    
set @POLICY_STATUS_REINSTATE =16    
set @POLICY_STATUS_ENDORSE =14    
set @POLICY_STATUS_CORRECTIVE =9    
set @POLICY_STATUS_REVERT =37    
--14          Commit Endorsement Process    
--16          Commit Reinstate Process     
--9           Commit Corrective User Process    
    
--if verstion is reverted VERSION    
declare @last_reverted_version int    
if exists(select new_policy_version_id    
 FROM POL_POLICY_PROCESS with(nolock)     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID     
 and new_policy_version_id=@POLICY_VERSION_ID and process_status<>'ROLLBACK'    
 AND PROCESS_ID=@POLICY_STATUS_REVERT     
)    
begin     
 select @last_reverted_version = dbo.piece(last_revert_back,'^',6)    
 FROM POL_POLICY_PROCESS with(nolock)     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID     
 and new_policy_version_id=@POLICY_VERSION_ID and process_status<>'ROLLBACK'    
 AND PROCESS_ID=@POLICY_STATUS_REVERT     
    
 select @REVERTED_PROCESS_ID=PROCESS_ID    
 FROM POL_POLICY_PROCESS with(nolock)     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID     
 and new_policy_version_id=@last_reverted_version and process_status<>'ROLLBACK'    
end    
-- end here    
    
set @LAPSE_COVERAGE=14244    
set @NO_LAPSE_COVERAGE=14245    
----    
DECLARE @CANCEL_EFFECTIVE_DATE varchar(20)        
SELECT @CANCEL_EFFECTIVE_DATE= isnull( case when @LANG_ID=2 then isnull(convert(varchar,max(EFFECTIVE_DATETIME),103),'') else isnull(convert(varchar,max(EFFECTIVE_DATETIME),101),'') end ,'') FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID         
AND POLICY_VERSION_ID=    
 (select max(policy_version_id) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=12)    
AND PROCESS_ID=12        
IF (@CANCEL_EFFECTIVE_DATE IS NULL)        
 SELECT @CANCEL_EFFECTIVE_DATE=isnull( case when @LANG_ID=2 then isnull(convert(varchar,max(EFFECTIVE_DATETIME),103),'') else isnull(convert(varchar,max(EFFECTIVE_DATETIME),101),'') end ,'')  FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID         
  AND POLICY_VERSION_ID =(select max(policy_version_id) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=20)    
  AND PROCESS_ID=20        
--check whether any reinstatement on the this term    
declare @REINSTATE_TYPE int    
declare @REINSTATE_EFFECTIVE_DATE VARCHAR(20)    
declare @REINSTATE_VERSION_ID smallint    
set @REINSTATE_TYPE=0    
SET @REINSTATE_VERSION_ID =0    
SELECT @CURRENT_TERM=CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
SELECT @REINSTATE_TYPE=isnull(max(CANCELLATION_TYPE),0),@REINSTATE_EFFECTIVE_DATE=isnull( case when @LANG_ID=2 then isnull(convert(varchar,max(EFFECTIVE_DATETIME),103),'') else isnull(convert(varchar,max(EFFECTIVE_DATETIME),101),'') end ,'')  ,    
 @REINSTATE_VERSION_ID=max(NEW_POLICY_VERSION_ID)    
 FROM POL_POLICY_PROCESS P with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID    
 AND NEW_POLICY_VERSION_ID     
 IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=P.CUSTOMER_ID AND POLICY_ID=P.POLICY_ID AND CURRENT_TERM=@CURRENT_TERM)    
 AND PROCESS_ID=16    
    
DECLARE @REINSTATE_CANCEL_DATE varchar(20)        
SELECT @REINSTATE_CANCEL_DATE=isnull( case when @LANG_ID=2 then isnull(convert(varchar,max(EFFECTIVE_DATETIME),103),'') else isnull(convert(varchar,max(EFFECTIVE_DATETIME),101),'') end ,'')  FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID         
AND POLICY_VERSION_ID=    
 (select max(policy_version_id) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=12    
  and POLICY_VERSION_ID<@REINSTATE_VERSION_ID    
  )    
AND PROCESS_ID=12     
----    
SELECT             
@POLICY_STATUS_CODE = STATUS_MASTER.POLICY_STATUS_CODE,    
@POLICY_STATUS =             
CASE PROCESS.PROCESS_ID              
 WHEN  @POLICY_STATUS_CANCELLED            
  --THEN (CASE WHEN STATUS_MASTER.POLICY_STATUS_CODE= 'SCANCEL' THEN STATUS_MASTER.POLICY_DESCRIPTION + (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + convert(char,EFFECTIVE_DATETIME,101)) ELSE (CASE PROCESS.CANCELLATION_TYPE           
  --THEN (CASE WHEN POLICY.POLICY_STATUS= 'SCANCEL'  THEN  STATUS_MASTER.POLICY_DESCRIPTION + ' - ' + (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY ,'') + ' - ' +  convert(char,PROCESS.EFFECTIVE_DATETIME,101)) ELSE (CASE PROCESS.CANCELLATION_TYPE             
  THEN (CASE WHEN POLICY.POLICY_STATUS= 'SCANCEL'  THEN  STATUS_MASTER.POLICY_DESCRIPTION + ' - ' + (ISNULL(MLV2.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) + ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,PROCESS.EFFECTIVE_DATETIME,103),'')
 else isnull(convert(char,PROCESS.EFFECTIVE_DATETIME,101),'') end ,'')) ELSE (CASE PROCESS.CANCELLATION_TYPE             
   WHEN 11989 -- Insured Request    
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')) --convert(char,COMPLETED_DATETIME,101))   same was below intead of EFFECTIVE_DATETIME    
   WHEN 11991 -- Non-Renewal    
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))             
   WHEN 11992  --NSF/ Replace    
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))        
   WHEN 11993--NSF/ No Replacement    
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))     
   WHEN 11990 --Company Request            
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))        
      
   WHEN 11971 --Cancel & Reinstatement           
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))             
   WHEN 11987 --Agents Request    
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))             
   WHEN 11988 --Cancel/Rewrite         
    THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))           
      
   WHEN 11969 THEN --non payment          
    CASE POLICY.BILL_TYPE WHEN 'DB' THEN         
    (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') +CASE WHEN @LANG_ID=2 THEN ' DIR' ELSE ' DB' END  + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')) --' DB'       
    ELSE        
    (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))        
    END        
    ELSE            
           isnull(MLV1.LOOKUP_FRAME_OR_MASONRY ,STATUS_MASTER.POLICY_DESCRIPTION )    
    END)    
  END)    
             
 WHEN @POLICY_STATUS_RESCINDED            
  THEN  CASE PROCESS.OTHER_RES_DATE_CD WHEN '1' THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,OTHER_RES_DATE,103),'') else isnull(convert(char,OTHER_RES_DATE,101),'') end ,''))   
   ELSE    
    (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))             
   END    
--  ELSE            
--  STATUS_MASTER.POLICY_DESCRIPTION       
      WHEN @POLICY_STATUS_NONRENEWD            
  --THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + convert(char,EFFECTIVE_DATETIME,101))             
   THEN  CASE WHEN POLICY.POLICY_STATUS= 'MNONRENEWED'  THEN  STATUS_MASTER.POLICY_DESCRIPTION +  ISNULL(' - ' + MLV2.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')    
    ELSE    
    (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))      
    END        
 WHEN @POLICY_STATUS_REINSTATE        
   THEN   STATUS_MASTER.POLICY_DESCRIPTION +  ISNULL(' - ' + MLV2.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) +     
    case when PROCESS.CANCELLATION_TYPE = @LAPSE_COVERAGE then  ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_CANCEL_DATE,103),'') else isnull(convert(char,@REINSTATE_CANCEL_DATE,101),'') end ,'')  + ' - ' +  isnull( case when 
@LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')    
        -- when PROCESS.CANCELLATION_TYPE = @NO_LAPSE_COVERAGE then ''      
                                     else    
       ''    
         end    
 WHEN  @POLICY_STATUS_ENDORSE     
   THEN   STATUS_MASTER.POLICY_DESCRIPTION +    
    CASE WHEN  NEW_POLICY_VERSION_ID >= @REINSTATE_VERSION_ID THEN     
    case  when @REINSTATE_TYPE != 0 then ' - ' +     
     --(SELECT ISNULL(LOOKUP_VALUE_DESC ,'') FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE LOOKUP_UNIQUE_ID = @REINSTATE_TYPE ) +     
     (ISNULL(dbo.fun_GetLookupDesc(@REINSTATE_TYPE,@LANG_ID) ,'')) +     
       case when @REINSTATE_TYPE = @LAPSE_COVERAGE then  ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_CANCEL_DATE,103),'') else isnull(convert(char,@REINSTATE_CANCEL_DATE,101),'') end ,'')  + ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_EFFECTIVE_DATE,103),'') else isnull(convert(char,@REINSTATE_EFFECTIVE_DATE,101),'') end ,'')      
                                      else ''  end    
    else    
    ''    
    end    
    ELSE    
    ''    
    END    
    
 WHEN  @POLICY_STATUS_CORRECTIVE     
   THEN STATUS_MASTER.POLICY_DESCRIPTION + CASE WHEN @LANG_ID=2 THEN '(PCU)' ELSE '(CUP)' END  --' (CUP)'    
 WHEN  @POLICY_STATUS_REVERT    
   THEN    
    CASE @REVERTED_PROCESS_ID     
    WHEN @POLICY_STATUS_ENDORSE    
     THEN   STATUS_MASTER.POLICY_DESCRIPTION +    
     CASE WHEN  NEW_POLICY_VERSION_ID >= @REINSTATE_VERSION_ID THEN     
     case  when @REINSTATE_TYPE != 0 then ' - ' +     
      --(SELECT ISNULL(LOOKUP_VALUE_DESC ,'') FROM MNT_LOOKUP_VALUES (NOLOCK) WHERE LOOKUP_UNIQUE_ID = @REINSTATE_TYPE ) +     
      (ISNULL(dbo.fun_GetLookupDesc(@REINSTATE_TYPE,@LANG_ID) ,'')) +    
        case when @REINSTATE_TYPE = @LAPSE_COVERAGE then  ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_CANCEL_DATE,103),'') else isnull(convert(char,@REINSTATE_CANCEL_DATE,101),'') end ,'')  + ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_EFFECTIVE_DATE,103),'') else isnull(convert(char,@REINSTATE_EFFECTIVE_DATE,101),'') end ,'')    
                        else ''  end    
      else    
     ''    
     end    
     ELSE    
     ''    
     END    
    WHEN @POLICY_STATUS_REINSTATE        
     THEN   STATUS_MASTER.POLICY_DESCRIPTION +  ISNULL(' - ' + MLV2.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) +     
     case when PROCESS.CANCELLATION_TYPE = @LAPSE_COVERAGE then  ' - ' +  isnull( case when @LANG_ID=2 then isnull(convert(char,@REINSTATE_CANCEL_DATE,103),'') else isnull(convert(char,@REINSTATE_CANCEL_DATE,101),'') end ,'')  + ' - ' +isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')    
                           else    
         ''    
          end    
    WHEN @POLICY_STATUS_RESCINDED            
     THEN  CASE PROCESS.OTHER_RES_DATE_CD WHEN '1' THEN  (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,OTHER_RES_DATE,103),'') else isnull(convert(char,OTHER_RES_DATE,101),'') end ,''))     
       ELSE    
       (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))             
       END    
    WHEN @POLICY_STATUS_NONRENEWD        
     THEN  CASE WHEN POLICY.POLICY_STATUS= 'MNONRENEWED'  THEN  STATUS_MASTER.POLICY_DESCRIPTION +  ISNULL(' - ' + MLV2.LOOKUP_VALUE_DESC,MLV1.LOOKUP_VALUE_DESC) + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,'')   
      ELSE    
      (ISNULL(MLV1.LOOKUP_FRAME_OR_MASONRY,'') + ' - ' + isnull( case when @LANG_ID=2 then isnull(convert(char,EFFECTIVE_DATETIME,103),'') else isnull(convert(char,EFFECTIVE_DATETIME,101),'') end ,''))      
      END         
    
    ELSE    
     STATUS_MASTER.POLICY_DESCRIPTION    
    END    
 ELSE            
  STATUS_MASTER.POLICY_DESCRIPTION     
    
 END              
FROM POL_CUSTOMER_POLICY_LIST POLICY with(nolock)             
INNER JOIN MNT_LOB_MASTER with(nolock)        
 ON  POLICY.POLICY_LOB = MNT_LOB_MASTER.LOB_ID                           
LEFT JOIN MNT_LOOKUP_VALUES PTL with(nolock)    
 ON POLICY.POLICY_TYPE= PTL.LOOKUP_UNIQUE_ID                      
LEFT JOIN   
(  
select POLICY_STATUS_CODE ,POLICY_DESCRIPTION ,3 as LANG_ID from POL_POLICY_STATUS_MASTER with(nolock)     
UNION    
select POLICY_STATUS_CODE ,POLICY_DESCRIPTION , LANG_ID from  POL_POLICY_STATUS_MASTER_MULTILINGUAL with(nolock)  
)STATUS_MASTER     
 ON STATUS_MASTER.POLICY_STATUS_CODE = ISNULL(POLICY.POLICY_STATUS,POLICY.APP_STATUS)                            
 AND STATUS_MASTER.LANG_ID=@LANG_ID  
LEFT OUTER JOIN POL_POLICY_PROCESS PROCESS WITH(NOLOCK)        
 ON POLICY.CUSTOMER_ID = PROCESS.CUSTOMER_ID     
 AND POLICY.POLICY_ID = PROCESS.POLICY_ID     
 AND POLICY.POLICY_VERSION_ID = PROCESS.NEW_POLICY_VERSION_ID     
 --AND POLICY.POLICY_STATUS = PROCESS.POLICY_CURRENT_STATUS             
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 with(nolock)     
 ON PROCESS.CANCELLATION_TYPE = MLV1.LOOKUP_UNIQUE_ID    
LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV2 ON  
MLV1.LOOKUP_UNIQUE_ID=MLV2.LOOKUP_UNIQUE_ID and MLV2.LANG_ID=@LANG_ID  
--LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV2 ON            
 -- PROCESS.REASON = MLV2.LOOKUP_UNIQUE_ID            
 WHERE                                 
(POLICY.CUSTOMER_ID = @CUSTOMER_ID)                                   
AND (POLICY.POLICY_ID=@POLICY_ID)                                 
AND (POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID)            
    
            
END        
        
  