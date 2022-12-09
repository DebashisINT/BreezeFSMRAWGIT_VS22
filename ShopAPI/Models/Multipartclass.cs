using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ShopAPI.Models
{



    class MyCustomData
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
    }

    class CustomMultipartFileStreamProvider : MultipartMemoryStreamProvider
    {
        public List<MyCustomData> CustomData { get; set; }

        public CustomMultipartFileStreamProvider()
        {
            CustomData = new List<MyCustomData>();
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach (var file in Contents)
            {
                var parameters = file.Headers.ContentDisposition.Parameters;
                var data = new MyCustomData
                {
                    Foo = int.Parse(GetNameHeaderValue(parameters, "Foo")),
                    Bar = GetNameHeaderValue(parameters, "Bar"),
                };

                CustomData.Add(data);
            }

            return base.ExecutePostProcessingAsync();
        }

        private static string GetNameHeaderValue(ICollection<NameValueHeaderValue> headerValues, string name)
        {
            var nameValueHeader = headerValues.FirstOrDefault(
                x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return nameValueHeader != null ? nameValueHeader.Value : null;
        }
    }
}