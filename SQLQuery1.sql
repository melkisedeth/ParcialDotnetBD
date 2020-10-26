

insert into Person (Identificacion,Nombre,Sexo,Edad,departamento,Ciudad,IdApoyo)
Values ('1','Juan','Masculino','23','Cesar','jagua','1');



insert into Apoyo (Id_Apoyo,Modalidad,Fecha,Aporte)
VALUES ('1','Economico','10/10/10','1000000');

select *from apoyo;
ALTER TABLE Person ADD CONSTRAINT Id_Apoyo_FK FOREIGN KEY (IdApoyo) 
REFERENCES Apoyo (Id_Apoyo);

go

commit;


SELECT p.Nombre, a.Aporte From person p 
join Apoyo a on (p.IdApoyo = a.Id_Apoyo)
where a.Id_Apoyo=1;

CREATE SEQUENCE  "AutoApoyo"  MINVALUE 1 MAXVALUE 9999 INCREMENT BY 1 START WITH 1;

-------------------------------------------------------------------------------------
use Tarea;

select *from apoyo;
select *from person;
delete from Apoyo;
delete from person;


DBCC CHECKIDENT (apoyo, RESEED, 0)
DBCC CHECKIDENT (person, RESEED, 0)

use Tarea;

CREATE TABLE [dbo].[Person](
[Identificacion] [nvarchar](50) NOT NULL PRIMARY KEY,
[Nombre] [nvarchar](50) NULL,
[Sexo] [nvarchar](15) NULL,
[Edad] [int] NULL,
[departamento] [nvarchar](20) NULL,
[Ciudad] [nvarchar](20) NULL
)

CREATE TABLE [dbo].[Apoyo](
[Id_Apoyo] int identity(1,1) not NULL,
[Modalidad] [nvarchar](50) NULL,
[Fecha] [nvarchar](50) NULL,
[Aporte] [nvarchar](50) NULL,
[Persona] [nvarchar](50) NOT NULL
)

/*ALTER TABLE Person ADD CONSTRAINT Id_Apoyo_FK FOREIGN KEY (IdApoyo) 
REFERENCES Apoyo (Id_Apoyo);*/

ALTER TABLE Apoyo ADD CONSTRAINT Id_Persona_FK FOREIGN KEY (Persona) 
REFERENCES Person (Identificacion);
go

commit;
select * from person;
select * from apoyo;
