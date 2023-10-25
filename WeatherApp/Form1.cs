using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Security.Policy;

namespace WeatherApp
{
    public partial class FORECAST : Form
    {
        //APi key to get the current weather forecast by city name
        string api_key = "1eda8667331c2ed937700b3e72a0d839";
        public FORECAST()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", txtSearch.Text.Trim(),api_key);
                var json = web.DownloadString(url);
                WeatherInformation.root info = JsonConvert.DeserializeObject<WeatherInformation.root>(json);
                forecast_icon.ImageLocation = "https://api.openweathermap.org/img/w/" + info.weather[0].icon + ".png";
                
                
                lblCond.Text = info.weather[0].main;
                lblDetails.Text = info.weather[0].description;

                lblW_Speed.Text = info.wind.speed.ToString();
                lblPress.Text = info.main.pressure.ToString();

                lblS_rise.Text = convertDateTime(info.sys.sunrise).ToString()+" AM";
                lblS_set.Text = convertDateTime(info.sys.sunset).ToString()+" PM";
            }
        }

        //Method to retrieve date as convert date to correct format for day of sunrise and sunset...
        //...for current date in time
        DateTime convertDateTime(long msec)
        {
            DateTime day = new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc);
            day = day.AddSeconds(msec).ToLocalTime();
            return day;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
