using AutoMapper;
using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.CosmosDb;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.ModelDto;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : EmployeeInterface
    {
        public readonly InterfaceCosmosDb _CosmosDb;
        public readonly IMapper _mapper;

        public EmployeeService(InterfaceCosmosDb cosmosDb,IMapper mapper) 
        {
            _CosmosDb = cosmosDb;
            _mapper = mapper;

        }

        public async Task<ModelEmployeeAdditionalDetails> AddAdditinalDetails(ModelEmployeeAdditionalDetails details)
        {
            var data=_mapper.Map<EmployeeAdditionalDetails>(details);
            data.Intialize(true, Credentials.EmployeeDocumentType, "Rushi", "rushi");

            var save = await _CosmosDb.AddAdditinalDetails(data);
            var model=_mapper.Map<ModelEmployeeAdditionalDetails>(save);
            return model;
        }

        public async Task<ModelEmployeeAdditionalDetails> AdditinalDetailsGetById(string iD)
        {
            var details= await _CosmosDb.AdditinalDetailsGetById(iD);
            var data = _mapper.Map<ModelEmployeeAdditionalDetails>(details);
            return data;

        }


        public async Task<ModelEmployeeAdditionalDetails> UpdateAdditinalDetails(ModelEmployeeAdditionalDetails details)
        {
            var data = await _CosmosDb.AdditinalDetailsGetById(details.Id);

            data.Active = false;
            data.Archived = true;

            await _CosmosDb.ReplaceDelAsync(data);
            data.Intialize(false, Credentials.EmployeeDocumentType, "rushi", "rushi");
            data.Active = false;

             var dt=_mapper.Map<EmployeeAdditionalDetails>(data);
            var save = await _CosmosDb.AddAdditinalDetails(dt);
            var model = _mapper.Map<ModelEmployeeAdditionalDetails>(save);
            return model;
        }

        public async Task<string> DeleteAdditaionalDetails(string ID)
        {
            var data = await _CosmosDb.AdditinalDetailsGetById(ID);

            data.Active = false;
            data.Archived= true;

            await _CosmosDb.ReplaceDelAsync(data);
            data.Intialize(false, Credentials.EmployeeDocumentType, "Rushi", "rushi");
            data.Active = false;


            await _CosmosDb.AddAdditinalDetails(data);
            return "Delete Additinal details Succesfulllll......";

        }

        public async Task<List<ModelEmployeeAdditionalDetails>> GetAllEmployees()
        {
            var data = await _CosmosDb.GetAllEmployees();
            
            var empList=new List<ModelEmployeeAdditionalDetails>();

            foreach(var item in data) 
            {
                var emp = _mapper.Map<ModelEmployeeAdditionalDetails>(item);
                
                empList.Add(emp);
            
            }
            return empList;

        }




        //basic Details
        public async Task<ModelEmployeeBasicDetails> AddEmployee(ModelEmployeeBasicDetails details)
        {
            var emp = _mapper.Map<EmployeeBasicDetails>(details);
            emp.Intialize(true, Credentials.EmployeeDocumentType, "rushi", "Rushi");
            var respnse = await _CosmosDb.AddEmployee(emp);
            var model=_mapper.Map<ModelEmployeeBasicDetails>(respnse);
            return model;
        }

       

        public async Task<string> DeleteEmployee(string id)
        {
           var emp= await _CosmosDb.GetEmployeeById(id);

            emp.Active = false;
            emp.Archived = true;

            await _CosmosDb.ReplaceDelAsync(emp);
            emp.Intialize(false, Credentials.EmployeeDocumentType, "rushi", "rushi");
            emp.Active = false;

            await _CosmosDb.AddEmployee(emp);
            return "delete succesfullll.......";
        }

        public async Task<ModelEmployeeBasicDetails> GetEmployeeById(string iD)
        {
            var emp= await _CosmosDb.GetEmployeeById(iD);
            var model = _mapper.Map<ModelEmployeeBasicDetails>(emp);
            return model;
        }

        public async Task<ModelEmployeeBasicDetails> UpdateEmployee(ModelEmployeeBasicDetails employee)
        {
            var emp= await _CosmosDb.GetEmployeeById(employee.Id);

            emp.Active = false;
            emp.Archived = true;

            await _CosmosDb.ReplaceDelAsync(employee);
            emp.Intialize(false, Credentials.EmployeeDocumentType, "rushi", "rushi");
            emp.Active = false;

            var data=_mapper.Map<EmployeeBasicDetails>(emp);
            var response = await _CosmosDb.AddEmployee(data);
            var model =_mapper.Map<ModelEmployeeBasicDetails>(response);

            return model;
        }

        public async Task<List<ModelEmployeeBasicDetails>> getAllBasicDetails()
        {
            var data = await _CosmosDb.getAllBasicDetails();

            var listDetails= new List<ModelEmployeeBasicDetails>();

            foreach(var item in data)
            {
                var model = _mapper.Map<ModelEmployeeBasicDetails>(item);
                listDetails.Add(model);

            }
            return listDetails;
        }

        public async Task<List<ModelEmployeeBasicDetails>> GetAllEmployeeBynickName(string nickName)
        {
            var allEmployees= await getAllBasicDetails();

            return allEmployees.FindAll(a => a.NickName == nickName);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria responseObject = new EmployeeFilterCriteria();

            //filter ==>nickname
            var checkFilter = employeeFilterCriteria.Filters.Any(a => a.FieldName == "nickName");
            
            //var nickname
            if(checkFilter)
            {
                var nickName = employeeFilterCriteria.Filters.Find(a => a.FieldName == "nickName");
            }

            var employess=await getAllBasicDetails();
            responseObject.TotalCount = employess.Count;
            responseObject.Page=employeeFilterCriteria.Page;
            responseObject.PageSize=employeeFilterCriteria.PageSize;

            var skip=employeeFilterCriteria.PageSize*(employeeFilterCriteria.Page- 1);

            employess=employess.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();
           foreach(var item in employess)
            {
                responseObject.employees.Add(item);
            }
            return responseObject;
        }
    }
}
