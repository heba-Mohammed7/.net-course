
--1--
select d.Dependent_name, d.Sex
from Dependent d
join Employee e on  e.SSN= d.ESSN 
where d.Sex = 'F' and e.Sex = 'F'
union
select d.Dependent_name, d.Sex
from Dependent d
join Employee e on  e.SSN=d.ESSN
where d.Sex = 'M' and e.Sex = 'M';

--2--
select p.Pname, sum(w.Hours)
from Project p
join Works_for w on p.Pnumber = w.Pno
group by p.Pname;

--3--
select top(1) d.*
from Departments d
join Employee e on  e.Dno = d.Dnum 
order by e.SSN asc;

--4--
select d.Dname,
       max(e.Salary) as maxSalary,
       min(e.Salary) as minSalary,
       avg(e.Salary) as avgSalary
from Departments d
join Employee e on  e.Dno = d.Dnum 
group by d.Dname;

--5-- check
select Lname
from Employee 
EXCEPT
select Lname
from Employee
join Dependent on Employee.SSN = Dependent.ESSN;


--6--
select d.Dnum, d.Dname, count(e.SSN)
from Departments d
join Employee e on e.Dno = d.Dnum 
group by d.Dnum, d.Dname
having avg(e.Salary) < (select avg(Salary) from Employee);

--7--
select d.Dname, e.Fname, e.Lname, p.Pname
from Departments d
join Employee e on e.Dno = d.Dnum
join Works_for w on e.SSN = w.ESSn
join Project p on p.Pnumber = w.Pno
order by d.Dname,e.Fname, e.Lname;


--8--
select top(2)Salary
from Employee
order by Salary desc


--9--
select CONCAT(Employee.Fname, ' ', Employee.Lname) AS FullName
from Employee
where CONCAT(Employee.Fname, ' ', Employee.Lname) IN (
    select Dependent.Dependent_name from Dependent
);

--10--
update Employee
SET Salary = Salary * 1.3
where SSN IN (
    select Employee.SSN
    from Employee
    join Works_for on Employee.SSN = Works_for.ESSn
    join Project on Project.Pnumber = Works_for.Pno 
    where Project.Pname = 'Al Rabwah'
);

--11--
select Employee.SSN, CONCAT(Employee.Fname, ' ', Employee.Lname) AS [FullName]
from Employee
where EXISTS (
    select 1
    from Dependent
    where Dependent.ESSN = Employee.SSN
);



--1--
insert into Departments (Dnum, Dname, MGRSSN, [MGRStart Date])
values (100, 'DEPT IT', 112233, '2006-11-01');

--2--
---a
update Departments
set MGRSSN = 968574, [MGRStart Date] = '2025-08-06'
where Dnum = 100;

---b
update Departments
set MGRSSN = 102672, [MGRStart Date] = '2025-08-06'
where Dnum = 20;
---c
update Employee
set Superssn = 102672, Dno = 20
where SSN = 102660;

--3--
BEGIN TRANSACTION;
DELETE FROM Dependent WHERE ESSN = 223344;
UPDATE Departments SET MGRSSN = 102672 WHERE MGRSSN = 223344;
UPDATE Employee SET Superssn = 102672 WHERE Superssn = 223344;
DELETE FROM Works_for WHERE ESSn = 223344;
DELETE FROM Employee WHERE SSN = 223344;
COMMIT;