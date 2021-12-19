using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using Amazon.SQS;
using Amazon.SQS.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyFirstLambda
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public  async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string name = "No Name";
            context.Logger.Log("My First Lambda Function with API");
            if( request.QueryStringParameters!= null && request.QueryStringParameters.ContainsKey("name"))
            {
                name = request.QueryStringParameters["name"];
                 
            }
            var userprovider = new UserProvider(new AmazonDynamoDBClient());
            var users = userprovider.GetAllUsers();
            var sqsclient = new AmazonSQSClient();
            var message = new SendMessageRequest
            {
                QueueUrl = "https://sqs.ap-south-1.amazonaws.com/634400732722/demo-queue",
                MessageBody = "Hello from lamba function with " + name
            };
            await  sqsclient.SendMessageAsync(message);
              
            return new APIGatewayProxyResponse() { StatusCode = 200,
                Body = JsonConvert.SerializeObject(users, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })
                };
           // return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Venkat is hered") };
          
            // return new OkObjectResult() { StatusCode = 200, Value = "MytypeVenkat" };
            // return $"Hello from the outside".ToUpper();
        }
    }

  

}
