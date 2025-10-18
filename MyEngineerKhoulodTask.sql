create database Employee_Task;
go

use Employee_Task;
go

create table [User]
(
    UserID          int primary key,
    Name            nvarchar(30) not null,
    Password        nvarchar(30) not null,
    Email           nvarchar(30) not null,
    Role            nvarchar(30) check([Role] in ('Employee','Manager'))
);
go

create table Task
(
    TaskID           int primary key,
    Title            nvarchar(40) not null,
    [Description]    nvarchar(max),
    [Status]         nvarchar(30) check([Status] in ('Pending','In Progress','Completed')),
    DueDate          datetime,
    UserId           int references [User](UserID)
);
go

insert into [User] values 
(1, 'sarah mostafa', 'sarah123', 'sarah@gmail.com', 'Manager'),
(2, 'soli mostafa', 'soli123', 'soli@gmail.com', 'Employee'),
(3, 'salma mostafa', 'saly123', 'saly@gmail.com', 'Employee'),
(4, 'habiba', 'habiba123', 'habib@gmail.com', 'Employee');
go

insert into Task values
(101, 'Frontend', 'Finish The Login Page', 'Pending', '2025-10-17', 2),
(102, 'Backend', 'Add the Payment Service', 'In Progress', '2025-10-17', 2),
(103, 'Deployment', 'Deploye The Last Version', 'Completed', '2025-10-17', 2);
go
insert into Task values(104, 'Frontend', 'Tring', 'Pending', '2025-10-17', 2);
