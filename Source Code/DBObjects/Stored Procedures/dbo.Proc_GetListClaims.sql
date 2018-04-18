IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetListClaims]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetListClaims]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROC dbo.Proc_GetListClaims 958,'claim number','fsd',1         
CREATE PROC dbo.Proc_GetListClaims                                                                                               
 @CLAIM_ID INT   ,        
 @SEARCHOPTION VARCHAR(10)=NULL,        
 @SEARCHTEXT VARCHAR(10)=NULL,
 @LANG_ID INT =1        
AS                                                                            
BEGIN           
 declare @SqlQuery varchar (8000)                                                                         
        
select @SqlQuery = '         
 SELECT top 100                
 (CAST(ISNULL(CLAIM_NUMBER,'''') AS VARCHAR) + ''^'' + CAST(ISNULL(CUSTOMER_ID,0) AS VARCHAR) + ''^'' +                   
 CAST(ISNULL(POLICY_ID,0) AS VARCHAR) + ''^'' + CAST(ISNULL(POLICY_VERSION_ID,0) AS VARCHAR) + ''^'' +                   
 CAST(ISNULL(CLAIM_ID,0) AS VARCHAR) + ''^'' + CAST(ISNULL(CLM_CLAIM_INFO.LOB_ID,0) AS VARCHAR) + ''^'' +                   
 CAST(ISNULL(HOMEOWNER,0) AS VARCHAR) + ''^'' + CAST(ISNULL(RECR_VEH,0) AS VARCHAR) + ''^'' +                   
 CAST(ISNULL(IN_MARINE,0) AS VARCHAR) + ''^'' + LTRIM(RTRIM(CAST(CONVERT(CHAR,LOSS_DATE,101) AS VARCHAR)))) AS CLAIM_CONCAT_STRING,                  
  CLAIM_ID,                                                            
  CLAIM_NUMBER,                                                  
  CONVERT(CHAR,LOSS_DATE,101) LOSS_DATE,                                                          
  LOSS_DATE AS LOSS_TIME,               
  CLM_ADJUSTER.ADJUSTER_NAME,             
  ISNULL(MNT_LOOKUP_VALUES_MULTILINGUAL.LOOKUP_VALUE_DESC,MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC) AS CLAIM_STATUS_DESC,              
  REPORTED_BY,                                                        
  CATASTROPHE_EVENT_CODE,            
  CLM_CATASTROPHE_EVENT.DESCRIPTION AS CATASTROPHE_DESC,                                                        
  CLAIMANT_INSURED,                                                        
  INSURED_RELATIONSHIP,                                                        
  CLAIMANT_NAME,                                                        
  COUNTRY,                                                        
  ZIP,                                                        
  ADDRESS1,                                                        
  ADDRESS2,                                                        
  CITY,                                                        
  HOME_PHONE,                                                        
  WORK_PHONE,                                                        
  MOBILE_PHONE,                                                        
  WHERE_CONTACT,                                                        
  WHEN_CONTACT,                                                        
 CONVERT(CHAR,DIARY_DATE,101)   DIARY_DATE,                                                            
  CLAIM_STATUS,                                                        
  OUTSTANDING_RESERVE,                                                        
  RESINSURANCE_RESERVE,                        
  PAID_LOSS,                                                        
  PAID_EXPENSE,                                        
  RECOVERIES,          
  CLAIM_DESCRIPTION,                                                        
 SUB_ADJUSTER_CONTACT,                                                  
 EXTENSION,                                            
 ISNULL(DUMMY_POLICY_ID,0) AS DUMMY_POLICY_ID,                                 
 LOSS_TIME_AM_PM,                                    
 LITIGATION_FILE,                                  
 RECOVERY,                                  
 RECOVERY_OUTSTANDING,                            
 STATE,                            
 CLAIMANT_PARTY,                          
 LINKED_TO_CLAIM,                          
 ADD_FAULT,                          
 TOTAL_LOSS,                          
 NOTIFY_REINSURER,                        
REPORTED_TO,                  
CONVERT(VARCHAR(10),FIRST_NOTICE_OF_LOSS,101) AS  FIRST_NOTICE_OF_LOSS                        
FROM                               
  CLM_CLAIM_INFO               
LEFT OUTER JOIN            
  CLM_ADJUSTER             
ON            
CLM_CLAIM_INFO.ADJUSTER_ID = CLM_ADJUSTER.ADJUSTER_ID        
LEFT OUTER JOIN            
 MNT_LOOKUP_VALUES             
ON CLM_CLAIM_INFO.CLAIM_STATUS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID  
LEFT OUTER JOIN 
 MNT_LOOKUP_VALUES_MULTILINGUAL ON CLM_CLAIM_INFO.CLAIM_STATUS=MNT_LOOKUP_VALUES_MULTILINGUAL.LOOKUP_UNIQUE_ID  AND LANG_ID='+convert(varchar(10),@LANG_ID)+'
LEFT OUTER JOIN            
 CLM_CATASTROPHE_EVENT            
ON             
 CLM_CATASTROPHE_EVENT.CATASTROPHE_EVENT_ID = CLM_CLAIM_INFO.CATASTROPHE_EVENT_CODE   '        
        
Declare @orderclause varchar(100)        
select @orderclause =  ' ORDER BY CLAIM_ID DESC  '        
        
Declare @WhereClause varchar(8000)        
select @WhereClause = ' WHERE  CLAIM_ID <> ' +  cast(@CLAIM_ID as varchar(100))  +  '  '        
        
if (@SearchOption = 'IN')        
 select @WhereClause = @WhereClause + ' AND CLAIMANT_NAME like ''%' + @searchtext + '%'' '   --Insured Name        
        
if (@SearchOption = 'ST')        
 select @WhereClause = @WhereClause + ' AND MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC like ''%' + @searchtext + '%'' '   --Status        
        
if (@SearchOption = 'CN')        
 select @WhereClause = @WhereClause + ' AND CLAIM_NUMBER like ''%' + @searchtext + '%'' '   --Claim Number        
        
        
exec (@SqlQuery + ' ' + @WhereClause + '  ' + @orderclause)        
        
END 

GO

