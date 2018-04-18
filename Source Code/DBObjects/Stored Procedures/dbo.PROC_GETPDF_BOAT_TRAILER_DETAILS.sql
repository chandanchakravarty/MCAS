IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPDF_BOAT_TRAILER_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPDF_BOAT_TRAILER_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE dbo.PROC_GETPDF_BOAT_TRAILER_DETAILS          
 @CUSTOMERID   int,            
 @POLID                int,            
 @VERSIONID   int,            
 @BOATID   int,            
 @CALLEDFROM  VARCHAR(20)            
AS            
BEGIN            
 IF @CALLEDFROM='APPLICATION'            
 BEGIN            
  SELECT YEAR, MANUFACTURER, MODEL, SERIAL_NO, convert(varchar,convert(money,INSURED_VALUE),1) INSURED_VALUE,          
   ISNULL(CONVERT(VARCHAR(20),TRAILER_NO),'') TRAILER_NUMBER,                        
  CASE ISNULL(TRAILER_NO,'')                         
  WHEN '' THEN ''                        
  ELSE 'Trailer'                        
  END TRAILERTYPE,                         
  CONVERT(VARCHAR,CONVERT(MONEY,INSURED_VALUE),1) TRAILER_LIMIT,  
  CONVERT(VARCHAR,CONVERT(MONEY,TRAILER_DED),1) TRAILER_DED, TRAILER_DED_AMOUNT_TEXT            
  FROM APP_WATERCRAFT_TRAILER_INFO             
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @POLID AND APP_VERSION_ID = @VERSIONID and             
  ASSOCIATED_BOAT=@BOATID            
  and APP_WATERCRAFT_TRAILER_INFO.is_active='Y'             
  ORDER BY TRAILER_NO            
 END            
 ELSE IF @CALLEDFROM='POLICY'            
 BEGIN            
  SELECT YEAR, MANUFACTURER, MODEL, SERIAL_NO, convert(varchar,convert(money,INSURED_VALUE),1) INSURED_VALUE,          
   ISNULL(CONVERT(VARCHAR(20),TRAILER_NO),'') TRAILER_NUMBER,                        
  CASE ISNULL(TRAILER_NO,'')                         
  WHEN '' THEN ''                        
  ELSE 'Trailer'                        
  END TRAILERTYPE,                         
  CONVERT(VARCHAR,CONVERT(MONEY,INSURED_VALUE),1) TRAILER_LIMIT,  
  CONVERT(VARCHAR,CONVERT(MONEY,TRAILER_DED),1) TRAILER_DED, TRAILER_DED_AMOUNT_TEXT     
  FROM POL_WATERCRAFT_TRAILER_INFO with (nolock)            
  WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLID AND POLICY_VERSION_ID = @VERSIONID             
  and             
  ASSOCIATED_BOAT=@BOATID            
  and POL_WATERCRAFT_TRAILER_INFO.is_active='Y'            
  ORDER BY TRAILER_NO            
 END             
            
              
END             
            
            
            
SET QUOTED_IDENTIFIER OFF             
            
          
        
        
      
    
  




GO

