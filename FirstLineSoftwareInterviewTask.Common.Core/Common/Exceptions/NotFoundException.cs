namespace FirstLineSoftwareInterviewTask.Common.Core.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}