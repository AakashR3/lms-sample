using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class TrainingsRepo : ITrainingsRepo
    {

        private readonly IDatabaseManager _db;
        public TrainingsRepo(IDatabaseManager db)
        {
            _db = db;
        }

        //public async Task<PagedResult<IEnumerable<Training>>> GetTrainings(int userId, int pageNumber, int pageSize)
        //{
        //    var conn = _db.CreateConnection();

        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@UserId", userId);
        //        param.Add("@PageNumber", pageNumber);
        //        param.Add("@PageSize", pageSize);
        //        param.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //        Query query = new Query()
        //        {
        //            queryType = QueryType.StoredProcedure,
        //            QueryText = StoredProcedures.GetTrainings,
        //            Data = param
        //        };

        //        var dataReader = await _db.GetMultipleResultAsync(conn, query);

        //        var data = await dataReader.ReadAsync<Training>();
        //        var pagedData = new PagedResult<IEnumerable<Training>>()
        //        {
        //            Data = data,
        //            Message = data.Count() < 1 ? "No records found!":null
        //        };
        //        pagedData.TotalRecords = param.Get<int>("@TotalRecords");
        //        return pagedData;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (conn.State != ConnectionState.Closed)
        //        {
        //            conn.Close();
        //        }
        //    }
        //}


        public Task<IEnumerable<Training>> GetTrainings(int userId, int pageNumber, int pageSize, string filter)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserId", userId);
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@filter", filter);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ListTrainings,
                    Parameters = param
                };

                var result = _db.GetMultipleRecords<Training>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }


        public async Task<int> CreateTrainings(CreateTrainingRequest request, int userId, int accoutId)
        {
            string Attendees = string.Join(";", request.Attendees);


            var param = new Dictionary<string, object>
            {
                { "@Name", request.Name },
                { "@Description", request.Description },
                { "@AccountID", accoutId },
                { "@CourseID", request.CourseID },
                { "@UserId", userId },
                { "@Attendees", Attendees },

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.CreateTraining,
                Parameters = param
            };

            var result = await _db.ExecuteScalarAsyncInteger(queryInfo);

            return result;
        }
        public async Task<bool> UpdateTrainings(UpdateTrainingRequest request, int userId, int accoutId)
        {
            string Attendees = string.Join(";", request.Attendees);

            var param = new Dictionary<string, object>
            {
                { "@Name", request.Name },
                { "@Description", request.Description },
                { "@Id", request.TrainingID },
                { "@CourseID", request.CourseID },
                { "@UserId", userId },
                { "@AccountId", accoutId },
                { "@Attendees", Attendees }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.UpdateTraining,
                Parameters = param
            };

            return await _db.ExecuteScalarAsyncInteger(queryInfo) > 0;

        }
        public async Task<bool> DeleteTrainings(int id, int userId)
        {
            var param = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@UserId", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.DeleteTraining,
                Parameters = param
            };

            return await _db.ExecuteScalarAsyncInteger(queryInfo) > 0;

        }


        public async Task<int> CreateSession(CreateMeetingRequest request, string Attendees,string meetingId, string eventId, int userId)
        {
            var param = new Dictionary<string, object>
            {
                { "@TrainingID", request.TrainingID },
                { "@Subject", request.Name },
                { "@Description", request.Description },
                { "@Begin",request.StartDateTime.ToString("dd-MM-yyyy HH:mm:ss")},
                { "@End",request.EndDateTime.ToString("dd-MM-yyyy HH:mm:ss")},
                { "@Recurrence", request.Recurrence },
                { "@Location", request.Location },
                { "@PlatformType", request.PlatformTypeID },
                { "@InstructorName", request.InstructorName },
                { "@Attendees", Attendees },
                { "@TimeZone", request.TimeZone },
                { "@MeetingId", meetingId },
                { "@EventId", eventId },
                { "@UserId", userId },                

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.CreateMeetingSession,
                Parameters = param
            };

            var result = await _db.ExecuteScalarAsyncInteger(queryInfo);

            return result;
        }

        public async Task<int> UpdateSession(UpdateMeetingRequest request, string Attendees, string meetingId,string eventId, int userId)
        {
            var param = new Dictionary<string, object>
            {
                { "@TrainingSessionID", request.TrainingSessionID },
                { "@TrainingID", request.TrainingID },                
                { "@Subject", request.Name },
                { "@Description", request.Description },
                { "@Begin",request.StartDateTime.ToString("dd-MM-yyyy HH:mm:ss")},
                { "@End",request.EndDateTime.ToString("dd-MM-yyyy HH:mm:ss")},
                { "@Recurrence", request.Recurrence },
                { "@Location", request.Location },
                { "@PlatformType", request.PlatformTypeID },
                { "@InstructorName", request.InstructorName },
                { "@Attendees", Attendees },
                { "@TimeZone", request.TimeZone },
                { "@MeetingId", meetingId },
                { "@EventId", eventId },
                { "@UserId", userId },

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.UpdateMeetingSession,
                Parameters = param
            };

            var result = await _db.ExecuteScalarAsyncInteger(queryInfo);

            return result;
        }


        public async Task<bool> DeleteSession(int id, int userId)
        {
            var param = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@UserId", userId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.DeleteMeetingSession,
                Parameters = param
            };

            return await _db.ExecuteScalarAsyncInteger(queryInfo) > 0;

        }

        public async Task<Session> GetSession(int id,int UserID)
        {
            var param = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@UserID", UserID }

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetSession,
                Parameters = param
            };

            return await _db.GetFirstRecordAsync<Session>(queryInfo);
        }

        public async Task<Training> GetTrainings(int id,int UserID)
        {
            var param = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@UserID", UserID },
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetTrainings,
                Parameters = param
            };

            return await _db.GetFirstRecordAsync<Training>(queryInfo);
        }

        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            var param = new Dictionary<string, object>();
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetPlatforms,
                Parameters = param
            };

            return await  _db.GetMultipleRecords<Platform>(queryInfo);

        }

        public Task<IEnumerable<Session>> TrainingSession(int TrainingID,int UserID)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@TrainingID", TrainingID);
                param.Add("@UserID", UserID);

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.ListSessions,
                    Parameters = param
                };

                var result = _db.GetMultipleRecords<Session>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }



        public Task<IEnumerable<Session>> CalendarSessions(string Date, int UserId)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@Date", Date);
                param.Add("@UserId", UserId);
                
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.CalendarSessions,
                    Parameters = param
                };

                var result = _db.GetMultipleRecords<Session>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}
