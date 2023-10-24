
-- Create the new table
CREATE TABLE dbo.as_asset_edits (
    edit_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    asset_id int NOT NULL,
    user_id int NOT NULL,
    title varchar(255) NULL, -- DEFAULT is not included since NULL is the default
    description text,  -- 'text' in SQL Server is used for large amounts of text, consider varchar(max) for a changeable length
    category_id int NULL, -- DEFAULT is not included since NULL is the default
    godot_version int NOT NULL,
    version_string varchar(20) NULL, -- DEFAULT is not included since NULL is the default
    cost varchar(25) NULL, -- DEFAULT is not included since NULL is the default
    download_provider tinyint NULL, -- DEFAULT is not included since NULL is the default
    download_commit varchar(2048) NULL, -- DEFAULT is not included since NULL is the default
    browse_url varchar(1024) NULL, -- DEFAULT is not included since NULL is the default
    issues_url varchar(1024) NULL, -- DEFAULT is not included since NULL is the default
    icon_url varchar(1024) NULL, -- DEFAULT is not included since NULL is the default
    status tinyint NOT NULL DEFAULT 0,
    reason text NOT NULL,
    submit_date datetime NOT NULL DEFAULT GETDATE(), -- GETDATE() is the SQL Server equivalent to CURRENT_TIMESTAMP
    modify_date datetime NOT NULL DEFAULT GETDATE(), -- GETDATE() is the SQL Server equivalent to CURRENT_TIMESTAMP
    CONSTRAINT PK_as_asset_edits PRIMARY KEY CLUSTERED (edit_id)  -- Defining the primary key constraint
);
GO
-- Create indices
CREATE INDEX IX_asset_id ON dbo.as_asset_edits (asset_id);
GO

CREATE INDEX IX_status ON dbo.as_asset_edits (status);
