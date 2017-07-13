-- Clear cached plans
dbcc freeproccache;
go

-- View cached plans
select usecounts, cacheobjtype, objtype, text, query_plan, cp.bucketid
from sys.dm_exec_cached_plans cp
cross apply sys.dm_exec_sql_text(plan_handle) sqlT
cross apply sys.dm_exec_query_plan(plan_handle) qp
where text like '%CityName%'
and text not like '%dm_exec_cached_plans%';

-- Execute our dynamic proc
exec dbo.SearchCitiesWithState @cityName = 'Atlanta';

-- View cached plans
select usecounts, cacheobjtype, objtype, text, query_plan, cp.bucketid
from sys.dm_exec_cached_plans cp
cross apply sys.dm_exec_sql_text(plan_handle) sqlT
cross apply sys.dm_exec_query_plan(plan_handle) qp
where text like '%CityName%'
and text not like '%dm_exec_cached_plans%';

-- Execute our dynamic proc with a different value
exec dbo.SearchCitiesWithState @cityName = 'Chicago';

-- View cached plans
select usecounts, cacheobjtype, objtype, text, query_plan, cp.bucketid
from sys.dm_exec_cached_plans cp
cross apply sys.dm_exec_sql_text(plan_handle) sqlT
cross apply sys.dm_exec_query_plan(plan_handle) qp
where text like '%CityName%'
and text not like '%dm_exec_cached_plans%';

-- Execute our dynamic proc with a different value
exec dbo.SearchCitiesWithState @cityName = 'Atlanta', @stateName = 'Georgia';

-- View cached plans
select usecounts, cacheobjtype, objtype, text, query_plan, cp.bucketid
from sys.dm_exec_cached_plans cp
cross apply sys.dm_exec_sql_text(plan_handle) sqlT
cross apply sys.dm_exec_query_plan(plan_handle) qp
where text like '%CityName%'
and text not like '%dm_exec_cached_plans%';