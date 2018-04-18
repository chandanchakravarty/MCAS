IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_WATERCRAFT_TRAILER_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_WATERCRAFT_TRAILER_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.UpdateAPP_WATERCRAFT_TRAILER_INFO        
Created by      : Anurag Verma        
Date            : 5/18/2005        
Purpose       :updating data in APP_WATERCRAFT_TRAILER_INFO        
Revison History :        
Used In        : Wolverine        
        
Modified By : Anurag Verma        
Modified On : 10/10/2005        
Purpose  : Adding Insured Value and removing trailer_value,cost_new,limit_desired,deductible and premium value    
  
Modified By : Asfa Praveen  
Modified On : 13/June/2007  
Purpose  : Adding Trailer_Ded field foe Trailer Deductible information      
        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- DROP PROC dbo.Proc_UpdateAPP_WATERCRAFT_TRAILER_INFO        
CREATE PROC dbo.Proc_UpdateAPP_WATERCRAFT_TRAILER_INFO        
(        
@CUSTOMER_ID     int,        
@APP_ID     int,        
@APP_VERSION_ID     smallint,        
@TRAILER_ID     smallint ,        
@TRAILER_NO     int,        
@YEAR     int,        
@MODEL NVARCHAR(40),       
@MANUFACTURER     nvarchar(150),        
@SERIAL_NO     nvarchar(50),        
        
@INSURED_VALUE     decimal(9),        
@ASSOCIATED_BOAT     smallint,        
@MODIFIED_BY     int,        
@LAST_UPDATED_DATETIME     datetime,      
@TRAILER_TYPE smallint ,  
@TRAILER_DED DECIMAL,
@TRAILER_DED_ID INT,
@TRAILER_DED_AMOUNT_TEXT NVARCHAR(200)   
)        
AS        
BEGIN        
Declare @Count int        
 Set @Count= (SELECT count(TRAILER_NO) FROM APP_WATERCRAFT_TRAILER_INFO        
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and  TRAILER_NO=@TRAILER_NO and TRAILER_ID<>@TRAILER_ID)        
        
        
if (@Count=0)        
  BEGIN        
  UPDATE APP_WATERCRAFT_TRAILER_INFO        
  SET        
   TRAILER_NO=@TRAILER_NO,        
   YEAR=@YEAR,        
   MODEL = @MODEL,        
   MANUFACTURER=@MANUFACTURER,        
   SERIAL_NO=@SERIAL_NO,        
           
   INSURED_VALUE=@INSURED_VALUE,        
   ASSOCIATED_BOAT=@ASSOCIATED_BOAT,        
   MODIFIED_BY=@MODIFIED_BY,        
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,      
   TRAILER_TYPE=@TRAILER_TYPE ,  
   TRAILER_DED = @TRAILER_DED,
   TRAILER_DED_ID = @TRAILER_DED_ID  ,
   TRAILER_DED_AMOUNT_TEXT = @TRAILER_DED_AMOUNT_TEXT
  WHERE        
   CUSTOMER_ID=@CUSTOMER_ID AND        
   APP_ID=@APP_ID AND        
   APP_VERSION_ID=@APP_VERSION_ID AND        
   TRAILER_ID=@TRAILER_ID        
        
          
 END        
ELSE        
 BEGIN        
  /*Record already exist*/        
  return  -1        
 END        
        
END        
        
        
        
      
    
    
    
  



GO

