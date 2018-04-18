IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERTUSER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERTUSER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC [PROC_INSERTUSER]
CREATE PROC [dbo].[PROC_INSERTUSER]                                          
(                                          
 @USER_LOGIN_ID NVARCHAR(10),                                          
 @USER_PWD  NVARCHAR(100),                                          
 @USER_TITLE  NVARCHAR(35),                                          
 @USER_FNAME  NVARCHAR(35),                                          
 @USER_LNAME  NVARCHAR(35),                                          
 @USER_INITIALS  NVARCHAR(3),                                          
 @USER_ADD1  NVARCHAR(70),                                          
 @USER_ADD2  NVARCHAR(70),                                          
 @USER_CITY  NVARCHAR(40),                                          
 @USER_STATE  NVARCHAR(5),                                          
 @USER_ZIP  NVARCHAR(11),                                          
 @USER_PHONE  NVARCHAR(20),                                          
 @USER_EXT  NVARCHAR(10),                                          
 @USER_FAX  NVARCHAR(20),                                          
 @USER_MOBILE  NVARCHAR(20),                                          
 @USER_EMAIL  NVARCHAR(50),                                          
 @IS_ACTIVE  NCHAR(1),                                          
 @CREATED_BY  INT,                                          
 @CREATED_DATETIME DATETIME,                                          
 @MODIFIED_BY  INT,                                          
 @LAST_UPDATED_DATETIME DATETIME,                                          
 @USER_SYSTEM_ID         VARCHAR(8),                                          
 @USER_IMAGE_FOLDER VARCHAR(15),                                          
 @USER_COLOR_SCHEME VARCHAR(15),       
 @LANG_ID INT = 1, --ADDED BY CHETNA ON 5 MARCH ,10                                   
 @USER_MGR_ID  INT,                                          
 @USER_DEF_DIV_ID SMALLINT,                                          
 @USER_DEF_DEPT_ID SMALLINT,                                          
 @USER_DEF_PC_ID  SMALLINT,                                          
 @USER_TIME_ZONE  VARCHAR(5),                                 
 @USER_NOTES  VARCHAR(120),                                     
 @USER_SUPERVISOR NCHAR(1),                                          
 @USER_TYPE_ID  SMALLINT,                                          
 @USER_ID  INT OUTPUT,                                          
 @COUNTRY   NVARCHAR(5),                                          
 @SUB_CODE  VARCHAR(10),                                          
 @SSN_NO   NVARCHAR(100),                                          
 @DATE_OF_BIRTH  DATETIME,                                          
 @DRIVER_LIC_NO NVARCHAR(60),                                          
 @DATE_EXPIRY  DATETIME,                                          
 @LICENSE_STATUS  NVARCHAR(10),                                          
 @NON_RESI_LICENSE_STATE NVARCHAR(5),                                          
 @NON_RESI_LICENSE_NO NVARCHAR(60),                                                         
 @NON_RESI_LICENSE_STATE2 NVARCHAR(5),                                          
 @NON_RESI_LICENSE_NO2 NVARCHAR(60),                                                                
 @LIC_BRICS_USER SMALLINT,                          
 @PINK_SLIP_NOTIFY NCHAR(1),                      
 @ADJUSTER_CODE VARCHAR(10) = NULL,                  
--ADDED BY SIBIN ON 9 DEC 08 FOR ITRACK ISSUE 4994                  
 @CHANGE_PWD_NEXT_LOGIN INT,                   
 @USER_LOCKED NCHAR(1),                       
--ADDED BY SIBIN FOR ITRACK ISSUE 4173 ON 15 JAN 09                  
 @NON_RESI_LICENSE_EXP_DATE DATETIME,                  
 @NON_RESI_LICENSE_EXP_DATE2 DATETIME,                  
 @NON_RESI_LICENSE_STATUS NVARCHAR(20),                  
 @NON_RESI_LICENSE_STATUS2 NVARCHAR(20),
 @REGIONAL_IDENTIFICATION NVARCHAR(40),
 @CPF NVARCHAR(40),
 @REG_ID_ISSUE_DATE DATETIME,
 @ACTIVITY INT,
 @REG_ID_ISSUE NVARCHAR(40)                       
--ADDED TILL HERE                                      
)                           
AS                                          
BEGIN     
                                                                        
 DECLARE @COUNT INT,                                          
 @AGENCY_LIC_NUM INT,                           
 @USER_LIC_NUM INT,                          
 @ADJUSTER_CODE_COUNT INT    
 
  IF (@LANG_ID=0)                  
 BEGIN  
	SET @LANG_ID=1  
 END
                               
 SELECT @COUNT = COUNT(USER_LOGIN_ID) FROM MNT_USER_LIST WITH(NOLOCK) WHERE USER_LOGIN_ID = @USER_LOGIN_ID              
 SELECT @ADJUSTER_CODE_COUNT=COUNT(USER_ID) FROM MNT_USER_LIST WITH(NOLOCK) WHERE USER_TYPE_ID=46 AND ADJUSTER_CODE=@ADJUSTER_CODE  
                     
IF(@COUNT=0)                        
BEGIN     
                                      
	IF EXISTS(SELECT SUB_CODE FROM MNT_USER_LIST WITH(NOLOCK)                                             
	WHERE CAST(CAST(SUB_CODE AS INT) AS VARCHAR(10)) = CAST(CAST(@SUB_CODE AS INT) AS VARCHAR(10)) AND USER_SYSTEM_ID = @USER_SYSTEM_ID)  --CAST ADDED BY CHARLES, ON 20-JUL-09 TO CHECK FOR 01 & 1, FOR ITRACK 6128                                           
	BEGIN                                          
		SELECT @USER_ID = -2                                          
		RETURN                                          
	END   
                                         
	IF (@LIC_BRICS_USER = 10963)                      
	BEGIN                                      
	 ---CHECK THAT THE NUMBER OF LICENSED USERS DO NOT EXCEED THE NUMBER OF LICENCES ASSIGNED TO CURRENT AGENCY                                      
	 SELECT @AGENCY_LIC_NUM=ISNULL(AGENCY_LIC_NUM,0) FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_CODE=@USER_SYSTEM_ID                                      
	 SELECT @USER_LIC_NUM=ISNULL(COUNT(USER_ID),0)+1 FROM MNT_USER_LIST WITH(NOLOCK)                                       
	 WHERE USER_SYSTEM_ID=@USER_SYSTEM_ID AND LIC_BRICS_USER=10963 AND IS_ACTIVE='Y'                                      
	                                      
	 IF(@USER_LIC_NUM > @AGENCY_LIC_NUM)                                      
	 BEGIN                                    
		SELECT @USER_ID = -3                                      
		RETURN                               
	 END 
	                             
	 IF(ISNULL(@ADJUSTER_CODE_COUNT,0)>0)                      
	 BEGIN                                    
		SELECT @USER_ID = -4                                      
		RETURN                               
	 END                               
	END                  
        
	IF LEN(@SUB_CODE)=1        
		SET @SUB_CODE = '00' + @SUB_CODE        
	ELSE IF LEN(@SUB_CODE)=2        
		SET @SUB_CODE = '0' + @SUB_CODE        
                              
  INSERT INTO MNT_USER_LIST                                            
  (                                          
   USER_LOGIN_ID,                                          
   USER_PWD,                                          
   USER_TITLE,                                          
   USER_FNAME,                                           
   USER_LNAME,                                      
   USER_INITIALS,                                          
   USER_ADD1,                                          
   USER_ADD2,                                          
   USER_CITY,                                          
   USER_STATE,                                          
   USER_ZIP,                              
   USER_PHONE,                                          
   USER_EXT,                                          
   USER_FAX,                                          
   USER_MOBILE,                                          
   USER_EMAIL,                                          
   IS_ACTIVE,                                    
   --USER_INACTIVE_DATETIME,                                          
   CREATED_BY,                                          
   CREATED_DATETIME,                                          
   MODIFIED_BY,          
   LAST_UPDATED_DATETIME,                                          
   USER_SYSTEM_ID,                                          
   USER_IMAGE_FOLDER,                                          
   USER_COLOR_SCHEME,         
   LANG_ID,  --ADDED BY CHETNA ON 5 MARCH,10                                   
   USER_MGR_ID  ,                                          
   USER_DEF_DIV_ID ,                                          
   USER_DEF_DEPT_ID ,                                          
   USER_DEF_PC_ID  ,                                          
   USER_TIME_ZONE ,                   
   USER_NOTES , --NOTES                                          
   USER_SPR  ,                                          
   USER_TYPE_ID ,                                          
   COUNTRY,                                          
   GRID_SIZE,                                          
   SUB_CODE,                                          
   SSN_NO,                                         
   DATE_OF_BIRTH,                                          
   DRIVER_DRIV_TYPE,                              
   DATE_EXPIRY,                                          
   LICENSE_STATUS,                                          
   NON_RESI_LICENSE_STATE,                                          
   NON_RESI_LICENSE_NO,                                
   NON_RESI_LICENSE_STATE2,                                          
   NON_RESI_LICENSE_NO2,                                       
   LIC_BRICS_USER,                          
   PINK_SLIP_NOTIFY ,                      
   ADJUSTER_CODE,                  
   --ADDED BY SIBIN ON 9 DEC 08 FOR ITRACK ISSUE 4994                  
   CHANGE_PWD_NEXT_LOGIN,                  
   USER_LOCKED,                                              
   --ADDED BY SIBIN FOR ITRACK ISSUE 4173 ON 15 JAN 09                 
   NON_RESI_LICENSE_EXP_DATE,                
   NON_RESI_LICENSE_EXP_DATE2,         
   NON_RESI_LICENSE_STATUS,                
   NON_RESI_LICENSE_STATUS2,
   REGIONAL_IDENTIFICATION,
   CPF ,
   REG_ID_ISSUE_DATE,
  ACTIVITY,
  REG_ID_ISSUE                                     
   --ADDED TILL HERE                                                       
  )                                          
  VALUES                                           
(                                          
   @USER_LOGIN_ID,                                          
   CONVERT(NVARCHAR,DBO.FUN_ENCRIPTTEXT(@USER_PWD)),                                          
   @USER_TITLE,                                          
   @USER_FNAME,                                          
   @USER_LNAME,                                          
   @USER_INITIALS,                                          
   @USER_ADD1,                                          
   @USER_ADD2,                                          
   @USER_CITY,                           
   @USER_STATE,                                          
   @USER_ZIP,                                          
   @USER_PHONE,                                          
   @USER_EXT,                                          
   @USER_FAX,                                          
   @USER_MOBILE,                                          
   @USER_EMAIL,                                          
   @IS_ACTIVE,                                          
   --@USER_INACTIVE_DATETIME,                                          
   @CREATED_BY,                                          
   @CREATED_DATETIME,                                          
   @MODIFIED_BY,                                          
   @LAST_UPDATED_DATETIME,                                          
   @USER_SYSTEM_ID,                                          
   @USER_IMAGE_FOLDER,                                          
   @USER_COLOR_SCHEME,     
   @LANG_ID,  --ADDED BY CHETNA                                               
   @USER_MGR_ID  ,                                          
   @USER_DEF_DIV_ID ,                                          
   @USER_DEF_DEPT_ID ,                                          
   @USER_DEF_PC_ID ,                                          
   @USER_TIME_ZONE ,                                
   @USER_NOTES , ----NOTES                                          
   @USER_SUPERVISOR ,                                          
   @USER_TYPE_ID ,     
   @COUNTRY,                                          
   10,                                          
   @SUB_CODE,                                          
   @SSN_NO ,                      
   @DATE_OF_BIRTH,                                          
   @DRIVER_LIC_NO,                                          
   @DATE_EXPIRY,                     
   @LICENSE_STATUS,                                          
   @NON_RESI_LICENSE_STATE,                                          
   @NON_RESI_LICENSE_NO,                                   
   @NON_RESI_LICENSE_STATE2,                                          
   @NON_RESI_LICENSE_NO2,                                         
   @LIC_BRICS_USER,                          
   @PINK_SLIP_NOTIFY,                      
   @ADJUSTER_CODE,                  
   --ADDED BY SIBIN ON 9 DEC 08 FOR ITRACK ISSUE 4994                  
   @CHANGE_PWD_NEXT_LOGIN,                  
   @USER_LOCKED,                                           
   --ADDED BY SIBIN FOR ITRACK ISSUE 4173 ON 15 JAN 09                 
   @NON_RESI_LICENSE_EXP_DATE,                
   @NON_RESI_LICENSE_EXP_DATE2,                
   @NON_RESI_LICENSE_STATUS,                
   @NON_RESI_LICENSE_STATUS2,
    @REGIONAL_IDENTIFICATION,
   @CPF ,
   @REG_ID_ISSUE_DATE,
   @ACTIVITY,
   @REG_ID_ISSUE                          
   --ADDED TILL HERE                                           
  )         
  /*RETREIVING THE USER ID*/                                     
  SELECT @USER_ID= MAX(USER_ID) FROM MNT_USER_LIST                         
                                                                                  
END                                          
ELSE                                          
 BEGIN                                                                               
  SELECT @USER_ID = -5-- -1  /*RECORD ALREADY EXIST*/   //Changed by Charles on 21-May-10 for Itrack 91                                        
 END                                          
END 
GO

