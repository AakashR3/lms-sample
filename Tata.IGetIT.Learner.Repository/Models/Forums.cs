using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Constants;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Forums
    {
        public int TotalReplyCount { get; set; }
        public int MyReplyCount { get; set; }
        public IEnumerable<ForumPosting> Posting { get; set; }
    }

    public class DiscussionForum
    {
        public int PostID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalReply { get; set; }
        public string PostedTime { get; set; } = string.Empty;
        public string PostFilePath { get; set; } = string.Empty;
        public int ReplyID { get; set; }
        public string ReplyText { get; set; }
        public string ReplyTime { get; set; }
        public string ReplyFilePath { get; set; }
        public int MyReplyCount { get; set; }
        public int TotalReplyCourse { get; set; }
        
    }
    public class ForumPosting
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalReply { get; set; }
        public string PostedTime { get; set; }
        public string PostFilePath { get; set; }
        public IEnumerable<ForumReply> Reply { get; set; }

    }
    public class ForumReply
    {
        public int ReplyID { get; set; }
        public string ReplyText { get; set; }
        public string ReplyTime { get; set; }
        public string ReplyFilePath { get; set; }
    }

    public class CreatePosting : IValidatableObject
    {
       
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public IFormFile File { get; set; }
        public string FilePath { get; set; }


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (CourseID<=0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_COURSEID));
            }
            if (Title==null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.TITLE_REQUIRED));
            }
            if (Description== null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.DESCRIPTION_REQUIRED));
            }

            if (File != null)
            {
                //if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "application/pdf" && Image.ContentType != "application/zip")
                //{
                //    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                //}

                if (!File.ContentType.Contains("image") && File.ContentType != "image/jpeg" && File.ContentType != "application/pdf" &&
                    !File.ContentType.Contains("zip") && File.ContentType != "application/octet-stream")
                {
                    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                }
            }

            return validationErrors;
        }
    }

    public class UpdatePosting : IValidatableObject
    {
        public int PostID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (PostID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_POSTID));
            }
            if (CourseID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_COURSEID));
            }
            if (Title == null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.TITLE_REQUIRED));
            }
            if (Description == null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.DESCRIPTION_REQUIRED));
            }

            if (File != null)
            {
                //if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "application/pdf" && Image.ContentType != "application/zip")
                //{
                //    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                //}

                if (!File.ContentType.Contains("image") && File.ContentType != "image/jpeg" && File.ContentType != "application/pdf" &&
                   !File.ContentType.Contains("zip") && File.ContentType != "application/octet-stream")
                {
                    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                }
            }

            return validationErrors;
        }
    }


    public class DeletePosting
    {
        public int PostID { get; set; }
    }

    public class DeletePostingResponse
    {
        public int PostID { get; set; }
        public int Result { get; set; }
        public string FilePath { get; set; }
    }

    public class CreateReply : IValidatableObject
    {
        public int PostID { get; set; }
        public string ReplyText { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (PostID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_POSTID));
            }
            if (ReplyText == null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.REPLY_TEXT_REQUIRED));
            }

            if (File != null)
            {
                //if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "application/pdf" && Image.ContentType != "application/zip")
                //{
                //    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                //}

                if (!File.ContentType.Contains("image") && File.ContentType != "image/jpeg" && File.ContentType != "application/pdf" &&
                   !File.ContentType.Contains("zip") && File.ContentType != "application/octet-stream")
                {
                    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                }
            }

            return validationErrors;
        }
    }

    public class UpdateReply : IValidatableObject
    {

        public int ReplyID { get; set; }
        public int PostID { get; set; }
        public string ReplyText { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }


        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (PostID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_POSTID));
            }
            if (ReplyText == null)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.REPLY_TEXT_REQUIRED));
            }

            if (File != null)
            {
                //if (Image.ContentType != "image/png" && Image.ContentType != "image/jpeg" && Image.ContentType != "application/pdf" && Image.ContentType != "application/zip")
                //{
                //    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                //}

                if (!File.ContentType.Contains("image") && File.ContentType != "image/jpeg" && File.ContentType != "application/pdf" &&
                   !File.ContentType.Contains("zip") && File.ContentType != "application/octet-stream")
                {
                    validationErrors.Add(new ValidationResult(LearnerAppConstants.SUPPORTED_FILE_FORMAT));
                }
            }

            return validationErrors;
        }


    }
    public class DeleteReply
    {
        public int ReplyID { get; set; }
    }

    public class DeleteReplyResponse
    {
        public int ReplyID { get; set; }
        public int Result { get; set; }
        public string FilePath { get; set; }
    }


    public class HideReply
    {
        public int ReplyID { get; set; }
        public int Status { get; set; }
    }

    public class DownloadFile
    {
        public string FileName { get; set; }
    }

    public class DownloadReponse
    {        
        public string FileName { get; set; }       
        public string ContentType { get; set; }
        public string FileType { get; set; }
        public string FileContent { get; set; }

    }


}
