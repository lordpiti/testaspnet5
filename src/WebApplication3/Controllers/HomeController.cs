using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MongoDB.Driver;
using WebApplication3.Models;
using MongoDB.Driver.Builders;
using DAL.Models;
using DAL.Interface;
using DAL.Concrete;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Framework.Runtime;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.OptionsModel;
using DAL.Properties;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository _bookRepository;
        private readonly IApplicationEnvironment _appEnvironment;
        private readonly CloudBlobClient _cloudStorageClient;

        public HomeController(IBookRepository bookRepository,
            IApplicationEnvironment applicationEnvironment, IOptions<AppSettings> settings)
        {
            _bookRepository = bookRepository;
            _appEnvironment = applicationEnvironment;

            //for some reason DI doesn't look to work properly on Azure when loading the config values on the Appsettings class
            //thats why the connection strings for the mongodb db are hardcoded here. Locally it's ok
            //This is only for azure
            var cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=pititest;AccountKey=UIAjevML+igZ7ldWewh0VrVsi2kWUetFPY9qHdvmi3J1Xjo1rb3QzNSc3zknBK+melgoIEshEx5XG7DLt1Vb/A==");

            
            //var cloudStorageAccount = CloudStorageAccount.Parse(settings.Options.StorageKey);

            _cloudStorageClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Route("/api/books")]
        public List<GenericBook> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        [HttpDelete]
        [Route("/api/books/{id}")]
        public bool DeleteBook(string id)
        {
            _bookRepository.Delete(id);

            return true;
        }

        [HttpPost]
        [Route("/api/books")]
        public GenericBook CreateBook([FromBody] GenericBook theBook)
        {
            return _bookRepository.Create(theBook);//test comment
        }

        [HttpPut]
        [Route("/api/books/{id}")]
        public GenericBook UpdateBook(string id, [FromBody]GenericBook book)
        {
            return _bookRepository.Update(id, book);
        }

        [HttpGet]
        [Route("/api/books/categories")]
        public IEnumerable<GenericCategory> GetCategories()
        {
            return _bookRepository.GetCategories();
        }

        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        [Route("/api/books/postImg")]
        public string PostImg([FromBody] ImageInfo postData)
        {
            string base64 = postData.bytes.Substring(postData.bytes.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(base64);

            var container = _cloudStorageClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            var blobName = postData.fileName;

            // Retrieve reference to a blob with an specified name
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            //TODO: if exists, then create another one
            //blockBlob.Exists();
            Stream fileStream = new MemoryStream(data);
            blockBlob.UploadFromStream(fileStream);

            //System.IO.File.WriteAllBytes("c:\\test\\jaja.png", data);

            return blockBlob.Uri.ToString();
        }

        [HttpGet]
        [Route("/api/books/getAll")]
        public List<ImageInfo> GetAll()
        {
            var list = new List<ImageInfo>();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = _cloudStorageClient.GetContainerReference("mycontainer");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    list.Add(new ImageInfo() { fileName = blob.Name, bytes = blob.Uri.ToString() });
                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
            return list;
        }
    }

    public class ImageInfo
    {
        public string bytes { get; set; }

        public string fileName { get; set; }
    }
}
