
using System; //1.using 相当于Python的import
using System.Threading; //用于Sleep
namespace PomoFocus;//2.命名空间 相当于Python的包,用于组织代码
class Program//3.类
{
    static void Main(string[] args)//4.Main方法--程序入口
    {
        int workMinutes = 25;
        int totalSeconds = workMinutes * 60;
        int remainingSeconds = totalSeconds;

        Console.Clear(); //清屏
        Console.Write("PomoFocus - 专注时刻\n");

        //Console.WriteLine($"番茄钟开始!专注{workMinutes} 分钟");//Console.WriteLine()能自动换行
        //Console.WriteLine($"倒计时:{remainingSeconds}秒");//Console.Write()不会自动换行
        
        while(remainingSeconds > 0)
        {
            //计算进度百分比 
            int elapsed = totalSeconds - remainingSeconds;
            int percent = elapsed * 100 / totalSeconds;
            
            //计算进度条长度（20格）
            int progressLength = percent / 5;

            //绘制进度条
            string progressBar = "[" + new string('=',progressLength) + new string('-', 20-progressLength) + $"]{percent}%";
             
            //格式化时间显示 mm:ss
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds %60;
            string timestr = $"{minutes:D2}:{seconds:D2}";//D2代表两位数
            Console.Write($"\r{progressBar} {timestr}");
            //Console.Write($"\r\n剩余时间:{remainingSeconds}秒");
            Thread.Sleep(1000);//延时
            remainingSeconds--;
        }
        Console.WriteLine("\n时间到!休息一下吧！ ");
        Console.Beep(1000,1000); //发出提示音
        
        //Console.WriteLine("你好,PomoFocus");//5.输出
        Console.ReadKey();//6.等待用户按键
    }
}

