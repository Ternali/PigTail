using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigTail
{
    class FetchInfo
    {
        public static string executeOperation(string command)
        {
            command = "python webhelper.py " + command + "&exit";
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = @"cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
            p.StandardInput.WriteLine(@"cd ..\..\Resources");
            p.StandardInput.WriteLine(command);
            //获取输出文本
            string output = p.StandardOutput.ReadToEnd();
            p.StandardInput.AutoFlush = true;
            //等待命令行退出
            p.WaitForExit();
            p.Close();
            //对控制台输出内容进行裁剪这部分是必须的
            int where = output.IndexOf("&exit");
            output = output.Substring(where + 5);
            return output;
        }
    }
}
