if exists(Select * from PAGNET_INTERFACE_CODES where ROW_ID=464)
update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Documento rejeitado pelo Pagnet-Retornou para Ebix',INTERFACE_CODE='990'
where ROW_ID=464

if exists(Select * from PAGNET_INTERFACE_CODES where ROW_ID=473)
update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Documento rejeitado pelo Pagnet-Retornou para Ebix',INTERFACE_CODE='990'
where ROW_ID=473

if exists(Select * from PAGNET_INTERFACE_CODES where ROW_ID=465)
update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Documento rejeitado pelo Pagnet-Esperando por correção no Pagnet',INTERFACE_CODE='991'
where ROW_ID=465


if exists(Select * from PAGNET_INTERFACE_CODES where ROW_ID=474)
update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Documento rejeitado pelo Pagnet-Esperando por correção no Pagnet',INTERFACE_CODE='991'
where ROW_ID =474
