  
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_MNT_REIN_GETXML_CONTRACT]                      
Created by      : Harmanjeet Singh                     
Date            : May 07, 2007                    
Purpose         : To insert the data into Reinsurer Construction Translation table.                      
Revison History :                      
modified by  : pravesh K Chandel      
Modified Date :29Aug2007      
      
Used In         : Wolverine               
 drop proc [dbo].[Proc_MNT_REIN_GETXML_CONTRACT]                    
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/       
--drop proc [dbo].[Proc_MNT_REIN_GETXML_CONTRACT]82,2                   
alter proc [dbo].[Proc_MNT_REIN_GETXML_CONTRACT]       
(                
 @CONTRACT_ID int,  
 @lang_id int=1        
  )                      
AS                      
BEGIN            
                      
                   
    SELECT      
      
       
 ISNULL(CONTRACT_ID,0) CONTRACT_ID ,        
 ISNULL(CONTRACT_TYPE,0) CONTRACT_TYPE     ,        
 ISNULL(CONTRACT_NUMBER,'')CONTRACT_NUMBER   ,      
 ISNULL(CONTRACT_DESC,'')CONTRACT_DESC   ,      
 ISNULL(LOSS_ADJUSTMENT_EXPENSE,'')LOSS_ADJUSTMENT_EXPENSE,      
 ISNULL(RISK_EXPOSURE,0)RISK_EXPOSURE ,      
 ISNULL(CONTRACT_LOB,0)CONTRACT_LOB      ,        
 ISNULL(STATE_ID,0) STATE_ID         ,       
 CONVERT(VARCHAR,ORIGINAL_CONTACT_DATE,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END)ORIGINAL_CONTACT_DATE ,      
 ISNULL(CONTACT_YEAR,'')CONTACT_YEAR ,      
 CONVERT(VARCHAR,EFFECTIVE_DATE,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END)EFFECTIVE_DATE   ,        
 CONVERT(VARCHAR,EXPIRATION_DATE,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END) EXPIRATION_DATE   ,        
 ISNULL(COMMISSION,'')COMMISSION ,      
 ISNULL(CALCULATION_BASE,0)CALCULATION_BASE  ,      
 ISNULL(PER_OCCURRENCE_LIMIT,'')PER_OCCURRENCE_LIMIT ,      
 ISNULL(ANNUAL_AGGREGATE,'')ANNUAL_AGGREGATE,      
 ISNULL(DEPOSIT_PREMIUMS,'')DEPOSIT_PREMIUMS ,      
 ISNULL(DEPOSIT_PREMIUM_PAYABLE,'')DEPOSIT_PREMIUM_PAYABLE,      
 ISNULL(MINIMUM_PREMIUM,'') MINIMUM_PREMIUM,      
 ISNULL(SEQUENCE_NUMBER,'')SEQUENCE_NUMBER,      
 CONVERT(VARCHAR,TERMINATION_DATE,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END)TERMINATION_DATE ,      
 ISNULL(TERMINATION_REASON,'') TERMINATION_REASON,      
 ISNULL(COMMENTS,'')COMMENTS ,      
 ISNULL(FOLLOW_UP_FIELDS,'') FOLLOW_UP_FIELDS,      
 ISNULL(COMMISSION_APPLICABLE,0)COMMISSION_APPLICABLE ,      
 ISNULL(REINSURANCE_PREMIUM_ACCOUNT,'') REINSURANCE_PREMIUM_ACCOUNT,      
 ISNULL(REINSURANCE_PAYABLE_ACCOUNT,'') REINSURANCE_PAYABLE_ACCOUNT,      
 ISNULL(REINSURANCE_COMMISSION_ACCOUNT,'')REINSURANCE_COMMISSION_ACCOUNT,      
 ISNULL(REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT,'') REINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT,      
      
 ISNULL(MODIFIED_BY,'')MODIFIED_BY    ,        
 CONVERT(VARCHAR,LAST_UPDATED_DATETIME,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END)LAST_UPDATED_DATETIME,      
       
 ISNULL(CREATED_BY ,'') CREATED_BY  ,        
 CONVERT(VARCHAR,CREATED_DATETIME ,CASE WHEN @lang_id=3 THEN 103 ELSE 101 END)CREATED_DATETIME,      
 ISNULL(IS_ACTIVE,'') IS_ACTIVE ,     
 ISNULL(FOLLOW_UP_FOR,'')FOLLOW_UP_FOR,  
 CASH_CALL_LIMIT,  
  ISNULL(MAX_NO_INSTALLMENT,'')MAX_NO_INSTALLMENT, --Added by Aditya for TFS BUG # 2512   
  ISNULL(RI_CONTRACTUAL_DEDUCTIBLE,0) RI_CONTRACTUAL_DEDUCTIBLE --Added by Aditya for TFS BUG # 2916  
      
 FROM MNT_REINSURANCE_CONTRACT      
          
 WHERE      
 CONTRACT_ID=@CONTRACT_ID;      
   
 --========================================================================  
 -- MODIFIED BY SANTOSH KUMAR GAUTAM ON 11 APR 2011 FOR ITRACK:462  
 --========================================================================  
 SELECT ISNULL( LMV.LOOKUP_VALUE_DESC, L.LOOKUP_VALUE_DESC) LOOKUPDESC,  
        L.LOOKUP_UNIQUE_ID LOOKUPID,  
        RSK.RISK_EXPOSURE       
 FROM MNT_LOOKUP_VALUES L INNER JOIN   
 MNT_REIN_CONTRACT_RISKEXPOSURE RSK ON RSK.RISK_EXPOSURE=L.LOOKUP_UNIQUE_ID and CONTRACT_ID=@CONTRACT_ID LEFT OUTER JOIN  
 MNT_LOOKUP_VALUES_MULTILINGUAL LMV  on LMV.LOOKUP_UNIQUE_ID=L.LOOKUP_UNIQUE_ID and LMV.LANG_ID=@lang_id;      
      
 SELECT B.LOB_ID LOB_ID,isnull(mlv.LOB_DESC,B.LOB_DESC) LOB_DESC,lb.CONTRACT_LOB      
 FROM MNT_REIN_CONTRACT_LOB lb INNER JOIN MNT_LOB_MASTER B      
 ON lb.CONTRACT_LOB=B.LOB_ID and CONTRACT_ID=@CONTRACT_ID  
 left outer join MNT_LOB_MASTER_MULTILINGUAL mlv on mlv.LOB_ID=B.LOB_ID and mlv.LANG_ID=@lang_id;      
      
 SELECT D.STATE_NAME STATE_NAME,D.STATE_ID STATE_ID,st.STATE_ID      
 FROM MNT_REIN_CONTRACT_STATE st inner join MNT_COUNTRY_STATE_LIST D       
 On D.State_ID=st.STATE_ID and CONTRACT_ID= @CONTRACT_ID;      
      
      
       
                     
  END      
      
      
      
      
      
    
    