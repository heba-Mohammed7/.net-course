--1--
select *from Employee;

--2--
select Fname,Lname,Salary,Dno from Employee;

--3--
select Pname,Plocation,Dnum from Project;

--4--
select concat(fname, ' ', lname) AS FullName,
       salary * 12 * 0.10 AS [ANNUAL COMM] from EMPLOYEE;

--5--
select SSN,Fname,salary from Employee
	where Salary>=1000;

--6--
select SSN,Fname,salary from Employee
	where Salary *12>=10000;

--7--
select Fname,salary from Employee
	where Sex='F';

--8--
select Dnum,Dname from Departments
	where MGRSSN =968574;

--9--
select Pnumber,Pname,Plocation from Project
	where Dnum=10;