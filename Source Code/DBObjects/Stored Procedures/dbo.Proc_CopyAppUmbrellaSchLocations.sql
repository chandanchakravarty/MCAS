IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyAppUmbrellaSchLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyAppUmbrellaSchLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name   : dbo.Proc_CopyAppUmbrellaSchLocations                
Created by  : Sumit Chhabra      
Date        : 09 Oc,2006                
Purpose     : Copy locations at umbrella sch.       
Revison History  :                                
 ------------------------------------------------------------                                      
Date     Review By          Comments                                    
                           
------   ------------       -------------------------*/                          
CREATE PROCEDURE dbo.Proc_CopyAppUmbrellaSchLocations                
(                
@CUSTOMER_ID int,      
@APP_ID int,      
@APP_VERSION_ID smallint,      
@ADDRESS_1 nvarchar(75),      
@ADDRESS_2 nvarchar(75),      
@CITY nvarchar(75),      
@STATE int,      
@ZIPCODE nvarchar(10),      
@PHONE_NUMBER nvarchar(15),      
@FAX_NUMBER nvarchar(15),      
@CREATED_BY int,
@COUNTY NVARCHAR(75)
--,@BUSS_FARM_PURSUITS_DESC varchar(50)
)                    
AS                         
BEGIN                          
      
declare @LOCATION_ID smallint      
declare @LOCATION_NUMBER int  
SELECT @LOCATION_ID = ISNULL(MAX(LOCATION_ID),0)+1,@LOCATION_NUMBER = ISNULL(MAX(LOCATION_NUMBER),0)+1 FROM APP_UMBRELLA_REAL_ESTATE_LOCATION WHERE      
 CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID = @APP_VERSION_ID      
      
insert into APP_UMBRELLA_REAL_ESTATE_LOCATION      
(      
CUSTOMER_ID,      
APP_ID,      
APP_VERSION_ID,    
LOCATION_ID,    
ADDRESS_1,      
ADDRESS_2,      
CITY,      
STATE,      
ZIPCODE,      
PHONE_NUMBER,      
FAX_NUMBER,      
CREATED_BY,      
CREATED_DATETIME,      
LOCATION_NUMBER,      
IS_ACTIVE,
COUNTY
--,BUSS_FARM_PURSUITS_DESC
)      
values      
(      
@CUSTOMER_ID,      
@APP_ID,      
@APP_VERSION_ID,     
@LOCATION_ID,     
@ADDRESS_1,      
@ADDRESS_2,      
@CITY,      
@STATE,      
@ZIPCODE,      
@PHONE_NUMBER,      
@FAX_NUMBER,      
@CREATED_BY,      
GetDate(),    
@LOCATION_NUMBER,      
'Y',
@COUNTY
--,@BUSS_FARM_PURSUITS_DESC

)      
              
END  



GO

