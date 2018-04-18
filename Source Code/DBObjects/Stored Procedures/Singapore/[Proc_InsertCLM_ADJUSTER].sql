IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ADJUSTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ADJUSTER]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name       : Dbo.Proc_InsertCLM_ADJUSTER                              
Created by      : Amar                              
Date            : 4/21/2006                              
Purpose       :Evaluation                              
Revison History :                              
Used In        : Wolverine                              
------------------------------------------------------------                              
Modified By  : Asfa Praveen          
Date   : 29/Aug/2007          
Purpose  : To add Adjuster_ID column   
  
Modified By  : Agniswar       
Date   : 16/Sep/2011          
Purpose  : To add columns SUB_ADJUSTER_GST, SUB_ADJUSTER_GST_REG_NO,SUB_ADJUSTER_MOBILE,SUB_ADJUSTER_CLASSIFICATION         

Modified By  : Kuldeep     
Date   : 03/01/2012
Purpose  : To alter column width SUB_ADJUSTER_COUNTRY, SA_COUNTRY
------------------------------------------------------------                                                                          
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--DROP PROC dbo.Proc_InsertCLM_ADJUSTER                              
CREATE PROC [dbo].[Proc_InsertCLM_ADJUSTER]                              
(                              
@ADJUSTER_ID     int output,                              
@ADJUSTER_TYPE     int,                              
@ADJUSTER_NAME     varchar(35),                              
@ADJUSTER_CODE      varchar(10),                            
@SUB_ADJUSTER     varchar(35),                              
@SUB_ADJUSTER_LEGAL_NAME     varchar(50),                              
@SUB_ADJUSTER_ADDRESS1     varchar(75),                              
@SUB_ADJUSTER_ADDRESS2     varchar(75),                              
@SUB_ADJUSTER_CITY     varchar(10),                              
@SUB_ADJUSTER_STATE     int,                              
@SUB_ADJUSTER_ZIP    varchar(11),                        
@SUB_ADJUSTER_PHONE     varchar(15),                              
@SUB_ADJUSTER_FAX     varchar(15),                              
@SUB_ADJUSTER_EMAIL     varchar(50),                              
@SUB_ADJUSTER_WEBSITE     varchar(150),                              
@SUB_ADJUSTER_NOTES     varchar(1000),                              
@IS_ACTIVE     char(1),                              
@CREATED_BY     int,                              
@CREATED_DATETIME     datetime,                          
@SUB_ADJUSTER_COUNTRY NVARCHAR(10),                     
@SUB_ADJUSTER_CONTACT_NAME varchar(50),                    
@SA_ADDRESS1 varchar(75),                    
@SA_ADDRESS2 varchar(75),                    
@SA_CITY varchar(10),                    
@SA_COUNTRY NVARCHAR(10),                    
@SA_STATE int,                    
@SA_ZIPCODE varchar(11),                    
@SA_PHONE varchar(15),                    
@SA_FAX varchar(15),                  
@LOB_ID nvarchar(120),          
@USER_ID     int  ,    
@DISPLAY_ON_CLAIM int,     -- ADDED BY SANTOSH KUMAR GAUTAM ON 13 APRIL 2011    
  
-- Added by Agniswar on 16 SEPTEMBER 2011  
@SUB_ADJUSTER_GST nvarchar(120) = null,   
@SUB_ADJUSTER_GST_REG_NO nvarchar(120) = null,  
@SUB_ADJUSTER_MOBILE nvarchar(120) = null,  
@SUB_ADJUSTER_CLASSIFICATION nvarchar(120) = null  
                                
)                              
AS                              
BEGIN                              
                       
DECLARE @THIRD_PARTY_ADJUSTER INT              
SET @THIRD_PARTY_ADJUSTER = 11738              
if(@ADJUSTER_TYPE<>@THIRD_PARTY_ADJUSTER)                     
begin                
-- IF EXISTS(SELECT ADJUSTER_ID FROM CLM_ADJUSTER WHERE ADJUSTER_CODE=@ADJUSTER_CODE)        
 IF EXISTS(SELECT ADJUSTER_ID FROM CLM_ADJUSTER WHERE USER_ID=@USER_ID)        
  return -2                                
end                
                              
select @ADJUSTER_ID=isnull(Max(ADJUSTER_ID),0)+1 from CLM_ADJUSTER                              
INSERT INTO CLM_ADJUSTER     
(                              
ADJUSTER_ID,                              
ADJUSTER_TYPE,                              
ADJUSTER_NAME,                              
ADJUSTER_CODE,                              
SUB_ADJUSTER,                              
SUB_ADJUSTER_LEGAL_NAME,                              
SUB_ADJUSTER_ADDRESS1,                              
SUB_ADJUSTER_ADDRESS2,                              
SUB_ADJUSTER_CITY,                              
SUB_ADJUSTER_STATE,                              
SUB_ADJUSTER_ZIP,                              
SUB_ADJUSTER_PHONE,                              
SUB_ADJUSTER_FAX,                              
SUB_ADJUSTER_EMAIL,                              
SUB_ADJUSTER_WEBSITE,                              
SUB_ADJUSTER_NOTES,                              
IS_ACTIVE,                         
CREATED_BY,                              
CREATED_DATETIME,                       
SUB_ADJUSTER_COUNTRY,                      
SUB_ADJUSTER_CONTACT_NAME,                    
SA_ADDRESS1,                    
SA_ADDRESS2,                    
SA_CITY,         
SA_COUNTRY,                    
SA_STATE,                    
SA_ZIPCODE,                   
SA_PHONE,                    
SA_FAX,                  
LOB_ID,          
USER_ID  ,    
DISPLAY_ON_CLAIM    ,  
-- Added by Agniswar on 16 SEPTEMBER 2011  
SUB_ADJUSTER_GST,   
SUB_ADJUSTER_GST_REG_NO,  
SUB_ADJUSTER_MOBILE,  
SUB_ADJUSTER_CLASSIFICATION              
)                              
VALUES                              
(                              
@ADJUSTER_ID,                              
@ADJUSTER_TYPE,                              
@ADJUSTER_NAME,                              
@ADJUSTER_CODE,               
@SUB_ADJUSTER,                              
@SUB_ADJUSTER_LEGAL_NAME,                              
@SUB_ADJUSTER_ADDRESS1,                              
@SUB_ADJUSTER_ADDRESS2,                              
@SUB_ADJUSTER_CITY,                              
@SUB_ADJUSTER_STATE,                              
@SUB_ADJUSTER_ZIP,                             
@SUB_ADJUSTER_PHONE,                              
@SUB_ADJUSTER_FAX,                              
@SUB_ADJUSTER_EMAIL,                              
@SUB_ADJUSTER_WEBSITE,                              
@SUB_ADJUSTER_NOTES,                            
@IS_ACTIVE,                              
@CREATED_BY,                              
@CREATED_DATETIME,                          
@SUB_ADJUSTER_COUNTRY,                      
@SUB_ADJUSTER_CONTACT_NAME,                    
@SA_ADDRESS1,                    
@SA_ADDRESS2,                    
@SA_CITY,                    
@SA_COUNTRY,                    
@SA_STATE,                    
@SA_ZIPCODE,                    
@SA_PHONE,                    
@SA_FAX,                  
@LOB_ID,          
@USER_ID  ,    
@DISPLAY_ON_CLAIM ,  
-- Added by Agniswar on 16 SEPTEMBER 2011  
@SUB_ADJUSTER_GST,   
@SUB_ADJUSTER_GST_REG_NO,  
@SUB_ADJUSTER_MOBILE,  
@SUB_ADJUSTER_CLASSIFICATION                  
)                              
END          