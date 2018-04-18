IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUser]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- drop proc dbo.Proc_UpdateUser                  
CREATE  PROC [dbo].[Proc_UpdateUser]                                
(                                
@User_Id  int  ,                                
@User_Login_Id  nvarchar(10),                                
@Sub_Code  varchar(10) = null,                                
@User_Pwd  nvarchar(100),                                
@User_Title  nvarchar(35),                                
@User_Fname  nvarchar(35),                                
@User_Lname  nvarchar(35),                                
@User_Initials  nvarchar(3),                                
@User_Add1  nvarchar(70),                                
@User_Add2  nvarchar(70),                                
@User_City  nvarchar(40),                                
@User_State  nvarchar(5),                                
@User_Zip  nvarchar(11),                                
@User_Phone  nvarchar(20),                                
@User_Ext  nvarchar(10),                                
@User_Fax  nvarchar(20),                                
@User_Mobile  nvarchar(20),                                
@User_Email  nvarchar(50),                                
@Is_Active  nchar(1),                                
--@User_Inactive_DateTime  DateTime,                                
--@Created_By  int,                                
--@Created_DateTime DateTime,                                
@Modified_By  int,                                
@Last_Updated_DateTime DateTime,                                
@User_System_Id         varchar(8),                                
@User_Image_Folder varchar(15),                                
--@User_Color_Scheme varchar(15),                                
                                
@User_Mgr_Id  int,                                
@User_Def_Div_Id smallint,                                
@User_Def_Dept_Id smallint,                                
@User_Def_Pc_Id smallint,                                
@User_Time_Zone varchar(5),                         
@User_Notes varchar(120), ----**********                              
@User_Supervisor nchar(1),                                
@User_Type_Id  smallint,                                
@Country  nvarchar(5),                                
@SSN_NO   nvarchar(100),                                
@DATE_OF_BIRTH  DateTime,                                
@DRIVER_LIC_NO nvarchar(60),                                
@DATE_EXPIRY  Datetime,                                
@LICENSE_STATUS  nvarchar(10),          
                                     
@NON_RESI_LICENSE_STATE nvarchar(5),                                
@NON_RESI_LICENSE_no    nvarchar(60),                           
                          
                          
@NON_RESI_LICENSE_STATE2 nvarchar(5),                                
@NON_RESI_LICENSE_no2    nvarchar(60),                      
                             
@LIC_BRICS_USER  smallint,                  
@PINK_SLIP_NOTIFY nchar(1),                  
@ADJUSTER_CODE varchar(10)=null,            
--Added by Sibin on 9 Dec 08 for Itrack Issue 4994            
 @CHANGE_PWD_NEXT_LOGIN int,             
 @USER_LOCKED nchar(1),        
        
--Added by Sibin for Itrack Issue 4173 on 15 Jan 09          
 @NON_RESI_LICENSE_EXP_DATE DateTime,          
 @NON_RESI_LICENSE_EXP_DATE2 DateTime,          
 @NON_RESI_LICENSE_STATUS nvarchar(20),          
 @NON_RESI_LICENSE_STATUS2 nvarchar(20),
 @REGIONAL_IDENTIFICATION NVARCHAR(40),
 @CPF NVARCHAR(40),
 @REG_ID_ISSUE_DATE DATETIME,
 @ACTIVITY INT,
 @REG_ID_ISSUE NVARCHAR(40)             
--Added till here                                 
)                               
AS                                
BEGIN                                
                            
declare @AGENCY_LIC_NUM int                            
declare @USER_LIC_NUM int                           
DECLARE @ADJUSTER_CODE_COUNT INT                   
declare @LIC_BRICS_USER_OLD  SMALLINT            
                            
If Exists(SELECT User_Login_Id                                
 FROM MNT_USER_LIST                                
 WHERE User_Login_Id = @User_Login_Id AND User_Id <> @User_Id and User_Login_Id<>null )                                
 BEGIN                      
  /*Code already exists*/                                
  return 0                                
 END                                
 ELSE                                
BEGIN                       
/*Checking for duplicate sub code*/                                
 If Exists(SELECT SUB_CODE                                
 FROM MNT_USER_LIST    
--Cast Added by Charles, on 20-Jul-09 to check for 01 & 1, for Itrack 6128                                                             
 WHERE (CAST(CAST(SUB_CODE AS INT) AS VARCHAR(10)) = CAST(CAST(@SUB_CODE AS INT) AS VARCHAR(10))   
AND User_System_Id = @User_System_Id)                                 
  AND  User_Id <> @User_Id )                                
 BEGIN                        
 /*Sub Code already exists*/            
  return -2                                
 END                 
                
SELECT @LIC_BRICS_USER_OLD = ISNULL(LIC_BRICS_USER,0) FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = @User_System_Id                
AND User_Id = @User_Id                
                               
IF (@LIC_BRICS_USER <> @LIC_BRICS_USER_OLD AND @LIC_BRICS_USER = 10963)                  
BEGIN                   
  ---Check that the number of licensed users do not exceed the number of licences assigned to current agency                              
 SELECT @AGENCY_LIC_NUM=ISNULL(AGENCY_LIC_NUM,0) FROM MNT_AGENCY_LIST WHERE AGENCY_CODE=@User_System_Id                              
 SELECT @USER_LIC_NUM=ISNULL(COUNT(USER_ID),0)+1 FROM MNT_USER_LIST                               
  WHERE USER_SYSTEM_ID=@User_System_Id AND LIC_BRICS_USER=10963 AND IS_ACTIVE='Y'                              
                  
 SELECT @ADJUSTER_CODE_COUNT=COUNT(USER_ID) FROM MNT_USER_LIST WHERE USER_TYPE_ID=46 AND ADJUSTER_CODE=@ADJUSTER_CODE                  
                    
 IF(@USER_LIC_NUM > @AGENCY_LIC_NUM)                     
 begin                  
  RETURN -3                              
 end                  
                  
/* IF(ISNULL(@ADJUSTER_CODE_COUNT,0)>0)                  
 begin                    
 SELECT @User_Id = -4                                  
 return                           
 end                  
 */                  
 END                
  
  if len(@SUB_CODE)=1  
 set @SUB_CODE = '00' + @SUB_CODE  
else if len(@SUB_CODE)=2  
 set @SUB_CODE = '0' + @SUB_CODE  
        
                           
 UPDATE MNT_USER_LIST                                
 SET                                 
  User_Login_Id  =@User_Login_Id,                                
  User_Pwd  = convert(nvarchar,dbo.fun_EncriptText(@User_Pwd)),                                
  User_Title  =@User_Title,                                
  User_Fname  =@User_Fname,                                
  User_Lname  =@User_Lname,                                
  User_Initials  =@User_Initials,                                
  User_Add1  =@User_Add1,                                
  User_Add2  =@User_Add2,                                
  User_State  =@User_State,                                
  User_Zip  =@User_Zip,                                
 User_Phone  =@User_Phone,                      
  User_Ext  =@User_Ext,                                
  User_Fax  =@User_Fax,                                
  User_Mobile  =@User_Mobile,                                
  Is_Active  =@Is_Active,                                
                                
  Modified_By  =@Modified_By,                                
  Last_Updated_DateTime =@Last_Updated_DateTime,                                
  User_System_Id  =@User_System_Id,                                
  --User_Image_Folder =@User_Image_Folder,                                
  --User_Color_Scheme =@User_Color_Scheme,                                
  User_Mgr_Id  =@User_Mgr_Id,                                
  User_Def_Div_Id  =@User_Def_Div_Id,                                
  User_Def_Dept_Id =@User_Def_Dept_Id,                                
  User_Def_Pc_Id  =@User_Def_Pc_Id,           
  User_Time_Zone  =@User_Time_Zone,                           
  User_Notes  =@User_Notes, ---***********                              
  User_Spr  =@User_Supervisor,                                
  User_Type_Id  =@User_Type_Id,                                
  Country   =@Country,                                
  Sub_Code  =@Sub_Code,                                
  user_city  =@User_City,                                
  user_email  =@User_Email,          
  SSN_NO   =@SSN_NO,                                
  DATE_OF_BIRTH  =@DATE_OF_BIRTH,                                
  DRIVER_DRIV_TYPE =@DRIVER_LIC_NO,                                
  DATE_EXPIRY  =@DATE_EXPIRY,                                
  LICENSE_STATUS  =@LICENSE_STATUS,                                    NON_RESI_LICENSE_STATE =@NON_RESI_LICENSE_STATE,                                
  NON_RESI_LICENSE_NO =@NON_RESI_LICENSE_NO,                           
                          
                          
  NON_RESI_LICENSE_STATE2 =@NON_RESI_LICENSE_STATE2,                                
  NON_RESI_LICENSE_NO2 =@NON_RESI_LICENSE_NO2,            
             
                             
  LIC_BRICS_USER = @LIC_BRICS_USER ,                  
  PINK_SLIP_NOTIFY = @PINK_SLIP_NOTIFY,                  
  ADJUSTER_CODE = @ADJUSTER_CODE,            
--Added by Sibin on 9 Dec 08 for Itrack Issue 4994            
  CHANGE_PWD_NEXT_LOGIN=@CHANGE_PWD_NEXT_LOGIN,            
  USER_LOCKED=@USER_LOCKED ,            
  USER_BAD_LOGINS = CASE WHEN @USER_LOCKED !='1' THEN  NULL ELSE USER_BAD_LOGINS END,        
--Added by Sibin for Itrack Issue 4173 on 15 Jan 09          
 NON_RESI_LICENSE_EXP_DATE = @NON_RESI_LICENSE_EXP_DATE,          
 NON_RESI_LICENSE_EXP_DATE2 = @NON_RESI_LICENSE_EXP_DATE2,          
 NON_RESI_LICENSE_STATUS =  @NON_RESI_LICENSE_STATUS,          
 NON_RESI_LICENSE_STATUS2 = @NON_RESI_LICENSE_STATUS2 ,
  CPF=@CPF,
  REG_ID_ISSUE_DATE=@REG_ID_ISSUE_DATE,
  ACTIVITY=@ACTIVITY, 
  REG_ID_ISSUE=@REG_ID_ISSUE, 
  REGIONAL_IDENTIFICATION =@REGIONAL_IDENTIFICATION            
--Added till here                             
                                 
 WHERE                                
  USER_ID   =@User_Id       
 /*Commented by Pravesh on 6 July- dont update  Home Office Underwriter automatically  at all    
--Added by Charles on 3-Jul-09 to update Home Office Underwriter      
DECLARE @USER_ID_FOUND INT    
SELECT @USER_ID_FOUND=DBO.INSTRING(HOME_UNDERWRITER,CONVERT(VARCHAR,@User_Id))     
FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_ID=27    
    
IF @USER_ID_FOUND = 0 AND @User_Type_Id =13    
BEGIN    
UPDATE MNT_AGENCY_LIST SET         
 HOME_UNDERWRITER=ISNULL(HOME_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 MOTOR_UNDERWRITER=ISNULL(MOTOR_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 PRIVATE_UNDERWRITER=ISNULL(PRIVATE_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 UMBRELLA_UNDERWRITER=ISNULL(UMBRELLA_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 GENERAL_UNDERWRITER=ISNULL(GENERAL_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 WATER_UNDERWRITER=ISNULL(WATER_UNDERWRITER+',','')+convert(varchar,@User_Id),        
 RENTAL_UNDERWRITER=ISNULL(RENTAL_UNDERWRITER+',','')+convert(varchar,@User_Id)        
WHERE AGENCY_ID=27     
END     
    
ELSE IF @USER_ID_FOUND > 0 AND @User_Type_Id <> 13    
BEGIN    
DECLARE @NEW_USER_ID_LIST VARCHAR(100)    
SELECT @NEW_USER_ID_LIST=    
CASE    
WHEN CHARINDEX(',',REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',','))=1    
THEN SUBSTRING(REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',','),2,    
LEN(REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',',')))    
WHEN CHARINDEX(',',REVERSE(REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',',')))=1    
THEN SUBSTRING(REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',','),1,    
LEN(REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',','))-1)    
ELSE    
REPLACE(REPLACE(HOME_UNDERWRITER,convert(varchar,@User_Id),''),',,',',')    
END    
FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_ID=27    
    
UPDATE MNT_AGENCY_LIST SET         
 HOME_UNDERWRITER=@NEW_USER_ID_LIST,        
 MOTOR_UNDERWRITER=@NEW_USER_ID_LIST,        
 PRIVATE_UNDERWRITER=@NEW_USER_ID_LIST,        
 UMBRELLA_UNDERWRITER=@NEW_USER_ID_LIST,        
 GENERAL_UNDERWRITER=@NEW_USER_ID_LIST,        
 WATER_UNDERWRITER=@NEW_USER_ID_LIST,        
 RENTAL_UNDERWRITER=@NEW_USER_ID_LIST        
WHERE AGENCY_ID=27     
END       
--Added till here      
    */                           
return 1                
            
END            
                  
END     
  
  

GO

