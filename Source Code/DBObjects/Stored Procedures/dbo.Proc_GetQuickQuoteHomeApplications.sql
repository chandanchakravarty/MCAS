IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteHomeApplications]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteHomeApplications]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_GetQuickQuoteHomeApplications              
Created by         : Praveen kasana             
Date               : 24 jan 2006              
Purpose            : To fetch the Home Application Numbers (with Status INCOMPLETE) for xml              
Revison History    :              
Used In            : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
Create   PROCEDURE Proc_GetQuickQuoteHomeApplications              
 @CUSTOMER_ID int,            
 @STATE_NAME varchar(20),            
 @APP_LOB varchar(5)=null             
AS              
BEGIN              
--get StateName            
Declare @STATE_ID int            
select @STATE_ID=STATE_ID from MNT_COUNTRY_STATE_LIST            
where STATE_NAME=@STATE_NAME            
            
Set @APP_LOB =1  --1 for Home            
--Select APP_NUMBER + ':' + convert(varchar,APP_ID) + ':' + convert(varchar,APP_VERSION_ID) as APP_NO from APP_LIST             
Select APP_NUMBER + ' : ' + convert(varchar,APP_ID) + ' : ' + convert(varchar,APP_VERSION) as APP_NO ,        
convert(varchar,CUSTOMER_ID) AS CUSTOMER_ID,        
convert(varchar,APP_ID) as APP_ID ,          
convert(varchar,APP_VERSION) as APP_VER_ID,  
convert(varchar,APP_NUMBER) as APP_NUMBER  
from APP_LIST             
where upper(isnull(APP_STATUS,'')) <> 'COMPLETE' and isnull(IS_ACTIVE,'Y')='Y'            
and CUSTOMER_ID = @CUSTOMER_ID            
and STATE_ID=@STATE_ID             
and APP_LOB=@APP_LOB     FOR XML AUTO;            
             
END              
              
/*              
 Proc_GetQuickQuoteHomeApplications 744,MICHIGAN            
*/              
           

  
              
            
            
              
              
            
          
        
      
    
  



GO

