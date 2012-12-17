using System;
using System.IO;
using System.Net;
using System.Text;

class SimpleHttpd
{
  static void Main(string[] args)
  {
    var port   = "1982";
    var root   = ".";
    var prefix = String.Format("http://*:{0}/", port);

    Log("listening {0}", prefix);

    var listener = new HttpListener();
    listener.Prefixes.Add(prefix);
    listener.Start();

    while (true) {
      var context  = listener.GetContext();
      var request  = context.Request;
      var response = context.Response;

      Log("{0} {1}", request.HttpMethod, request.RawUrl);

      var path = root + request.RawUrl;

      try {
        var content = File.ReadAllBytes(path);
        response.ContentType = "text/plain";
        response.AppendHeader("Content-Length", content.Length.ToString());
        response.OutputStream.Write(content, 0, content.Length);
      }
      catch (FileNotFoundException e) {
        ErrorResponse(response, 404, e);
      }
      catch (UnauthorizedAccessException e) {
        ErrorResponse(response, 403, e);
      }
      catch (Exception e) {
        ErrorResponse(response, 500, e);
      }
      response.Close();
    }
  }

  static void ErrorResponse(HttpListenerResponse response, Int32 status, Exception e)
  {
        response.StatusCode  = status;
        response.ContentType = "text/plain";
        var content = Encoding.Unicode.GetBytes(e.ToString() + "\n");
        response.AppendHeader("Content-Length", content.Length.ToString());
        response.OutputStream.Write(content, 0, content.Length);
  }

  static void Log(string format, params object[] args) {
    var s = string.Format(format, args);
    Console.WriteLine("{0} {1}", DateTime.Now, s);
  }
}
// vim: set tabstop=2:
// vim: set shiftwidth=2:
