--1--
select Dnum,Dname,Fname from Departments inner join Employee
	on SSN=MGRSSN;

--2--
select Dname,Pname from Departments d inner join Project p
	on p.Dnum=d.Dnum;

--3--
select e.Fname,d.* from Employee e inner join Dependent d 
	on e.SSN= d.ESSN;

--4--
select Pnumber,Pname,Plocation from Project 
	where City in ('Cairo', 'Alex');

--5--
select * from Project 
	where Pname LIKE 'A%';

--6--
select * from Employee
	where Dno=30 and Salary between 1000 and 2000;

--7--
select * from Employee e inner join Works_for w
	on e.SSN=w.ESSn
	inner join Project p  on p.Pnumber=w.Pno 
	where e.Dno=10 and w.Hours>=10 and p.Pname='AL Rabwah';
	--OR--
select * from Employee e , Works_for w, Project p 
	where e.SSN=w.ESSn and p.Pnumber=w.Pno 
	and e.Dno=10 and w.Hours>=10 and p.Pname='AL Rabwah';

--8--
select e.* ,s.Fname from Employee e, Employee s 
	where e.Superssn=s.SSN and CONCAT(s.Fname, ' ', s.Lname) = 'Kamel Mohamed';

--9--
select * from Employee e , Works_for w, Project p 
	where e.SSN=w.ESSn and p.Pnumber=w.Pno 
	ORDER BY p.Pname;

--10--
select p.Pnumber ,d.Dname,e.Lname,e.Address,e.Bdate from Employee e , Departments d, Project p 
	where e.SSN=d.MGRSSN and d.Dnum=p.Dnum and City='Cairo';

--11--
select e.* from Employee e,Departments d
	where e.SSN=d.MGRSSN;

--12--
select e.*,d.* from Employee e left outer join Dependent d 
	on e.SSN= d.ESSN;

--DML--

--1--
insert into Employee (Fname, Lname, SSN, Dno, Superssn, Salary)
values ('Heba', 'Mohammed', 102672, 30, 112233, 3000);

--2--
insert into Employee (Fname, Lname, SSN, Dno)
values ('Menna', 'Mohammed', 102660, 30);

--3--
update Employee
set Salary = Salary * 1.2
where SSN = 102672;