using System;
using SplashKitSDK;

public class MessageServer 
{
    private bool _running = true;
    private WebServer _server = new WebServer(8080);

    public bool IsRunnning 
    {
        get { return _running; }
    }

    public bool HasIncomingRequests
    {
        get { return _server.HasIncomingRequests; }
    }

    public void StopServer() 
    {
        if(_running)
        {
            _running = false;
            _server.Stop();
        }
    }

    public void HandleNextRequest()
    {
        HttpRequest request = _server.NextWebRequest;

        try
        {
            if ( request.IsGetRequestFor("/stop") )
            {
                request.SendResponse("Server Stopped");
                StopServer();
            }
            else if ( request.IsGetRequestFor("/index.html") || request.IsGetRequestFor("/") )
            {
                request.SendHtmlFileResponse("index.html");
            }
            else
            {
                request.SendResponse(HttpStatusCode.HttpStatusBadRequest);
            }
        }
        catch
        {
            request.SendResponse(HttpStatusCode.HttpStatusInternalServerError);
        }
    }
}
