using Newtonsoft.Json;

using System;
using System.IO;
using System.Collections.Generic;

namespace Struct.Core
{
    public class LocalStorage
    {
        private readonly string storageFile = "storage";

        /// <summary>
        /// Local Data
        /// </summary>
        private Dictionary<string, byte[]> localData;

        /// <summary>
        /// Construct
        /// </summary>
        public LocalStorage()
        {
            this.localData = File.Exists(this.storageFile) ?
                JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(File.ReadAllText(storageFile)) :
                new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// Returns true/false if
        /// the key exists in the
        /// dictionary.
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        /// <returns>Exists</returns>
        public bool Exists(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");
            else
                return this.localData.ContainsKey(propertyName);
        }

        /// <summary>
        /// Get data from the local
        /// storage and convert it
        /// to an object.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="PropertyName">Property Name</param>
        /// <returns>Object</returns>
        public T Get<T>(string propertyName)
        {
            // property name is required
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("PropertyName");

            // if key exists decrypt/convert and return
            if (this.localData.ContainsKey(propertyName))
                return JsonConvert.DeserializeObject<T>(
                    Encrypter.Decrypt(this.localData[propertyName])
                );

            // if key doesn't exist return default
            else
                return default(T);
        }

        /// <summary>
        /// Save data to the local storage
        /// data set.
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        /// <param name="propertyData">Property Data</param>
        public void Set(string propertyName, object propertyData)
        {
            // check required
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");
            else if (propertyData == null)
                throw new ArgumentNullException("propertyData");

            // if key exists, replace it
            if (this.localData.ContainsKey(propertyName))
            {
                this.localData[propertyName] = Encrypter.Encrypt(
                    JsonConvert.SerializeObject(propertyData)
                );
            }

            // if key doesn't exist, add it
            else
            {
                this.localData.Add(propertyName,
                    Encrypter.Encrypt(JsonConvert.SerializeObject(propertyData))
                );
            }

            // write data to file
            File.WriteAllText(this.storageFile, JsonConvert.SerializeObject(this.localData));
        }
    }
}
