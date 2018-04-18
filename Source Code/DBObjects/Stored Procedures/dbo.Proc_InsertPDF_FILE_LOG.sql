IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPDF_FILE_LOG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPDF_FILE_LOG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name    : dbo.Proc_InsertPRINT_JOBS                      
Created by   : Neeraj Singh     
Date         : 31-08-2009            
Purpose      : To add records at PDF_FILE_LOG table      
Revison History :                    
modifed By	:
modified Date	:
Purpose		: 
modifed By	:
modified Date	:
Purpose		: 
Used In  :   Wolverine                           
 ------------------------------------------------------------                                    
Date     Review By          Comments                                  
 drop proc        dbo.Proc_InsertPDF_FILE_LOG                   
------   ------------       -------------------------*/                       
CREATE PROC dbo.Proc_InsertPDF_FILE_LOG 
(                                 
@CUSTOMER_ID int,      
@POLICY_ID int,      
@POLICY_VERSION_ID smallint,      
@DOCUMENT_CODE nvarchar(50),      
@PRINT_DATETIME datetime,      
@URL_PATH nvarchar(200),    
@CREATED_DATETIME datetime,      
@CREATED_BY int,    
@ENTITY_TYPE varchar(50) =null,
@AGENCY_ID int = null,
@FILE_NAME VARCHAR(400)=NULL   ,
@PROCESS_ID   int=null
)                    
AS                    
BEGIN            
      
if (@AGENCY_ID is null or @AGENCY_ID =0)     
	SELECT @AGENCY_ID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
        
 INSERT INTO PDF_FILE_LOG         
 (      
  CUSTOMER_ID,      
  POLICY_ID,      
  POLICY_VERSION_ID,      
  DOCUMENT_CODE,      
  PRINT_DATETIME,      
  URL_PATH,     
  CREATED_DATETIME,      
  CREATED_BY,      
  IS_ACTIVE,    
  ENTITY_TYPE,
  AGENCY_ID,
  FILE_NAME ,
 PROCESS_ID     
 )      
 VALUES      
 (      
  @CUSTOMER_ID,      
  @POLICY_ID,      
  @POLICY_VERSION_ID,      
  @DOCUMENT_CODE,      
  @PRINT_DATETIME,      
  @URL_PATH,     
  @CREATED_DATETIME,      
  @CREATED_BY,      
  'Y',    
  @ENTITY_TYPE,
  @AGENCY_ID,
  @FILE_NAME ,
  @PROCESS_ID
 )      
       
END    
  









GO

