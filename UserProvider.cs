using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace MyFirstLambda
{
    public class UserProvider
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;

        public UserProvider(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task<User[]> GetAllUsers()
        {
            var users = new List<User>();
            //var result = await amazonDynamoDB.ScanAsync(new ScanRequest { TableName = "user-table" });
            //if (result != null && result.Items != null)
            //{
            //    foreach (var item in result.Items)
            //    {
            //        item.TryGetValue("city", out var city);
            //        item.TryGetValue("Email", out var Email);
            //        item.TryGetValue("Address", out var Address);
            //        item.TryGetValue("Phone", out var Phone);
            //        users.Add(new User { Address = Address?.S, city = city?.S, Email = Email?.S, Phone = Phone?.S });
            //    }

            //    return users.ToArray();

            //}
            return Array.Empty<User>();
        }


    }
}
