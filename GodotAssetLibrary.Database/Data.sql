GO
-- Inserting data
INSERT INTO dbo.as_categories (category, category_type) VALUES
    ('2D Tools', 0),
    ('3D Tools', 0),
    ('Shaders', 0),
    ('Materials', 0),
    ('Tools', 0),
    ('Scripts', 0),
    ('Misc', 0),
    ('Templates', 1),
    ('Projects', 1),
    ('Demos', 1);

    GO

INSERT INTO dbo.as_licenses ([Tag], [Name]) VALUES
('MIT', 'MIT'),
('MPL-2.0', 'MPL-2.0'),
('GPLv3', 'GPL v3'),
('GPLv2', 'GPL v2'),
('LGPLv3', 'LGPL v3'),
('LGPLv2.1', 'LGPL v2.1'),
('LGPLv2', 'LGPL v2'),
('AGPLv3', 'AGPL v3'),
('Apache-2.0', 'Apache 2.0'),
('CC0', 'CC0 1.0 Universal'),
('CC-BY-4.0', 'CC BY 4.0 International'),
('CC-BY-3.0', 'CC BY 3.0 Unported'),
('CC-BY-SA-4.0', 'CC BY-SA 4.0 International'),
('CC-BY-SA-3.0', 'CC BY-SA 3.0 Unported'),
('BSD-2-Clause', 'BSD 2-clause License'),
('BSD-3-Clause', 'BSD 3-clause License'),
('BSL-1.0', 'Boost Software License'),
('Unlicense', 'The Unlicense License');

GO


-- Insert data into the as_versions table
INSERT INTO dbo.as_versions (Tag) VALUES
    ('2.0'),
    ('2.1'),
    ('2.2'),
    ('3.0'),
    ('3.1'),
    ('3.2'),
    ('3.3'),
    ('3.4'),
    ('3.5'),
    ('4.0'),
    ('4.1'),
    ('4.2'),
    ('unknown'),
    ('custom_build');
