IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinsurers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinsurers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name        : dbo.[Proc_GetReinsurers]                              
Created by       : Chetna Agarwal                            
Date             : 20-04-2010                              
Purpose          : retrieving data from MNT_REIN_COMAPANY_LIST                                                      
Used In          : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.[Proc_GetReinsurers]  27987,121,1  
CREATE PROC [dbo].[Proc_GetReinsurers]     
(      
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,          
 @POLICY_VERSION_ID INT     
)         
AS          
BEGIN      
    
 Declare @FROMEFFECTIVEDATE datetime    
   Declare  @TOEFFECTIVEDATE datetime    
       
  select @FROMEFFECTIVEDATE=isnull(POLICY_EFFECTIVE_DATE,APP_EFFECTIVE_DATE),    
         @TOEFFECTIVEDATE=isnull(POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE)    
  from POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  where CUSTOMER_ID = @CUSTOMER_ID    
  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID    
        
 SELECT REIN_COMAPANY_ID, REIN_COMAPANY_NAME FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'Y')='Y'     
 AND REIN_COMPANY_TYPE = '13054' AND   
 (@FROMEFFECTIVEDATE>=MNT_REIN_COMAPANY_LIST.EFFECTIVE_DATE AND
  @FROMEFFECTIVEDATE<=ISNULL(MNT_REIN_COMAPANY_LIST.TERMINATION_DATE,'01-01-3000'))
  --itrack 847 
 --(MNT_REIN_COMAPANY_LIST.EFFECTIVE_DATE <=@FROMEFFECTIVEDATE AND ISNULL(MNT_REIN_COMAPANY_LIST.TERMINATION_DATE,'01-01-3000')>=@TOEFFECTIVEDATE)     
 ORDER BY  REIN_COMAPANY_NAME         
END     
GO

