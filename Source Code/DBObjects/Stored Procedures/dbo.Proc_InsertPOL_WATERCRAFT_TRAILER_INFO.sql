IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_WATERCRAFT_TRAILER_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_WATERCRAFT_TRAILER_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertPOL_WATERCRAFT_TRAILER_INFO          
Created by      : Vijay Arora      
Date            : 28-11-2005      
Purpose        : Inserting data in POL_WATERCRAFT_TRAILER_INFO          
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- DROP PROC Dbo.Proc_InsertPOL_WATERCRAFT_TRAILER_INFO          
CREATE  PROC Dbo.Proc_InsertPOL_WATERCRAFT_TRAILER_INFO          
(          
@CUSTOMER_ID     int,          
@POLICY_ID     int,          
@POLICY_VERSION_ID     smallint,          
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
@TRAILER_TYPE smallint ,  
@TRAILER_DED DECIMAL,
@TRAILER_DED_ID INT,
@TRAILER_DED_AMOUNT_TEXT NVARCHAR(200)           
)          
AS          
BEGIN          
          
declare @TRAILERID int           
select @TRAILERID=ISNULL(max(TRAILER_ID),0)+1 from POL_WATERCRAFT_TRAILER_INFO  where CUSTOMER_ID=@CUSTOMER_ID       
AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID          
         
          
Declare @Count int          
 Set @Count= (SELECT count(TRAILER_NO) FROM POL_WATERCRAFT_TRAILER_INFO          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND TRAILER_NO=@TRAILER_NO)          
          
 if (@Count=0)          
  BEGIN          
 INSERT INTO POL_WATERCRAFT_TRAILER_INFO          
 (          
  CUSTOMER_ID,          
  POLICY_ID,          
  POLICY_VERSION_ID,          
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
  @POLICY_ID,          
  @POLICY_VERSION_ID,          
  @TRAILERID,          
  @TRAILER_NO,          
  @YEAR,    
  @MODEL,          
  @MANUFACTURER,          
  @SERIAL_NO,          
  @INSURED_VALUE,          
  @ASSOCIATED_BOAT,          
  @IS_ACTIVE,          
  @CREATED_BY,          
  @CREATED_DATETIME,    
  @TRAILER_TYPE  ,  
  @TRAILER_DED ,
  @TRAILER_DED_ID   ,
  @TRAILER_DED_AMOUNT_TEXT 
 )          
           
SET @TRAILER_ID = @TRAILERID      
END      
ELSE          
 BEGIN          
  /*Record already exist*/          
  SELECT @TRAILER_ID = -1          
 END          
END        
        
        
        
        
        
        
        
        
        
      
    
    
    
    
  



GO

