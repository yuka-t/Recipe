using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace myApplication
{
    public class HTTPController
    {
        // exeファイルがあるディレクトリ
        private string rootAddress = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        private HttpListener listner = null;

        public HTTPController()
        {
        }

        public async Task<bool> AsyncConnectionServer(string connectionAddress)
        {
            bool isSucceed = true;

            try
            {
                using (listner = new HttpListener())
                {
                    listner.Prefixes.Add(connectionAddress);
                    listner.Start();

                    await Task.Run(() =>
                    {
                        while (listner != null)
                        {
                            HttpListenerContext context = listner.GetContext();
                            HttpListenerResponse res = context.Response;
                            res.StatusCode = 200;
                            byte[] content = Encoding.UTF8.GetBytes("HELLOをかえすだけの愚直サーバ。今後ここはhtmlを表示するように変更");
                            res.OutputStream.Write(content, 0, content.Length);
                            res.Close();
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                isSucceed = false;
            }

            return isSucceed;
        }

        public void StartsUpServer(string connectionAddress)
        {
            Process.Start(connectionAddress);
        }

        public void DisConnectServer()
        {
            listner = null;
        }
    }
}
