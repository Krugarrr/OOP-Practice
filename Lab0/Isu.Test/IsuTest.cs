using Isu.Entities;
using Isu.Models;
using Isu.Service;
using Isu.Services;
using Isu.Tools;
using Xunit;

namespace Isu.Test;

public class IsuTest
{
    private IsuService _isuService = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _isuService.AddGroup(new GroupName("M3112"));
        Student student = _isuService.AddStudent(group, "Денис Петров");

        Assert.Contains(student, group.Students);
        Assert.Equal(student.Group, group.Name);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        const int limit = 25;
        Group group = _isuService.AddGroup(new GroupName("M3103"));

        for (int i = 0; i < limit; i++)
        {
            _isuService.AddStudent(group, $"Елизавета Петрова{i}");
        }

        Assert.Throws<StudentsCountException>(() => _isuService.AddStudent(group, "Эмин Керимов"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameLengthException>(() => _isuService.AddGroup(new GroupName("M3123124")));
        Assert.Throws<GroupNameLetterException>(() => _isuService.AddGroup(new GroupName("f1231")));
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