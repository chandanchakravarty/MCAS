IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ClaimStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ClaimStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN    
--drop proc dbo.rpt_ClaimStatus       
--GO    
--    
/*----------------------------------------------------------                      
Proc Name        : dbo.rpt_ClaimStatus                     
Created by       : Swarup              
Date             : 07/01/2008           124     
Purpose          :                
Revison History  :                      
Used In          : Wolverine                     
--rpt_ClaimStatus '','',null,null,null,null,'',''               
------------------------------------------------------------ */                
-- drop proc dbo.rpt_ClaimStatus              
CREATE PROCEDURE [dbo].[rpt_ClaimStatus]          
 @AGENCYID VARCHAR(80)= NULL,          
 @INSUREDID VARCHAR(80)= NULL,          
 @CLAIMID VARCHAR(80)= NULL,          
 @CUSTOMERID VARCHAR(80)= NULL,          
 @POLICYID VARCHAR(80)= NULL,          
 @VERSIONID VARCHAR(80)= NULL,          
 @STARTDATEID VARCHAR(80)= NULL,          
 @ENDDATEID VARCHAR(80)= NULL,  
 @ORDERBY VARCHAR(100)        
AS          
DECLARE @sql VARCHAR(8000)     
BEGIN     
    
IF (@AGENCYID = '' OR @AGENCYID IS NULL)    
 SET @AGENCYID='0'    
IF (@INSUREDID = '' OR @INSUREDID IS NULL)    
 SET @INSUREDID='0'    
IF (@CLAIMID = '' OR @CLAIMID IS NULL)    
 SET @CLAIMID='0'    
IF (@CUSTOMERID = '' OR @CUSTOMERID IS NULL)    
 SET @CUSTOMERID='0'    
IF (@POLICYID = '' OR @POLICYID IS NULL)    
 SET @POLICYID='0'    
IF (@VERSIONID = '' OR @VERSIONID IS NULL)    
 SET @VERSIONID='0'    
IF (@STARTDATEID = '' OR @STARTDATEID IS NULL)    
 SET @STARTDATEID='0'    
IF (@ENDDATEID = '' OR @ENDDATEID IS NULL)    
 SET @ENDDATEID='0'    
          
  Select @sql = 'SELECT DISTINCT AGN.AGENCY_ID, AGN.AGENCY_DISPLAY_NAME as AGENCYNAME, CLAIM_NUMBER,    
CCI.LOSS_DATE AS LOSS_DATE ,CCI.CREATED_DATETIME,    
MLV.LOOKUP_VALUE_DESC AS CLAIM_STATUS,    
PCPL.POLICY_NUMBER + '' v''+ PCPL.POLICY_DISP_VERSION AS POLICY_NUMBER,MLM.LOB_DESC,    
--ISNULL(CLT.CUSTOMER_FIRST_NAME,'''') + '' '' + ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'''') +'' '' + ISNULL(CLT.CUSTOMER_LAST_NAME,'''') AS INSURED,    
CP.NAME AS INSURED,cci.diary_date,          
  CA.ADJUSTER_NAME,PAID_LOSS FROM CLM_CLAIM_INFO CCI     
LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL     
ON CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID     
AND CCI.POLICY_ID = PCPL.POLICY_ID     
AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID           
  LEFT OUTER JOIN MNT_LOB_MASTER MLM ON MLM.LOB_ID = CCI.LOB_ID          
  INNER JOIN CLT_CUSTOMER_LIST CLT ON CLT.CUSTOMER_ID=CCI.CUSTOMER_ID          
  INNER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_ID = CA.ADJUSTER_ID          
  INNER JOIN CLM_PARTIES CP ON CP.CLAIM_ID = CCI.CLAIM_ID          
  INNER JOIN CLM_TYPE_DETAIL CTD ON CP.PARTY_TYPE_ID = CTD.DETAIL_TYPE_ID        
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID =CCI.CLAIM_STATUS       
INNER JOIN MNT_AGENCY_LIST AGN ON AGN.AGENCY_ID = PCPL.AGENCY_ID       
WHERE CTD.DETAIL_TYPE_ID=10 '         
    
    
IF (@AGENCYID <> '0')    
BEGIN    
 SELECT @sql=@sql + ' AND AGN.AGENCY_ID = (''' + @AGENCYID +''')'    
END    
           
IF (@INSUREDID <> '0')          
  BEGIN          
   SELECT @sql=@sql + ' AND CP.NAME = (''' + @INSUREDID +''')'          
  END          
IF (@CLAIMID <> '0')          
  BEGIN          
   SELECT @sql=@sql + ' AND CCI.CLAIM_ID IN(' + @CLAIMID +')'          
  END          
IF (@POLICYID <> '0')          
  BEGIN          
   SELECT @sql=@sql + ' AND CCI.CUSTOMER_ID IN(' + @CUSTOMERID +')'          
   SELECT @sql=@sql + ' AND CCI.POLICY_ID IN(' + @POLICYID +')'          
   SELECT @sql=@sql + ' AND CCI.POLICY_VERSION_ID IN(' + @VERSIONID +')'          
  END          
IF (@STARTDATEID <> '0')          
  BEGIN      
print  @STARTDATEID     
--Changed by Shikha Dixit agaunst itrack# 5443 on 8th May 2009.    
   --SELECT @sql=@sql + ' AND CAST(CONVERT(VARCHAR,CCI.CREATED_DATETIME ,101) AS DATETIME) >= ''' + CONVERT(VARCHAR, @STARTDATEID,101) + ''''         
 SELECT @sql = @sql + ' AND DATEDIFF(DD,CCI.CREATED_DATETIME,''' + @STARTDATEID + ''')<=0'    
  END          
IF (@ENDDATEID <> '0')          
  BEGIN     
   --SELECT @sql=@sql + ' AND CAST(CONVERT(VARCHAR,CCI.CREATED_DATETIME ,101) AS DATETIME) <= ''' + CONVERT(VARCHAR, @ENDDATEID,101) + ''''      
 SELECT @sql = @sql + ' AND DATEDIFF(DD,CCI.CREATED_DATETIME,''' + @ENDDATEID + ''')>=0'    
  END        
--SELECT @sql = @sql + ' order by AGENCYNAME, CLAIM_NUMBER'  
SELECT @sql = @sql + ' ORDER BY ' + @ORDERBY  -- Done for Itrack Issue 6070 on 8 July 09       
EXEC (@sql)          
--PRINT @sql          
END     
--    
--GO    
--EXEC rpt_ClaimStatus '','','','','','','5/28/2009','','CLAIM_NUMBER'   
--ROLLBACK TRAN    
    
    
    
    
    
    

GO

