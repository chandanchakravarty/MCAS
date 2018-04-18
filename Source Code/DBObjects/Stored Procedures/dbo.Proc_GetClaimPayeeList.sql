IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimPayeeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimPayeeList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------   
  
Proc Name : dbo.[Proc_GetClaimPayeeList]   
  
Created by : Shubhanshu Pandey   
  
Date : 16/2/2011   
  
Purpose : 
  
Revison History :   
  
Used In : Ebix Advantage  
  
------ ------------ -------------------------*/   
--DROP PROC [Proc_GetClaimPayeeList]  
  
  
CREATE PROCEDURE  [dbo].[Proc_GetClaimPayeeList]  
(  
 @CLAIM_ID INT,  
 @ACTIVITY_ID INT   
)  
AS  
BEGIN

  SELECT PAYEE_ID ,
  
     CASE 
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=10   THEN 'CLM_BENF' --THEN 'CLM_INSURED'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=12   THEN 'CLM_BENF' --THEN 'CLM_ADJUSTER'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=618  THEN 'CLM_BENF' --THEN 'CLM_COINS'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=619  THEN 'CLM_BENF' --THEN 'CLM_REINS'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=239  THEN 'CLM_BENF' --THEN 'CLM_INSURED'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=112  THEN 'CLM_BENF' --THEN 'CLM_ADJUSTER'
	  WHEN  CLM_PARTIES.PARTY_TYPE_ID=208  THEN 'CLM_BENF' --THEN 'CLM_AGENCY'
    END AS PARTY_TYPE
  FROM CLM_PAYEE WITH(NOLOCK) LEFT OUTER JOIN 
       CLM_PARTIES WITH(NOLOCK) ON CLM_PARTIES.CLAIM_ID=CLM_PAYEE.CLAIM_ID AND CLM_PAYEE.PARTY_ID=CLM_PARTIES.PARTY_ID     
  WHERE CLM_PAYEE.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID
  
  
  -- FOR REINSURER PARTY
  SELECT PARTY_TYPE_ID 
  FROM  CLM_PARTIES  
  WHERE CLAIM_ID=@CLAIM_ID AND PARTY_TYPE_ID=619 
  
END
GO

