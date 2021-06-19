select CAST(speed as int) as speed,Count(Id) as  count ,  sum(ChildrenCount) as ChildrenCount  from Animals
group by CAST(speed as int)



select a.Speed, p.Speed as PSpeed , (a.Speed - p.Speed ) as diff
from animals as a inner join animals as p on a.ParentId = p.id


select speed, count(id) from animals 
where isalive =1
group by speed
order by count(id)


select crea from animals where IsAlive = 1