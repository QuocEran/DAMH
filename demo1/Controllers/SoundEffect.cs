using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms; 

namespace demo1.Controllers
{
    public class        SoundEffect : ApplicationContext
    {
        SoundPlayer player = new SoundPlayer();
        public SoundEffect()
        {

        }
        //Khởi tạo
        public SoundEffect(string soundPath)
        {
            TurnOnMusic(soundPath);
        }
        //Thực hiện mở nhạc
        private void TurnOnMusic(string soundPath)
        {
            try
            {
                player.SoundLocation = soundPath;
                player.PlayLooping();
            }
            catch { }
        }
    }
}
