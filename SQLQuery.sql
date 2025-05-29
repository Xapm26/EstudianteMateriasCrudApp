    -- Crea la base de datos si no existe
    IF NOT EXISTS (
        SELECT name 
        FROM sys.databases 
        WHERE name = 'EstudianteMateriasDB'
    )
    BEGIN
        CREATE DATABASE EstudianteMateriasDB;
    END
    GO

    -- Usa la base de datos
    USE EstudianteMateriasDB;
    GO

    -- Creamos tabla Estudiantes si no existe
    IF NOT EXISTS (
        SELECT * 
        FROM sys.tables 
        WHERE name = 'Estudiantes' AND schema_id = SCHEMA_ID('dbo')
    )
    BEGIN
        CREATE TABLE dbo.Estudiantes (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Nombre NVARCHAR(100) NOT NULL,
            Documento NVARCHAR(50) NOT NULL UNIQUE,
            Correo NVARCHAR(100) NOT NULL
        );
    END
    GO

    -- Creamos tabla Materias si no existe
    IF NOT EXISTS (
        SELECT * 
        FROM sys.tables 
        WHERE name = 'Materias' AND schema_id = SCHEMA_ID('dbo')
    )
    BEGIN
        CREATE TABLE dbo.Materias (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Nombre NVARCHAR(100) NOT NULL,
            Codigo NVARCHAR(100) NOT NULL UNIQUE,
            Creditos INT NOT NULL
        );
    END
    GO

    -- Creamos tabla Inscripciones (relación muchos a muchos)
    CREATE TABLE Inscripciones (
        Id INT IDENTITY(1,1) PRIMARY KEY DEFAULT,
        EstudianteId INT NOT NULL,
        MateriaId INT NOT NULL,
        FechaInscripcion DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Inscripciones_Estudiantes FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id) ON DELETE CASCADE,
        CONSTRAINT FK_Inscripciones_Materias FOREIGN KEY (MateriaId) REFERENCES Materias(Id) ON DELETE CASCADE,
        CONSTRAINT UQ_Inscripcion UNIQUE (EstudianteId, MateriaId) 
    );
    DELETE FROM Inscripciones;
    GO
