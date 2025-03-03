using System;
using UnityEngine;

public static class DialogFileReader
{
    //静态类，使用正则表达式匹配对话文件中的对话，存储在一个数组中
    
    static string NormalPattern = @"^\[name\s*=\s*""([^""]*)""\]\[icon\s*=\s*""([^""]*)""\]:\s*(.*)";
    //[name = "person1"][icon = "test_icon"]: 你好
    
    static string NormalChoicePattern =
        @"^\[choice\s*=\s*(\d+)\]\[name\s*=\s*""([^""]*)""\]\[icon\s*=\s*""([^""]*)""\]:\s*(.*)";
    //[choice = 1][name = "person2"][icon = "test_icon"]: 测试a
    
    static string NarrationPattern = @"^\[narration\]:\s*(.*)";
    //[narration]: 这是一段旁白
    
    static string NarrationChoicePattern = @"^\[choice\s*=\s*(\d+)\]\[narration\]:\s*(.*)";
    //[choice = 1][narration]: 这是一段旁白

    static string BackgroundPattern = @"^#\s*background\s*:\s*(\w+)";
    //# background : test_background
    
    static string OptionNumberPattern = @"^\[option\s*=\s*(\d+)\]";
    //[option = 1]
    
    static string OptionPattern = @"^\[name\s*=\s*\""(person\d+)\""\]\[(\d+)\]:\s*(.*)";
    //[name = "person1"][1]: 选项1


    public static void ReadDialogFile(out OneDialog[] dialogDatabase,string file)
    {
        int WritingIndex = 0;
        string[] lines = System.IO.File.ReadAllLines(file);
        lines = Array.FindAll(lines, l => l.Contains('#') || l.Contains('['));
        dialogDatabase = new OneDialog[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            dialogDatabase[i] = new OneDialog();
        }

        for (int index = 0; index < dialogDatabase.Length; index++)
        {
            var line = lines[index];
            if (System.Text.RegularExpressions.Regex.IsMatch(line, NormalPattern))
            {
                //对话
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, NormalPattern);
                string name = match.Groups[1].Value;
                string icon = match.Groups[2].Value;
                string sentence = match.Groups[3].Value;
                
                dialogDatabase[WritingIndex].Speaker = name;
                dialogDatabase[WritingIndex].SpeakerIcon = icon;
                dialogDatabase[WritingIndex].Sentence = sentence;

                dialogDatabase[WritingIndex].state = OneDialog.DialogState.Normal;

                WritingIndex++;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(line, NormalChoicePattern))
            {
                //选项对话
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, NormalChoicePattern);
                string choice = match.Groups[1].Value; //以此选项才会显示
                string name = match.Groups[2].Value;
                string icon = match.Groups[3].Value;
                string sentence = match.Groups[4].Value;
                
                dialogDatabase[WritingIndex].NeedOption = int.Parse(choice);
                dialogDatabase[WritingIndex].Speaker = name;
                dialogDatabase[WritingIndex].SpeakerIcon = icon;
                dialogDatabase[WritingIndex].Sentence = sentence;

                dialogDatabase[WritingIndex].state = OneDialog.DialogState.NormalChoice;

                WritingIndex++;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(line, NarrationPattern))
            {
                //旁白
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, NarrationPattern);
                string sentence = match.Groups[1].Value;
                
                dialogDatabase[WritingIndex].Sentence = sentence;

                dialogDatabase[WritingIndex].state = OneDialog.DialogState.Narration;

                WritingIndex++;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(line, NarrationChoicePattern))
            {
                //选项旁白
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, NarrationChoicePattern);
                string choice = match.Groups[1].Value;
                string sentence = match.Groups[2].Value;
                
                dialogDatabase[WritingIndex].NeedOption = int.Parse(choice);
                dialogDatabase[WritingIndex].Sentence = sentence;

                dialogDatabase[WritingIndex].state = OneDialog.DialogState.NarrationChoice;

                WritingIndex++;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(line, BackgroundPattern))
            {
                //背景
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, BackgroundPattern);
                string background = match.Groups[1].Value;
                if (dialogDatabase != null)
                {
                    
                    dialogDatabase[WritingIndex].BackgroundName = background;

                    dialogDatabase[WritingIndex].state = OneDialog.DialogState.Background;
                }

                WritingIndex++;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(line, OptionNumberPattern))
            {
                //选项
                System.Text.RegularExpressions.Match match =
                    System.Text.RegularExpressions.Regex.Match(line, OptionNumberPattern);
                int OptionNumber = int.Parse(match.Groups[1].Value); //选项数量
                
                dialogDatabase[WritingIndex].OptionDialogs = new OneDialog[OptionNumber]; //初始化选项对话数组
                for (int i = 0; i < OptionNumber; i++)
                {
                    dialogDatabase[WritingIndex].OptionDialogs[i] = new OneDialog();
                }
                for (int i = 0; i < dialogDatabase[WritingIndex].OptionDialogs.Length; i++)
                {
                    //读取选项对话并存储
                    index++;
                    var matchDialog = System.Text.RegularExpressions.Regex.Match(lines[index], OptionPattern);
                    var matchNarration =
                        System.Text.RegularExpressions.Regex.Match(lines[index], NarrationPattern);
                    if (matchDialog.Success)
                    {
                        dialogDatabase[WritingIndex].OptionDialogs[i].Speaker = matchDialog.Groups[1].Value;
                        dialogDatabase[WritingIndex].OptionDialogs[i].NeedOption = int.Parse(matchDialog.Groups[2].Value);
                        dialogDatabase[WritingIndex].OptionDialogs[i].Sentence = matchDialog.Groups[3].Value;
                    }else if (matchNarration.Success)
                    {
                        dialogDatabase[WritingIndex].OptionDialogs[i].NeedOption = int.Parse(matchNarration.Groups[1].Value);
                        dialogDatabase[WritingIndex].OptionDialogs[i].Sentence = matchNarration.Groups[2].Value;
                    }
                    else
                    {
                        Debug.LogError("Option Dialog Error");
                    }

                }

                dialogDatabase[WritingIndex].state = OneDialog.DialogState.Option;

                WritingIndex++;

            }
        }
        dialogDatabase = Array.FindAll(dialogDatabase, d => d.state != OneDialog.DialogState.Init);
    }
}
