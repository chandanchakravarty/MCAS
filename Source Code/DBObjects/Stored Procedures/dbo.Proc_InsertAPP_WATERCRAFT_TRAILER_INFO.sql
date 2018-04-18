IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_WATERCRAFT_TRAILER_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_WATERCRAFT_TRAILER_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.InsertAPP_WATERCRAFT_TRAILER_INFO            
Created by      : Anurag Verma            
Date            : 5/18/2005            
Purpose       :inserting data in APP_WATERCRAFT_TRAILER_INFO            
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
-- DROP PROC dbo.Proc_InsertAPP_WATERCRAFT_TRAILER_INFO                
CREATE  PROC dbo.Proc_InsertAPP_WATERCRAFT_TRAILER_INFO            
(            
@CUSTOMER_ID     int,            
@APP_ID     int,            
@APP_VERSION_ID     smallint,            
@TRAILER_ID     smallint OUTPUT,            
@TRAILER_NO     int,            
@YEAR     int,            
@MODEL NVARCHAR(40),          
@MANUFACTURER     nvarchar(150),            
@SERIAL_NO     nvarchar(50),            
@INSURED_VALUE     decimal(9),            
@ASSOCIATED_BOAT     smallint,            
@IS_ACTIVE     nchar(2),            
@CREATED_BY     int,            
@CREATED_DATETIME     datetime,      
@TRAILER_TYPE smallint,  
@TRAILER_DED DECIMAL,
@TRAILER_DED_ID INT,
@TRAILER_DED_AMOUNT_TEXT NVARCHAR(200)     
)            
AS            
BEGIN            
            
declare @TRAILERID int             
select @TRAILERID=ISNULL(max(TRAILER_ID),0)+1 from APP_WATERCRAFT_TRAILER_INFO  where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
            
            
Declare @Count int            
 Set @Count= (SELECT count(TRAILER_NO) FROM APP_WATERCRAFT_TRAILER_INFO            
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and  TRAILER_NO=@TRAILER_NO)            
            
            
 if (@Count=0)            
  BEGIN            
 INSERT INTO APP_WATERCRAFT_TRAILER_INFO            
 (            
  CUSTOMER_ID,            
  APP_ID,            
  APP_VERSION_ID,            
  TRAILER_ID,            
  TRAILER_NO,            
  YEAR,            
  MODEL,          
  MANUFACTURER,            
  SERIAL_NO,            
            
  INSURED_VALUE,            
  ASSOCIATED_BOAT,            
  IS_ACTIVE,            
  CREATED_BY,            
  CREATED_DATETIME,      
  TRAILER_TYPE ,  
  TRAILER_DED,
  TRAILER_DED_ID   ,
  TRAILER_DED_AMOUNT_TEXT            
 )            
 VALUES            
 (            
  @CUSTOMER_ID,            
  @APP_ID,            
  @APP_VERSION_ID,            
  @TRAILERID,            
  @TRAILER_NO,            
  @YEAR,            
  @MODEL,        
  @MANUFACTURER,            
  @SERIAL_NO,            
          
  @INSURED_VALUE,            
  @ASSOCIATED_BOAT,            
  'Y',            
  @CREATED_BY,            
  @CREATED_DATETIME,      
  @TRAILER_TYPE,  
  @TRAILER_DED,
  @TRAILER_DED_ID   ,
  @TRAILER_DED_AMOUNT_TEXT 
 )            
             
 SELECT @TRAILERID=ISNULL(MAX(TRAILER_ID),-1) FROM APP_WATERCRAFT_TRAILER_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
 SET @TRAILER_ID=@TRAILERID            
 END            
ELSE            
 BEGIN            
  /*Record already exist*/            
  SELECT @TRAILER_ID = -1            
 END            
END          
          
          
          
          
          
          
          
          
          
        
      
    
    
    
  



GO

