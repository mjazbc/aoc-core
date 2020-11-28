using System;
using System.IO;
using System.Linq;

namespace aoc_core
{
    public class AocInput
    {
        private string _inputText;
        
        public bool IsSet;

        public void LoadFromFile(string filePath)
        {
            _inputText = File.ReadAllText(filePath); 
            IsSet = true;
        }
        public void Load(string input)
        {
            _inputText = input;
            IsSet = true;
        }
        public void Load(int input)
        {
            _inputText = input.ToString();
            IsSet = true;
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
