using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;

namespace Point01Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly EPESEntities _context;

        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1(EPESEntities context)
        {
            InitializeComponent();
            _context = context;
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += OnTimedEvent;
            timer.Interval = 86400000; //(24 Hour) number in milisecinds  60000 = 1 Min.
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Day == 15)
            {
                GetDataPoint01();
            }
        }

        private void GetDataPoint01()
        {
            var office = _context.Offices.Where(d => d.Code != "00000000" && d.Code.Substring(5, 3) == "000");
            foreach (var item in office)
            {
                var url = "http://10.20.37.11:7072/serviceTier/webapi/All/officeId/" + item.Code + "/year/" + DateYear.ToString("D4") + "/month/" + DateTime.Now.AddMonths(-1).Month.ToString("D2");
                var tax = _download_serialized_json_data<RootObject>(url);
                var taxOwn = tax.TaxCol.FirstOrDefault(t => t.officeCode == item.Code);
                if (taxOwn != null)
                {
                    or.ResultMonthValue = taxOwn.CMcurrentYear;
                }
            }
        }

        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
    }
}
