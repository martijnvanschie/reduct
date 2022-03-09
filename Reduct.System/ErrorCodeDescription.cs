namespace Reduct.System
{
    public class ErrorCodeDescription
    {
        public ErrorCode ExitCode;
        public string Message;
        public int ExitCodeInt => (int)ExitCode;

        internal bool IsFormatter = false;

        public ErrorCodeDescription(ErrorCode exitCode, string message, bool isFormatter = false)
        {
            this.ExitCode = exitCode;
            this.Message = message;
            this.IsFormatter = isFormatter;
        }
    }
}