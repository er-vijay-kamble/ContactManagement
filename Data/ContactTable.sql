
CREATE TABLE [dbo].[Contact]
(
[ID]				INT CONSTRAINT [PK_ContactId]
	PRIMARY KEY ([ID])		
	IDENTITY (1, 1) NOT FOR REPLICATION			NOT NULL, 
[FirstName]         VARCHAR (50)    NOT NULL,
[LastName]          VARCHAR (50)    NOT NULL,
[Email]             VARCHAR (50)    NOT NULL,
[PhoneNumber]       VARCHAR (50)    NOT NULL,
[IsActive]          BIT             NULL CONSTRAINT [DF_Contact_IsActive] DEFAULT(0),
);
 
GO