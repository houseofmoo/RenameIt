using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Common.Writers
{
    /// <summary>
    /// Contains the <see cref="Write{T}(string, T)"/> 
    /// method to write a classes public properties to a binary file as bytes
    /// </summary>
    public static class PropertiesToBytesWriter
    {
        /// <summary>
        /// Holds the appropriate action based on the properties type
        /// </summary>
        private static Dictionary<Type, Action<BinaryWriter, object>> _actions = new Dictionary<Type, Action<BinaryWriter, object>>()
        {
            // primitives
            { typeof(char), (w, v) => w.Write((char)v) },
            { typeof(bool), (w, v) => w.Write((bool)v) },
            { typeof(sbyte), (w, v) => w.Write((sbyte)v) },
            { typeof(byte), (w, v) => w.Write((byte)v) },
            { typeof(short), (w, v) => w.Write((short)v) },
            { typeof(ushort), (w, v) => w.Write((ushort)v) },
            { typeof(int), (w, v) => w.Write((int)v) },
            { typeof(uint), (w, v) => w.Write((uint)v) },
            { typeof(Int64), (w, v) => w.Write((Int64)v) },
            { typeof(UInt64), (w, v) => w.Write((UInt64)v) },
            { typeof(float), (w, v) => w.Write((float)v) },
            { typeof(double), (w, v) => w.Write((double)v) },

            // array of primitives
            { typeof(char[]), (w, v) => writeArray(w, (v as char[])) },
            { typeof(bool[]), (w, v) => writeArray(w, (v as bool[])) },
            { typeof(sbyte[]), (w, v) => writeArray(w, (v as sbyte[])) },
            { typeof(byte[]), (w, v) => writeArray(w, (v as byte[])) },
            { typeof(short[]), (w, v) => writeArray(w, (v as short[])) },
            { typeof(ushort[]), (w, v) => writeArray(w, (v as ushort[])) },
            { typeof(int[]), (w, v) => writeArray(w, (v as int[])) },
            { typeof(uint[]), (w, v) => writeArray(w, (v as uint[])) },
            { typeof(Int64[]), (w, v) => writeArray(w, (v as Int64[])) },
            { typeof(UInt64[]), (w, v) => writeArray(w, (v as UInt64[])) },
            { typeof(float[]), (w, v) => writeArray(w, (v as float[])) },
            { typeof(double[]), (w, v) => writeArray(w, (v as double[])) },
        };

        /// <summary>
        /// Iterates over an objects public properties, writing each property as bytes to a specified bin file
        /// </summary>
        /// <typeparam name="T">The class type</typeparam>
        /// <param name="filePath">File path to .bin file</param>
        /// <param name="obj">Object to iterate over</param>
        public static void Write<T>(string filePath, T obj)
        {
            // get class objects property info
            PropertyInfo[] properties = obj.GetType().GetProperties();

            // error check for empty object
            if (properties == null || properties.Length == 0)
                return;

            // create binary writer with file path
            using (var binWriter = new BinaryWriter(new FileStream(filePath, FileMode.Append)))
            {
                // loop over properties
                foreach (var prop in properties)
                {
                    // get the value of the property
                    object value = prop.GetValue(obj);

                    // get the type of the value
                    Type propType = prop.PropertyType;

                    // get appropriate action
                    Action<BinaryWriter, object> action = _actions[propType];

                    // execute action
                    action(binWriter, value);
                }
            }
        }

        /// <summary>
        /// Writes a nested class to bin file
        /// <para></para>
        /// The classes type needs to be added to the <see cref="_actions"/> dictionary for this
        /// method to know how to deal with the object
        /// </summary>
        /// <typeparam name="T">Nested classes type</typeparam>
        /// <param name="binWriter">The <see cref="BinaryWriter"/></param>
        /// <param name="obj">The object whose properties we want to write</param>
        private static void writeNestedClass<T>(BinaryWriter binWriter, T obj) where T : new()
        {
            // get class objects property info
            PropertyInfo[] properties = obj.GetType().GetProperties();

            // error check for empty object
            if (properties == null || properties.Length == 0)
                return;

            // loop over properties
            foreach (var prop in properties)
            {
                // get the value of the property
                object value = prop.GetValue(obj);

                // get the type of the value
                Type propType = prop.PropertyType;

                // get appropriate action
                Action<BinaryWriter, object> action = _actions[propType];

                // execute action
                action(binWriter, value);
            }
        }

        /// <summary>
        /// If the property is a list, write each value in the array
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="binWriter">The <see cref="BinaryWriter"/> that writes the value</param>
        /// <param name="array">The array that holds the values</param>
        private static void writeArray<T>(BinaryWriter binWriter, T[] array)
        {
            // if list is null, something broke!
            if (array == null)
                Debugger.Break();

            // write each value in the list
            foreach (var val in array)
            {
                // get appropriate action
                Action<BinaryWriter, object> action = _actions[val.GetType()];

                // execute action
                action(binWriter, val);
            }
        }

        /// <summary>
        /// Iterates over a array of class objects and writes their properties to bin file
        /// <para></para>
        /// The classes type needs to be added to the <see cref="_actions"/> dictionary for this
        /// method to know how to deal with the object
        /// </summary>
        /// <typeparam name="T">Type of objects in array</typeparam>
        /// <param name="binWriter">The <see cref="BinaryWriter"/></param>
        /// <param name="list">The array containing the objects</param>
        private static void writeArrayOfNestedClasses<T>(BinaryWriter binWriter, T[] list) where T : new()
        {
            foreach (var obj in list)
            {
                writeNestedClass(binWriter, obj);
            }
        }
    }
}
