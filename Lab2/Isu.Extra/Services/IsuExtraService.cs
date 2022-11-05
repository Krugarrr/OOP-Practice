using System.Reflection.PortableExecutable;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private readonly List<MegaFacultyStudent> _students;
    private readonly List<OgnpCourse> _ognpCourses;
    private readonly List<MegaFacultyGroup> _groups;
    private readonly IIsuService _isuService;

    public IsuExtraService()
    {
        _isuService = new IsuService();
        _students = new List<MegaFacultyStudent>();
        _groups = new List<MegaFacultyGroup>();
        _ognpCourses = new List<OgnpCourse>();
    }

    public MegaFacultyGroup AddGroup(GroupName groupName, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        ArgumentNullException.ThrowIfNull(schedule);

        Group group = _isuService.AddGroup(groupName);
        var mgroup = new MegaFacultyGroup(group, schedule);
        _groups.Add(mgroup);
        return mgroup;
    }

    public MegaFacultyStudent AddStudent(Group group, string name)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(name);

        Student student = _isuService.AddStudent(group, name);
        var mstudent = new MegaFacultyStudent(student);
        _students.Add(mstudent);
        return mstudent;
    }

    public OgnpCourse AddOgnpCourse(OgnpName ognpName, MegaFacultyName megaFacultyName)
    {
        ArgumentNullException.ThrowIfNull(ognpName);
        ArgumentNullException.ThrowIfNull(megaFacultyName);

        var course = new OgnpCourse(ognpName, megaFacultyName);
        _ognpCourses.Add(course);
        return course;
    }

    public OgnpStream AddOgnpStream(OgnpName ognpName, StreamName name, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(ognpName);
        ArgumentNullException.ThrowIfNull(name);
        if (!OgnpCourseExist(ognpName))
            throw OgnpException.OGNPCourseDoesntExistException();

        OgnpCourse course = FindOgnpCourse(ognpName);
        return course.AddStream(name, schedule);
    }

    public void AddStudentToOgnp(MegaFacultyStudent student, OgnpStream ognpStream)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(ognpStream);

        if (!OgnpCourseExist(GetOgnpByStream(ognpStream).Name))
            throw OgnpException.OGNPCourseDoesntExistException();

        MegaFacultyGroup group = GetGroupByStudent(student);
        ognpStream.AddStudent(student, group.Schedule);
    }

    public void RemoveStudentOgnp(MegaFacultyStudent student, OgnpStream ognpStream)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(ognpStream);

        if (!OgnpCourseExist(GetOgnpByStream(ognpStream).Name))
            throw OgnpException.OGNPCourseDoesntExistException();

        if (!ognpStream.RemoveStudent(student))
            throw OgnpException.StudentRemoveException();
    }

    public void ChangeGroup(MegaFacultyStudent student, MegaFacultyGroup newGroup)
    {
        _isuService.ChangeStudentGroup(student.FacultyStudent, newGroup.FacultyGroup);
        foreach (var s in student.OgnpStreams)
        {
            if (s.Schedule.CompareClasses(newGroup.Schedule.ClassShedule))
            {
                OgnpCourse course = _ognpCourses.First(c => c.Streams.Contains(s));
                RemoveStudentOgnp(student, s);
                AddStudentToOgnp(student, course.AddStudent(student, newGroup.Schedule));
            }
        }
    }

    public OgnpCourse FindOgnpCourse(OgnpName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _ognpCourses.FirstOrDefault(c => c.Name.Equals(name));
    }

    public IReadOnlyList<OgnpStream> GetOgnpStreams(OgnpName ognpName)
    {
        ArgumentNullException.ThrowIfNull(ognpName);
        OgnpCourse course = FindOgnpCourse(ognpName);
        if (!OgnpCourseExist(ognpName))
        {
            throw OgnpException.OGNPCourseDoesntExistException();
        }

        if (!course.Streams.Any())
        {
            throw OgnpException.StreamDoesntExistException();
        }

        return course.Streams;
    }

    public OgnpStream FindStream(StreamName streamName)
    {
        ArgumentNullException.ThrowIfNull(streamName);
        OgnpCourse course = _ognpCourses.FirstOrDefault(c => c.Streams.FirstOrDefault(s => s.Name == streamName) is not null);
        return course.Streams.FirstOrDefault(s => s.Name == streamName);
    }

    public IReadOnlyList<MegaFacultyStudent> GetStudentsByStream(StreamName streamName)
    {
        ArgumentNullException.ThrowIfNull(streamName);
        OgnpStream stream = FindStream(streamName);
        if (stream is null)
            throw OgnpException.StreamDoesntExistException();
        return stream.Students;
    }

    public IReadOnlyCollection<MegaFacultyStudent> GetOgnpNonRegisteredStudents(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        return _students.Where(s => s.FacultyStudent.Group.Equals(groupName) & !s.OgnpStreams.Any()).ToList();
    }

    private MegaFacultyGroup GetGroup(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        Group group = _isuService.GetGroup(groupName);
        MegaFacultyGroup mgroup = _groups.FirstOrDefault(g => g.FacultyGroup == group)
                                  ?? throw OgnpException.CouldNotFindGroupException();
        return mgroup;
    }

    private MegaFacultyGroup GetGroupByStudent(MegaFacultyStudent student)
    {
        return _groups.First(g => student.FacultyStudent.Group.Equals(g.FacultyGroup.Name))
                                    ?? throw OgnpException.CouldNotGetGroupException();
    }

    private bool OgnpCourseExist(OgnpName name)
        => _ognpCourses.Any(c => c.Name.Equals(name));

    private OgnpCourse GetOgnpByStream(OgnpStream stream)
    {
        return _ognpCourses.FirstOrDefault(c => c.Streams.Contains(stream))
               ?? throw OgnpException.StreamDoesntExistException();
    }
}