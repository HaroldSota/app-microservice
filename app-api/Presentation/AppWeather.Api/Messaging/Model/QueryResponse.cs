using System;

namespace AppWeather.Api.Messaging.Model
{
    public sealed class QueryResponse<TQueryResult>
    {
        public QueryResponse(TQueryResult result)
        {
            MessageType = MessageType.OK;
            Result = result;
        }



        public QueryResponse(MessageType messageType, QueryResponseError error)
        {
            MessageType = messageType;
            Error = error;
        }

        public QueryResponse(MessageType messageType, Exception exception)
        {
            MessageType = messageType;
            Error = new QueryResponseError(exception);
        }

        public MessageType MessageType { get; private set; }

        public TQueryResult Result { get; private set; }

        public QueryResponseError Error { get; private set; }

    }

}