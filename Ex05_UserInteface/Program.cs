using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_UserInteface
{
    public class Program
    {
        public static void Main()
        {
            GameSettingsForm newSettingForm = new GameSettingsForm();
            newSettingForm.ShowDialog();
        }
    }
}
