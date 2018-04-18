IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCoverageDetailXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCoverageDetailXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------          
Proc Name          : Dbo.Proc_GetCoverageDetailXml          
Created by           : Mohit Gupta          
Date                    : 19/05/2005          
Purpose               :           
Revison History :          
Modified by  :Pravesh k Chandel / Praveen Kumar   
Modified Date : 9 aug 2007   /   19/08/2007 [DISPLAY_ON_CLAIM,CLAIM_RESERVE_APPLY] 
Purpose  : to fetch coverage base on the cov_id if COvrage is Reinsurance or normal Coverage    
Used In                :   Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
---drop proc  Proc_GetCoverageDetailXml 10005,1,2  
CREATE   PROCEDURE [dbo].[Proc_GetCoverageDetailXml]          
(          
 @Cov_Id int   ,    
 @COV_TYPE   nvarchar(10)=null,
 @LANG_ID INT=NULL    
)          
AS          
BEGIN      
if  (isnull(@COV_TYPE,'0')!='3')       
 SELECT          
 COV_ID,          
 COV_REF_CODE,          
 COV_CODE,          
 COV_DES,          
 STATE_ID,          
 LOB_ID,          
 IS_ACTIVE,          
 IS_DEFAULT,
 SUB_LOB_ID,          
 TYPE,          
 PURPOSE,          
 LIMIT_TYPE,          
 DEDUCTIBLE_TYPE,          
 IsLimitApplicable,          
 IsDeductApplicable,          
 isnull(ISADDDEDUCTIBLE_APP,0) ISADDDEDUCTIBLE_APP,          
 isnull(ADDDEDUCTIBLE_TYPE,0) ADDDEDUCTIBLE_TYPE,          
 INCLUDED,          
 COVERAGE_TYPE,          
 CONVERT(integer,RANK) as RANK ,          
 IS_MANDATORY,         
 CONVERT(VARCHAR,ISNULL(EFFECTIVE_FROM_DATE,'1950-01-01'),CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END ) AS EFFECTIVE_FROM_DATE,          
 CONVERT(VARCHAR,EFFECTIVE_TO_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS EFFECTIVE_TO_DATE,           
 CONVERT(VARCHAR,DISABLED_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DISABLED_DATE,        
 REINSURANCE_LOB,        
 REINSURANCE_COV,        
 ASLOB,        
 REINSURANCE_CALC,    
 FORM_NUMBER,    
 COMM_VEHICLE,    
 COMM_REIN_COV_CAT,    
 REIN_ASLOB,    
 COMM_CALC,    
 REIN_REPORT_BUCK,    
 REIN_REPORT_BUCK_COMM,  
 CONVERT(VARCHAR,MANDATORY_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS MANDATORY_DATE,  
 CONVERT(VARCHAR,NON_MANDATORY_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS NON_MANDATORY_DATE,  
 CONVERT(VARCHAR,DEFAULT_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DEFAULT_DATE,  
 CONVERT(VARCHAR,NON_DEFAULT_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS NON_DEFAULT_DATE,
DISPLAY_ON_CLAIM,
CLAIM_RESERVE_APPLY,
ISNULL(IS_MAIN,'0') AS IS_MAIN,
COV_TYPE_ABBR,
SUSEP_COV_CODE
FROM MNT_COVERAGE            
 WHERE COV_ID=@Cov_Id          
else    
 SELECT       
 COV_ID,          
 COV_REF_CODE,          
 COV_CODE,          
 COV_DES,          
 STATE_ID,          
 LOB_ID,          
 IS_ACTIVE,          
 IS_DEFAULT,          
 TYPE,          
 0 as PURPOSE,          
 LIMIT_TYPE,          
 DEDUCTIBLE_TYPE,          
 IsLimitApplicable,          
 IsDeductApplicable,          
 isnull(ISADDDEDUCTIBLE_APP,0) ISADDDEDUCTIBLE_APP,          
 isnull(ADDDEDUCTIBLE_TYPE,0) ADDDEDUCTIBLE_TYPE,          
 0 as INCLUDED,          
 COVERAGE_TYPE,          
 CONVERT(integer,RANK) as RANK ,          
 IS_MANDATORY,          
 CONVERT(VARCHAR,ISNULL(EFFECTIVE_FROM_DATE,'1950-01-01'),CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS EFFECTIVE_FROM_DATE,          
 CONVERT(VARCHAR,EFFECTIVE_TO_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS EFFECTIVE_TO_DATE,           
 CONVERT(VARCHAR,DISABLED_DATE    ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DISABLED_DATE,        
 REINSURANCE_LOB,        
 REINSURANCE_COV,        
 ASLOB,        
 REINSURANCE_CALC,    
 REIN_REPORT_BUCK,    
 FORM_NUMBER,    
 COMM_VEHICLE,    
 COMM_REIN_COV_CAT,    
 REIN_ASLOB,    
 COMM_CALC,
 SUB_LOB_ID,    
 REIN_REPORT_BUCK_COMM,  
 CONVERT(VARCHAR,MANDATORY_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS MANDATORY_DATE,  
 CONVERT(VARCHAR,NON_MANDATORY_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS NON_MANDATORY_DATE,  
 CONVERT(VARCHAR,DEFAULT_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DEFAULT_DATE,  
 CONVERT(VARCHAR,NON_DEFAULT_DATE ,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS NON_DEFAULT_DATE, 
 DISPLAY_ON_CLAIM,
CLAIM_RESERVE_APPLY,
ISNULL(IS_MAIN,'0') AS IS_MAIN,
COV_TYPE_ABBR,
SUSEP_COV_CODE       
 FROM MNT_REINSURANCE_COVERAGE          
 WHERE COV_ID=@Cov_Id          
          
END          
          
          
          
          
    
    
    
    
    
    
    
    
    
    
    
GO

