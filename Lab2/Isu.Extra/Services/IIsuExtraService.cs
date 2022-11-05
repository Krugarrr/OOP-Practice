using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    public MegaFacultyStudent AddStudent(Group group, string name);
    public MegaFacultyGroup AddGroup(GroupName groupName, Schedule schedule);
    public OgnpCourse AddOgnpCourse(OgnpName ognpName, MegaFacultyName megaFacultyName);
    public OgnpStream AddOgnpStream(OgnpName ognpName, StreamName name, Schedule schedule);
    public void AddStudentToOgnp(MegaFacultyStudent student, OgnpStream ognpStream);
    public void RemoveStudentOgnp(MegaFacultyStudent student, OgnpStream ognpStream);
    public OgnpCourse FindOgnpCourse(OgnpName name);
    public IReadOnlyList<OgnpStream> GetOgnpStreams(OgnpName ognpName);
    public OgnpStream FindStream(StreamName streamName);
    public IReadOnlyList<MegaFacultyStudent> GetStudentsByStream(StreamName streamName);
    public IReadOnlyCollection<MegaFacultyStudent> GetOgnpNonRegisteredStudents(GroupName groupName);
}