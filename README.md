# ClickSharp

## EF Basic Startup Migrations
dotnet ef migrations add FirstInit --startup-project ClickSharp --project ClickSharp.DataLayer -- --environment DEBUG </br>
<br />
```bash
dotnet ef database update --startup-project ClickSharp --project ClickSharp.DataLayer -- --environment DEBUG
```

Steps on Azure:  
1. googleit->"azure for students"
2. create free account on *@student.put.poznan.pl domain
3. you should have azure for student subscription
4. create resources group
5. create sql server
6. create sql db
7. create app service
8. easy deploy from git repository