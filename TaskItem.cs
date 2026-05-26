using System;

namespace PomoFocus;

public class TaskItem
{
    // 自动属性(C#特色语法)
    public string Name { get; set; }        // 任务名称
    public bool IsCompleted { get; set; }   // 完成情况
    public DateTime CreatedAt { get; set; } // 创建时间  ⬅️ 也建议改一下：CreateAt → CreatedAt
    public int PomodoroCount { get; set; }  // 专注次数 ⬅️ 改这里：PomodoCount → PomodoroCount
    
    public TaskItem(string name)
    {
        Name = name;
        IsCompleted = false;
        CreatedAt = DateTime.Now;  // ⬅️ 改这里
        PomodoroCount = 0;         // ⬅️ 改这里
    }
    
    public string Display()
    {
        string checkMark = IsCompleted ? "[YES]" : "[N O]";  // 建议改为 [ ] 更清晰
        return $"{checkMark} {Name} (专注: {PomodoroCount}次)";  // ⬅️ 改这里
    }
}