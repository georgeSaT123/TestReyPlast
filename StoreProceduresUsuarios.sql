CREATE TABLE USUARIO(
	Id int NOT NULL PRIMARY KEY,
	Nombres varchar(150),
	Apellidos varchar(150)
);

Select Id, Nombres, Apellidos From USUARIO;

Insert Into USUARIO(Id, Nombres, Apellidos)
Values(1, 'Henry', 'Huertas');

UPDATE USUARIO SET Nombres = 'George Kevin', Apellidos = 'Sanchez Tomasto' WHERE Id = 2

CREATE PROCEDURE SP_MostrarUsuario
AS
BEGIN
	Select Id, Nombres, Apellidos From USUARIO;
END

EXEC SP_MostrarUsuario

CREATE PROCEDURE SP_InsertarUsuario
	@Id int,
	@Nombres varchar(150),
	@Apellidos varchar(150)
AS
BEGIN
	Insert Into USUARIO(Id, Nombres, Apellidos)
	Values(@Id, @Nombres, @Apellidos);
END

EXEC SP_InsertarUsuario 2, 'George', 'Sanchez';

CREATE PROCEDURE SP_ActualizarUsuario
	@Id int,
	@Nombres varchar(150),
	@Apellidos varchar(150)
AS
BEGIN
	UPDATE USUARIO SET Nombres = @Nombres, Apellidos = @Apellidos WHERE Id = @Id
END

EXEC SP_ActualizarUsuario 2, 'George', 'Sanchez';

CREATE PROCEDURE SP_EliminarUsuario
	@Id int
AS
BEGIN
	DELETE FROM USUARIO WHERE Id = @Id;
END

EXEC SP_EliminarUsuario 3