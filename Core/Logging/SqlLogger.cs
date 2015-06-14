using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;

namespace RoundTheClock.Core.Logging
{
    public class SqlLogger : DbCommandInterceptor
    {
        private ILog _logger = LogManager.GetLogger(typeof(SqlLogger));

        private delegate void ExecutingMethod<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext);

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            CommandExecuting<int>(base.NonQueryExecuting, command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            CommandExecuting<DbDataReader>(base.ReaderExecuting, command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            CommandExecuting<object>(base.ScalarExecuting, command, interceptionContext);
        }

        private void CommandExecuting<T>(ExecutingMethod<T> executingMethod, DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            executingMethod.Invoke(command, interceptionContext);

            if (interceptionContext.Exception != null)
            {

                string sqlStr = command.CommandText;
                foreach (DbParameter parameter in command.Parameters)
                {
                    sqlStr = sqlStr.Replace(parameter.ParameterName,
                                ToDbFormattedString(parameter.Value));
                }
                sqlStr = sqlStr.Replace("\r\n", " ");
                _logger.Error(String.Format("Error executing command: {0}", sqlStr), interceptionContext.Exception);
            }
            else
            {
                _logger.Info(String.Format("Executing command: {0}", command.CommandText));
            }
        }

        private string ToDbFormattedString(object p)
        {
            if (p is String)
                return "'" + (p as string).Replace("'", "''") + "'";
            return p.ToString();
        }
    }
}