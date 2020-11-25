using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Util
{
    public class AocInput
    {
        private readonly string _inputText;
        public AocInput(string inputPath)
        {
            _inputText = File.ReadAllText(inputPath);
        }

        public int AsInt() => int.Parse(_inputText);
        public string AsString() => _inputText;
        public int[] AsIntArray(char separator = '\n') => ConvertToArray<int>(separator);
        public string[] AsStringArray(char separator = '\n') => ConvertToArray<string>(separator);
        public T AsCustomType<T>(Func<string, T> TypeConverter) => TypeConverter(_inputText);
        public T[] AsCustomTypeArray<T>(Func<string, T[]> TypeConverter) => TypeConverter(_inputText);
        private T[] ConvertToArray<T>(char separator)
        {
            return _inputText.Split(separator).Select(l => (T)Convert.ChangeType(l, typeof(T))).ToArray();
        }
    }
}
