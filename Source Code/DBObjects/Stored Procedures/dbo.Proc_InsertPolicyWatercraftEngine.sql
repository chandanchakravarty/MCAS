IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name        : dbo.Proc_InsertPolicyWatercraftEngine    
Created by       : Vijay Arora  
Date             : 22-11-2005  
Purpose         : Insert Policy WaterCraft Engine Info  
Revison History  :    
Used In          : Wolverine    
------------------------------------------------------------    
    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_InsertPolicyWatercraftEngine  
CREATE PROC dbo.Proc_InsertPolicyWatercraftEngine  
(    
 @CUSTOMER_ID     int,    
 @POLICY_ID     int,    
 @POLICY_VERSION_ID     smallint,    
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
 @CREATED_DATETIME     datetime    
)    
AS    
BEGIN    
    
   
Declare @Count int    
 Set @Count= (SELECT count(ENGINE_NO) FROM POL_WATERCRAFT_ENGINE_INFO    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and ASSOCIATED_BOAT = @ASSOCIATED_BOAT AND ENGINE_NO=@ENGINE_NO)    
    
 if (@Count=0)    -- ENGINE_NO IS UNIQUE
     BEGIN    
	 select @ENGINE_ID=isnull(max(ENGINE_ID),0)+1 from  POL_WATERCRAFT_ENGINE_INFO     
	 WHERE     
	  CUSTOMER_ID = @CUSTOMER_ID AND    
	  POLICY_ID = @POLICY_ID AND    
	  POLICY_VERSION_ID = @POLICY_VERSION_ID    
	  
	 INSERT INTO POL_WATERCRAFT_ENGINE_INFO    
	 (    
	  CUSTOMER_ID,    
	  POLICY_ID,    
	  POLICY_VERSION_ID,    
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
	  @POLICY_ID,    
	  @POLICY_VERSION_ID,    
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
	  'Y'    
	 )    
   END    
     
ELSE    
	     
	 BEGIN    
	  return -1     
	 END    
END    
    
    
    


GO

