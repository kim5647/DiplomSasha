using Core.Core.ModelsSasha;
using SQLServer.DatabaseContext;
using SQLServer.Repository.RepositorySasha;
using System.Collections.Generic;

namespace HablonProject.ServicesSasha.BaseServise;

public abstract class BaseServices
{
    private readonly DBContext _dbContext;
    public readonly EmployeeRepository _employeeRepository;
    public readonly PositionRepository _positionRepository;
    public readonly DepartmentRepository _departmentRepository;
    public readonly ProjectAssignmentRepository _projectAssignmentRepository;
    public readonly RoleRepository _roleRepository;
    public readonly ProjectRepository _projectRepository;
    public readonly StatusRepository _statusRepository;
    public readonly TasksRepository _tasksRepository;
    public readonly TaskAssignmentRepository _taskAssignmentRepository;
    public readonly UsersRepository _usersRepository;
    public BaseServices()
    {
        _dbContext = new DBContext();
        _employeeRepository = new EmployeeRepository(_dbContext);
        _positionRepository = new PositionRepository(_dbContext);
        _departmentRepository = new DepartmentRepository(_dbContext);
        _projectAssignmentRepository = new ProjectAssignmentRepository(_dbContext);
        _roleRepository = new RoleRepository(_dbContext);
        _projectRepository = new ProjectRepository(_dbContext);
        _statusRepository = new StatusRepository(_dbContext);
        _tasksRepository = new TasksRepository(_dbContext);
        _taskAssignmentRepository = new TaskAssignmentRepository(_dbContext);
        _usersRepository = new UsersRepository(_dbContext);
    }
    public Employee GetEmployee(Users users) => _employeeRepository.GetEmployeeByUserID(users);
    public List<Employee> GetEmployee() => _employeeRepository.GetAllEmployees();
    public Position GetPostion(Employee employee) => _positionRepository.GetPositionById(employee);
    public Department GetDepartment(Position position) => _departmentRepository.GetDepartmentById(position);
    public List<ProjectAssignment> GetProjectAssignment(Employee employee) => _projectAssignmentRepository.GetProjectAssignmentByEmployeeId(employee);
    public Role GetRole(ProjectAssignment projectAssignment) => _roleRepository.GetRoleById(projectAssignment);
    public List<Role> GetRole() => _roleRepository.GetAllRoles();
    public List<Project> GetProject(List<ProjectAssignment> projectAssignment) => _projectRepository.GetProjectById(projectAssignment);
    public Project GetProject(Project project) => _projectRepository.GetProjectByOne(project);
    public List<Statuse> GetStatuse(List<Project> project) => _statusRepository.GetStatusById(project);
    public List<Statuse> GetStatuse(List<Tasks> tasks) => _statusRepository.GetStatusById(tasks);
    public List<Statuse> GetStatuse() => _statusRepository.GetAllStatuses();
    public List<Tasks> GetTasks(List<TaskAssignment> tasks) => _tasksRepository.GetTaskById(tasks);
    public Tasks GetTasks(Project project) => _tasksRepository.GetTaskById(project);
    public Tasks GetTasks(Statuse statuse) => _tasksRepository.GetTaskById(statuse);
    public TaskAssignment GetTaskAssignment(Tasks tasks) => _taskAssignmentRepository.GetTaskAssignmentById(tasks);
    public List<TaskAssignment> GetTaskAssignment(Employee employee) => _taskAssignmentRepository.GetTaskAssignmentById(employee);
    public TaskAssignment GetTaskAssignment(TaskAssignment taskAssignment) => _taskAssignmentRepository.GetTaskAssignmentById(taskAssignment);
    public Users GetUsers(Employee employee) => _usersRepository.GetUserById(employee);
    public Users GetUsers(Users users) => _usersRepository.GetUserById(users);
}

