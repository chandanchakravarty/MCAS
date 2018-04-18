IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERTXMLFORPDF]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERTXMLFORPDF]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PROC_INSERTXMLFORPDF] 
             

            @CUSTOMER_ID varchar(10), 

            @POLICY_ID varchar(10), 

            @POLICY_VERSION_ID varchar(10), 

			@CALLED_FOR nvarchar(20),			

            @CUSTOMER_XML text --for xml

AS                                          

BEGIN     

INSERT INTO ACT_PREMIUM_NOTICE_PROCCESS_LOG
            (

                        CUSTOMER_ID ,

                        POLICY_ID ,

                        POLICY_VERSION_ID,
						
						CALLED_FOR,
						
                        DEC_CUSTOMERXML, 
						
						DATE_TIME

            )            

            VALUES            

            (       

                        @CUSTOMER_ID,

                        @POLICY_ID,

                        @POLICY_VERSION_ID,
						
						@CALLED_FOR,

                        @CUSTOMER_XML,

						GETDATE()

            )

END









GO

