IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWatercraftEngineInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWatercraftEngineInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Stored Procedure dbo.Proc_InsertWatercraftEngineInfo    Script Date: 5/24/2006 10:42:18 AM ******/
--//DO
/*----------------------------------------------------------  
Proc Name       : dbo.WatercraftEngineInfo  
Created by      : Nidh  
Date            : 5/18/2005  
Purpose       :Insert Engine Info  
Revison History :  
Used In        : Wolverine  
  
Modified By : Anurag Verma  
Modified On : Oct 11,2005  
Purpose  : Removing Fuel_type,limit_desired,deductible,premium,current_value and adding insuring_value in query   
  
  
------------------------------------------------------------  
  
Date     Review By          Comments  
------   ------------       -------------------------*/ 
-- drop proc dbo.Proc_InsertWatercraftEngineInfo  
CREATE     PROC dbo.Proc_InsertWatercraftEngineInfo  
(  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@ENGINE_ID     smallint output,  
@ENGINE_NO     nvarchar(20),  
@YEAR     int,  
@MAKE     nvarchar(75),  
@MODEL     nvarchar(75),  
@SERIAL_NO     nvarchar(75),  
@HORSEPOWER     nvarchar(5),  
@INSURING_VALUE     decimal(12,2)=null,  
@ASSOCIATED_BOAT     smallint,
@FUEL_TYPE int,  
@OTHER     nvarchar(100),  
@CREATED_BY     int,  
@CREATED_DATETIME     datetime,
@IS_ACTIVE nvarchar(1)
  
)  
AS  
BEGIN  
  
  
  
  
Declare @Count int  
 Set @Count= (SELECT count(ENGINE_NO) FROM APP_WATERCRAFT_ENGINE_INFO  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and ENGINE_NO=@ENGINE_NO and ASSOCIATED_BOAT = @ASSOCIATED_BOAT)  
  
  
 if (@Count=0)  
 BEGIN  
 select @ENGINE_ID=isnull(max(ENGINE_ID),0)+1 from  APP_WATERCRAFT_ENGINE_INFO   
 WHERE   
  CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
 INSERT INTO APP_WATERCRAFT_ENGINE_INFO  
 (  
  CUSTOMER_ID,  
  APP_ID,  
  APP_VERSION_ID,  
  ENGINE_ID,  
  ENGINE_NO,  
  YEAR,  
  MAKE,  
  MODEL,  
  SERIAL_NO,  
  HORSEPOWER,  
  INSURING_VALUE,  
  ASSOCIATED_BOAT,
  FUEL_TYPE,  
  OTHER,  
  CREATED_BY,  
  CREATED_DATETIME,
  IS_ACTIVE
 )  
 VALUES  
 (  
  @CUSTOMER_ID,  
  @APP_ID,  
  @APP_VERSION_ID,  
  @ENGINE_ID,  
  @ENGINE_NO,  
  @YEAR,  
  @MAKE,  
  @MODEL,  
  @SERIAL_NO,  
  @HORSEPOWER,  
  @INSURING_VALUE,  
  @ASSOCIATED_BOAT,
  @FUEL_TYPE,
  @OTHER,  
  @CREATED_BY,  
  @CREATED_DATETIME,
  @IS_ACTIVE 
 )  
END  
   
ELSE  
   
 BEGIN  
  select @ENGINE_ID =0  	
  return -1   
 END  
END  
  
  
  



GO

