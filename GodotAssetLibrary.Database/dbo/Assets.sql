-- Create the new table
CREATE TABLE dbo.as_assets (
    asset_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    user_id int NOT NULL DEFAULT 0,
    title varchar(255) NOT NULL,
    description text NOT NULL,  -- 'text' in SQL Server is used for large amounts of text, consider varchar(max) for a changeable length
    category_id int NOT NULL DEFAULT 6,
    godot_version int NOT NULL,
    version int NOT NULL,
    version_string varchar(20) NOT NULL,
    cost varchar(25) NOT NULL DEFAULT 'GPLv3',
    rating int NOT NULL DEFAULT 1,
    support_level tinyint NOT NULL,
    download_provider tinyint NOT NULL,
    download_commit varchar(2048) NOT NULL,
    browse_url varchar(1024) NOT NULL,
    issues_url varchar(1024) NOT NULL,
    icon_url varchar(1024) NOT NULL,
    searchable bit NOT NULL DEFAULT 0,  -- 'bit' is used for boolean values in SQL Server (equivalent to tinyint(1) in MySQL)
    modify_date datetime NOT NULL DEFAULT GETDATE(), -- GETDATE() is the SQL Server equivalent to CURRENT_TIMESTAMP
    CONSTRAINT PK_as_assets PRIMARY KEY CLUSTERED (asset_id)  -- Defining the primary key constraint
);
