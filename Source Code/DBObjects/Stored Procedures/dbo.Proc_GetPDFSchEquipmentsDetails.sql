IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFSchEquipmentsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFSchEquipmentsDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_GetPDFSchEquipmentsDetails]    
(      
 @CUSTOMERID   int,      
 @POLID                int,      
 @VERSIONID   int,      
 @CALLEDFROM  VARCHAR(20),      
 @BOATID   INT = 0      
)      
AS      
BEGIN      
 IF @BOATID = 0       
 BEGIN       
  IF (@CALLEDFROM='APPLICATION')      
  BEGIN      
   SELECT       
    ISNULL(CAST(EQUIP_NO AS VARCHAR),'') EQUIP_NO,ISNULL(MAKE,'') MAKE,ISNULL(SERIAL_NO,'') SERIAL_NO,CASE EQUIPMENT_TYPE WHEN 11766 THEN 'Yes' ELSE 'No' END EQUIPMENT_TYPE,      
    CASE WHEN INSURED_VALUE IS NULL THEN '$0.00' ELSE '$' + convert(varchar,convert(money,INSURED_VALUE),1) END INSURED_VALUE,  
    CASE WHEN EQUIP_AMOUNT IS NULL THEN '' ELSE '$' + convert(varchar,convert(money,EQUIP_AMOUNT),1) END EQUIP_AMOUNT,      
    CASE MV.LOOKUP_VALUE_DESC 
			WHEN 'OTHERS'
				THEN
					''
			ELSE
					MV.LOOKUP_VALUE_DESC
	END
 + 
		CASE OTHER_DESCRIPTION 
			WHEN ''
				THEN
					''
		ELSE
				 ISNULL(OTHER_DESCRIPTION,'') 
		END
EQUIPDESC     
   FROM APP_WATERCRAFT_EQUIP_DETAILLS      
    INNER JOIN MNT_LOOKUP_VALUES MV ON MV.LOOKUP_ID=986 AND MV.LOOKUP_UNIQUE_ID=EQUIP_TYPE      
   WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND APP_WATERCRAFT_EQUIP_DETAILLS.IS_ACTIVE='Y'      
   ORDER BY EQUIP_NO      
       
  END      
  ELSE IF (@CALLEDFROM='POLICY')      
  BEGIN      
   SELECT       
    ISNULL(CAST(EQUIP_NO AS VARCHAR),'') EQUIP_NO,ISNULL(MAKE,'') MAKE,ISNULL(SERIAL_NO,'') SERIAL_NO,CASE EQUIPMENT_TYPE WHEN 11766 THEN 'Yes' ELSE 'No' END EQUIPMENT_TYPE,      
    CASE WHEN INSURED_VALUE IS NULL THEN '$0.00' ELSE '$' + convert(varchar,convert(money,INSURED_VALUE),1) END INSURED_VALUE,  
    CASE WHEN EQUIP_AMOUNT IS NULL THEN '' ELSE '$' + convert(varchar,convert(money,EQUIP_AMOUNT),1) END EQUIP_AMOUNT,      
    CASE MV.LOOKUP_VALUE_DESC 
			WHEN 'OTHERS'
				THEN
					''
			ELSE
					MV.LOOKUP_VALUE_DESC
	END
+
	CASE OTHER_DESCRIPTION 
			WHEN ''
				THEN
					''
		ELSE
				 ISNULL(OTHER_DESCRIPTION,'')  
		END
	EQUIPDESC          
   FROM POL_WATERCRAFT_EQUIP_DETAILLS      
    INNER JOIN MNT_LOOKUP_VALUES MV ON MV.LOOKUP_ID=986 AND MV.LOOKUP_UNIQUE_ID=EQUIP_TYPE      
   WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND POL_WATERCRAFT_EQUIP_DETAILLS.IS_ACTIVE='Y'      
   ORDER BY EQUIP_NO      
  END      
 END      
 ELSE      
 BEGIN      
  IF (@CALLEDFROM='APPLICATION')      
  BEGIN      
   SELECT       
    ISNULL(CAST(EQUIP_NO AS VARCHAR),'') EQUIP_NO,ISNULL(MAKE,'') MAKE,ISNULL(SERIAL_NO,'') SERIAL_NO,CASE EQUIPMENT_TYPE WHEN 11766 THEN 'Yes' ELSE 'No' END EQUIPMENT_TYPE,      
    CASE WHEN INSURED_VALUE IS NULL THEN '$0.00' ELSE '$' + convert(varchar,convert(money,INSURED_VALUE),1) END INSURED_VALUE,  
    CASE WHEN EQUIP_AMOUNT IS NULL THEN '' ELSE '$' + convert(varchar,convert(money,EQUIP_AMOUNT),1) END EQUIP_AMOUNT,      
   CASE MV.LOOKUP_VALUE_DESC 
			WHEN 'OTHERS'
				THEN
					''
			ELSE
					MV.LOOKUP_VALUE_DESC
	END
 +
CASE OTHER_DESCRIPTION 
			WHEN ''
				THEN
					''
		ELSE
				 ISNULL(OTHER_DESCRIPTION,'')   
		END
EQUIPDESC       
   FROM APP_WATERCRAFT_EQUIP_DETAILLS      
    INNER JOIN MNT_LOOKUP_VALUES MV ON MV.LOOKUP_ID=986 AND MV.LOOKUP_UNIQUE_ID=EQUIP_TYPE      
   WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND      
   ASSOCIATED_BOAT = @BOATID AND APP_WATERCRAFT_EQUIP_DETAILLS.IS_ACTIVE='Y'      
   ORDER BY EQUIP_NO      
       
  END      
  ELSE IF (@CALLEDFROM='POLICY')      
  BEGIN      
   SELECT       
    ISNULL(CAST(EQUIP_NO AS VARCHAR),'') EQUIP_NO,ISNULL(MAKE,'') MAKE,ISNULL(SERIAL_NO,'') SERIAL_NO,CASE EQUIPMENT_TYPE WHEN 11766 THEN 'Yes' ELSE 'No' END EQUIPMENT_TYPE,      
    CASE WHEN INSURED_VALUE IS NULL THEN '$0.00' ELSE '$' + convert(varchar,convert(money,INSURED_VALUE),1) END INSURED_VALUE,  
    CASE WHEN EQUIP_AMOUNT IS NULL THEN '' ELSE '$' + convert(varchar,convert(money,EQUIP_AMOUNT),1) END EQUIP_AMOUNT,      
    CASE MV.LOOKUP_VALUE_DESC 
			WHEN 'OTHERS'
				THEN
					''
			ELSE
					MV.LOOKUP_VALUE_DESC
	END
+
	CASE OTHER_DESCRIPTION 
			WHEN ''
				THEN
					''
		ELSE
				 ISNULL(OTHER_DESCRIPTION,'') 
	END
	EQUIPDESC             
   FROM POL_WATERCRAFT_EQUIP_DETAILLS      
    INNER JOIN MNT_LOOKUP_VALUES MV ON MV.LOOKUP_ID=986 AND MV.LOOKUP_UNIQUE_ID=EQUIP_TYPE      
   WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND      
   ASSOCIATED_BOAT = @BOATID AND POL_WATERCRAFT_EQUIP_DETAILLS.IS_ACTIVE='Y'      
   ORDER BY EQUIP_NO      
  END      
     
 END   
END



GO

