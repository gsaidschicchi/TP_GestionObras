USE master;
GO

IF DB_ID('GestionObras') IS NULL
BEGIN
    CREATE DATABASE GestionObras;
END
GO

USE GestionObras;
GO

IF OBJECT_ID('dbo.Obra', 'U') IS NULL
BEGIN
    CREATE TABLE Obra
    (
        Codigo INT NOT NULL PRIMARY KEY,
        Nombre VARCHAR(100) NOT NULL,
        Direccion VARCHAR(150) NOT NULL,
        Estado VARCHAR(30) NOT NULL,
        EstadoSupervision VARCHAR(30) NOT NULL,
        InformadaAlSupervisor BIT NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.Contratista', 'U') IS NULL
BEGIN
    CREATE TABLE Contratista
    (
        CUIT VARCHAR(20) NOT NULL PRIMARY KEY,
        RazonSocial VARCHAR(100) NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.Operario', 'U') IS NULL
BEGIN
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
END
GO

IF OBJECT_ID('dbo.Supervisor', 'U') IS NULL
BEGIN
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
END
GO

IF OBJECT_ID('dbo.Cuadrilla', 'U') IS NULL
BEGIN
    CREATE TABLE Cuadrilla
    (
        Codigo INT NOT NULL PRIMARY KEY,
        Nombre VARCHAR(100) NOT NULL,
        CodigoObra INT NULL,
        CUITContratista VARCHAR(20) NULL
    );
END
GO

IF COL_LENGTH('dbo.Cuadrilla', 'CodigoObra') IS NULL
BEGIN
    ALTER TABLE Cuadrilla ADD CodigoObra INT NULL;
END
GO

IF COL_LENGTH('dbo.Cuadrilla', 'CUITContratista') IS NULL
BEGIN
    ALTER TABLE Cuadrilla ADD CUITContratista VARCHAR(20) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Cuadrilla_Obra')
BEGIN
    ALTER TABLE Cuadrilla
    ADD CONSTRAINT FK_Cuadrilla_Obra
    FOREIGN KEY (CodigoObra) REFERENCES Obra(Codigo);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Cuadrilla_Contratista')
BEGIN
    ALTER TABLE Cuadrilla
    ADD CONSTRAINT FK_Cuadrilla_Contratista
    FOREIGN KEY (CUITContratista) REFERENCES Contratista(CUIT);
END
GO

IF OBJECT_ID('dbo.Cuadrilla_Operario', 'U') IS NULL
BEGIN
    CREATE TABLE Cuadrilla_Operario
    (
        CodigoCuadrilla INT NOT NULL,
        IdCodigoOperario VARCHAR(10) NOT NULL,
        CONSTRAINT PK_Cuadrilla_Operario PRIMARY KEY (CodigoCuadrilla, IdCodigoOperario),
        CONSTRAINT FK_CuadrillaOperario_Cuadrilla FOREIGN KEY (CodigoCuadrilla) REFERENCES Cuadrilla(Codigo),
        CONSTRAINT FK_CuadrillaOperario_Operario FOREIGN KEY (IdCodigoOperario) REFERENCES Operario(IdCodigo)
    );
END
GO

IF OBJECT_ID('dbo.Usuario', 'U') IS NULL
BEGIN
    CREATE TABLE Usuario
    (
        IdUsuario INT NOT NULL PRIMARY KEY,
        Usuario VARCHAR(50) NOT NULL,
        Password VARCHAR(64) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Usuario_Usuario' AND object_id = OBJECT_ID('dbo.Usuario'))
BEGIN
    CREATE UNIQUE INDEX UX_Usuario_Usuario ON Usuario(Usuario);
END
GO

IF NOT EXISTS (SELECT 1 FROM Usuario WHERE Usuario = 'admin')
BEGIN
    INSERT INTO Usuario (IdUsuario, Usuario, Password)
    VALUES
    (
        1,
        'admin',
        '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4'
    );
END
GO
