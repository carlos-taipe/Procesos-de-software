create table Tipo (
IDTipo int primary key identity(1,1),
Nombre varchar(30) not null
)

create table Categoria(
IDCategoria int primary key identity(1,1),
Nombre varchar(30) not null
)

create table Documento(
IDDocumento int primary key identity(1,1),
Nombre varchar(5) not null
)


create table Proveedor(
IDProveedor int primary key identity(1,1),
Nombre varchar(150) not null,
Direccion varchar(150),
Telefono varchar(12),
IDDocumento int not null,
NumeroDocumento varchar(11) not null,
foreign key (IDDocumento) references Documento(IDDocumento)
)

create table Producto(
IDProducto int primary key identity(1,1),
Nombre varchar(150) not null,
Precio decimal(5,2) not null,
Cantidad int not null,
IDProveedor int not null,
IDCategoria int not null,
Foto image,
foreign key (IDProveedor) references Proveedor(IDProveedor),
foreign key (IDCategoria) references Categoria(IDCategoria)
)

create table Usuario(
DNI varchar(10) primary key,
Email varchar(100) not null,
Contraseña varchar(100) not null,
Nombre varchar(200) not null,
Direccion varchar(150),
Telefono varchar(12),
IDTipo int not null,
foreign key (IDTipo) references Tipo(IDTipo)
)

create table Venta(
IDVenta int primary key identity(1,1),
DNI varchar(10) not null,
Fecha date not null,
Monto decimal(7,2) not null,
foreign key (DNI) references Usuario(DNI)
)



create table Ingreso(
IDIngreso int primary key identity(1,1),
IDProveedor int not null,
DNI varchar(10) not null,
Fecha date not null,
Monto decimal(7,2) not null,
foreign key (DNI) references Usuario(DNI),
foreign key (IDProveedor) references Proveedor(IDProveedor)
)

create table DetalleVenta(
IDVenta int not null,
IDProducto int not null,
primary key (IDVenta,IDProducto),
foreign key (IDVenta) references Venta(IDVenta),
foreign key (IDProducto) references Producto(IDProducto)
)
create table DetalleIngreso(
IDIngreso int not null,
IDProducto int not null,
primary key(IDIngreso,IDProducto),
Cantidad int not null,
Monto decimal(5,2) not null,
foreign key (IDIngreso) references Ingreso(IDIngreso),
foreign key (IDProducto) references Producto(IDProducto)
)
