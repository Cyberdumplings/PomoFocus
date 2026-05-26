using System;
using System.Collections.Generic;
using System.Linq;

namespace PomoFocus;

public class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>(); // 存储所有任务的列表

    // 统计已完成数量
    private int GetCompletedCount()
    {
        return tasks.Count(t => t.IsCompleted);
    }
    
    // 添加任务
    public void AddTask(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("任务名称不能为空! ");
            return;
        }
        
        TaskItem task = new TaskItem(name);
        tasks.Add(task);
        Console.WriteLine($"已添加任务:{name}");
    }
    
    // 显示所有任务
    public void ShowTasks()
    {
        // 修复：Count 是属性，不是方法，不需要加 ()
        if (tasks.Count == 0)
        {
            Console.WriteLine("暂无任务，请先添加任务");
            return;
        }
        
        Console.WriteLine("\n今日任务列表:");
        Console.WriteLine("----------------");
        
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Display()}");
        }
        
        Console.WriteLine($"\n总计:{tasks.Count}个任务, 已完成:{GetCompletedCount()}");
    }

    // 完成任务（方法名改为 CompleteTask，与 Program.cs 调用一致）
    public void CompleteTask(int index)
    {
        if (index >= 1 && index <= tasks.Count)
        {
            TaskItem task = tasks[index - 1];
            task.IsCompleted = true;
            Console.WriteLine($"恭喜完成任务:{task.Name}");
        }
        else
        {
            Console.WriteLine("无效的任务编号!");
        }
    }
    
    // 完成任务（通过任务对象）
    public void CompleteTask(TaskItem task)
    {
        if (task != null)
        {
            task.IsCompleted = true;
            Console.WriteLine($"恭喜完成任务:{task.Name}");
        }
    }

    // 增加专注次数（通过索引）
    public void IncrementPomodoro(int index)
    {
        if (index >= 1 && index <= tasks.Count)
        {
            // 修复：属性名应该是 PomodoroCount（之前写成了 PomodoCount）
            tasks[index - 1].PomodoroCount++;
            Console.WriteLine($"已为任务添加一个番茄钟");
        }
    }
    
    // 增加专注次数（通过任务对象）- 给 Program.cs 用
    public void IncrementPomodoro(TaskItem task)
    {
        if (task != null)
        {
            task.PomodoroCount++;
            Console.WriteLine($"已为任务 [{task.Name}] 增加一个番茄钟");
        }
    }

    // 获取未完成的任务
    public List<TaskItem> GetUncompletedTasks()
    {
        return tasks.Where(t => !t.IsCompleted).ToList();
    }
    
    // 获取所有任务
    public List<TaskItem> GetAllTasks()
    {
        return tasks;
    }
    
    // 加载任务
    public void LoadTasks(List<TaskItem> loadedTasks)
    {
        tasks = loadedTasks ?? new List<TaskItem>();
    }
}