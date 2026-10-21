IF OBJECT_ID('dbo.dnotas') IS NOT NULL DROP TABLE dbo.dnotas;
IF OBJECT_ID('dbo.matricula') IS NOT NULL DROP TABLE dbo.matricula;
GO

CREATE TABLE dbo.matricula (
    idMatricula INT IDENTITY (1, 1) NOT NULL,
    idAlumno    INT NOT NULL,
    idPeriodo   INT NOT NULL,
    fecha       DATE NOT NULL,
    PRIMARY KEY CLUSTERED (idMatricula ASC),
    CONSTRAINT FK_matricula_alumnos  FOREIGN KEY (idAlumno)  REFERENCES dbo.alumnos (idAlumno),
    CONSTRAINT FK_matricula_periodos FOREIGN KEY (idPeriodo) REFERENCES dbo.periodos (idPeriodo)
);
GO

CREATE TABLE dbo.dnotas (
    idDetalle INT IDENTITY (1, 1) NOT NULL,
    idNota    INT NOT NULL,
    idMateria INT NOT NULL,
    lab1      DECIMAL(4,2) NOT NULL DEFAULT 0,
    lab2      DECIMAL(4,2) NOT NULL DEFAULT 0,
    parcial   DECIMAL(4,2) NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED (idDetalle ASC),
    CONSTRAINT FK_dnotas_matricula FOREIGN KEY (idNota)    REFERENCES dbo.matricula (idMatricula),
    CONSTRAINT FK_dnotas_materias  FOREIGN KEY (idMateria) REFERENCES dbo.materias (idMateria)
);
GO

INSERT INTO dbo.matricula (idAlumno, idPeriodo, fecha)
SELECT a.idAlumno, (SELECT MIN(idPeriodo) FROM dbo.periodos), GETDATE()
FROM dbo.alumnos a;
GO

INSERT INTO dbo.dnotas (idNota, idMateria)
SELECT m.idMatricula, ma.idMateria
FROM dbo.matricula m CROSS JOIN dbo.materias ma;
GO