IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_getpolicy_details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_getpolicy_details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name			: dbo.proc_getpolicy_details      
Created by			:    
Date				: 
Purpose				: 
Revison History		:	                            
Modified by			: Lalit kumar Chauhan	
Modified Date		: August 26,2010
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------
   drop proc dbo.proc_getpolicy_details
-- dbo.proc_getpolicy_details 1692,118,4   
 */  

CREATE PROC [dbo].[proc_getpolicy_details]     
(    
 @POLICY_NUM VARCHAR(40),  
 @CALLED_FROM VARCHAR(20)=NULL  
 )   
AS                                              
  
BEGIN      
    IF(@CALLED_FROM <> 'AUTO_END') 
		BEGIN
			 SELECT  top 1 CUSTOMER_ID AS CUSTOMER_ID ,  
			 POLICY_ID AS POLICY_ID ,   
			 POLICY_VERSION_ID AS POLICY_VERSION_ID 
			 FROM POL_CUSTOMER_POLICY_LIST with(nolock)  
			 WHERE POLICY_NUMBER LIKE '%' +@POLICY_NUM+'%'     
			 order by POLICY_VERSION_ID DESC  
		END
    ELSE
		BEGIN
				SELECT  top 1 CUSTOMER_ID AS CUSTOMER_ID ,  
				 POLICY_ID AS POLICY_ID , POLICY_LOB, 
				 POLICY_VERSION_ID AS POLICY_VERSION_ID , POLICY_NUMBER , APP_EFFECTIVE_DATE AS POLICY_EFF_DATE , APP_EXPIRATION_DATE AS POLICY_EXP_DATE
				 
				 FROM POL_CUSTOMER_POLICY_LIST with(nolock)  
				 WHERE POLICY_NUMBER = @POLICY_NUM 
				 order by POLICY_VERSION_ID DESC  
		END
END    
    
  
GO

