IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDocumentList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDocumentList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--BEGIN TRAN         
--drop proc dbo.Proc_GetPolicyDocumentList        
--go 
-- =============================================
-- Modified By:		<Pradeep Kushwaha>
-- Modified date: <09-May-2011>
-- Description:	<Description,,>
-- =============================================
-- Proc_GetPolicyDocumentList  28241,143,2,1
--drop proc Proc_GetPolicyDocumentList    
    
 --exec Proc_GetPolicyDocumentList  2885,659,7,1
CREATE PROC [dbo].[Proc_GetPolicyDocumentList]                
(  
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT, 
@LANG_ID int,
@DOCUMENT_CODE nvarchar(50)='',
@CLAIM_ID INT = NULL,        
@ACTIVITY_ID INT = NULL
)  
            
AS                    
BEGIN   
DECLARE @PROCESS_ID INT 
IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND PROCESS_STATUS<>'ROLLBACK')
BEGIN
SELECT @PROCESS_ID=PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND PROCESS_STATUS<>'ROLLBACK'
END
ELSE
BEGIN
SET @PROCESS_ID=0
END
    
   select   
     
   J.[CUSTOMER_ID]  
           ,J.[POLICY_ID]  
           ,J.[POLICY_VERSION_ID]  
           ,DOCUMENT_CODE as DOC_CODE,
            case when DOCUMENT_CODE='POLICY_DOC' then CASE WHEN @PROCESS_ID=14 THEN dbo.fun_GetMessage(421,@LANG_ID) ELSE dbo.fun_GetMessage(422,@LANG_ID) END else     
			case when DOCUMENT_CODE='CANC_NOTICE' then dbo.fun_GetMessage(415,@LANG_ID) else     
			case when DOCUMENT_CODE='REFUSAL' then dbo.fun_GetMessage(416,@LANG_ID) else       
			case when DOCUMENT_CODE='CNNSFAGNR' then dbo.fun_GetMessage(417,@LANG_ID) else     
			case when DOCUMENT_CODE='CLTIN' then dbo.fun_GetMessage(418,@LANG_ID) else     
			case when DOCUMENT_CODE='BOLETO' then dbo.fun_GetMessage(419,@LANG_ID) else     
			case when DOCUMENT_CODE='POLICY1' then dbo.fun_GetMessage(420,@LANG_ID) else     
			case when DOCUMENT_CODE='CLM_RECEIPT' then dbo.fun_GetMessage(410,@LANG_ID) else     
			case when DOCUMENT_CODE='POLICY' then dbo.fun_GetMessage(420,@LANG_ID) else     
									   DOCUMENT_CODE	 
									   end end end end end end end end end
           
            --  else 
									   --case when DOCUMENT_CODE='POLICY_DOC' then CASE WHEN @PROCESS_ID=14 THEN 'Documento de endosso' ELSE 'Documento de Apólice' END else 
									   --case when DOCUMENT_CODE='CANC_NOTICE' then 'Aviso de Cancelamento' else 
									   --case when DOCUMENT_CODE='REFUSAL' then 'Recusa' else 
									   --case when DOCUMENT_CODE='CNNSFAGNR' then 'Aviso de Cancelamento - NSF' else 
									   --case when DOCUMENT_CODE='CLTIN' then 'Aviso de Cancelamento' else 
									   --case when DOCUMENT_CODE='BOLETO' then 'BOLETO' else 
									   --case when DOCUMENT_CODE='POLICY1' then 'POLÍTICA' else 
									   --case when DOCUMENT_CODE='CLM_RECEIPT' then 'Recibo de Sinistro' else 
									   --case when DOCUMENT_CODE='POLICY' then 'POLÍTICA' else 
									   --DOCUMENT_CODE	 
									   --end end end end end end end end end 
            --  end
              as [DOCUMENT_CODE]       
           ,[PRINT_DATETIME]  
           ,[URL_PATH]  
           ,[ONDEMAND_FLAG]  
           ,[PRINT_SUCCESSFUL]  
           ,[PRINTED_DATETIME]  
           ,[DUPLEX]  
           ,J.[CREATED_BY]  
           ,J.[CREATED_DATETIME]  
           ,J.[MODIFIED_BY]  
           ,J.[LAST_UPDATED_DATETIME]  
           ,J.[IS_ACTIVE]  
           --,[ENTITY_TYPE]  
           ,--SUBSTRING(Entity_Type,1,1)+LOWER(SUBSTRING(Entity_Type,2,LEN(Entity_Type))) as ENTITY_TYPE 
           --case when @LANG_ID=1 then case when ENTITY_TYPE='CUSTOMER' then 'Customer' else
           --                            case when ENTITY_TYPE='LEADER' then 'Leader' else
           --                            case when ENTITY_TYPE='BROKER' then 'Broker' else
           --                            case when ENTITY_TYPE='CLAIM' then 'Claim' else
           --                            case when ENTITY_TYPE='FOLLOWER' then 'Follower' else
           --                            case when ENTITY_TYPE='AGENCY' then 'Agency' else
           --                            case when ENTITY_TYPE='CARRIER' then 'Carrier' else
           --                            ENTITY_TYPE
           --                            end  end end  end end end end
           --              else
									  -- case when ENTITY_TYPE='CUSTOMER' then 'Cliente' else
           --                            case when ENTITY_TYPE='LEADER' then 'Líder' else
           --                            case when ENTITY_TYPE='BROKER' then 'Corretor' else
           --                            case when ENTITY_TYPE='CLAIM' then 'Sinistro' else
           --                            case when ENTITY_TYPE='FOLLOWER' then 'Seguidor'  else
           --                            case when ENTITY_TYPE='AGENCY' then 'Agência' else
           --                            case when ENTITY_TYPE='TÉCNICA' then 'Técnica' else
           --                            ENTITY_TYPE 
           --                            end end end end end end end
           --     end as ENTITY_TYPE  
                
                
                
                
            --     CASE WHEN @LANG_ID=1 THEN 
									   --CASE WHEN ENTITY_TYPE='CUSTOMER' THEN 'Customer' + ' ('+ISNULL(C.FIRST_NAME,'')+' '+ISNULL(C.MIDDLE_NAME,'')+' '+ISNULL(C.LAST_NAME,'')+')'  
									   --WHEN ENTITY_TYPE='LEADER' THEN 'Leader' 
            --                           WHEN ENTITY_TYPE='BROKER' THEN 'Broker' 
            --                           WHEN ENTITY_TYPE='CLAIM' THEN 'Claim' 
            --                           WHEN ENTITY_TYPE='FOLLOWER' THEN 'Follower' 
            --                           WHEN ENTITY_TYPE='AGENCY' THEN 'Agency' 
            --                           WHEN ENTITY_TYPE='CARRIER' THEN 'Carrier'
            --                           WHEN ENTITY_TYPE='CLM_BENF' THEN 'CLM_BENF'   
            --                           WHEN ENTITY_TYPE='CLM_DEPT' THEN 'CLM_DEPT' 
            --                           WHEN ENTITY_TYPE='CLM_HO' THEN 'CLM_HO'    
            --                           WHEN ENTITY_TYPE='CLM_REINS' THEN 'CLM_REINS' 
            --                           ELSE
            --                           ENTITY_TYPE
            --                           END
                                       
            --             ELSE
									   --CASE WHEN ENTITY_TYPE='CUSTOMER' THEN 'Cliente'+ ' ('+ISNULL(C.FIRST_NAME,'')+' '+ISNULL(C.MIDDLE_NAME,'')+' '+ISNULL(C.LAST_NAME,'')+')'   
            --                           WHEN ENTITY_TYPE='LEADER' THEN 'Líder' 
            --                           WHEN ENTITY_TYPE='BROKER' THEN 'Corretor' 
            --                           WHEN ENTITY_TYPE='CLAIM' THEN 'Sinistro' 
            --                           WHEN ENTITY_TYPE='FOLLOWER' THEN 'Seguidor'  
            --                           WHEN ENTITY_TYPE='AGENCY' THEN 'Agência' 
            --                           WHEN ENTITY_TYPE='CARRIER' THEN 'Técnica' 
            --                           WHEN ENTITY_TYPE='CLM_BENF' THEN 'BENEFICIARIO'   
            --                           WHEN ENTITY_TYPE='CLM_DEPT' THEN 'ORGAO EMISSOR - SINISTRO(ARQUIVO)'   
            --                           WHEN ENTITY_TYPE='CLM_HO' THEN 'MATRIZ – CONTABILIDADE' 
            --                           WHEN ENTITY_TYPE='CLM_REINS' THEN 'RESSEGURO' 
            --                           ELSE   
            --                           ENTITY_TYPE 
            --                           END
                                       
          CASE WHEN ENTITY_TYPE='CUSTOMER' THEN dbo.fun_GetMessage(423,@LANG_ID) + ' ('+ISNULL(C.FIRST_NAME,'')+' '+ISNULL(C.MIDDLE_NAME,'')+' '+ISNULL(C.LAST_NAME,'')+')'      
            WHEN ENTITY_TYPE='LEADER' THEN dbo.fun_GetMessage(424,@LANG_ID)      
                                       WHEN ENTITY_TYPE='BROKER' THEN dbo.fun_GetMessage(425,@LANG_ID)      
                                       WHEN ENTITY_TYPE='CLAIM' THEN dbo.fun_GetMessage(426,@LANG_ID)      
                                       WHEN ENTITY_TYPE='FOLLOWER' THEN dbo.fun_GetMessage(427,@LANG_ID)      
                                       WHEN ENTITY_TYPE='AGENCY' THEN dbo.fun_GetMessage(428,@LANG_ID)      
                                       WHEN ENTITY_TYPE='CARRIER' THEN dbo.fun_GetMessage(429,@LANG_ID)      
                                       WHEN ENTITY_TYPE='CLM_BENF' THEN dbo.fun_GetMessage(411,@LANG_ID)        
                                       WHEN ENTITY_TYPE='CLM_DEPT' THEN dbo.fun_GetMessage(412,@LANG_ID)       
                                       WHEN ENTITY_TYPE='CLM_HO' THEN dbo.fun_GetMessage(413,@LANG_ID)      
                                       WHEN ENTITY_TYPE='CLM_REINS' THEN dbo.fun_GetMessage(414,@LANG_ID)      
                                       ELSE    
                                       ENTITY_TYPE                                 
                                       
                END AS ENTITY_TYPE   
           ,[AGENCY_ID]  
           ,[PICKFROM]  
           ,[FILE_NAME]  
           ,[PROCESS_ID]  
           ,[IS_PROCESSED]  
           ,[ENTITY_ID]  
           ,[ATTEMPTS]
           ,[PRINT_JOBS_ID]
    from PRINT_JOBS  J WITH(NOLOCK)   LEFT OUTER JOIN
         --- MODIEFIED BY SANTOSH KUMAR GAUTAM ON 22 JUNE 2011
         --- DISPLAY CUSTOMER NAME WITH TYPES
		 POL_APPLICANT_LIST P WITH(NOLOCK) ON J.CUSTOMER_ID=P.CUSTOMER_ID AND J.POLICY_ID=P.POLICY_ID AND J.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND  P.APPLICANT_ID=ISNULL(j.ENTITY_ID,0) LEFT OUTER JOIN
		 CLT_APPLICANT_LIST C WITH(NOLOCK) ON P.APPLICANT_ID=C.APPLICANT_ID --AND J.ENTITY_TYPE='CUSTOMER'
   where J.CUSTOMER_ID=@CUSTOMER_ID and J.POLICY_ID=@POLICY_ID AND J.POLICY_VERSION_ID=@POLICY_VERSION_ID AND
   ISNULL(J.IS_ACTIVE,'N')='Y' AND --ITRACK - 1383
   --DOCUMENT_CODE = (CASE @DOCUMENT_CODE
			--		WHEN ''  THEN  DOCUMENT_CODE 
			--		ELSE @DOCUMENT_CODE 
			--	   END)
             (@DOCUMENT_CODE = '' OR DOCUMENT_CODE = @DOCUMENT_CODE) AND
			(@CLAIM_ID IS NULL OR CLAIM_ID = @CLAIM_ID)	AND
			(@ACTIVITY_ID IS NULL OR ACTIVITY_ID = @ACTIVITY_ID)	   
			
    ORDER BY ENTITY_TYPE   
        
     
   --select SUBSTRING(Entity_Type,1,1)+LOWER(SUBSTRING(Entity_Type,2,LEN(Entity_Type))) from PRINT_JOBS  WITH(NOLOCK)   
 ----IF (@ENTITY_TYPE = 'CUSTOMER')             
 -- SELECT PJ.FILE_NAME,PJ.DOCUMENT_CODE FROM PRINT_JOBS PJ WHERE       
 -- CUSTOMER_ID=@CUSTOMER_ID and ENTITY_TYPE='broker'      
 -- SELECT PJ.FILE_NAME,PJ.DOCUMENT_CODE FROM PRINT_JOBS PJ WHERE CUSTOMER_ID=2156          
 -- --and ENTITY_TYPE='CUSTOMER'      
 ----ELSE IF(@ENTITY_TYPE = 'BROKER')            
 ----SELECT PJ.FILE_NAME,PJ.DOCUMENT_CODE FROM PRINT_JOBS PJ WHERE CUSTOMER_ID=@CUSTOMER_ID            
             
END   
  


GO

