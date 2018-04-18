IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateEndorsmentDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateEndorsmentDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateEndorsmentDetails              
Created by      : Gaurav              
Date            : 8/26/2005              
Purpose       :Evaluation              
Modified  by     : Sumit chhabra        
Date            : 12/19/2005              
Purpose        : Attachment is removed        
        
Modified  by    : Ravindra        
Date            : 05-28-2006        
Purpose        :         
        
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
----drop proc Proc_UpdateEndorsmentDetails              
CREATE PROC dbo.Proc_UpdateEndorsmentDetails              
(              
@ENDORSMENT_ID     int,              
@STATE_ID     smallint,              
@LOB_ID     smallint,              
@PURPOSE     nchar(1),              
@TYPE     nchar(1),              
@DESCRIPTION     varchar(500),              
@TEXT     varchar(500),           
@ENDORSEMENT_CODE  varchar(50),           
@ENDORS_ASSOC_COVERAGE     nchar(1),              
@SELECT_COVERAGE     int,              
@ENDORS_PRINT nchar(1),              
@EFFECTIVE_FROM_DATE datetime,        
@EFFECTIVE_TO_DATE datetime,        
@DISABLED_DATE datetime,        
@IS_ACTIVE nchar(1),              
@MODIFIED_BY     int,              
@LAST_UPDATED_DATETIME     datetime,      
@FORM_NUMBER varchar(20),      
@EDITION_DATE datetime,  
@INCREASED_LIMIT int,
@PRINT_ORDER int      
)              
AS              
BEGIN              
Update  MNT_ENDORSMENT_DETAILS              
set              
-- STATE_ID  =  @STATE_ID,              
-- LOB_ID  =  @LOB_ID,              
-- PURPOSE  =  @PURPOSE,              
-- TYPE  =  @TYPE,              
-- DESCRIPTION  =  @DESCRIPTION,              
-- TEXT  =  TEXT,            
-- ENDORSEMENT_CODE=@ENDORSEMENT_CODE,            
-- ENDORS_ASSOC_COVERAGE  =  @ENDORS_ASSOC_COVERAGE,              
-- SELECT_COVERAGE  =  @SELECT_COVERAGE,              
 ENDORS_PRINT=@ENDORS_PRINT,              
-- IS_ACTIVE=@IS_ACTIVE,              
        
EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE,        
EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE,        
DISABLED_DATE      = @DISABLED_DATE ,            
MODIFIED_BY  =  @MODIFIED_BY,              
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME,      
FORM_NUMBER = @FORM_NUMBER,      
EDITION_DATE = @EDITION_DATE,  
INCREASED_LIMIT = @INCREASED_LIMIT,
PRINT_ORDER = @PRINT_ORDER             
              
where  ENDORSMENT_ID = @ENDORSMENT_ID              
              
END              
              
              
              
              
            
          
        
        
        
        
      
    
  



GO

