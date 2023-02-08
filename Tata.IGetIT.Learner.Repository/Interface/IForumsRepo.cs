using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IForumsRepo
    {
        public Task<int> CreatePosting(CreatePosting posting, int userId);
        public Task<int> UpdatePosting(UpdatePosting posting, int userId);
        public Task<DeletePostingResponse> DeletePosting(DeletePosting posting, int userId);
        public Task<int> CreateReply(CreateReply createReply, int userId);
        public Task<int> UpdateReply(UpdateReply updateReply, int userId);
        public Task<DeleteReplyResponse> DeleteReply(DeleteReply reply, int userId);

        public Task<int> HideReply(HideReply reply, int userId);

        public Task<IEnumerable<DiscussionForum>> CourseForum(int UserID, int CourseID,string SearchText, string SearchType);

    }
}
