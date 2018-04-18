IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SavePOL_POL_CO_INSURANCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SavePOL_POL_CO_INSURANCE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC Proc_SavePOL_POL_CO_INSURANCE      
CREATE PROC [dbo].[Proc_SavePOL_POL_CO_INSURANCE]      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,      
@COMPANY_ID INT,      
@CO_INSURER_NAME NVARCHAR(50),      
@LEADER_FOLLOWER INT,      
@COINSURANCE_PERCENT DECIMAL(18,2),      
@COINSURANCE_FEE DECIMAL(18,2),      
@BROKER_COMMISSION DECIMAL(18,2),      
@TRANSACTION_ID NVARCHAR(25),      
@LEADER_POLICY_NUMBER NVARCHAR(50),      
@FLAG BIT,      
@COINSURANCE_ID INT = NULL,
@CREATED_BY INT,
@CREATED_DATETIME DATETIME ,
@Trans_Id   int   OUTPUT ,
@ENDORSEMENT_POLICY_NUMBER nvarchar(50) =NULL  
AS      
BEGIN      
 DECLARE @MAX_COINSURANCE_ID INT
 DECLARE @COINSURANCE_PERCENT_SUM decimal(18,2)       
 DECLARE @COINSURANCE_FEE_SUM decimal(18,2)          
 
     
  SELECT  @COINSURANCE_PERCENT_SUM=sum(COINSURANCE_PERCENT) + @COINSURANCE_PERCENT  
  FROM   POL_CO_INSURANCE PC   with (nolock) inner join POL_CUSTOMER_POLICY_LIST PCPL ON 
PC.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PC.POLICY_ID = PCPL.POLICY_ID AND PC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID
AND  PCPL.CO_INSURANCE = 14548 
   WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @POLICY_ID  and PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID
  and COINSURANCE_ID<>@COINSURANCE_ID 
  
--  SELECT  @COINSURANCE_FEE_SUM=sum(COINSURANCE_FEE) + @COINSURANCE_FEE 
--  FROM   POL_CO_INSURANCE PC  with (nolock) inner join POL_CUSTOMER_POLICY_LIST PCPL ON 
--PC.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PC.POLICY_ID = PCPL.POLICY_ID AND PC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID
--AND  PCPL.CO_INSURANCE = 14548 
--  WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @POLICY_ID  and PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID
--  and COINSURANCE_ID<>@COINSURANCE_ID 
      
SELECT @MAX_COINSURANCE_ID = ISNULL(MAX(COINSURANCE_ID),0) + 1 FROM POL_CO_INSURANCE  with (nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
    

    
  IF(@COINSURANCE_PERCENT_SUM > 100)     
   BEGIN            
    SET @Trans_Id = 4   
     END        
 --  ELSE
 --BEGIN	
 -- IF(@COINSURANCE_FEE_SUM > 100)     
 --  BEGIN            
 --   SET @Trans_Id = 6   
 --    END  
 
  ELSE
 BEGIN	
  IF  EXISTS(SELECT DISTINCT ISNULL(TRANSACTION_ID,'') FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE TRANSACTION_ID=@TRANSACTION_ID and TRANSACTION_ID<>''
  AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  and COINSURANCE_ID<>@COINSURANCE_ID  ) 
  BEGIN   
  SET @Trans_Id = 1      
  END
  
  

 --else IF  EXISTS(SELECT COMPANY_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE COMPANY_ID=@COMPANY_ID AND CUSTOMER_ID = @CUSTOMER_ID         AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND COINSURANCE_PERCENT <>NULL) 
 --BEGIN   
 -- SET @Trans_Id = 5     
 -- END
 
 
 
else
 IF @FLAG = 0    
 
 
 
 BEGIN  
 --  IF NOT EXISTS(SELECT TRANSACTION_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE TRANSACTION_ID=@TRANSACTION_ID )   
   BEGIN
   IF(@BROKER_COMMISSION = 0 )
    SELECT @BROKER_COMMISSION = null
  INSERT INTO POL_CO_INSURANCE (COINSURANCE_ID,COMPANY_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CO_INSURER_NAME,LEADER_FOLLOWER,COINSURANCE_PERCENT,      
  COINSURANCE_FEE,BROKER_COMMISSION,TRANSACTION_ID,LEADER_POLICY_NUMBER,CREATED_BY,CREATED_DATETIME,IS_ACTIVE,
  ENDORSEMENT_POLICY_NUMBER)      
  VALUES      
  (@MAX_COINSURANCE_ID, @COMPANY_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @CO_INSURER_NAME, @LEADER_FOLLOWER, @COINSURANCE_PERCENT,      
  @COINSURANCE_FEE,@BROKER_COMMISSION,@TRANSACTION_ID,@LEADER_POLICY_NUMBER,@CREATED_BY,@CREATED_DATETIME,'Y',
  @ENDORSEMENT_POLICY_NUMBER)      
    END  
    SET @Trans_Id = 2        
 END

  
     
           
 ELSE      
 BEGIN    
  
 -- IF NOT EXISTS(SELECT TRANSACTION_ID FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE TRANSACTION_ID=@TRANSACTION_ID AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   ) 
  BEGIN
  IF(@BROKER_COMMISSION = 0 )
  BEGIN
    SELECT @BROKER_COMMISSION = null  
  UPDATE POL_CO_INSURANCE SET CO_INSURER_NAME = @CO_INSURER_NAME, LEADER_FOLLOWER = @LEADER_FOLLOWER, COINSURANCE_PERCENT = @COINSURANCE_PERCENT,      
  COINSURANCE_FEE = @COINSURANCE_FEE, BROKER_COMMISSION = @BROKER_COMMISSION, TRANSACTION_ID = @TRANSACTION_ID, LEADER_POLICY_NUMBER = @LEADER_POLICY_NUMBER 
  ,MODIFIED_BY=@CREATED_BY ,ENDORSEMENT_POLICY_NUMBER=@ENDORSEMENT_POLICY_NUMBER 
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID       
  AND COMPANY_ID = @COMPANY_ID AND COINSURANCE_ID=@COINSURANCE_ID  
	SET @Trans_Id = 3
  END 
 
 END
 END
 END
 END
 


GO

