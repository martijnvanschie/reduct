using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reduct.System.IO
{
    public static class StreamExtentions
    {
        /// <summary>
        /// Reads the specified lenght from the stream as a Int32.
        /// </summary>
        /// <param name="stream">The stream objec to read from.</param>
        /// <param name="lenght">The lenght to read from the stream.</param>
        /// <param name="offset">The offset position from where to start reading.</param>
        /// <returns>An bytes read converted to an Int32.</returns>
        public static int ReadInt32(this Stream stream, int lenght, int offset = 0)
        {
            byte[] buffer = ReadBytes(stream, lenght, offset);
            var value = BitConverter.ToInt32(buffer, 0);
            return value;
        }

        /// <summary>
        /// Reads the specified lenght from the stream as a String.
        /// </summary>
        /// <param name="stream">The stream objec to read from.</param>
        /// <param name="lenght">The lenght to read from the stream.</param>
        /// <param name="offset">The offset position from where to start reading.</param>
        /// <returns>An bytes read converted to an String.</returns>
        public static string ReadString(this Stream stream, int lenght, int offset = 0)
        {
            byte[] buffer = ReadBytes(stream, lenght, offset);
            var value = BitConverter.ToString(buffer, 0);
            return value;
        }

        /// <summary>
        /// Reads the specified lenght from the stream as an byte array..
        /// </summary>
        /// <param name="stream">The stream objec to read from.</param>
        /// <param name="lenght">The lenght to read from the stream.</param>
        /// <param name="offset">The offset position from where to start reading.</param>
        /// <returns>An bytes read converted to an byte array.</returns>
        public static byte[] ReadBytes(this Stream stream, int lenght, int offset = 0)
        {
            byte[] buffer = new byte[lenght];
            stream.Read(buffer, offset, lenght);
            return buffer;
        }
    }
}
