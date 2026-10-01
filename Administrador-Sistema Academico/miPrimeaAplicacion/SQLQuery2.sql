-- 1. Materias de prueba
IF NOT EXISTS (SELECT 1 FROM dbo.materias)
    INSERT INTO dbo.materias (codigo, nombre, uv)
    VALUES (101, 'Programacion I', 4),
           (102, 'Matematica I', 4),
           (103, 'Fisica I', 3);
GO

-- 2. Período de prueba
IF NOT EXISTS (SELECT 1 FROM dbo.periodos)
    INSERT INTO dbo.periodos (periodo, fecha)
    VALUES ('I-2026', GETDATE());
GO

-- 3. Una matrícula por alumno en ese período
INSERT INTO dbo.matricula (idAlumno, idPeriodo, fecha)
SELECT a.idAlumno, (SELECT MIN(idPeriodo) FROM dbo.periodos), GETDATE()
FROM dbo.alumnos a
WHERE NOT EXISTS (SELECT 1 FROM dbo.matricula m WHERE m.idAlumno = a.idAlumno);
GO

-- 4. Una fila de notas por matrícula y materia
INSERT INTO dbo.dnotas (idNota, idMateria)
SELECT m.idMatricula, ma.idMateria
FROM dbo.matricula m CROSS JOIN dbo.materias ma
WHERE NOT EXISTS (SELECT 1 FROM dbo.dnotas d
                  WHERE d.idNota = m.idMatricula AND d.idMateria = ma.idMateria);
GO