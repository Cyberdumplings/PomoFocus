using System;
using System.Threading;

namespace PomoFocus;

class Program
{
    static TaskManager taskManager = new TaskManager();
    static DataStorage dataStorage = new DataStorage();
    
    static void Main(string[] args)
    {
        Console.Title = "PomoFocus - 番茄钟效率工具";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("欢迎使用 PomoFocus！");
        Console.ResetColor();
        
        // 加载保存的任务
        var savedTasks = dataStorage.LoadTasks();
        if (savedTasks.Count > 0)
        {
            taskManager.LoadTasks(savedTasks);
        }
        
        // 主菜单循环
        while (true)
        {
            ShowMainMenu();
            var key = Console.ReadKey(true).Key;
            
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    AddTask();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ShowTasksAndSelect();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    StartPomodoro();
                    break;
                case ConsoleKey.Q:
                    SaveAndExit();
                    return;
            }
        }
    }
    
    static void ShowMainMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("PomoFocus - 主菜单");
        Console.ResetColor();
        Console.WriteLine("─────────────────");
        Console.WriteLine("1. 添加任务");
        Console.WriteLine("2. 查看/完成任务");
        Console.WriteLine("3. 开始番茄钟");
        Console.WriteLine("Q. 退出");
        Console.WriteLine("─────────────────");
        Console.Write("请选择：");
    }
    
    static void AddTask()
    {
        Console.Clear();
        Console.Write("请输入任务名称：");
        string? name = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            taskManager.AddTask(name);
            dataStorage.SaveTasks(taskManager.GetAllTasks());
        }
        else
        {
            Console.WriteLine("任务名称不能为空！");
        }
        
        Console.WriteLine("\n按任意键返回菜单...");
        Console.ReadKey();
    }
    
    static void ShowTasksAndSelect()
    {
        Console.Clear();
        taskManager.ShowTasks();
        
        if (taskManager.GetAllTasks().Count > 0)
        {
            Console.WriteLine("\n请输入要完成的任务编号（0 返回）：");
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out int index) && index > 0)
            {
                // 修复1：直接传入 int 类型的 index
                taskManager.CompleteTask(index);
                dataStorage.SaveTasks(taskManager.GetAllTasks());
            }
        }
        
        Console.WriteLine("\n按任意键返回菜单...");
        Console.ReadKey();
    }
    
    static void StartPomodoro()
    {
        var uncompleted = taskManager.GetUncompletedTasks();
        if (uncompleted.Count == 0)
        {
            Console.WriteLine("没有未完成的任务，请先添加任务！");
            Console.ReadKey();
            return;
        }
        
        Console.Clear();
        Console.WriteLine("选择要专注的任务：");
        for (int i = 0; i < uncompleted.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {uncompleted[i].Name}");
        }
        
        Console.Write("请选择：");
        string? input = Console.ReadLine();
        
        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= uncompleted.Count)
        {
            var selectedTask = uncompleted[choice - 1];
            
            Console.WriteLine($"\n开始专注：【{selectedTask.Name}】");
            Console.WriteLine("按任意键开始...");
            Console.ReadKey();
            
            // 运行番茄钟
            RunPomodoroTimer(selectedTask);
            
            // 修复2：直接传入任务对象，而不是索引
            taskManager.IncrementPomodoro(selectedTask);
            dataStorage.SaveTasks(taskManager.GetAllTasks());
        }
    }
    
    static void RunPomodoroTimer(TaskItem currentTask)
    {
        int workMinutes = 25;
        int totalSeconds = workMinutes * 60;
        int remainingSeconds = totalSeconds;
        
        Console.Clear();
        Console.WriteLine($"正在专注：{currentTask.Name}\n");
        
        while (remainingSeconds > 0)
        {
            // 计算进度
            int percent = (totalSeconds - remainingSeconds) * 100 / totalSeconds;
            int progressLength = percent / 5;
            
            // 绘制进度条
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('=', progressLength));
            Console.ResetColor();
            Console.Write(new string('-', 20 - progressLength));
            Console.Write($"] {percent}%  ");
            
            // 显示剩余时间
            TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);
            Console.Write($"{time:mm\\:ss}");
            
            // 检测按键暂停
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.P)
                {
                    Console.WriteLine("\n已暂停，按 R 继续...");
                    while (Console.ReadKey(true).Key != ConsoleKey.R) { }
                    Console.Clear();
                    Console.WriteLine($"继续专注：{currentTask.Name}\n");
                }
                else if (key == ConsoleKey.Q)
                {
                    Console.WriteLine("\n已退出番茄钟");
                    return;
                }
            }
            
            // 重置光标位置并倒计时
            Console.SetCursorPosition(0, Console.CursorTop);
            Thread.Sleep(1000);
            remainingSeconds--;
        }
        
        Console.WriteLine("\n\n专注完成！干得漂亮！");
        
        // 播放提示音（仅 Windows）
        if (OperatingSystem.IsWindows())
        {
            Console.Beep(1000, 500);
        }
        
        Console.ReadKey();
    }
    
    static void SaveAndExit()
    {
        dataStorage.SaveTasks(taskManager.GetAllTasks());
        Console.Clear();
        Console.WriteLine("再见！保持专注！");
        Thread.Sleep(1000);
    }
}