IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateFlageForPDFService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateFlageForPDFService]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*-----------------------------------------------------------------------    
 Proc Name      : dbo.Proc_UpdateFlageForPDFService                    
 Created By     : Praveen Kumar       
 Created date   : 12/08/2010        
 Purpose        : Upadate flage for Generated pdf documents       
 used in        : EbixAdvantage        
       
 DROP PROC Proc_UpdateFlageForPDFService  2126,179,2,'Y'      
-----------------------------------------------------------------------*/    
CREATE proc [dbo].[Proc_UpdateFlageForPDFService]                  
(    
 @CUSTOMER_ID int=NULL,        
 @POLICY_ID int=NULL,        
 @POLICY_VERSION_ID int=NULL,    
 @UPDATE_FLAG NVARCHAR(1)=NULL    
)    
 AS    
 BEGIN    
 IF @UPDATE_FLAG = 'Y'    
  BEGIN    
        UPDATE  ACT_PREMIUM_NOTICE_PROCCESS_LOG  SET IS_FILE_GENERATED ='Y'    
        WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
            AND ISNULL(CALLED_FOR,'')<>'PREM_NOTICE'     
  END    
      
 END    
    
GO

