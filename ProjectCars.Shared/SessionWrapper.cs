using System;

namespace ProjectCars.Shared
{
    public class SessionWrapper<T> where T : struct
    {
        public Guid SessionId { get; }
        public T Data { get; }
        public DateTime Timestamp { get; }

        public SessionWrapper(T data, Guid sessionId)
        {
            Data = data;
            SessionId = sessionId;
            Timestamp = DateTime.UtcNow;
        }
    }
}