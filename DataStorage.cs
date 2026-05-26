using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PomoFocus;

public class DataStorage
{
    private string filePath = "tasks.json";

    public void SaveTasks(List<TaskItem> tasks)
    {
        try
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
            Console.WriteLine("数据已自动保存");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存失败:{ex.Message}");
        }
    }

    public List<TaskItem> LoadTasks()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                // 修复1：添加 ? 表示可能为 null，并用 ?? 提供默认值
                List<TaskItem>? tasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
                
                if (tasks != null)
                {
                    Console.WriteLine($"已加载 {tasks.Count} 个任务");
                    return tasks;
                }
                else
                {
                    Console.WriteLine("文件内容为空，创建新数据");
                    return new List<TaskItem>();
                }
            }
            else
            {
                Console.WriteLine("未找到保存文件，将创建新数据");
                return new List<TaskItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"加载失败:{ex.Message}");
            // 修复2：添加 () 调用构造函数
            return new List<TaskItem>();
        }
    }
}