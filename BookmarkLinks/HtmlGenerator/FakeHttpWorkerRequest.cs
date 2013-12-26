using System;
using System.IO;
using System.Text;
using System.Web;

namespace EPiServer.CodeSample.BookmarkLinks.HtmlGenerator
{
    public class FakeHttpWorkerRequest : HttpWorkerRequest
    {
        public string QueryString { get; set; }
        public string PageVirtualPath { get; set; }
        public string ApplicationPhysicalPath { get; set; }
        public string ApplicationVirtualPath { get; set; }
        public TextWriter OutputTextWriter { get; set; }

        public override string GetUriPath()
        {
            return string.Concat(ApplicationVirtualPath, PageVirtualPath);
        }

        public override string GetQueryString()
        {
            return QueryString.TrimStart('?');
        }

        public override string GetRawUrl()
        {
            return string.Concat(ApplicationVirtualPath, PageVirtualPath, QueryString);
        }

        public override void SendResponseFromMemory(byte[] data, int length)
        {
            OutputTextWriter.Write(Encoding.UTF8.GetChars(data, 0, length));
        }

        public override string GetHttpVerbName() { return "GET"; }
        public override string GetHttpVersion() { return "HTTP/1.0"; }
        public override string GetRemoteAddress() { return "127.0.0.1"; }
        public override string GetLocalAddress() { return "127.0.0.1"; }
        public override int GetRemotePort() { return 0; }
        public override int GetLocalPort() { return 80; }

        public override void SendStatus(int statusCode, string statusDescription) { }
        public override void SendKnownResponseHeader(int index, string value) { }
        public override void SendUnknownResponseHeader(string name, string value) { }
        public override void SendResponseFromFile(string filename, long offset, long length) { }
        public override void SendResponseFromFile(IntPtr handle, long offset, long length) { }
        public override void FlushResponse(bool finalFlush) { }
        public override void EndOfRequest() { }
    }
}
