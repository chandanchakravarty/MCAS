IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrimaryApplicantInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrimaryApplicantInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetPrimaryApplicantInfo_Pol 6588,25,3 ,null  ,'POL'                                
Created By    : Praveen kasana                                
Modified Date   : 08-10-2006                                  
Purpose     : Name of the Primary Contact, Name of the 1st Co-applicant                   
Address Line 1, Address Line 2 of the Primary Contact City, State (abbreviation) & Zip code of the Primary Applicant                   
   
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
--drop proc  dbo.Proc_GetPrimaryApplicantInfo_Pol                          
CREATE  PROC [dbo].[Proc_GetPrimaryApplicantInfo_Pol]           
                                   
@CUSTOMERID  varchar(11),            
@POLICYID    int,                    
@POLICYVERSIONID   int,                                       
@USERID  varchar(20) = null,            
@CALLEDFROM  varchar(11)            
            
AS                                    
BEGIN                     
Declare @FIRST_NAME varchar(100)   
Declare @MIDDLE_NAME varchar(20)              
Declare @LAST_NAME varchar(20)              
Declare @ADDRESS1 nvarchar(70)           
Declare @ADDRESS2 nvarchar(70)          
Declare @CITY varchar(20)            
Declare @COUNTRY varchar(20)            
Declare @STATE varchar(20)            
Declare @ZIP_CODE varchar(20)      
DECLARE @STATE_CODE VARCHAR(5)
DECLARE @LAN_ID VARCHAR(5)
--Declare @AGENCY_DISPLAY_NAME varchar(20)            
--Declare @AGENCY_PHONE varchar(20)            
--declare @STATE_CODE VARCHAR(4)          
       
      
Declare @AGENCY_DISPLAY_NAME nvarchar(75)                    
Declare @AGENCY_PHONE varchar(20) --Agency Phone                   
Declare @AGENCY_ADD1 nvarchar(70)                    
Declare @AGENCY_ADD2 nvarchar(70)                    
Declare @AGENCY_CITY varchar(20)                    
Declare @AGENCY_STATE varchar(20)                    
Declare @AGENCY_ZIP varchar(20)            
Declare @APP_TERMS varchar(20)       
    
IF(@CALLEDFROM='QQ')            
 BEGIN            
   SELECT @FIRST_NAME=ISNULL(FIRST_NAME,''),@MIDDLE_NAME=ISNULL(MIDDLE_NAME,''),@LAST_NAME=isnull(LAST_NAME,''),@ADDRESS1=ISNULL(ADDRESS1,''),@ADDRESS2=ISNULL(ADDRESS2,''),@CITY=ISNULL(CITY,''),@COUNTRY=ISNULL(COUNTRY,'')                        
  ,@STATE= ISNULL(ST.STATE_NAME,''),@ZIP_CODE=ISNULL(ZIP_CODE,'') ,@STATE_CODE = ISNULL(ST.STATE_CODE,'')                 
  FROM CLT_APPLICANT_LIST AS APP with(nolock)       
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST ST with(nolock) ON                  
  ST.STATE_ID= APP.STATE                       
  WHERE CUSTOMER_ID=@CUSTOMERID  AND IS_PRIMARY_APPLICANT =1          
       
    --AGENCY INFO                
  SELECT           
  @AGENCY_ADD1 =  ISNULL(AL.AGENCY_ADD1,''),          
  @AGENCY_ADD2 = ISNULL(AL.AGENCY_ADD2,''),          
  @AGENCY_CITY = ISNULL(AL.AGENCY_CITY,''),      
  @AGENCY_STATE = ISNULL(SL.STATE_NAME,''),           
  @AGENCY_ZIP = ISNULL(AL.AGENCY_ZIP,''),        
  @AGENCY_DISPLAY_NAME = ISNULL(AL.AGENCY_DISPLAY_NAME,''),        
  @AGENCY_PHONE=ISNULL(AL.AGENCY_PHONE,'')        
  FROM MNT_AGENCY_LIST AL  with(nolock)          
 LEFT JOIN MNT_COUNTRY_STATE_LIST SL with(nolock) ON AL.AGENCY_STATE = SL.STATE_ID               
 INNER JOIN CLT_CUSTOMER_LIST CL with(nolock) ON CL.CUSTOMER_AGENCY_ID  = AL.AGENCY_ID      
 WHERE CL.CUSTOMER_ID = @CUSTOMERID                
      END            
ELSE            
   BEGIN            
  SELECT @FIRST_NAME=ISNULL(CCL.CUSTOMER_FIRST_NAME,''),@MIDDLE_NAME=ISNULL(CLT.MIDDLE_NAME,''),@LAST_NAME=ISNULL(CLT.LAST_NAME,''),@ADDRESS1=ISNULL(CLT.ADDRESS1,''),@ADDRESS2=ISNULL(CLT.ADDRESS2,''),@CITY=ISNULL(CLT.CITY,''),@COUNTRY=ISNULL(CLT.COUNTRY,'')       
  
         
  ,@STATE= ISNULL(ST.STATE_NAME,''),@ZIP_CODE=ISNULL(CLT.ZIP_CODE,''),@STATE_CODE = ISNULL(ST.STATE_CODE,'')       
  FROM CLT_APPLICANT_LIST AS CLT with(nolock)         
  INNER JOIN POL_APPLICANT_LIST POL  with(nolock)        
  ON POL.APPLICANT_ID = CLT.APPLICANT_ID          
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST ST  with(nolock) ON          
  ST.STATE_ID= CLT.STATE   
  LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL WITH(NOLOCK) on   --Added by Aditya for tfs bug # 1166
  CCL.CUSTOMER_ID = CLT.CUSTOMER_ID                
  WHERE POL.CUSTOMER_ID=@CUSTOMERID AND            
  POL.POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID             
  AND POL.IS_PRIMARY_APPLICANT =1          
  --AGENCY INFO      
  SELECT       
  @AGENCY_ADD1 =  ISNULL(AL.AGENCY_ADD1,''),          
  @AGENCY_ADD2 = ISNULL(AL.AGENCY_ADD2,''),          
  @AGENCY_CITY = ISNULL(AL.AGENCY_CITY,''),      
  @AGENCY_STATE = ISNULL(SL.STATE_NAME,''),           
  @AGENCY_ZIP = ISNULL(AL.AGENCY_ZIP,''),        
  @AGENCY_DISPLAY_NAME = ISNULL(AL.AGENCY_DISPLAY_NAME,''),        
  @AGENCY_PHONE=ISNULL(AL.AGENCY_PHONE,''),    
  @APP_TERMS = ISNULL(APP_TERMS,'')    
        
  FROM MNT_AGENCY_LIST AL  with(nolock)      
  LEFT JOIN MNT_COUNTRY_STATE_LIST SL  with(nolock) ON AL.AGENCY_STATE = SL.STATE_ID               
  INNER JOIN POL_CUSTOMER_POLICY_LIST POL with(nolock) ON POL.AGENCY_ID  = AL.AGENCY_ID      
  WHERE POL.CUSTOMER_ID=@CUSTOMERID AND  POL.POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID               
    END            
                
                
                  
--AGENCY INFO                  
/*SELECT      
@AGENCY_ADD1 =  AGENCY_ADD1,            
 @AGENCY_ADD2 = AGENCY_ADD2,            
 @AGENCY_CITY = AGENCY_CITY,            
 @AGENCY_STATE = SL.STATE_NAME,          
 @AGENCY_ZIP = AGENCY_ZIP,          
 @AGENCY_DISPLAY_NAME = AGENCY_DISPLAY_NAME,          
 @AGENCY_PHONE=AGENCY_PHONE       
        
FROM MNT_AGENCY_LIST AL      
INNER JOIN MNT_COUNTRY_STATE_LIST SL           
ON AL.AGENCY_STATE = SL.STATE_ID   
 */                
SELECT @LAN_ID = LANG_ID from MNT_USER_LIST 
WHERE  [USER_ID]=@USERID  
                 
select                   
@FIRST_NAME as FIRST_NAME,  
@MIDDLE_NAME AS MIDDLE_NAME,  
@LAST_NAME AS LAST_NAME,                  
@ADDRESS1 as ADDRESS1,                  
@ADDRESS2 as ADDRESS2,                
@COUNTRY as COUNTRY,                
@STATE as STATE ,             
@STATE_CODE AS STATE_CODE,             
@CITY as CITY,              
@ZIP_CODE as ZIP_CODE,                
--@AGENCY_DISPLAY_NAME as AGENCY_DISPLAY_NAME   ,      
--@AGENCY_PHONE as AGENCY_PHONE               
@AGENCY_ADD1 as AGENCY_ADD1,            
@AGENCY_ADD2 as AGENCY_ADD2,            
@AGENCY_CITY as AGENCY_CITY,            
@AGENCY_STATE as AGENCY_STATE,            
@AGENCY_ZIP as AGENCY_ZIP,            
@AGENCY_DISPLAY_NAME as AGENCY_DISPLAY_NAME,              
@AGENCY_PHONE as AGENCY_PHONE,    
@APP_TERMS as APP_TERMS ,                     
@LAN_ID   as LANG_ID              
                  
                    
                           
                                   
END  
  
GO

