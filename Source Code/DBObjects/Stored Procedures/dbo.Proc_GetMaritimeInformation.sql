IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMaritimeInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMaritimeInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                        
Proc Name             : Dbo.Proc_GetMaritimeInformation                                        
Created by            : Santosh Kumar Gautam                                       
Date                  : 09/11/2010                                       
Purpose               : To retrieve maritime id and maritime information                                         
Revison History       :                                        
Used In               : To fill dropdown at risk information page.(CLAIM module)                                        
------------------------------------------------------------                                        
Date     Review By          Comments           
  
drop Proc Proc_GetMaritimeInformation                               
------   ------------       -------------------------*/                                        
--           
            
--         
      
CREATE PROCEDURE [dbo].[Proc_GetMaritimeInformation]            
             
@CUSTOMER_ID         INT,                                                                        
@POLICTY_ID          INT,                                                                        
@POLICY_VERSION_ID   INT,
@CLAIM_ID            INT                                                                        
                                                                  
            
AS            
BEGIN 

SELECT   M.MARITIME_ID ,
         (ISNULL(CAST(M.VESSEL_NUMBER AS NVARCHAR(10)),'')+'-'+ISNULL(M.NAME_OF_VESSEL,'')+'-'+ISNULL(M.TYPE_OF_VESSEL,'')+'-'+
         ISNULL(CAST(M.MANUFACTURE_YEAR AS NVARCHAR(4)),'')+'-'+
         ISNULL(M.MANUFACTURER,''))
         AS MARITIME
FROM POL_MARITIME M WITH(NOLOCK) INNER JOIN     
     CLM_CLAIM_INFO I ON I.CLAIM_ID=@CLAIM_ID
WHERE (M.CUSTOMER_ID=@CUSTOMER_ID AND M.POLICY_ID=@POLICTY_ID AND M.POLICY_VERSION_ID= @POLICY_VERSION_ID AND M.IS_ACTIVE='Y')   
    
END            
            
     
    
GO

