IF DB_ID('GestionObras') IS NULL
BEGIN
    CREATE DATABASE GestionObras;
END
GO

USE GestionObras;
GO

IF OBJECT_ID('Cuadrilla_Operario', 'U') IS NOT NULL
    DROP TABLE Cuadrilla_Operario;
GO

IF OBJECT_ID('Cuadrilla', 'U') IS NOT NULL
    DROP TABLE Cuadrilla;
GO

IF OBJECT_ID('Contratista', 'U') IS NOT NULL
    DROP TABLE Contratista;
GO

IF OBJECT_ID('Operario', 'U') IS NOT NULL
    DROP TABLE Operario;
GO

IF OBJECT_ID('Supervisor', 'U') IS NOT NULL
    DROP TABLE Supervisor;
GO

IF OBJECT_ID('Obra', 'U') IS NOT NULL
    DROP TABLE Obra;
GO

CREATE TABLE Obra
(
    Codigo INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(150) NOT NULL,
    Estado VARCHAR(30) NOT NULL,
    EstadoSupervision VARCHAR(30) NOT NULL,
    InformadaAlSupervisor BIT NOT NULL
);
GO

CREATE TABLE Contratista
(
    CUIT VARCHAR(20) NOT NULL PRIMARY KEY,
    RazonSocial VARCHAR(120) NOT NULL
);
GO

CREATE TABLE Operario
(
    IdCodigo VARCHAR(10) NOT NULL PRIMARY KEY,
    DNI VARCHAR(20) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Telefono VARCHAR(30) NULL,
    SueldoBase FLOAT NOT NULL,
    Legajo INT NOT NULL,
    Especialidad VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Supervisor
(
    IdCodigo VARCHAR(10) NOT NULL PRIMARY KEY,
    DNI VARCHAR(20) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Telefono VARCHAR(30) NULL,
    SueldoBase FLOAT NOT NULL,
    IdSupervisor INT NOT NULL,
    Sector VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Cuadrilla
(
    Codigo INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    CodigoObra INT NULL,
    CUITContratista VARCHAR(20) NULL,

    CONSTRAINT FK_Cuadrilla_Obra
        FOREIGN KEY (CodigoObra)
        REFERENCES Obra(Codigo),

    CONSTRAINT FK_Cuadrilla_Contratista
        FOREIGN KEY (CUITContratista)
        REFERENCES Contratista(CUIT)
);
GO

CREATE TABLE Cuadrilla_Operario
(
    CodigoCuadrilla INT NOT NULL,
    IdCodigoOperario VARCHAR(10) NOT NULL,

    CONSTRAINT PK_Cuadrilla_Operario
        PRIMARY KEY (CodigoCuadrilla, IdCodigoOperario),

    CONSTRAINT FK_CuadrillaOperario_Cuadrilla
        FOREIGN KEY (CodigoCuadrilla)
        REFERENCES Cuadrilla(Codigo),

    CONSTRAINT FK_CuadrillaOperario_Operario
        FOREIGN KEY (IdCodigoOperario)
        REFERENCES Operario(IdCodigo)
);
GO

INSERT INTO Obra
(Codigo, Nombre, Direccion, Estado, EstadoSupervision, InformadaAlSupervisor)
VALUES
(1, 'Obra Norte', 'Av. Siempre Viva 123', 'PENDIENTE', 'PENDIENTE', 0),
(2, 'Obra Sur', 'Calle Falsa 1234', 'PENDIENTE', 'PENDIENTE', 0),
(3, 'Obra Centro', 'Av. Rivadavia 2500', 'PENDIENTE', 'PENDIENTE', 0);
GO

INSERT INTO Contratista (CUIT, RazonSocial)
VALUES ('30-12345678-9', 'Contratista Demo');
GO

INSERT INTO Operario
(IdCodigo, DNI, Nombre, Apellido, Telefono, SueldoBase, Legajo, Especialidad)
VALUES
('000001', '30111222', 'Juan', 'Perez', '1155551111', 1000000, 101, 'Fibra Optica'),
('000002', '32222333', 'Carlos', 'Gomez', '1155552222', 950000, 102, 'Obra Civil');
GO

INSERT INTO Supervisor
(IdCodigo, DNI, Nombre, Apellido, Telefono, SueldoBase, IdSupervisor, Sector)
VALUES
('000101', '28999888', 'Laura', 'Suarez', '1155553333', 1500000, 1, 'Supervision');
GO

INSERT INTO Cuadrilla (Codigo, Nombre, CodigoObra, CUITContratista)
VALUES (1, 'CUAD_1', NULL, '30-12345678-9');
GO

INSERT INTO Cuadrilla_Operario (CodigoCuadrilla, IdCodigoOperario)
VALUES
(1, '000001'),
(1, '000002');
GO

SELECT * FROM Obra;
SELECT * FROM Contratista;
SELECT * FROM Operario;
SELECT * FROM Supervisor;
SELECT * FROM Cuadrilla;
SELECT * FROM Cuadrilla_Operario;
GO
