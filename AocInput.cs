using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_core
{
    public class AocInput
    {
        private string _inputText;
        
        public bool IsSet {get; private set;}

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
        public int[] AsIntArray(string separator) => ConvertToArray<int>(separator);
        public int[] AsIntArray() => ConvertToArray<int>(Environment.NewLine);
        
        public long[] AsLongArray(string separator) => ConvertToArray<long>(separator);
        public long[] AsLongArray() => ConvertToArray<long>(Environment.NewLine);
        public string[] AsStringArray(string separator) => ConvertToArray<string>(separator);
        public string[] AsStringArray() => ConvertToArray<string>(Environment.NewLine);
        public char[][] AsCharMatrix(string separator) => AsStringArray(separator).Select(line => line.ToCharArray()).ToArray();
        public char[,] AsCharMatrix() 
        { 
            var lines = AsStringArray(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
            var output = new char[lines.Length, lines[0].Length];
            for(int y = 0; y < output.GetLength(0); y++)
                for(int x = 0; x < output.GetLength(1); x++)
                    output[y,x] = lines[y][x];

            return output;
        }
        public T AsCustomType<T>(Func<string, T> TypeConverter) => TypeConverter(_inputText);
        public IEnumerable<T> AsCustomTypeEnumerable<T>(Func<string, T> TypeConverter, string separator) => _inputText.Split(separator).Select(l => TypeConverter(l));
        public IEnumerable<T> AsCustomTypeEnumerable<T>(Func<string, T> TypeConverter) => _inputText.Split(Environment.NewLine).Select(l => TypeConverter(l));
        private T[] ConvertToArray<T>(string separator)
        {
            return _inputText.Split(separator).Select(l => (T)Convert.ChangeType(l, typeof(T))).ToArray();
        }
    }
}
