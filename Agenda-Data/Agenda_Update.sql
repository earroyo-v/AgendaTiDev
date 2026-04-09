IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_Users_Email')
BEGIN
	CREATE UNIQUE NONCLUSTERED INDEX
	IX_Users_Email
	ON Usuario(Email);
END

CREATE TABLE Roles (
	Id INT PRIMARY KEY IDENTITY,
	Nombre NVARCHAR(50) NOT NULL,
	Descripcion NVARCHAR(50)
	)

ALTER TABLE Usuario
ADD IdRol INT

ALTER TABLE Usuario 
ADD CONSTRAINT Fk_Usuario_Roles
FOREIGN KEY (IdRol) REFERENCES Roles(Id)

INSERT INTO Roles Values
('Admin','Administradores'),
('User','Usuario')

--SELECT*FROM Usuario U
--INNER JOIN Roles R ON U.IdUsuario = R.Id

--UPDATE Usuario SET IDROL = 2 WHERE IdUsuario = 2