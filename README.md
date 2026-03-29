# Database Migration
1. Be on the Infrastructure Project Location in Bash
2. Microsoft.EntityFrameworkCore.Design needs to be installed in start up project (API)
3. Run this: 
   - dotnet ef migrations add InitialCreate --project Infrastructure.csproj --startup-project ../MercatusAPI/MercatusAPI.csproj

# Database Update
1. Be on the Infrastructure Project Location in Bash
2. Run this:
    - dotnet ef database update --project Infrastructure.csproj --startup-project ../MercatusAPI/MercatusAPI.csproj