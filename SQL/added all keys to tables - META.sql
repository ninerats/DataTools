
select 
'if not exists (select * from sys.key_constraints where name = ''' + c.name + ''') ALTER TABLE ' + s.name + '.' + t.name + ' ADD  CONSTRAINT [' + c.name + '] PRIMARY KEY CLUSTERED (	' + 
(STUFF((SELECT CAST(', [' + KU.COLUMN_NAME + ']' AS VARCHAR(MAX)) 
         FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KU
         WHERE (KU.CONSTRAINT_NAME = c.name) 
         FOR XML PATH ('')), 1, 2, ''))  + ')'
from sys.key_constraints c
inner join sys.tables t on c.parent_object_id  = t.object_id
inner join sys.schemas s on t.schema_id = s.schema_id
where c.type = 'PK'

