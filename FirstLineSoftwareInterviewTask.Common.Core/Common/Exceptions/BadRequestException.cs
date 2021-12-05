namespace FirstLineSoftwareInterviewTask.Common.Core.Common.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        protected BadRequestException(string message)
            : base("Bad Request", message)
        {
        }
    }
}