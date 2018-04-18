IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWatercraftOperatorInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWatercraftOperatorInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_InsertWatercraftOperatorInfo_ACORD      
  -----------------------------------         
/*----------------------------------------------------------              
Proc Name       : dbo.WatercraftOperatorInfo              
Created by      : nid hi              
Date            : 5/18/2005              
Purpose       :Evaluation              
Revison History :              
Used In        : Wolverine              
        
             
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_InsertWatercraftOperatorInfo_ACORD              
(              
 @CUSTOMER_ID     int,              
 @APP_ID     int,              
 @APP_VERSION_ID     smallint,              
 @DRIVER_ID     smallint output,              
 --@DRIVER_FNAME     nvarchar(150),      
 @DRIVER_FNAME     nvarchar(75),              
 @DRIVER_MNAME     nvarchar(25),              
 --@DRIVER_LNAME     nvarchar(150),      
 @DRIVER_LNAME     nvarchar(75),               
 --@DRIVER_CODE     nvarchar(40),      
 @DRIVER_CODE     nvarchar(20),      
 --@DRIVER_SUFFIX     nvarchar(20),      
 @DRIVER_SUFFIX     nvarchar(10),               
 --@DRIVER_ADD1     nvarchar(140),              
 --@DRIVER_ADD2     nvarchar(140),      
 @DRIVER_ADD1     nvarchar(70),              
 @DRIVER_ADD2     nvarchar(70),               
 --@DRIVER_CITY     nvarchar(80),      
 @DRIVER_CITY     nvarchar(40),               
 --@DRIVER_STATE     nvarchar(10),      
 @DRIVER_STATE     nvarchar(5),      
 @DRIVER_ZIP     varchar(11),              
 --@DRIVER_COUNTRY     nchar(10),      
 @DRIVER_COUNTRY     nchar(5),     
 @MARITAL_STATUS     nchar(1),           
 @DRIVER_DOB     datetime =null,              
 --@DRIVER_SSN     nvarchar(22),      
 @DRIVER_SSN     nvarchar(88),       
 @DRIVER_SEX   nchar(2),    
 --@DRIVER_DRIV_LIC     nvarchar(60),      
 @DRIVER_DRIV_LIC     nvarchar(30),               
 --@DRIVER_LIC_STATE     nvarchar(10),      
 @DRIVER_LIC_STATE     nvarchar(5),               
 @DRIVER_COST_GAURAD_AUX     int =null,              
 @CREATED_BY     int,              
 @CREATED_DATETIME     datetime =null,              
 @EXPERIENCE_CREDIT DECIMAL(9,2)=NULL,              
 @VEHICLE_ID INT=NULL,              
 @PERCENT_DRIVEN DECIMAL(9,2)=NULL,            
 @APP_VEHICLE_PRIN_OCC_ID  INT    ,        
@YEARS_LICENSED int,  
--Added on 11 may 2006  
@WAT_SAFETY_COURSE int,  
@CERT_COAST_GUARD int,
@VIOLATIONS int = null
  
  
        
)              
AS              
BEGIN           
/*          
Check for duplicate driver code has been done away with             
If Not Exists              
(              
SELECT DRIVER_CODE FROM APP_WATERCRAFT_DRIVER_DETAILS               
WHERE DRIVER_CODE=@DRIVER_CODE              
AND CUSTOMER_ID = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID)              
              
BEGIN*/          
select @DRIVER_ID=isnull(max(DRIVER_ID),0)+1               
from APP_WATERCRAFT_DRIVER_DETAILS               
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID

           
SET  @DRIVER_SSN = NULL               
SELECT @DRIVER_SSN = ISNULL(CO_APPL_SSN_NO,'') FROM 
CLT_APPLICANT_LIST with(nolock)
WHERE CUSTOMER_ID = @CUSTOMER_ID  
    AND ISNULL(LTRIM(RTRIM(FIRST_NAME)),'') = ISNULL(@DRIVER_FNAME,'') AND
        ISNULL(LTRIM(RTRIM(MIDDLE_NAME)),'') = ISNULL(@DRIVER_MNAME,'')  
    AND ISNULL(LTRIM(RTRIM(LAST_NAME)),'') = ISNULL(@DRIVER_LNAME,'')              
        
INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS              
(              
 CUSTOMER_ID,              
 APP_ID,              
 APP_VERSION_ID,              
 DRIVER_ID,              
 DRIVER_FNAME,              
 DRIVER_MNAME,              
 DRIVER_LNAME,              
 DRIVER_CODE,              
 DRIVER_SUFFIX,              
 DRIVER_ADD1,              
 DRIVER_ADD2,              
 DRIVER_CITY,              
 DRIVER_STATE,              
 DRIVER_ZIP,              
 DRIVER_COUNTRY,  
 MARITAL_STATUS,              
 DRIVER_DOB,              
 DRIVER_SSN,              
 DRIVER_SEX,              
 DRIVER_DRIV_LIC,              
 DRIVER_LIC_STATE,          
 DRIVER_COST_GAURAD_AUX,              
 IS_ACTIVE,              
 CREATED_BY,              
 CREATED_DATETIME,              
 EXPERIENCE_CREDIT,              
 VEHICLE_ID,              
 PERCENT_DRIVEN,            
 APP_VEHICLE_PRIN_OCC_ID ,        
YEARS_LICENSED ,  
--Added on 11 may 2006  
WAT_SAFETY_COURSE,  
CERT_COAST_GUARD,
VIOLATIONS
         
)              
VALUES              
(              
 @CUSTOMER_ID,              
 @APP_ID,              
 @APP_VERSION_ID,              
 @DRIVER_ID,              
 @DRIVER_FNAME,              
 @DRIVER_MNAME,              
 @DRIVER_LNAME,              
 @DRIVER_CODE,           
 @DRIVER_SUFFIX,              
 @DRIVER_ADD1,              
 @DRIVER_ADD2,              
 @DRIVER_CITY,              
 @DRIVER_STATE,              
 @DRIVER_ZIP,              
 @DRIVER_COUNTRY,   
 @MARITAL_STATUS,             
 @DRIVER_DOB,              
 @DRIVER_SSN,              
 @DRIVER_SEX,              
 @DRIVER_DRIV_LIC,              
 @DRIVER_LIC_STATE,              
 @DRIVER_COST_GAURAD_AUX,              
 'Y',              
 @CREATED_BY,              
 @CREATED_DATETIME,              
 @EXPERIENCE_CREDIT ,              
 @VEHICLE_ID,              
 @PERCENT_DRIVEN,            
 @APP_VEHICLE_PRIN_OCC_ID ,        
 @YEARS_LICENSED,  
 --Added on 11 may 2006  
 @WAT_SAFETY_COURSE,  
 @CERT_COAST_GUARD,
 @VIOLATIONS  
           
)        

--Modified 14 sep 2007      
INSERT INTO APP_OPERATOR_ASSIGNED_BOAT
(        
 CUSTOMER_ID,        
 APP_ID,        
 APP_VERSION_ID,        
 DRIVER_ID,        
 BOAT_ID,        
 APP_VEHICLE_PRIN_OCC_ID        
)        
VALUES        
(        
 @CUSTOMER_ID,        
 @APP_ID,        
 @APP_VERSION_ID,        
 @DRIVER_ID,        
 @VEHICLE_ID,        
 @APP_VEHICLE_PRIN_OCC_ID        
)        

END              
              
              
--END            
            
          
        
        
        
        
      
    
  
  
  










GO

