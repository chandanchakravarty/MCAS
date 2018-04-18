IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERTPOL_IM_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERTPOL_IM_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
CREATE PROC dbo.PROC_INSERTPOL_IM_SCH_ITEMS_CVGS      
(      
  @CUSTOMER_ID     INT,      
  @POL_ID        INT,      
  @POL_VERSION_ID  INT,      
  @ITEM_ID        INT,      
  @CREATED_BY       INT,      
  @CREATED_DATETIME      DATETIME,      
  @DEDUCTIBLE   DECIMAL(18,2)     
)      
AS      
  
BEGIN  
IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      POLICY_ID =  @POL_ID AND
      POLICY_VERSION_ID =  @POL_VERSION_ID AND
      ITEM_ID=@ITEM_ID )
BEGIN        
  INSERT INTO POL_HOME_OWNER_SCH_ITEMS_CVGS      
  (      
    CUSTOMER_ID, Policy_ID, Policy_VERSION_ID, ITEM_ID,IS_ACTIVE,CREATED_BY, CREATED_DATETIME,DEDUCTIBLE         
	
  )      
  VALUES      
  (      
    @CUSTOMER_ID, @POL_ID, @POL_VERSION_ID, @ITEM_ID, 'Y', @CREATED_BY, @CREATED_DATETIME,@DEDUCTIBLE      
  )      
END 
ELSE
BEGIN
UPDATE POL_HOME_OWNER_SCH_ITEMS_CVGS SET
	DEDUCTIBLE=@DEDUCTIBLE
	WHERE 
	CUSTOMER_ID = @CUSTOMER_ID AND
	POLICY_ID =  @POL_ID AND
	POLICY_VERSION_ID =  @POL_VERSION_ID AND
	ITEM_ID=@ITEM_ID

END 
end
  
  







GO

