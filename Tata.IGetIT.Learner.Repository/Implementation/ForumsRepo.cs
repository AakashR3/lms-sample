using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class ForumsRepo : IForumsRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public ForumsRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<int> CreatePosting(CreatePosting posting, int userId)
        {
            var param = new Dictionary<string, object>
            {
                  { "@CourseID", posting.CourseID },
                  { "@Title", posting.Title },
                  { "@Description", posting.Description},
                  { "@FilePath", posting.FilePath },
                  { "@PostedBy", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.FORUM_CREATE_POSTING,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> UpdatePosting(UpdatePosting posting, int userId)
        {
            var param = new Dictionary<string, object>
            {
                  { "@PostID", posting.PostID },
                  { "@CourseID", posting.CourseID },
                  { "@Title", posting.Title },
                  { "@Description", posting.Description},
                  { "@FilePath", posting.FilePath },
                  { "@PostedBy", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.FORUM_UPDATE_POSTING,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        //public Task<DeletePostingResponse> DeletePosting(DeletePosting posting, int userId)
        //{
        //    var param = new Dictionary<string, object>
        //    {
        //          { "@PostID", posting.PostID },
        //          { "@DeletedBy", userId }
        //    };

        //    QueryInfo queryInfo = new QueryInfo()
        //    {
        //        queryType = QueryType.StoredProcedure,
        //        QueryText = StoredProcedures.FORUM_DELETE_POSTING,
        //        Parameters = param
        //    };

        //    return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        //}

        public Task<DeletePostingResponse> DeletePosting(DeletePosting posting, int userId)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@PostID", posting.PostID);
                param.Add("@DeletedBy", userId);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.FORUM_DELETE_POSTING,
                    Parameters = param
                };

                return _databaseOperations.GetFirstRecordAsync<DeletePostingResponse>(queryInfo);

            }
            catch
            {
                throw;
            }
        }

        public Task<int> CreateReply(CreateReply createReply, int userId)
        {
            var param = new Dictionary<string, object>
            {
                  { "@PostID", createReply.PostID },
                  { "@ReplyText", createReply.ReplyText },
                  { "@FilePath", createReply.FilePath },
                  { "@ReplyBy", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.FORUM_CREATE_REPLY,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> UpdateReply(UpdateReply updateReply, int userId)
        {
            var param = new Dictionary<string, object>
            {
                  { "@ReplyID", updateReply.ReplyID },
                  { "@PostID", updateReply.PostID },
                  { "@ReplyText", updateReply.ReplyText },
                  { "@FilePath", updateReply.FilePath },
                  { "@ReplyBy", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.FORUM_UPDATE_REPLY,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        //public Task<int> DeleteReply(DeleteReply reply, int userId)
        //{
        //    var param = new Dictionary<string, object>
        //    {
        //          { "@ReplyID", reply.ReplyID },
        //          { "@DeletedBy", userId }
        //    };

        //    QueryInfo queryInfo = new QueryInfo()
        //    {
        //        queryType = QueryType.StoredProcedure,
        //        QueryText = StoredProcedures.FORUM_DELETE_REPLY,
        //        Parameters = param
        //    };

        //    return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        //}

        public Task<DeleteReplyResponse> DeleteReply(DeleteReply reply, int userId)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@ReplyID", reply.ReplyID);
                param.Add("@DeletedBy", userId);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.FORUM_DELETE_REPLY,
                    Parameters = param
                };

                return _databaseOperations.GetFirstRecordAsync<DeleteReplyResponse>(queryInfo);

            }
            catch
            {
                throw;
            }
        }

        public Task<int> HideReply(HideReply reply, int userId)
        {
            var param = new Dictionary<string, object>
            {
                  { "@ReplyID", reply.ReplyID },
                  { "@Status", reply.Status },
                  { "@UserId", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.FORUM_HIDE_REPLY,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }


        public Task<IEnumerable<DiscussionForum>> CourseForum(int UserID, int CourseID, string SearchText, string SearchType)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                  { "@CourseID", CourseID },
                  { "@UserID", UserID },
                  { "@SearchText", SearchText },
                  { "@SearchType", SearchType }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.FORUM_LIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<DiscussionForum>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
