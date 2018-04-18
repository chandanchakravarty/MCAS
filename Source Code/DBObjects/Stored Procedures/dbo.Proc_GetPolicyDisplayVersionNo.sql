IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDisplayVersionNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDisplayVersionNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN         
--drop proc dbo.Proc_GetPolicyDisplayVersionNo        
--go 
/*----------------------------------------------------------                                                            
Proc Name       : dbo.Proc_GetPolicyDisplayVersionNo                          
Created by      : Shubhanshu Pandey                                                         
Date            : 16/06/2011                                                           
Purpose         :                                                                                               
Revison History :                                                            
Used In        :                                                           
------------------------------------------------------------                                                            
Date     Review By          Comments                                                            
------   ------------       -------------------------*/                                                            
    
 --DROP PROC Proc_GetPolicyDisplayVersionNo '889982011230196000265'     
 -------------------------------------------------------*/       
CREATE PROCEDURE [dbo].[Proc_GetPolicyDisplayVersionNo]                  
(        
  @POLICY_NUMBER NVARCHAR(150)
)      
AS      
 BEGIN      
  DECLARE @CUSTOMER_ID INT
  DECLARE @POLICY_ID INT
  DECLARE @POLICY_STATUS NVARCHAR(40)='NORMAL'
  SELECT
		 @CUSTOMER_ID = CUSTOMER_ID,
		 @POLICY_ID = POLICY_ID,
		 @POLICY_STATUS = POLICY_STATUS 
  FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @POLICY_NUMBER 
  AND  POLICY_STATUS='RENEWED'
		
IF(@POLICY_STATUS = 'NORMAL')
	BEGIN
		SELECT 
			POLICY_NUMBER + '_' + POLICY_DISP_VERSION AS POLICY_DISP_VERSION_AGG,
			CUSTOMER_ID,
			POLICY_ID,
			POLICY_VERSION_ID,
			POLICY_DISP_VERSION  
	
		FROM 
			POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
		WHERE 
			POLICY_NUMBER = @POLICY_NUMBER 
		ORDER BY 
			POLICY_DISP_VERSION ASC
	END	
	
IF(@POLICY_STATUS = 'RENEWED')
	BEGIN
		SELECT 
			POLICY_NUMBER + '_' + POLICY_DISP_VERSION AS POLICY_DISP_VERSION_AGG,
			CUSTOMER_ID,
			POLICY_ID,
			POLICY_VERSION_ID,
			POLICY_DISP_VERSION
			
		FROM 
			POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
		WHERE 
			CUSTOMER_ID = @CUSTOMER_ID AND
			POLICY_ID = @POLICY_ID  
		ORDER BY 
			POLICY_DISP_VERSION ASC
	END				
    
 END 
 

GO

