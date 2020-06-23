USE [Tiendita]
GO
/****** Object:  StoredProcedure [dbo].[sp_ProductosAgotados]    Script Date: 13/06/2020 12:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sp_ProductosAgotados]

as
begin

select Productos.IDProducto,Productos.proNombre,Productos.proCantidad,Categorias.catNombre,Proveedores.provNombre
from Productos
inner join Proveedores on Productos.RUC=Proveedores.RUC
inner join Categorias on Productos.IDCategoria= Categorias.IDCategoria
where proCantidad=0

end