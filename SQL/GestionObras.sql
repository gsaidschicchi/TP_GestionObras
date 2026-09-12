USE GestionObras;
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

INSERT INTO Obra
(
    Codigo,
    Nombre,
    Direccion,
    Estado,
    EstadoSupervision,
    InformadaAlSupervisor
)
VALUES
(
    1,
    'Obra Norte',
    'Av. Siempre Viva 123',
    'PENDIENTE',
    'PENDIENTE',
    0
);
GO

INSERT INTO Obra
(
    Codigo,
    Nombre,
    Direccion,
    Estado,
    EstadoSupervision,
    InformadaAlSupervisor
)
VALUES
(
    2,
    'Obra Sur',
    'Calle Falsa 1234',
    'PENDIENTE',
    'PENDIENTE',
    0
);
GO

SELECT *
FROM Obra;
GO

DROP TABLE Cuadrilla;
GO

SELECT *
FROM Cuadrilla;
GO

CREATE TABLE Cuadrilla
(
    Codigo INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    CodigoObra INT NULL
);
GO

ALTER TABLE Cuadrilla
ADD CONSTRAINT FK_Cuadrilla_Obra
FOREIGN KEY (CodigoObra)
REFERENCES Obra(Codigo);
GO



INSERT INTO Obra
(
    Codigo,
    Nombre,
    Direccion,
    Estado,
    EstadoSupervision,
    InformadaAlSupervisor
)
VALUES
(
    3,
    'Obra Centro',
    'Av. Rivadavia 2500',
    'PENDIENTE',
    'PENDIENTE',
    0
);
GO

SELECT *
FROM Cuadrilla;
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

INSERT INTO Operario
(
    IdCodigo,
    DNI,
    Nombre,
    Apellido,
    Telefono,
    SueldoBase,
    Legajo,
    Especialidad
)
VALUES
(
    '000002',
    '32222333',
    'Carlos',
    'Gomez',
    '1155552222',
    950000,
    102,
    'Obra Civil'
);
GO

SELECT *
FROM Operario;
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

INSERT INTO Cuadrilla_Operario
(
    CodigoCuadrilla,
    IdCodigoOperario
)
VALUES
(
    1,
    '000001'
);
GO

INSERT INTO Cuadrilla_Operario
(
    CodigoCuadrilla,
    IdCodigoOperario
)
VALUES
(
    1,
    '000002'
);
GO

SELECT *
FROM Cuadrilla_Operario;
GO

SELECT
    C.Codigo AS CodigoCuadrilla,
    C.Nombre AS NombreCuadrilla,
    O.IdCodigo,
    O.Nombre,
    O.Apellido,
    O.Especialidad
FROM Cuadrilla C
INNER JOIN Cuadrilla_Operario CO
    ON C.Codigo = CO.CodigoCuadrilla
INNER JOIN Operario O
    ON CO.IdCodigoOperario = O.IdCodigo;
GO