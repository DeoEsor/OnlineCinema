using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CinemaDesktop.MVVM.Commands;
using Microsoft.Win32;

namespace CinemaDesktop.MVVM.Model;

public class Message
{
    public Message()
    {
        DownloadCommand = new RelayCommand(o =>
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = FileName.Split('.').First();
            dlg.DefaultExt = FileName.Split('.')[1];
            dlg.Filter = $"Documents (.{dlg.DefaultExt})|*.{dlg.DefaultExt}";

            if(dlg.ShowDialog() == true)
                File.WriteAllText(dlg.FileName, Encoding.UTF8.GetString(MessageData));
        });
    }
    public enum MessageType
    {
        String,
        File
    }

    public ICommand DownloadCommand { get; set; }
    
    private string _fileName = String.Empty;

    public string FileName
    {
        get => _fileName;
        set => _fileName = value;
    }

    public bool IsFile => Type == MessageType.File;
    public MessageType Type { get; set; }
    public byte[] MessageData { get; set; }
}