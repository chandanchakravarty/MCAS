IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsuranceCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsuranceCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /***********************************************************        
Creatted By : Pravesh K Chandel        
Modify date : 9 Aug 2007    
purpose   : Update Reinsurance Coverage       
Modified by           : sonal  // Praveen Kumar   
Date                    : 21 june 2010  // 19/08/2010        
Purpose               :  added new fields   // added new fields [DISPLAY_ON_CLAIM/CLAIM_RESERVE_APPLY]    

Modified by           : ankit
Date                    : 26 oct 2010     
Purpose               :  added IS_MAIN

Modify By : Abhinav  
Modify date  :8 Nov 2010           
purpose   : UPDATE Sub_lob_id  

Modify By : Shikha  
Modify date  : 30 May 2011          
purpose   : Add COV_TYPE_ABBR,SUSEP_COV_CODE  
 
************************************************************/        

--drop proc dbo.Proc_UpdateReinsuranceCoverage            
CREATE   PROCEDURE [dbo].[Proc_UpdateReinsuranceCoverage]            
(             
 @COV_ID int ,            
    
 @EFFECTIVE_FROM_DATE datetime,        
 @EFFECTIVE_TO_DATE datetime,         
 @DISABLED_DATE datetime,       
 @REINSURANCE_LOB int,      
 @REINSURANCE_COV int,      
 @ASLOB int,      
 @REINSURANCE_CALC int,    
 @FORM_NUMBER varchar(20) ,    
 @REIN_REPORT_BUCK int ,    
 @COMM_VEHICLE int,    
 @COMM_REIN_COV_CAT int,    
 @REIN_ASLOB int,    
 @COMM_CALC int,    
 @REIN_REPORT_BUCK_COMM int,  
  /* new fields added by sonal*/  
 @IS_MANDATORY int,  
  @IS_DEFAULT bit,   
 @MANDATORY_DATE DATETIME =NULL,    
 @NON_MANDATORY_DATE DATETIME=NULL,    
 @DEFAULT_DATE DATETIME=NULL,    
 @NON_DEFAULT_DATE DATETIME=NULL ,
 @DISPLAY_ON_CLAIM int, 
 @CLAIM_RESERVE_APPLY int,
 @IS_MAIN NCHAR, 
 @SUB_LOB_ID int,
 @COV_TYPE_ABBR nvarchar(10) =NULL,
 @SUSEP_COV_CODE numeric(4,0) =NULL      
)            
AS            
BEGIN            
UPDATE MNT_REINSURANCE_COVERAGE            
 SET         
 REINSURANCE_LOB=@REINSURANCE_LOB,      
 REINSURANCE_COV=@REINSURANCE_COV,      
 ASLOB=@ASLOB,      
 REINSURANCE_CALC=@REINSURANCE_CALC,        
 EFFECTIVE_FROM_DATE=@EFFECTIVE_FROM_DATE,        
 EFFECTIVE_TO_DATE=@EFFECTIVE_TO_DATE,        
 DISABLED_DATE=@DISABLED_DATE,    
 FORM_NUMBER=@FORM_NUMBER ,    
 REIN_REPORT_BUCK=@REIN_REPORT_BUCK,    
 COMM_VEHICLE=@COMM_VEHICLE,    
 COMM_REIN_COV_CAT=@COMM_REIN_COV_CAT,      
 REIN_ASLOB=@REIN_ASLOB,    
 COMM_CALC=@COMM_CALC,   
 IS_MANDATORY=@IS_MANDATORY,  
 IS_DEFAULT=@IS_DEFAULT,   
 REIN_REPORT_BUCK_COMM=@REIN_REPORT_BUCK_COMM,  
 MANDATORY_DATE =@MANDATORY_DATE,  
 NON_MANDATORY_DATE =@NON_MANDATORY_DATE,    
 DEFAULT_DATE =@DEFAULT_DATE,    
 NON_DEFAULT_DATE =@NON_DEFAULT_DATE,    
 DISPLAY_ON_CLAIM =@DISPLAY_ON_CLAIM, 
 CLAIM_RESERVE_APPLY =@CLAIM_RESERVE_APPLY,
 IS_MAIN=@IS_MAIN,
 SUB_LOB_ID=@SUB_LOB_ID,
  COV_TYPE_ABBR=@COV_TYPE_ABBR,
 SUSEP_COV_CODE=@SUSEP_COV_CODE   
WHERE COV_ID=@COV_ID            
      
     
END          
          
        
        
        
        
        
        
        
      
      
      
      
    
    
    
    
    
    
    
GO

