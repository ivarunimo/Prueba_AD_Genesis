CREATE TABLE [User] (
  [id] int PRIMARY KEY,
  [username] varchar(50),
  [password] varchar(500),
  [email] varchar(150),
  [fullname] varchar(250)
)
GO

CREATE TABLE [Account] (
  [id] int PRIMARY KEY,
  [number] varchar(15),
  [balance] double,
  [account_name] varchar(50),
  [id_user] int
)
GO

CREATE TABLE [DebitCard] (
  [id] int PRIMARY KEY,
  [number] varchar(16),
  [active] boolean,
  [id_account] int,
  [id_user] int,
  [type] varchar(10),
  [expDate] varchar(5),
  [name_card] varchar(30)
)
GO

CREATE TABLE [Movement] (
  [id] int PRIMARY KEY,
  [origin] int,
  [id_user] int,
  [description] varchar(250),
  [type] int,
  [kind] int,
  [fecha] date,
  [amount] double
)
GO

CREATE TABLE [Transfer] (
  [id] int PRIMARY KEY,
  [origin] int,
  [destiny] int,
  [description] varchar(150),
  [amount] double
)
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([id_user]) REFERENCES [User] ([id])
GO

ALTER TABLE [DebitCard] ADD FOREIGN KEY ([id_account]) REFERENCES [Account] ([id])
GO

ALTER TABLE [DebitCard] ADD FOREIGN KEY ([id_user]) REFERENCES [User] ([id])
GO

ALTER TABLE [Movement] ADD FOREIGN KEY ([origin]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Movement] ADD FOREIGN KEY ([id_user]) REFERENCES [User] ([id])
GO

ALTER TABLE [Transfer] ADD FOREIGN KEY ([origin]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Transfer] ADD FOREIGN KEY ([destiny]) REFERENCES [Account] ([id])
GO
