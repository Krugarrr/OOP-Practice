using Isu.Entities;
using Isu.Models;
using Isu.Service;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuTest
{
    private IsuService _isuService = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _isuService.AddGroup(new GroupName("M3112"));
        Group group1 = _isuService.AddGroup(new GroupName("M3113"));
        Student student = _isuService.AddStudent(group, "Пенис Детров");

        Assert.Contains(student, group.Students);
        Assert.Equal(student.Group, group.Name);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        const int limit = 26;
        Group group = _isuService.AddGroup(new GroupName("M3103"));

        for (int i = 0; i <= limit; i++)
        {
            _isuService.AddStudent(group, $"Елизавета Петрова{i}");
        }
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Group group = _isuService.AddGroup(new GroupName("M313414414"));
        Group group1 = _isuService.AddGroup(new GroupName("f3112"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group group = _isuService.AddGroup(new GroupName("M3103"));
        Group newGroup = _isuService.AddGroup(new GroupName("M3111"));
        Student student = _isuService.AddStudent(group, "Елизавета Петрова");

        _isuService.ChangeStudentGroup(student, newGroup);
        Assert.Equal(student.Group, newGroup.Name);
    }
}