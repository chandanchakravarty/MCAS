IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_DriverMVR_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_DriverMVR_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  ----------------------------------------------------------                      
Proc Name   : dbo.Proc_GetMotorcycleRule_DriverMVR_Pol            
Created by  : Manoj Rathore            
Date        : 2 Jan 2008            
Purpose     : Get the MVR info for motorcycle rule                       
Revison History  :                            
modified by  : Pravesh K Chandel
Date        : 3 March 09
Purpose     : Get the MVR info for motorcycle rule of active drivers                      

 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/ 
-- DROP PROC dbo.Proc_GetMotorcycleRule_DriverMVR_Pol   864,1,2,2                    
CREATE procedure dbo.Proc_GetMotorcycleRule_DriverMVR_Pol          
(            
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID int,            
 --@DRIVER_ID  int--,                      
 @APPMVRID int         
)                
AS                     
BEGIN       
 DECLARE @MVR_VIOLATION_ID INT    
 DECLARE @CONVICTION_DATE VARCHAR(20)
 DECLARE @OCCURENCE_DATE VARCHAR(20)  
 DECLARE @VIOLATION_TYPE INT   
	IF EXISTS (SELECT CUSTOMER_ID FROM POL_MVR_INFORMATION                                                             
	WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID) --AND  DRIVER_ID=@DRIVER_ID)                        
	BEGIN

		SELECT
			@MVR_VIOLATION_ID = ISNULL(VIOLATION_ID,0),
			--@MVR_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)),
			@CONVICTION_DATE = ISNULL(CONVERT(VARCHAR(20),MVR_DATE,101),''),			
			@VIOLATION_TYPE = ISNULL(VIOLATION_TYPE,0),
			--@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),CONVERT(VARCHAR(20),GETDATE(),101)) --ISNULL(OCCURENCE_DATE,GETDATE())
			@OCCURENCE_DATE = ISNULL(CONVERT(VARCHAR(20),OCCURENCE_DATE,101),'')
        
		FROM   POL_MVR_INFORMATION  PMI WITH(NOLOCK) 
		INNER JOIN POL_DRIVER_DETAILS PDD WITH(NOLOCK) ON PDD.CUSTOMER_ID=PMI.CUSTOMER_ID AND PDD.POLICY_ID=PMI.POLICY_ID AND PDD.POLICY_VERSION_ID=PMI.POLICY_VERSION_ID 
					AND PDD.DRIVER_ID=PMI.DRIVER_ID AND ISNULL(PDD.IS_ACTIVE,'Y')='Y'
		WHERE  PMI.CUSTOMER_ID = @CUSTOMER_ID AND PMI.POLICY_ID=@POLICY_ID AND PMI.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND PMI.POL_MVR_ID=@APPMVRID  --AND  DRIVER_ID=@DRIVER_ID   
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

