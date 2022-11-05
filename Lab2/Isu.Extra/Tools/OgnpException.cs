using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class OgnpException : IsuExtraExceptions
{
    public OgnpException(string message)
        : base(message) { }

    public static OgnpException StudentRemoveException()
    {
        throw new OgnpException("Could not remove student from OGNP");
    }

    public static OgnpException StreamDoesntExistException()
    {
        throw new OgnpException("This stream does not exist");
    }

    public static OgnpException OGNPCourseDoesntExistException()
    {
        throw new OgnpException("This OGNP course does not exist");
    }

    public static OgnpException OGNPLimitException()
    {
        throw new OgnpException("You've reached ognp max or min limit");
    }

    public static OgnpException CouldNotGetGroupException()
    {
        throw new OgnpException("Could not get this group");
    }

    public static OgnpException CouldNotFindGroupException()
    {
        throw new OgnpException("Could not find this group");
    }
}