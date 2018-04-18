IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFRVAdditionalInterests]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFRVAdditionalInterests]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROCEDURE Proc_GetPDFRVAdditionalInterests  
CREATE  PROCEDURE Proc_GetPDFRVAdditionalInterests  
(  
 @CUSTOMERID   int,  
 @POLID                int,  
 @VERSIONID   int,  
 @RVID    int,  
 @CALLEDFROM  VARCHAR(20)  
)  
AS  
BEGIN  
 IF (@CALLEDFROM='APPLICATION')  
 BEGIN  
  SELECT   
   ADD_INT_ID,LOOKUP_VALUE_DESC NATUREOFINT,LOAN_REF_NUMBER,  
   HI.HOLDER_NAME + ', ' + HI.HOLDER_ADD1 + ', '+HI.HOLDER_ADD2+', '+HI.HOLDER_CITY+', '+STATE_CODE+', '+HI.HOLDER_ZIP+ '' ADDLINT_NAME ,
	REC_VEH_ID 
  FROM APP_HOMEOWNER_REC_VEH_ADD_INT AI  with(nolock) 
   INNER JOIN MNT_HOLDER_INTEREST_LIST HI with(nolock) ON HI.HOLDER_ID=AI.HOLDER_ID  
   INNER JOIN MNT_COUNTRY_STATE_LIST with(nolock) ON STATE_ID=HI.HOLDER_STATE  
   INNER JOIN MNT_LOOKUP_VALUES with(nolock) ON LOOKUP_ID=1213 AND LOOKUP_UNIQUE_ID=NATURE_OF_INTEREST  
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND REC_VEH_ID=isnull(@RVID,REC_VEH_ID) --ORDER BY ADD_INT_ID  
   
 UNION   
  
  SELECT   
   ADD_INT_ID,LOOKUP_VALUE_DESC NATUREOFINT,LOAN_REF_NUMBER,  
   Case ISNULL(holder_id,'') when '' then AI.HOLDER_NAME + ', ' + AI.HOLDER_ADD1 + ', '+AI.HOLDER_ADD2+', '+AI.HOLDER_CITY+', '+STATE_CODE+', '+AI.HOLDER_ZIP+ ''   
   ELSE (SELECT HOLDER_NAME + ', ' + HOLDER_ADD1 + ', '+HOLDER_ADD2+', '+HOLDER_CITY+', '+STATE_CODE+', '+HOLDER_ZIP+ '' FROM MNT_HOLDER_INTEREST_LIST WHERE HOLDER_ID=AI.HOLDER_ID) end as ADDLINT_NAME  ,
	REC_VEH_ID
  FROM APP_HOMEOWNER_REC_VEH_ADD_INT AI with(nolock)  
   --INNER JOIN MNT_HOLDER_INTEREST_LIST HI ON HI.HOLDER_ID=AI.HOLDER_ID  
   INNER JOIN MNT_COUNTRY_STATE_LIST with(nolock) ON STATE_ID=HOLDER_STATE  
   INNER JOIN MNT_LOOKUP_VALUES with(nolock) ON LOOKUP_ID=1213 AND LOOKUP_UNIQUE_ID=NATURE_OF_INTEREST  
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND REC_VEH_ID=isnull(@RVID,REC_VEH_ID) --ORDER BY ADD_INT_ID  
END  
  
 ELSE IF (@CALLEDFROM='POLICY')  
 BEGIN  
  SELECT   
   ADD_INT_ID,LOOKUP_VALUE_DESC NATUREOFINT,LOAN_REF_NUMBER,  
   HI.HOLDER_NAME + ', ' + HI.HOLDER_ADD1 + ', '+HI.HOLDER_ADD2+', '+HI.HOLDER_CITY+', '+STATE_CODE+', '+HI.HOLDER_ZIP+ '' ADDLINT_NAME  ,
	REC_VEH_ID
  FROM POL_HOMEOWNER_REC_VEH_ADD_INT AI  with(nolock) 
   INNER JOIN MNT_HOLDER_INTEREST_LIST HI with(nolock) ON HI.HOLDER_ID=AI.HOLDER_ID  
   INNER JOIN MNT_COUNTRY_STATE_LIST with(nolock) ON STATE_ID=HI.HOLDER_STATE  
   INNER JOIN MNT_LOOKUP_VALUES with(nolock) ON LOOKUP_ID=1213 AND LOOKUP_UNIQUE_ID=NATURE_OF_INTEREST  
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND REC_VEH_ID=isnull(@RVID,REC_VEH_ID) --ORDER BY ADD_INT_ID  
    
 UNION  
  SELECT   
   ADD_INT_ID,LOOKUP_VALUE_DESC NATUREOFINT,LOAN_REF_NUMBER,  
   Case ISNULL(holder_id,'') when '' then AI.HOLDER_NAME + ', ' + AI.HOLDER_ADD1 + ', '+AI.HOLDER_ADD2+', '+AI.HOLDER_CITY+', '+STATE_CODE+', '+AI.HOLDER_ZIP+ ''   
   ELSE (SELECT HOLDER_NAME + ', ' + HOLDER_ADD1 + ', '+HOLDER_ADD2+', '+HOLDER_CITY+', '+STATE_CODE+', '+HOLDER_ZIP+ '' FROM MNT_HOLDER_INTEREST_LIST WHERE HOLDER_ID=AI.HOLDER_ID) end as ADDLINT_NAME  ,
	REC_VEH_ID
  FROM POL_HOMEOWNER_REC_VEH_ADD_INT AI with(nolock) 
   --INNER JOIN MNT_HOLDER_INTEREST_LIST HI ON HI.HOLDER_ID=AI.HOLDER_ID  
   INNER JOIN MNT_COUNTRY_STATE_LIST with(nolock)  ON STATE_ID=HOLDER_STATE  
   INNER JOIN MNT_LOOKUP_VALUES with(nolock)  ON LOOKUP_ID=1213 AND LOOKUP_UNIQUE_ID=NATURE_OF_INTEREST  
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND REC_VEH_ID=isnull(@RVID,REC_VEH_ID) --ORDER BY ADD_INT_ID  
 END  
END  
  
  
  
  
  
  
  
  
  




GO

