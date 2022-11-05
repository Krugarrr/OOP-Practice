using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Extra.Services.AddressBuilder;
using Isu.Extra.Services.UniversityClassBuilder;
using Isu.Extra.Tools;
using Isu.Models;
using Xunit;
namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private IIsuExtraService _isuExtraService = new IsuExtraService();
    private IAddressBuilder _addressBuilder = new AddressBuilder();
    private IUniversityClassBuilder _universityClassBuilder = new UniversityClassBuilder();
    private ClassAddress address;
    private ClassAddress anotherAddress;
    private UniversityClass lesson;
    private UniversityClass anotherLesson;
    private UniversityClass exceptionLesson;
    private Schedule schedule;
    private Schedule anotherSchedule;
    private Schedule exceptionSchedule;

    public IsuExtraTest()
    {
        address = _addressBuilder
            .WithStreet("Кронва")
            .WithHousing(49)
            .WithClassroom(147)
            .Build();

        anotherAddress = _addressBuilder
            .WithStreet("Пупкино-Зааааречино")
            .WithHousing(228)
            .WithClassroom(228322)
            .Build();

        lesson = _universityClassBuilder
            .WithAddress(address)
            .WithName("Оопешечка")
            .WithTeacher(new Teacher("Макаронич"))
            .WithTime(new ClassTime(DayOfWeek.Saturday, new TimeOnly(17, 20)))
            .Build();

        anotherLesson = _universityClassBuilder
            .WithAddress(anotherAddress)
            .WithName("Безполезнич")
            .WithTeacher(new Teacher("Смешной дедус"))
            .WithTime(new ClassTime(DayOfWeek.Saturday, new TimeOnly(16, 00)))
            .Build();

        exceptionLesson = _universityClassBuilder
            .WithAddress(anotherAddress)
            .WithName("Ещё один безполезнич")
            .WithTeacher(new Teacher("Ещё один дедус"))
            .WithTime(new ClassTime(DayOfWeek.Saturday, new TimeOnly(16, 00)))
            .Build();
        schedule = Schedule.Builder.AddClass(lesson).Build();
        anotherSchedule = Schedule.Builder.AddClass(anotherLesson).Build();
        exceptionSchedule = Schedule.Builder.AddClass(exceptionLesson).Build();
    }

    [Fact]
    public void AddNewOgnpCourse_CourseCanBeFound()
    {
        var ognpName = new OgnpName("Науки о жизни");
        var megaFacultyName = new MegaFacultyName("Факультет биотехнологий и низкотемпературных систем");
        OgnpCourse course = _isuExtraService.AddOgnpCourse(ognpName, megaFacultyName);
        OgnpCourse fcourse = _isuExtraService.FindOgnpCourse(ognpName);

        Assert.Equal(course.Name, ognpName);
        Assert.Equal(fcourse, course);
        Assert.Equal(fcourse.Name, ognpName);
    }

    [Fact]
    public void AddStudentToOgnpCourse_SchedulesCouldNotIntersect()
    {
        var ognpName = new OgnpName("Науки об истинном кринже");
        var anotherOgnpName = new OgnpName("Науки о маленьком кринже");
        var exceptionOgnpName = new OgnpName("Науки о кошках");
        var megaFacultyName = new MegaFacultyName("Мегафакультет кринжа");

        MegaFacultyGroup megaFacultyGroup = _isuExtraService.AddGroup(new GroupName("M32081"), anotherSchedule);
        MegaFacultyStudent student = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Санёчек Терентьев");

        OgnpCourse course = _isuExtraService.AddOgnpCourse(ognpName, megaFacultyName);
        OgnpStream stream = _isuExtraService.AddOgnpStream(ognpName, new StreamName("КРИНЖ-1/2"), schedule);

        OgnpCourse anotherCourse = _isuExtraService.AddOgnpCourse(anotherOgnpName, megaFacultyName);
        OgnpStream anotherStream = _isuExtraService.AddOgnpStream(anotherOgnpName, new StreamName("КРИНЖ-3/4"), anotherSchedule);

        OgnpCourse exceptionCourse = _isuExtraService.AddOgnpCourse(exceptionOgnpName, megaFacultyName);
        OgnpStream exceptionStream = _isuExtraService.AddOgnpStream(exceptionOgnpName, new StreamName("КРИНЖ-2/3"), exceptionSchedule);

        _isuExtraService.AddStudentToOgnp(student, stream);

        Assert.Equal(student.OgnpStreams.First(), stream);
        Assert.Throws<ScheduleException>(() => _isuExtraService.AddStudentToOgnp(student, exceptionStream));
    }

    [Fact]
    public void RemoveStudentFromOgnp_OgnpsCouldNotBeLessThanZero()
    {
        var schedule = Schedule.Builder.AddClass(lesson).Build();
        var anotherSchedule = Schedule.Builder.AddClass(anotherLesson).Build();

        var ognpName = new OgnpName("Науки об истинном кринже");
        var megaFacultyName = new MegaFacultyName("Мегафакультет кринжа");

        MegaFacultyGroup megaFacultyGroup = _isuExtraService.AddGroup(new GroupName("M32081"), anotherSchedule);
        MegaFacultyStudent student = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Санёчек Терентьев");

        OgnpCourse course = _isuExtraService.AddOgnpCourse(ognpName, megaFacultyName);
        OgnpStream stream = _isuExtraService.AddOgnpStream(ognpName, new StreamName("КРИНЖ-1/2"), schedule);

        _isuExtraService.AddStudentToOgnp(student, stream);
        _isuExtraService.RemoveStudentOgnp(student, stream);

        Assert.Empty(student.OgnpStreams);
        Assert.Throws<OgnpException>(() => _isuExtraService.RemoveStudentOgnp(student, stream));
    }

    [Fact]
    public void GetStreamsByCourse_ThrowIfEmpty()
    {
        var ognpName = new OgnpName("Науки об истинном кринже");
        var anotherOgnpName = new OgnpName("Науки об маленьком кринже");
        var megaFacultyName = new MegaFacultyName("Мегафакультет кринжа");

        MegaFacultyGroup megaFacultyGroup = _isuExtraService.AddGroup(new GroupName("M32081"), anotherSchedule);
        MegaFacultyStudent student = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Санёчек Терентьев");

        OgnpCourse course = _isuExtraService.AddOgnpCourse(ognpName, megaFacultyName);
        OgnpCourse anotherCourse = _isuExtraService.AddOgnpCourse(anotherOgnpName, megaFacultyName);
        OgnpStream stream = _isuExtraService.AddOgnpStream(ognpName, new StreamName("КРИНЖ-1/2"), schedule);
        OgnpStream anotherStream = _isuExtraService.AddOgnpStream(ognpName, new StreamName("КРИНЖ-3/4"), anotherSchedule);
        var streams = _isuExtraService.GetOgnpStreams(ognpName);

        Assert.Equal(streams.First(), stream);
        Assert.Equal(streams.Last(), anotherStream);
        Assert.Throws<OgnpException>(() => _isuExtraService.GetOgnpStreams(anotherOgnpName));
    }

    [Fact]
    public void GetStudentsByStream()
    {
        var ognpName = new OgnpName("Науки об истинном кринже");
        var anotherOgnpName = new OgnpName("Науки об маленьком кринже");
        var megaFacultyName = new MegaFacultyName("Мегафакультет кринжа");
        var streamName = new StreamName("КРИНЖ-1/2");
        var anotherStreamName = new StreamName("КРИНЖ-3/4");

        MegaFacultyGroup megaFacultyGroup = _isuExtraService.AddGroup(new GroupName("M32081"), anotherSchedule);
        MegaFacultyStudent student = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Санёчек Терентьев");
        MegaFacultyStudent bestStudent = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Зариночка Пипуля");
        MegaFacultyStudent anotherStudent = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Андрей Ослов");

        OgnpCourse course = _isuExtraService.AddOgnpCourse(ognpName, megaFacultyName);
        OgnpCourse anotherCourse = _isuExtraService.AddOgnpCourse(anotherOgnpName, megaFacultyName);

        OgnpStream stream = _isuExtraService.AddOgnpStream(ognpName, streamName, schedule);
        OgnpStream anotherStream = _isuExtraService.AddOgnpStream(anotherOgnpName, anotherStreamName, schedule);
        _isuExtraService.AddStudentToOgnp(student, stream);
        _isuExtraService.AddStudentToOgnp(bestStudent, stream);
        _isuExtraService.AddStudentToOgnp(anotherStudent, anotherStream);

        var students = _isuExtraService.GetStudentsByStream(streamName);
        var anotherStudents = _isuExtraService.GetStudentsByStream(anotherStreamName);

        Assert.Contains(student, students);
        Assert.Contains(bestStudent, students);
        Assert.Contains(anotherStudent, anotherStudents);
    }

    [Fact]
    public void GetNonRegisteredToOgnpStudents()
    {
        var ognpName = new OgnpName("Науки об истинном кринже");
        var anotherOgnpName = new OgnpName("Науки об маленьком кринже");
        var megaFacultyName = new MegaFacultyName("Мегафакультет кринжа");
        var streamName = new StreamName("КРИНЖ-1/2");
        var anotherStreamName = new StreamName("КРИНЖ-3/4");

        MegaFacultyGroup megaFacultyGroup = _isuExtraService.AddGroup(new GroupName("M32081"), schedule);
        MegaFacultyStudent student = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Санёчек Терентьев");
        MegaFacultyStudent bestStudent = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Зариночка Пипуля");
        MegaFacultyStudent anotherStudent = _isuExtraService.AddStudent(megaFacultyGroup.FacultyGroup, "Андрей Ослов");

        var students = _isuExtraService.GetOgnpNonRegisteredStudents(new GroupName("M32081"));

        Assert.Contains(student, students);
        Assert.Contains(bestStudent, students);
        Assert.Contains(anotherStudent, students);
    }
}