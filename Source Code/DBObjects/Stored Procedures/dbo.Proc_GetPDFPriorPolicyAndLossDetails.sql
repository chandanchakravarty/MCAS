IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFPriorPolicyAndLossDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFPriorPolicyAndLossDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROCEDURE Proc_GetPDFPriorPolicyAndLossDetails    
CREATE PROCEDURE dbo.Proc_GetPDFPriorPolicyAndLossDetails    
(    
 @CUSTOMERID   int,    
 @LOBCODE  VARCHAR(20),    
 @DATAFOR  VARCHAR(20)  --'POLICY' FOR PRIOR POLICY AND 'LOSS' FOR PRIOR LOSS    
)    
AS    
BEGIN
print @DATAFOR    
 IF (@DATAFOR='POLICY')    
 BEGIN    
  SELECT     
   ISNULL(OLD_POLICY_NUMBER,'') OLD_POLICY_NUMBER ,    
   ISNULL(CARRIER,'') CARRIER,CASE WHEN EXP_DATE IS NULL THEN '' ELSE CONVERT(VARCHAR(11),EXP_DATE,101) END EFF_DATE    
   ,YEARS_PRIOR_COMP     
  FROM APP_PRIOR_CARRIER_INFO  CI  with(nolock)    
   INNER JOIN MNT_LOB_MASTER LM  with(nolock)  ON CI.LOB=LM.LOB_ID    
  WHERE CUSTOMER_ID=@CUSTOMERID and LOB_CODE = @LOBCODE and ci.is_active='Y'    
  order by APP_PRIOR_CARRIER_INFO_ID desc    
    
 END    
 ELSE IF (@DATAFOR='LOSS')    
 BEGIN    
  SELECT     
   CASE WHEN OCCURENCE_DATE IS NULL THEN '' ELSE CONVERT(VARCHAR(11),OCCURENCE_DATE,101) END OCCURENCE_DATE,    
   LOOKUP_VALUE_DESC LOSS_TYPE,ISNULL(REMARKS,'') LOSS_DESC,
   convert(varchar,convert(money,AMOUNT_PAID),1)  as AMOUNT  
       
  FROM APP_PRIOR_LOSS_INFO  CI  with(nolock)    
   INNER JOIN MNT_LOB_MASTER LM  with(nolock)  ON CI.LOB=LM.LOB_ID    
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MV ON LOOKUP_ID=563 AND LOOKUP_UNIQUE_ID=LOSS_TYPE    
  WHERE CUSTOMER_ID=@CUSTOMERID and LOB_CODE = @LOBCODE and ci.is_active='Y'    
 END    
END    




GO

