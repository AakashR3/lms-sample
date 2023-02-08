using Azure.Storage.Blobs;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class ForumsService : IForumsService
    {
        private readonly IForumsRepo _forumsRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ForumConfig _forumConfig;
        public ForumsService(IForumsRepo forumsRepo, IOptions<ForumConfig> forumConfig)
        {
            if (forumsRepo == null)
            {
                new ArgumentNullException("ForumsRepo cannot be null");
            }
            _forumsRepo = forumsRepo;
            _forumConfig = forumConfig.Value;
        }
        public async Task<int> CreatePosting(CreatePosting posting, int userId, List<string> errorsMessages)
        {
            try
            {
                var fullPath = "";
                if (posting.File != null && posting.File.Length > 0)
                {
                    var file = posting.File;
                    var folderName = "Posting";
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var newFileName = UtilityHelper.GetUniqueFileName(fileName);
                        fullPath = Path.Combine(folderName, newFileName);
                        posting.FilePath = fullPath;
                        BlobContainerClient container = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);

                        //await container.CreateAsync();
                        BlobClient client = container.GetBlobClient(fullPath);
                        //Open a stream for the file we want to upload
                        await using (Stream? data = posting.File.OpenReadStream())
                        {   // Upload the file async
                            await client.UploadAsync(data, true);
                        }
                    }
                }
                var result = await _forumsRepo.CreatePosting(posting, userId);
                switch (result)
                {
                    case 1:
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_CREATE_POSTING);
                        break;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdatePosting(UpdatePosting posting, int userId, List<string> errorsMessages)
        {
            try
            {
                var fullPath = "";
                if (posting.File != null && posting.File.Length > 0)
                {
                    var file = posting.File;
                    var folderName = "Posting";
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var newFileName = UtilityHelper.GetUniqueFileName(fileName);
                        fullPath = Path.Combine(folderName, newFileName);
                        posting.FilePath = fullPath;
                        BlobContainerClient container = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);
                        //await container.CreateAsync();
                        BlobClient client = container.GetBlobClient(fullPath);
                        //Open a stream for the file we want to upload
                        await using (Stream? data = posting.File.OpenReadStream())
                        {   // Upload the file async
                            await client.UploadAsync(data, true);
                        }
                    }
                }
                var result = await _forumsRepo.UpdatePosting(posting, userId);
                switch (result)
                {
                    case 1:
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_UPDATE_POSTING);
                        break;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DeletePostingResponse> DeletePosting(DeletePosting posting, int userId, List<string> errorsMessages)
        {
            try
            {
                var response = await _forumsRepo.DeletePosting(posting, userId);

                if (response == null)
                {
                    errorsMessages.Add(LearnerAppConstants.UNABLE_TO_DELETE_POSTING);
                }
                else
                {
                    if (response.Result == 1)
                    {
                        string[] FileNewName = response.FilePath.Replace("\\", "/").Split("/");
                        string SplitedFileName = FileNewName[1];

                        List<string> errorMessage = new();
                        BlobContainerClient client = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);
                        BlobClient file = client.GetBlobClient(response.FilePath);
                        // Check if the file exists in the container
                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                    else
                    {
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_DELETE_POSTING);
                    }
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CreateReply(CreateReply reply, int userId, List<string> errorsMessages)
        {
            try
            {
                var fullPath = "";
                if (reply.File != null && reply.File.Length > 0)
                {
                    var file = reply.File;
                    var folderName = "Reply";
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var newFileName = UtilityHelper.GetUniqueFileName(fileName);
                        fullPath = Path.Combine(folderName, newFileName);
                        reply.FilePath = fullPath;
                        BlobContainerClient container = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);
                        //await container.CreateAsync();
                        BlobClient client = container.GetBlobClient(fullPath);
                        //Open a stream for the file we want to upload
                        await using (Stream? data = reply.File.OpenReadStream())
                        {   // Upload the file async
                            await client.UploadAsync(data, true);
                        }
                    }
                }
                var result = await _forumsRepo.CreateReply(reply, userId);
                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.INVALID_POSTID);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.INVALID_POST_STATUS_CLOSED);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_CREATE_REPLY);
                        break;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateReply(UpdateReply reply, int userId, List<string> errorsMessages)
        {
            try
            {
                var fullPath = "";
                if (reply.File != null && reply.File.Length > 0)
                {
                    var file = reply.File;
                    var folderName = "Reply";
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var newFileName = UtilityHelper.GetUniqueFileName(fileName);
                        fullPath = Path.Combine(folderName, newFileName);
                        reply.FilePath = fullPath;
                        BlobContainerClient container = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);
                        //await container.CreateAsync();
                        BlobClient client = container.GetBlobClient(fullPath);
                        //Open a stream for the file we want to upload
                        await using (Stream? data = reply.File.OpenReadStream())
                        {   // Upload the file async
                            await client.UploadAsync(data, true);
                        }
                    }
                }
                var result = await _forumsRepo.UpdateReply(reply, userId);
                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.INVALID_POSTID);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.INVALID_POST_STATUS_CLOSED);
                        break;

                    case -3:
                        errorsMessages.Add(LearnerAppConstants.INVALID_REPLY_STATUS);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_UPDATE_REPLY);
                        break;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DeleteReplyResponse> DeleteReply(DeleteReply reply, int userId, List<string> errorsMessages)
        {
            try
            {
                var response = await _forumsRepo.DeleteReply(reply, userId);
                if (response == null)
                {
                    errorsMessages.Add(LearnerAppConstants.UNABLE_TO_DELETE_REPLY);
                }
                else
                {
                    if (response.Result == 1)
                    {
                        string[] FileNewName = response.FilePath.Replace("\\", "/").Split("/");
                        string SplitedFileName = FileNewName[1];

                        List<string> errorMessage = new();
                        var _storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=igetit;AccountKey=JtMgB031F2+R4JBLMF8R2sBvYNMKfnCt2kyd7h7JW9xr+yNNrrd0WJ9qCrbCuIIYJUPuvyaFhUDIyQZejx5tWw==;EndpointSuffix=core.windows.net";
                        var _storageContainerName = "discussionforum";
                        BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
                        BlobClient file = client.GetBlobClient(response.FilePath);
                        // Check if the file exists in the container
                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                    else
                    {
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_DELETE_REPLY);
                    }
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> HideReply(HideReply reply, int userId, List<string> errorsMessages)
        {
            try
            {
                var result = await _forumsRepo.HideReply(reply, userId);
                switch (result)
                {
                    case 1:
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_TO_MODIFY_DETAILS);
                        break;
                }
                return result;
            }
            catch
            {
                throw;
            }
        }


        public async Task<Forums> CourseForum(int UserID, int CourseID, string SearchText, string SearchType, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _forumsRepo.CourseForum(UserID, CourseID, SearchText, SearchType);
                Forums data = new Forums();

                if (result.Any())
                {
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    using JsonDocument doc = JsonDocument.Parse(jsonString);
                    JsonElement root = doc.RootElement;
                    var u1 = root[0];
                    int MyReplyCount = Convert.ToInt32(u1.GetProperty("MyReplyCount").ToString());
                    int TotalReplyCourse = Convert.ToInt32(u1.GetProperty("TotalReplyCourse").ToString());

                    var forums = from c in result
                                 group c by new
                                 {
                                     c.PostID,
                                     c.Title,
                                     c.Description,
                                     c.TotalReply,
                                     c.PostedTime,
                                     c.PostFilePath,
                                 } into gcs
                                 select new ForumPosting()
                                 {
                                     PostID = gcs.Key.PostID,
                                     Title = gcs.Key.Title,
                                     Description = gcs.Key.Description,
                                     TotalReply = gcs.Key.TotalReply,
                                     PostedTime = gcs.Key.PostedTime,
                                     PostFilePath = gcs.Key.PostFilePath,
                                     Reply = (from gc in gcs.ToList()
                                              select new ForumReply()
                                              {
                                                  ReplyID = gc.ReplyID,
                                                  ReplyText = gc.ReplyText,
                                                  ReplyTime = gc.ReplyTime,
                                                  ReplyFilePath = gc.ReplyFilePath
                                              })
                                 };

                    data.Posting = forums;
                    data.MyReplyCount = MyReplyCount;
                    data.TotalReplyCount = TotalReplyCourse;
                    return data;
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                    return data;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<DownloadReponse> DownloadFile(DownloadFile downloadFile, List<string> ErrorsMessages)
        {
            try
            {
                DownloadReponse reponse = new DownloadReponse();
                string[] FileNewName = downloadFile.FileName.Replace("\\", "/").Split("/");
                string SplitedFileName = FileNewName[1];

                List<string> errorMessage = new();
                BlobContainerClient client = new BlobContainerClient(_forumConfig.ConnectionString, _forumConfig.ContainerName);
                BlobClient file = client.GetBlobClient(downloadFile.FileName);
                // Check if the file exists in the container

                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;
                    var content = await file.DownloadContentAsync();
                    string name = SplitedFileName;
                    string contentType = content.Value.Details.ContentType;
                    string base64 = UtilityHelper.ConvertToBase64(blobContent);

                    var provider = new FileExtensionContentTypeProvider();
                    string FilecontentType;
                    if (!provider.TryGetContentType(SplitedFileName, out FilecontentType))
                    {
                        FilecontentType = "application/octet-stream";
                    }
                    reponse.FileContent = base64;
                    reponse.ContentType = contentType;
                    reponse.FileName = SplitedFileName;
                    reponse.FileType = FilecontentType;
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.RESOURCE_NOT_FOUND);
                    ErrorsMessages.Add(LearnerAppConstants.RESOURCE_NOT_FOUND);
                }
                return reponse;
            }
            catch
            {
                throw;
            }

        }
    }
}
