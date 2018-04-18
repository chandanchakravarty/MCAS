IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetUser                
Created by      : Gaurav                        
Date            : 5/31/2005                        
Purpose       : Return the Query                       
Revison History :                        
Used In   : Wolverine                        
                
Modiifed By : Anurag Verma                
Modified On : 13/10/2005                
Purpose  : Fetching Agency Details                
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                 
-- Proc_GetUser 196          
-- drop proc dbo.Proc_GetUser                              
CREATE  PROC [dbo].[Proc_GetUser]                        
(                        
   @UserId  int                    
)                        
AS                        
BEGIN                        
Select distinct usrList.USER_ID ,usrList.USER_LOGIN_ID,usrList.USER_TYPE_ID,          
dbo.fun_DecriptText(usrList.USER_PWD) as USER_PWD,          
usrList.SUB_CODE,                
dbo.fun_DecriptText(usrList.USER_PWD) as USER_CONFIRM_PWD,          
usrList.USER_TITLE,usrList.USER_FNAME,usrList.USER_LNAME,                
usrList.USER_INITIALS,usrList.USER_ADD1,usrList.USER_ADD2,usrList.USER_CITY,usrList.USER_STATE,                
usrList.USER_ZIP,usrList.USER_PHONE,usrList.USER_EXT,usrList.USER_FAX,usrList.USER_MOBILE,usrList.USER_EMAIL,                
usrList.USER_SPR,usrList.USER_MGR_ID,Convert(varchar(5),usrList.USER_DEF_DIV_ID)+'_'+Convert(varchar(5),                
usrList.USER_DEF_DEPT_ID)+'_'+Convert(varchar(5),usrList.USER_DEF_PC_ID) as USER_DEF_PC_ID,          
usrList.IS_ACTIVE,
usrList.REGIONAL_IDENTIFICATION, 
 usrList.CPF,
 usrList.REG_ID_ISSUE_DATE,
  usrList.ACTIVITY,
  usrList.REG_ID_ISSUE,                
usrList.USER_TIME_ZONE,usrList.USER_COLOR_SCHEME,usrList.USER_SYSTEM_ID,usrList.COUNTRY as USER_COUNTRY                 
,MAL.Agency_add1,                
MAL.AGENCY_ADD2,MAL.AGENCY_CITY,MAL.AGENCY_STATE,MAL.AGENCY_ZIP,MAL.AGENCY_COUNTRY,MAL.AGENCY_PHONE,                
usrList.SSN_NO,
--Convert(varchar,usrList.DATE_OF_BIRTH,101) 
DATE_OF_BIRTH,usrList.DRIVER_DRIV_TYPE AS DRIVER_LIC_NO,
--Convert(varchar,usrList.DATE_EXPIRY,101)
 DATE_EXPIRY,usrList.LICENSE_STATUS,                
usrList.NON_RESI_LICENSE_STATE,usrList.NON_RESI_LICENSE_NO,usrList.LIC_BRICS_USER  ,usrList.NON_RESI_LICENSE_STATE2,usrList.NON_RESI_LICENSE_NO2,            
usrList.PINK_SLIP_NOTIFY as PINK_SLIP_NOTIFY,usrList.USER_NOTES AS USER_NOTES,          
          
Convert(varchar(5),MNT_DIV_DEPT_MAPPING.DIV_ID)+'_'+Convert(varchar(5),                
MNT_DEPT_LIST.DEPT_NAME)+'_'+Convert(varchar(5),MNT_DEPT_PC_MAPPING.PC_ID) as DEFAULT_HEIRARCHY,          
ISNULL(ADJUSTER_CODE,'') AS ADJUSTER_CODE,        
--Added by Sibin on 10 Dec 08 for itrack Issue 4994        
usrList.CHANGE_PWD_NEXT_LOGIN,usrList.USER_LOCKED,  
--Added by Sibin on 15 Jan 09 for Itrack Issue 4173   
--Convert(varchar,usrList.NON_RESI_LICENSE_EXP_DATE,101)
 NON_RESI_LICENSE_EXP_DATE,usrList.NON_RESI_LICENSE_STATUS,  
 -- Convert(varchar,usrList.NON_RESI_LICENSE_EXP_DATE2,101) 
 NON_RESI_LICENSE_EXP_DATE2,usrList.NON_RESI_LICENSE_STATUS2    
          
From MNT_USER_LIST usrList                
INNER JOIN MNT_AGENCY_LIST MAL ON MAL.AGENCY_CODE=USRLIST.USER_SYSTEM_ID             
             
LEFT JOIN MNT_DIV_DEPT_MAPPING ON MNT_DIV_DEPT_MAPPING.DIV_ID = usrList.USER_DEF_DIV_ID           
LEFT JOIN MNT_DEPT_LIST ON MNT_DEPT_LIST.DEPT_ID = usrList.USER_DEF_DEPT_ID          
LEFT JOIN MNT_DEPT_PC_MAPPING ON MNT_DEPT_PC_MAPPING.PC_ID = usrList.USER_DEF_PC_ID          
          
where usrList.USER_ID=@UserId              
          
END 
GO

