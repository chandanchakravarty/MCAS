IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GetDetailsFromVIN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GetDetailsFromVIN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                    
Proc Name       : dbo.PROC_GetDetailsFromVIN                                                    
Created by      : Sumit Chhabra                  
Date            : 09/11/2006    
Purpose       : To Get VIN details     
Revison History :                                                    
Used In        : Wolverine    
Modified By : Neeraj Singh           
Modified Date : 06-16-2009              
Purpose :                                              
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                                                    
--drop PROC dbo.PROC_GetDetailsFromVIN                                                 
CREATE PROC dbo.PROC_GetDetailsFromVIN 
(                                                    
@VIN VARCHAR(1000)                  
)                                                    
AS                                                    
BEGIN      
      
SELECT @VIN = REPLACE (@VIN,'REPLACE_CHAR','&')     
declare @vin1 varchar(10)          
declare @vin2 varchar(10)  
set @vin1=left(@vin,8) + '&' + substring(@vin,10,1)   
set @vin2=left(@vin1,3) + '&' + substring(@vin1,5,6)    
IF EXISTS 
	(SELECT VIN FROM MNT_VIN_MASTER 
		WHERE 
		((VIN like  @VIN1 + '%')  or (VIN like  @VIN2 + '%' )) )
	-- Commented as VIN Compare logic have been changed  for time being
--(SUBSTRING(VIN, 1, 3) +  SUBSTRING(VIN, 5, 4) + SUBSTRING(VIN, 10, 1)) = 
			--	(SUBSTRING(@VIN, 1, 3) +  SUBSTRING(@VIN, 5, 4) + SUBSTRING(@VIN, 10, 1)))
BEGIN	
 IF EXISTS(SELECT VIN FROM MNT_VIN_MASTER 
		WHERE 
			(SUBSTRING(VIN, 1, 3) +  SUBSTRING(VIN, 5, 4) + SUBSTRING(VIN, 10, 1)) = 
			(SUBSTRING(@VIN, 1, 3) +  SUBSTRING(@VIN, 5, 4) + SUBSTRING(@VIN, 10, 1))
			AND AIRBAG IS NULL)  
	 SELECT '1' AS RESULT,      
		 ISNULL(M.MODEL_YEAR,'') MODEL_YEAR,   
		 ISNULL(V.LOOKUP_VALUE_DESC,'') MAKE_CODE,      
--		 ISNULL(M.MAKE_NAME,'') MAKE_NAME,      
--  		 ISNULL(M.MAKE_CODE,'') MAKE_CODE, 		     
		 ISNULL(M.SERIES_NAME,'') SERIES_NAME,       
		 ISNULL(M.BODY_TYPE,'') BODY_TYPE,       
		 ISNULL(M.ANTI_LCK_BRAKES,'') ANTI_LCK_BRAKES,       
		 '' AS AIRBAG,       
		 ISNULL(M.SYMBOL,'') SYMBOL         
	 FROM MNT_VIN_MASTER M  
	 LEFT OUTER JOIN MNT_LOOKUP_VALUES V
	 ON
		V.LOOKUP_VALUE_CODE = M.MAKE_CODE
		AND
		V.LOOKUP_ID = 1308
	 WHERE
		((M.VIN like  @VIN1 + '%')  or (M.VIN like  @VIN2 + '%' )) 
		-- Commented as VIN Compare logic have been changed  for time being
	  	--(SUBSTRING(M.VIN, 1, 3) +  SUBSTRING(M.VIN, 5, 4) + SUBSTRING(M.VIN, 10, 1)) = 
		--(SUBSTRING(@VIN, 1, 3) +  SUBSTRING(@VIN, 5, 4) + SUBSTRING(@VIN, 10, 1))
	  AND V.IS_ACTIVE='Y'
 ELSE  
	 SELECT '1' AS RESULT,      
	 ISNULL(M.MODEL_YEAR,'') MODEL_YEAR,       
	 ISNULL(V2.LOOKUP_VALUE_DESC,'') MAKE_CODE,    
--	 ISNULL(M.MAKE_NAME,'') MAKE_NAME,     
	 --ISNULL(M.MAKE_CODE,'') MAKE_CODE,  
	 ISNULL(M.SERIES_NAME,'') SERIES_NAME,       
	 ISNULL(M.BODY_TYPE,'') BODY_TYPE,       
	 ISNULL(M.ANTI_LCK_BRAKES,'') ANTI_LCK_BRAKES,       
	 ISNULL(V1.LOOKUP_UNIQUE_ID,'') AIRBAG,       
	 ISNULL(M.SYMBOL,'') SYMBOL         
	 FROM MNT_VIN_MASTER M  
	LEFT OUTER JOIN   
	 MNT_LOOKUP_VALUES V1  
	 ON  
	  V1.LOOKUP_VALUE_CODE = M.AIRBAG  
	 LEFT OUTER JOIN MNT_LOOKUP_VALUES V2
	 ON
		V2.LOOKUP_VALUE_CODE = M.MAKE_CODE
		AND 
		V2.LOOKUP_ID = 1308
		AND
		V1.LOOKUP_ID = 20
	WHERE   
	-- Commented as VIN Compare logic have been changed  for time being
	 --	(SUBSTRING(VIN, 1, 3) +  SUBSTRING(VIN, 5, 4) + SUBSTRING(VIN, 10, 1)) = 
	--		(SUBSTRING(@VIN, 1, 3) +  SUBSTRING(@VIN, 5, 4) + SUBSTRING(@VIN, 10, 1))  AND  
 ((M.VIN like  @VIN1 + '%')  or (M.VIN like  @VIN2 + '%' )) and
	 V1.IS_ACTIVE='Y'  AND V2.IS_ACTIVE='Y'
END
ELSE      
 SELECT '-1' AS RESULT      
END      

GO

