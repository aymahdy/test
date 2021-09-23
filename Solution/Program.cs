using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    public class Solution
    {
        static void Main(string[] args)
        {
            ILogger<string> logger = new Logger<string>();
            IDataStore dataStore = new DataStore(logger);
            string data;
            logger.Write("Program: Program Started");
            dataStore.Create("testkey", "testdata");
            dataStore.TryRead("testkey2", out data);
            dataStore.TryRead("testkey1", out data);
            dataStore.Update("testkey", "testdata2");
            dataStore.Delete("testkey");
            logger.Write("Program: Program Finished");

        }
    }


    public interface IDataStore
    {
        bool Create(string key, string data);
        bool TryRead(string key, out string data);
        bool Update(string key, string data);
        bool Delete(string key);
    }

    public class DataStore : IDataStore
    {
        private readonly string _baseMessage;
        private Dictionary<string, string> _keyDataPairs;
        private readonly ILogger<string> _logger;
        public DataStore(ILogger<string> logger)
        {
            _logger = logger;
            _keyDataPairs = new Dictionary<string, string>();
            _baseMessage = "MyDataStore: ";
        }
        public bool Create(string key, string data)
        {
            try
            {
                _logger.Write($"{_baseMessage}Call - Create ('{key}','{data}')");
                _keyDataPairs.Add(key, data);
                _logger.Write($"{_baseMessage}Result - Entry with key '{key}' and data '{data}' created");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool TryRead(string key, out string data)
        {
            try
            {
                _logger.Write($"{_baseMessage}Call - Read ('{key}')");
                data = _keyDataPairs.FirstOrDefault(a => a.Key == key).Value;
                _logger.Write(string.IsNullOrWhiteSpace(data)
                    ? $"{_baseMessage}Result - Entry with key '{key}' does not exists"
                    : $"{_baseMessage}Result - Entry with key '{key}' returned");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                data = null;
                return false;
            }
        }

        public bool Update(string key, string data)
        {
            try
            {
                _logger.Write($"{_baseMessage}Call - Update ('{key}','{data}')");
                _keyDataPairs[key] = data;
                _logger.Write($"{_baseMessage}Result - Entry with key '{key}' updated with the data '{data}'");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(string key)
        {
            try
            {
                _logger.Write($"{_baseMessage}Call - Remove ('{key}')");
                _keyDataPairs.Remove(key);
                _logger.Write($"{_baseMessage}Result - Entry with key '{key}' removed");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
    public interface ILogger<T>
    {
        void Write(T message);
    }

    public class Logger<T> : ILogger<T>
    {
        public void Write(T message)
        {
            Console.WriteLine(message);
        }
    }
}
