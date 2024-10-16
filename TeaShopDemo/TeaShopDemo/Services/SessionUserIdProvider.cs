namespace TeaShopDemo.Services
{
    using Microsoft.AspNetCore.SignalR;

    public class SessionUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();
            var session = httpContext.Session;

            if (!session.IsAvailable)
            {
                session.LoadAsync().GetAwaiter().GetResult();
            }

            if (!session.TryGetValue("SessionId", out _))
            {
                var sessionId = Guid.NewGuid().ToString();
                session.SetString("SessionId", sessionId);
                return sessionId;
            }

            return session.GetString("SessionId");
        }
    }
}

