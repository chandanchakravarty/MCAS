  
--Proc Name       : dbo.Proc_UpdateMasterDetail        
--Created by      : SNEHA      
--Date            : 24/10/2011             
--Purpose   :To Update in MNT_MASTER_DETAIL    
--Revison History :                
--Used In   : Ebix Advantage                
--------------------------------------------------------------                
--Date     Review By          Comments                
--------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_UpdateMasterDetail    
CREATE PROC [dbo].Proc_UpdateMasterDetail    
(     
 
    @TYPE_UNIQUE_ID		[INT] ,	
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
	@MEMO				[NVARCHAR](25)    
    
)    
AS    
BEGIN    
    
 UPDATE MNT_MASTER_DETAIL    
 SET    
  	TYPE_ID			=@TYPE_ID	,	
	TYPE_CODE		=@TYPE_CODE,		
	TYPE_NAME		=@TYPE_NAME,		
	ADDRESS			=@ADDRESS,	
	ADDRESS1		=@ADDRESS1,	
	CITY			=@CITY,		
	COUNTRY			=@COUNTRY,	
	TEL_NO_OFF		=@TEL_NO_OFF,		
	MOBILE_NO		=@MOBILE_NO,		
	E_MAIL			=@E_MAIL,		
	GST				=@GST,	
	CONTACT_PERSON	=@CONTACT_PERSON,
	PROVINCE		=@PROVINCE,			
	POST_CODE		=@POST_CODE,			
	TEL_NO_RES		=@TEL_NO_RES,			
	FAX_NO			=@FAX_NO,			
	GST_REG_NO		=@GST_REG_NO,			
	WITHHOLDING_TAX	=@WITHHOLDING_TAX,
	STATUS			=@STATUS,				
	SOLICITOR_TYPE	=@SOLICITOR_TYPE,		
	PRIVATE_E_MAIL	=@PRIVATE_E_MAIL,		
	SURVEYOR_SOURCE =@SURVEYOR_SOURCE,	
	CLASSIFICATION	=@CLASSIFICATION,	
	MEMO			=@MEMO	
 WHERE TYPE_UNIQUE_ID   = @TYPE_UNIQUE_ID    
END    

