IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFHomeownerUnderwritingDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFHomeownerUnderwritingDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name          : Proc_GetPDFHomeownerUnderwritingDetails            
Created by         : Anurag Verma            
Date               : 27-June-2006            
Purpose            :             
Revison History    :            
Used In            : Wolverine              
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROCEDURE Proc_GetPDFHomeownerUnderwritingDetails 817,90,1,'application'           
CREATE      PROCEDURE dbo.Proc_GetPDFHomeownerUnderwritingDetails            
(            
 @CUSTOMERID   int,            
 @POLID                int,            
 @VERSIONID   int,            
 @CALLEDFROM  VARCHAR(20)            
)            
AS            
BEGIN            
 IF (@CALLEDFROM='APPLICATION')            
 BEGIN            
  SELECT             
    ANY_FARMING_BUSINESS_COND,ANY_RESIDENCE_EMPLOYEE,ANY_OTHER_RESI_OWNED,ANY_OTH_INSU_COMP            
   ,HAS_INSU_TRANSFERED_AGENCY,ANY_COV_DECLINED_CANCELED,ANIMALS_EXO_PETS_HISTORY,            
   CASE WHEN OF_ACRES > 5 THEN 1 ELSE 0 END OF_ACRES,CONVICTION_DEGREE_IN_PAST,ANY_RENOVATION            
   ,IS_PROP_NEXT_COMMERICAL,TRAMPOLINE,BUILD_UNDER_CON_GEN_CONT,            
   DESC_FARMING_BUSINESS_COND FARMING_DESC,DESC_RESIDENCE_EMPLOYEE,DESC_OTHER_RESIDENCE,DESC_OTHER_INSURANCE            
   ,DESC_INSU_TRANSFERED_AGENCY,DESC_COV_DECLINED_CANCELED,BREED + ' ' + MLV.LOOKUP_VALUE_DESC BREED_OTHER_DESCRIPTION,DESC_CONVICTION_DEGREE_IN_PAST            
   ,DESC_RENOVATION,DESC_PROPERTY,DESC_TRAMPOLINE,REMARKS,convert(varchar(10),LAST_INSPECTED_DATE,101) LAST_INSPECTED_DATE            
   ,IS_RENTED_IN_PART,IS_DWELLING_OWNED_BY_OTHER,DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER,SWIMMING_POOL,NO_OF_PETS,NO_HORSES,PROPERTY_ON_MORE_THAN,            
    PROPERTY_ON_MORE_THAN_DESC,isnull(DESC_MULTI_POLICY_DISC_APPLIED,'') DESC_MULTI_POLICY_DISC_APPLIED ,  PROPERTY_USED_WHOLE_PART,        
   PROPERTY_USED_WHOLE_PART_DESC PROPERTY_DESC,          
     case  when isnull(DESC_RESIDENCE_EMPLOYEE,'') <>'' then 'Question #2: '+convert (nvarchar(300),isnull(DESC_RESIDENCE_EMPLOYEE,''))+'
' else '' end   
+ case when isnull(DESC_OTHER_RESIDENCE,'') <> ''then 'Question #4: '+convert (nvarchar(300),isnull(DESC_OTHER_RESIDENCE,'')) + '
' else '' end         
+ case when isnull(DESC_OTHER_INSURANCE,'') <> ''then 'Question #5: '+convert (nvarchar(300),isnull(DESC_OTHER_INSURANCE,'')) +'  
' else '' end         
+ case when isnull(DESC_INSU_TRANSFERED_AGENCY,'') <> ''then 'Question #6: '+convert (nvarchar(300),isnull(DESC_INSU_TRANSFERED_AGENCY,'')) +'    
' else '' end         
+ case when isnull(DESC_COV_DECLINED_CANCELED,'') <> ''then 'Question #7: '+convert (nvarchar(300),isnull(DESC_COV_DECLINED_CANCELED,'')) +'    
' else '' end         
+ case when isnull(NO_OF_PETS,'') <> ''then 'Question #9: '+convert (nvarchar(300),isnull(NO_OF_PETS,0))+':'+isnull(BREED + ' ' + MLV.LOOKUP_VALUE_DESC,'') +'    
' else '' end         
+ case when isnull(DESC_CONVICTION_DEGREE_IN_PAST,'') <> ''then 'Question #14: '+convert (nvarchar(300),isnull(DESC_CONVICTION_DEGREE_IN_PAST,'')) +'   
' else '' end         
+ case when isnull(DESC_RENOVATION,'') <> ''then 'Question #19: '+convert (nvarchar(300),isnull(DESC_RENOVATION,'')) +'   
' else '' end         
+ case when isnull(DESC_PROPERTY,'') <> ''then 'Question #21: '+convert (nvarchar(300),isnull(DESC_PROPERTY,'')) +'   
' else '' end         
+ case when isnull(DESC_TRAMPOLINE,'') <> ''then 'Question #22: '+convert (nvarchar(300),isnull(DESC_TRAMPOLINE,'')) +'
' else '' end         
+ case when isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'') <> ''then 'Question #25: '+convert (nvarchar(300),isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'')) +'  
' else '' end         
+ case when isnull(REMARKS,'') <> ''then 'Any Other Remarks: ' + convert (nvarchar(300),isnull(REMARKS,''))+'
' else '' end         
 as RENTAL_REMARKS_PDF    
 /* 'Question #2: '+convert (nvarchar(300),isnull(DESC_RESIDENCE_EMPLOYEE,'')) +'    
'+'Question #4: '+convert (nvarchar(300),isnull(DESC_OTHER_RESIDENCE,'')) +'   
'+'Question #5: '+convert (nvarchar(300),isnull(DESC_OTHER_INSURANCE,'')) +'   
'+'Question #6: '+convert (nvarchar(300),isnull(DESC_INSU_TRANSFERED_AGENCY,'')) +'    
'+'Question #7: '+convert (nvarchar(300),isnull(DESC_COV_DECLINED_CANCELED,'')) +'    
'+'Question #9: '+convert (nvarchar(300),isnull(NO_OF_PETS,0))+':'+isnull(BREED + ' ' + MLV.LOOKUP_VALUE_DESC,'') +'    
'+'Question #14: '+convert (nvarchar(300),isnull(DESC_CONVICTION_DEGREE_IN_PAST,'')) +'   
'+'Question #19: '+convert (nvarchar(300),isnull(DESC_RENOVATION,'')) +'   
'+'Question #21: '+convert (nvarchar(300),isnull(DESC_PROPERTY,'')) +'   
'+'Question #22: '+convert (nvarchar(300),isnull(DESC_TRAMPOLINE,'')) +'
'+'Question #25: '+convert (nvarchar(300),isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'')) +'  
'+'Any Other Remarks: ' + convert (nvarchar(300),isnull(REMARKS,'')) as RENTAL_REMARKS_PDF    
   */ 
  FROM APP_HOME_OWNER_GEN_INFO GI            
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON convert(nvarchar(100),MLV.LOOKUP_UNIQUE_ID)=GI.OTHER_DESCRIPTION AND MLV.LOOKUP_ID=1241            
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID             
            
 END            
 ELSE IF (@CALLEDFROM='POLICY')            
 BEGIN            
  SELECT ANY_FARMING_BUSINESS_COND,ANY_RESIDENCE_EMPLOYEE,ANY_OTHER_RESI_OWNED,ANY_OTH_INSU_COMP            
   ,HAS_INSU_TRANSFERED_AGENCY,ANY_COV_DECLINED_CANCELED,ANIMALS_EXO_PETS_HISTORY,            
   CASE WHEN OF_ACRES > 5 THEN 1 ELSE 0 END OF_ACRES,CONVICTION_DEGREE_IN_PAST,ANY_RENOVATION            
   ,IS_PROP_NEXT_COMMERICAL,TRAMPOLINE,BUILD_UNDER_CON_GEN_CONT,            
   DESC_FARMING_BUSINESS_COND FARMING_DESC,DESC_RESIDENCE_EMPLOYEE,DESC_OTHER_RESIDENCE,DESC_OTHER_INSURANCE            
   ,DESC_INSU_TRANSFERED_AGENCY,DESC_COV_DECLINED_CANCELED,BREED + ' ' + MLV.LOOKUP_VALUE_DESC BREED_OTHER_DESCRIPTION,DESC_CONVICTION_DEGREE_IN_PAST            
   ,DESC_RENOVATION,DESC_PROPERTY,DESC_TRAMPOLINE,REMARKS,convert(varchar(10),LAST_INSPECTED_DATE,101) LAST_INSPECTED_DATE            
   ,IS_RENTED_IN_PART,IS_DWELLING_OWNED_BY_OTHER,DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER,SWIMMING_POOL,NO_OF_PETS,NO_HORSES,PROPERTY_ON_MORE_THAN,            
PROPERTY_ON_MORE_THAN_DESC,isnull(DESC_MULTI_POLICY_DISC_APPLIED,'') DESC_MULTI_POLICY_DISC_APPLIED  , PROPERTY_USED_WHOLE_PART,         
PROPERTY_USED_WHOLE_PART_DESC PROPERTY_DESC, 
case  when isnull(DESC_RESIDENCE_EMPLOYEE,'') <>'' then 'Question #2: '+convert (nvarchar(300),isnull(DESC_RESIDENCE_EMPLOYEE,''))+'
' else '' end   
+ case when isnull(DESC_OTHER_RESIDENCE,'') <> ''then 'Question #4: '+convert (nvarchar(300),isnull(DESC_OTHER_RESIDENCE,'')) + '
' else '' end         
+ case when isnull(DESC_OTHER_INSURANCE,'') <> ''then 'Question #5: '+convert (nvarchar(300),isnull(DESC_OTHER_INSURANCE,'')) +'  
' else '' end         
+ case when isnull(DESC_INSU_TRANSFERED_AGENCY,'') <> ''then 'Question #6: '+convert (nvarchar(300),isnull(DESC_INSU_TRANSFERED_AGENCY,'')) +'    
' else '' end         
+ case when isnull(DESC_COV_DECLINED_CANCELED,'') <> ''then 'Question #7: '+convert (nvarchar(300),isnull(DESC_COV_DECLINED_CANCELED,'')) +'    
' else '' end         
+ case when isnull(NO_OF_PETS,'') <> ''then 'Question #9: '+convert (nvarchar(300),isnull(NO_OF_PETS,0))+':'+isnull(BREED + ' ' + MLV.LOOKUP_VALUE_DESC,'') +'    
' else '' end         
+ case when isnull(DESC_CONVICTION_DEGREE_IN_PAST,'') <> ''then 'Question #14: '+convert (nvarchar(300),isnull(DESC_CONVICTION_DEGREE_IN_PAST,'')) +'   
' else '' end         
+ case when isnull(DESC_RENOVATION,'') <> ''then 'Question #19: '+convert (nvarchar(300),isnull(DESC_RENOVATION,'')) +'   
' else '' end         
+ case when isnull(DESC_PROPERTY,'') <> ''then 'Question #21: '+convert (nvarchar(300),isnull(DESC_PROPERTY,'')) +'   
' else '' end         
+ case when isnull(DESC_TRAMPOLINE,'') <> ''then 'Question #22: '+convert (nvarchar(300),isnull(DESC_TRAMPOLINE,'')) +'
' else '' end         
+ case when isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'') <> ''then 'Question #25: '+convert (nvarchar(300),isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'')) +'  
' else '' end         
+ case when isnull(REMARKS,'') <> ''then 'Any Other Remarks: ' + convert (nvarchar(300),isnull(REMARKS,''))+'
' else '' end         
 as RENTAL_REMARKS_PDF       
  /*'Question #2: '+convert (nvarchar(300),isnull(DESC_RESIDENCE_EMPLOYEE,'')) +'    
'+'Question #4: '+convert (nvarchar(300),isnull(DESC_OTHER_RESIDENCE,'')) +'   
'+'Question #5: '+convert (nvarchar(300),isnull(DESC_OTHER_INSURANCE,'')) +'   
'+'Question #6: '+convert (nvarchar(300),isnull(DESC_INSU_TRANSFERED_AGENCY,'')) +'    
'+'Question #7: '+convert (nvarchar(300),isnull(DESC_COV_DECLINED_CANCELED,'')) +'    
'+'Question #9: '+convert (nvarchar(300),isnull(NO_OF_PETS,0))+':'+isnull(BREED + ' ' + MLV.LOOKUP_VALUE_DESC,'') +'    
'+'Question #14: '+convert (nvarchar(300),isnull(DESC_CONVICTION_DEGREE_IN_PAST,'')) +'    
'+'Question #19: '+convert (nvarchar(300),isnull(DESC_RENOVATION,'')) +'    
'+'Question #21: '+convert (nvarchar(300),isnull(DESC_PROPERTY,'')) +'    
'+'Question #22: '+convert (nvarchar(300),isnull(DESC_TRAMPOLINE,'')) +'
'+'Question #25: '+convert (nvarchar(300),isnull(DESC_BUILD_UNDER_CON_GEN_CONT,'')) +'    
'+'Any Other Remarks: ' + convert (nvarchar(300),isnull(REMARKS,'')) as RENTAL_REMARKS_PDF    
    */
    
          
  FROM POL_HOME_OWNER_GEN_INFO GI            
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON convert(nvarchar(100),MLV.LOOKUP_UNIQUE_ID)=GI.OTHER_DESCRIPTION AND MLV.LOOKUP_ID=1241            
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID            
 END            
END            

GO

