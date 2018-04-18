IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrimaryApplicantInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrimaryApplicantInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name       : dbo.Proc_GetPrimaryApplicantInfo 936,70                                         
Created By      : Praveen kasana                                      
Modified Date   : 08-10-2006                                        
Purpose     : Name of the Primary Contact, Name of the 1st Co-applicant                         
Address Line 1, Address Line 2 of the Primary Contact City, State (abbreviation) & Zip code of the Primary Applicant                         
                        
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------                                        
                  
--Proc_GetPrimaryApplicantInfo 932,NULL ,NULL ,NULL,'QQ'                  
  --drop PROC Dbo.Proc_GetPrimaryApplicantInfo   
  select PHONE,BUSINESS_PHONE,* from CLT_APPLICANT_LIST WHERE CUSTOMER_ID=2101 AND IS_PRIMARY_APPLICANT =1  
  Proc_GetPrimaryApplicantInfo 2101,45,1,398,'APP'  
  */                                 
CREATE  PROC [dbo].[Proc_GetPrimaryApplicantInfo]                
                                         
@CUSTOMERID  varchar(11),                  
@APPID  INT = NULL,                                        
@APPVERSIONID     INT = NULL,                                        
@USERID  varchar(20) = NULL,                  
@CALLEDFROM  varchar(11)                  
                  
AS                                          
BEGIN                           
Declare @FIRST_NAME varchar(20)               
Declare @MIDDLE_NAME varchar(20)              
Declare @LAST_NAME varchar(20)                
Declare @ADDRESS1 nvarchar(70)                  
Declare @ADDRESS2 nvarchar(70)                 
Declare @CITY varchar(20)                  
Declare @COUNTRY varchar(20)                  
Declare @STATE varchar(20)                  
Declare @ZIP_CODE varchar(20)              
DECLARE @STATE_CODE VARCHAR(5)                  
----AGENCY VARIABLE-----              
Declare @AGENCY_DISPLAY_NAME nvarchar(75)                  
Declare @AGENCY_PHONE varchar(20) --Agency Phone                 
Declare @AGENCY_ADD1 nvarchar(70)                  
Declare @AGENCY_ADD2 nvarchar(70)               
Declare @AGENCY_CITY varchar(20)                  
Declare @AGENCY_STATE varchar(20)                  
Declare @AGENCY_ZIP varchar(20)          
Declare @APP_TERMS varchar(20)     
Declare @PHONE_NO varchar(20)      
          
          
          
IF(@CALLEDFROM='QQ')                  
BEGIN        
  SELECT @PHONE_NO =  
  CASE CLT_APPLICANT_LIST.APPLICANT_TYPE  
   WHEN '11109'  
    THEN  
   CLT_APPLICANT_LIST.BUSINESS_PHONE     
   WHEN '11110'  
  THEN  
    CLT_APPLICANT_LIST.PHONE  
  END  
    
  FROM CLT_APPLICANT_LIST WITH (NOLOCK)    
  WHERE CUSTOMER_ID=@CUSTOMERID  AND IS_PRIMARY_APPLICANT =1  
  
  
  SELECT     
 @FIRST_NAME = ISNULL(FIRST_NAME,''),     
 @MIDDLE_NAME = ISNULL(MIDDLE_NAME,''),    
 @LAST_NAME=ISNULL(LAST_NAME,''),    
 @ADDRESS1=ISNULL(ADDRESS1,''),    
 @ADDRESS2=ISNULL(ADDRESS2,''),    
 @CITY=ISNULL(CITY,''),    
 @COUNTRY=ISNULL(COUNTRY,''),                        
 @STATE= ISNULL(ST.STATE_NAME,''),    
 @ZIP_CODE=ISNULL(ZIP_CODE,'') ,    
 @STATE_CODE = ISNULL(ST.STATE_CODE,'')               
  FROM CLT_APPLICANT_LIST AS APP   WITH (NOLOCK)    
  INNER JOIN MNT_COUNTRY_STATE_LIST ST WITH (NOLOCK) ON                  
  ST.STATE_ID= APP.STATE                       
  WHERE CUSTOMER_ID=@CUSTOMERID  AND IS_PRIMARY_APPLICANT =1          
       
  --AGENCY INFO                
  SELECT           
  @AGENCY_ADD1 =  ISNULL(AL.AGENCY_ADD1,''),          
  @AGENCY_ADD2 = ISNULL(AL.AGENCY_ADD2,''),          
  @AGENCY_CITY = ISNULL(AL.AGENCY_CITY,''),      
  @AGENCY_STATE = ISNULL(SL.STATE_NAME,''),           
  @AGENCY_ZIP = AL.AGENCY_ZIP,        
  @AGENCY_DISPLAY_NAME = ISNULL(AL.AGENCY_DISPLAY_NAME,''),        
  @AGENCY_PHONE=ISNULL(AL.AGENCY_PHONE,'')    
   
  FROM MNT_AGENCY_LIST AL  WITH (NOLOCK)       
 LEFT JOIN MNT_COUNTRY_STATE_LIST SL   WITH (NOLOCK) ON AL.AGENCY_STATE = SL.STATE_ID               
 INNER JOIN CLT_CUSTOMER_LIST CL  WITH (NOLOCK) ON CL.CUSTOMER_AGENCY_ID  = AL.AGENCY_ID   
   
      
 WHERE CL.CUSTOMER_ID = @CUSTOMERID      
      
            
END                  
ELSE               
BEGIN      
  
  SELECT @PHONE_NO =  
  CASE CLT_APPLICANT_LIST.APPLICANT_TYPE  
   WHEN '11109'  
    THEN  
   CLT_APPLICANT_LIST.BUSINESS_PHONE     
   WHEN '11110'  
  THEN  
    CLT_APPLICANT_LIST.PHONE  
  END  
    
  FROM CLT_APPLICANT_LIST WITH (NOLOCK)    
  WHERE CUSTOMER_ID=@CUSTOMERID  AND IS_PRIMARY_APPLICANT =1  
                
  SELECT @FIRST_NAME=ISNULL(CLT.FIRST_NAME,''),@MIDDLE_NAME=ISNULL(CLT.MIDDLE_NAME,''),@LAST_NAME=ISNULL(CLT.LAST_NAME,''),
  @ADDRESS1=ISNULL(CLT.ADDRESS1,''),@ADDRESS2=ISNULL(CLT.ADDRESS2,''),@CITY=ISNULL(CLT.CITY,''),@COUNTRY=ISNULL(CLT.COUNTRY ,'')      
  ,@STATE= ISNULL(ST.STATE_NAME,''),@ZIP_CODE=ISNULL(CLT.ZIP_CODE,''),@STATE_CODE = ISNULL(ST.STATE_CODE,'')       
  FROM CLT_APPLICANT_LIST AS CLT with(nolock)                
  INNER JOIN POL_APPLICANT_LIST APP with(nolock)                 
  ON APP.APPLICANT_ID = CLT.APPLICANT_ID                  
  INNER JOIN MNT_COUNTRY_STATE_LIST ST  WITH (NOLOCK) ON                  
  ST.STATE_ID= CLT.STATE                        
  WHERE APP.CUSTOMER_ID=@CUSTOMERID AND APP.POLICY_ID = @APPID AND APP.POLICY_VERSION_ID = @APPVERSIONID           
  AND APP.IS_PRIMARY_APPLICANT =1          
 --AGENCY INFO      
  SELECT       
  @AGENCY_ADD1 =  ISNULL(AL.AGENCY_ADD1,''),          
  @AGENCY_ADD2 = ISNULL(AL.AGENCY_ADD2,''),          
  @AGENCY_CITY = ISNULL(AL.AGENCY_CITY,''),      
  @AGENCY_STATE = ISNULL(SL.STATE_NAME,''),           
  @AGENCY_ZIP = ISNULL(AL.AGENCY_ZIP,''),        
  @AGENCY_DISPLAY_NAME = ISNULL(AL.AGENCY_DISPLAY_NAME,''),        
  @AGENCY_PHONE = ISNULL(AL.AGENCY_PHONE,''),    
  @APP_TERMS = ISNULL(APP_TERMS,'')    
  FROM MNT_AGENCY_LIST AL  WITH (NOLOCK)        
  LEFT JOIN MNT_COUNTRY_STATE_LIST SL  WITH (NOLOCK)  ON AL.AGENCY_STATE = SL.STATE_ID               
  INNER JOIN APP_LIST APP  WITH (NOLOCK) ON APP.APP_AGENCY_ID  = AL.AGENCY_ID      
  WHERE APP.CUSTOMER_ID=@CUSTOMERID AND APP.APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID             
              
END                  
                      
    
---Retrieve data                        
SELECT                         
@FIRST_NAME as FIRST_NAME,              
@MIDDLE_NAME as MIDDLE_NAME,              
@LAST_NAME as LAST_NAME,                      
@ADDRESS1 as ADDRESS1,                        
@ADDRESS2 as ADDRESS2,                      
@COUNTRY as COUNTRY,                      
@STATE as STATE ,                      
@CITY as CITY,              
@ZIP_CODE as ZIP_CODE,                     
@STATE_CODE as STATE_CODE,            
--AGENCY INFO                 
@AGENCY_ADD1 as AGENCY_ADD1,          
@AGENCY_ADD2 as AGENCY_ADD2,          
@AGENCY_CITY as AGENCY_CITY,          
@AGENCY_STATE as AGENCY_STATE,          
@AGENCY_ZIP as AGENCY_ZIP,          
@AGENCY_DISPLAY_NAME as AGENCY_DISPLAY_NAME,            
@AGENCY_PHONE as AGENCY_PHONE,                      
@APP_TERMS as APP_TERMS,                  
@PHONE_NO  as APLICANT_PHONE_NO  
                        
                          
                                 
                                         
END                
        
        
                              
                            
                          
                        
                      
                    
                  
                  
                
              
              
            
          
        
      
      
      
      
      
      
      
      
      
    
    
    
    
    
    
GO

