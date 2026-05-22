
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
        Console.Write("🍅 PomoFocus - 专注时刻\n");

        Console.WriteLine($"番茄钟开始!专注{workMinutes} 分钟");//Console.WriteLine()能自动换行
        Console.WriteLine($"倒计时:{remainingSeconds}秒");//Console.Write()不会自动换行
        
        while(remainingSeconds > 0)
        {
            Console.Write($"\r剩余时间:{remainingSeconds}秒");
            Thread.Sleep(1000);//延时
            remainingSeconds--;
        }
        Console.WriteLine("\n时间到!");
        Console.Beep(1000,1000); //发出提示音
        
        //Console.WriteLine("你好,PomoFocus");//5.输出
        Console.ReadKey();//6.等待用户按键
    }
}

