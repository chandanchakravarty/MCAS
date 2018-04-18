IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTransactionIdCoinsurance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTransactionIdCoinsurance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : Proc_UpdateTransactionIdCoinsurance  
Created by      : PRAVEER PANGHAL         
Date            : 15/03/2011                      
Purpose         : Update transaction id on endorsement Commit  
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC Proc_UpdateTransactionIdCoinsurance
*/       
CREATE PROC [dbo].[Proc_UpdateTransactionIdCoinsurance]
(  
@CUSTOMER_ID INT,  
@POLICY_ID INT   ,  
@POLICY_VERSION_ID SMALLINT
)
AS

DECLARE @CO_INSURANCE INT,@LEADER INT= 14548, @sequenceNumber decimal(15,0),
 @COMPANY_ID INT, @COUNT int, @COUNTER INT =0  
 
	 SELECT @CO_INSURANCE = CO_INSURANCE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
	  WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID  = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
	 
	  SELECT @sequenceNumber= REIN_SEQUENCE_NUMBER from POL_CO_INSURANCE PCI WITH(NOLOCK) 
	 LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON PCI.COMPANY_ID=MRCL.REIN_COMAPANY_ID 
	WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID  = @POLICY_VERSION_ID
BEGIN


IF(@CO_INSURANCE=@LEADER)	
BEGIN

 --update POL_CO_INSURANCE set TRANSACTION_ID=@sequenceNumber
 --  WHERE CUSTOMER_ID = @CUSTOMER_ID 
 --AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID  = @POLICY_VERSION_ID 
 
  CREATE TABLE #TEMP_TABLE_2  
 (   ID INT IDENTITY(1,1), companyId int, transactionId decimal(15,0)  )  
   
   insert into #TEMP_TABLE_2
   (  companyId, transactionId )   
 
	 SELECT REIN_COMAPANY_ID,REIN_SEQUENCE_NUMBER from POL_CO_INSURANCE PCI WITH(NOLOCK) 
	 LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON PCI.COMPANY_ID=MRCL.REIN_COMAPANY_ID 
	  WHERE   CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID
  
 
	 select @COUNT= COUNT(*) from #TEMP_TABLE_2
	 SELECT @sequenceNumber= REIN_SEQUENCE_NUMBER,@COMPANY_ID= COMPANY_ID from POL_CO_INSURANCE PCI WITH(NOLOCK) 
	 LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK) ON PCI.COMPANY_ID=MRCL.REIN_COMAPANY_ID 
	  WHERE   CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID

    SET @COUNTER=1
while( @count>0)
BEGIN
	select @COMPANY_ID=companyId,@sequenceNumber=transactionId from #TEMP_TABLE_2 WHERE ID =@COUNTER 
	UPDATE POL_CO_INSURANCE
	SET TRANSACTION_ID = @sequenceNumber
	WHERE COMPANY_ID =@COMPANY_ID AND  
	 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID
	  and LEADER_FOLLOWER=14549
	SET @sequenceNumber = @sequenceNumber+1 
	UPDATE MNT_REIN_COMAPANY_LIST 
	SET REIN_SEQUENCE_NUMBER=@sequenceNumber
	WHERE REIN_COMAPANY_ID=@COMPANY_ID
	SET @COUNTER = @COUNTER +1  
	SET @COUNT=@COUNT-1          
   END 
 DROP TABLE #TEMP_TABLE_2  

END
END
GO

