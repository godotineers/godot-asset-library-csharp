
-- Create the new table
CREATE TABLE dbo.as_users (
    user_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    username varchar(100) NOT NULL,
    email varchar(1024) NOT NULL,
    password_hash varchar(64) NOT NULL,
    [type] tinyint NOT NULL DEFAULT 0,
    session_token varbinary(24) NULL,
    reset_token varbinary(24) NULL,
    CONSTRAINT PK_as_users PRIMARY KEY CLUSTERED (user_id)
);
go
-- Create unique indexes
CREATE UNIQUE INDEX UX_username ON dbo.as_users (username);
go
CREATE UNIQUE INDEX UX_session_token ON dbo.as_users (session_token);
go
-- Add index for 'reset_token' column
CREATE INDEX IX_reset_token ON dbo.as_users (reset_token);
