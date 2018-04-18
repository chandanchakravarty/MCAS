IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       	: dbo.Proc_UpdatePolicyWatercraftEngine
Created by      	: Vijay Arora
Date            	: 22-11-2005
Purpose       		:Update Policy WaterCraft Engine Info  
Revison History 	:  
Used In        		: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_UpdatePolicyWatercraftEngine  
CREATE    PROC dbo.Proc_UpdatePolicyWatercraftEngine  
(  
	@CUSTOMER_ID     int,  
	@POLICY_ID     int,  
	@POLICY_VERSION_ID     smallint,  
	@ENGINE_ID     smallint,  
	@ENGINE_NO     nvarchar(20),  
	@YEAR     int,  
	@MAKE     nvarchar(75),  
	@MODEL     nvarchar(75),  
	@SERIAL_NO     nvarchar(75),  
	@HORSEPOWER     nvarchar(5),  
	@INSURING_VALUE     decimal(9,2)=null,  
	@ASSOCIATED_BOAT     smallint,
	@FUEL_TYPE int,  
	@OTHER     nvarchar(100),  
	@MODIFIED_BY int,  
	@LAST_UPDATED_DATETIME datetime  
  
)  
AS  
BEGIN  
Declare @Count int  
 Set @Count= (SELECT count(ENGINE_NO) FROM POL_WATERCRAFT_ENGINE_INFO  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and ENGINE_ID<>@ENGINE_ID and ENGINE_NO=@ENGINE_NO)  
 
  
if (@Count=0)  
  BEGIN  
 update  POL_WATERCRAFT_ENGINE_INFO  
 set   
 ENGINE_NO=@ENGINE_NO,  
 YEAR=@YEAR,  
 MAKE=@MAKE,  
 MODEL=@MODEL,  
 SERIAL_NO=@SERIAL_NO,  
 HORSEPOWER=@HORSEPOWER,  
 INSURING_VALUE=@INSURING_VALUE,  
 ASSOCIATED_BOAT=@ASSOCIATED_BOAT,
 FUEL_TYPE=@FUEL_TYPE,  
 OTHER=@OTHER,  
 MODIFIED_BY=@MODIFIED_BY,  
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
 where   
 CUSTOMER_ID=@CUSTOMER_ID AND  
 POLICY_ID=@POLICY_ID AND  
 POLICY_VERSION_ID =@POLICY_VERSION_ID AND  
 ENGINE_ID =@ENGINE_ID  
  
END  
else  
  
begin  
 return -1  
end  
end  
  
  
  





GO

