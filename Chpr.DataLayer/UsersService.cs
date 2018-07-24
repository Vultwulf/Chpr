using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Chpr.DataLayer.Entities;

namespace Chpr.DataLayer
{
    public class UsersService : ServiceBase
    {
        /// <summary>
        ///  AddUser will accept a User object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            Context.Save<User>(user);
        }

        /// <summary>
        /// ModifyUser tries to load an existing ModifyUser, modifies and saves it back. If the Item doesn’t exist, it raises an exception
        /// </summary>
        /// <param name="user"></param>
        public void ModifyUser(User user)
        {
            Context.Save<User>(user);
        }

        /// <summary>
        /// GetUser will perform a Table Scan operation to return given user
        /// </summary>
        /// <param name="userName">UserName string</param>
        /// <returns>Collection of Users</returns>
        public User GetUser(string userName)
        {
            User user = Context.Load<User>(userName);
            return user;
        }

        /// <summary>
        /// GetUser will perform a Table Scan operation to return given user
        /// </summary>
        /// <param name="userName">UserName string</param>
        /// <param name="password">Password string</param>
        /// <returns>Single User</returns>
        public async Task<User> GetUser(string userName, string password)
        {
            User user = null;

            User userRetrieved = await Context.LoadAsync<User>(userName);

            if (userRetrieved != null && userRetrieved.Password.Equals(this.EncodePassword(password)))
            {
                user = userRetrieved;
            }

            return user;
        }

        /// <summary>
        /// GetAllUsers will perform a Table Scan operation to return all the Users
        /// </summary>
        /// <returns>Collection of Users</returns>
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = Context.Scan<User>();
            return users;
        }

        /// <summary>
        /// Delete User will remove an item from DynamoDb
        /// </summary>
        /// <param name="userName">UserName string</param>
        public void DeleteUser(string userName)
        {
            Context.Delete<User>(userName);
        }

        /// <summary>
        /// Returns an encoded form of the given password
        /// </summary>
        /// <param name="password">The Password</param>
        /// <returns>Encoded Password string</returns>
        public string EncodePassword(string password)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            return encoded;
        }
    }
}