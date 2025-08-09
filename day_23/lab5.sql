--1--
select COUNT(*) as StudentCount
from Student
WHERE St_Age is not null;

--2--
select distinct Ins_Name
from Instructor;

--3--
select 
    St_Id as [Student ID],
    isnull(St_Fname + ' ' + St_Lname, 'Unknown') as [Student Full Name],
    isnull(Dept_Name, 'No Department') as [Department Name]
from Student s
left join Department d on s.Dept_Id = d.Dept_Id;

--4--
select 
    Ins_Name as [Instructor Name],
    isnull(Dept_Name, 'No Department') as [Department Name]
from Instructor i
left join Department d on i.Dept_Id = d.Dept_Id;

--5--
select 
    isnull(s.St_Fname + ' ' + s.St_Lname, 'Unknown') as [Student Full Name],
    c.Crs_Name as [Course Name]
from Student s
inner join Stud_Course sc on s.St_Id = sc.St_Id
inner join Course c on sc.Crs_Id = c.Crs_Id
where sc.Grade is not null;

--6--
select 
    t.Top_Name as [Topic Name],
    count(c.Crs_Id) as [Number of Courses]
from Topic t
left join Course c on t.Top_Id = c.Top_Id
group by t.Top_Name;

--7--
select 
    max(Salary) as [Max Salary],
    min(Salary) as [Min Salary]
from Instructor;


--8--
select 
    Ins_Name as [Instructor Name],
    Salary as [Salary]
from Instructor
where Salary < (select avg(Salary) from Instructor where Salary is not null);


--9--
select 
    Dept_Name as [Department Name]
from Department d
inner join Instructor i on d.Dept_Id = i.Dept_Id
where i.Salary = (select min(Salary) from Instructor where Salary is not null);

--10--
select top 2 Salary as [Salary]
from Instructor
where Salary is not null
order by Salary desc;

--11--
select 
    Ins_Name AS [Instructor Name],
    COALESCE(cast(Salary AS DECIMAL(10,2)), 0.00) AS [Compensation]
FROM Instructor;

--12--
select 
    avg(Salary) AS [Average Salary]
from Instructor
where Salary is not null;


--13--
select 
    s.st_fname as [student first name],
    i.ins_name as [supervisor name]
from student s
left join department d on s.dept_id = d.dept_id
left join instructor i on d.dept_id = i.dept_id;

--14--
select ins_name as [instructor name],
    salary as [salary],
    dept_name as [department name],
    rank_value
from (
    select ins_name, salary, d.dept_name,
        rank() over (partition by d.dept_id order by salary desc) as rank_value
    from instructor i
    left join department d on i.dept_id = d.dept_id
    where salary is not null
) ranked_salaries
where rank_value <= 2;

--15--
select st_fname as [student first name], st_lname as [student last name], dept_name as [department name]
from (
    select st_fname, st_lname, d.dept_name,
        row_number() over (partition by d.dept_id order by newid()) as row_num
    from student s
    left join department d on s.dept_id = d.dept_id
) randomized_students
where row_num = 1;




-------------------   part 2 -------------------
--1--
select 
    salesorderid as [sales order id],
    shipdate as [ship date]
from SalesLT.salesorderheader
where shipdate between '2002-07-28' and '2014-07-29';

--2--
select productid as [product id],
       name as [product name]
from saleslt.product
where standardcost < 110.00;

--3--
select productid as [product id], name as [product name]
from saleslt.product
where weight is null;

--4--
select *
from saleslt.product
where color in ('silver', 'black', 'red');

--5--
select *
from saleslt.product
where name like 'b%';

--6--
update saleslt.productdescription
set description = 'chromoly steel_high of defects'
where productdescriptionid = 3;

select *
from saleslt.productdescription
where description like '%\_%' escape '\';

--7--
select orderdate,
       sum(totaldue) as totalsales
from saleslt.salesorderheader
where orderdate between '2001-07-01' and '2014-07-31'
group by orderdate
order by orderdate;

--8--
select distinct ModifiedDate as [hire date]
from saleslt.Customer;

--9--
select avg(distinct listprice)
from saleslt.product;

--10--
select 'the ' + name + ' is only! ' + cast(listprice as varchar)
from saleslt.product
where listprice between 100 and 120
order by listprice;

--11--

--12--
select convert(varchar, getdate(), 101) as todayDate
union
select convert(varchar, getdate(), 102)
union
select convert(varchar, getdate(), 103)
union
select convert(varchar, getdate(), 120);
