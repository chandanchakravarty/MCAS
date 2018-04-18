IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertEndorsmentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertEndorsmentDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertEndorsmentDetails              
Created by      : Gaurav              
Date            : 8/26/2005              
Purpose        : Evaluation              
Modified by     : Sumit Chhabra        
Date            : 12/19/2005              
Purpose        : Attachment is removed        
        
Modified by     : Ravindra         
Date            : 05-29-2006        
Purpose        :         
        
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
-- drop proc Proc_InsertEndorsmentDetails              
CREATE PROC dbo.Proc_InsertEndorsmentDetails              
(              
@ENDORSMENT_ID     int out,              
@STATE_ID     smallint,              
@LOB_ID     smallint,              
@PURPOSE     nchar(1),              
@TYPE     nchar(1),              
@DESCRIPTION     varchar(500),              
@TEXT     varchar(500),          
@ENDORSEMENT_CODE VARCHAR(50),             
@ENDORS_ASSOC_COVERAGE     nchar(1),              
@SELECT_COVERAGE     int,              
@ENDORS_PRINT nchar(1),              
--@ENDORS_ATTACHMENT varchar(300),              
@EFFECTIVE_FROM_DATE datetime,        
@EFFECTIVE_TO_DATE datetime,        
@DISABLED_DATE datetime,        
@IS_ACTIVE     nchar(1),              
@CREATED_BY     int,              
@CREATED_DATETIME     datetime,      
@FORM_NUMBER varchar(20),      
@EDITION_DATE datetime,      
@INCREASED_LIMIT int = null,
@PRINT_ORDER int = null    
)              
AS              
BEGIN              
              
select @ENDORSMENT_ID=isnull(Max(ENDORSMENT_ID),0)+1 from MNT_ENDORSMENT_DETAILS              
INSERT INTO MNT_ENDORSMENT_DETAILS              
(              
ENDORSMENT_ID,              
STATE_ID,              
LOB_ID,              
PURPOSE,              
TYPE,              
DESCRIPTION,              
TEXT,           
ENDORSEMENT_CODE   ,          
ENDORS_ASSOC_COVERAGE,              
SELECT_COVERAGE,              
ENDORS_PRINT,              
--ENDORS_ATTACHMENT,              
EFFECTIVE_FROM_DATE,        
EFFECTIVE_TO_DATE ,        
DISABLED_DATE ,        
IS_ACTIVE,              
CREATED_BY,              
CREATED_DATETIME,      
FORM_NUMBER,      
EDITION_DATE,    
INCREASED_LIMIT,
PRINT_ORDER              
)              
VALUES              
(              
@ENDORSMENT_ID,              
@STATE_ID,              
@LOB_ID,              
@PURPOSE,              
@TYPE,              
@DESCRIPTION,              
@TEXT,              
@ENDORSEMENT_CODE,          
@ENDORS_ASSOC_COVERAGE,              
@SELECT_COVERAGE,              
@ENDORS_PRINT,              
--@ENDORS_ATTACHMENT,              
@EFFECTIVE_FROM_DATE,        
@EFFECTIVE_TO_DATE ,        
@DISABLED_DATE ,        
@IS_ACTIVE,              
@CREATED_BY,              
@CREATED_DATETIME,      
@FORM_NUMBER,      
@EDITION_DATE,    
@INCREASED_LIMIT,
@PRINT_ORDER    
)              
END              
            
          
        
        
        
      
    
  



GO

