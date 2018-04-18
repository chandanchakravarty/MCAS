--BEGIN TRANSACTION ABHI
If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=455 and INTERFACE_CODE=000)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Enviado para o Pagnet'
where ROW_ID=455 and INTERFACE_CODE=000
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=456 and INTERFACE_CODE=004)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Solicitação de Pagamento no Pagnet'
where ROW_ID=456 and INTERFACE_CODE=004
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=457 and INTERFACE_CODE=005)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento efetuado no Pagnet'
where ROW_ID=457 and INTERFACE_CODE=005
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=458 and INTERFACE_CODE=006)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado no Pagnet'
where ROW_ID=458 and INTERFACE_CODE=006
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=459 and INTERFACE_CODE=007)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado pelo Banco (com Observações)'
where ROW_ID=459 and INTERFACE_CODE=007
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=460 and INTERFACE_CODE=010)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento efetuado no Pagnet'
where ROW_ID=460 and INTERFACE_CODE=010
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=461 and INTERFACE_CODE=011)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado pelo Banco (com Observações)'
where ROW_ID=461 and INTERFACE_CODE=011
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=462 and INTERFACE_CODE=012)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado no Pagnet'
where ROW_ID=462 and INTERFACE_CODE=012
End



If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=463 and INTERFACE_CODE=999)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Em Análise'
where ROW_ID=463 and INTERFACE_CODE=999
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=466 and INTERFACE_CODE=000)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Enviado para o Pagnet'
where ROW_ID=466 and INTERFACE_CODE=000
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=467 and INTERFACE_CODE=002)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Solicitação de Pagamento no Pagnet'
where ROW_ID=467 and INTERFACE_CODE=002
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=468 and INTERFACE_CODE=003)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento efetuado no Pagnet'
where ROW_ID=468 and INTERFACE_CODE=003
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=469 and INTERFACE_CODE=004)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado no Pagnet'
where ROW_ID=469 and INTERFACE_CODE=004
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=470 and INTERFACE_CODE=005)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Pagamento Cancelado pelo Banco (com Observações)'
where ROW_ID=470 and INTERFACE_CODE=005
End


If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=471 and INTERFACE_CODE=006)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Em Análise'
where ROW_ID=471 and INTERFACE_CODE=006
End

If Exists(select * from PAGNET_INTERFACE_CODES  where ROW_ID=472 and INTERFACE_CODE=999)
Begin
Update PAGNET_INTERFACE_CODES
set PORT_DESCRIPTION='Em Análise'
where ROW_ID=472 and INTERFACE_CODE=999
End
--ROLLBACK TRANSACTION ABHI











