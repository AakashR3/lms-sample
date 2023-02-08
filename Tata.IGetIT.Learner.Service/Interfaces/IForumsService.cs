using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IForumsService
    {
        public Task<int> CreatePosting(CreatePosting posting,int userId, List<string> ErrorsMessages);
        public Task<int> UpdatePosting(UpdatePosting posting, int userId, List<string> ErrorsMessages);
        public Task<DeletePostingResponse> DeletePosting(DeletePosting posting, int userId, List<string> ErrorsMessages);
        public Task<int> CreateReply(CreateReply createReply, int userId, List<string> ErrorsMessages);
        public Task<int> UpdateReply(UpdateReply updateReply, int userId, List<string> ErrorsMessages);
        public Task<DeleteReplyResponse> DeleteReply(DeleteReply reply, int userId, List<string> ErrorsMessages);
       
        public Task<int> HideReply(HideReply reply, int userId, List<string> ErrorsMessages);
        public Task<Forums> CourseForum(int UserID, int CourseID,string SearchText, string SearchType, List<string> ErrorsMessages);
        public Task<DownloadReponse> DownloadFile(DownloadFile downloadFile, List<string> ErrorsMessages);
    }
}
