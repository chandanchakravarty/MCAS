IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCSRProducers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCSRProducers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
 /*        
Proc_GetCSRProducers 144 ,11,'W001'       
    
Modify by : Pravesh K Chandel  
date    : 3 March 2011  
Puprpose   : fetch CSR applicable to all broker and all products if not specified on product level  
DROP PROC dbo.Proc_GetCSRProducers    
*/        
CREATE PROC [dbo].[Proc_GetCSRProducers]          
@AgencyID int,    
@LOB_ID SMALLINT, --Added by Charles on 28-May-2010       
@USER_SYSTEM_ID NVARCHAR(8) =NULL --Added by Charles on 31-May-2010  
AS          
BEGIN             
     
 --Commented by Charles on 28-May-2010    
 --DECLARE @WOLV_ID INT      
  --SET @WOLV_ID = 27     
      
 --Added by Charles on 28-May-2010    
 DECLARE @CARRIER_ID INT,    
 @CARRIER_CODE NVARCHAR(10),  
 @CARRIER_AGENCY_ID INT,     
 @QUERY NVARCHAR(MAX)     
    
 --Commented by Charles on 3-Jun-10 for Itrack 120  
 SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,''), @CARRIER_ID = SYSP.SYS_CARRIER_ID FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)             
 INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID     
  --Added till here    
  select @CARRIER_AGENCY_ID=AGENCY_ID FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_CODE=@CARRIER_CODE  
 -- If logged in agency is not Carrier then show agency specific data         
 --IF @AgencyID <> @WOLV_ID   --Commented by Charles on 28-May-2010    
   --IF UPPER(@USER_SYSTEM_ID) <> UPPER(@CARRIER_CODE) --Commented by Charles on 3-Jun-10 for Itrack 120     
   BEGIN    
      -- FETCH csr APPLICABLE TO ALL IF NOT SPECIFIED TO THAT PERTICULAR AGENCY AND PRODUCT  
    IF EXISTS (SELECT  MARKETERS FROM MNT_AGENCY_UNDERWRITERS WITH(NOLOCK) WHERE AGENCY_ID = @AgencyID AND LOB_ID = @LOB_ID)  
       BEGIN  
  --Added by Charles on 28-May-2010    
  SET @QUERY =' SELECT  UL.[USER_ID], UL.USER_FNAME +''  ''+  UL.USER_LNAME AS USERNAME,    
  (ISNULL(CAST(UL.[USER_ID] AS VARCHAR),'''') +  '' '' + ISNULL(UL.USER_FNAME,'''') + '' '' + ISNULL(UL.USER_LNAME,'''')) AS USER_NAME_ID,        
  UL.IS_ACTIVE       
  FROM MNT_USER_LIST UL WITH(NOLOCK)      
  WHERE UL.[USER_ID] IN (' +   
  (SELECT   '''' + replace(MARKETERS,',',''',''' ) + ''''  
  FROM MNT_AGENCY_UNDERWRITERS WITH(NOLOCK) WHERE AGENCY_ID = @AgencyID AND LOB_ID = @LOB_ID)    
  + ') ORDER BY USERNAME ASC '     
   END  
 ELSE  
 BEGIN  
  SET @QUERY =' SELECT  UL.[USER_ID], UL.USER_FNAME +''  ''+  UL.USER_LNAME AS USERNAME,    
  (ISNULL(CAST(UL.[USER_ID] AS VARCHAR),'''') +  '' '' + ISNULL(UL.USER_FNAME,'''') + '' '' + ISNULL(UL.USER_LNAME,'''')) AS USER_NAME_ID,        
  UL.IS_ACTIVE       
  FROM MNT_USER_LIST UL WITH(NOLOCK)      
  WHERE UL.[USER_ID] IN (' +   
  (SELECT   '''' + replace(MARKETERS,',',''',''' ) + ''''  
  FROM MNT_AGENCY_UNDERWRITERS WITH(NOLOCK) WHERE AGENCY_ID = @CARRIER_AGENCY_ID AND LOB_ID = 0)    
  + ') ORDER BY USERNAME ASC '   
 END     
   --SELECT  @QUERY    
   EXEC (@QUERY)     
   --INNER JOIN MNT_AGENCY_LIST MAL WITH(NOLOCK) ON MAL.AGENCY_CODE  = UL.USER_SYSTEM_ID  --Commented by Charles on 28-May-2010        
   --WHERE MAL.AGENCY_ID = @AGENCYID  --Commented by Charles on 28-May-2010        
   END   
   --Commented by Charles on 3-Jun-10 for Itrack 120  
   /*       
   ELSE        
   BEGIN        
   -- Users will be displayed in CSR combo for the agency, no matter whether deactivated or activated.        
   SELECT UL.[USER_ID], UL.USER_FNAME +'  '+  UL.USER_LNAME AS USERNAME,    
   (ISNULL(CAST(UL.[USER_ID] AS VARCHAR),'') +  ' ' + ISNULL(UL.USER_FNAME,'') + ' ' + ISNULL(UL.USER_LNAME,'')) AS USER_NAME_ID,          
   UL.IS_ACTIVE          
   FROM MNT_USER_LIST UL WITH(NOLOCK)     
   WHERE UL.USER_SYSTEM_ID = @CARRIER_CODE  --Added by Charles on 28-May-2010     
   --INNER JOIN MNT_AGENCY_LIST MAL WITH(NOLOCK) ON MAL.AGENCY_CODE  = UL.USER_SYSTEM_ID --Commented by Charles on 28-May-2010       
   ORDER BY USERNAME ASC        
   END        
   */  
END   
  
GO

