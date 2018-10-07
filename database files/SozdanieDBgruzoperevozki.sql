USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'Грузоперевозки')
DROP DATABASE [Грузоперевозки]
GO
USE [master]
CREATE DATABASE [Грузоперевозки] ON  PRIMARY 
( NAME = N'Грузоперевозки', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Грузоперевозки.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Грузоперевозки_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Грузоперевозки_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
ALTER DATABASE [Грузоперевозки] SET COMPATIBILITY_LEVEL = 100
GO
 use [Грузоперевозки]
 
CREATE TABLE Водители (
	ВодительID           int IDENTITY(1,1),
	ФИО                  varchar(50) NOT NULL,
    МобТелефон           varchar(12) NOT NULL
       
       
)
go


ALTER TABLE Водители
       ADD PRIMARY KEY NONCLUSTERED (ВодительID)
go


CREATE TABLE ЗаказаноТоваров (
       ЗаказID              int NOT NULL,
       КодТовара            int NOT NULL,
       РасценкаТоннЗаКм     money NOT NULL,
       Количество           int NOT NULL,
       Масса                int NOT NULL
)
go


ALTER TABLE ЗаказаноТоваров
       ADD PRIMARY KEY NONCLUSTERED (КодТовара, ЗаказID)
go


CREATE TABLE Заказы (
       ЗаказID              int IDENTITY(1,1),
       СрокПоставки         date NOT NULL,
       ДатаЗаказа           date NOT NULL,
       КлиентID             int NOT NULL,
       МестоНазначения      varchar(50) NOT NULL,
       СостояниеID          int NOT NULL,
       ДатаДоставки         date NULL,
       ТрСредствоID         int NOT NULL,
       ВодительID           int NOT NULL
)
go


ALTER TABLE Заказы
       ADD PRIMARY KEY NONCLUSTERED (ЗаказID)
go


CREATE TABLE Клиенты (
       КлиентID             int IDENTITY(1,1),
       ФИО                  varchar(50) NOT NULL,
       Адрес                varchar(50) NOT NULL,
       Телефон              varchar(20) NOT NULL
)
go


ALTER TABLE Клиенты
       ADD PRIMARY KEY NONCLUSTERED (КлиентID)
go


CREATE TABLE СкладТоваров (
       КодТовара            int IDENTITY(1,1),
       Остаток              INTEGER NOT NULL,
       Цена                 MONEY NOT NULL,
       Наименование         varchar(50) NOT NULL
)
go


ALTER TABLE СкладТоваров
       ADD PRIMARY KEY NONCLUSTERED (КодТовара)
go


CREATE TABLE СостояниеЗаказа (
	СостояниеID          int NOT NULL,
    Состояние            varchar(50) NOT NULL
       
)
go


ALTER TABLE СостояниеЗаказа
       ADD PRIMARY KEY NONCLUSTERED (СостояниеID)
go


CREATE TABLE ТранспортноеСредство (
       ТрСредствоID         int IDENTITY(1,1),
       Марка                varchar(20) NOT NULL,
       Грузоподъемность     float NOT NULL
)
go


ALTER TABLE ТранспортноеСредство
       ADD PRIMARY KEY NONCLUSTERED (ТрСредствоID)
go


ALTER TABLE ЗаказаноТоваров
       ADD FOREIGN KEY (КодТовара)
                             REFERENCES СкладТоваров
go


ALTER TABLE ЗаказаноТоваров
       ADD FOREIGN KEY (ЗаказID)
                             REFERENCES Заказы
go


ALTER TABLE Заказы
       ADD FOREIGN KEY (ВодительID)
                             REFERENCES Водители
go


ALTER TABLE Заказы
       ADD FOREIGN KEY (ТрСредствоID)
                             REFERENCES ТранспортноеСредство
go


ALTER TABLE Заказы
       ADD FOREIGN KEY (СостояниеID)
                             REFERENCES СостояниеЗаказа
go


ALTER TABLE Заказы
       ADD FOREIGN KEY (КлиентID)
                             REFERENCES Клиенты
go



