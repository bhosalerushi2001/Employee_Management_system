using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Interface
{
    public interface EmployeeInterface
    {
        Task<ModelEmployeeAdditionalDetails> AddAdditinalDetails(ModelEmployeeAdditionalDetails details);
        Task<ModelEmployeeBasicDetails> AddEmployee(ModelEmployeeBasicDetails details);
        Task<ModelEmployeeAdditionalDetails> AdditinalDetailsGetById(string iD);
        Task<string> DeleteAdditaionalDetails(string ID);
        Task<string> DeleteEmployee(string id);
        Task<List<ModelEmployeeBasicDetails>> getAllBasicDetails();
        Task<List<ModelEmployeeAdditionalDetails>> GetAllEmployees();
        Task<List<ModelEmployeeBasicDetails>> GetAllEmployeeBynickName(string nickName);
        Task<ModelEmployeeBasicDetails> GetEmployeeById(string iD);
        Task<ModelEmployeeAdditionalDetails> UpdateAdditinalDetails(ModelEmployeeAdditionalDetails details);
        Task<ModelEmployeeBasicDetails> UpdateEmployee(ModelEmployeeBasicDetails employee);
        Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria);
    }
}
