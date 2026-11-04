IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'db_parcial2_3')
    CREATE DATABASE db_parcial2_3;
GO

USE db_parcial2_3;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'usuarios_USSS081126')
    CREATE TABLE usuarios_USSS081126
    (
        idUsuario  INT IDENTITY(1,1) CONSTRAINT PK_usuarios_USSS081126 PRIMARY KEY,
        usuario    VARCHAR(35) NOT NULL CONSTRAINT UQ_usuarios_USSS081126_usuario UNIQUE,
        clave      VARCHAR(35) NOT NULL,
        nombre     VARCHAR(65) NOT NULL,
        direccion  VARCHAR(85) NULL,
        telefono   VARCHAR(9)  NULL
    );

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'alumnos')
    CREATE TABLE alumnos
    (
        idAlumno   INT IDENTITY(1,1) CONSTRAINT PK_alumnos PRIMARY KEY,
        codigo     VARCHAR(10) NOT NULL CONSTRAINT UQ_alumnos_codigo UNIQUE,
        nombre     VARCHAR(65) NOT NULL,
        direccion  VARCHAR(85) NULL,
        telefono   VARCHAR(9)  NULL
    );

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'materias')
    CREATE TABLE materias
    (
        idMateria  INT IDENTITY(1,1) CONSTRAINT PK_materias PRIMARY KEY,
        codigo     INT NOT NULL CONSTRAINT UQ_materias_codigo UNIQUE,
        nombre     VARCHAR(65) NOT NULL,
        uv         INT NOT NULL
    );

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'periodos')
    CREATE TABLE periodos
    (
        idPeriodo  INT IDENTITY(1,1) CONSTRAINT PK_periodos PRIMARY KEY,
        periodo    VARCHAR(30) NOT NULL,
        fecha      DATE NOT NULL
    );

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'notas')
    CREATE TABLE notas
    (
        idNota     INT IDENTITY(1,1) CONSTRAINT PK_notas PRIMARY KEY,
        idAlumno   INT NOT NULL CONSTRAINT FK_notas_alumnos  REFERENCES alumnos (idAlumno),
        idPeriodo  INT NOT NULL CONSTRAINT FK_notas_periodos REFERENCES periodos (idPeriodo),
        fecha      DATE NOT NULL
    );

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'dnotas')
    CREATE TABLE dnotas
    (
        idDetalle  INT IDENTITY(1,1) CONSTRAINT PK_dnotas PRIMARY KEY,
        idNota     INT NOT NULL CONSTRAINT FK_dnotas_notas    REFERENCES notas (idNota),
        idMateria  INT NOT NULL CONSTRAINT FK_dnotas_materias REFERENCES materias (idMateria),
        lab1       DECIMAL(4,2) NOT NULL CONSTRAINT DF_dnotas_lab1 DEFAULT 0,
        lab2       DECIMAL(4,2) NOT NULL CONSTRAINT DF_dnotas_lab2 DEFAULT 0,
        parcial    DECIMAL(4,2) NOT NULL CONSTRAINT DF_dnotas_parcial DEFAULT 0
    );
GO

IF NOT EXISTS (SELECT 1 FROM usuarios_USSS081126)
    INSERT INTO usuarios_USSS081126 (usuario, clave, nombre, direccion, telefono)
    SELECT 'invitado01', 'Invitado1', 'Usuario Invitado',      'Alegría, Usulután', '7000-0001' UNION ALL
    SELECT 'kvasquez',   'Vasquez88', 'Karla Beatriz Vásquez', 'Jucuapa, Usulután', '7611-4590' UNION ALL
    SELECT 'operador24', 'Turno24h',  'Operador de Turno',     'Berlín, Usulután',  '2663-7788';

IF NOT EXISTS (SELECT 1 FROM alumnos)
    INSERT INTO alumnos (codigo, nombre, direccion, telefono)
    SELECT 'SMU0012026', 'Valeria Nohemí Guzmán',  'Berlín, Usulután',          '7801-1101' UNION ALL
    SELECT 'SMU0022026', 'Óscar René Benítez',     'Alegría, Usulután',         '7801-1102' UNION ALL
    SELECT 'SMU0032026', 'Paola Lisseth Amaya',    'Tecapán, Usulután',         '7801-1103' UNION ALL
    SELECT 'SMU0042026', 'Rodrigo Ismael Saravia', 'Estanzuelas, Usulután',     '7801-1104';

IF NOT EXISTS (SELECT 1 FROM materias)
    INSERT INTO materias (codigo, nombre, uv)
    SELECT 301, 'Introducción a la Informática', 3 UNION ALL
    SELECT 302, 'Cálculo Diferencial',           4 UNION ALL
    SELECT 303, 'Programación Computacional I',  4;

IF NOT EXISTS (SELECT 1 FROM periodos)
    INSERT INTO periodos (periodo, fecha)
    SELECT 'Ciclo 1 - 2026', '2026-02-02' UNION ALL
    SELECT 'Ciclo 2 - 2026', '2026-08-03';
GO

IF NOT EXISTS (SELECT 1 FROM notas)
BEGIN
    INSERT INTO notas (idAlumno, idPeriodo, fecha)
    SELECT a.idAlumno, p.idPeriodo, p.fecha
    FROM alumnos a, periodos p;

    INSERT INTO dnotas (idNota, idMateria, lab1, lab2, parcial)
    SELECT n.idNota, m.idMateria,
           ROUND(5 + ((n.idNota * 19 + m.idMateria * 11) % 49) / 10.0, 1),
           ROUND(5 + ((n.idNota * 23 + m.idMateria * 7)  % 49) / 10.0, 1),
           ROUND(4 + ((n.idNota * 29 + m.idMateria * 13) % 59) / 10.0, 1)
    FROM notas n, materias m;
END
GO

SELECT 'usuarios' AS tabla, COUNT(*) AS filas FROM usuarios_USSS081126
UNION ALL SELECT 'alumnos',  COUNT(*) FROM alumnos
UNION ALL SELECT 'materias', COUNT(*) FROM materias
UNION ALL SELECT 'periodos', COUNT(*) FROM periodos
UNION ALL SELECT 'notas',    COUNT(*) FROM notas
UNION ALL SELECT 'dnotas',   COUNT(*) FROM dnotas;
GO
