IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_MVRInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_MVRInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  ----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetWatercraftRule_MVRInfo            
Created by  : Manoj Rathore            
Date        : 25 Nov.,2005            
Purpose     : Get the MVR info for motorcycle rule                       
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/ 
-- DROP PROC dbo.Proc_GetWatercraftRule_MVRInfo                     
CREATE procedure dbo.Proc_GetWatercraftRule_MVRInfo          
(            
 @CUSTOMERID int,            
 @APPID int,            
 @APPVERSIONID int,            
 @DRIVERID  int,                      
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

IF EXISTS(SELECT CUSTOMER_ID FROM APP_WATER_MVR_INFORMATION                                                             
	WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID) --AND  DRIVER_ID=@DRIVERID)                        
	BEGIN
		SELECT @MVR_VIOLATION_ID = ISNULL(VIOLATION_ID,0),
		--@MVR_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)),
		@CONVICTION_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),''),			
		@VIOLATION_TYPE = ISNULL(VIOLATION_TYPE,0),
		--@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)) --ISNULL(OCCURENCE_DATE,GETDATE())
		@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),''),
		@DETAILS = ISNULL(DETAILS,'') ,
		@DRIVER_NAME=ISNULL(DR.DRIVER_FNAME,'')  + ' ' + ISNULL(DR.DRIVER_MNAME,'')  + ' '+ ISNULL(DR.DRIVER_LNAME,'') + ' ('+  DR.DRIVER_CODE + ')'
   
		
		FROM   APP_WATER_MVR_INFORMATION   MVR WITH(NOLOCK) 
		INNER JOIN APP_WATERCRAFT_DRIVER_DETAILS DR WITH(NOLOCK) ON DR.CUSTOMER_ID=MVR.CUSTOMER_ID AND  DR.APP_ID=MVR.APP_ID 
			AND DR.APP_VERSION_ID=MVR.APP_VERSION_ID AND DR.DRIVER_ID= MVR.DRIVER_ID

		WHERE  MVR.CUSTOMER_ID = @CUSTOMERID AND MVR.APP_ID=@APPID AND MVR.APP_VERSION_ID=@APPVERSIONID AND MVR.APP_WATER_MVR_ID=@APPMVRID     
		AND MVR.DRIVER_ID=@DRIVERID
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
	@DRIVER_NAME   AS DRIVER_NAME

END 




GO

