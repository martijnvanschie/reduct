namespace Reduct.System
{
    public enum ErrorCode : int
    {
        // 0 Success
        SUCCES = 0,
        ERROR_INVALID_FUNCTION = 1,
        // 1 - 99 Input Errors

        ERROR_ARGUMENTS_INVALID_AMOUNT = 100,
        ERROR_ARGUMENT_BAD_INPUT = 101,
        ERROR_ARGUMENT_ASSEMBLY_NOT_FOUND = 102

        // 100 - 199 Process Errors

        // 200-299 Internal Errors
    }
}