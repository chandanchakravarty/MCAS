/*                          
Created By:    Sumit Chhabra              
Created Date : 26 Dec 2006                          
Purpose     :  To Fetch list of additional interest for the given LOB        
MODIFIED BY :pRAVESH k CHANDEL  
DATE  :3 AUG 09  
PURPOSE  : APPLYING NO LOCK  
  
drop proc  Proc_GetAddlIntList_PolProc             
*/                          
ALTER PROC dbo.Proc_GetAddlIntList_PolProc                                                                                 
            
@CUSTOMER_ID INT,                                                                  
@POL_ID INT,                                                                                                          
@POL_VERSION_ID SMALLINT,                                                                                                          
@LOB_ID INT              
AS                                                                                                          
BEGIN                
                           
if(@LOB_ID=2 or @LOB_ID=3 or @LOB_ID = 38) --Auto/Motor            
begin            
 SELECT 'AUTO' +           
 /* '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +*/          
 '^' + CAST(ISNULL(VEHICLE_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  (ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,''))  
 ELSE  
   (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
    ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS   ADD_INT_DETAILS             
 FROM             
 POL_ADD_OTHER_INT POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID            
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'  
end            
else if(@LOB_ID=6) --Rental Dwelling        
begin        
 SELECT 'REDW' +           
 /* '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(DWELLING_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
    (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS             
 FROM             
 POL_HOME_OWNER_ADD_INT POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID               
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'       
end        
else if(@LOB_ID=1) --Home Owner        
begin        
 SELECT 'HOME' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(DWELLING_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
   (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS                 
 FROM             
 POL_HOME_OWNER_ADD_INT   POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'        
         
 UNION        
     
 SELECT 'WAT' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(BOAT_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
   (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS              
 FROM             
 Pol_WATERCRAFT_COV_ADD_INT    POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                       
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'         
        
 UNION        
        
 SELECT 'REC_VEH' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(REC_VEH_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
  (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS                 
 FROM             
 POL_HOMEOWNER_REC_VEH_ADD_INT   POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'        
    
UNION   
 SELECT 'WTRAL' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(TRAILER_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
  (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS                   
 FROM             
 POL_WATERCRAFT_TRAILER_ADD_INT   POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'        
  
end        
else if(@LOB_ID=4) --Watercraft        
begin        
 SELECT 'WAT' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(BOAT_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
   (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS                   
 FROM             
 Pol_WATERCRAFT_COV_ADD_INT   POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'        
  
 UNION      
  
 SELECT 'WTRAL' +       /*    
 '^' + CAST(ISNULL(CUSTOMER_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_ID,'') AS VARCHAR) +          
 '^' + CAST(ISNULL(POLICY_VERSION_ID,'') AS VARCHAR) +      */    
 '^' + CAST(ISNULL(TRAILER_ID,'') AS VARCHAR) + '^' + CAST(ISNULL(ADD_INT_ID,'') AS VARCHAR) AS ADD_INT_ID,                
 CASE ISNULL(POLICY.HOLDER_ID,0)   
 WHEN  0 THEN  ISNULL(POLICY.HOLDER_NAME,'') + ',' + ISNULL(POLICY.HOLDER_ADD1,'')  + ' ' +  ISNULL(POLICY.HOLDER_ADD2,'')  + ',' +              
    ISNULL(POLICY.HOLDER_CITY,'')   
 ELSE   
  (ISNULL(HOLDER.HOLDER_NAME,'') + ',' + ISNULL(HOLDER.HOLDER_ADD1,'')  + ' ' +  ISNULL(HOLDER.HOLDER_ADD2,'')  + ',' +              
     ISNULL(HOLDER.HOLDER_CITY,''))  
 END AS ADD_INT_DETAILS                   
 FROM             
 POL_WATERCRAFT_TRAILER_ADD_INT   POLICY WITH(NOLOCK)  
 LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST HOLDER WITH(NOLOCK)  
 ON  
 POLICY.HOLDER_ID = HOLDER.HOLDER_ID                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND ISNULL(POLICY.IS_ACTIVE,'N')='Y'        
end        
   
else   
begin  
select '' as  ADD_INT_DETAILS where 1=2       
end  
           
END         
  
  
  
  