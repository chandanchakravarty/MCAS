IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_MVRInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_MVRInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  ----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetWatercraftRule_MVRInfo_Pol            
Created by  : Manoj Rathore            
Date        : 3 Jan 2008            
Purpose     : Get the MVR info for Watercraft rule                       
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/ 
-- DROP PROC dbo.Proc_GetWatercraftRule_MVRInfo_Pol   1009,141,1,2                    
CREATE procedure dbo.Proc_GetWatercraftRule_MVRInfo_Pol          
(            
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID int,            
 @DRIVER_ID  int,                      
 @APPMVRID int         
)                
AS                     
BEGIN       
 DECLARE @MVR_VIOLATION_ID INT    
 DECLARE @CONVICTION_DATE VARCHAR(20)
 DECLARE @OCCURENCE_DATE VARCHAR(20)  
 DECLARE @VIOLATION_TYPE INT
 DECLARE @DETAILS   VARCHAR(500) 
 DECLARE @DRIVER_NAME   VARCHAR(50) 
  
	IF EXISTS (SELECT CUSTOMER_ID FROM POL_WATERCRAFT_MVR_INFORMATION                                                             
	WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID )--AND  DRIVER_ID=@DRIVER_ID)                        
	BEGIN

		SELECT
			@MVR_VIOLATION_ID = ISNULL(VIOLATION_ID,0),
			--@MVR_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)),
			@CONVICTION_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),''),			
			@VIOLATION_TYPE = ISNULL(VIOLATION_TYPE,0),
			--@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)) --ISNULL(OCCURENCE_DATE,GETDATE())
			@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),''),
			@DETAILS = ISNULL(DETAILS,'') ,
			@DRIVER_NAME=ISNULL(DR.DRIVER_FNAME,'')  + ' ' + ISNULL(DR.DRIVER_MNAME,'')  + ' '+ ISNULL(DR.DRIVER_LNAME,'') + ' ('+  DR.DRIVER_CODE + ')'
        
		FROM   POL_WATERCRAFT_MVR_INFORMATION   MVR WITH(NOLOCK) 
		INNER JOIN POL_WATERCRAFT_DRIVER_DETAILS DR WITH(NOLOCK) ON DR.CUSTOMER_ID=MVR.CUSTOMER_ID AND  DR.POLICY_ID=MVR.POLICY_ID 
			AND DR.POLICY_VERSION_ID=MVR.POLICY_VERSION_ID AND DR.DRIVER_ID= MVR.DRIVER_ID
		WHERE  MVR.CUSTOMER_ID = @CUSTOMER_ID AND MVR.POLICY_ID=@POLICY_ID AND MVR.POLICY_VERSION_ID=@POLICY_VERSION_ID AND MVR.APP_WATER_MVR_ID=@APPMVRID 
		AND MVR.DRIVER_ID= @DRIVER_ID 
	END
	ELSE                                                       
	BEGIN                            
		SET @MVR_VIOLATION_ID =0                                                           
		SET @CONVICTION_DATE = NULL        
		set @OCCURENCE_DATE = NULL	                             
		
	END
	
	SET @MVR_VIOLATION_ID=1
	IF((@MVR_VIOLATION_ID=0 OR @MVR_VIOLATION_ID='') and @DETAILS='')
	BEGIN  
		SET @MVR_VIOLATION_ID=0
	END
--========================================
 SELECT @MVR_VIOLATION_ID AS MVR_VIOLATION_ID,
	@CONVICTION_DATE AS CONVICTION_DATE,
	@OCCURENCE_DATE AS OCCURENCE_DATE,
	@DRIVER_NAME  AS DRIVER_NAME
	

 END 







GO

