IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SavePolicyDocumentXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SavePolicyDocumentXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
 Proc Name      : dbo.Proc_SavePolicyDocumentXML              
 Created By  : Pravesh K Chandel  
 Created date : 20 May 2010  
 Purpose  : Save Policy Documents XML  
 used in  : EbixAdvantage  
 Modified By : Praveen Kumar
 Modification Date :23/07/2010
 
 DROP PROC Proc_SavePolicyDocumentXML  
 -------------------------------------------------------*/   
CREATE proc [dbo].[Proc_SavePolicyDocumentXML]               
(    
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID int,  
 @DOC_XML  text,           
 @DOC_TYPE varchar(15)  
)  
as  
 BEGIN  
  
 DECLARE @POLICY_STATUS NVARCHAR(50)


if not exists(select CUSTOMER_ID from ACT_PREMIUM_NOTICE_PROCCESS_LOG WITH(NOLOCK)  
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
 and CALLED_FOR=@DOC_TYPE  
)  
	begin  

		--SELECT @POLICY_STATUS=POLICY_STATUS from POL_CUSTOMER_POLICY_LIST WHERE
		--CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID
	
		 BEGIN
		 INSERT INTO ACT_PREMIUM_NOTICE_PROCCESS_LOG    
		 (    
		 CUSTOMER_ID ,    
		 POLICY_ID ,    
		 POLICY_VERSION_ID,    
		 CALLED_FOR,    
		 DEC_CUSTOMERXML,     
		 DATE_TIME    
		 )                
		 VALUES                
		 (           
		 @CUSTOMER_ID,    
		 @POLICY_ID,    
		 @POLICY_VERSION_ID,    
		 @DOC_TYPE,    
		 @DOC_XML,    
		 GETDATE()    
		 ) 
		END
	
	end
	
if  exists(select CUSTOMER_ID from ACT_PREMIUM_NOTICE_PROCCESS_LOG  WITH(NOLOCK)
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
 and CALLED_FOR=@DOC_TYPE  
) 
	 begin  

		 SELECT @POLICY_STATUS= POLICY_STATUS from POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE
		 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
		 
		 IF @POLICY_STATUS <> 'NORMAL' 
		
			 BEGIN
				 UPDATE  ACT_PREMIUM_NOTICE_PROCCESS_LOG  SET DEC_CUSTOMERXML =@DOC_XML,
	   			 DATE_TIME   = GETDATE() 
	   			 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
	   			
			 END
    end
   
 END  
  
GO

