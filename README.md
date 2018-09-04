# CoreAngular-TestProject-NTier
Test project using ASP.NET Core 2.1 and Angular 6. Classic n-tier architecture with anemic domain model.

**Live Demo** can be viewed here: http://tarastestvm1.westeurope.cloudapp.azure.com:8080 (link is temporary)

### Building and running solution
Visual Studio 2017 is required to build this solution. You might need to do all or some of the following:
  - install .net core 2.1 (https://www.microsoft.com/net/download/dotnet-core/2.1)
  - install latest updates to Visual Studio (https://docs.microsoft.com/en-us/visualstudio/install/update-visual-studio?view=vs-2017)
  - install **node.js development tools** component in Visual Studio
  - install node.js (https://nodejs.org/en/download/)
  - install Angular CLI (execute **npm install -g @angular/cli** command in cmd)
  - create local database server (execute **sqllocaldb create** command in cmd)
  
### Notes on architecture
**Repository** and **Unit of Work** patterns in DAL layer are not implemented, considering that Entity Framework DBContext implements both. This additional layer of abstraction can be added if needed

**/api/products** server endpoint implements OData for filtering, sorting, paging etc. (https://www.odata.org/odata-services/). Some examples of its usage are in integration tests.
