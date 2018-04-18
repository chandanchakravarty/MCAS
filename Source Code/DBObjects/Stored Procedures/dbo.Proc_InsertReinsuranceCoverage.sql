IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertReinsuranceCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertReinsuranceCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name          : Dbo.Proc_InsertReinsuranceCoverage                  
Created by           : Pravesh K Chandel          
Date                    : 9 Aug 2007            
Purpose               :                   
Revison History :                  
Used In                :   Wolverine    
                  
Modified by           : sonal    // Praveen Kumar    
Date                    : 21 june 2010   // 19/08/2010         
Purpose               :  added new fields   //  added new fields [DISPLAY_ON_CLAIM/CLAIM_RESERVE_APPLY]                 
----------                  
Modified by           : aNKIT
Date                    : 26 OCT 2010
Purpose               :  added IS_MAIN   //
Modify By : Abhinav  
Modify date  :8 Nov 2010           
purpose   : Add Sub_lob_id
Date     Review By          Comments                  
----------
Modify By : Shikha  
Modify date  : 30 May 2011          
purpose   : Add COV_TYPE_ABBR,SUSEP_COV_CODE

------   ------------       -------------------------*/                  
--drop proc Proc_InsertReinsuranceCoverage                  
CREATE   PROCEDURE [dbo].[Proc_InsertReinsuranceCoverage]                  
(                   
 @COV_ID int output,                  
 @COV_REF_CODE varchar(10),                  
 @COV_CODE varchar(10),                  
 @COV_DES varchar(250),                  
 @STATE_ID int,                  
 @LOB_ID int,                  
 @IS_ACTIVE nchar,                  
 @IS_DEFAULT bit,                  
 @TYPE nchar,                  
 @LIMIT_TYPE nchar,                  
 @DEDUCTIBLE_TYPE nchar,                  
 @IsLimitApplicable nchar,                  
 @IsDeductApplicable nchar,                  
 @COVERAGE_TYPE nchar(10),                  
 @RANK int,                  
 @IS_MANDATORY int,                  
 @EFFECTIVE_FROM_DATE datetime,                  
 @EFFECTIVE_TO_DATE datetime,                   
 @DISABLED_DATE datetime,                 
 @REINSURANCE_LOB int,                
 @REINSURANCE_COV int,                
 @ASLOB int,                
 @REINSURANCE_CALC int,                
 @ISADDDEDUCTIBLE_APP nchar   ,          
 @ADDDEDUCTIBLE_TYPE nchar,             
 @FORM_NUMBER varchar(20) ,    
 @REIN_REPORT_BUCK int ,    
 @COMM_VEHICLE int,    
 @COMM_REIN_COV_CAT int,    
 @REIN_ASLOB int,    
 @COMM_CALC int,    
 @REIN_REPORT_BUCK_COMM int,  
 /* new fields added by sonal*/  
 @MANDATORY_DATE DATETIME =NULL,    
 @NON_MANDATORY_DATE DATETIME=NULL,    
 @DEFAULT_DATE DATETIME=NULL,    
 @NON_DEFAULT_DATE DATETIME=NULL,
 @DISPLAY_ON_CLAIM int, 
 @CLAIM_RESERVE_APPLY int,
 @IS_MAIN NCHAR  = NULL,
 @SUB_LOB_ID int,
 @COV_TYPE_ABBR nvarchar(10) = NULL,
 @SUSEP_COV_CODE numeric(4,0) = NULL
 
)                  
AS                  
BEGIN                  
      
--ReInsurance Coverage Id will start from 20000    
SELECT @COV_ID = IsNull(Max(COV_ID),20000) + 1 FROM MNT_REINSURANCE_COVERAGE  where   COV_ID > 20000      
--SELECT @COV_ID = IsNull(Max(COV_ID),0) + 1 FROM MNT_REINSURANCE_COVERAGE        
INSERT INTO  MNT_REINSURANCE_COVERAGE                  
(                  
COV_ID,COV_REF_CODE,COV_CODE,COV_DES,STATE_ID,LOB_ID,IS_ACTIVE, IS_DEFAULT,TYPE,LIMIT_TYPE,DEDUCTIBLE_TYPE,IsLimitApplicable,IsDeductApplicable,COVERAGE_TYPE                   
,RANK,IS_MANDATORY,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,REINSURANCE_LOB,REINSURANCE_COV,ASLOB,REINSURANCE_CALC,ISADDDEDUCTIBLE_APP,ADDDEDUCTIBLE_TYPE,FORM_NUMBER ,REIN_REPORT_BUCK,    
COMM_VEHICLE,COMM_REIN_COV_CAT,REIN_ASLOB,COMM_CALC,REIN_REPORT_BUCK_COMM,MANDATORY_DATE,NON_MANDATORY_DATE,DEFAULT_DATE,NON_DEFAULT_DATE
,DISPLAY_ON_CLAIM,CLAIM_RESERVE_APPLY,IS_MAIN,SUB_LOB_ID,COV_TYPE_ABBR,SUSEP_COV_CODE 
)                  
VALUES                  
(                  
@COV_ID,@COV_REF_CODE,@COV_CODE,@COV_DES,@STATE_ID,@LOB_ID,@IS_ACTIVE,@IS_DEFAULT,@TYPE,@LIMIT_TYPE,@DEDUCTIBLE_TYPE,@IsLimitApplicable,@IsDeductApplicable,@COVERAGE_TYPE                  
,@RANK,@IS_MANDATORY,@EFFECTIVE_FROM_DATE,@EFFECTIVE_TO_DATE,@DISABLED_DATE,@REINSURANCE_LOB,@REINSURANCE_COV,@ASLOB,@REINSURANCE_CALC,@ISADDDEDUCTIBLE_APP,@ADDDEDUCTIBLE_TYPE,@FORM_NUMBER,@REIN_REPORT_BUCK,    
@COMM_VEHICLE,@COMM_REIN_COV_CAT,@REIN_ASLOB,@COMM_CALC,@REIN_REPORT_BUCK_COMM,@MANDATORY_DATE,@NON_MANDATORY_DATE,@DEFAULT_DATE,@NON_DEFAULT_DATE            
,@DISPLAY_ON_CLAIM ,@CLAIM_RESERVE_APPLY,@IS_MAIN,@SUB_LOB_ID,@COV_TYPE_ABBR,@SUSEP_COV_CODE 
)                  
            
           
END      

 
               
                  
                
              
            
            
            
            
            
            
          
        
      
      
      
      
    
    
    
    
    
GO

