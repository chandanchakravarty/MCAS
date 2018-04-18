IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETAPPLICANTEMAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETAPPLICANTEMAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Proc_GetApplicantEmail 511  
  
  
/*----------------------------------------------------------              
Proc Name  : dbo.Proc_GetApplicantEmail             
Created by       : Pravesh Chandel              
Date             : 06/11/2006             
Purpose       : Fetch the E-Mail addresses of               
Revison History :              
Used In   : Wolverine              
DROP PROC Proc_GetApplicantEmail             
------------------------------------------------------------              
Date      Review By          Comments              
03/22/2007 Shailja Rampal    Changes done to show email along with name for CO-APPLICANTS    
    
DROP PROC Proc_GetApplicantEmail   
------   ------------       -------------------------*/              
CREATE PROC dbo.PROC_GETAPPLICANTEMAIL             
(              
 @CUSTOMER_ID  INT          
)              
AS              
BEGIN              
 -- CUSTOMER    
 SELECT CUSTOMER_FIRST_NAME + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER_LAST_NAME,'') + ' ' + '(' + CUSTOMER_EMAIL + ')' AS CUSTOMER_NAME, CUSTOMER_EMAIL          
 FROM CLT_CUSTOMER_LIST  WHERE  CUSTOMER_ID = @CUSTOMER_ID    
      
 UNION     
 -- CO-APPLICANTS    
 SELECT FIRST_NAME + ' ' + ISNULL(MIDDLE_NAME,'') + ' ' + ISNULL(LAST_NAME,'') + ' ' + '(' + EMAIL + ')' AS CUSTOMER_NAME, EMAIL AS CUSTOMER_EMAIL      
 FROM  CLT_APPLICANT_LIST  WHERE CUSTOMER_ID = @CUSTOMER_ID          
--      
--   UNION    
--  --CSR    
--  SELECT DISTINCT USER_FNAME + ' ' + USER_LNAME + '(' + USER_EMAIL + ')' AS CUSTOMER_NAME,USER_EMAIL CUSTOMER_EMAIL  FROM MNT_USER_LIST UL INNER JOIN    
--  APP_LIST AL ON AL.CSR=UL.USER_ID    
--  WHERE AL.CUSTOMER_ID=@CUSTOMER_ID    
 -- USER    
 UNION    
    
 SELECT USER_FNAME + ' ' + USER_LNAME + ' ' + '(' + USER_EMAIL + ')' AS CUSTOMER_NAME,MUL.USER_EMAIL FROM CLT_CUSTOMER_LIST CUST INNER JOIN MNT_AGENCY_LIST AGN     
 ON CUST.CUSTOMER_AGENCY_ID  = AGN.AGENCY_ID    
 INNER JOIN MNT_USER_LIST  MUL ON AGN.AGENCY_CODE = MUL.USER_SYSTEM_ID    
 WHERE CUST.CUSTOMER_ID = @Customer_ID    
    
 UNION    
 --PRODUCER    
 SELECT DISTINCT USER_FNAME + ' ' + USER_LNAME + ' ' + '(' + USER_EMAIL + ')' AS CUSTOMER_NAME,USER_EMAIL CUSTOMER_EMAIL  FROM MNT_USER_LIST UL INNER JOIN    
 APP_LIST AL ON AL.PRODUCER=UL.USER_ID    
 WHERE AL.CUSTOMER_ID=@CUSTOMER_ID    
    
--     
 -- GET CSR FOR RECIPIENTS LISTBOX    
 --CSR    
 SELECT DISTINCT USER_FNAME + ' ' + USER_LNAME + ' ' + '(' + USER_EMAIL + ')' AS CUSTOMER_NAME,USER_EMAIL CUSTOMER_EMAIL  FROM MNT_USER_LIST UL INNER JOIN    
 APP_LIST AL ON AL.CSR=UL.USER_ID    
 WHERE AL.CUSTOMER_ID=@CUSTOMER_ID    
    
  
END          
    
    
    
    
    
    
  
  
  



GO

