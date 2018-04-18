IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPRINT_JOBS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPRINT_JOBS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name    : dbo.Proc_InsertPRINT_JOBS                        
Created by   : Sumit Chhabra        
Date         : 28-12-2006              
Purpose      : To add records at Print Jobs table        
Revison History :                      
modifed By :Pravesh K Chandel  
modified Date :20 Feb 2008  
Purpose  : Add New Column Process_id  
modifed By :Pravesh K Chandel  
modified Date :23 APRIL 2008  
Purpose  : fetch agency id if it is not passed  
Used In  :   Wolverine                             
 ------------------------------------------------------------                                      
Date     Review By          Comments                                    
 drop proc        dbo.Proc_InsertPRINT_JOBS                     
------   ------------       -------------------------*/                         
CREATE PROC [dbo].[Proc_InsertPRINT_JOBS]                      
(                                   
@CUSTOMER_ID int,        
@POLICY_ID int,        
@POLICY_VERSION_ID smallint,        
@DOCUMENT_CODE nvarchar(50),        
@PRINT_DATETIME datetime,        
@URL_PATH nvarchar(200),        
@ONDEMAND_FLAG nchar(1),        
@PRINT_SUCCESSFUL nchar(1),        
@PRINTED_DATETIME datetime,        
@DUPLEX nchar(1),        
@CREATED_DATETIME datetime,        
@CREATED_BY int,      
@ENTITY_TYPE varchar(20) =null,  --Changed size to 20 by Charles on 8-Sep-09
@AGENCY_ID int = null,  
@FILE_NAME VARCHAR(400)=NULL   ,  
@PROCESS_ID   int=null  ,
@ENTITY_ID   int=null  ,
-- MODIFIED BY SANTOSH GAUTAM ON 23 FEB 2011 
@CLAIM_ID      int=null  ,
@ACTIVITY_ID   int=null  

)                      
AS                      
BEGIN              
        
--DECLARE @PRINT_JOBS_ID int   
if (@AGENCY_ID is null or @AGENCY_ID =0)       
 SELECT @AGENCY_ID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
-- SELECT @PRINT_JOBS_ID=ISNULL(MAX(PRINT_JOBS_ID),0) + 1 FROM PRINT_JOBS         
--  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
        
 INSERT INTO PRINT_JOBS           
 (        
  CUSTOMER_ID,        
  POLICY_ID,        
  POLICY_VERSION_ID,        
--  PRINT_JOBS_ID,        
  DOCUMENT_CODE,        
  PRINT_DATETIME,        
  URL_PATH,        
  ONDEMAND_FLAG,        
  PRINT_SUCCESSFUL,        
  PRINTED_DATETIME,        
  DUPLEX,        
  CREATED_DATETIME,        
  CREATED_BY,        
  IS_ACTIVE,      
  ENTITY_TYPE,  
  AGENCY_ID,  
  FILE_NAME ,  
 PROCESS_ID ,
 ENTITY_ID ,
 CLAIM_ID,
 ACTIVITY_ID     
 )        
 VALUES        
 (        
  @CUSTOMER_ID,        
  @POLICY_ID,        
  @POLICY_VERSION_ID,        
--  @PRINT_JOBS_ID,        
  @DOCUMENT_CODE,        
  @PRINT_DATETIME,        
  @URL_PATH,        
  @ONDEMAND_FLAG,        
  @PRINT_SUCCESSFUL,        
  @PRINTED_DATETIME,        
  @DUPLEX,        
  @CREATED_DATETIME,        
  @CREATED_BY,        
  'Y',      
  @ENTITY_TYPE,  
  @AGENCY_ID,  
  @FILE_NAME ,  
@PROCESS_ID  ,
@ENTITY_ID,
@CLAIM_ID,
@ACTIVITY_ID
 )        
         
END      
    

GO

