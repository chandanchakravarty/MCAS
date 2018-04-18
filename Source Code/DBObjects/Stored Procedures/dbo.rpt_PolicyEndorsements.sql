IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_PolicyEndorsements]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_PolicyEndorsements]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop PROCEDURE dbo.rpt_PolicyEndorsements
--go  
    
CREATE PROCEDURE dbo.rpt_PolicyEndorsements    
 @intCustomerId VARCHAR(8000),    
 @intPolicyId VARCHAR(8000),    
 @intAgencyId VARCHAR(800)=NULL    
AS    
    
BEGIN    
-- Commented by Asfa (24-Jan-2008) -iTrack issue #3479    
--  IF(@intPolicyId = '-1')    
--  BEGIN    
--    SET @intPolicyId = 0    
--  END    
    
-- Added by Asfa (24-Jan-2008) -iTrack issue #3479    
 IF(@intPolicyId = '-1')    
   return    
    
 DECLARE @sql VARCHAR(8000)    
 Select @sql = 'SELECT PPE.CUSTOMER_ID,PPE.POLICY_ID,PPE.ENDORSEMENT_DATE,PPE.ENDORSEMENT_STATUS,PPED.ENDORSEMENT_DETAIL_ID,  
PPED.ENDORSEMENT_TYPE,PPED.ENDORSEMENT_DESC,   PPED.REMARKS, --PCPL.CREATED_DATETIME,  
POL.EFFECTIVE_DATETIME AS CREATED_DATETIME, MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC,   
CCL.CUSTOMER_FIRST_NAME,  CCL.CUSTOMER_MIDDLE_NAME, CCL.CUSTOMER_LAST_NAME, PCPL.POLICY_NUMBER, --PCPL.POLICY_DISP_VERSION,  
--ISNULL(CONVERT(VARCHAR(50),POL.NEW_POLICY_VERSION_ID)+ ''.0'', '''')  AS POLICY_DISP_VERSION,    
PCPL.POLICY_DISP_VERSION,  
PCPL.APP_EFFECTIVE_DATE, PCPL.APP_EXPIRATION_DATE    
FROM         POL_POLICY_ENDORSEMENTS PPE INNER JOIN  POL_POLICY_ENDORSEMENTS_DETAILS PPED ON   PPE.CUSTOMER_ID = PPED.CUSTOMER_ID AND    
PPE.POLICY_ID = PPED.POLICY_ID AND   PPE.POLICY_VERSION_ID = PPED.POLICY_VERSION_ID AND   PPE.ENDORSEMENT_NO = PPED.ENDORSEMENT_NO   
INNER JOIN  POL_CUSTOMER_POLICY_LIST PCPL ON PPE.CUSTOMER_ID = PCPL.CUSTOMER_ID AND  PPE.POLICY_ID = PCPL.POLICY_ID AND    
PPE.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID INNER JOIN  MNT_LOOKUP_VALUES ON MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = PPED.ENDORSEMENT_TYPE   
INNER JOIN  CLT_CUSTOMER_LIST CCL ON PPE.CUSTOMER_ID = CCL.CUSTOMER_ID    
INNER JOIN POL_POLICY_PROCESS POL     ON PPED.CUSTOMER_ID = POL.CUSTOMER_ID     AND PPED.POLICY_ID = POL.POLICY_ID       
AND POL.CREATED_DATETIME=    (SELECT TOP 1 POL1.CREATED_DATETIME FROM POL_POLICY_PROCESS POL1     WHERE PPED.CUSTOMER_ID = POL1.CUSTOMER_ID       
AND PPED.POLICY_ID = POL1.POLICY_ID     AND POL1.CREATED_DATETIME < PPED.CREATED_DATETIME     ORDER BY POL1.CREATED_DATETIME DESC)      '    
 --IF (@intCustomerId <> 0)    
 IF (@intCustomerId <> '0')    
         BEGIN    
       --  SELECT @sql=@sql + ' WHERE POL_POLICY_ENDORSEMENTS.CUSTOMER_ID = ' + CAST(@intCustomerId AS VARCHAR(10))    
  SELECT @sql=@sql + ' WHERE PPE.CUSTOMER_ID IN (' + @intCustomerId + ')'     
  END    
  --IF (@intPolicyId <> 0)    
  IF (@intPolicyId <> '0')    
         BEGIN    
   -- SELECT @sql=@sql + ' AND POL_POLICY_ENDORSEMENTS.POLICY_ID = ' + CAST(@intPolicyId AS VARCHAR(10))    
  SELECT @sql=@sql + ' AND PPE.POLICY_ID IN (' + @intPolicyId + ')'    
  END    
    
  IF (isnull(@intAgencyId,'0') <> '0')    
         BEGIN    
   -- SELECT @sql=@sql + ' AND POL_POLICY_ENDORSEMENTS.POLICY_ID = ' + CAST(@intPolicyId AS VARCHAR(10)) 
--order by Condition Added For  Itrack Issue #6337   
  SELECT @sql=@sql + ' AND PCPL.AGENCY_ID IN (' + @intAgencyId + ')ORDER BY PPE.CUSTOMER_ID ,PPE.POLICY_ID ,PPED.ENDORSEMENT_DETAIL_ID '    
  END 

--Condition Added For  Itrack Issue #6337
  IF(@INTCUSTOMERID = '0' AND @INTPOLICYID = '0' AND ISNULL(@INTAGENCYID,'0') = '0') 

   BEGIN
	SELECT @SQL=@SQL +  'ORDER BY PPE.CUSTOMER_ID ,PPE.POLICY_ID ,PPED.ENDORSEMENT_DETAIL_ID '
	END   
    
print(@sql)    
exec(@sql)    
    
END
--go
--exec rpt_PolicyEndorsements 0,0,0 
--rollback tran


GO

