using System;
using System.Collections.Generic;
using ServiceStack;
using Autoquery.ServiceModel;

namespace Autoquery.ServiceInterface
{
    public class CustomAutoQueryDataServices : Service
    {
        public IAutoQueryData AutoQuery { get; set; }

        public object Any(QueryRequestLogs query)
        {
            var date = query.Date.GetValueOrDefault(DateTime.UtcNow);
            var logSuffix = query.ViewErrors ? "-errors" : "";
            var csvLogsFile = VirtualFileSources.GetFile(
                "requestlogs/{0}-{1}/{0}-{1}-{2}{3}.csv".Fmt(
                    date.Year.ToString("0000"),
                    date.Month.ToString("00"),
                    date.Day.ToString("00"),
                    logSuffix));

            if (csvLogsFile == null)
                throw HttpError.NotFound("No logs found on " + date.ToShortDateString());

            var logs = csvLogsFile.ReadAllText().FromCsv<List<RequestLogEntry>>();

  
            try
            {
              var   q = AutoQuery.CreateQuery(query, Request,
                    db: new MemoryDataSource<RequestLogEntry>(logs, query, Request));
                return AutoQuery.Execute(query, q);
            }
            catch (Exception ex)
            {

// just to get stacktrace
   return ex;
            
            }
        
        }
        public object Any(TodayLogs request) =>
    Any(new QueryRequestLogs { Date = DateTime.UtcNow });

    }
}