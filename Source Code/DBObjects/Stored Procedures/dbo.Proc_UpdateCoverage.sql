IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************          
Modified By : Ravindra          
Modify date : 05-08-2006          
purpose   : Added @EFFECTIVE_FROM_DATE @EFFECTIVE_TO_DATE @DISABLED_DATE           
          
Modified By : Pravesh k. Chandel // Praveen Kumar       
Modify date : 27 April 2007      // 19/08/2010    
purpose   : update associated Endosement  Also  //  DISPLAY_ON_CLAIM ,CLAIM_RESERVE_APPLY fields added      

Modified By : aNKIT gOEL 
Modify date : 26 oCT 2010 
purpose   : UPDATE iS_MAIN 

Modify By : Abhinav  
Modify date  :8 Nov 2010           
purpose   : UPDATE Sub_lob_id   

Modify By : Shikha  
Modify date  : 30 May 2011          
purpose   : Add COV_TYPE_ABBR,SUSEP_COV_CODE 
 
************************************************************/          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        
--drop proc Proc_UpdateCoverage              
CREATE   PROCEDURE [dbo].[Proc_UpdateCoverage]              
(               
 @COV_ID int ,              
 @COV_REF_CODE varchar(10),              
 @COV_CODE varchar(10),              
 @COV_DES varchar(250),              
 @STATE_ID int,              
 @LOB_ID int,              
 @IS_DEFAULT bit,              
 @TYPE nchar,              
 @PURPOSE nchar,              
 @LIMIT_TYPE nchar,              
 @DEDUCTIBLE_TYPE nchar,              
 @IsLimitApplicable nchar,              
 @IsDeductApplicable nchar,              
 @INCLUDED int,              
 @COVERAGE_TYPE nchar(10),              
 @RANK int,              
 @IS_MANDATORY int ,          
 @EFFECTIVE_FROM_DATE datetime,          
 @EFFECTIVE_TO_DATE datetime,           
 @DISABLED_DATE datetime,         
 @REINSURANCE_LOB int,        
 @REINSURANCE_COV int,        
 @ASLOB int,        
 @REINSURANCE_CALC int,      
 @COMM_VEHICLE int,      
 @COMM_REIN_COV_CAT int,      
 @REIN_ASLOB int,      
 @COMM_CALC int,      
 @REIN_REPORT_BUCK int,      
 @REIN_REPORT_BUCK_COMM int,             
 @FORM_NUMBER varchar(20),  
 @MANDATORY_DATE DATETIME =NULL,  
 @NON_MANDATORY_DATE DATETIME=NULL,  
 @DEFAULT_DATE DATETIME=NULL,  
 @NON_DEFAULT_DATE DATETIME=NULL,
  @DISPLAY_ON_CLAIM int, 
 @CLAIM_RESERVE_APPLY int,
 @IS_MAIN  nchar,
 @SUB_LOB_ID int ,
 @COV_TYPE_ABBR nvarchar(10)= NULL,
 @SUSEP_COV_CODE numeric(4,0) = NULL 
)              
AS              
BEGIN              
UPDATE MNT_COVERAGE              
 SET           
 /*COV_REF_CODE =@COV_REF_CODE,          
 COV_CODE=@COV_CODE,          
 COV_DES=@COV_DES,          
 STATE_ID=@STATE_ID,          
 LOB_ID=@LOB_ID,          
 IS_DEFAULT=@IS_DEFAULT,          
 TYPE=@TYPE,          
 PURPOSE=@PURPOSE,          
 LIMIT_TYPE=@LIMIT_TYPE,          
 DEDUCTIBLE_TYPE=@DEDUCTIBLE_TYPE,              
 IsLimitApplicable=@IsLimitApplicable ,          
 IsDeductApplicable=@IsDeductApplicable ,              
 INCLUDED =@INCLUDED,              
 COVERAGE_TYPE =@COVERAGE_TYPE,              
 RANK =@RANK ,              
 IS_MANDATORY=@IS_MANDATORY ,   */   
  IS_DEFAULT=@IS_DEFAULT,    
 IS_MANDATORY=@IS_MANDATORY,          
 REINSURANCE_LOB=@REINSURANCE_LOB,        
 REINSURANCE_COV=@REINSURANCE_COV,    
 RANK =@RANK ,        
 ASLOB=@ASLOB,        
 REINSURANCE_CALC=@REINSURANCE_CALC,      
 COMM_VEHICLE=@COMM_VEHICLE,      
 COMM_REIN_COV_CAT=@COMM_REIN_COV_CAT,      
 REIN_ASLOB=@REIN_ASLOB,      
 COMM_CALC=@COMM_CALC,          
 REIN_REPORT_BUCK=@REIN_REPORT_BUCK ,       
 REIN_REPORT_BUCK_COMM =@REIN_REPORT_BUCK_COMM,      
 EFFECTIVE_FROM_DATE=@EFFECTIVE_FROM_DATE,          
 EFFECTIVE_TO_DATE=@EFFECTIVE_TO_DATE,          
 DISABLED_DATE=@DISABLED_DATE,      
 FORM_NUMBER=@FORM_NUMBER,   
 MANDATORY_DATE=@MANDATORY_DATE,  
 NON_MANDATORY_DATE=@NON_MANDATORY_DATE,  
 DEFAULT_DATE=@DEFAULT_DATE,  
NON_DEFAULT_DATE=@NON_DEFAULT_DATE,
 DISPLAY_ON_CLAIM =@DISPLAY_ON_CLAIM, 
 CLAIM_RESERVE_APPLY =@CLAIM_RESERVE_APPLY,
  IS_MAIN =@IS_MAIN,
  SUB_LOB_ID=@SUB_LOB_ID, 
 COV_TYPE_ABBR=@COV_TYPE_ABBR,
 SUSEP_COV_CODE=@SUSEP_COV_CODE
 WHERE COV_ID=@COV_ID              
        
--update Associated Endorsment also  -- by Pravesh        
if  (@TYPE ='2')        
begin        
update  mnt_Endorsment_details        
 set  EFFECTIVE_FROM_DATE=@EFFECTIVE_FROM_DATE,          
  EFFECTIVE_TO_DATE=@EFFECTIVE_TO_DATE,          
  DISABLED_DATE=@DISABLED_DATE          
 where SELECT_COVERAGE=@COV_ID        
        
end        
        
        
END            
            
          
          
          
          
          
          
          
        
        
        
        
      
      
      
      
      
      
    
GO

