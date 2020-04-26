using System;

namespace AppWeather.Api.Messaging.Model
{
    /// <summary>
    ///     Query response object
    /// </summary>
    /// <typeparam name="TQueryResult"></typeparam>
    public sealed class QueryResponse<TQueryResult>
    {
        /// <summary>
        ///    QueryResponse ctor. successful 
        /// </summary>
        /// <param name="result">Query result</param>
        public QueryResponse(TQueryResult result)
        {
            MessageType = MessageType.OK;
            Result = result;
        }

        /// <summary>
        ///    QueryResponse ctor. on validation error 
        /// </summary>
        public QueryResponse(MessageType messageType, QueryResponseError error)
        {
            MessageType = messageType;
            Error = error;
        }

        /// <summary>
        ///    QueryResponse ctor. on exception error 
        /// </summary>
        public QueryResponse(MessageType messageType, Exception exception)
        {
            MessageType = messageType;
            Error = new QueryResponseError(exception);
        }

        /// <summary>
        ///     Query execution status
        /// </summary>
        public MessageType MessageType { get; private set; }

        /// <summary>
        ///  Query execution result
        /// </summary>
        public TQueryResult Result { get; private set; }

        /// <summary>
        ///     Query execution error
        /// </summary>
        public QueryResponseError Error { get; private set; }
    }
}