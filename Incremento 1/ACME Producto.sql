
/*Procedimiento Añadir*/
create procedure sp_A_Producto
@Nombre varchar(150),
@Precio decimal(5,2),
@Cantidad int,
@IDProveedor int,
@IDCategoria int,
@Foto image
as 
begin
insert into Producto
values( 
@Nombre,
@Precio,
@Cantidad,
@IDProveedor,
@IDCategoria,
@Foto)
end


/*Procedimiento Cambiar*/
create procedure sp_M_Producto
@IDProducto int,
@Nombre varchar(150),
@Precio decimal(5,2),
@Cantidad int,
@IDProveedor int,
@IDCategoria int
as begin 
update Producto
set Nombre = @Nombre, Precio = @Precio, Cantidad = @Cantidad,
IDProveedor=@IDProveedor,IDCategoria = @IDCategoria 
where @IDProducto = IDProducto
end


/*Procedimiento Mostrar*/

create procedure sp_M_Producto
@Nombre varchar(150)
as
begin

select *from Producto where proNombre=@Nombre

end

/*Procedimiento Eliminar*/

create procedure sp_E_Producto
@IDProducto int,
@Estado int
as
begin

update Producto
set Estado=@Estado
where IDProducto=@IDProducto
end




