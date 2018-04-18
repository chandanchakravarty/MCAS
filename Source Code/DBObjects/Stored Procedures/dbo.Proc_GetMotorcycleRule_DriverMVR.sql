IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_DriverMVR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_DriverMVR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  ----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetMotorcycleRule_DriverMVR            
Created by  : Ashwini            
Date        : 25 Nov.,2005            
Purpose     : Get the MVR info for motorcycle rule                       
Revison History  :                            
modified by  : Pravesh K Chandel
Date        : 3 March 09
Purpose     : Get the MVR info for motorcycle rule of active drivers                      

 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/ 
-- DROP PROC dbo.Proc_GetMotorcycleRule_DriverMVR                     
CREATE procedure dbo.Proc_GetMotorcycleRule_DriverMVR            
(            
 @CUSTOMERID int,            
 @APPID int,            
 @APPVERSIONID int,            
 --@DRIVERID  int,                      
 @APPMVRID int         
)                
AS                     
BEGIN       
 DECLARE @MVR_VIOLATION_ID INT    
 DECLARE @CONVICTION_DATE VARCHAR(20)
 DECLARE @OCCURENCE_DATE VARCHAR(20)  
 DECLARE @VIOLATION_TYPE INT  
 
IF EXISTS(SELECT CUSTOMER_ID FROM APP_MVR_INFORMATION                                                             
	WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID) --AND  DRIVER_ID=@DRIVERID)                        
	BEGIN
		SELECT @MVR_VIOLATION_ID = ISNULL(VIOLATION_ID,0),
		--@MVR_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)),
		@CONVICTION_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),''),			
		@VIOLATION_TYPE = ISNULL(VIOLATION_TYPE,0),
		--@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)) --ISNULL(OCCURENCE_DATE,GETDATE())
		@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),'') 
		
		FROM   APP_MVR_INFORMATION   PMI WITH(NOLOCK) 
		INNER JOIN APP_DRIVER_DETAILS PDD WITH(NOLOCK) ON PDD.CUSTOMER_ID=PMI.CUSTOMER_ID AND PDD.APP_ID=PMI.APP_ID AND PDD.APP_VERSION_ID=PMI.APP_VERSION_ID 
					AND PDD.DRIVER_ID=PMI.DRIVER_ID AND ISNULL(PDD.IS_ACTIVE,'Y')='Y'

		WHERE  PMI.CUSTOMER_ID = @CUSTOMERID AND PMI.APP_ID=@APPID AND PMI.APP_VERSION_ID=@APPVERSIONID  AND PMI.APP_MVR_ID=@APPMVRID --AND  DRIVER_ID=@DRIVERID    
	END
	
	ELSE                                                       
	BEGIN                            
		SET @MVR_VIOLATION_ID =0                                                           
		SET @CONVICTION_DATE = NULL        
		set @OCCURENCE_DATE = NULL	                             
		
	END
--========================================
 SELECT @MVR_VIOLATION_ID AS MVR_VIOLATION_ID,
	@CONVICTION_DATE AS CONVICTION_DATE,
	@OCCURENCE_DATE AS OCCURENCE_DATE

END 





GO

