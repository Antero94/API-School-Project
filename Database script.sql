create database ga_emne7_studentblogg;

create user if not exists "ga-app"@"localhost" identified by "ga_5ecret-%";
create user if not exists "ga-app"@"%" identified by "ga-5ecret-%";

grant all privileges on ga_emne7_studentblogg.* to "ga-app"@"%";
grant all privileges on ga_emne7_studentblogg.* to "ga-app"@"localhost";

flush privileges;

select * from users;