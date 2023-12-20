using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp.UIControllers
{
        
    public class User
    {
        public string language { set;  get; }
        public User()
        {
            
        }

        public void setLanguage(String language)
        {
            this.language = language;
        }
    }
}
