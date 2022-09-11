using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;
    private int _firstId = 100000;

    public IsuService()
    {
        _students = new List<Student>();
        _groups = new List<Group>();
    }

    public Group AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (GroupExist(name))
            throw new GroupExistException($"Group {name} already exist");

        Group group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(group);

        if (StudentExist(name, group.Name, _firstId))
            throw new StudentExistException("This student already exist");

        _firstId++;
        group = GetGroup(group.Name);

        Student student = new Student(_firstId, name, group.Name);
        _students.Add(student);
        group.AddStudent(student);

        return student;
    }

    public Student GetStudent(int id)
    {
        Student student = _students.FirstOrDefault(s => s.Id == id)
                          ?? throw new StudentExistException($"Could not find student by Id: {id}");
        return student;
    }

    public Group GetGroup(GroupName groupName)
    {
        Group group = _groups.FirstOrDefault(g => g.Name == groupName)
                ?? throw new GroupExistException($"Could not find group by name: {groupName}");
        return group;
    }

    public Student? FindStudent(int id)
        => _students.FirstOrDefault(s => s.Id == id);

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        if (!GroupExist(groupName))
            throw new GroupExistException($"Group {groupName} does not exist");

        Group group = _groups.First(gr => gr.Name == groupName);
        return group.Students;
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        ArgumentNullException.ThrowIfNull(courseNumber);
        return _students.Where(student => student.Group.Course == courseNumber).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        return _groups.FirstOrDefault(g => g.Name == groupName);
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        ArgumentNullException.ThrowIfNull(courseNumber);
        return _groups.Where(group => group.Course == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(newGroup);

        Group group = _groups.First(g => g.Name == student.Group);

        group.RemoveStudent(student.Id);
        newGroup.AddStudent(student);
    }

    private bool GroupExist(GroupName groupName)
        => _groups.Any(group => group.Name == groupName);

    private bool StudentExist(string name, GroupName groupName, int id)
        => _students.Any(student => student.Group == groupName
                                    && student.Name == name
                                    && student.Id == id);
}