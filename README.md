# Adatb�ziskezel�s SQL alapon
## Adatb�zis szerkezet�nek m�dos�t�sa
## L�trehoz�s
``` CREATE DATABASE adatbazis_neve ```

## T�rl�s
``` DROP DATABASE adatbazis_neve ```

## T�bla l�trehoz�sa
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
(2, "Nagy Alad�r", "2004-12-01"),
(3, "Abdul Musztafa",NULL);
INSERT INTO classes(id, identifier) VALUES
(1, "10.B"),
(2, "12.C");

INSERT INTO students_classes(class_id, student_id) VALUES
(1,1),
(2,1),
(3,2);
```
## Lek�rdez�sek
### Minden adat kilist�z�sa egy t�bl�b�l
SELECT * FROM students;
### Meghat�rozott adatok kilist�z�sa egy t�bl�b�l
SELECT name FROM students;
SELECT name, birth_date FROM students;
### Adatok list�z�sa felt�tel ment�n
SELECT * FROM students WHERE birth_date IS NOT NULL;
### Rendez�s
#### N�vekv�
SELECT * FROM students ORDER BY name ASC;
vagy csak sim�n ASC n�lk�l (ez az alap�rtelmezett)
SELECT * FROM students ORDER BY name;
#### Cs�kken�
SELECT * FROM students ORDER BY name DESC;

# EntityFrameworkCoreSamples

## ORM (Object Relational Mapping)
ORM-nek nevezz�k azokat a szoftverelemeket melyek arra hivatottak, hogy �sszek�sse a k�dunkat egy adatb�zissal.
Minden valamit mag�ra ad� keretrendszernek van ilyen eleme. .NET-ben az Entity Framework ny�jtja ezt.

## Strat�gi�k
- **Code First:** Amikor a k�dunkb�l kiindulva gener�ljuk le az adatb�zist
- **Database First:** Amikor az adatb�zisb�l kiindulva gener�ljuk le a k�dokat amin kereszt�l haszn�lhatjuk.
Ha nincs adatb�zis amihez musz�j alkalmazkodnod �rdemes a Code First strat�gi�t alkalmazni, kevesebb k�dol�si sz�ks�glet miatt.

## Entity Frameworkr�l �ltal�noss�gban

### Verzi�k

#### Keretrendszer verzi�i

R�gebben a .NET k�vetkez�k�ppen n�zett ki:
- **.NET Framework:** F� verzi�, a legteljesesebb funkcionalit�st biztos�totta, Windowson m�k�d�tt csak
- **.NET Core:** Platformf�ggetlen verzi�, cs�kkentett keretrendszer funkcionalit�ssal
- **.NET Standard:** .NET szabv�nyos�tott verzi�ja

.NET 5-t�l ez megv�ltozott, a .NET Framework fejleszt�se meg�llt a f� verzi� a .NET Core lett amit ezut�n csak sim�n .NET-nek h�vtak.

#### Entity Framework verzi�i

Entity Frameworkb�l is van k�t verzi�:
- **Entity Framework:** .NET Framework ORM implement�ci�ja
- **Entity Framework Core:** .NET Core majd a .NET ORM implement�ci�ja

Mivel mi .NET-et haszn�lunk m�r mindig az Entity Framework Core-t fogjuk majd haszn�lni.

## Entity Framework Core alapok

### K�d-adatb�zis k�zti kapcsolat kialak�t�sa SQL Server eset�n CodeFirst megk�zel�t�ssel

#### Csomagok telep�t�se
Nuget Package Managerben telep�ts�k a projektbe a k�vetkez� csomagokat:
- **Microsoft.EntityFrameworkCore:** Az alap ORM logik�t kapjuk ez�ltal. (Pl.: DbContext oszt�ly)
- **Microsoft.EntityFrameworkCore.SqlServer:** Hogy egy nyelvet besz�ljen az ORM az Sql Serverrel
- **Microsoft.EntityFrameworkCore.Design:** Code first-el kapcsolatos utas�t�sokhoz kell.

#### Modell kialak�t�sa

##### Context
