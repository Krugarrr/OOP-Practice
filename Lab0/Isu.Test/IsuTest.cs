using Isu.Entities;
using Isu.Models;
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
        Group group = _isuService.AddGroup(new GroupName("M3103"));

        for (int i = 0; i < group.MaxGroupCapacity; i++)
        {
            _isuService.AddStudent(group, $"Елизавета Петрова{i}");
        }

        Assert.Throws<StudentsCountException>(() => _isuService.AddStudent(group, "Александр Терентьев"));
    }

    [Theory]
    [InlineData("M148888")]
    [InlineData("M228322")]
    public void CreateGroupWithInvalidName_ThrowLengthException(string groupName)
    {
        Assert.Throws<GroupNameLengthException>(() => _isuService.AddGroup(new GroupName(groupName)));
    }

    [Theory]
    [InlineData("f3198")]
    [InlineData("adsada")]
    public void CreateGroupWithInvalidName_ThrowLetterException(string groupName)
    {
        Assert.Throws<GroupNameLetterException>(() => _isuService.AddGroup(new GroupName(groupName)));
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