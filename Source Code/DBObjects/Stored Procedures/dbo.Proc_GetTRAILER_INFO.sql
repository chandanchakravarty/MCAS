IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTRAILER_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTRAILER_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetTRAILER_INFO                
Created by      :  Asfa Praveen    
Date            :  28/Feb/2008                
Purpose         :  To get Trailer info corresponding to customer_id, policy_id, policy_version_id, vehicle_id
Revison History :                
Used In         :   Wolverine                
-------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetTRAILER_INFO 1009, 240, 1, 1            
CREATE proc [dbo].[Proc_GetTRAILER_INFO]                
@CUSTOMER_ID INT,                
@POLICY_ID INT,
@POLICY_VERSION_ID SMALLINT,
@ASSOCIATED_BOAT SMALLINT
AS                
BEGIN 

 SELECT TRAILER_ID, YEAR, MANUFACTURER, SERIAL_NO, ASSOCIATED_BOAT, IS_ACTIVE, 
 SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,INSURED_VALUE),1),0,CHARINDEX('.',CONVERT(VARCHAR(30), CONVERT(MONEY,INSURED_VALUE),1),0)) AS INSURED_VALUE,
 TRAILER_TYPE, MODEL, TRAILER_DED  FROM POL_WATERCRAFT_TRAILER_INFO with (nolock)    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ASSOCIATED_BOAT=@ASSOCIATED_BOAT

END         
        
      
    
  










GO

