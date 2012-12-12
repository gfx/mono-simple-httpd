using System;
using System.IO;
using System.Net;
using System.Text;

class SimpleHttpd
{
  static void Main()
  {
    var root   = ".";
    var prefix = "http://*:2000/";

    var listener = new HttpListener();
    listener.Prefixes.Add(prefix);
    listener.Start();

    while (true) {
      var context  = listener.GetContext();
      var request  = context.Request;
      var response = context.Response;

      Console.WriteLine(request.RawUrl);

      var path = root + request.RawUrl;

      try {
        var content = File.ReadAllBytes(path);
        response.OutputStream.Write(content, 0, content.Length);
      }
      catch (FileNotFoundException e) {
        ErrorResponse(response, 404, e);
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
        response.OutputStream.Write(content, 0, content.Length);
  }
}
// vim: set tabstop=2:
// vim: set shiftwidth=2:
