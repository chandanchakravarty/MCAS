IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_WATERCRAFT_TRAILER_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_WATERCRAFT_TRAILER_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : Dbo.Proc_INSERT_WATERCRAFT_ACORD                          
Created by      : Praveen kasana        
Date            : 10 Feb 2006        
Purpose         :Insert a Trailer information from Quick quote                         
Revison History :                          
Used In        : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/               
        
        
                       
CREATE    PROC Dbo.Proc_INSERT_WATERCRAFT_TRAILER_ACORD                          
(                          
 @CUSTOMER_ID     int,                          
 @APP_ID     int,                          
 @APP_VERSION_ID     smallint,                          
 @TRAILER_ID   smallint output,                          
 @TRAILER_NO     int,                          
 @YEAR     int,           
 @MANUFACTURER  nvarchar(75),         
 @SERIAL_NO     nvarchar(25),    
 @MODEL     nvarchar(40) = null,     
 @ASSOCIATED_BOAT smallint,        
 @CREATED_BY     int,                          
 @CREATED_DATETIME     datetime,             
 @INSURED_VALUE decimal(10,2) ,                     
 @TRAILER_TYPE_CODE char(10),  

 @TRAILER_DED  decimal(9)=null,
 @TRAILER_DED_ID int=null,
 @TRAILER_DED_AMOUNT_TEXT nvarchar(400)=null              
                   
                          
)                          
AS                          
BEGIN                          
                     
   DECLARE @TRAILER_OF_TYPE int               
                     
   --Trailer Type                  
   EXECUTE @TRAILER_OF_TYPE = Proc_GetLookupID 'WCTCD',@TRAILER_TYPE_CODE                                                  
                     
          
                 
   IF ( @@ERROR <> 0 )                
   BEGIN                
 RETURN                
   END                  
                
   --EXECUTE @WAT_NAV = Proc_GetLookupID 'WNVC',@WATERS_NAVIGATED                                                   
                    
   --print('2')                
                  
   --IF ( @@ERROR <> 0 )                
   --BEGIN                
 --RETURN                
   --END                  
                 
     -- print('3')                
                
   --EXECUTE @HULL_MATERIAL = Proc_GetLookupID '%HULL',@HULL_MATERIAL_CODE                   
                    
  --IF ( @@ERROR <> 0 )                
   --BEGIN                
 --RETURN                
   --END                  
                  
 select @TRAILER_ID=isnull(max(TRAILER_ID),0)+1                   
 from APP_WATERCRAFT_TRAILER_INFO WHERE                    
   CUSTOMER_ID = @CUSTOMER_ID AND                          
   APP_ID = @APP_ID AND                          
   APP_VERSION_ID = @APP_VERSION_ID                  
                          
   INSERT INTO APP_WATERCRAFT_TRAILER_INFO                          
   (                          
     CUSTOMER_ID,                          
     APP_ID,                          
     APP_VERSION_ID,                          
     TRAILER_ID,                          
     TRAILER_NO,                          
     YEAR,                          
     MANUFACTURER,          
     SERIAL_NO,
     MODEL,  	        
     ASSOCIATED_BOAT,        
     IS_ACTIVE,                          
     CREATED_BY,                          
     CREATED_DATETIME,    
     INSURED_VALUE,    
     TRAILER_TYPE ,  
     TRAILER_DED,
     TRAILER_DED_ID, 
     TRAILER_DED_AMOUNT_TEXT       
                      
                          
             
   )                          
   VALUES                          
   (                          
     @CUSTOMER_ID,          
     @APP_ID,                          
     @APP_VERSION_ID,                          
     @TRAILER_ID,                          
     @TRAILER_ID,                          
     @YEAR,                          
     @MANUFACTURER,          
     @SERIAL_NO,
     @MODEL,        
     @ASSOCIATED_BOAT,        
     'Y',        
     @CREATED_BY,                
     @CREATED_DATETIME,    
     @INSURED_VALUE,    
     Convert(numeric,@TRAILER_TYPE_CODE) ,  
     @TRAILER_DED ,     
     @TRAILER_DED_ID ,
     @TRAILER_DED_AMOUNT_TEXT               
                     
   )                          
                    
END              
            
          
        
      
    
    
    
  





GO

