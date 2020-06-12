using System;

namespace loader
{
	public enum Win32ErrorCode
	{
		ERROR_SUCCESS = 0,
		NO_ERROR = 0,
		ERROR_INVALID_FUNCTION = 1,
		ERROR_FILE_NOT_FOUND = 2,
		ERROR_PATH_NOT_FOUND = 3,
		ERROR_TOO_MANY_OPEN_FILES = 4,
		ERROR_ACCESS_DENIED = 5
	}
}