USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'��������������')
DROP DATABASE [��������������]
GO
USE [master]
CREATE DATABASE [��������������] ON  PRIMARY 
( NAME = N'��������������', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\��������������.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'��������������_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\��������������_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
ALTER DATABASE [��������������] SET COMPATIBILITY_LEVEL = 100
GO
 use [��������������]
 
CREATE TABLE �������� (
	��������ID           int IDENTITY(1,1),
	���                  varchar(50) NOT NULL,
    ����������           varchar(12) NOT NULL
       
       
)
go


ALTER TABLE ��������
       ADD PRIMARY KEY NONCLUSTERED (��������ID)
go


CREATE TABLE ��������������� (
       �����ID              int NOT NULL,
       ���������            int NOT NULL,
       ����������������     money NOT NULL,
       ����������           int NOT NULL,
       �����                int NOT NULL
)
go


ALTER TABLE ���������������
       ADD PRIMARY KEY NONCLUSTERED (���������, �����ID)
go


CREATE TABLE ������ (
       �����ID              int IDENTITY(1,1),
       ������������         date NOT NULL,
       ����������           date NOT NULL,
       ������ID             int NOT NULL,
       ���������������      varchar(50) NOT NULL,
       ���������ID          int NOT NULL,
       ������������         date NULL,
       ����������ID         int NOT NULL,
       ��������ID           int NOT NULL
)
go


ALTER TABLE ������
       ADD PRIMARY KEY NONCLUSTERED (�����ID)
go


CREATE TABLE ������� (
       ������ID             int IDENTITY(1,1),
       ���                  varchar(50) NOT NULL,
       �����                varchar(50) NOT NULL,
       �������              varchar(20) NOT NULL
)
go


ALTER TABLE �������
       ADD PRIMARY KEY NONCLUSTERED (������ID)
go


CREATE TABLE ������������ (
       ���������            int IDENTITY(1,1),
       �������              INTEGER NOT NULL,
       ����                 MONEY NOT NULL,
       ������������         varchar(50) NOT NULL
)
go


ALTER TABLE ������������
       ADD PRIMARY KEY NONCLUSTERED (���������)
go


CREATE TABLE ��������������� (
	���������ID          int NOT NULL,
    ���������            varchar(50) NOT NULL
       
)
go


ALTER TABLE ���������������
       ADD PRIMARY KEY NONCLUSTERED (���������ID)
go


CREATE TABLE �������������������� (
       ����������ID         int IDENTITY(1,1),
       �����                varchar(20) NOT NULL,
       ����������������     float NOT NULL
)
go


ALTER TABLE ��������������������
       ADD PRIMARY KEY NONCLUSTERED (����������ID)
go


ALTER TABLE ���������������
       ADD FOREIGN KEY (���������)
                             REFERENCES ������������
go


ALTER TABLE ���������������
       ADD FOREIGN KEY (�����ID)
                             REFERENCES ������
go


ALTER TABLE ������
       ADD FOREIGN KEY (��������ID)
                             REFERENCES ��������
go


ALTER TABLE ������
       ADD FOREIGN KEY (����������ID)
                             REFERENCES ��������������������
go


ALTER TABLE ������
       ADD FOREIGN KEY (���������ID)
                             REFERENCES ���������������
go


ALTER TABLE ������
       ADD FOREIGN KEY (������ID)
                             REFERENCES �������
go



