﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpVoxReader
{
    public static class Extensions
    {
        /// <summary>
        /// Read a color (ARGB) value from a <see cref="UInt32"/>
        /// </summary>
        /// <param name="value">Input value</param>
        /// <param name="a">Alpha value</param>
        /// <param name="r">Red value</param>
        /// <param name="g">Green value</param>
        /// <param name="b">Blue value</param>
        public static void ToARGB(this UInt32 value, out byte a, out byte r, out byte g, out byte b)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            a = bytes[3];
            r = bytes[2];
            g = bytes[1];
            b = bytes[0];
        }

        /// <summary>
        /// Try Get name from attributes Dictionary.
        /// </summary>
        /// <param name="attributes">Chunk attributes.</param>
        /// <param name="name">result name. If the returned value is false, null will be null. </param>
        /// <returns>Success or failure of name acquisition</returns>
        public static bool TryGetName(this Dictionary<string, byte[]> attributes, out string name)
        {
            if (attributes.TryGetValue("_name", out var bytes))
            {
                name = System.Text.Encoding.ASCII.GetString(bytes);
                return true;
            }

            name = null;
            return false;
        }
    }
}
