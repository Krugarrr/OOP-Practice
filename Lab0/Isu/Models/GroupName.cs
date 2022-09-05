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

    public GroupName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (name.Length is < MinGroupNameLength or >= MaxGroupNameLength)
            throw new GroupNameLengthException($"{name.Length} is an invalid length for a group name");

        if (!char.IsUpper(name[0]))
        {
            throw new GroupNameLetterException($"Only capital letter is allowed in the group name. " +
                                               $"{name[0]} does not fit");
        }

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

    private int GetCourse(string name) => CharUnicodeInfo.GetDecimalDigitValue(name[2]);
}