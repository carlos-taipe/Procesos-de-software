create procedure sp_A_DetalleVenta


create function fnc_C_IDVenta
return int
declare @IDVenta int
as
select top 1 @IDVenta = IDVenta from Ventas order by IDVenta desc 
return @IDVenta 


@DetalleVenta as t_detalleVenta Readonly
as
begin
set nocount on
insert into  DetalleVenta (IDProducto,Cantidad,SubTotal,Precio)
select IDProducto,Cantidad,SubTotal,Precio from @DetalleVenta
end




create trigger TR_DetalleVenta
on ventas
for insert 

as
declare @IDVenta int
select top 1 @IDVenta=IDVenta from Ventas order by IDVenta desc
begin


set nocount on
insert into DetalleVenta 
end


