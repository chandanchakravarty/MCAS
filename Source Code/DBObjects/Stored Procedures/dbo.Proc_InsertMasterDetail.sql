    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_InsertMasterDetail]              
Created by      : SNEHA          
Date            : 24/10/2011                      
Purpose         :INSERT RECORDS IN MNT_MASTER_DETAIL TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_InsertMasterDetail]        
      
*/  

      
CREATE PROC [dbo].Proc_InsertMasterDetail      
(   
    @TYPE_UNIQUE_ID		[INT]  OUT,	
	@TYPE_ID			[INT] ,
	@TYPE_CODE			[NVARCHAR](25) ,
	@TYPE_NAME			[NVARCHAR](100) ,
	@ADDRESS			[NVARCHAR](50) ,
	@ADDRESS1			[NVARCHAR](50) ,
	@CITY				[NVARCHAR](25) ,
	@COUNTRY			[NVARCHAR](25) ,
	@TEL_NO_OFF			[NVARCHAR](25) ,
	@MOBILE_NO			[NVARCHAR](25) ,
	@E_MAIL				[NVARCHAR](25) ,
	@GST				[DECIMAL](18,2) ,
	@CONTACT_PERSON		[NVARCHAR](100) ,
	@PROVINCE			[NVARCHAR](25) ,
	@POST_CODE			[NVARCHAR](25) ,
	@TEL_NO_RES			[NVARCHAR](25) ,
	@FAX_NO				[NVARCHAR](25) ,
	@GST_REG_NO			[NVARCHAR](25) ,
	@WITHHOLDING_TAX	[DECIMAL](18,2) ,
	@STATUS				[NVARCHAR](25) ,
	@SOLICITOR_TYPE		[NVARCHAR](25) ,
	@PRIVATE_E_MAIL		[NVARCHAR](25) ,
	@SURVEYOR_SOURCE	[NVARCHAR](25) ,
	@CLASSIFICATION		[NVARCHAR](25) ,
	@MEMO				[NVARCHAR](25) ,
	@IS_ACTIVE          [nchar] (2)
)
AS 
BEGIN

SELECT  @TYPE_UNIQUE_ID=ISNULL(MAX(TYPE_UNIQUE_ID),0)+1 FROM MNT_MASTER_DETAIL    
INSERT INTO MNT_MASTER_DETAIL
(
    TYPE_UNIQUE_ID,		
	TYPE_ID		,	
	TYPE_CODE	,		
	TYPE_NAME	,		
	ADDRESS		,
	ADDRESS1		,	
	CITY		,		
	COUNTRY		,	
	TEL_NO_OFF	,		
	MOBILE_NO	,		
	E_MAIL		,		
	GST			,	
	CONTACT_PERSON	,
	PROVINCE,			
	POST_CODE,			
	TEL_NO_RES,			
	FAX_NO	,			
	GST_REG_NO,			
	WITHHOLDING_TAX	,
	STATUS,				
	SOLICITOR_TYPE,		
	PRIVATE_E_MAIL,		
	SURVEYOR_SOURCE,	
	CLASSIFICATION,	
	MEMO,
	IS_ACTIVE				
)
VALUES
(
	@TYPE_UNIQUE_ID		 ,	
	@TYPE_ID			 ,
	@TYPE_CODE			 ,
	@TYPE_NAME			 ,
	@ADDRESS			 ,
	@ADDRESS1			 ,
	@CITY				 ,
	@COUNTRY			 ,
	@TEL_NO_OFF			 ,
	@MOBILE_NO			 ,
	@E_MAIL				 ,
	@GST				 ,
	@CONTACT_PERSON		 ,
	@PROVINCE			 ,
	@POST_CODE			 ,
	@TEL_NO_RES			 ,
	@FAX_NO				 ,
	@GST_REG_NO			 ,
	@WITHHOLDING_TAX	 ,
	@STATUS				 ,
	@SOLICITOR_TYPE		 ,
	@PRIVATE_E_MAIL		 ,
	@SURVEYOR_SOURCE	 ,
	@CLASSIFICATION		 ,
	@MEMO			,
	'Y'	 
)
END
