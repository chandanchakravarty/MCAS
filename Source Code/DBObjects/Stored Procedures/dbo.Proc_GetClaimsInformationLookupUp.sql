IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimsInformationLookupUp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimsInformationLookupUp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetClaimsInformationLookupUp                                                  
Created by      : Sumit Chhabra                                                      
Date            : 27/04/2006                                                        
Purpose         : Get Lookup Data for various dropdowns at use in Claims Information screen                                  
Created by      : Sumit Chhabra                                                       
Revison History :                                                        
Used In        : Ebix Advantage Claim Module                                                        
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
--drop PROC dbo.Proc_GetClaimsInformationLookupUp                                  
CREATE PROC [dbo].[Proc_GetClaimsInformationLookupUp]                                 
@CUSTOMER_ID int,                    
@LOSS_DATE datetime,            
@POLICY_ID int = null,            
@POLICY_VERSION_ID smallint = null  ,  
@LOB_ID int = null            
          
AS                                                        
BEGIN                                                         
            
--Get the Adjuster Code Values                                  
 --SELECT ADJUSTER_NAME, CAST(ADJUSTER_ID AS VARCHAR)+ '^'+ CAST(ISNULL(MUL.ADJUSTER_CODE,0)AS VARCHAR) AS ADJUSTER_ID_CODE           
 --FROM CLM_ADJUSTER CA LEFT JOIN MNT_USER_LIST MUL ON MUL.USER_ID = CA.USER_ID WHERE CA.IS_ACTIVE='Y'     
--Done for Itrack Issue 6823 on 15 Dec 09    
 SELECT ADJUSTER_NAME, CAST(CA.ADJUSTER_ID AS VARCHAR)+ '^'+ CAST(ISNULL(CA.ADJUSTER_CODE,0)AS VARCHAR) AS ADJUSTER_ID_CODE           
 FROM CLM_ADJUSTER CA LEFT OUTER JOIN  
 MNT_USER_LIST MUL ON MUL.USER_ID = CA.USER_ID LEFT OUTER JOIN  
 CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK) ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID AND CA.IS_ACTIVE='Y'
 JOIN
(SELECT ADJUSTER_ID, MAX(EFFECTIVE_DATE) EFFECTIVE_DATE FROM CLM_ADJUSTER_AUTHORITY 
 WHERE LOB_ID = @LOB_ID AND CLM_ADJUSTER_AUTHORITY.IS_ACTIVE = 'Y' GROUP BY ADJUSTER_ID) temp1
 ON (temp1.ADJUSTER_ID = CAA.ADJUSTER_ID AND temp1.EFFECTIVE_DATE = CAA.EFFECTIVE_DATE)                 
 WHERE CA.DISPLAY_ON_CLAIM=10963 AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y'                              
                                  
--Get the Catastrophe Event Data                                  
--  SELECT CATASTROPHE_EVENT_ID,DESCRIPTION FROM  CLM_CATASTROPHE_EVENT                                  
-- exec Proc_GetClaimCatastropheCode @LOSS_DATE                    
                                  
--Get the Country Data                                  
 SELECT COUNTRY_ID,COUNTRY_NAME FROM MNT_COUNTRY_LIST                                  
                                  
--Get Claim Status Code values                                  
exec Proc_GetLookupValues 'CLMST',null,1                 
--Get Claim Status Under Code values                                  
--exec Proc_GetLookupValues 'CLMST',null,1               
--Select users who have checked the box Recieve Pink Slip Notification              
/*            
Commenting the following code as it will be called from different proc altogether            
if @CLAIM_ID IS NOT NULL AND @CLAIM_ID<>0--When claim has been added, fetch selected users at the top            
BEGIN            
 SELECT @RECIEVE_PINK_SLIP_USERS_LIST=ISNULL(RECIEVE_PINK_SLIP_USERS_LIST,0) FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID            
 set @STATEMENT='select USER_ID,(ISNULL(USER_FNAME,'''') + '' '' +  ISNULL(USER_LNAME,'''') + '' ( '' + ISNULL(USER_LOGIN_ID,'''') + '' )'') AS USER_NAME,1 as tab_num From mnt_user_list where USER_ID in (' + @RECIEVE_PINK_SLIP_USERS_LIST + ')            
     union            
     select USER_ID,(ISNULL(USER_FNAME,'''') + '' '' +  ISNULL(USER_LNAME,'''') + '' ( '' + ISNULL(USER_LOGIN_ID,'''') + '' )'') AS USER_NAME,2 as tab_num From mnt_user_list where PINK_SLIP_NOTIFY=''Y'' and USER_ID not in (' + @RECIEVE_PINK_SLIP_USERS_LIS
  
    
    
    
    
      
      
        
          
            
            
            
            
T + ')            
     order by tab_num '             
 exec (@STATEMENT)            
END            
ELSE            
 select USER_ID,(ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') + ' ( ' + ISNULL(USER_LOGIN_ID,'') + ' )') AS USER_NAME From mnt_user_list where isnull(PINK_SLIP_NOTIFY,'N')='Y'                                        
*/                          
--Following code is commented as it will be used now                                
--Get Claim Relationship to Insured Values --(type_id = 10 = Relationship to the Insured)                            
--  SELECT DETAIL_TYPE_ID,DETAIL_TYPE_DESCRIPTION FROM  CLM_TYPE_DETAIL WHERE TYPE_ID=10                             
                          
--Following recordset will be fetched from the page itself..no need to fetch this data from here now                  
/*--Get Customer Details for the claimant/insured drop-down                          
SELECT  'Yes' as YES_OPTION,isnull(CUSTOMER_FIRST_NAME,'') + ' ' +  ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') + '^' +                            
 ISNULL(CUSTOMER_COUNTRY,'')  + '^' +  ISNULL(CUSTOMER_ZIP,'')  + '^' +   ISNULL(CUSTOMER_ADDRESS1,'')  + '^' +                            
 ISNULL(CUSTOMER_ADDRESS2,'')  + '^' +  ISNULL(CUSTOMER_CITY,'')  + '^' +  ISNULL(CUSTOMER_HOME_PHONE,'')  + '^' +                            
 ISNULL(CUSTOMER_BUSINESS_PHONE,'')  + '^' +  ISNULL(CUSTOMER_MOBILE,'') + '^' + ISNULL(CUSTOMER_STATE,'')                    
 AS CUSTOMER_DATA                          
FROM  CLT_CUSTOMER_LIST                                                                  
WHERE CUSTOMER_ID  =  @CUSTOMER_ID                           
*/                                         
           
--SELECT ISNULL(AGENCY_DISPLAY_NAME,'') AS AGENCY_DISPLAY_NAME FROM POL_CUSTOMER_POLICY_LIST POLICY LEFT OUTER JOIN MNT_AGENCY_LIST AGENCY ON             
-- POLICY.AGENCY_ID = AGENCY.AGENCY_ID            
--WHERE             
-- POLICY.CUSTOMER_ID=@CUSTOMER_ID AND POLICY.POLICY_ID=@POLICY_ID AND POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID       
--Retrieve the Agency Display Name for the policy               
--Added by Santosh Kumar Gautam on 17 Nov 2010 (Old value Above SQL statement)  
 SELECT ISNULL(AGENCY_DISPLAY_NAME,'') AS AGENCY_DISPLAY_NAME   
 FROM POL_REMUNERATION PR LEFT OUTER JOIN MNT_AGENCY_LIST AGENCY ON PR.BROKER_ID = AGENCY.AGENCY_ID          
 WHERE  ( PR.LEADER= 10963 AND --HERE 10963 MEANS YES i.e, SELECT LEADER BORKER NAME  
          PR.CUSTOMER_ID=@CUSTOMER_ID AND   
          PR.POLICY_ID=@POLICY_ID AND   
          PR.POLICY_VERSION_ID=@POLICY_VERSION_ID   
        )               
            
--Get Pink Slip Types values                                  
--Commented the following lookup as it will be called from different proc            
--exec Proc_GetLookupValues 'PSTYP',null,1               
            
            
--Get Claimant Party Data from lookup            
exec Proc_GetLookupValues 'CLM_PT',null,1                 
              
exec Proc_GetLookupValues 'CLMSTU',null,1       
            
END             
            
      
  
GO

