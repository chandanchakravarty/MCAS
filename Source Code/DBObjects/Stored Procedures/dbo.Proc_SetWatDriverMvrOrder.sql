IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetWatDriverMvrOrder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetWatDriverMvrOrder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name                : Dbo.Proc_SetWatDriverMvrOrder                                  
Created by               :Mohit Agarwal                                                          
Date                    : 26-Jun-2007                                                         
Purpose                  : To set Driver MVR Ordered field          
Revison History          :                                                            
Used In                  : Wolverine                                                            
------------------------------------------------------------                                                            
Date     Review By          Comments                                                           
drop proc dbo.Proc_SetWatDriverMvrOrder                                                     
------   ------------       -------------------------*/                                                            
CREATE proc dbo.Proc_SetWatDriverMvrOrder                                                         
(                                                            
 @CUSTOMER_ID    int,                                                            
 @APP_ID    int,                                                            
 @APP_VERSION_ID   int,             
 @DRIVER_ID   int,             
 @MVR_REMARKS varchar(250),
 @MVR_STATUS varchar(10), 
 @CALLED_FROM varchar(50)=null ,
 @MVR_LIC_CLASS varchar(50) =NULL,
 @DRIVER_LICENSE_APPLICATION varchar(100)=NULL              
)                                                            
as                                             
BEGIN 
/*
"C"		indicates a CLEAR report
<space> indicates a non-CLEAR report
"1" or "2" indicates a NOT FOUND
"E"		indicates a ERROR/REJECT
and on page
	<option value="C">Clear</option>
	<option value="V">Non Clear</option>
	<option value="N">Not Found</option>
	<option value="E">Error/Reject</option>
*/ 
 DECLARE @VIOLATIONS INT  
  SET @VIOLATIONS =10963
if (@MVR_STATUS='' or  @MVR_STATUS=' ')
	set @MVR_STATUS='V' 
else if (@MVR_STATUS='1' or @MVR_STATUS='2')
 BEGIN
	set @MVR_STATUS='N' 
	SET @VIOLATIONS =10964 --Itrack 4740
 END 
else if (@MVR_STATUS='E' or @MVR_STATUS='C' )
 BEGIN
	SET @VIOLATIONS =10964 --Itrack 4740
 END 
                                        

IF UPPER(ISNULL(@CALLED_FROM,'')) != 'POLICY'          
BEGIN  
          
UPDATE APP_WATERCRAFT_DRIVER_DETAILS SET MVR_ORDERED = 10963, 
			DATE_ORDERED = GetDate(),
			MVR_STATUS = @MVR_STATUS,
			MVR_REMARKS = @MVR_REMARKS ,  
			VIOLATIONS	=@VIOLATIONS ,
			MVR_LIC_CLASS	= @MVR_LIC_CLASS,
			MVR_DRIV_LIC_APPL= @DRIVER_LICENSE_APPLICATION 
    
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=APP_ID AND @APP_VERSION_ID=APP_VERSION_ID    
AND @DRIVER_ID=DRIVER_ID    
    
END  
  
ELSE  
BEGIN  
UPDATE POL_WATERCRAFT_DRIVER_DETAILS SET MVR_ORDERED = 10963, 
			DATE_ORDERED = GetDate(),
			MVR_STATUS = @MVR_STATUS,
			MVR_REMARKS = @MVR_REMARKS,    
    		VIOLATIONS	=@VIOLATIONS ,
			MVR_LIC_CLASS	= @MVR_LIC_CLASS,
			MVR_DRIV_LIC_APPL= @DRIVER_LICENSE_APPLICATION
WHERE @CUSTOMER_ID=CUSTOMER_ID AND @APP_ID=POLICY_ID AND @APP_VERSION_ID=POLICY_VERSION_ID    
AND @DRIVER_ID=DRIVER_ID   
END  
    
      
END     
    
  







GO

