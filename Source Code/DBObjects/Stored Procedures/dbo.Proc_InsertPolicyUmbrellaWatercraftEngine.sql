IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyUmbrellaWatercraftEngine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyUmbrellaWatercraftEngine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name        : dbo.Proc_InsertPolicyUmbrellaWatercraftEngine      
Created by       : Sumit Chhabra  
Date             : 21-03-2006    
Purpose         : Insert Policy Umbrella WaterCraft Engine Info    
Revison History  :      
Used In          : Wolverine      
------------------------------------------------------------      
      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_InsertPolicyUmbrellaWatercraftEngine    
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
 @OTHER     nvarchar(100),      
 @CREATED_BY     int,      
 @CREATED_DATETIME     datetime      
)      
AS      
BEGIN      
      
     
Declare @Count int      
 Set @Count= (SELECT count(ENGINE_NO) FROM POL_UMBRELLA_WATERCRAFT_ENGINE_INFO      
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID and ENGINE_NO=@ENGINE_NO)      
      
 if (@Count=0)      
 BEGIN      
 select @ENGINE_ID=isnull(max(ENGINE_ID),0)+1 from  POL_UMBRELLA_WATERCRAFT_ENGINE_INFO       
 WHERE       
  CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID      
    
 INSERT INTO POL_UMBRELLA_WATERCRAFT_ENGINE_INFO      
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

