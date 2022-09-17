using System;
using System.ComponentModel;
using System.Globalization;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int MaxGroupNameLength = 7;
    private const int MinGroupNameLength = 5;
    private const int CourseNumberPosition = 2;
    private const int GroupLetterPosition = 0;
    public GroupName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        ValidateGroupName(name);

        Name = name;
        Course = new CourseNumber(GetCourse(Name));
    }

    public string Name { get; }
    public CourseNumber Course { get; }

    public bool Equals(GroupName? other)
        => other is not null
            && Name.Equals(other.Name)
            && Course.Equals(other.Course);

    public override bool Equals(object? obj)
    {
        if (obj is GroupName groupName)
        {
            return Equals(groupName);
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Name, Course);

    private int GetCourse(string name) => CharUnicodeInfo.GetDecimalDigitValue(name[CourseNumberPosition]);

    private void ValidateGroupName(string name)
    {
        if (name.Length is < MinGroupNameLength or >= MaxGroupNameLength)
            throw new GroupNameLengthException($"{name.Length} is an invalid length for a group name");

        if (!char.IsUpper(name[GroupLetterPosition]))
        {
            throw new GroupNameLetterException($"Only capital letter is allowed in the group name. " +
                                               $"{name[GroupLetterPosition]} does not fit");
        }
    }
}