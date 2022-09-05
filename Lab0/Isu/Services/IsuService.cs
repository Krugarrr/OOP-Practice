using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;

namespace Isu.Service;

public class IsuService : IIsuService
{
    private List<Student> _students;
    private List<Group> _groups;
    private int firstId = 1_000_000;

    public IsuService()
    {
        _students = new List<Student>();
        _groups = new List<Group>();
    }

    public Group AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (GroupExist(name))
            throw new GroupExistException($"Group {name} is doesn't exist");

        Group group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(group);

        if (StudentExist(name, group.Name, firstId++))
            throw new StudentExistException();

        group = _groups.First(g => g.Name == group.Name)
                      ?? throw new GroupExistException($"Group {group.Name} is doesn't exist");

        Student student = new Student(firstId, name, group.Name, group.Name.Course);
        _students.Add(student);
        group.AddStudent(student);

        // group.Validate();
        return student;
    }

    public Student GetStudent(int id)
    {
        Student student = _students.First(s => s.Id == id);
        return student;
    }

    public Student? FindStudent(int id)
    {
        Student? student = _students.Find(s => s.Id == id);
        return student;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        if (GroupExist(groupName))
            throw new GroupExistException($"Group {groupName} is doesn't exist");

        Group group = _groups.First(gr => gr.Name == groupName);
        var students = new List<Student>();

        students.AddRange(group.Students);
        return students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var students = new List<Student>();
        students.AddRange(_students.FindAll(s => s.Course == courseNumber));
        return students;
    }

    public Group FindGroup(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);

        Group group = _groups.First(g => g.Name == groupName)
                      ?? throw new GroupExistException($"Group {groupName} is doesn't exist");
        return group;
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var group = new List<Group>();
        group.AddRange(_groups.FindAll(s => s.Course == courseNumber));
        return group;
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