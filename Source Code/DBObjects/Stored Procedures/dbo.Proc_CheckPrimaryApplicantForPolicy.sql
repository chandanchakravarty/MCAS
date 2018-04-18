IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPrimaryApplicantForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPrimaryApplicantForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name          : Dbo.Proc_CheckPrimaryApplicantForPolicy                    
Created by         : Vijay Arora                    
Date               : 28-10-2005                    
Purpose            :                       
Revison History :                      
------------------------------------------------------------                      
Date     Review By          Comments                
drop proc Proc_CheckPrimaryApplicantForPolicy 
EXEC Proc_CheckPrimaryApplicantForPolicy 28163,11,2                  
------   ------------       -------------------------*/     
                 
CREATE PROCEDURE [dbo].[Proc_CheckPrimaryApplicantForPolicy]                    
(                      
 @CUSTOMERID  int,                      
 @POLICYID       int,                      
 @POLICYVERSIONID  int                      
)                      
AS  

DECLARE @LOB_ID NVARCHAR(10) 

SELECT @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID 
        AND   POLICY_VERSION_ID=@POLICYVERSIONID                
BEGIN                      
                      
SELECT               
 T2.APPLICANT_ID,ISNULL(T2.FIRST_NAME,'') + ' ' + ISNULL(T2.MIDDLE_NAME,'') + ' ' + ISNULL(T2.LAST_NAME,'')             
 APPLICANTNAME,                
 ISNULL(T2.ADDRESS1,'')+case when T2.ADDRESS1!='' THEN CASE WHEN T2.NUMBER!='' THEN ', ' ELSE '' END ELSE ''END+ISNULL(T2.NUMBER,'')+ case when T2.ADDRESS2 <>'' then ISNULL('  '+T2.ADDRESS2,'') else '' end ADDRESS,T2.CITY,T2.ZIP_CODE,T1.IS_PRIMARY_APPLICANT,    
 --+ CASE WHEN T2.DISTRICT!='' THEN ' - ' ELSE ' ' END +ISNULL(T2.DISTRICT,'')+CASE WHEN T2.CITY!='' THEN CASE WHEN T2.CITY !='' THEN ' - ' ELSE ' ' END ELSE '' END+ ISNULL(t2.CITY,'')+CASE WHEN T3.STATE_CODE!='' THEN CASE WHEN T2.CITY!='' THEN '/' ELSE ' - ' END ELSE '' END+ISNULL(t3.STATE_CODE,'')+CASE WHEN T2.ZIP_CODE!='' THEN CASE WHEN T3.STATE_CODE!='' THEN ' - ' ELSE ' ' END ELSE '' END + ISNULL(t2.ZIP_CODE,'')
-- Changed by Charles, T2.IS_PRIMARY_APPLICANT to T1.IS_PRIMARY_APPLICANT for Itrack 5655, 12-May-09. 
 T3.STATE_CODE STATE,T4.LOOKUP_VALUE_DESC TITLE,  MNT_ACTIVITY_MASTER.ACTIVITY_DESC POSITION,
 --Added by Lalit for master policy on, oct 20,2010
 T1.COMMISSION_PERCENT,T1.FEES_PERCENT,
 T1.PRO_LABORE_PERCENT
 
FROM                
 POL_APPLICANT_LIST T1   WITH(NOLOCK)             
LEFT OUTER JOIN                
 CLT_APPLICANT_LIST T2  WITH(NOLOCK)              
ON                
 T1.CUSTOMER_ID = T2.CUSTOMER_ID AND                
 T1.APPLICANT_ID = T2.APPLICANT_ID             
LEFT JOIN                
 MNT_COUNTRY_STATE_LIST T3         
ON                
 T2.STATE = T3.STATE_ID  and T2.COUNTRY=T3.COUNTRY_ID              
LEFT OUTER JOIN                
 MNT_LOOKUP_VALUES T4      
ON                
 T2.TITLE = T4.LOOKUP_UNIQUE_ID  
LEFT OUTER JOIN                
 MNT_LOOKUP_VALUES T5          
 ON                
 T2.POSITION = T5.LOOKUP_UNIQUE_ID  
LEFT OUTER JOIN 
MNT_ACTIVITY_MASTER
on
MNT_ACTIVITY_MASTER.ACTIVITY_ID=  T2.TITLE
WHERE                
 T1.CUSTOMER_ID = @CUSTOMERID AND T1.POLICY_ID =@POLICYID AND T1.POLICY_VERSION_ID = @POLICYVERSIONID AND                
 T2.IS_ACTIVE='Y'      
 
 
 select APPLICABLE_COMMISSION from MNT_LOB_MASTER where LOB_ID=@LOB_ID
                   
END


GO

