IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_FETCH_RECON_AMOUNT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_FETCH_RECON_AMOUNT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.PROC_FETCH_RECON_AMOUNT
--go 
/*----------------------------------------------------------
Proc Name       : PROC_FETCH_RECON_AMOUNT
Created by      : Raghav
Date            : 28/05/2009
Purpose    	: TO pick recon_amount and customername  for transaction log.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       ------------- 
------------*/
--drop proc dbo.PROC_FETCH_RECON_AMOUNT
CREATE PROC DBO.PROC_FETCH_RECON_AMOUNT 
(
 @IDEN_ROW_NO INT  ,
 @ENTITY_TYPE VARCHAR(10),
 @ENTITY_TYPE_ID int = null
)
AS  
BEGIN   
    IF(@ENTITY_TYPE = 'CUST')
 BEGIN
			IF(@IDEN_ROW_NO <> 0) --UPDATE
			BEGIN
				SELECT 
				ISNULL(RECON_AMOUNT,0.0) AS  RECON_AMOUNT
				FROM ACT_CUSTOMER_RECON_GROUP_DETAILS CUST WITH(NOLOCK)       
				WHERE IDEN_ROW_NO =  @IDEN_ROW_NO  
			END
ELSE --SAVE 
			BEGIN         
				SELECT 
				ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' 
				+ ISNULL(CUSTOMER_LAST_NAME,'') 
				AS ENTITY_NAME 
				FROM CLT_CUSTOMER_LIST  WITH(NOLOCK)       
				WHERE CUSTOMER_ID =  @ENTITY_TYPE_ID 				
			END
		
       
 END  


ELSE IF(@ENTITY_TYPE = 'AGN') 
 BEGIN
         IF(@IDEN_ROW_NO <> 0)
			BEGIN
			   SELECT ISNULL(RECON_AMOUNT,0.0) AS RECON_AMOUNT 			
			   FROM ACT_AGENCY_RECON_GROUP_DETAILS ARGP WITH(NOLOCK) 			   			                   
			   WHERE IDEN_ROW_NO = @IDEN_ROW_NO
			END

ELSE
           BEGIN 
              SELECT 
			  ISNULL(AGENCY_DISPLAY_NAME , '') AS ENTITY_NAME   
			  FROM MNT_AGENCY_LIST  WITH(NOLOCK) 
              WHERE AGENCY_ID = @ENTITY_TYPE_ID 
           END

END
   
ELSE IF(@ENTITY_TYPE = 'VEN')
BEGIN
	 
    IF(@IDEN_ROW_NO <> 0)
      
       BEGIN          
          SELECT ISNULL(RECON_AMOUNT,0.0) AS RECON_AMOUNT		
		  FROM ACT_VENDOR_RECON_GROUP_DETAILS AVGD WITH(NOLOCK)
          WHERE IDEN_ROW_NO = @IDEN_ROW_NO 

      END
ELSE
   
      BEGIN
  
            SELECT 
	     	Isnull(COMPANY_NAME,'')  AS ENTITY_NAME 
    		FROM MNT_VENDOR_LIST AVGD WITH(NOLOCK)
            WHERE VENDOR_ID = @ENTITY_TYPE_ID 

      END  

 
    
END

END

--go
--exec PROC_FETCH_RECON_AMOUNT 27965, 'AGN'
--rollback tran 







GO

