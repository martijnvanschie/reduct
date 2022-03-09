namespace Reduct.System
{
    /// <summary>
    /// Reference:
    /// https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-
    /// </summary>
    public static class ErrorCodes
    {
        private static List<ErrorCodeDescription> _exitCodes = new List<ErrorCodeDescription>();

        public static ErrorCodeDescription GetDescription(ErrorCode exitCode)
        {
            try
            {
                return _exitCodes.First(d => d.ExitCode == exitCode);
            }
            catch (Exception ex)
            {
                return new ErrorCodeDescription(ErrorCode.ERROR_INVALID_FUNCTION, $"Error Code [{exitCode}] was not found. [{ex.Message}]");
            }
        }

        static ErrorCodes()
        {
            _exitCodes.Add(new ErrorCodeDescription(ErrorCode.SUCCES, "Succesfull"));
            _exitCodes.Add(new ErrorCodeDescription(ErrorCode.ERROR_ARGUMENTS_INVALID_AMOUNT, "Invalid amount of arguments provided"));
            _exitCodes.Add(new ErrorCodeDescription(ErrorCode.ERROR_ARGUMENT_BAD_INPUT, "Unable to process arguments. Bad arguments"));
            _exitCodes.Add(new ErrorCodeDescription(ErrorCode.ERROR_ARGUMENT_ASSEMBLY_NOT_FOUND, "Invalid argument. Input assembly not found"));
        }
    }
}