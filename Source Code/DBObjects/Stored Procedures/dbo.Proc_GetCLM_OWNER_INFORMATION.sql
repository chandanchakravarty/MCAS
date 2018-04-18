IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_OWNER_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_OWNER_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GetCLM_OWNER_INFORMATION                                      
Created by      : Sumit Chhabra                                          
Date            : 02/05/2006                                            
Purpose         : Fetch data from CLM_OWNER_INFORMATION  table for claim owner screen                        
Created by      : Sumit Chhabra                                           
Revison History :                                            
Used In        : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------*/                                            
CREATE PROC dbo.Proc_GetCLM_OWNER_INFORMATION                                                                    
@OWNER_ID int,      
@CLAIM_ID int      
AS                                            
BEGIN                                            
          
 SELECT          
  OWNER_ID,          
  CLAIM_ID,          
  TYPE_OF_OWNER,          
  NAME,          
  ADDRESS1,          
  ADDRESS2,          
  CITY,          
  STATE,          
  ZIP,          
  HOME_PHONE,          
  MOBILE_PHONE,          
  ISNULL(DEFAULT_PHONE_TO_NOTICE,'') DEFAULT_PHONE_TO_NOTICE,        
  ISNULL(PRODUCTS_INSURED_IS,'') PRODUCTS_INSURED_IS,        
  ISNULL(OTHER_DESCRIPTION,'') OTHER_DESCRIPTION,        
  ISNULL(TYPE_OF_PRODUCT,'') TYPE_OF_PRODUCT,        
  ISNULL(WHERE_PRODUCT_SEEN,'') WHERE_PRODUCT_SEEN,        
  ISNULL(OTHER_LIABILITY,'') OTHER_LIABILITY,        
  IS_ACTIVE,          
  WORK_PHONE,          
  EXTENSION,    
 VEHICLE_OWNER,  
 VEHICLE_ID,
 COUNTRY          
 FROM          
  CLM_OWNER_INFORMATION          
 WHERE          
  OWNER_ID=@OWNER_ID and      
 CLAIM_ID=@CLAIM_ID            
                
END            
          
          
        
      
    
  



GO

