# Adatbáziskezelés SQL alapon
## Adatbázis szerkezetének módosítása
## Létrehozás
``` CREATE DATABASE adatbazis_neve ```

## Törlés
``` DROP DATABASE adatbazis_neve ```

## Tábla létrehozása
``` sql
USE adatbazis_neve;
CREATE TABLE test_table (
    egesz int PRIMARY KEY,
    nemegesz float,
    karakterlanc varchar(50),
    datum date,
    ido time,
    idobelyeg timestamp,
    datumesido datetime
);
```

## Minta
``` sql
DROP DATABASE IF EXISTS student_db;
CREATE DATABASE student_db CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE student_db;
CREATE TABLE students(
    id int PRIMARY KEY AUTO_INCREMENT,
    name varchar(100) NOT NULL,
    birth_date date
);
CREATE TABLE classes (
	id int PRIMARY KEY AUTO_INCREMENT,
    identifier varchar(10) UNIQUE
);

CREATE TABLE students_classes (
    class_id int REFERENCES classes(id),
    student_id int REFERENCES students(id),
    PRIMARY KEY(class_id, student_id)
);

INSERT INTO students(id, name, birth_date) VALUES
(1, "Kis Miska", "2000-10-10"),
(2, "Nagy Aladár", "2004-12-01"),
(3, "Abdul Musztafa",NULL);
INSERT INTO classes(id, identifier) VALUES
(1, "10.B"),
(2, "12.C");

INSERT INTO students_classes(class_id, student_id) VALUES
(1,1),
(2,1),
(3,2);
```
## Lekérdezések
### Minden adat kilistázása egy táblából
SELECT * FROM students;
### Meghatározott adatok kilistázása egy táblából
SELECT name FROM students;
SELECT name, birth_date FROM students;
### Adatok listázása feltétel mentén
SELECT * FROM students WHERE birth_date IS NOT NULL;
### Rendezés
#### Növekvõ
SELECT * FROM students ORDER BY name ASC;
vagy csak simán ASC nélkül (ez az alapértelmezett)
SELECT * FROM students ORDER BY name;
#### Csökkenõ
SELECT * FROM students ORDER BY name DESC;

# EntityFrameworkCoreSamples

## ORM (Object Relational Mapping)
ORM-nek nevezzük azokat a szoftverelemeket melyek arra hivatottak, hogy összekösse a kódunkat egy adatbázissal.
Minden valamit magára adó keretrendszernek van ilyen eleme. .NET-ben az Entity Framework nyújtja ezt.

## Stratégiák
- **Code First:** Amikor a kódunkból kiindulva generáljuk le az adatbázist
- **Database First:** Amikor az adatbázisból kiindulva generáljuk le a kódokat amin keresztül használhatjuk.
Ha nincs adatbázis amihez muszáj alkalmazkodnod érdemes a Code First stratégiát alkalmazni, kevesebb kódolási szükséglet miatt.

## Entity Frameworkról általánosságban

### Verziók

#### Keretrendszer verziói

Régebben a .NET következõképpen nézett ki:
- **.NET Framework:** Fõ verzió, a legteljesesebb funkcionalitást biztosította, Windowson mûködött csak
- **.NET Core:** Platformfüggetlen verzió, csökkentett keretrendszer funkcionalitással
- **.NET Standard:** .NET szabványosított verziója

.NET 5-tõl ez megváltozott, a .NET Framework fejlesztése megállt a fõ verzió a .NET Core lett amit ezután csak simán .NET-nek hívtak.

#### Entity Framework verziói

Entity Frameworkból is van két verzió:
- **Entity Framework:** .NET Framework ORM implementációja
- **Entity Framework Core:** .NET Core majd a .NET ORM implementációja

Mivel mi .NET-et használunk már mindig az Entity Framework Core-t fogjuk majd használni.

## Entity Framework Core alapok

### Kód-adatbázis közti kapcsolat kialakítása SQL Server esetén CodeFirst megközelítéssel

#### Csomagok telepítése
Nuget Package Managerben telepítsük a projektbe a következõ csomagokat:
- **Microsoft.EntityFrameworkCore:** Az alap ORM logikát kapjuk ezáltal. (Pl.: DbContext osztály)
- **Microsoft.EntityFrameworkCore.SqlServer:** Hogy egy nyelvet beszéljen az ORM az Sql Serverrel
- **Microsoft.EntityFrameworkCore.Design:** Code first-el kapcsolatos utasításokhoz kell.

#### Modell kialakítása

##### Context
