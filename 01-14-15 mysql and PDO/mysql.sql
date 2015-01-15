CREATE TABLE Students(
id INT NOT NULL,
name VARCHAR(31) NOT NULL,
age INT NOT NULL,
PRIMARY KEY(id),
INDEX(name)
)ENGINE=INNODB;

INSERT INTO Students
VALUES(1, 'Elliot', 21), (2, 'George', 25), (3, 'Thomas Johnson', 38), (4, 'Beth Gray', 5);

SELECT * FROM Students
WHERE age = 5;

SELECT * FROM Students
WHERE age = 5 OR name = 'Elliot';

DELETE FROM Students
WHERE age = 21;

UPDATE Students
SET name = 'George Brian'
WHERE name = 'George';

or double click in phpMyAdmin row value

SELECT * FROM Students
ORDER BY name;

SELECT COUNT(*) FROM Students;

SELECT SUM(age)
FROM Students;

SELECT AVG(age)
FROM Students;

SELECT age, COUNT(*) FROM Students
GROUP BY age;

SELECT * FROM Students
WHERE name like '%Brian';

CREATE TABLE Clubs(
studentId INT NOT NULL,
name VARCHAR(31) NOT NULL,
PRIMARY KEY(studentId),
INDEX(name)
)ENGINE=INNODB;

INSERT INTO Clubs
VALUES(1, 'Basketball club'), (2, 'Cooking club'), (3, 'Gamer club');

SELECT s.name FROM Students s
JOIN Clubs c ON s.id = c.studentId
WHERE c.name = 'Gamer club';

SELECT AVG(s.age) FROM Students s
JOIN Clubs c ON s.id = c.studentId
GROUP BY c.name;